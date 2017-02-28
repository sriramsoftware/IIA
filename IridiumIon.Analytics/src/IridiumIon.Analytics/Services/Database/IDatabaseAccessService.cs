using LiteDB;

namespace IridiumIon.Analytics.Services.Database
{
    public interface IDatabaseAccessService
    {
        LiteDatabase GetDatabase();
    }
}