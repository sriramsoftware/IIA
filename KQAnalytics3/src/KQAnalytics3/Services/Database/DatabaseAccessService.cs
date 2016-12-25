using LiteDB;

namespace KQAnalytics3.Services.Database
{
    public class DatabaseAccessService : IDatabaseAccessService
    {
        private LiteDatabase _dbInstance;

        public LiteDatabase OpenOrCreateDefault()
        {
            if (_dbInstance == null)
            {
                //_dbInstance = new LiteDatabase($"kqanalytics.lidb;Password={KQRegistry.ServerConfiguration.DatabaseEncryptionPassword}");
                _dbInstance = new LiteDatabase($"kqanalytics.lidb");
            }
            return _dbInstance;
        }
    }
}