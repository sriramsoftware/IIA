using IridiumIon.Analytics.Services.Resources;
using System.IO;

namespace IridiumIon.Analytics.Data
{
    public static class TrackingScriptProvider
    {
        public static string TrackingScriptName => "kq3.js";

        public static Stream CreateTrackingScript()
        {
            return new ResourceProviderService().GetResource(TrackingScriptName);
        }
    }
}