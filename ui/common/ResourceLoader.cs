using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LaminariaCore_General.utils;
using MCSMLauncher.common;
using MCSMLauncher.common.factories;

namespace MCSMLauncher.ui.common
{
    /// <summary>
    /// This class is responsible for providing endpoints for all the program loading operations. <br></br>
    /// Acts as a middleman between the UI and the backend.
    /// </summary>
    public static class ResourceLoader
    {
        
        /// <summary>
        /// Uses the server-type handler to retrieve the versions for the server type, and updates the set cache file
        /// for it with the results.
        /// </summary>
        /// <param name="serverType">The server type to update</param>
        /// <param name="factory">The ServerTypeMappingsFactory to use in order to get the version contents</param>
        public static async Task UpdateCacheFileForServerType(string serverType, ServerTypeMappingsFactory factory)
        {
            // Accesses the mappings factory and retrieves the versions for the server type, as well as the cache file path.
            Dictionary<string, string> versions = await factory.GetHandlerFor(serverType).GetVersions();
            string cachePath = factory.GetCacheFileFor(serverType);

            if (versions == null) Logging.Logger.Warn($"Failed to retrieve versions for {serverType}.");

            // If we couldn't retrieve any versions for the server type, and the cache has content in it, keep it. 
            if (factory.GetCacheContentsForType(serverType)?.Count > 0 && versions == null)
            {
                Logging.Logger.Info($"Using previously cached versions for {serverType}.");
                return;
            }

            // Writes the cache into its correct cache filepath, in the format "version>url".
            List<string> content = versions == null
                ? new List<string>()
                : versions.ToList().Select(x => $"{x.Key}>{x.Value}").ToList();
            
            FileUtils.DumpToFile(cachePath, content);
        }
        
    }
}