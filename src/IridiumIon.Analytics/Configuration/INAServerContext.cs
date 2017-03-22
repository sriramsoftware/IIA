using AutoMapper;
using Iridium.PluginEngine;
using IridiumIon.Analytics.Services.Resources;
using LiteDB;

namespace IridiumIon.Analytics.Configuration
{
    public interface INAServerContext
    {
        ComponentRegistry DefaultComponentRegistry { get; }
        NAServerParameters Parameters { get; }
        NAServerState ServerState { get; }
        LiteDatabase Database { get; }
        ResourceProviderService ResourceProvider { get; }

        void ConnectDatabase();
    }
}