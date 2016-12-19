using System.IO;

namespace KQAnalytics3.Services.Resources
{
    public static class ResourceProviderService
    {
        public static string ResourceBasePath => Path.Combine("Resources");

        public static Stream GetResource(string resourcePath)
        {
            var resCompletePath = Path.Combine(ResourceBasePath, resourcePath);
            return File.OpenRead(resourcePath);
        }

        public static string ReadResourceContents(string resourcePath)
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