﻿using System.Collections.Generic;
using MCSMLauncher.common.builders.abstraction;
using MCSMLauncher.requests;
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
        public AbstractBaseRequestHandler GetHandlerFor(string serverType) =>
            Mappings.ContainsKey(serverType.ToLower()) ? (AbstractBaseRequestHandler) Mappings[serverType.ToLower()]["handler"] : null;

        /// <summary>
        /// Gets the request parser for the given server type. If the server type is not supported,
        /// return null.
        /// </summary>
        /// <param name="serverType">The server type to return the parser for</param>
        /// <returns>An instance of IBaseRequestParser mapped to the server type</returns>
        public IBaseRequestParser GetParserFor(string serverType) =>
            Mappings.ContainsKey(serverType.ToLower()) ? (IBaseRequestParser) Mappings[serverType.ToLower()]["parser"] : null;
        
        /// <summary>
        /// Gets the server builder for the given server type. If the server type is not supported,
        /// return null.
        /// </summary>
        /// <param name="serverType">The server type to return the builder for</param>
        /// <returns></returns>
        public AbstractServerBuilder GetBuilderFor(string serverType) =>
            Mappings.ContainsKey(serverType.ToLower()) ? (AbstractServerBuilder) Mappings[serverType.ToLower()]["builder"] : null;
        
        /// <summary>
        /// Gets the cache file path for the given server type. If the server type is not supported,
        /// return null.
        /// </summary>
        /// <param name="serverType">The server type to return the cache file for</param>
        /// <returns>The path for the cache file mapped to the server type</returns>
        public string GetCacheFileFor(string serverType) =>
            Mappings.ContainsKey(serverType.ToLower()) ? (string) Mappings[serverType.ToLower()]["cache_file"] : null;

        /// <summary>
        /// Accesses the file system and returns the correct cache contents based on a provided server type, in
        /// the form of a dictionary mapping VersionName:DownloadLink
        /// </summary>
        /// <param name="serverType">The server type to get the version cache file for.</param>
        /// <returns>A dictionary mapping the version names to their server download links</returns>
        public Dictionary<string, string> GetCacheContentsForType(string serverType) =>
            FileToDictionary(this.GetCacheFileFor(serverType));
        
        /// <summary>
        /// Returns a list of all the supported server types.
        /// </summary>
        /// <returns>A List(string) containing all the supported server types.</returns>
        public List<string> GetSupportedServerTypes() => new List<string>(Mappings.Keys);
        
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