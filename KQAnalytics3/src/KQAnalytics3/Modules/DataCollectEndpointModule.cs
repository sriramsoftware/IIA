using KQAnalytics3.Data;
using Nancy;
using System;

namespace KQAnalytics3.Modules
{
    public class DataCollectEndpointModule : NancyModule
    {
        public DataCollectEndpointModule()
        {
            var processData = new Func<dynamic, object>(args =>
            {

                return "0";
            });
            Post("/k", processData);
            Get("/k.png", args =>
            {
                var processResult = processData(args);
                return Response.FromStream(TrackingImageProvider.CreateTrackingPixel(), "image/png");
            });
        }
    }
}