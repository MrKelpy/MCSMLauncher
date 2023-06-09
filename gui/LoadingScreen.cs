﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common;
using MCSMLauncher.common.factories;
using MCSMLauncher.utils;
using PgpsUtilsAEFC.utils;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.gui
{
    /// <summary>
    /// This form serves as a loading screen for the launcher, which gets displayed as the launcher prepares
    /// to be used.
    /// </summary>
    public sealed partial class LoadingScreen : Form
    {
        /// <summary>
        /// Main constructor for the LoadingScreen form. Prepares everything for the loading screen to be displayed, and for
        /// its functions to be executed.
        /// </summary>
        public LoadingScreen()
        {
            this.InitializeComponent();
            this.CenterToScreen();

            this.PictureBoxLoading.Image =
                Image.FromFile(
                    FileSystem.GetFirstDocumentNamed(
                        Path.GetFileName(ConfigurationManager.AppSettings.Get("Asset.Gif.Clock"))));
            this.BackgroundImage =
                Image.FromFile(FileSystem.GetFirstDocumentNamed(
                    Path.GetFileName(ConfigurationManager.AppSettings.Get("Asset.Image.LoadingScreen"))));
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
            await NetworkUtils.RecurrentTestAsync(this.LabelStatus);

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
            ServerTypeMappingsFactory mappingsFactory = new ServerTypeMappingsFactory();

            // Iterates through every server type, and updates the cache for each one.
            foreach (string serverType in mappingsFactory.GetSupportedServerTypes())
            {
                this.LabelStatus.Text = Logging.LOGGER.Info(@$"Updating the {serverType} cache...");

                Dictionary<string, string> versions = await mappingsFactory.GetHandlerFor(serverType).GetVersions();
                string cachePath = mappingsFactory.GetCacheFileFor(serverType);

                if (versions == null) Logging.LOGGER.Warn($"Failed to retrieve versions for {serverType}.");

                // If we couldn't retrieve any versions for the server type, and the cache has content in it, keep it. 
                if (mappingsFactory.GetCacheContentsForType(serverType)?.Count > 0 && versions == null)
                {
                    Logging.LOGGER.Info($"Using previously cached versions for {serverType}.");
                    continue;
                }

                // Writes the cache into its correct cache filepath, in the format "version>url".
                FileUtils.DumpToFile(cachePath,
                    versions == null
                        ? new List<string>()
                        : versions.ToList().Select(x => $"{x.Key}>{x.Value}").ToList());
            }
        }

        /// <summary>
        /// Cancels the loading phase and closes the application if the user manages to close the app.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void LoadingScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ReSharper disable once SimplifyLinqExpressionUseAll
            if (!(new StackTrace().GetFrames() ?? Array.Empty<StackFrame>()).Any(x => x.GetMethod().Name == "Close"))
                Environment.Exit(1);
        }
    }
}