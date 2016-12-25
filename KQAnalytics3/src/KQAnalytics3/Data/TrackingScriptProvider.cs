using KQAnalytics3.Services.Resources;
using System.IO;

namespace KQAnalytics3.Data
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