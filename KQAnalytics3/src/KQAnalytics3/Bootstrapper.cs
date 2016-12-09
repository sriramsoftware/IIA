using AutoMapper;
using KQAnalytics3.Configuration;
using KQAnalytics3.Models.Data;
using KQAnalytics3.Utilities;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Conventions;
using Nancy.Session;
using Nancy.TinyIoc;
using Newtonsoft.Json;
using System.IO;

namespace KQAnalytics3
{
    public class Bootstrapper : DefaultNancyBootstrapper
    {
        protected override void ConfigureConventions(NancyConventions nancyConventions)
        {
            base.ConfigureConventions(nancyConventions);
            nancyConventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("assets", "wwwroot/assets/")
            );
            nancyConventions.StaticContentsConventions.Add(
                StaticContentConventionBuilder.AddDirectory("static", "wwwroot/static/")
            );
        }

        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);

            // Read KQConfig configuration file
            var configFileCont = File.ReadAllText("kqconfig.json");
            KQRegistry.ServerConfiguration = JsonConvert.DeserializeObject<KQServerConfiguration>(configFileCont);

            // TODO: Do any required initialization for Nancy here
            CookieBasedSessions.Enable(pipelines);

            // Enable CORS
            pipelines.AfterRequest.AddItemToEndOfPipeline((ctx) =>
            {
                foreach (var origin in KQRegistry.ServerConfiguration.CorsOptions.Origins)
                {
                    ctx.Response.WithHeader("Access-Control-Allow-Origin", origin);
                }
                ctx.Response
                    .WithHeader("Access-Control-Allow-Methods", "POST,GET")
                    .WithHeader("Access-Control-Allow-Headers", "Accept, Origin, Content-type");
            });

            // Set up whitelist/blacklist
            if (KQRegistry.ServerConfiguration.BlacklistConfiguration.Enable && KQRegistry.ServerConfiguration.WhitelistConfiguration.Enable)
            {
                throw new ConfigurationException("The whitelist and the blacklist cannot be enabled simultaneously");
            }

            if (KQRegistry.ServerConfiguration.BlacklistConfiguration.Enable)
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

            if (KQRegistry.ServerConfiguration.WhitelistConfiguration.Enable)
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
                cfg.CreateMap<LogRequest, RedirectRequest>();
            });
        }
    }
}