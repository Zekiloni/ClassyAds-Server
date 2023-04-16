
using MyAds.Interfaces;
using System.Security.Claims;

namespace MyAds.Middlewares
{
    public class UserAuthentication
    {
        private readonly RequestDelegate _next;
        private readonly IUserService _userService;

        public UserAuthentication(RequestDelegate next, IUserService userService)
        {
            _next = next;
            _userService = userService;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var userClaims = context.User.Claims;

            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                var userId = userIdClaim.Value;

                var user = _userService.GetUserById(int.Parse(userId));

                if (user != null)
                {
                    context.Items["User"] = user;
                }
            }

            await _next(context);
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
