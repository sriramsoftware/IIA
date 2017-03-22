namespace IridiumIon.Analytics
{
    using IridiumIon.Analytics.Configuration;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Nancy.Owin;

    public class Startup
    {
        public const string ServerParametersConfigurationFileName = "iianalytics.json";
        public const string ServerStateStorageFileName = "iiaserver_state.lidb";

        private readonly IConfigurationRoot config;
        private NAServerContext serverContext;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                              .AddJsonFile(ServerParametersConfigurationFileName,
                                optional: true,
                                reloadOnChange: true)
                              .SetBasePath(env.ContentRootPath);

            config = builder.Build();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // Adds services required for using options.
            services.AddOptions();
            // Register IConfiguration
            services.Configure<NAServerParameters>(config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IApplicationLifetime applicationLifetime)
        {
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Create default server parameters
            var serverParameters = new NAServerParameters
            {
                DatabaseConfiguration = new NADatabaseConfiguration()
            };
            // Bind configuration file data to server parameters
            config.Bind(serverParameters);
            // Create a server context from the parameters
            serverContext = NAServerConfigurator.CreateContext(serverParameters);

            // Register shutdown
            applicationLifetime.ApplicationStopping.Register(OnShutdown);

            // Load persistent state data
            NAServerConfigurator.LoadState(serverContext, ServerStateStorageFileName);

            app.UseOwin(x => x.UseNancy(opt => opt.Bootstrapper = new NABootstrapper(serverContext)));
        }

        private void OnShutdown()
        {
            // Persist server state
            serverContext.ServerState.Persist();
        }
    }
}