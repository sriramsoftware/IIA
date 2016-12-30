using LiteDB;

namespace KQAnalytics3.Services.Database
{
    public interface IDatabaseAccessService
    {
        LiteDatabase OpenOrCreateDefault();
    }
}