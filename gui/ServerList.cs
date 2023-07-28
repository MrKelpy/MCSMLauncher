using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common;
using MCSMLauncher.common.caches;
using MCSMLauncher.common.factories;
using MCSMLauncher.common.models;
using MCSMLauncher.common.server.starters.abstraction;
using MCSMLauncher.utils;
using PgpsUtilsAEFC.common;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.gui
{
    /// <summary>
    /// This form handles all operations related to launching and playing on created servers.
    /// </summary>
    public partial class ServerList : Form
    {

        /// <summary>
        /// Main constructor for the ServerList form. Private to enforce the singleton model.
        /// </summary>
        private ServerList()
        {
            InitializeComponent();

            // Sets the info layout pictures
            foreach (Label label in ServerListLayout.Controls.OfType<Label>().Where(x => x.Tag != null && x.Tag.ToString().Equals("tooltip")).ToList())
            {
                label.BackgroundImage = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(ConfigurationManager.AppSettings.Get("Asset.Icon.Tooltip"))));
                label.BackgroundImageLayout = ImageLayout.Zoom;
            }

            ButtonRefresh.BackgroundImage = Image.FromFile(FileSystem.GetFirstDocumentNamed(Path.GetFileName(ConfigurationManager.AppSettings.Get("Asset.Icon.Refresh"))));
        }
        
        /// <summary>
        /// The instance of the class to use, matching the singleton model.
        /// </summary>
        // ReSharper disable once InconsistentNaming
        public static ServerList INSTANCE { get; } = new ();
        
        /// <summary>
        /// Refreshes the grid asynchronously, clearing everything and reading all of the existing
        /// servers, re-adding them into the grid.
        /// </summary>
        public async Task RefreshGridAsync()
        {
            // Iterates over every server in the servers section and creates an addition task for them
            GridServerList.Rows.Clear();
            
            // Gets the global editors cache
            GlobalEditorsCache cache = GlobalEditorsCache.INSTANCE;

            // Creates a list of sorted editors to work with, to then create a task list
            List<ServerEditor> editors = FileSystem.AddSection("servers").GetAllTopLevelSections().Select(cache.GetOrCreate).ToList();
            List<Task> taskList = editors.Select(AddServerToListAsync).ToList();

            // Waits for all of the tasks to complete
            await Task.WhenAll(taskList);
            Logging.LOGGER.Info("Refreshed the server list.");

            // Sort the servers by version
            SortGrid();
        }

        /// <summary>
        /// Returns the layout of the current form, so that it can be added to another form.
        /// </summary>
        /// <returns>A Panel representing this form's layout.</returns>
        public Panel GetLayout()
        {
            return ServerListLayout;
        }

        /// <summary>
        /// Checks if a given server name already exists inside the Grid, and returns
        /// it if it does.
        /// </summary>
        /// <param name="serverName">The server name to check for</param>
        /// <returns>The row that contains the server name</returns>
        private DataGridViewRow GetRowFromName(string serverName)
        {
            foreach (DataGridViewRow row in GridServerList.Rows)
                if (row.Cells[2].Value.Equals(serverName))
                    return row;

            return null;
        }

        /// <summary>
        /// Sorts the grid by version, from highest to lowest.
        /// </summary>
        public void SortGrid()
        {
            GridServerList.Sort(GridServerList.Columns[2], ListSortDirection.Ascending);

            GridServerList.Sort(Comparer<DataGridViewRow>.Create((a, b) =>
                new MinecraftVersion(b.Cells[1].Value.ToString()).CompareTo(
                    new MinecraftVersion(a.Cells[1].Value.ToString()))));
        }

        /// <summary>
        /// Adds a server to the server list given the server section.
        /// </summary>
        /// <param name="editor">The editor to work with</param>
        public void AddServerToList(ServerEditor editor)
        {
            // Prevents server duplicates from being displayed
            if (GetRowFromName(editor.ServerSection.SimpleName) != null) return;

            // Deserializes the server settings file to access the server information.
            ServerInformation info = editor.GetServerInformation();

            // Gets the image path for the server type, and adds the server to the list.
            // We have to parse the type to get the first word, since there could be snapshots of the type,
            // making the type similar to "serverType snapshots".
            string typeImagePath = FileSystem.GetFirstSectionNamed("assets").GetFirstDocumentNamed(info.Type.Split(' ')[0].ToLower() + ".png");
            
            // Invokes the addition of the server to the list in the main thread.
            Mainframe.INSTANCE.Invoke(new MethodInvoker(() =>
            {
                Image typeImage = Image.FromFile(typeImagePath);
                GridServerList.Rows.Add(typeImage, info.Version, editor.ServerSection.SimpleName); 
            }));

            // Sets the server button's text to "Start" by default.
            GetRowFromName(editor.ServerSection.SimpleName).Cells[5].Value = "Start";
        }

        /// <summary>
        /// Performs an addition to the server list asynchronously.
        /// </summary>
        /// <param name="editor">The editor to work with</param>
        public async Task AddServerToListAsync(ServerEditor editor)
        {
            await Task.Run(() => AddServerToList(editor));
        }

        /// <summary>
        /// Removes a server from the server list given the server name.
        /// </summary>
        /// <param name="serverName">The name of the server to remove from the list</param>
        public void RemoveFromList(string serverName)
        {
            // Iterates over every row in the server list, and removes the row if the server name matches.
            foreach (DataGridViewRow row in GridServerList.Rows)
                
                // Checks the third column (the server name column) to see if the server name matches.
                if (row.Cells[2].Value.Equals(Path.GetFileName(serverName)))
                {
                    GridServerList.Rows.Remove(row);
                    break;
                }
        }

        /// <summary>
        /// Removes a server from the server list asynchronously.
        /// </summary>
        /// <param name="serverName">The name of the server to remove from the list</param>
        public async Task RemoveFromListAsync(string serverName)
        {
            await Task.Run(() => RemoveFromList(serverName));
        }

        /// <summary>
        /// Updates a given server's play button state to either "Start" or "Running" depending on whether
        /// the server is running or not.
        /// </summary>
        /// <param name="editor">The server editor to work with</param>
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        private void UpdateServerButtonState(ServerEditor editor)
        {
            // Gets the necessary information to update the server button state.
            ServerInformation info = editor.GetServerInformation();
            string procName = ProcessUtils.GetProcessById(info.CurrentServerProcessID)?.ProcessName;
            string serverName = editor.ServerSection.SimpleName;

            // Makes sure that the row exists before trying to update it.
            DataGridViewRow row = GetRowFromName(serverName);
            if (row == null) return;
            
            // Handles the server if it is running; In which case there will be a process
            // with a set PID, specified in the server settings file, running as an mc server.
            if (Math.Pow(info.CurrentServerProcessID, 2) != 1 && (procName == "java" || procName == "cmd"))
            {
                ForceUpdateServerState(serverName, "Running");
                UpdateServerIP(editor);
                return; 
            }

            // Changes the button state to "Start" if the server is not running, and resets the process ID.
            row.Cells[5].Value = "Start";
            row.Cells[3].Value = "";
            info.CurrentServerProcessID = -1;
            
            // Updates and flushes the buffers with the new information.
            editor.UpdateBuffers(info.ToDictionary());
            editor.FlushBuffers();
        }

        /// <summary>
        /// Forces an update to the server's state, regardless of whether it's running or not.
        /// </summary>
        /// <param name="serverName">The server to update the state for</param>
        /// <param name="state">The new state</param>
        public void ForceUpdateServerState(string serverName, string state)
        {
            DataGridViewRow row = GetRowFromName(serverName);
            if (row == null) return;
            row.Cells[5].Value = state;
        }

        /// <summary>
        /// Performs an update to the server's play button state asynchronously.
        /// </summary>
        /// <param name="editor">The editor to work with</param>
        public async Task UpdateServerButtonStateAsync(ServerEditor editor)
        {
            await Task.Run(() => UpdateServerButtonState(editor));
        }


        /// <summary>
        /// Iterates through all of the servers listed in the Grid, and tries to update every row's
        /// running state, as long as they're not in the "start" state.
        /// </summary>
        public async Task UpdateAllButtonStatesAsync()
        {
            List<Task> tasks = new ();

            // Iterates through all the listed servers and adds a task to update their state if they're running
            foreach (DataGridViewRow row in GridServerList.Rows)
            {
                Section serverSection = FileSystem.GetFirstSectionNamed("servers/" + row.Cells[2].Value);
                ServerEditor editor = GlobalEditorsCache.INSTANCE.GetOrCreate(serverSection);
                tasks.Add(UpdateServerButtonStateAsync(editor));
            }

            await Task.WhenAll(tasks);
        }

        /// <summary>
        /// Updates the server's IP address in the server list.
        /// </summary>
        /// <param name="editor">The ServerEditor instance to use</param>
        public void UpdateServerIP(ServerEditor editor)
        {
            // Gets the server's IP address and updates the server list.
            DataGridViewRow row = GetRowFromName(editor.ServerSection.SimpleName);
            ServerInformation settings = editor.GetServerInformation();

            if (row == null || row.Cells[3].Value?.ToString() == "Copied to Clipboard") return;
            
            // Updates the server's IP address in the server list.
            row.Cells[3].Value = editor.GetFromBuffers("server-ip") != ""
                ? editor.GetFromBuffers("server-ip")
                : settings.IPAddress + ":" + editor.GetFromBuffers("server-port");
        }

        /// <summary>
        /// De-selects the selected row in the server list, so that the selections won't pollute the screen.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void GridServerList_SelectionChanged(object sender, EventArgs e)
        {
            GridServerList.ClearSelection();
        }

        /// <summary>
        /// Opens the server edit prompt when the user clicks on any "Options" button.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private async void GridServerList_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Initialises the selected row for clarity.
            DataGridViewRow selectedRow = GridServerList.Rows[e.RowIndex];
            
            switch (e.ColumnIndex)
            {
                
                // In case the user clicks on... The Server IP cell.
                case 3 when e.RowIndex >= 0:
                {
                    await IPAddressCellClick(selectedRow);
                    break;
                }

                // In case the user clicks on... Any Options button.
                case 4 when e.RowIndex >= 0:
                {
                    OptionsButtonClick(selectedRow);
                    break;
                }

                // In case the user clicks on... Any "Start" button.
                case 5 when e.RowIndex >= 0 && selectedRow.Cells[5].Value.ToString() == "Start":
                {
                    await StartButtonClick(selectedRow);
                    break;
                }
            }
        }
        
        /// <summary>
        /// When the user clicks on the IP address cell, copy the IP address to the clipboard.
        /// </summary>
        /// <param name="ipRow">The IP Address row in the grid</param>
        private static async Task IPAddressCellClick(DataGridViewRow ipRow)
        {
            // Prevent the user from spamming the "Copy to Clipboard" button.
            string value = ipRow.Cells[3].Value.ToString();
            if (value == "Copied to Clipboard") return;
            Clipboard.SetText(value);

            // Adds a "Copied to Clipboard" message to the IP address cell, and removes it after 2 seconds.
            ipRow.Cells[3].Value = "Copied to Clipboard";
            await Task.Delay(500);
            ipRow.Cells[3].Value = value;
        }

        /// <summary>
        /// In case the user clicks on the "Options" button, open the server edit prompt.
        /// </summary>
        /// <param name="buttonRow">The row where the button was clicked</param>
        private static void OptionsButtonClick(DataGridViewRow buttonRow)
        {
            // Get the server's section from its name
            string serverName = buttonRow.Cells[2].Value.ToString();
            Section serverSection = FileSystem.AddSection($"servers/{serverName}");
            
            // Create and show the edit prompt.
            ServerEditor editor = GlobalEditorsCache.INSTANCE.GetOrCreate(serverSection);
            ServerEditPrompt editPrompt = new (editor);
            editPrompt.ShowDialog();
        }

        /// <summary>
        /// When the user clicks on the "Start" button, run the appropriate server starter.
        /// </summary>
        /// <param name="buttonRow">The row in which the button was clicked on</param>
        private static async Task StartButtonClick(DataGridViewRow buttonRow)
        {
            try
            {
                // Get the server's section from its name
                string serverName = buttonRow.Cells[2].Value.ToString();
                Section serverSection = FileSystem.AddSection($"servers/{serverName}");
                
                // Get the server's type and starter
                ServerEditor editor = GlobalEditorsCache.INSTANCE.GetOrCreate(serverSection);
                string serverType = editor.GetServerInformation().Type;
                AbstractServerStarter serverStarter = new ServerTypeMappingsFactory().GetStarterFor(serverType);

                // Start the server
                await serverStarter.Run(serverSection, editor);
            }
            
            // If an error occurs, let the user know.
            catch (Exception ex)
            {
                Logging.LOGGER.Error(ex);
                MessageBox.Show($@"An error occurred whilst starting the server. {Environment.NewLine}Please check the integrity of the server files.",
                    @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Refreshes the server list asynchronously.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private async void ButtonRefresh_Click(object sender, EventArgs e)
        {
            GlobalEditorsCache.INSTANCE.Clear();
            await RefreshGridAsync();
            await UpdateAllButtonStatesAsync();
        }
    }
}