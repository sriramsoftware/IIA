using ImageSharp;
using ImageSharp.Formats;
using System.IO;

namespace KQAnalytics3.Data
{
    public static class TrackingImageProvider
    {
        public static Stream TrackingPixelStream { get; }

        static TrackingImageProvider()
        {
            // Create tracking pixel image
            var trackingPixelImg = new Image(1, 1);
            using (var imgPixels = trackingPixelImg.Lock())
            {
                imgPixels[0, 0] = Color.Transparent;
            }
            trackingPixelImg.Save(TrackingPixelStream, new PngEncoder());
        }
    }
}