using Nancy;
using System.Collections.Generic;
using System.Security.Claims;

namespace KQAnalytics3.Services.Authentication.Security
{
    public static class ApiAccessModuleSecurityExtensions
    {
        public static void RequiresAllClaims(this NancyModule module, IEnumerable<Claim> claims, Claim adminClaim = null)
        {
            module.Before.AddItemToEndOfPipeline((ctx) =>
            {
                if (ctx.CurrentUser == null)
                {
                    return HttpStatusCode.Unauthorized;
                }
                // Override claim
                if (ctx.CurrentUser.EnsureClaim(adminClaim))
                {
                    return null;
                }
                // Make sure all claims are available
                foreach (var testClaim in claims)
                {
                    if (!ctx.CurrentUser.EnsureClaim(testClaim))
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                }
                return null;
            });
        }

        public static bool EnsureClaim(this ClaimsPrincipal principal, Claim claim)
        {
            return principal.HasClaim(claim.Type, claim.Value);
        }
    }
}