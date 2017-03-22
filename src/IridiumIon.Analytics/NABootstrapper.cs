using AutoMapper;
using IridiumIon.Analytics.Configuration;
using IridiumIon.Analytics.Models.Data;
using IridiumIon.Analytics.Services.Authentication;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Session;
using Nancy.TinyIoc;

namespace IridiumIon.Analytics
{
    public class NABootstrapper : DefaultNancyBootstrapper
    {
        public NAServerContext ServerContext { get; set; }

        public NABootstrapper(NAServerContext serverContext)
        {
            ServerContext = serverContext;
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            // Connect to database
            ServerContext.ConnectDatabase();

            // TODO (Disabled): Load plugins

            // Enable cookie sessions
            CookieBasedSessions.Enable(pipelines);

            // Enable authentication
            StatelessAuthentication.Enable(pipelines, new StatelessAuthenticationConfiguration(ctx =>
            {
                // Take API from query string
                var apiKey = (string)ctx.Request.Query.apikey.Value;

                // get user identity
                var authenticator = new ClientAuthenticationService(ServerContext);
                return authenticator.ResolveClientIdentity(apiKey);
            }));

            // Enable CORS
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                foreach (var origin in ServerContext.Parameters.CorsOrigins)
                {
                    ctx.Response.WithHeader("Access-Control-Allow-Origin", origin);
                }
                ctx.Response
                    .WithHeader("Access-Control-Allow-Methods", "GET,POST,PUT,DELETE")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
            });

            // Initialize object data mapper
            Mapper.Initialize(cfg =>
            {
                // Create maps
                cfg.CreateMap<LogRequest, HitRequest>();
                cfg.CreateMap<HitRequest, FetchScriptRequest>();
                cfg.CreateMap<LogRequest, RedirectRequest>();
                cfg.CreateMap<LogRequest, TagRequest>();
            });
        }

        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            base.ConfigureApplicationContainer(container);

            // Register IoC components
            container.Register<INAServerContext>(ServerContext);
        }
    }
}