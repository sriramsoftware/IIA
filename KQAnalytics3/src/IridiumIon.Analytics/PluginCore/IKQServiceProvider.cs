using IridiumIon.Analytics.Services.DataCollection;
using IridiumIon.Analytics.Services.DataQuery;
using IridiumIon.Analytics.Services.Resources;

namespace IridiumIon.Analytics.PluginCore
{
    public interface IKQServiceProvider
    {
        DataLoggerService DataLoggerService { get; }
        SessionStorageService SessionStorageService { get; }
        DataQueryDateService DataQueryDateService { get; }
        ResourceProviderService ResourceProviderService { get; }
    }
}