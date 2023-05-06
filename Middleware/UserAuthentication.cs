
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MyAds.Interfaces;
using MyAds.Entities;

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

                        var _users = _serviceProvider.GetService(typeof(IUserService)) as IUserService;

                        if (_users != null)
                        {
                            var user = _users.GetUserById(int.Parse(userId));

                            if (user != null)
                            {
                                context.Items["User"] = user;
                            }
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
    }
}
