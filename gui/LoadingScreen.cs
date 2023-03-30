using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common;
using MCSMLauncher.requests.forge;
using MCSMLauncher.requests.mcversions;
using MCSMLauncher.requests.spigot;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.gui
{
    /// <summary>
    /// This form serves as a loading screen for the launcher, which gets displayed as the launcher prepares
    /// to be used.
    /// </summary>
    public partial class LoadingScreen : Form
    {
        /// <summary>
        /// Main constructor for the LoadingScreen form. Prepares everything for the loading screen to be displayed, and for
        /// its functions to be executed.
        /// </summary>
        public LoadingScreen()
        {
            InitializeComponent();
            CenterToScreen();
            FileSystem.AddSection("versioncache");
        }
        
        /// <summary>
        /// Runs the actual loading logic, once the form has been loaded.
        /// First checks for an internet connection, and waits until one is estabilished, then updates the version cache
        /// and closes the form.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private async void LoadingScreen_Load(object sender, EventArgs e)
        {
            // Keeps checking if an internet connection exists, and only continues if so.
            while (true)
            {
                Logging.LOGGER.Info(@"Checking for an internet connection...");
                if (NetworkTester.IsWifiConnected()) break;
                LabelStatus.Text = @"Could not connect to the internet. Retrying...";
                await Task.Delay(2 * 1000);
            }

            // Updates the cache and stops the loading phase.
            await this.UpdateVersionCache();
            this.Close();
        }

        /// <summary>
        /// Sends a request to every server type handler and writes the results into the version cache.
        /// These results will be used to display the versions in the server creation screen.
        /// </summary>
        private async Task UpdateVersionCache()
        {
            // TODO: Fix this horrible code
            
            LabelStatus.Text = Logging.LOGGER.Info(@"Updating the vanilla versions cache...");
            var vanillaReleases = await new MCVRequestHandler().GetVersions();
            
            LabelStatus.Text = Logging.LOGGER.Info(@"Updating the vanilla snapshots versions cache...");
            var vanillaSnapshots = await new MCVRequestHandler().GetSnapshots();
            
            LabelStatus.Text = Logging.LOGGER.Info(@"Updating the spigot versions cache...");
            var spigotReleases = await new SpigotRequestHandler().GetVersions();
            
            LabelStatus.Text = Logging.LOGGER.Info(@"Updating the forge versions cache...");
            var forgeReleases = await new ForgeRequestHandler().GetVersions();

            // Adds the version cache files into the versioncache section
            Section versionCache = FileSystem.GetFirstSectionNamed("versioncache");
            string vanillaReleasesPath = versionCache.AddDocument(Constants.VANILLA_RELEASES_CACHE_FILENAME);
            string vanillaSnapshotsPath = versionCache.AddDocument(Constants.VANILLA_SNAPSHOTS_CACHE_FILENAME);
            string spigotReleasesPath = versionCache.AddDocument(Constants.SPIGOT_RELEASES_CACHE_FILENAME);
            string forgeReleasesPath = versionCache.AddDocument(Constants.FORGE_RELEASES_CACHE_FILENAME);
            
            // Writes the caches into their files
            FileUtils.DumpToFile(vanillaReleasesPath, vanillaReleases == null ? new List<string>() : 
                vanillaReleases.ToList().Select(x => $"{x.Key}>{x.Value}").ToList());
            
            FileUtils.DumpToFile(vanillaSnapshotsPath, vanillaSnapshots == null ? new List<string>() : 
                vanillaSnapshots.ToList().Select(x => $"{x.Key}>{x.Value}").ToList());
            
            FileUtils.DumpToFile(spigotReleasesPath, spigotReleases == null ? new List<string>() : 
                spigotReleases.ToList().Select(x => $"{x.Key}>{x.Value}").ToList());
            
            FileUtils.DumpToFile(forgeReleasesPath, forgeReleases == null ? new List<string>() : 
                forgeReleases.ToList().Select(x => $"{x.Key}>{x.Value}").ToList());
        }
    }
}