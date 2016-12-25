using KQAnalytics3.Models.Data;
using KQAnalytics3.Services.Database;
using KQAnalytics3.Utilities;
using LiteDB;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KQAnalytics3.Services.DataCollection
{
    /// <summary>
    /// A static instance of a logger service that can be used to log data to the database
    /// </summary>
    public class DataLoggerService
    {
        public async Task SaveLogRequestAsync(LogRequest request)
        {
            await Task.Run(() =>
            {
                var db = KQRegistry.DatabaseAccessService.OpenOrCreateDefault();
                // Get logged requests collection
                var loggedRequests = db.GetCollection<LogRequest>(DatabaseConstants.LoggedRequestDataKey);
                // Use ACID transaction
                using (var trans = db.BeginTrans())
                {
                    // Insert new request into database
                    loggedRequests.Insert(request);

                    trans.Commit();
                }
                // Index requests by date
                loggedRequests.EnsureIndex(x => x.Timestamp);
                loggedRequests.EnsureIndex(x => x.Kind);
            });
        }

        public async Task SaveTagRequestAsync(TagRequest request)
        {
            await Task.Run(() =>
            {
                var db = KQRegistry.DatabaseAccessService.OpenOrCreateDefault();
                // Get logged requests collection
                var tagRequests = db.GetCollection<TagRequest>(DatabaseConstants.TaggedRequestDataKey);
                // Use ACID transaction
                using (var trans = db.BeginTrans())
                {
                    // Insert new request into database
                    tagRequests.Insert(request);

                    trans.Commit();
                }
                // Index requests by date
                tagRequests.EnsureIndex(x => x.Timestamp);
                tagRequests.EnsureIndex(x => x.Tag);
            });
        }

        public async Task<IEnumerable<LogRequest>> QueryRequestsAsync(int limit)
        {
            var db = KQRegistry.DatabaseAccessService.OpenOrCreateDefault();
            var result = await Task.Run(() =>
            {
                // Get logged requests collection
                var loggedRequests = db.GetCollection<LogRequest>(DatabaseConstants.LoggedRequestDataKey);
                // Log by descending timestamp
                return loggedRequests.Find(Query.All(nameof(LogRequest.Timestamp), Query.Descending), limit: limit);
            });
            return result;
        }

        public async Task<IEnumerable<TagRequest>> QueryTaggedRequestsAsync(int limit, string[] filterTags = null)
        {
            var db = KQRegistry.DatabaseAccessService.OpenOrCreateDefault();
            var result = await Task.Run(() =>
            {
                // Get tagged requests collection
                var taggedRequests = db.GetCollection<TagRequest>(DatabaseConstants.TaggedRequestDataKey);
                // Log by descending timestamp
                return taggedRequests.Find(
                    Query.And(
                        Query.All(nameof(TagRequest.Timestamp), Query.Descending),
                        Query.Where(nameof(TagRequest.Tag), v => filterTags == null || filterTags.Any(f => WildcardMatcher.IsMatch(v.AsString, f)))
                    ), limit: limit
                );
            });
            return result;
        }
    }
}