using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;

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
            this.ServerSection = serverSection;
            this.LoadToForm(this.PropertiesToDictionary());
        }

        /// <summary>
        /// Collects all the information from the form and returns it as a dictionary.
        /// </summary>
        /// <returns>A dictionary containing all of the settings in the form as a dictionary</returns>
        private Dictionary<string, string> FormToDictionary()
        {
            Dictionary<string, string> formInformation = new Dictionary<string, string>();
            
            Controls.OfType<Control>().Where(x => x.GetType() != typeof(CheckBox)).ToList().ForEach(x => formInformation.Add(x.Tag.ToString(), x.Text));
            Controls.OfType<CheckBox>().Where(x => x.Tag != null).ToList().ForEach(x => formInformation.Add(x.Tag.ToString(), x.Checked.ToString().ToLower()));

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
            foreach (KeyValuePair<string,string> item in dictionaryToLoad)
            {
                // Finds the control with the same tag as the key in the dictionary
                Control control = Controls.OfType<Control>().First(x => x.Tag.ToString().Equals(item.Key));
                
                // If the control is a checkbox, we have to parse the value to a boolean, otherwise we
                // just set the text of the control to the value.
                if (control.GetType() == typeof(CheckBox))
                    ((CheckBox) control).Checked = bool.Parse(item.Value);
                else
                    control.Text = item.Value;
            }
        }

        /// <summary>
        /// Reads the properties file and returns a dictionary with the key and value of each line.
        /// </summary>
        /// <returns>A dictionary containing the key:val's of the properties file</returns>
        private Dictionary<string, string> PropertiesToDictionary()
        {
            Dictionary<string, string> propertiesDictionary = new Dictionary<string, string>();
            string filepath = ServerSection.GetFirstFileNamed("server.properties");
            if (filepath == null) return propertiesDictionary;
            
            // Reads the file line by line, and adds the key and value to the dictionary.
            foreach (string line in FileUtils.ReadFromFile(filepath))
            {
                string[] splitLine = line.Split('=');
                propertiesDictionary.Add(splitLine[0], splitLine[1]);
            }

            return propertiesDictionary;
        }

        /// <summary>
        /// Loads the current form's properties into the server.properties file.
        /// </summary>
        /// <param name="dictionaryToLoad">The dictionary to load into the form</param>
        private void LoadToProperties(Dictionary<string, string> dictionaryToLoad)
        {
            string filepath = ServerSection.GetFirstFileNamed("server.properties");
            if (filepath == null) return;
            List<string> propertiesFile = FileUtils.ReadFromFile(filepath);
            
            // Iterates through the dictionary and replaces the line in the file with the same key
            foreach (KeyValuePair<string,string> property in dictionaryToLoad)
            {
                int lineIndex = propertiesFile.FindIndex(x => x.StartsWith(property.Key));
                propertiesFile[lineIndex] = $"{property.Key}={property.Value}";
            }
            
            // Writes the new edited file contents to disk.
            FileUtils.DumpToFile(ServerSection.GetFirstFileNamed("server.properties"), propertiesFile);
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
            ServerList.INSTANCE.RemoveFromList(ServerSection.Name);
            Directory.Delete(ServerSection.SectionFullPath, true);
            this.Close();
        }
    }
}