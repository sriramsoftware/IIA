using IridiumIon.Analytics.Configuration.Access;
using IridiumIon.Analytics.Services.Authentication;
using IridiumIon.Analytics.Services.Authentication.Security;
using IridiumIon.Analytics.Services.DataCollection;
using IridiumIon.Analytics.Utilities;

namespace IridiumIon.Analytics.Modules.Api.Query
{
    public class SessionDataQueryModule : NABaseModule
    {
        public SessionDataQueryModule() : base("/api")
        {
            var accessValidator = new ClientApiAccessValidator();
            this.RequiresAllClaims(accessValidator.GetAccessClaimListFromScopes(new[] {
                ApiAccessScope.Read,
                ApiAccessScope.QuerySessionData
            }), accessValidator.GetAccessClaim(ApiAccessScope.Admin));

            // Query SessionData
            // Id is the ID of the session to find
            Get("/query/sessiondata/{id}", async args =>
            {
                var sessionStorageService = new SessionStorageService();
                var data = await sessionStorageService.GetSessionFromIdentifierAsync((string)args.id);
                return Response.AsJsonNet(data);
            });
        }
    }
}