using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaminariaCore_General.utils;
using LaminariaCore_Winforms.common;
using mcsm.common;
using mcsm.common.factories;
using mcsm.requests.content;
using mcsm.utils;
using static mcsm.common.Constants;

namespace mcsm.api
{
    /// <summary>
    /// This class is responsible for providing methods for all the program loading operations. <br></br>
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

        /// <summary>
        /// Downloads the program assets needed for the program to run correctly. These are stored in the assets folder.
        /// <br></br>
        /// In order for an asset to be registered, it must be in the app.config file, prefixed with "Asset".*
        /// </summary>
        /// <param name="label">An optional label used for user updates</param>
        /// <param name="progressBar">An optional progress bar for progress updates</param>
        public static async Task DownloadRegisteredAssets(Label label = null, ProgressBar progressBar = null)
        {
            // Logs the initial assets and gets the essential resources to use
            Logging.Logger.Info("Downloading initial assets...");
            Section assets = FileSystem.GetFirstSectionNamed("assets");
            List<string> config = ConfigurationManager.AppSettings.AllKeys.Where(x => x.StartsWith("Asset")).ToList();
            
            if (AreAssetsOK(assets, config)) return;

            // Keeps checking if an internet connection exists, and only continues if so.
            await NetworkUtilExtensions.RecurrentTestAsync(label);

            // Resets the assets folder just in case
            FileSystem.RemoveSection(assets?.Name);
            FileSystem.AddSection("assets");

            // Iterates over every configuration key and downloads the file corresponding to it.
            // While that is happening, updates the loading bar.
            for (int index = 0; index < config.Count; index++)
            {
                string settingKey = config[index];

                string filename = Path.GetFileName(ConfigurationManager.AppSettings.Get(settingKey));
                string filepath = Path.Combine(FileSystem.GetFirstSectionNamed("assets").SectionFullPath, filename);

                if (label != null) label.Text = $@"Downloading Assets... ({filename})";
                await FileDownloader.DownloadFileAsync(filepath, ConfigurationManager.AppSettings.Get(settingKey));
                if (progressBar != null) progressBar.Value = (int)(((double)index + 1) / config.Count * 100);
            }
            
        }

        /// <summary>
        /// Iterates over every registered asset and tries to load it as a bitmap. If it fails, it means
        /// that the asset is corrupted or missing. Also fails if the amount of assets in the assets folder
        /// is less than the amount registered in the app.config file.
        /// </summary>
        /// <param name="assets">The assets section to get the files from</param>
        /// <param name="config">The configuration file to evaluate</param>
        /// <returns>Whether or not all the assets are ready to be used</returns>
        private static bool AreAssetsOK(Section assets, List<string> config)
        {
            // Quick check to see if the assets folder has the same amount of files as the config file.
            if (assets?.GetAllDocuments().Length != config.Count)
                return false;
            
            // Tries to load every asset as a bitmap. If it fails, it means that the asset is corrupted or missing.
            try
            {
                foreach (string filepath in assets.GetAllDocuments())
                    using (Bitmap _ = new(filepath)) {}
                
                return true;
            }
            catch (ArgumentException) { return false; }
        }
    }
}