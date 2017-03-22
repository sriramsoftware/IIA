using System.Security.Claims;

namespace IridiumIon.Analytics.Services.Authentication
{
    public class ClientAuthenticationService
    {
        public static ClaimsPrincipal ResolveClientIdentity(string apiKey)
        {
            var currentKey = KQRegistry.KeyCache.FindKeyByKeyString(apiKey);
            if (currentKey != null)
            {
                // Give client identity
                var accessValidator = new ClientApiAccessValidator();
                var keyAuthClaims = accessValidator.GetAuthClaims(currentKey);
                return new ClaimsPrincipal(new ClaimsIdentity(keyAuthClaims));
                //return new ClaimsPrincipal(new GenericIdentity("data client", "stateless"));
            }
            return null;
        }
    }
}