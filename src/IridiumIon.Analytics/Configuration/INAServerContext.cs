using AutoMapper;
using Iridium.PluginEngine;

namespace IridiumIon.Analytics.Configuration
{
    public interface INAServerContext
    {
        ComponentRegistry DefaultComponentRegistry { get; }
        IMapper RequestDataMapper { get; set; }
        NAServerParameters Parameters { get; }
        NAServerState ServerState { get; }
        LiteDatabase Database { get; set; }

        void ConnectDatabase();
    }
}