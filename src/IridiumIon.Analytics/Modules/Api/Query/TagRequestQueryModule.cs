using IridiumIon.Analytics.Configuration.Access;
using IridiumIon.Analytics.Services.Authentication;
using IridiumIon.Analytics.Services.Authentication.Security;
using IridiumIon.Analytics.Services.DataCollection;
using IridiumIon.Analytics.Utilities;

namespace IridiumIon.Analytics.Modules.Api.Query
{
    public class TagRequestQueryModule : IIABaseModule
    {
        public TagRequestQueryModule() : base("/api")
        {
            var accessValidator = new ClientApiAccessValidator();
            this.RequiresAllClaims(accessValidator.GetAccessClaimListFromScopes(new[] {
                ApiAccessScope.Read,
                ApiAccessScope.QueryTagRequests
            }), accessValidator.GetAccessClaim(ApiAccessScope.Admin));

            // Query Tagged Requests
            // Tag is the tag to filter by
            // Limit is the max number of log requests to return
            Get("/query/tagged/{tags}/{limit:int}", async args =>
            {
                var itemLimit = args.limit as int? ?? 100;
                var filterTags = (args.tags != null) ? ((string)args.tags).Split(',') : null;
                var dataLoggerService = new DataLoggerService();
                var data = await dataLoggerService.QueryTaggedRequestsAsync(itemLimit, filterTags);
                return Response.AsJsonNet(data);
            });
        }
    }
}