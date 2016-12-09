using System.Linq;
using System.Security.Claims;
using System.Security.Principal;

namespace KQAnalytics3.Services.Authentication
{
    public class ClientAuthenticationService
    {
        public static ClaimsPrincipal ResolveClientIdentity(string apiKey)
        {
            // TODO!
            if (KQRegistry.ServerConfiguration.DataApiKeys.Contains(apiKey))
            {
                // Give client identity
                return new ClaimsPrincipal(new GenericIdentity("data client", "stateless"));
            }
            return null;
        }
    }
}