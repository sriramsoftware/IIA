using IridiumIon.Analytics.Services.DataCollection;
using IridiumIon.Analytics.Services.DataQuery;
using IridiumIon.Analytics.Services.Resources;

namespace IridiumIon.Analytics.PluginCore
{
    public class KQServiceProvider : IKQServiceProvider
    {
        public DataLoggerService DataLoggerService { get; } = new DataLoggerService();
        public SessionStorageService SessionStorageService { get; } = new SessionStorageService();
        public DataQueryDateService DataQueryDateService { get; } = new DataQueryDateService();
        public ResourceProviderService ResourceProviderService { get; } = new ResourceProviderService();
    }
}