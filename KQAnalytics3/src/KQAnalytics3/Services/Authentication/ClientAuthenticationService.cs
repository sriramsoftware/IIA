using System.Security.Claims;

namespace KQAnalytics3.Services.Authentication
{
    public class ClientAuthenticationService
    {
        public static ClaimsPrincipal ResolveClientIdentity(string apiKey)
        {
            var currentKey = KQRegistry.KeyCache.FindKeyByKeyString(apiKey);
            if (currentKey != null)
            {
                // Give client identity
                var keyAuthClaims = ClientApiAccessValidator.GetAuthClaims(currentKey);
                return new ClaimsPrincipal(new ClaimsIdentity(keyAuthClaims));
                //return new ClaimsPrincipal(new GenericIdentity("data client", "stateless"));
            }
            return null;
        }
    }
}