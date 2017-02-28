using Nancy;
using System.IO;

namespace IridiumIon.Analytics.Metadata
{
    public class RootPathProvider : IRootPathProvider
    {
        public string GetRootPath()
        {
            return Directory.GetCurrentDirectory();
        }
    }
}