using Newtonsoft.Json;
using System.IO;

namespace IridiumIon.Analytics.Configuration
{
    public static class KQConfigurationAggregator
    {
        public static void Initialize()
        {
            // Load default configuration
            KQRegistry.ServerConfiguration = new KQServerConfiguration();
            Reload();
        }

        public static void LoadConfiguration(KQServerConfiguration conf)
        {
            KQRegistry.ServerConfiguration = conf;
        }

        public static void LoadConfigurationFile()
        {
            LoadConfigurationFile(KQRegistry.CommonConfigurationFileName);
        }

        public static void LoadConfigurationFile(string fileName)
        {
            // Read KQConfig configuration file
            if (File.Exists(fileName))
            {
                var configFileCont = File.ReadAllText(fileName);
                JsonConvert.PopulateObject(configFileCont, KQRegistry.ServerConfiguration); // Merge with custom configuration
            }
        }

        /// <summary>
        /// Notifies KQ components that a new configuration is active. This should be called whenever the configuration is modified
        /// </summary>
        public static void Reload()
        {
            // Propagate configuration
            KQRegistry.PropagateConfiguration();

            // Reload caches
            KQRegistry.UpdateKeyCache();
        }
    }
}