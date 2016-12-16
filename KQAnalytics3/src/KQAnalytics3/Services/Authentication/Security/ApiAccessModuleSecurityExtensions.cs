using Nancy;
using System.Collections.Generic;
using System.Security.Claims;

namespace KQAnalytics3.Services.Authentication.Security
{
    public static class ApiAccessModuleSecurityExtensions
    {
        public static void RequiresAllClaims(this NancyModule module, IEnumerable<Claim> claims)
        {
            module.Before.AddItemToEndOfPipeline((ctx) =>
            {
                if (ctx.CurrentUser == null)
                {
                    return HttpStatusCode.Unauthorized;
                }
                // Make sure all claims are available
                foreach (var testClaim in claims)
                {
                    if (!ctx.CurrentUser.HasClaim(testClaim.Type, testClaim.Value))
                    {
                        return HttpStatusCode.Unauthorized;
                    }
                }
                return null;
            });
        }
    }
}