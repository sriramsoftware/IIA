using LiteDB;

namespace KQAnalytics3.Services.Database
{
    public static class DatabaseAccessService
    {
        public static string LoggedRequestDataKey => "lrequests";

        private static LiteDatabase _dbInstance;

        public static LiteDatabase OpenOrCreateDefault()
        {
            if (_dbInstance == null)
            {
                _dbInstance = new LiteDatabase("kqanalytics.lidb");
            }
            return _dbInstance;
        }
    }
}