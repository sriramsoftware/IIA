using KQAnalytics3.Configuration.Access;
using KQAnalytics3.Services.Authentication;
using KQAnalytics3.Services.Authentication.Security;
using KQAnalytics3.Services.DataCollection;
using KQAnalytics3.Utilities;
using Nancy;

namespace KQAnalytics3.Modules.Api.Query
{
    public class TagRequestQueryModule : NancyModule
    {
        public TagRequestQueryModule() : base("/api")
        {
            this.RequiresAllClaims(ClientApiAccessValidator.GetAccessClaimListFromScopes(new[] {
                ApiAccessScope.Read,
                ApiAccessScope.QueryTagRequests
            }), ClientApiAccessValidator.GetAccessClaim(ApiAccessScope.Admin));

            // Query Tagged Requests
            // Tag is the tag to filter by
            // Limit is the max number of log requests to return. Default 100
            Get("/query/tagged/{limit:int}/{tags?}", async args =>
            {
                var itemLimit = args.limit as int? ?? 100;
                var filterTags = (args != null) ? ((string)args.tags).Split(',') : null;
                var data = await DataLoggerService.QueryTaggedRequestsAsync(itemLimit, filterTags);
                return Response.AsJsonNet(data);
            });
        }
    }
}