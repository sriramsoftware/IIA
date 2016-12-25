using KQAnalytics3.Metadata;

namespace KQAnalytics3.Modules.WebClient
{
    public class WebClientInfoModule : KQBaseModule
    {
        public WebClientInfoModule()
        {
            Get("/info", _ => $"{KQAnalyticsServerInfo.KQAnalyticsProductName}");
        }
    }
}