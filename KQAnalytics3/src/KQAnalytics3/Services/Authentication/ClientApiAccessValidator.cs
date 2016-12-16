using KQAnalytics3.Configuration.Access;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace KQAnalytics3.Services.Authentication
{
    public static class ClientApiAccessValidator
    {
        public static IEnumerable<Claim> GetAuthClaims(ApiAccessKey accessKey)
        {
            throw new NotImplementedException();
        }
    }
}