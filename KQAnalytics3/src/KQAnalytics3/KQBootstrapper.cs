using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace KQAnalytics3
{
    public class KQBootstrapper : DefaultNancyBootstrapper
    {
        protected override void ApplicationStartup(TinyIoCContainer container, IPipelines pipelines)
        {
            base.ApplicationStartup(container, pipelines);
            KQBootstrapperHelper.HandleApplicationStartup(container, pipelines);
        }
    }
}