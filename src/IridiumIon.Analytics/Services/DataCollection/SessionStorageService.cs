using IridiumIon.Analytics.Configuration;
using IridiumIon.Analytics.Models.Data;
using IridiumIon.Analytics.Services.Database;
using System.Threading.Tasks;

namespace IridiumIon.Analytics.Services.DataCollection
{
    public class SessionStorageService
    {
        public const string SessionUserCookieStorageKey = "kq_session";

        public INAServerContext ServerContext { get; }

        public SessionStorageService(INAServerContext serverContext)
        {
            ServerContext = serverContext;
        }

        public async Task<UserSession> GetSessionFromIdentifierAsync(string identifier)
        {
            return await Task.Run(() =>
            {
                UserSession ret = null;

                // Get stored sessions collection
                var storedSessions = ServerContext.Database.GetCollection<UserSession>(DatabaseConstants.LoggedRequestDataKey);

                ret = storedSessions.FindOne(x => x.SessionId == identifier);

                return ret;
            });
        }

        public async Task SaveSessionAsync(UserSession session)
        {
            await Task.Run(() =>
            {
                // Get logged requests collection
                var loggedRequests = ServerContext.Database.GetCollection<UserSession>(DatabaseConstants.LoggedRequestDataKey);
                // Use ACID transaction
                using (var trans = ServerContext.Database.BeginTrans())
                {
                    // Insert new session into database
                    loggedRequests.Insert(session);

                    trans.Commit();
                }
                // Index requests by identifier
                loggedRequests.EnsureIndex(x => x.SessionId);
            });
        }
    }
}