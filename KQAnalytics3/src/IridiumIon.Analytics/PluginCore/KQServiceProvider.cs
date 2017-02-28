using KQAnalytics3.Services.DataCollection;
using KQAnalytics3.Services.DataQuery;
using KQAnalytics3.Services.Resources;

namespace KQAnalytics3.PluginCore
{
    public class KQServiceProvider : IKQServiceProvider
    {
        public DataLoggerService DataLoggerService { get; } = new DataLoggerService();
        public SessionStorageService SessionStorageService { get; } = new SessionStorageService();
        public DataQueryDateService DataQueryDateService { get; } = new DataQueryDateService();
        public ResourceProviderService ResourceProviderService { get; } = new ResourceProviderService();
    }
}