using AutoMapper;
using Iridium.PluginEngine;
using IridiumIon.Analytics.Configuration.Access;
using IridiumIon.Analytics.Services.Resources;
using LiteDB;
using OsmiumSubstrate.Configuration;

namespace IridiumIon.Analytics.Configuration
{
    public interface INAServerContext : ISubstrateServerContext<NAAccessKey, NAApiAccessScope>
    {
        ComponentRegistry DefaultComponentRegistry { get; }
        NAServerParameters Parameters { get; }
        LiteDatabase Database { get; }
        ResourceProviderService ResourceProvider { get; }

        void ConnectDatabase();
    }
}