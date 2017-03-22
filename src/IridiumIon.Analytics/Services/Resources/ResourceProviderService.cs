using IridiumIon.Analytics.Configuration;
using IridiumIon.Analytics.Data;
using System.IO;

namespace IridiumIon.Analytics.Services.Resources
{
    public class ResourceProviderService
    {
        public string ResourceBasePath { get; }

        public INAServerContext ServerContext { get; }

        public TrackingScriptProvider TrackingScript { get; set; }
        public TrackingImageProvider TrackingImage { get; set; }

        public ResourceProviderService(INAServerContext serverContext)
        {
            ServerContext = serverContext;
            ResourceBasePath = Path.Combine(ServerContext.Parameters.BaseDirectory, "Resources", "lib");
            TrackingScript = new TrackingScriptProvider(serverContext);
            TrackingImage = new TrackingImageProvider(serverContext);
        }

        public Stream GetResource(string resourcePath)
        {
            var resCompletePath = Path.Combine(ResourceBasePath, resourcePath);
            return File.OpenRead(resCompletePath);
        }

        public string ReadResourceContents(string resourcePath)
        {
            var result = string.Empty;
            using (var sr = new StreamReader(GetResource(resourcePath)))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
    }
}