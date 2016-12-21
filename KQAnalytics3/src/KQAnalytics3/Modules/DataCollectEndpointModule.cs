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
            // Data: sid - cross domain tracking id
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

        /// <summary>
        /// Creates or retrieves a session for the user
        /// </summary>
        /// <param name="customId">A custom session ID. This will take precedence over an existing session.</param>
        /// <returns>A Tuple that contains the session object and a boolean indicating whether the session was newly created</returns>
        private async Task<Tuple<UserSession, bool>> CreateOrRetrieveSessionAsync(Guid? customId = null)
        {
            UserSession ret = null;
            var newSession = true; // Whether the session is new
            // Check if a stored session is available
            var storedSessData = Request.Session[SessionStorageService.SessionUserCookieStorageKey] as string;
            if (storedSessData != null && customId == null)
            {
                // [Attempt to] Find matching session
                ret = await SessionStorageService.GetSessionFromIdentifierAsync(storedSessData);
                if (ret != null)
                {
                    // Session was already available
                    newSession = false;
                }
            }
            if (storedSessData == null || ret == null || customId != null)
            {
                // Register and attempt to save session
                var session = new UserSession(customId ?? Guid.NewGuid())
                {
                    UserAgent = Request.Headers.UserAgent,
                    StartTime = DateTime.Now
                };
                // Register session in database
                await SessionStorageService.SaveSessionAsync(session);

                // Store session data
                Request.Session[SessionStorageService.SessionUserCookieStorageKey] = session.SessionId;
                ret = session;
                newSession = true;
            }
            return new Tuple<UserSession, bool>(ret, newSession);
        }

        /// <summary>
        /// Process and log the request and associated data
        /// </summary>
        private async Task ProcessRequestDataAsync(DataRequestType requestType = DataRequestType.Log)
        {
            Guid? sessionIdentifier = null; // Null means it will be automatically created
            // A cross-domain SID can be specified
            var sentSessId = (string)Request.Form.sid;
            // TODO: Maybe validation to ensure SID is not being overwritten
            if (sentSessId != null)
            {
                Guid resultSessGuid;
                if (Guid.TryParse(sentSessId, out resultSessGuid))
                {
                    // TODO: Possibly note that session used custom ID
                    sessionIdentifier = resultSessGuid;
                }
            }

            var sessionInfo = await CreateOrRetrieveSessionAsync(sessionIdentifier);
            var currentSession = sessionInfo.Item1;
            var newSession = sessionInfo.Item2;

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
                            (string)Request.Query.u // Query string
                            ?? (string)Request.Form.u // Form data
                            ?? Request.Headers.Referrer; // Referrer
                                                         // Check if FetchScript
                        if (requestType.HasFlag(DataRequestType.FetchScript))
                        {
                            // Map to FetchScriptRequest
                            var fetchScriptReq = Mapper.Map<FetchScriptRequest>(hitReq);
                            hitReq = fetchScriptReq;
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