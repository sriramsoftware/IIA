using KQAnalytics3.Configuration.Access;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace KQAnalytics3.Services.Authentication
{
    public static class ClientApiAccessValidator
    {
        public static string AuthTypeKey => "authType";
        public static string AccessScopeKey => "accessScope";

        public static IEnumerable<Claim> GetAuthClaims(ApiAccessKey accessKey)
        {
            var claimList = new List<Claim>
            {
                new Claim(AuthTypeKey, RemoteAuthTypes.StatelessKey),
            };
            var accessScopeClaims = accessKey.AccessScopes.Select(accessScope => new Claim(AccessScopeKey, accessScope.ToString()));
            claimList.AddRange(accessScopeClaims);
            return claimList;
        }

        public static Claim GetAccessClaim(ApiAccessScope scope)
        {
            return new Claim(AccessScopeKey, scope.ToString());
        }

        public static ApiAccessScope GetAccessScope(Claim accessScopeClaim)
        {
            return (ApiAccessScope)Enum.Parse(typeof(ApiAccessScope), accessScopeClaim.Value);
        }

        public static IEnumerable<Claim> GetAccessClaimListFromScopes(ApiAccessScope[] scopes)
        {
            return scopes.Select(x => new Claim(AccessScopeKey, x.ToString()));
        }
    }
}