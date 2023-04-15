using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;


namespace WebApplication1.Middlewares
{
    public class UserAuthentication
    {
        private readonly RequestDelegate _next;
        private readonly Database database;

        public UserAuthentication(RequestDelegate next, Database database)
        {
            _next = next;
            database = database;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Retrieve the authenticated user's claims
            var userClaims = context.User.Claims;

            // Extract the user's ID claim
            var userIdClaim = userClaims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                // Get the user ID from the claim
                var userId = userIdClaim.Value;

                // Retrieve the user from the database or another data store
                var user = await database.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (user != null)
                {
                    // Set the user object as a property on the HttpContext
                    context.Items["User"] = user;
                }
            }

            // Call the next middleware in the pipeline
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
