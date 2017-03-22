using AutoMapper;
using Iridium.PluginEngine;
using LiteDB;

namespace IridiumIon.Analytics.Configuration
{
    public interface INAServerContext
    {
        ComponentRegistry DefaultComponentRegistry { get; }
        IMapper RequestDataMapper { get; set; }
        NAServerParameters Parameters { get; }
        NAServerState ServerState { get; }
        LiteDatabase Database { get; }

        void ConnectDatabase();
    }
}