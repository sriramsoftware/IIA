using IridiumIon.Analytics.Models.Data;
using IridiumIon.Analytics.Services.Database;
using System;
using System.Collections.Generic;

namespace IridiumIon.Analytics.Services.DataQuery
{
    public class DataQueryDateService
    {
        public IEnumerable<LogRequest> GetAllRequestsAfterDate(DateTime startDate)
        {
            var loggedRequests = ServerContext.Database.GetCollection<LogRequest>(DatabaseConstants.LoggedRequestDataKey);
            return loggedRequests.Find(x => x.Timestamp > startDate);
        }
    }
}