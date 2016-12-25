using KQAnalytics3.Configuration;
using KQAnalytics3.Tests.Mocks;
using System.IO;

namespace KQAnalytics3.Tests.Modules
{
    public class KQModuleTest
    {
        public KQModuleTest()
        {
            // Inject components
            var currentDir = Directory.GetCurrentDirectory();
            KQRegistry.CurrentDirectory = currentDir;
            KQConfigurationAggregator.Initialize();
            KQRegistry.DatabaseAccessService = new MockDatabaseAccessService();
        }
    }
}