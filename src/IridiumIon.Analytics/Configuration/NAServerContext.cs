using AutoMapper;
using Iridium.PluginEngine;
using IridiumIon.Analytics.Services.Database;

namespace IridiumIon.Analytics.Configuration
{
    public class NAServerContext : INAServerContext
    {
        public KQServerConfiguration ServerConfiguration { get; private set; }
        public IMapper RequestDataMapper { get; set; }

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