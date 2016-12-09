using System;

namespace KQAnalytics3.Models.Requests
{
    public class LogRequest
    {
        /// <summary>
        /// Stores an identifier that should be unique to the request
        /// </summary>
        public Guid Identifier { get; set; }

        /// <summary>
        /// Stores the query URL to the KQ server
        /// </summary>
        public string KQApiNode { get; set; }

        /// <summary>
        /// Stores the client address
        /// </summary>
        public string OriginAddress { get; set; }
    }
}