
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyAds.Interfaces;
using MyAds.Entities;
using System.Net;

namespace MyAds.Middlewares
{
    public class UserAuthentication
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;
        private readonly IServiceProvider _serviceProvider;

        public UserAuthentication(RequestDelegate next, IConfiguration config, IServiceProvider serviceProvider)
        {
            _next = next;
            _serviceProvider = serviceProvider;
            _configuration = config;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var headers = context.Request.Headers;
            var token = GetTokenFromHeader(headers);

            if (!string.IsNullOrEmpty(token))
            {
                var secretKey = _configuration.GetValue<string>("Jwt:Secret");
                var key = Encoding.ASCII.GetBytes(secretKey!);

                try
                {
                    var tokenHandler = new JwtSecurityTokenHandler();
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

                    Console.WriteLine("middleware before userIdClaim != null, userIdClaim " + userIdClaim);

                    if (userIdClaim != null)
                    {

                        var userId = userIdClaim.Value;
                        Console.WriteLine("middleware userId " + userId);

                        var _users = _serviceProvider.GetService(typeof(IUserService)) as IUserService;
                        Console.WriteLine("middleware after _users");

                        if (_users != null)
                        {
                            var user = await _users.GetUserById(int.Parse(userId));
                            Console.WriteLine("middleware user checking");
                            if (user != null)
                            {
                                Console.WriteLine("middleware user is not null");
                                context.Items["User"] = user;
                            }
                        }
                    }
                }
                catch (Exception)
                {
                    // token validation failed
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
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
    }
}
