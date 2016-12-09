using LiteDB;

namespace KQAnalytics3.Services.Database
{
    public static class DatabaseAccessService
    {
        public static string UsersCollectionDatabaseKey => "Users";

        public static LiteDatabase OpenOrCreateDefault()
        {
            //kqanalytics3.lidb
            return new LiteDatabase("kqanalytics.lidb");
        }
    }
}