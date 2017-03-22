using IridiumIon.Analytics.Configuration;
using IridiumIon.Analytics.Configuration.Access;
using System.Security.Claims;

namespace IridiumIon.Analytics.Services.Authentication
{
    public class ClientAuthenticationService
    {
        public INAServerContext ServerContext { get; set; }

        public ClientAuthenticationService(INAServerContext serverContext)
        {
            ServerContext = serverContext;
        }

        public NAAccessKey ResolveKey(string apiKey)
        {
            return ServerContext.ServerState.ApiKeys.FirstOrDefault(x => x.Key == apiKey);
        }

        public ClaimsPrincipal ResolveClientIdentity(string apiKey)
        {
            var currentKey = ResolveKey(apiKey);
            if (currentKey != null)
            {
                // Give client identity
                var accessValidator = new ClientApiAccessValidator();
                var keyAuthClaims = accessValidator.GetAuthClaims(currentKey);
                return new ClaimsPrincipal(new ClaimsIdentity(keyAuthClaims));
            }
            return null;
        }
    }
}