using KQAnalytics3.Models.Data;
using KQAnalytics3.Services.Database;
using LiteDB;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KQAnalytics3.Services.DataCollection
{
    /// <summary>
    /// A static instance of a logger service that can be used to log data to the database
    /// </summary>
    public static class DataLoggerService
    {
        public static async Task LogAsync(LogRequest request)
        {
            var db = DatabaseAccessService.OpenOrCreateDefault();
            // Get logged requests collection
            var loggedRequests = db.GetCollection<LogRequest>(DatabaseAccessService.LoggedRequestDataKey);
            // Use ACID transaction
            using (var trans = db.BeginTrans())
            {
                // Insert new request into database
                loggedRequests.Insert(request);

                trans.Commit();
            }
            // Index requests by date
            loggedRequests.EnsureIndex(x => x.TimeStamp);
        }

        public static async Task<IEnumerable<LogRequest>> QueryRequestsAsync(int limit)
        {
            IEnumerable<LogRequest> result;
            var db = DatabaseAccessService.OpenOrCreateDefault();

            // Get logged requests collection
            var loggedRequests = db.GetCollection<LogRequest>(DatabaseAccessService.LoggedRequestDataKey);
            // Log by descending timestamp
            result = loggedRequests.Find(Query.All("TimeStamp", Query.Descending), limit: limit);

            return result;
        }
    }
}