using System.Collections.Generic;
using System.Linq;
using MCSMLauncher.common.server.builders.abstraction;
using MCSMLauncher.common.server.starters.abstraction;
using MCSMLauncher.requests.abstraction;
using PgpsUtilsAEFC.utils;

namespace MCSMLauncher.common.factories
{
    /// <summary>
    /// This factory class aims to provide every handler, parser and cache file path for every
    /// server type based on the requirements.
    /// </summary>
    public partial class ServerTypeMappingsFactory
    {
        /// <summary>
        /// Gets the request handler for the given server type. If the server type is not supported,
        /// return null.
        /// </summary>
        /// <param name="serverType">The server type to return the handler for</param>
        /// <returns>An instance of AbstractBaseRequestHandler mapped to the server type</returns>
        public AbstractBaseRequestHandler GetHandlerFor(string serverType)
        {
            return this.Mappings.ContainsKey(serverType.ToLower())
                ? (AbstractBaseRequestHandler)this.Mappings[serverType.ToLower()]["handler"]
                : null;
        }

        /// <summary>
        /// Gets the request parser for the given server type. If the server type is not supported,
        /// return null.
        /// </summary>
        /// <param name="serverType">The server type to return the parser for</param>
        /// <returns>An instance of AbstractBaseRequestParser mapped to the server type</returns>
        public AbstractBaseRequestParser GetParserFor(string serverType)
        {
            return this.Mappings.ContainsKey(serverType.ToLower())
                ? (AbstractBaseRequestParser)this.Mappings[serverType.ToLower()]["parser"]
                : null;
        }

        /// <summary>
        /// Gets the server builder for the given server type. If the server type is not supported,
        /// return null.
        /// </summary>
        /// <param name="serverType">The server type to return the builder for</param>
        /// <returns>An instance of AbstractServerBuilder mapped to the server type</returns>
        public AbstractServerBuilder GetBuilderFor(string serverType)
        {
            return this.Mappings.ContainsKey(serverType.ToLower())
                ? (AbstractServerBuilder)this.Mappings[serverType.ToLower()]["builder"]
                : null;
        }

        /// <summary>
        /// Gets the server starter for the given server type. If the server type is not supported,
        /// return null.
        /// </summary>
        /// <param name="serverType">The server type to return the starter for</param>
        /// <returns>An instance of AbstractServerStarter mapped to the server typ1</returns>
        public AbstractServerStarter GetStarterFor(string serverType)
        {
            return this.Mappings.ContainsKey(serverType.ToLower())
                ? (AbstractServerStarter)this.Mappings[serverType.ToLower()]["starter"]
                : null;
        }

        /// <summary>
        /// Gets the cache file path for the given server type. If the server type is not supported,
        /// return null.
        /// </summary>
        /// <param name="serverType">The server type to return the cache file for</param>
        /// <returns>The path for the cache file mapped to the server type</returns>
        public string GetCacheFileFor(string serverType)
        {
            return this.Mappings.ContainsKey(serverType.ToLower())
                ? (string)this.Mappings[serverType.ToLower()]["cache_file"]
                : null;
        }

        /// <summary>
        /// Accesses the file system and returns the correct cache contents based on a provided server type, in
        /// the form of a dictionary mapping VersionName:DownloadLink
        /// </summary>
        /// <param name="serverType">The server type to get the version cache file for.</param>
        /// <returns>A dictionary mapping the version names to their server download links</returns>
        public Dictionary<string, string> GetCacheContentsForType(string serverType)
        {
            return FileToDictionary(this.GetCacheFileFor(serverType));
        }

        /// <summary>
        /// Returns a list of all the supported server types.
        /// </summary>
        /// <returns>A List(string) containing all the supported server types.</returns>
        public List<string> GetSupportedServerTypes()
        {
            return new List<string>(this.Mappings.Keys).Where(x => x != "unknown").ToList();
        }

        /// <summary>
        /// Iterates over each line, and breaks it by the > character, then adds the result to a dictionary, mapping
        /// the first part to the second part, equivalent to VersionName:DownloadLink.
        /// </summary>
        /// <param name="path">The path to the file to convert</param>
        /// <returns>The VersionName:DownloadLink mapping</returns>
        private static Dictionary<string, string> FileToDictionary(string path)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            // Iterates over each line, and breaks it by the > character, logically defines as the separator,
            // and adds the result to a dictionary.
            foreach (string line in FileUtils.ReadFromFile(path))
            {
                string[] split = line.Split('>');
                result.Add(split[0].Trim(), split[1].Trim());
            }

            return result.Count > 0 ? result : null;
        }
    }
}