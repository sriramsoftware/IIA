using System.IO;

namespace IridiumIon.Analytics.Services.Resources
{
    public class ResourceProviderService
    {
        public static string ResourceBasePath => Path.Combine(KQRegistry.CurrentDirectory, "Resources", "lib");

        public Stream GetResource(string resourcePath)
        {
            var resCompletePath = Path.Combine(ResourceBasePath, resourcePath);
            return File.OpenRead(resCompletePath);
        }

        public string ReadResourceContents(string resourcePath)
        {
            var result = string.Empty;
            using (var sr =new StreamReader(GetResource(resourcePath)))
            {
                result = sr.ReadToEnd();
            }
            return result;
        }
    }
}