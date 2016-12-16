using KQAnalytics3.Configuration.Access;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace KQAnalytics3.Services.Authentication
{
    public static class ClientApiAccessValidator
    {
        public static string AuthTypeKey => "authType";
        public static string AccessScopeKey => "accessScope";

        public static IEnumerable<Claim> GetAuthClaims(ApiAccessKey accessKey)
        {
            return new Claim[]
            {
                new Claim(AuthTypeKey, "stateless"),
                new Claim(AccessScopeKey, accessKey.AccessScope.ToString())
            };
        }

        public static ApiAccessScope GetAccessScope(Claim accessScopeClaim)
        {
            return (ApiAccessScope)Enum.Parse(typeof(ApiAccessScope), accessScopeClaim.Value);
        }
    }
}