using System;

namespace KQAnalytics3.Models.Data
{
    public class UserSession : DatabaseObject
    {
        public Guid SessionId { get; set; } = Guid.NewGuid();

        public string UserAgent { get; set; }
    }
}
