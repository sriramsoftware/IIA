using KQAnalytics3.Models.Data;
using KQAnalytics3.Services.DataCollection;
using Nancy;
using Nancy.Security;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace KQAnalytics3.Modules
{
    public class DataQueryApiModule : NancyModule
    {
        public DataQueryApiModule() : base("/api")
        {
            // Require stateless auth
            this.RequiresAuthentication();

            // Limit is the max number of log requests to return. Default 100
            Get("/query/{limit}", async args =>
            {
                var itemLimit = args.limit ?? 100;
                var data = await DataLoggerService.QueryRequests(itemLimit);
                var responseData = (string)JsonConvert.SerializeObject(data);
                return Response.AsText(responseData, "application/json");
            });
        }
    }
}