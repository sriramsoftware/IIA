using ImageSharp;
using ImageSharp.Formats;
using IridiumIon.Analytics.Configuration;
using System.IO;

namespace IridiumIon.Analytics.Data
{
    public class TrackingImageProvider
    {
        public INAServerContext ServerContext { get; }

        public TrackingImageProvider(INAServerContext serverContext)
        {
            ServerContext = serverContext;
        }

        public Stream CreateTrackingPixel()
        {
            // Create tracking pixel image
            var outputStream = new MemoryStream();
            var trackingPixelImg = new Image(1, 1);
            using (var imgPixels = trackingPixelImg.Lock())
            {
                imgPixels[0, 0] = Color.Transparent;
            }
            trackingPixelImg.Save(outputStream, new PngEncoder());
            return outputStream;
        }
    }
}