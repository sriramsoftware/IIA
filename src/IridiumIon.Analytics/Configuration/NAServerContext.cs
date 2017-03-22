﻿using AutoMapper;
using Iridium.PluginEngine;
using IridiumIon.Analytics.Services.Resources;
using LiteDB;

namespace IridiumIon.Analytics.Configuration
{
    public class NAServerContext : INAServerContext
    {
        public NAServerContext(NAServerParameters serverParameters)
        {
            Parameters = serverParameters;
        }

        // Parameters (starting configuration)
        public NAServerParameters Parameters { get; }

        // Persistent State
        public NAServerState ServerState { get; internal set; }

        // Database access
        public LiteDatabase Database { get; private set; }

        // Resources
        public ResourceProviderService ResourceProvider { get; private set; }

        // PluginEngine
        public ComponentRegistry DefaultComponentRegistry { get; } = new ComponentRegistry();

        public void ConnectDatabase()
        {
            // Database
            Database = new LiteDatabase(Parameters.DatabaseConfiguration.FileName);
        }
    }
}