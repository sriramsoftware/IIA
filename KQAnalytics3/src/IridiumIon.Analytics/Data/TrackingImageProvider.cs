using ImageSharp;
using ImageSharp.Formats;
using System.IO;

namespace KQAnalytics3.Data
{
    public static class TrackingImageProvider
    {
        static TrackingImageProvider()
        {
        }

        public static Stream CreateTrackingPixel()
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