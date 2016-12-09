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
            // Tracking Post
            Post("/k", processData);
            // Tracking image
            Get("/k.png", args =>
            {
                var processResult = processData(args);
                return Response.FromStream(TrackingImageProvider.CreateTrackingPixel(), "image/png");
            });
            // Redirect
            Get("/r", args =>
            {
                var processResult = processData(args);
                var targetUrl = (string)Request.Query.t;
                if (targetUrl == null) return new Response().WithStatusCode(HttpStatusCode.BadRequest);
                // Do additional data logging
                return Response.AsRedirect(targetUrl);
            });
        }
    }
}