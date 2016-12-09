using KQAnalytics3.Models.Data;
using KQAnalytics3.Services.DataCollection;
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

            // Limit is the max number of log requests to return. Default 100
            Get("/query/{limit}", args =>
            {
                var itemLimit = args.limit ?? 100;
                return Response.AsJson(
                    (LogRequest[])DataLoggerService.QueryRequests(itemLimit).ToArray()
                );
            });
        }
    }
}