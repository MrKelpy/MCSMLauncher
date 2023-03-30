using System;
using System.Collections.Generic;
using MCSMLauncher.requests;
using MCSMLauncher.requests.forge;
using MCSMLauncher.requests.mcversions;
using MCSMLauncher.requests.spigot;

namespace MCSMLauncher.common.factories
{
    /// <summary>
    /// This factory class allows the program to be provided with the correct request handler
    /// based on an input server type name.
    /// </summary>
    public class RequestParserFactory
    {

        /// <summary>
        /// The dictionary mapping the server types to the correct implementations of IBaseRequestParser
        /// </summary>
        private Dictionary<string, IBaseRequestParser> TypeParserMapping { get; } = new Dictionary<string, IBaseRequestParser>()
            {
                { "vanilla", new MCVRequestParser() },
                { "vanilla snapshots", new MCVRequestParser() },
                { "spigot", new SpigotRequestParser() },
                { "forge", new ForgeRequestParser() }
            };

        /// <summary>
        /// Accesses the type to parser mapping and returns the correct instance of the implementation.
        /// </summary>
        /// <param name="serverType">The server type to get</param>
        /// <returns>An instance of an implementation of IBaseRequestParser</returns>
        public IBaseRequestParser GetRequestParserFor(string serverType) =>
            TypeParserMapping.ContainsKey(serverType.ToLower()) ? TypeParserMapping[serverType.ToLower()] : null;

    }
}