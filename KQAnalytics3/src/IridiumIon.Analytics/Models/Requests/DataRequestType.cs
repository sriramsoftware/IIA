using System;

namespace KQAnalytics3.Models.Requests
{
    [Flags]
    public enum DataRequestType
    {
        /// <summary>
        /// Log the request
        /// </summary>
        Log = 1 << 0,

        /// <summary>
        /// Represents a hit, or single view/usage
        /// </summary>
        Hit = 1 << 1,

        /// <summary>
        /// Used to log clicks and navigation
        /// </summary>
        Click = 1 << 2,

        /// <summary>
        /// Used to log redirects throughout the app. Should not be used with Hit.
        /// Implies the Web flag, but it should still be specified.
        /// </summary>
        Redirect = 1 << 3,

        /// <summary>
        /// Indicates that the target application is a web app
        /// </summary>
        Web = 1 << 4,

        /// <summary>
        /// Indicates that the request was manually tagged. Should not be used with Hit
        /// </summary>
        Tag = 1 << 5,

        /// <summary>
        /// Indicates that this was a request to retrieve the tracking script
        /// </summary>
        FetchScript,
    }
}