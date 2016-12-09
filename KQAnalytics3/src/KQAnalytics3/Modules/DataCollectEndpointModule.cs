using AutoMapper;
using KQAnalytics3.Data;
using KQAnalytics3.Models.Data;
using KQAnalytics3.Models.Requests;
using KQAnalytics3.Services.DataCollection;
using Nancy;
using System;
using System.Threading.Tasks;

namespace KQAnalytics3.Modules
{
    public class DataCollectEndpointModule : NancyModule
    {
        public DataCollectEndpointModule()
        {
            // Tracking Post
            // Data: u - the source URL
            Post("/k", args =>
            {
                ProcessRequestData(DataRequestType.Log | DataRequestType.Hit | DataRequestType.Web);
                return new Response().WithStatusCode(HttpStatusCode.OK);
            });
            // Tracking image
            // Params: u - the source URL
            Get("/k.png", args =>
            {
                ProcessRequestData(DataRequestType.Log | DataRequestType.Hit | DataRequestType.Web);
                // Send tracking pixel
                return Response.FromStream(TrackingImageProvider.CreateTrackingPixel(), "image/png");
            });
            // Redirect
            // Params: t - The target URL
            Get("/r", args =>
            {
                ProcessRequestData(DataRequestType.Log | DataRequestType.Redirect | DataRequestType.Web);
                var targetUrl = (string)Request.Query.t;
                if (targetUrl == null) return new Response().WithStatusCode(HttpStatusCode.BadRequest);
                // Do additional data logging
                return Response.AsRedirect(targetUrl);
            });
        }

        private UserSession CreateOrRetrieveSession()
        {
            UserSession ret = null;
            // Check if a stored session is available
            var storedSessData = Request.Session[SessionStorageService.SessionUserCookieStorageKey];
            if (storedSessData == null)
            {
                // Register and attempt to save session
                var session = new UserSession
                {
                    UserAgent = Request.Headers.UserAgent
                };
                Request.Session[SessionStorageService.SessionUserCookieStorageKey] = session.SessionId;
            }
            else
            {
                // Parse stored session data
            }
            return ret;
        }

        /// <summary>
        /// Process and log the request and associated data
        /// </summary>
        private void ProcessRequestData(DataRequestType requestType = DataRequestType.Log)
        {
            var currentSession = CreateOrRetrieveSession();            

            var eventIdentifier = Guid.NewGuid();
            if (requestType.HasFlag(DataRequestType.Log))
            {
                var req = new LogRequest { Identifier = eventIdentifier, SessionIdentifier = currentSession.SessionId };
                // Get client address
                var clientAddr = GetClientAddress();
                req.OriginAddress = clientAddr;
                req.KQApiNode = Request.Url;
                if (requestType.HasFlag(DataRequestType.Hit))
                {
                    var hitReq = Mapper.Map<HitRequest>(req);
                    if (requestType.HasFlag(DataRequestType.Web))
                    {
                        hitReq.Referrer = Request.Headers.Referrer;
                        // Attempt to get page URL
                        hitReq.PageIdentifier =
                            Request.Query.u // Query string
                            ?? Request.Form.u // Form data
                            ?? Request.Headers.Referrer; // Referer
                    }
                    req = hitReq;
                }
                else if (requestType.HasFlag(DataRequestType.Redirect))
                {
                    // TODO: Log redirect
                    var redirReq = Mapper.Map<RedirectRequest>(req);
                    // This flag IMPLIES the Web flag
                    if (requestType.HasFlag(DataRequestType.Web))
                    {
                        // Get target URL and save
                        redirReq.DestinationUrl = Request.Query.t;
                    }
                    req = redirReq;
                }
                // Save data using Logger service, on the thread pool
                var saveDataTask = Task.Factory.StartNew(async () =>
                {
                    await DataLoggerService.Log(req);
                });
            }
        }

        private string GetClientAddress()
        {
            return Request.UserHostAddress;
        }
    }
}