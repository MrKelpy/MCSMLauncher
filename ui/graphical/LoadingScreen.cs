using System;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MCSMLauncher.api;
using MCSMLauncher.common;
using MCSMLauncher.common.factories;
using MCSMLauncher.utils;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.ui.graphical
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
            InitializeComponent();
            CenterToScreen();

            // Gets the path to the clock asset and the loading screen asset from the config file.
            string clockAssetPath = ConfigurationManager.AppSettings.Get("Asset.Gif.Clock");
            string loadingScreenAsset = ConfigurationManager.AppSettings.Get("Asset.Image.LoadingScreen");
            
            // Loads the assets into the form.
            PictureBoxLoading.Image = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(clockAssetPath)));
            BackgroundImage = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(loadingScreenAsset)));
            
            FileSystem.AddSection("versioncache");
        }

        /// <summary>
        /// First checks for an internet connection, and waits until one is established, then updates the version cache
        /// and closes the form.
        /// Sends a request to every server type handler and writes the results into the version cache.
        /// These results will be used to display the versions in the server creation screen.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private async void LoadingScreen_Load(object sender, EventArgs e)
        {
            // Keeps checking if an internet connection exists, and only continues if so.
            await NetworkUtilExtensions.RecurrentTestAsync(LabelStatus);

            // Updates the cache and stops the loading phase.
            ServerTypeMappingsFactory mappingsFactory = new ();

            foreach (string serverType in mappingsFactory.GetSupportedServerTypes())
            {
                LabelStatus.Text = Logging.Logger.Info(@$"Updating the {serverType} cache...");
                await ResourceLoader.UpdateCacheFileForServerType(serverType, mappingsFactory);
            }
            
            Close();
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