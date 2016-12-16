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