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
using MCSMLauncher.requests.content;
using MCSMLauncher.utils;
using PgpsUtilsAEFC.common;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.gui
{
    /// <summary>
    /// This form is meant to be displayed before the loading screen, to let the user know that some assets
    /// are being downloaded, and to show progress.
    /// </summary>
    public partial class PreLoadingScreen : Form
    {
        /// <summary>
        /// Main constructor for the InitialInstallationPrompt class. Initializes the components
        /// in the form.
        /// </summary>
        public PreLoadingScreen()
        {
            this.InitializeComponent();
            this.CenterToParent();
        }

        /// <summary>
        /// Performs any necessary pre loading tasks.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private async void PreLoadingScreen_Load(object sender, EventArgs e)
        {
            // Downloads the initial assets
            try
            {
                await this.DownloadInitialAssets();
            }
            catch (Exception err)
            {
                Logging.LOGGER.Fatal(@"An unexpected error occured and the program was forced to exit.");
                Logging.LOGGER.Fatal(err.Message + "\n" + err.StackTrace, LoggingType.FILE);
            }
        }

        /// <summary>
        /// Changes the text in the Downloading Assets label to display the currently downloading asset.
        /// </summary>
        /// <param name="assetName">The currently downloading asset name</param>
        public void SetDownloadingAssetName(string assetName)
        {
            this.LabelDownloadingAsset.Text = $@"Downloading Assets... ({assetName})";
        }

        /// <summary>
        /// Downloads the initial assets to allow the program to start up correctly without
        /// taking too much space.
        /// Whilst this initial download is taking place, show a small form with a loading bar indicating
        /// progress.
        /// </summary>
        private async Task DownloadInitialAssets()
        {
            // Logs the initial assets and gets the essential resources to use
            Logging.LOGGER.Info("Downloading initial assets...");
            Section assets = FileSystem.GetFirstSectionNamed("assets");
            List<string> config = ConfigurationManager.AppSettings.AllKeys.Where(x => x.StartsWith("Asset")).ToList();

            // Checks if the assets folder contains all the files. If it does, then try to check if they aren't corrupted
            // by trying to convert them to a bitmap. If none are corrupted, then return.
            // If one of them is, or assets has no files, then continue. 
            try
            {
                if (assets?.GetAllDocuments().Length != config.Count) throw new ArgumentException();
                foreach (string filepath in assets.GetAllDocuments())
                    using (Bitmap _ = new Bitmap(filepath))
                    {
                    }

                this.Close();
                return;
            }
            catch (ArgumentException)
            {
            } // ignored

            // Keeps checking if an internet connection exists, and only continues if so.
            await NetworkUtils.RecurrentTestAsync(this.LabelDownloadingAsset);

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

                this.SetDownloadingAssetName(filename);
                await FileDownloader.DownloadFileAsync(filepath, ConfigurationManager.AppSettings.Get(settingKey));
                this.ProgressBarDownload.Value = (int)(((double)index + 1) / config.Count * 100);
            }

            this.Close();
        }

        /// <summary>
        /// Cancels the pre loading phase and closes the application if the user manages to close the app.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void PreLoadingScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ReSharper disable once SimplifyLinqExpressionUseAll
            if (!(new StackTrace().GetFrames() ?? Array.Empty<StackFrame>()).Any(x => x.GetMethod().Name == "Close"))
                Environment.Exit(1);
        }
    }
}