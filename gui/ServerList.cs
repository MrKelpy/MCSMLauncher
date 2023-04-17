using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common.models;
using MCSMLauncher.requests.mcversions;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.forms.extensions;
using PgpsUtilsAEFC.utils;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.gui
{
    /// <summary>
    /// This form handles all operations related to launching and playing on created servers.
    /// </summary>
    public partial class ServerList : Form
    {

        /// <summary>
        /// The instance of the class to use, matching the singleton model.
        /// </summary>
        public static ServerList INSTANCE { get; } = new ServerList();
        
        /// <summary>
        /// Main constructor for the ServerList form. Private to enforce the singleton model.
        /// </summary>
        private ServerList()
        {
            InitializeComponent();

            // Iterates over every server in the servers section, reads their settings file, and adds them
            // to the list.
            foreach (Section serverSection in FileSystem.AddSection("servers").GetAllTopLevelSections())
                this.AddServerToList(serverSection);
        }

        /// <summary>
        /// Returns the layout of the current form, so that it can be added to another form.
        /// </summary>
        /// <returns>A Panel representing this form's layout.</returns>
        public Panel GetLayout() => this.ServerListLayout;

        /// <summary>
        /// Adds a server to the server list given the server section.
        /// </summary>
        public void AddServerToList(Section section)
        {
            // First checks if the server settings file exists, and if it doesn't, adds the server to the
            // list as "Unknown", creating the settings file.
            string settingsPath = Path.Combine(section.SectionFullPath, "server_settings.xml");
            if (!File.Exists(settingsPath))
            {
                XMLUtils.SerializeToFile<ServerInformation>(settingsPath, new ServerInformation
                {
                    Type = "unknown",
                    Version = "Unknown",
                    BackupsPath = section.AddSection("backups/server").SectionFullPath,
                    PlayerdataBackupsPath = section.AddSection("backups/playerdata").SectionFullPath,
                    Port = 25565,
                    Ram = 1024,
                    JavaRuntimePath = "java"
                });
                GridServerList.Rows.Add(Image.FromFile(FileSystem.GetFirstSectionNamed("assets").GetFirstFileNamed("unknown.png")), "Unknown", section.Name, "Offline");
                return;
            } 
            
            // Deserializes the server settings file to access the server information.
            ServerInformation info = XMLUtils.DeserializeFromFile<ServerInformation>(settingsPath);
            
            // Gets the image path for the server type, and adds the server to the list.
            // We have to parse the type to get the first word, since there could be snapshots of the type,
            // making the type similar to "serverType snapshots".
            string typeImagePath = FileSystem.GetFirstSectionNamed("assets")
                .GetFirstFileNamed(info.Type.Split(' ')[0].ToLower() + ".png");
            
            GridServerList.Rows.Add(Image.FromFile(typeImagePath), info.Version, section.Name, "Offline");
        }

        /// <summary>
        /// Removes a server from the server list given the server name.
        /// </summary>
        /// <param name="serverName">The name of the server to remove from the list</param>
        public void RemoveFromList(string serverName)
        {
            // Iterates over every row in the server list, and removes the row if the server name matches.
            foreach (DataGridViewRow row in GridServerList.Rows)
            {
                // Checks the third column (the server name column) to see if the server name matches.
                if (row.Cells[2].Value.Equals(Path.GetFileName(serverName)))
                {
                    GridServerList.Rows.Remove(row);
                    break;
                }
            }
        }

        /// <summary>
        /// De-selects the selected row in the server list, so that the selections won't pollute the screen.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void GridServerList_SelectionChanged(object sender, EventArgs e) => GridServerList.ClearSelection();

        /// <summary>
        /// Opens the server edit prompt when the user clicks on any "Options" button.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void GridServerList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

            // If the user clicks on any "Options" button, we open the server edit prompt.
            if (e.ColumnIndex == 4 && e.RowIndex >= 0)
            {
                string serverName = GridServerList.Rows[e.RowIndex].Cells[2].Value.ToString();
                Section serverSection = FileSystem.AddSection($"servers/{serverName}");
                ServerEditPrompt editPrompt = new ServerEditPrompt(serverSection);
                editPrompt.ShowDialog();
            }
        }
    }
}
