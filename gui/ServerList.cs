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
            Task.Run(RefreshGridAsync);
        }

        /// <summary>
        /// Refreshes the grid asynchronously, clearing everything and reading all of the existing
        /// servers, re-adding them into the grid.
        /// </summary>
        public async Task RefreshGridAsync()
        {
            // Iterates over every server in the servers section and creates an addition task for them
            GridServerList.Rows.Clear();
            
            // Creates a list of tasks invoking AddServerToList in the original thread, sorted.
            List<Section> sections = FileSystem.AddSection("servers").GetAllTopLevelSections().ToList();
            List<Task> taskList = sections.Select(AddServerToListAsync).ToList();

            await Task.WhenAll(taskList);
            
            // Sort the servers by version
            GridServerList.Sort(Comparer<DataGridViewRow>.Create((a, b) =>
                new Version(b.Cells[1].Value.ToString() is var cell && cell != "Unknown" ? cell : "0.0.0")
                    .CompareTo(new Version(a.Cells[1].Value.ToString() is var cell2 && cell2 != "Unknown" ? cell2 : "0.0.0"))));
        }

        /// <summary>
        /// Returns the layout of the current form, so that it can be added to another form.
        /// </summary>
        /// <returns>A Panel representing this form's layout.</returns>
        public Panel GetLayout() => this.ServerListLayout;
        
        /// <summary>
        /// Checks if a given server name already exists inside the Grid
        /// </summary>
        /// <param name="serverName">The server name to check for</param>
        /// <returns>Whether or not the server name exists in the grid</returns>
        public bool ExistsInGrid(string serverName)
        {
            foreach (DataGridViewRow row in GridServerList.Rows)
                if (row.Cells[2].Value.ToString().Equals(serverName)) return true;

            return false;
        }

        /// <summary>
        /// Adds a server to the server list given the server section.
        /// </summary>
        /// <param name="section">The section to add into the server list</param>
        public void AddServerToList(Section section)
        {
            // Prevents server duplicates from being displayed
            if (this.ExistsInGrid(section.SimpleName)) return;
            
            // First checks if the server settings file exists, and if it doesn't, adds the server to the
            // list as "Unknown", creating the settings file.
            string settingsPath = Path.Combine(section.SectionFullPath, "server_settings.xml");
            if (!File.Exists(settingsPath))
            {
                XMLUtils.SerializeToFile<ServerInformation>(settingsPath, new ServerInformation
                {
                    Type = "unknown",
                    Version = "Unknown",
                    ServerBackupsPath = section.AddSection("backups/server").SectionFullPath,
                    PlayerdataBackupsPath = section.AddSection("backups/playerdata").SectionFullPath,
                    Port = 25565,
                    Ram = 1024,
                    JavaRuntimePath = "java"
                });

                Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate()
                {
                    GridServerList.Rows.Add(
                        Image.FromFile(FileSystem.GetFirstSectionNamed("assets").GetFirstDocumentNamed("unknown.png")),
                        "Unknown",
                        Path.GetFileName(section.Name), "Offline");
                }));
                return;
            } 
            
            // Deserializes the server settings file to access the server information.
            ServerInformation info = XMLUtils.DeserializeFromFile<ServerInformation>(settingsPath);
            
            // Gets the image path for the server type, and adds the server to the list.
            // We have to parse the type to get the first word, since there could be snapshots of the type,
            // making the type similar to "serverType snapshots".
            string typeImagePath = FileSystem.GetFirstSectionNamed("assets")
                .GetFirstDocumentNamed(info.Type.Split(' ')[0].ToLower() + ".png");

            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate()
            {
                GridServerList.Rows.Add(Image.FromFile(typeImagePath), info.Version, Path.GetFileName(section.Name),
                    "Offline");
            }));
        }

        /// <summary>
        /// Performs an addition to the server list asynchronously.
        /// </summary>
        /// <param name="section">The section to add into the server list</param>
        public async Task AddServerToListAsync(Section section) => await Task.Run(() => AddServerToList(section)); 

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
        /// Removes a server from the server list asynchronously.
        /// </summary>
        /// <param name="serverName">The name of the server to remove from the list</param>
        public async Task RemoveFromListAsync(string serverName) => await Task.Run(() => RemoveFromList(serverName));

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
