using KQAnalytics3.Models.Requests;
using KQAnalytics3.Services.Database;
using System.Threading.Tasks;

namespace KQAnalytics3.Services.DataCollection
{
    /// <summary>
    /// A static instance of a logger service that can be used to log data to the database
    /// </summary>
    public static class DataLoggerService
    {
        public static async Task Log(LogRequest request)
        {
            using (var db = DatabaseAccessService.OpenOrCreateDefault())
            {
                // TODO: ...
            }
        }
    }
}