using IridiumIon.Analytics.Metadata;

namespace IridiumIon.Analytics.Modules.WebClient
{
    public class WebClientInfoModule : NABaseModule
    {
        public WebClientInfoModule()
        {
            Get("/info", _ => $"{KQAnalyticsServerInfo.KQAnalyticsProductName}");
        }
    }
}