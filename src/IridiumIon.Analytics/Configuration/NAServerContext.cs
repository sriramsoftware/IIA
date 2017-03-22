using AutoMapper;
using Iridium.PluginEngine;
using IridiumIon.Analytics.Services.Database;

namespace IridiumIon.Analytics.Configuration
{
    public class NAServerContext : INAServerContext
    {

        public NAServerContext(NAServerParameters serverParameters)
        {
            Parameters = serverParameters;
        }

        // Parameters (starting configuration)
        public NAServerParameters Parameters { get; }

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