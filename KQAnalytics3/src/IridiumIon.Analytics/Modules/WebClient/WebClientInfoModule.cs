using IridiumIon.Analytics.Metadata;

namespace IridiumIon.Analytics.Modules.WebClient
{
    public class WebClientInfoModule : KQBaseModule
    {
        public WebClientInfoModule()
        {
            Get("/info", _ => $"{KQAnalyticsServerInfo.KQAnalyticsProductName}");
        }
    }
}