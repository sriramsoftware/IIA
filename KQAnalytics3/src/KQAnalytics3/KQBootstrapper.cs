using AutoMapper;
using KQAnalytics3.Models.Data;
using KQAnalytics3.PluginCore;
using KQAnalytics3.Services.Authentication;
using KQAnalytics3.Utilities;
using Nancy;
using Nancy.Authentication.Stateless;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Session;
using Nancy.TinyIoc;

namespace KQAnalytics3
{
    public class KQBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
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
                    foreach (var blacklistedAddress in KQRegistry.ServerConfiguration.BlacklistConfiguration.Hosts)
                    {
                        if (WildcardMatcher.IsMatch(userAddr, blacklistedAddress))
                        {
                            return new Response().WithStatusCode(HttpStatusCode.Forbidden);
                        }
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
                    foreach (var whitelistedAddress in KQRegistry.ServerConfiguration.BlacklistConfiguration.Hosts)
                    {
                        if (WildcardMatcher.IsMatch(userAddr, whitelistedAddress))
                        {
                            return null; // Pass it on
                        }
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
    }
}