using IridiumIon.Analytics.Configuration;
using IridiumIon.Analytics.Configuration.Access;
using Nancy;
using OsmiumSubstrate.Services.Authentication;
using OsmiumSubstrate.Services.Authentication.Security;
using OsmiumSubstrate.Utilities;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace IridiumIon.Analytics.Modules.Security
{
    public class KeyManagementModule : NABaseModule
    {
        public INAServerContext ServerContext { get; set; }

        public KeyManagementModule(INAServerContext serverContext) : base("/km")
        {
            ServerContext = serverContext;

            var accessValidator = new StatelessClientValidator<NAAccessKey, NAApiAccessScope>();
            this.RequiresAllClaims(new[] { accessValidator.GetAccessClaim(NAApiAccessScope.Admin) });

            // API key management
            Post("/keys/create/{keyid}", HandleCreateKeyRequestAsync);
            Get("/keys/get/{keyid}", HandleGetKeyRequestAsync);
            Get("/keys/list", HandleListKeyRequestAsync);
            Delete("/keys/delete/{keyid}", HandleDeleteKeyRequestAsync);

            // Persist state after successful request
            After += ctx =>
            {
                if (ctx.Response.StatusCode == HttpStatusCode.OK)
                {
                    ServerContext.ServerState.Persist();
                }
            };
        }

        private async Task<Response> HandleCreateKeyRequestAsync(dynamic args)
        {
            // Parameters:
            var keyid = ((string)args.keyid);
            var keyScopeParam = ((string)Request.Query.scopes);
            if (keyScopeParam == null) return HttpStatusCode.BadRequest;
            var keyScopes = keyScopeParam.Split('|');
            return await Task.Run(() =>
            {
                try
                {
                    var key = new NAAccessKey
                    {
                        Key = keyid,
                        AccessScopes = keyScopes.Select(x => (NAApiAccessScope)Enum.Parse(typeof(NAApiAccessScope), x)).ToArray()
                    };

                    // Store key
                    lock (ServerContext.ServerState.ApiKeys)
                    {
                        ServerContext.ServerState.ApiKeys.Add(key);
                    }
                    return Response.AsJsonNet(key);
                }
                catch (ArgumentException)
                {
                    return HttpStatusCode.BadRequest;
                }
            });
        }

        private async Task<Response> HandleGetKeyRequestAsync(dynamic args)
        {
            return await Task.Run(() =>
            {
                var keyid = ((string)args.keyid);
                var key = ServerContext.ServerState.ApiKeys.FirstOrDefault(x => x.Key == keyid);
                if (key == null) return HttpStatusCode.NotFound;
                return Response.AsJsonNet(key);
            });
        }

        private async Task<Response> HandleListKeyRequestAsync(dynamic args)
        {
            return await Task.Run(() =>
            {
                return Response.AsJsonNet(ServerContext.ServerState.ApiKeys);
            });
        }

        private async Task<Response> HandleDeleteKeyRequestAsync(dynamic args)
        {
            return await Task.Run(() =>
            {
                var keyid = ((string)args.keyid);
                var key = ServerContext.ServerState.ApiKeys.FirstOrDefault(x => x.Key == keyid);
                if (key == null) return HttpStatusCode.NotFound;
                lock (ServerContext.ServerState.ApiKeys)
                {
                    ServerContext.ServerState.ApiKeys.Remove(key);
                }
                return HttpStatusCode.OK;
            });
        }
    }
}