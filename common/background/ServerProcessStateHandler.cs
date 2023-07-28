#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using MCSMLauncher.common.caches;
using MCSMLauncher.common.interfaces;
using MCSMLauncher.gui;
using PgpsUtilsAEFC.common;

namespace MCSMLauncher.common.background
{
    /// <summary>
    /// This class implements a background process for checking the state of every server every second, and
    /// making sure that any servers that end up stopping are properly updated in the server list.
    /// </summary>
    public class ServerProcessStateHandler : IBackgroundRunner
    {

        /// <summary>
        /// Runs the background task.
        /// </summary>
        public async void RunTask()
        {
            GlobalEditorsCache editorsCache = GlobalEditorsCache.INSTANCE;
            
            while (true) // This specific background task should run forever.
            {
                Thread.Sleep(1 * 1000); // 1 second of cooldown between each check.

                foreach (DataGridViewRow row in ServerList.INSTANCE.GridServerList.Rows)
                {
                    // Targets only the servers that are running.
                    if (row.Cells[5].Value?.ToString() != "Running") continue;

                    // Gets the server name from the row.
                    string? serverName = row.Cells[2]?.Value.ToString();
                    if (serverName == null) continue;
                    
                    // Gets the server editor from the cache, or adds it to the cache if it's not there, and updates the button state.
                    Section serverSection = Constants.FileSystem.GetFirstSectionNamed("servers/" + serverName);
                    ServerEditor editor = editorsCache.GetOrCreate(serverSection);
                    await ServerList.INSTANCE.UpdateServerButtonStateAsync(editor);
                }
            }
        }
    }
}