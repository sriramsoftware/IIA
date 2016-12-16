using KQAnalytics3.Metadata;
using Nancy;

namespace KQAnalytics3.Modules.WebClient
{
    public class WebClientInfoModule : NancyModule
    {
        public WebClientInfoModule()
        {
            Get("/info", _ => $"{KQAnalyticsServerInfo.KQAnalyticsProductName}");
        }
    }
}