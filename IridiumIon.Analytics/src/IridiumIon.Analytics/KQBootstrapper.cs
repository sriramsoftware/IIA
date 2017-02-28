using Nancy;
using Nancy.Bootstrapper;
using Nancy.TinyIoc;

namespace IridiumIon.Analytics
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