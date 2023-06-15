
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;


namespace ClassyAdsServer.Middlewares
{
    public class UserAuthenticationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _configuration;

        public UserAuthenticationMiddleware(RequestDelegate next, IConfiguration config)
        {
            _next = next;
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

                    if (userIdClaim != null)
                    {
                        var userId = userIdClaim.Value;
                        context.Items["UserId"] = int.Parse(userId);
                    }
                }
                catch (Exception)
                {
                    context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                }
            }

            await _next(context);
        }

        private static string? GetTokenFromHeader(IHeaderDictionary headers)
        {
            var authHeader = headers["Authorization"].FirstOrDefault();
            if (authHeader != null && authHeader.StartsWith("Bearer "))
            {
                return authHeader["Bearer ".Length..];
            }
            else
            {
                return null;
            }
        }
    }
}
