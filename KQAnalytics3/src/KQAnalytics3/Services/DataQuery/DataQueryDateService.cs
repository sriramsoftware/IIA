using KQAnalytics3.Models.Data;
using KQAnalytics3.Services.Database;
using System;
using System.Collections.Generic;

namespace KQAnalytics3.Services.DataQuery
{
    public static class DataQueryDateService
    {
        public static IEnumerable<LogRequest> GetAllRequestsAfterDate(DateTime startDate)
        {
            var db = DatabaseAccessService.OpenOrCreateDefault();
            var loggedRequests = db.GetCollection<LogRequest>(DatabaseAccessService.LoggedRequestDataKey);
            return loggedRequests.Find(x => x.Timestamp > startDate);
        }
    }
}