using KQAnalytics3.Configuration;
using KQAnalytics3.Tests.Mocks;
using System;
using System.IO;

namespace KQAnalytics3.Tests.Modules
{
    public class KQModuleTestFixture : IDisposable
    {
        public KQModuleTestFixture()
        {
            // Inject components
            var currentDir = Directory.GetCurrentDirectory();
            KQRegistry.CurrentDirectory = currentDir;
            KQConfigurationAggregator.Initialize();
            KQRegistry.DatabaseAccessService = new MockDatabaseAccessService();
        }

        public void Dispose()
        {
            // TODO: Cleanup
        }
    }
}