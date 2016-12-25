using KQAnalytics3.Services.DataCollection;
using KQAnalytics3.Services.DataQuery;
using KQAnalytics3.Services.Resources;

namespace KQAnalytics3.PluginCore
{
    public interface IKQServiceProvider
    {
        DataLoggerService DataLoggerService { get; }
        SessionStorageService SessionStorageService { get; }
        DataQueryDateService DataQueryDateService { get; }
        ResourceProviderService ResourceProviderService { get; }
    }
}