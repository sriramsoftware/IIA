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
                var data = await DataLoggerService.QueryRequests(itemLimit);
                return Response.AsJsonNet(data);
            });

            // Query SessionData
            // Id is the ID of the session to find
            Get("/query/sessiondata/{id}", async args =>
            {
                var data = await SessionStorageService.GetSessionFromIdentifier((string)args.id);
                return Response.AsJsonNet(data);
            });
        }
    }
}