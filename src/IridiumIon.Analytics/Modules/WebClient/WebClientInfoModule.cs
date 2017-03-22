using IridiumIon.Analytics.Metadata;

namespace IridiumIon.Analytics.Modules.WebClient
{
    public class WebClientInfoModule : NABaseModule
    {
        public WebClientInfoModule() : base("/")
        {
            Get("/info", _ => $"{NAServerInfo.IIAProductName}");
        }
    }
}