using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Windows.Forms;
using LaminariaCore_General.utils;
using LaminariaCore_Winforms.common;
using MCSMLauncher.api.server;
using MCSMLauncher.common;
using MCSMLauncher.common.caches;
using MCSMLauncher.common.factories;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.ui.graphical
{
    /// <summary>
    /// This singleton form handles any operations related to the creation of a new server.
    /// </summary>
    public partial class NewServer : Form
    {
        /// <summary>
        /// The API instance to interact with the servers.
        /// </summary>
        private ServerAPI ServerAPI { get; } = new ();
        
        /// <summary>
        /// Main constructor for the NewServer form. Private in order to enforce the usage
        /// of the instance declared above.
        /// </summary>
        private NewServer()
        {
            InitializeComponent();

            // Loads the images for the form from the assets folder
            string loadingGif = ConfigurationManager.AppSettings.Get("Asset.Gif.Loader");
            string folderBrowser = ConfigurationManager.AppSettings.Get("Asset.Icon.FolderBrowser");
            string tooltipImage = ConfigurationManager.AppSettings.Get("Asset.Icon.Tooltip");
            
            PictureBoxLoading.Image = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(loadingGif)));
            ButtonFolderBrowser.Image = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(folderBrowser)));
            Image tooltip = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(tooltipImage)));

            // Iterates through all the labels in the form and sets the tooltip image if they're marked as such
            foreach (Label label in NewServerLayout.Controls.OfType<Label>()
                         .Where(x => x.Tag != null && x.Tag.ToString().Equals("tooltip")).ToList())
            {
                label.BackgroundImage = tooltip;
                label.BackgroundImageLayout = ImageLayout.Zoom;
            }

            // Sets the server types inside the server type box
            ComboBoxServerType.Items.AddRange(new ServerTypeMappingsFactory().GetSupportedServerTypes()
                .Select(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x)).ToArray<object>());

            // Checks all the java versions available in Program Files and sets them in the java version box
            string programFilesJavaPath = Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramW6432%"), "Java");
            string programFilesX86JavaPath = Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%"), "Java");

            if (Directory.Exists(programFilesJavaPath))
                ComboBoxJavaVersion.Items.AddRange(
                    Directory.GetDirectories(programFilesJavaPath).ToArray<object>());

            if (Directory.Exists(programFilesX86JavaPath))
                ComboBoxJavaVersion.Items.AddRange(Directory.GetDirectories(programFilesX86JavaPath)
                    .ToArray<object>());

            // Automatically selects the first java version if there is one
            ComboBoxJavaVersion.SelectedIndex = ComboBoxJavaVersion.Items.Count > 0 ? 0 : -1;
        }

        /// <summary>
        /// The instance of the class to use, matching the singleton model.
        /// </summary>
        public static NewServer Instance { get; } = new ();

        /// <summary>
        /// Returns the layout of the current form, so that it can be added to another form.
        /// </summary>
        /// <returns>A Panel representing this form's layout.</returns>
        public Panel GetLayout() =>  NewServerLayout;

        /// <summary>
        /// Toggles the state of the controls on the form, switching between the loading state, where
        /// the user can't interact with the form, and the normal state, where the user can.
        /// </summary>
        /// <param name="enabled">Whether or not the user can interact with the controls.</param>
        private void ToggleControlsState(bool enabled)
        {
            TextBoxServerName.Enabled = ComboServerVersion.Enabled = ComboBoxServerType.Enabled
                = ComboBoxJavaVersion.Enabled = ButtonFolderBrowser.Enabled = ButtonBuild.Visible = enabled;
            
            PictureBoxLoading.Visible = !enabled;
        }

        /// <summary>
        /// Unlocks or changes the contents of the server version box based on the selected
        /// server type.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ComboBoxServerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ComboBoxServerType.Text == "") return;

            // Prepares the server version box for the new server type version list
            ButtonBuild.Enabled = false;
            ComboServerVersion.Enabled = true;
            ComboServerVersion.Items.Clear();
            ComboServerVersion.ForeColor = Color.Black;

            // Adds the versions and snapshots (if applicable) to the server version box. If the server type is not
            // recognized, the versions box is disabled.
            try
            {
                Dictionary<string, string> cache =
                    new ServerTypeMappingsFactory().GetCacheContentsForType(ComboBoxServerType.Text);

                foreach (KeyValuePair<string, string> item in cache) ComboServerVersion.Items.Add(item.Key);
            }

            // If we can't find any versions for the server type, a null reference exception will be thrown.
            // In that case, block the version selection with a message.
            catch (NullReferenceException)
            {
                Logging.Logger.Warn($"Couldn't load any versions for the {ComboBoxServerType.Text} server type.",
                    LoggingType.File);
                
                ComboServerVersion.Items.Add(@"Couldn't load any versions for this server type.");
                ComboServerVersion.ForeColor = Color.Firebrick;
                ComboServerVersion.SelectedIndex = 0;
                ComboServerVersion.Enabled = ButtonBuild.Enabled = false;
            }
        }

        /// <summary>
        /// Builds the server according to the type and version selected.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        [SuppressMessage("ReSharper", "LocalizableElement")]
        private async void ButtonBuild_Click(object sender, EventArgs e)
        {
            Section serversSection = FileSystem.AddSection("servers");
            LabelServerNameError.Visible = false;
            RichTextBoxConsoleOutput.Clear(); RichTextBoxConsoleOutput.ForeColor = Color.Black;

            try
            {
                ServerBuilder builder = ServerAPI.Builder(TextBoxServerName.Text, ComboBoxServerType.Text,
                    ComboServerVersion.Text);
                
                builder.VerifyInformation();
                
                // Starts to build the server, first disabling the controls so the user can't interact with them,
                // then building the server, and finally re-enabling the controls.
                ToggleControlsState(false);

                // Runs the server builder with the output handler set to the console text box
                MessageProcessingOutputHandler outputSystem = new(RichTextBoxConsoleOutput);
                builder.Run(outputSystem);
            }

            // If a timeout exception happened, log it and tell the user that a timeout happened
            catch (TimeoutException err)
            {
                Logging.Logger.Error(err.StackTrace);
                MessageBox.Show($"The time limit for the downloads has exceeded. (Request timed out) {Environment.NewLine}Please try again later.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                GlobalEditorsCache.INSTANCE.Remove(TextBoxServerName.Text);
                serversSection.RemoveSection(TextBoxServerName.Text);
            }

            // If a network error happened, log it and tell the user that a network error happened
            catch (HttpRequestException err)
            {
                Logging.Logger.Error(err);
                string errorMessage = !NetworkUtils.IsWifiConnected()
                    ? $"A network error happened while building the server. {Environment.NewLine}Please check your internet connection and try again."
                    : $"Could not establish a connection to the download servers. {Environment.NewLine}Please try again later, the download servers for this type and version might be down!";

                MessageBox.Show(errorMessage, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                GlobalEditorsCache.INSTANCE.Remove(TextBoxServerName.Text);
                serversSection.RemoveSection(TextBoxServerName.Text);
            }

            // If an unknown exception was raised, log it as such and tell the user that an error occured
            catch (Exception err)
            {
                Logging.Logger.Error(err);
                MessageBox.Show($"An error occurred while building the server. {Environment.NewLine}Please try again.",
                    @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                GlobalEditorsCache.INSTANCE.Remove(TextBoxServerName.Text);
                serversSection.RemoveSection(TextBoxServerName.Text);
            }

            ToggleControlsState(true);
            await ServerList.INSTANCE.RefreshGridAsync();
        }

        /// <summary>
        /// When a version is selected, unlocks the build button if and only if the server name field
        /// is also filled.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ComboServerVersion_SelectedIndexChanged(object sender, EventArgs e)
        {
            ButtonBuild.Enabled = TextBoxServerName.Text.Length > 0 && ComboServerVersion.Enabled;
        }

        /// <summary>
        /// When the server name is written into, unlocks the build button, if and only if the server version
        /// is selected.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void TextBoxServerName_TextChanged(object sender, EventArgs e)
        {
            LabelServerNameError.Visible = false;
            ButtonBuild.Enabled = TextBoxServerName.Text.Length > 0 &&
                                       ComboServerVersion.Text.Length > 0 && ComboServerVersion.Enabled;
        }

        /// <summary>
        /// Brings up the folder browser dialog, allowing the user to select
        /// their preferred java version folder.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void FolderBrowserButton_Click(object sender, EventArgs e)
        {
            DialogResult result = FolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                int index = ComboBoxJavaVersion.Items.Add(FolderBrowser.SelectedPath);
                ComboBoxJavaVersion.SelectedItem = ComboBoxJavaVersion.Items[index];
            }
        }
    }
}