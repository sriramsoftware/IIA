using IridiumIon.Analytics.Configuration;
using IridiumIon.Analytics.Services.Resources;
using System.IO;

namespace IridiumIon.Analytics.Data
{
    public class TrackingScriptProvider
    {
        public string TrackingScriptName => "ia3.js";

        public INAServerContext ServerContext { get; }

        public TrackingScriptProvider(INAServerContext serverContext)
        {
            ServerContext = serverContext;
        }

        public Stream CreateTrackingScript()
        {
            return new ResourceProviderService(ServerContext).GetResource(TrackingScriptName);
        }
    }
}