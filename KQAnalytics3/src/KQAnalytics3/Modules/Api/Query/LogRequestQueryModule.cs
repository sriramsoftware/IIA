using KQAnalytics3.Configuration.Access;
using KQAnalytics3.Services.Authentication;
using KQAnalytics3.Services.DataCollection;
using KQAnalytics3.Utilities;
using Nancy;
using Nancy.Security;
using System;

namespace KQAnalytics3.Modules.Api.Query
{
    public class LogRequestQueryModule : NancyModule
    {
        public LogRequestQueryModule()
        {
            var requiredClaims = ClientApiAccessValidator.GetAccessClaimListFromScopes(new [] {
                ApiAccessScope.Read,
                ApiAccessScope.QueryLogRequests
            });
            this.RequiresClaims();
            //this.RequiresClaims(/*... TODO ...*/);

            // Query Log Requests
            // Limit is the max number of log requests to return. Default 100
            Get("/query/logrequests/{limit:int}", async args =>
            {
                var itemLimit = args.limit as int? ?? 100;
                var data = await DataLoggerService.QueryRequestsAsync(itemLimit);
                return Response.AsJsonNet(data);
            });
        }
    }
}