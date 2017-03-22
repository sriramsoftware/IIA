using Iridium.PluginEngine;
using IridiumIon.Analytics.Configuration.Access;
using IridiumIon.Analytics.Services.Resources;
using LiteDB;
using OsmiumSubstrate.Configuration;

namespace IridiumIon.Analytics.Configuration
{
    public class NAServerContext : INAServerContext
    {
        public NAServerContext(NAServerParameters serverParameters)
        {
            Parameters = serverParameters;
            ResourceProvider = new ResourceProviderService(this);
        }

        // Parameters (starting configuration)
        public NAServerParameters Parameters { get; }

        // Persistent State
        public NAServerState ServerState { get; internal set; }

        ISubstrateServerState<NAAccessKey, NAApiAccessScope> ISubstrateServerContext<NAAccessKey, NAApiAccessScope>.SubstrateServerState => ServerState;

        // Database access
        public LiteDatabase Database { get; private set; }

        // Resources
        public ResourceProviderService ResourceProvider { get; private set; }

        // PluginEngine
        public ComponentRegistry DefaultComponentRegistry { get; } = new ComponentRegistry();

        public void ConnectDatabase()
        {
            // Database
            Database = new LiteDatabase(Parameters.DatabaseConfiguration.FileName);
        }
    }
}