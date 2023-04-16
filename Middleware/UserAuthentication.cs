
using Microsoft.IdentityModel.Tokens;
using MyAds.Entities;
using MyAds.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace MyAds.Middlewares
{
    public class UserAuthentication
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;


        public UserAuthentication(RequestDelegate next, IUserService userService, IConfiguration config)
        {
            _next = next;
            _userService = userService;
            _configuration = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var headers = context.Request.Headers;
            var token = GetTokenFromHeader(headers);

            if (!string.IsNullOrEmpty(token))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var secretKey = _configuration.GetValue<string>("Jwt:Secret");
                var key = Encoding.ASCII.GetBytes(secretKey!);

                try
                {
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ClockSkew = TimeSpan.Zero
                    };

                    var claimsPrincipal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);
                    var userIdClaim = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

                    if (userIdClaim != null)
                    {
                        var userId = userIdClaim.Value;

                        var user = _userService.GetUserById(int.Parse(userId));

                        if (user != null)
                        {
                            context.Items["User"] = user;
                        }
                    }
                }
                catch (Exception)
                {
                    // Token validation failed
                }
            }

            await _next(context);
        }

        private string? GetTokenFromHeader(IHeaderDictionary headers)
        {
            var authHeader = headers["Authorization"].FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                return authHeader.Substring("Bearer ".Length);
            }
            else
            {
                return null;
            }
        }

        public string CreateUserToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var secretKey = _configuration.GetValue<string>("Jwt:Key");
            var key = Encoding.ASCII.GetBytes(secretKey!);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, user.Username),
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }


    public static class UserAuthenticationExtensions
    {
        public static IApplicationBuilder UseUserAuthentication(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<UserAuthentication>();
        }
    }
}
