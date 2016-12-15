using AutoMapper;
using KQAnalytics3.Configuration;

namespace KQAnalytics3
{
    public static class KQRegistry
    {
        public static KQServerConfiguration ServerConfiguration { get; set; }
        public static IMapper RequestDataMapper { get; set; }
        public static string CommonConfigurationFileName => "kqconfig.json";
    }
}