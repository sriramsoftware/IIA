using KQAnalytics3.Configuration.Access;
using KQAnalytics3.Services.Authentication;
using KQAnalytics3.Services.Authentication.Security;
using KQAnalytics3.Services.DataCollection;
using KQAnalytics3.Utilities;

namespace KQAnalytics3.Modules.Api.Query
{
    public class SessionDataQueryModule : KQBaseModule
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