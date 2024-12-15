using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace LoanApi.Filters
{
    public class ValidateTokenClaimsFilter : IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // Access the current user's claims
            var user = context.HttpContext.User;
            var userIdClaim = user.FindFirst("id")?.Value;

            // Check if the 'id' claim is missing
            if (string.IsNullOrEmpty(userIdClaim))
            {
                context.Result = new UnauthorizedObjectResult(new { error = "Invalid token: 'id' claim is missing." });
            }
        }
    }
}
