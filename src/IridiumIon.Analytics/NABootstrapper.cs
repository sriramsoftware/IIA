using AutoMapper;
using IridiumIon.Analytics.Configuration;
using IridiumIon.Analytics.Models.Data;
using IridiumIon.Analytics.PluginCore;
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

            // Load plugins
            KQPluginAggregator.LoadAllPlugins();

            // Enable cookie sessions
            CookieBasedSessions.Enable(pipelines);

            // Enable authentication
            StatelessAuthentication.Enable(pipelines, new StatelessAuthenticationConfiguration(ctx =>
            {
                // Take API from query string
                var apiKey = (string)ctx.Request.Query.apikey.Value;

                // get user identity
                return ClientAuthenticationService.ResolveClientIdentity(apiKey);
            }));

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