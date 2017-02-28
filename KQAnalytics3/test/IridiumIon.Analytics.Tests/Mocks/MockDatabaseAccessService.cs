using KQAnalytics3.Services.Database;
using LiteDB;
using System.IO;

namespace KQAnalytics3.Tests.Mocks
{
    public class MockDatabaseAccessService : IDatabaseAccessService
    {
        private LiteDatabase _dbInstance;

        public LiteDatabase OpenOrCreateDefault()
        {
            if (_dbInstance == null)
            {
                _dbInstance = new LiteDatabase(new MemoryStream());
            }
            return _dbInstance;
        }
    }
}