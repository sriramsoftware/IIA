using AutoMapper;
using Iridium.PluginEngine;

namespace IridiumIon.Analytics.Configuration
{
    public interface INAServerContext
    {
        ComponentRegistry DefaultComponentRegistry { get; }
        IMapper RequestDataMapper { get; set; }
        KQServerConfiguration ServerConfiguration { get; }

        void ConnectDatabase();
    }
}