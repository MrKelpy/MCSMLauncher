using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaminariaCore_Winforms.common;
using MCSMLauncher.common;
using MCSMLauncher.requests.content;
using MCSMLauncher.ui.common;
using MCSMLauncher.utils;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.ui.graphical
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
            InitializeComponent();
            CenterToParent();
        }

        /// <summary>
        /// Performs any necessary pre loading tasks.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private async void PreLoadingScreen_Load(object sender, EventArgs e)
        {
            try
            {
                await ResourceLoader.DownloadRegisteredAssets(LabelDownloadingAsset, ProgressBarDownload);
                Close();
            }
            catch (Exception err)
            {
                Logging.Logger.Fatal(@"An unexpected error occured and the program was forced to exit.");
                Logging.Logger.Fatal(err.Message + "\n" + err.StackTrace, LoggingType.File);
            }
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