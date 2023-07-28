using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MCSMLauncher.common;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.gui
{
    /// <summary>
    /// This form aims to provide an interface to edit the settings of a server.
    /// </summary>
    public partial class ServerEditPrompt : Form
    {
        /// <summary>
        /// The server directory to edit. This is purely here for clarity.
        /// </summary>
        private Section ServerSection { get; set; }
        
        /// <summary>
        /// The ServerEditor instance to use with this editing prompt.
        /// </summary>
        private ServerEditor Editor { get; }

        /// <summary>
        /// Main constructor for the ServerEditPrompt form. Initialises the form and loads the
        /// information from the server.properties file into the form.
        /// </summary>
        /// <param name="editor">The ServerEditor instance to use with this editing prompt</param>
        public ServerEditPrompt(ServerEditor editor)
        {
            InitializeComponent();
            StartPosition = FormStartPosition.CenterParent;
            ServerSection = editor.ServerSection;
            Editor = editor;

            // Loads the properties and settings into the form
            LoadToForm(editor.GetBuffersCopy());

            // Edits some values in the form that have to be manually placed
            CheckBoxCracked.Checked = !CheckBoxCracked.Checked;
            CheckBoxSpawnProtection.Checked = editor.BuffersContain("spawn-protection") && editor.GetFromBuffers<int>("spawn-protection") > 0;
            TextBoxServerName.Text = ServerSection.SimpleName;

            // Loads the icons for the folder browsing buttons
            ButtonFolderBrowsing.Image = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(ConfigurationManager.AppSettings.Get("Asset.Icon.FolderBrowser"))));
            ButtonFolderBrowsing2.Image = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(ConfigurationManager.AppSettings.Get("Asset.Icon.FolderBrowser"))));
            ButtonFolderBrowsing3.Image = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(ConfigurationManager.AppSettings.Get("Asset.Icon.FolderBrowser"))));

            // Sets the info layout pictures
            foreach (Label label in Controls.OfType<Label>().Where(x => x.Tag != null && x.Tag.ToString().Equals("tooltip")).ToList())
            {
                label.BackgroundImage = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(ConfigurationManager.AppSettings.Get("Asset.Icon.Tooltip"))));
                label.BackgroundImageLayout = ImageLayout.Zoom;
            }
        }

        /// <summary>
        /// Switches the focus to the first label in the form when the form is loaded, so that nothing
        /// is selected.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ServerEditPrompt_Load(object sender, EventArgs e)
        {
            ActiveControl = label1;
        }

        /// <summary>
        /// Collects all the information from the form and returns it as a dictionary.
        /// </summary>
        /// <returns>A dictionary containing all of the settings in the form as a dictionary</returns>
        private Dictionary<string, string> FormToDictionary()
        {
            Dictionary<string, string> formInformation = new();

            // Gets the information from the valid controls, excluding the buttons and the checkboxes,
            // and adds it them to the dictionary
            Controls.OfType<Control>()
                .Where(x => x.GetType() != typeof(CheckBox) && x.GetType() != typeof(Button) && x.Tag != null && x.Tag.ToString() != string.Empty).ToList()
                .ForEach(x => formInformation.Add(x.Tag.ToString(), x.Text));

            // Does the same, but specifically for checkboxes, since they have to be parsed for booleans
            Controls.OfType<CheckBox>()
                .Where(x => x.Tag != null && x.Tag.ToString() != string.Empty).ToList()
                .ForEach(x => formInformation.Add(x.Tag.ToString(), x.Checked.ToString().ToLower()));

            // Inverts the online mode since the checkbox is named "Cracked" (Opposite of online mode), and
            // sets the spawn protection to 0 if it is disabled in the form
            formInformation["online-mode"] = (!bool.Parse(formInformation["online-mode"])).ToString().ToLower();
            if (!CheckBoxSpawnProtection.Checked) formInformation["spawn-protection"] = "0";
            
            return formInformation;
        }

        /// <summary>
        /// Loads a dictionary with information coming from the server.properties file into the
        /// fields inside of the ServerEditPrompt form. Every key is mapped to a tag existing in every
        /// Text, Combo and Numeric box.
        /// </summary>
        /// <param name="dictionaryToLoad">The dictionary to load into the form</param>
        private void LoadToForm(Dictionary<string, string> dictionaryToLoad)
        {
            foreach (KeyValuePair<string, string> item in dictionaryToLoad)
            {
                // Finds the control with the same tag as the key in the dictionary
                Control control = Controls.OfType<Control>()
                    .Where(x => x.GetType() != typeof(Button) && x.Tag != null)
                    .FirstOrDefault(x => x.Tag.ToString().Equals(item.Key));
                
                if (control == null) continue;

                // If the control is a checkbox, we have to parse the value to a boolean, otherwise we
                // just set the text of the control to the value.
                if (control.GetType() == typeof(CheckBox))
                    ((CheckBox)control).Checked = bool.Parse(item.Value);
                
                else control.Text = item.Value;
            }
        }

        /// <summary>
        /// Creates a new process, opening the server's folder.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ButtonOpenServerFolder_Click(object sender, EventArgs e)
        {
            Process.Start(ServerSection.SectionFullPath);
        }

        /// <summary>
        /// Saves the current form's properties into the server.properties file and closes the form.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Gets the necessary resources to edit save the server's properties and settings
            string newServerSectionPath = Path.GetDirectoryName(ServerSection.SectionFullPath) + "/" + TextBoxServerName.Text;

            try
            {
                // Updates and flushes the buffers
                Editor.UpdateBuffers(FormToDictionary());
                Editor.FlushBuffers();
            }
            catch (SystemException)
            {
                MessageBox.Show(@"One or more fields are incorrectly filled. Please correct them and try again.", @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            // Renames the server's folder to the new name if it changed.
            if (!ServerSection.SectionFullPath.EqualsPath(newServerSectionPath))
            {
                ServerList.INSTANCE.RemoveFromList(ServerSection.Name);
                Directory.Move(ServerSection.SectionFullPath, newServerSectionPath);

                // Updates the ServerSection property to the new path.
                ServerSection = FileSystem.AddSection("servers/" + TextBoxServerName.Text);
                ServerList.INSTANCE.AddServerToList(Editor);
            }

            Close();
        }

        /// <summary>
        /// Turns the spawn protection numeric box on and off depending on the state of the checkbox.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void CheckBoxSpawnProtection_CheckedChanged(object sender, EventArgs e)
        {
            NumericSpawnProtection.Visible = CheckBoxSpawnProtection.Checked;
        }

        /// <summary>
        /// Allows the user to delete the server from the list, and deletes the directory from disk.
        /// This is done after receiving confirmation from the user.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ButtonDeleteServer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Are you sure you want to delete this server?", @"Delete server",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;

            try
            {
                // Removes the server from the list, deletes the directory and closes the form.
                Directory.Delete(ServerSection.SectionFullPath, true);
                ServerList.INSTANCE.RemoveFromList(ServerSection.SimpleName);
                Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"An error occurred while deleting the server: " + exception.Message, @"Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Opens a folder browser dialog bound to a given TextBox in its Tag, and allows the user to select
        /// a folder to use as the server's backup path.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ButtonFolderBrowsing_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            TextBox boundTextBox =
                Controls.OfType<TextBox>().First(x => x.Tag.ToString() == button.Tag.ToString());

            DialogResult result = FolderBrowser.ShowDialog();
            if (result == DialogResult.OK) boundTextBox.Text = FolderBrowser.SelectedPath;
        }

        /// <summary>
        /// Refreshes the grid after every edit to any server's properties and settings.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ServerEditPrompt_FormClosed(object sender, FormClosedEventArgs e)
        {
            ServerList.INSTANCE.SortGrid();
        }
    }
}