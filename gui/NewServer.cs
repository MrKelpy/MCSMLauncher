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
using MCSMLauncher.common;
using MCSMLauncher.common.factories;
using MCSMLauncher.common.server.builders.abstraction;
using MCSMLauncher.utils;
using PgpsUtilsAEFC.common;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.gui
{
    /// <summary>
    /// This singleton form handles any operations related to the creation of a new server.
    /// </summary>
    public partial class NewServer : Form
    {
        /// <summary>
        /// Main constructor for the NewServer form. Private in order to enforce the usage
        /// of the instance declared above.
        /// </summary>
        private NewServer()
        {
            this.InitializeComponent();

            // Loads the images for the form
            this.PictureBoxLoading.Image =
                Image.FromFile(
                    FileSystem.GetFirstDocumentNamed(
                        Path.GetFileName(ConfigurationManager.AppSettings.Get("Asset.Gif.Loader"))));
            this.ButtonFolderBrowser.Image =
                Image.FromFile(FileSystem.GetFirstDocumentNamed(
                    Path.GetFileName(ConfigurationManager.AppSettings.Get("Asset.Icon.FolderBrowser"))));

            foreach (Label label in this.NewServerLayout.Controls.OfType<Label>()
                         .Where(x => x.Tag != null && x.Tag.ToString().Equals("tooltip")).ToList())
            {
                label.BackgroundImage =
                    Image.FromFile(FileSystem.GetFirstDocumentNamed(
                        Path.GetFileName(ConfigurationManager.AppSettings.Get("Asset.Icon.Tooltip"))));
                label.BackgroundImageLayout = ImageLayout.Zoom;
            }

            // Sets the server types inside the server type box
            this.ComboBoxServerType.Items.AddRange(new ServerTypeMappingsFactory().GetSupportedServerTypes()
                .Select(x => CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x)).ToArray<object>());

            // Checks all the java versions available in Program Files and sets them in the java version box
            string programFilesJavaPath =
                Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramW6432%"), "Java");
            string programFilesX86JavaPath =
                Path.Combine(Environment.ExpandEnvironmentVariables("%ProgramFiles(x86)%"), "Java");

            if (Directory.Exists(programFilesJavaPath))
                this.ComboBoxJavaVersion.Items.AddRange(
                    Directory.GetDirectories(programFilesJavaPath).ToArray<object>());

            if (Directory.Exists(programFilesX86JavaPath))
                this.ComboBoxJavaVersion.Items.AddRange(Directory.GetDirectories(programFilesX86JavaPath)
                    .ToArray<object>());

            // Automatically selects the first java version if there is one
            this.ComboBoxJavaVersion.SelectedIndex = this.ComboBoxJavaVersion.Items.Count > 0 ? 0 : -1;
        }

        /// <summary>
        /// The instance of the class to use, matching the singleton model.
        /// </summary>
        public static NewServer INSTANCE { get; } = new NewServer();

        /// <summary>
        /// The list of invalid server names, used to check if the server name is valid.
        /// These names are reserved by Windows for special folders.
        /// </summary>
        private List<string> InvalidServerNames { get; } = new List<string>
        {
            "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6",
            "COM7", "COM8", "COM9", "COM0", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5",
            "LPT6", "LPT7", "LPT8", "LPT9", "LPT0"
        };

        /// <summary>
        /// Returns the layout of the current form, so that it can be added to another form.
        /// </summary>
        /// <returns>A Panel representing this form's layout.</returns>
        public Panel GetLayout()
        {
            return this.NewServerLayout;
        }

        /// <summary>
        /// Toggles the state of the controls on the form, switching between the loading state, where
        /// the user can't interact with the form, and the normal state, where the user can.
        /// </summary>
        /// <param name="enabled">Whether or not the user can interact with the controls.</param>
        public void ToggleControlsState(bool enabled)
        {
            this.TextBoxServerName.Enabled = this.ComboServerVersion.Enabled = this.ComboBoxServerType.Enabled
                = this.ComboBoxJavaVersion.Enabled =
                    this.ButtonFolderBrowser.Enabled = this.ButtonBuild.Visible = enabled;
            this.PictureBoxLoading.Visible = !enabled;
        }

        /// <summary>
        /// Unlocks or changes the contents of the server version box based on the selected
        /// server type.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ComboBoxServerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ComboBoxServerType.Text == "") return;

            // Prepares the server version box for the new server type version list
            this.ButtonBuild.Enabled = false;
            this.ComboServerVersion.Enabled = true;
            this.ComboServerVersion.Items.Clear();
            this.ComboServerVersion.ForeColor = Color.Black;

            // Adds the versions and snapshots (if applicable) to the server version box. If the server type is not
            // recognized, the versions box is disabled.
            try
            {
                Dictionary<string, string> cache =
                    new ServerTypeMappingsFactory().GetCacheContentsForType(this.ComboBoxServerType.Text);

                foreach (KeyValuePair<string, string> item in cache) this.ComboServerVersion.Items.Add(item.Key);
            }

            // If we can't find any versions for the server type, a null reference exception will be thrown.
            // In that case, block the version selection with a message.
            catch (NullReferenceException)
            {
                Logging.LOGGER.Warn($"Couldn't load any versions for the {this.ComboBoxServerType.Text} server type.",
                    LoggingType.FILE);
                this.ComboServerVersion.Items.Add(@"Couldn't load any versions for this server type.");
                this.ComboServerVersion.ForeColor = Color.Firebrick;
                this.ComboServerVersion.SelectedIndex = 0;
                this.ComboServerVersion.Enabled = this.ButtonBuild.Enabled = false;
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
            this.LabelServerNameError.Visible = false;
            this.RichTextBoxConsoleOutput.Clear();
            this.RichTextBoxConsoleOutput.ForeColor = Color.Black;

            // Prevents invalid characters in the server name
            if (this.TextBoxServerName.Text.ToList().Any(Path.GetInvalidPathChars().Contains) ||
                this.TextBoxServerName.Text.Contains(' '))
            {
                this.LabelServerNameError.Text = @"Invalid characters in server name.";
                this.LabelServerNameError.Visible = true;
                return;
            }

            // Prevents invalid server names
            if (this.InvalidServerNames.Any(x => x.ToUpper().Equals(this.TextBoxServerName.Text.ToUpper())))
            {
                this.LabelServerNameError.Text = @"Invalid server name.";
                this.LabelServerNameError.Visible = true;
                return;
            }

            // Prevent two servers from having the same name
            if (serversSection.GetAllSections().Any(x => x.SimpleName == this.TextBoxServerName.Text))
            {
                this.LabelServerNameError.Text = @"A server with that name already exists.";
                this.LabelServerNameError.Visible = true;
                return;
            }

            // Starts to build the server, first disabling the controls so the user can't interact with them,
            // then building the server, and finally re-enabling the controls.
            this.ToggleControlsState(false);

            try
            {
                AbstractServerBuilder builder =
                    new ServerTypeMappingsFactory().GetBuilderFor(this.ComboBoxServerType.Text);
                await builder.Build(this.TextBoxServerName.Text, this.ComboBoxServerType.Text,
                    this.ComboServerVersion.Text);
            }

            // If a timeout exception happened, log it and tell the user that a timeout happened
            catch (TimeoutException err)
            {
                Logging.LOGGER.Error(err.StackTrace);
                MessageBox.Show(
                    $"The time limit for the downloads has exceeded. (Request timed out) {Environment.NewLine}Please try again later.",
                    @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                serversSection.RemoveSection(this.TextBoxServerName.Text);
            }

            // If a network error happened, log it and tell the user that a network error happened
            catch (HttpRequestException err)
            {
                Logging.LOGGER.Error(err.StackTrace);
                string errorMessage = !NetworkUtils.IsWifiConnected()
                    ? $"A network error happened while building the server. {Environment.NewLine}Please check your internet connection and try again."
                    : $"Could not establish a connection to the download servers. {Environment.NewLine}Please try again later, the download servers for this type and version might be down!";

                MessageBox.Show(errorMessage, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                serversSection.RemoveSection(this.TextBoxServerName.Text);
            }

            // If an unknown exception was raised, log it as such and tell the user that an error occured
            catch (Exception err)
            {
                Logging.LOGGER.Error(err.StackTrace);
                MessageBox.Show($"An error occurred while building the server. {Environment.NewLine}Please try again.",
                    @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                serversSection.RemoveSection(this.TextBoxServerName.Text);
            }

            this.ToggleControlsState(true);
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
            this.ButtonBuild.Enabled = this.TextBoxServerName.Text.Length > 0 && this.ComboServerVersion.Enabled;
        }

        /// <summary>
        /// When the server name is written into, unlocks the build button, if and only if the server version
        /// is selected.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void TextBoxServerName_TextChanged(object sender, EventArgs e)
        {
            this.LabelServerNameError.Visible = false;
            this.ButtonBuild.Enabled = this.TextBoxServerName.Text.Length > 0 &&
                                       this.ComboServerVersion.Text.Length > 0 && this.ComboServerVersion.Enabled;
        }

        /// <summary>
        /// Brings up the folder browser dialog, allowing the user to select
        /// their preferred java version folder.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void FolderBrowserButton_Click(object sender, EventArgs e)
        {
            DialogResult result = this.FolderBrowser.ShowDialog();
            if (result == DialogResult.OK)
            {
                int index = this.ComboBoxJavaVersion.Items.Add(this.FolderBrowser.SelectedPath);
                this.ComboBoxJavaVersion.SelectedItem = this.ComboBoxJavaVersion.Items[index];
            }
        }
    }
}