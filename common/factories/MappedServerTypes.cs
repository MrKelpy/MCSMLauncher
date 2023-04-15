using System.Collections.Generic;
using MCSMLauncher.common.builders;
using MCSMLauncher.requests.forge;
using MCSMLauncher.requests.mcversions;
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
                        { "cache_file", FileSystem.AddSection("versioncache").AddDocument("vanilla_releases.cache") },
                    }
                },
                {
                    "vanilla snapshots", new Dictionary<string, object>()
                    {
                        { "handler", new MCVRequestHandler() },
                        { "parser", new MCVRequestParser() },
                        { "cache_file", FileSystem.AddSection("versioncache").AddDocument("vanilla_snapshots.cache") }, 
                    }
                },
                {
                    "spigot", new Dictionary<string, object>()
                    {
                        { "handler", new SpigotRequestHandler() },
                        { "parser", new SpigotRequestParser() },
                        { "cache_file", FileSystem.AddSection("versioncache").AddDocument("spigot_releases.cache") }, 
                    }
                },
                {
                    "forge", new Dictionary<string, object>()
                    {
                        { "handler", new ForgeRequestHandler() },
                        { "parser", new ForgeRequestParser() },
                        { "cache_file", FileSystem.AddSection("versioncache").AddDocument("forge_releases.cache") },
                    }
                },
            };
    }
}