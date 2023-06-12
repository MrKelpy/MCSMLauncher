using System.Collections.Generic;
using MCSMLauncher.common.server.builders;
using MCSMLauncher.common.server.starters;
using MCSMLauncher.requests.forge;
using MCSMLauncher.requests.mcversions;
using MCSMLauncher.requests.mcversions.full;
using MCSMLauncher.requests.mcversions.snapshots;
using MCSMLauncher.requests.spigot;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.common.factories
{
    /// <summary>
    /// This is a partial class of ServerTypeMappingsFactory, containing the actual mappings to be
    /// interacted with.
    /// </summary>
    public partial class ServerTypeMappingsFactory
    {
        
        /// <summary>
        /// The dictionary containing the values for every server type supported.
        /// </summary>
        private Dictionary<string, Dictionary<string, object>> Mappings { get; } =
            new Dictionary<string, Dictionary<string, object>>()
            {
                {
                    "vanilla", new Dictionary<string, object>()
                    {
                        { "handler", new MCVRequestHandler() },
                        { "parser", new MCVRequestParser() },
                        { "builder", new MCVBuilder() },
                        { "starter", new MCVServerStarter() },
                        { "cache_file", FileSystem.AddSection("versioncache").AddDocument("vanilla_releases.cache") },
                    }
                },
                {
                    "vanilla snapshots", new Dictionary<string, object>()
                    {
                        { "handler", new MCVSnapshotsRequestHandler() },
                        { "parser", new MCVRequestParser() },
                        { "builder", new MCVBuilder() },
                        { "starter", new MCVServerStarter() },
                        { "cache_file", FileSystem.AddSection("versioncache").AddDocument("vanilla_snapshots.cache") }, 
                    }
                },
                {
                    "spigot", new Dictionary<string, object>()
                    {
                        { "handler", new SpigotRequestHandler() },
                        { "parser", new SpigotRequestParser() },
                        { "builder", new SpigotBuilder() },
                        { "starter", new SpigotServerStarter() },
                        { "cache_file", FileSystem.AddSection("versioncache").AddDocument("spigot_releases.cache") }, 
                    }
                },
                {
                    "forge", new Dictionary<string, object>()
                    {
                        { "handler", new ForgeRequestHandler() },
                        { "parser", new ForgeRequestParser() },
                        { "builder", new ForgeBuilder() },
                        { "starter", new ForgeServerStarter() },
                        { "cache_file", FileSystem.AddSection("versioncache").AddDocument("forge_releases.cache") },
                    }
                },
                {
                    "unknown", new Dictionary<string, object>()
                    {
                        { "handler", null },
                        { "parser", null },
                        { "builder", null },
                        { "starter", new MCVServerStarter() },
                        { "cache_file", null },
                    }
                }
            };
    }
}