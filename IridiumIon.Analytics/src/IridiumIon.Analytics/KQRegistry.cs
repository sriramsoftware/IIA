using AutoMapper;
using Iridium.PluginEngine;
using IridiumIon.Analytics.Configuration;
using IridiumIon.Analytics.Configuration.Access;
using IridiumIon.Analytics.Services.Database;

namespace IridiumIon.Analytics
{
    public static class KQRegistry
    {
        public static KQServerConfiguration ServerConfiguration { get; set; }
        public static IMapper RequestDataMapper { get; set; }
        public static string CommonConfigurationFileName => "kqconfig.json";
        public static ApiKeyCache KeyCache { get; private set; }
        public static string CurrentDirectory { get; set; }
        public static string KQBasePath { get; set; }

        static KQRegistry()
        {
            Initialize();
        }

        // Database access
        public static IDatabaseAccessService DatabaseAccessService { get; set; }

        // PluginEngine
        public static ComponentRegistry DefaultComponentRegistry => new ComponentRegistry();

        public static void Initialize()
        {
            // Values
            ServerConfiguration = null;
            RequestDataMapper = null;
            KeyCache = new ApiKeyCache();
            KQBasePath = "/kq";

            // Database
            DatabaseAccessService = new DatabaseAccessService();
        }

        public static void UpdateKeyCache()
        {
            KeyCache = new ApiKeyCache(ServerConfiguration.ApiKeys);
        }

        public static void PropagateConfiguration()
        {
            // See if configuration overrides base path
            KQBasePath = ServerConfiguration?.BasePathPrefix ?? KQBasePath;
        }
    }
}