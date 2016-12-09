using System;

namespace KQAnalytics3.Models.Data
{
    public class UserSession : DatabaseObject
    {
        public string SessionId { get; set; } = Guid.NewGuid().ToString("N");

        public string UserAgent { get; set; }
    }
}
