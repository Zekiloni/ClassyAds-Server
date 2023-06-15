using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ClassyAdsServer.Interfaces;
using ClassyAdsServer.Middlewares;
using ClassyAdsServer.Services;
using System.Text;
using System.Text.Json.Serialization;


IConfigurationRoot configuration = new ConfigurationBuilder()
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json")
       .AddJsonFile("Configs/adConfig.json")
       .Build();

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");


builder.Services.AddDbContextPool<DatabaseContext>(options =>
{
    options.UseMySQL(connectionString!, mysqlOptions =>
    {
        mysqlOptions.EnableRetryOnFailure(1, TimeSpan.FromSeconds(5), null);
    });
});

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        });

// Application services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<IAdvertisementService, AdvertisementService>();
builder.Services.AddScoped<IAdvertisementMediaService, AdvertisementMediaFileService>();


// Adding controllers
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var key = Encoding.ASCII.GetBytes(configuration["Jwt:Secret"]!);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});


try
{
    var app = builder.Build();

    // pipeline
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
    }

    app.UseHttpsRedirection();

    app.UseCors(builder => {
        builder.WithOrigins("http://localhost:8080")
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowCredentials();
    });

    app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<UserAuthenticationMiddleware>(configuration);

    app.MapControllers();


    app.Run();
}
catch (Exception e)
{
    Console.Write(e);
}


