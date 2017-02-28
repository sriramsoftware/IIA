using Nancy;
using System.IO;

namespace KQAnalytics3.Metadata
{
    public class RootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}