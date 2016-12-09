using AutoMapper;
using KQAnalytics3.Data;
using KQAnalytics3.Models.Requests;
using Nancy;
using System;

namespace KQAnalytics3.Modules
{
    public class DataCollectEndpointModule : NancyModule
    {
        public DataCollectEndpointModule()
        {
            // Tracking Post
            Post("/k", args =>
            {
                ProcessRequestData(DataRequestType.Log | DataRequestType.Hit | DataRequestType.Web);
                return new Response().WithStatusCode(HttpStatusCode.OK);
            });
            // Tracking image
            Get("/k.png", args =>
            {
                ProcessRequestData(DataRequestType.Log | DataRequestType.Hit | DataRequestType.Web);
                // Send tracking pixel
                return Response.FromStream(TrackingImageProvider.CreateTrackingPixel(), "image/png");
            });
            // Redirect
            Get("/r", args =>
            {
                ProcessRequestData(DataRequestType.Log | DataRequestType.Redirect | DataRequestType.Web);
                var targetUrl = (string)Request.Query.t;
                if (targetUrl == null) return new Response().WithStatusCode(HttpStatusCode.BadRequest);
                // Do additional data logging
                return Response.AsRedirect(targetUrl);
            });
        }

        /// <summary>
        /// Process and log the request and associated data
        /// </summary>
        private void ProcessRequestData(DataRequestType requestType = DataRequestType.Log)
        {
            var eventIdentifier = Guid.NewGuid();
            if (requestType.HasFlag(DataRequestType.Log))
            {
                var req = new LogRequest { Identifier = eventIdentifier };
                if (requestType.HasFlag(DataRequestType.Hit))
                {
                    var hitReq = Mapper.Map<HitRequest>(req);
                    
                    req = hitReq;
                }
            }
        }
    }
}