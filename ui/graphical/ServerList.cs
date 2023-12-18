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
using LaminariaCore_General.utils;
using LaminariaCore_Winforms.common;
using mcsm.api.server;
using mcsm.common;
using mcsm.common.caches;
using mcsm.common.handlers;
using mcsm.common.models;
using mcsm.utils;
using static mcsm.common.Constants;

namespace mcsm.ui.graphical
{
    /// <summary>
    /// This form handles all operations related to launching and playing on created servers.
    /// </summary>
    public partial class ServerList : Form
    {
        
        /// <summary>
        /// The servers section, containing all of the servers.
        /// </summary>
        private Section ServersSection { get; }

        /// <summary>
        /// Main constructor for the ServerList form. Private to enforce the singleton model.
        /// </summary>
        private ServerList()
        {
            InitializeComponent();
            this.ServersSection = FileSystem.GetFirstSectionNamed("servers");

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
            
            // Creates a list of tasks to run, adding each server to the grid
            List<Task> taskList = ServersSection.GetAllTopLevelSections().Select(AddServerToListAsync).ToList();

            // Waits for all of the tasks to complete
            await Task.WhenAll(taskList);
            Logging.Logger.Info("Refreshed the server list.");

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
        /// <param name="serverSection">The server's section in the file system</param>
        public void AddServerToList(Section serverSection)
        {
            ServerEditing editingApi = new ServerAPI().Editor(serverSection.SimpleName);
            
            // Prevents server duplicates from being displayed
            if (GetRowFromName(editingApi.GetServerName()) != null) return;

            // Deserializes the server settings file to access the server information.
            ServerInformation info = editingApi.GetServerInformation();

            // Gets the image path for the server type, and adds the server to the list.
            // We have to parse the type to get the first word, since there could be snapshots of the type,
            // making the type similar to "serverType snapshots".
            string typeImagePath = FileSystem.GetFirstSectionNamed("assets").GetFirstDocumentNamed(info.Type.Split(' ')[0].ToLower() + ".png");
            
            // Invokes the addition of the server to the list in the main thread.
            Mainframe.INSTANCE.Invoke(new MethodInvoker(() =>
            {
                Image typeImage = Image.FromFile(typeImagePath);
                GridServerList.Rows.Add(typeImage, info.Version, editingApi.GetServerName()); 
            }));

            // Sets the server start button text to "Start" by default
            GetRowFromName(editingApi.GetServerName()).Cells[5].Value = "Start";
            
            // Sets the server stop button text to "Stop" by default
            GetRowFromName(editingApi.GetServerName()).Cells[6].Value = "Stop";
        }

        /// <summary>
        /// Performs an addition to the server list asynchronously.
        /// </summary>
        /// <param name="serverSection">The server's section in the filesystem</param>
        public async Task AddServerToListAsync(Section serverSection)
        {
            await Task.Run(() => AddServerToList(serverSection));
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
        /// <param name="serverName">The server name to use towards the API</param>
        [SuppressMessage("ReSharper", "CompareOfFloatsByEqualityOperator")]
        private void UpdateServerButtonState(string serverName)
        {
            // Gets the necessary information to update the server button state.
            ServerEditing editingApi = new ServerAPI().Editor(serverName);
            ServerInformation info = editingApi.GetServerInformation();
            string procName = ProcessUtils.GetProcessById(info.CurrentServerProcessID)?.ProcessName;

            // Makes sure that the row exists before trying to update it.
            DataGridViewRow row = GetRowFromName(serverName);
            if (row == null) return;
            
            // Handles the server if it is running; In which case there will be a process
            // with a set PID, specified in the server settings file, running as an mc server.
            if (info.CurrentServerProcessID != -1 && procName is "java" or "cmd")
            {
                if (row.Cells[5].Value is not "Stopping") ForceUpdateServerState(serverName, "Running");
                UpdateServerIP(editingApi.Raw());
                return; 
            }
            
            // To save resources, only update the server button state if it isn't running.
            if (info.CurrentServerProcessID == -1) return;

            row.Cells[5].Value = "Start";
            row.Cells[6].Value = "Stop";
            row.Cells[6].Tag = null;
            row.Cells[3].Value = "";
            info.CurrentServerProcessID = -1;
            
            // Updates and flushes the buffers with the new information.
            editingApi.UpdateServerSettings(info.ToDictionary());
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
        /// <param name="serverName">The server name to work with</param>
        public async Task UpdateServerButtonStateAsync(string serverName)
        {
            await Task.Run(() => UpdateServerButtonState(serverName));
        }


        /// <summary>
        /// Iterates through all of the servers listed in the Grid, and tries to update every row's
        /// running state, as long as they're not in the "start" state.
        /// </summary>
        public async Task UpdateAllButtonStatesAsync()
        {
            foreach (DataGridViewRow row in GridServerList.Rows)
                await UpdateServerButtonStateAsync(row.Cells[2].Value.ToString());
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
            row.Cells[3].Value = editor.GetFromBuffers("server-ip") != null
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
            if (e.RowIndex == -1) return; // Ignore the header row.
            
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
                
                // In case the user clicks on... Any "Stop" or "Kill" button.
                case 6 when e.RowIndex >= 0 && selectedRow.Cells[6].Value.ToString() == "Stop" 
                            || selectedRow.Cells[6].Value.ToString() == "Kill":
                {
                    await StopButtonClick(selectedRow);
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
            ServerEditPrompt editPrompt = new (serverSection);
            editPrompt.ShowDialog();
        }
        
        /// <summary>
        /// When the user clicks on the "Start" button, run the appropriate server starter.
        /// </summary>
        /// <param name="buttonRow">The row in which the button was clicked on</param>
        private static Task StartButtonClick(DataGridViewRow buttonRow)
        {
            // Get the server's section from its name
            string serverName = buttonRow.Cells[2].Value.ToString();
            Section serverSection = FileSystem.AddSection($"servers/{serverName}");
            
            try
            {
                INSTANCE.ForceUpdateServerState(serverSection.SimpleName, "Starting");
                INSTANCE.GetRowFromName(serverSection.SimpleName).Cells[3].Value = "Resolving...";
                new ServerAPI().Starter(serverSection.SimpleName).Run(new MessageProcessingOutputHandler());
            }
            
            // If an error occurs, let the user know.
            catch (Exception ex)
            {
                Logging.Logger.Error(ex);
                MessageBox.Show($@"An error occurred whilst starting the server. {Environment.NewLine}Please check the integrity of the server files.",
                    @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                INSTANCE.ForceUpdateServerState(serverSection.SimpleName, "Start");
            }
            
            return Task.CompletedTask;
        }
        
        /// <summary>
        /// When the user clicks on the "Stop" button, it'll send a stop command to the server, and change
        /// the button to a "Kill" button, which will kill the server's process if clicked on again.
        /// </summary>
        /// <param name="buttonRow"></param>
        private async Task StopButtonClick(DataGridViewRow buttonRow)
        {
            // If the server is not running or being stopped, ignore the button click.
            string serverState = buttonRow.Cells[5].Value.ToString();
            if (serverState is not ("Running" or "Stopping")) return;
            
            // If the stop button is in wait mode, wait for the ServerProcessStateHandler to update.
            string stopMode = buttonRow.Cells[6].Tag?.ToString();
            if (stopMode is "Wait") return;
            
            // Get the necessary information from the row and the server's API.
            string serverName = buttonRow.Cells[2].Value.ToString();

            // If the stopping button is in kill mode, kill its process immediately.
            if (stopMode is "Kill")
            {
                buttonRow.Cells[6].Tag = "Wait";  // After killing, prevent button spamming.

                // Wait for the settings file to be released by the server process and kill it.
                // This prevents accidental data corruption.
                string settingsFile = ServersSection.GetFirstDocumentNamed("server_settings.xml");
                using var _ = await FileUtilExtensions.WaitForFileAsync(settingsFile);
                
                
                return;
            }

            try
            {
                // Gracefully stop the server and switch the button to kill mode.
                buttonRow.Cells[6].Value = buttonRow.Cells[6].Tag = "Kill";
                buttonRow.Cells[5].Value = "Stopping";
                new ServerAPI().Interactions(serverName).WriteToServerStdin("stop");
            }
            
            // If an error occurs, let the user know.
            catch (Exception ex)
            {
                Logging.Logger.Error(ex);
                MessageBox.Show($@"An error occurred whilst stopping the server.",
                    @"Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                
                buttonRow.Cells[6].Value = "Stop";
                buttonRow.Cells[6].Tag = null;
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