using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common;
using MCSMLauncher.common.factories;
using MCSMLauncher.requests;
using PgpsUtilsAEFC.common;

namespace MCSMLauncher.gui
{
    /// <summary>
    /// This singleton form handles any operations related to the creation of a new server.
    /// </summary>
    public partial class NewServer : Form
    {
        /// <summary>
        /// The instance of the class to use, matching the singleton model.
        /// </summary>
        public static NewServer INSTANCE { get; } = new NewServer();

        /// <summary>
        /// Main constructor for the NewServer form. Private in order to enforce the usage
        /// of the instance declared above.
        /// </summary>
        private NewServer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns the layout of the current form, so that it can be added to another form.
        /// </summary>
        /// <returns>A Panel representing this form's layout.</returns>
        public Panel GetLayout() => this.NewServerLayout;

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
            ComboServerVersion.Enabled = true;
            ComboServerVersion.Items.Clear();
            ComboServerVersion.ForeColor = Color.Black;

            // Adds the versions and snapshots (if applicable) to the server version box. If the server type is not
            // recognized, the versions box is disabled.
            try
            {
                Dictionary<string, string> cache = ServerTypeVersionsFactory.GetCacheForType(ComboBoxServerType.Text);

                foreach (KeyValuePair<string, string> item in cache)
                    ComboServerVersion.Items.Add(item.Key);
            }
            
            // If we can't find any versions for the server type, a null reference exception will be thrown.
            // In that case, block the version selection with a message.
            catch (NullReferenceException)
            {
                Logging.LOGGER.Warn($"Couldn't load any versions for the {ComboBoxServerType.Text} server type.", LoggingType.FILE);
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
        private void ButtonBuild_Click(object sender, EventArgs e)
        {
            Section serversSection = Constants.FileSystem.AddSection("servers");
            LabelServerNameError.Visible = false;
            
            // Prevent two servers from having the same name
            if (serversSection.GetAllSections().Any(x => x.Name == TextBoxServerName.Text))
            {
                LabelServerNameError.Text = @"A server with that name already exists.";
                return;
            }

            serversSection.AddSection(TextBoxServerName.Text);
            
            
            TextBoxServerName.Text = string.Empty;
            ComboBoxServerType.Text = ComboServerVersion.Text = null;
        }

        /// <summary>
        /// When a version is selected, unlocks the build button if and only if the server name field
        /// is also filled.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ComboServerVersion_SelectedIndexChanged(object sender, EventArgs e) =>
            ButtonBuild.Enabled = TextBoxServerName.Text.Length > 0 && ComboServerVersion.Enabled;

        /// <summary>
        /// When the server name is written into, unlocks the build button, if and only if the server version
        /// is selected.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void TextBoxServerName_TextChanged(object sender, EventArgs e) =>
            ButtonBuild.Enabled = TextBoxServerName.Text.Length > 0 && ComboServerVersion.Text.Length > 0 && ComboServerVersion.Enabled;
    }
}
