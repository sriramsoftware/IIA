using AutoMapper;
using Iridium.PluginEngine;
using IridiumIon.Analytics.Services.Database;

namespace IridiumIon.Analytics.Configuration
{
    public class NAServerContext : INAServerContext
    {
        public NAServerParameters Parameters { get; }

        public NAServerContext(NAServerParameters serverParameters)
        {
            Parameters = serverParameters;
        }

        // Configuration
        public KQServerConfiguration ServerConfiguration { get; private set; }

        // AutoMapper
        public IMapper RequestDataMapper { get; set; }

        // Persistent State
        public NAServerState ServerState { get; internal set; }

        // Database access
        public static IDatabaseAccessService Database { get; set; }

        // PluginEngine
        public ComponentRegistry DefaultComponentRegistry { get; } = new ComponentRegistry();

        public void ConnectDatabase()
        {
            // Database
            Database = new DatabaseAccessService();
        }
    }
}