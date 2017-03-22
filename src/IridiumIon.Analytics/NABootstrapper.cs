using AutoMapper;
using IridiumIon.Analytics.Configuration;
using IridiumIon.Analytics.Models.Data;
using IridiumIon.Analytics.PluginCore;
using IridiumIon.Analytics.Services.Authentication;
using IridiumIon.Analytics.Utilities;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Session;
using Nancy.TinyIoc;
using System.Linq;

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

            // Enable CORS
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                if (KQRegistry.ServerConfiguration.CorsOptions != null)
                {
                    foreach (var origin in KQRegistry.ServerConfiguration.CorsOptions.Origins)
                    {
                        ctx.Response.WithHeader("Access-Control-Allow-Origin", origin);
                    }
                    ctx.Response
                        .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                        .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
                }
            });

            // Set up whitelist/blacklist
            if (KQRegistry.ServerConfiguration.BlacklistConfiguration != null
                && KQRegistry.ServerConfiguration.BlacklistConfiguration.Enable
                && KQRegistry.ServerConfiguration.WhitelistConfiguration != null
                && KQRegistry.ServerConfiguration.WhitelistConfiguration.Enable)
            {
                throw new ConfigurationException("The whitelist and the blacklist cannot be enabled simultaneously");
            }

            if (KQRegistry.ServerConfiguration.BlacklistConfiguration != null
                && KQRegistry.ServerConfiguration.BlacklistConfiguration.Enable)
            {
                pipelines.BeforeRequest.AddItemToStartOfPipeline((ctx) =>
                {
                    var userAddr = ctx.Request.UserHostAddress;
                    if (KQRegistry.ServerConfiguration.BlacklistConfiguration.Hosts.Any(blacklistedAddress => WildcardMatcher.IsMatch(userAddr, blacklistedAddress)))
                    {
                        return new Response().WithStatusCode(HttpStatusCode.Forbidden);
                    }
                    return null; // Take no action
                });
            }

            if (KQRegistry.ServerConfiguration.WhitelistConfiguration != null
                && KQRegistry.ServerConfiguration.WhitelistConfiguration.Enable)
            {
                pipelines.BeforeRequest.AddItemToStartOfPipeline((ctx) =>
                {
                    var userAddr = ctx.Request.UserHostAddress;
                    if (KQRegistry.ServerConfiguration.BlacklistConfiguration.Hosts.Any(whitelistedAddress => WildcardMatcher.IsMatch(userAddr, whitelistedAddress)))
                    {
                        return null; // Pass it on
                    }
                    // Not on whitelist
                    return new Response().WithStatusCode(HttpStatusCode.Forbidden);
                });
            }

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