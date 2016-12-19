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
            // Tracking Post. Intended to be used from web apps
            // Data: u - the source URL
            Post("/k", async args =>
            {
                await ProcessRequestDataAsync(DataRequestType.Log | DataRequestType.Hit | DataRequestType.Web);
                return new Response().WithStatusCode(HttpStatusCode.OK);
            });

            // Custom data post. Intended for tagging with custom data
            // Data: tag - A short custom tag field
            // Data: data - A custom data field that holds a string
            Post("/c", async args =>
            {
                await ProcessRequestDataAsync(DataRequestType.Log | DataRequestType.Tag);
                return new Response().WithStatusCode(HttpStatusCode.OK);
            });

            // Tracking image
            // Params: u - the source URL
            Get("/k.png", async args =>
            {
                await ProcessRequestDataAsync(DataRequestType.Log | DataRequestType.Hit | DataRequestType.Web);
                // Send tracking pixel
                return Response.FromStream(TrackingImageProvider.CreateTrackingPixel(), "image/png");
            });
            // Tracking script
            // Params: u - the source URL
            Get("k.js", async args =>
            {
                await ProcessRequestDataAsync(DataRequestType.Log | DataRequestType.Hit | DataRequestType.Web | DataRequestType.FetchScript);
                return Response.FromStream(TrackingScriptProvider.CreateTrackingScript(), "application/javascript");
            });
            // Redirect
            // Params: t - The target URL
            Get("/r", async args =>
            {
                await ProcessRequestDataAsync(DataRequestType.Log | DataRequestType.Redirect | DataRequestType.Web);
                var targetUrl = (string)Request.Query.t;
                if (targetUrl == null) return new Response().WithStatusCode(HttpStatusCode.BadRequest);
                // Do additional data logging
                return Response.AsRedirect(targetUrl);
            });
        }

        private async Task<UserSession> CreateOrRetrieveSessionAsync()
        {
            UserSession ret = null;
            // Check if a stored session is available
            var storedSessData = Request.Session[SessionStorageService.SessionUserCookieStorageKey] as string;
            if (storedSessData != null)
            {
                // [Attempt to] Find matching session
                ret = await SessionStorageService.GetSessionFromIdentifierAsync(storedSessData);
            }
            if (storedSessData == null || ret == null)
            {
                // Register and attempt to save session
                var session = new UserSession
                {
                    UserAgent = Request.Headers.UserAgent,
                    StartTime = DateTime.Now
                };
                // Register session in database
                await SessionStorageService.SaveSessionAsync(session);

                // Store session data
                Request.Session[SessionStorageService.SessionUserCookieStorageKey] = session.SessionId;
                ret = session;
            }
            return ret;
        }

        /// <summary>
        /// Process and log the request and associated data
        /// </summary>
        private async Task ProcessRequestDataAsync(DataRequestType requestType = DataRequestType.Log)
        {
            var currentSession = await CreateOrRetrieveSessionAsync();

            var eventIdentifier = Guid.NewGuid();
            if (requestType.HasFlag(DataRequestType.Log))
            {
                var req = new LogRequest
                {
                    Identifier = eventIdentifier,
                    SessionIdentifier = currentSession.SessionId,
                    Timestamp = DateTime.Now,
                    RequestType = requestType
                };
                // Get client address
                var clientAddr = GetClientAddress();
                req.OriginAddress = clientAddr;
                req.KQApiNode = Request.Url;
                if (requestType.HasFlag(DataRequestType.Hit))
                {
                    // Map to Hit request
                    var hitReq = Mapper.Map<HitRequest>(req);
                    // Check if also a web request
                    if (requestType.HasFlag(DataRequestType.Web))
                    {
                        hitReq.Referrer = Request.Headers.Referrer;
                        // Attempt to get page URL
                        hitReq.PageIdentifier =
                            Request.Query.u // Query string
                            ?? Request.Form.u // Form data
                            ?? Request.Headers.Referrer; // Referer
                                                         // Check if FetchScript
                        if (requestType.HasFlag(DataRequestType.FetchScript))
                        {
                            
                        }
                    }
                    req = hitReq;
                }
                // Tag is not compatible with Hit
                else if (requestType.HasFlag(DataRequestType.Tag))
                {
                    // Log with custom data
                    var tagReq = Mapper.Map<TagRequest>(req);
                    tagReq.Tag = Request.Form.tag;
                    tagReq.ExtraData = Request.Form.data;
                    req = tagReq;
                }
                // Redirect is not compatible with Tag or Hit
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
                    await DataLoggerService.SaveLogRequestAsync(req);
                });
                // Custom saving
                if (req is TagRequest)
                {
                    var saveTagTask = Task.Factory.StartNew(async () =>
                    {
                        await DataLoggerService.SaveTagRequestAsync((TagRequest)req);
                    });
                }
            }
        }

        private string GetClientAddress()
        {
            return Request.UserHostAddress;
        }
    }
}