using KQAnalytics3.Services.DataCollection;
using KQAnalytics3.Utilities;
using Nancy;
using Nancy.Security;

namespace KQAnalytics3.Modules
{
    public class DataQueryApiModule : NancyModule
    {
        public DataQueryApiModule() : base("/api")
        {
            // Require stateless auth
            this.RequiresAuthentication();

            // Query Log Requests
            // Limit is the max number of log requests to return. Default 100
            Get("/query/logrequests/{limit}", async args =>
            {
                var itemLimit = args.limit as int? ?? 100;
                var data = await DataLoggerService.QueryRequestsAsync(itemLimit);
                return Response.AsJsonNet(data);
            });

            // Query Tagged Requests
            // Tag is the tag to filter by
            // Limit is the max number of log requests to return. Default 100
            Get("/query/tagged/{limit}/{tag}", async args =>
            {
                var itemLimit = args.limit as int? ?? 100;
                var data = await DataLoggerService.QueryRequestsAsync(itemLimit);
                return Response.AsJsonNet(data);
            });

            // Query SessionData
            // Id is the ID of the session to find
            Get("/query/sessiondata/{id}", async args =>
            {
                var data = await SessionStorageService.GetSessionFromIdentifierAsync((string)args.id);
                return Response.AsJsonNet(data);
            });
        }
    }
}