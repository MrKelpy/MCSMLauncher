using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MCSMLauncher.common;
using MCSMLauncher.common.models;
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
        /// The server directory to edit.
        /// </summary>
        private Section ServerSection { get; set; }
        
        /// <summary>
        /// Main constructor for the ServerEditPrompt form. Initialises the form and loads the
        /// information from the server.properties file into the form.
        /// </summary>
        /// <param name="serverSection"></param>
        public ServerEditPrompt(Section serverSection)
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterParent;
            this.ServerSection = serverSection;
            this.LoadToForm(this.PropertiesToDictionary());
            
            ButtonFolderBrowsing.Image = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(ConfigurationManager.AppSettings.Get("FolderBrowser.Icon"))));
            ButtonFolderBrowsing2.Image = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(ConfigurationManager.AppSettings.Get("FolderBrowser.Icon"))));
            ButtonFolderBrowsing3.Image = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(ConfigurationManager.AppSettings.Get("FolderBrowser.Icon"))));
        }
        
        /// <summary>
        /// Switches the focus to the first label in the form when the form is loaded, so that nothing
        /// is selected.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ServerEditPrompt_Load(object sender, EventArgs e) => this.ActiveControl = label1;

        /// <summary>
        /// Collects all the information from the form and returns it as a dictionary.
        /// </summary>
        /// <returns>A dictionary containing all of the settings in the form as a dictionary</returns>
        public Dictionary<string, string> FormToDictionary()
        {
            Dictionary<string, string> formInformation = new Dictionary<string, string>();
            
            // Iterates through all the controls in the form and adds the tag and text of each control, excluding
            // the ones without tags
            Controls.OfType<Control>().Where(x => x.GetType() != typeof(CheckBox) && x.GetType() != typeof(Button) && x.Tag != null && x.Tag.ToString() != string.Empty).ToList().ForEach(x => formInformation.Add(x.Tag.ToString(), x.Text));
            Controls.OfType<CheckBox>().Where(x => x.Tag != null && x.Tag.ToString() != string.Empty).ToList().ForEach(x => formInformation.Add(x.Tag.ToString(), x.Checked.ToString().ToLower()));
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
        public void LoadToForm(Dictionary<string, string> dictionaryToLoad)
        {
            foreach (KeyValuePair<string,string> item in dictionaryToLoad)
            {
                // Finds the control with the same tag as the key in the dictionary
                Control control = Controls.OfType<Control>().Where(x => x.GetType() != typeof(Button) && x.Tag != null).FirstOrDefault(x => x.Tag.ToString().Equals(item.Key));
                if (control == null) continue;
                
                // If the control is a checkbox, we have to parse the value to a boolean, otherwise we
                // just set the text of the control to the value.
                if (control.GetType() == typeof(CheckBox))
                    ((CheckBox) control).Checked = bool.Parse(item.Value);
                else
                    control.Text = item.Value;
            }

            CheckBoxCracked.Checked = !CheckBoxCracked.Checked;
            CheckBoxSpawnProtection.Checked = dictionaryToLoad.ContainsKey("spawn-protection") && int.Parse(dictionaryToLoad["spawn-protection"]) > 0;
            TextBoxServerName.Text = ServerSection.SimpleName;
        }

        /// <summary>
        /// Reads the properties file and returns a dictionary with the key and value of each line.
        /// </summary>
        /// <returns>A dictionary containing the key:val's of the properties file</returns>
        public Dictionary<string, string> PropertiesToDictionary()
        {
            Dictionary<string, string> propertiesDictionary = new Dictionary<string, string>();
            string propertiesPath = ServerSection.GetFirstDocumentNamed("server.properties");
            string settingsPath = ServerSection.GetFirstDocumentNamed("server_settings.xml");
            
            if (propertiesPath == null) return propertiesDictionary;
            
            // Reads the file line by line, and adds the key and value to the dictionary.
            foreach (string line in FileUtils.ReadFromFile(propertiesPath))
            {
                if (line.StartsWith("#") || line.StartsWith("server-port")) continue;
                string[] splitLine = line.Split('=');
                propertiesDictionary.Add(splitLine[0], splitLine[1]);
            }
            
            // If the server_settings.xml file exists, deserialize it and add the values to the dictionary. 
            ServerInformation info = XMLUtils.DeserializeFromFile<ServerInformation>(settingsPath);
            
            info.GetType().GetProperties().Where(x => x.Name.ToLower() != "port")
                .ToList().ForEach(x => propertiesDictionary[x.Name.ToLower()] =  x.GetValue(info)?.ToString() ?? "");
            
            propertiesDictionary.Add("server-port", info.Port.ToString());

            return propertiesDictionary;
        }

        /// <summary>
        /// Loads the current form's properties into the server.properties file.
        /// </summary>
        /// <param name="dictionaryToLoad">The dictionary to load into the form</param>
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void LoadToProperties(Dictionary<string, string> dictionaryToLoad)
        {
            string propertiesFilepath = ServerSection.GetFirstDocumentNamed("server.properties");
            string settingsFilepath = ServerSection.GetFirstDocumentNamed("server_settings.xml");
            
            if (propertiesFilepath == null) return;
            List<string> propertiesFile = FileUtils.ReadFromFile(propertiesFilepath);
            
            // Loads the information from the form into the ServerInformation object and serializes it again
            ServerInformation updatedServerInformation = XMLUtils.DeserializeFromFile<ServerInformation>(settingsFilepath);
            updatedServerInformation.Port = int.Parse(dictionaryToLoad["server-port"]);
            updatedServerInformation.Ram = int.Parse(dictionaryToLoad["ram"]);
            updatedServerInformation.PlayerdataBackupsPath = dictionaryToLoad["playerdatabackupspath"];
            updatedServerInformation.ServerBackupsPath = dictionaryToLoad["serverbackupspath"];
            
            // Iterates through the dictionary and replaces the line in the file with the same key
            for (var i = 0; i < dictionaryToLoad.Count; i++)
            {
                string key = dictionaryToLoad.Keys.ToArray()[i];
                int keyIndex = propertiesFile.FindIndex(x => x.ToLower().Contains(key));
                
                // If the current key is a settings file key, skip it.
                if (updatedServerInformation.GetType().GetProperties().Any(x => String.Equals(key.ToLower(), x.Name.ToLower(), StringComparison.InvariantCulture))) 
                    continue;
                
                if (keyIndex != -1) propertiesFile[keyIndex] = $"{key}={dictionaryToLoad[key]}";
                else propertiesFile.Add($"{key}={dictionaryToLoad[key]}");
            }

            File.Delete(settingsFilepath);
            
            // Writes the new edited file contents to disk.
            XMLUtils.SerializeToFile<ServerInformation>(settingsFilepath, updatedServerInformation);
            FileUtils.DumpToFile(propertiesFilepath, propertiesFile);
        }

        /// <summary>
        /// Creates a new process, opening the server's folder.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ButtonOpenServerFolder_Click(object sender, EventArgs e) => Process.Start(ServerSection.SectionFullPath);

        /// <summary>
        /// Saves the current form's properties into the server.properties file and closes the form.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ButtonSave_Click(object sender, EventArgs e)
        {
            // Renames the server's folder to the new name if it changed.
            string newServerSectionPath = Path.GetDirectoryName(ServerSection.SectionFullPath) + "/" + TextBoxServerName.Text;

            if (!ServerSection.SectionFullPath.EqualsPath(newServerSectionPath))
            {
                ServerList.INSTANCE.RemoveFromList(ServerSection.Name);
                Directory.Move(ServerSection.SectionFullPath, newServerSectionPath);

                // Updates the ServerSection property to the new path.
                ServerSection = Constants.FileSystem.AddSection("servers/" + TextBoxServerName.Text);
                ServerList.INSTANCE.AddServerToList(ServerSection);
            }

            // Loads the properties into the server.properties and settings files and closes the form.
            this.LoadToProperties(this.FormToDictionary());
            this.Close();
        }

        /// <summary>
        /// Turns the spawn protection numeric box on and off depending on the state of the checkbox.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void CheckBoxSpawnProtection_CheckedChanged(object sender, EventArgs e) =>
            NumericSpawnProtection.Visible = CheckBoxSpawnProtection.Checked;

        /// <summary>
        /// Allows the user to delete the server from the list, and deletes the directory from disk.
        /// This is done after receiving confirmation from the user.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ButtonDeleteServer_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"Are you sure you want to delete this server?", @"Delete server", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                return;
            
            // Removes the server from the list, deletes the directory and closes the form.
            try
            {
                Directory.Delete(ServerSection.SectionFullPath, true);
                ServerList.INSTANCE.RemoveFromList(ServerSection.SimpleName);
                this.Close();
            }
            catch (Exception exception)
            { MessageBox.Show(@"An error occurred while deleting the server: " + exception.Message, @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error); }
        }

        /// <summary>
        /// Opens a folder browser dialog bound to a given TextBox in its Tag, and allows the user to select
        /// a folder to use as the server's backup path.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ButtonFolderBrowsing_Click(object sender, EventArgs e)
        {
            Button button = (Button) sender;
            TextBox boundTextBox = Controls.OfType<TextBox>().First(x => x.Tag.ToString() == button.Tag.ToString());
            
            DialogResult result = FolderBrowser.ShowDialog();
            if (result == DialogResult.OK) boundTextBox.Text = FolderBrowser.SelectedPath;
        }
    }
}