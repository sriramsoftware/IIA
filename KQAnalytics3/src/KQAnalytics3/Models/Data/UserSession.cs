using System;

namespace KQAnalytics3.Models.Data
{
    public class UserSession : DatabaseObject
    {
        public Guid SesionId { get; set; } = Guid.NewGuid();
    }
}
