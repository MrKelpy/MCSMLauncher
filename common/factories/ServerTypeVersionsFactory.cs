using System.Collections.Generic;
using System.IO;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.common.factories
{
    /// <summary>
    /// This class implements a factory method to return the correct caches to the different server types
    /// </summary>
    public class ServerTypeVersionsFactory
    {

        /// <summary>
        /// Accesses the file system and returns the correct cache file based on a provided server type, in
        /// the form of a dictionary mapping VersionName:DownloadLink
        /// </summary>
        /// <param name="serverType">The server type to get the version cache file for.</param>
        /// <returns>A dictionary mapping the version names to their server download links</returns>
        public static Dictionary<string, string> GetCacheForType(string serverType)
        {
            Section versionCache = FileSystem.GetFirstSectionNamed("versioncache");

            switch (serverType.ToLower())
            {
                case "vanilla":
                    return FileToDictionary(versionCache.GetFirstFileNamed(Constants.VANILLA_RELEASES_CACHE_FILENAME));
                case "vanilla snapshots":
                    return FileToDictionary(versionCache.GetFirstFileNamed(Constants.VANILLA_SNAPSHOTS_CACHE_FILENAME));
                case "spigot":
                    return FileToDictionary(versionCache.GetFirstFileNamed(Constants.SPIGOT_RELEASES_CACHE_FILENAME));
                case "forge":
                    return FileToDictionary(versionCache.GetFirstFileNamed(Constants.FORGE_RELEASES_CACHE_FILENAME));
                default:
                    return null;
            }
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