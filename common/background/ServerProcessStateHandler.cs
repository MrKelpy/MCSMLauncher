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
        /// A quick-access cache of the server editors, used to avoid having to re-fetch them every time.
        /// </summary>
        private QuickAccessEditorsCache QuickAccessCache { get; } = new ();
        
        /// <summary>
        /// A counter used to determine when to clean up the quick-access cache.
        /// </summary>
        private int CleanupCounter { get; set; } = 0;

        /// <summary>
        /// Runs the background task.
        /// </summary>
        public async void RunTask()
        {
            GlobalEditorsCache editorsCache = GlobalEditorsCache.INSTANCE;
            bool firstRun = true;
            
            while (true) // This specific background task should run forever.
            {
                Thread.Sleep(1 * 1000); // 1 second of cooldown between each check.

                foreach (DataGridViewRow row in ServerList.INSTANCE.GridServerList.Rows)
                {
                    // Gets the server name from the row.
                    string? serverName = row.Cells[2]?.Value.ToString();
                    if (serverName == null) continue;
                    
                    // Only update the server button state if the server is running.
                    if (row.Cells[5]?.Value.ToString() is not ("Running" or "Starting") && !firstRun) continue;
                    
                    // Firstly, tries to get the server editor from the quick-access cache.
                    ServerEditor quickEditor = QuickAccessCache.Get(serverName);
                    if (quickEditor != null)
                    {
                        // Updates the button state.
                        await ServerList.INSTANCE.UpdateServerButtonStateAsync(quickEditor);
                        continue;
                    }
                    
                    // If the server editor is not in the quick-access cache, get it from the global cache.
                    Section serverSection = Constants.FileSystem.GetFirstSectionNamed("servers/" + serverName);
                    ServerEditor editor = editorsCache.GetOrCreate(serverSection);
                    
                    // Adds the server editor to the quick-access cache for future use and updates the button state.
                    QuickAccessCache.Add(editor);
                    await ServerList.INSTANCE.UpdateServerButtonStateAsync(editor);
                }
                
                // Signals that the first run is over.
                firstRun = false;

                // Allows for the quick-access cache to be cleaned up every 10 seconds.
                if (CleanupCounter++ < 10) continue;
                CleanupQuickAccessCache();
                CleanupCounter = 0;
            }
        }

        /// <summary>
        /// Removes any servers that are not in the server list anymore from the quick-access cache.
        /// </summary>
        private void CleanupQuickAccessCache()
        {
            // Removes any servers that are not in the server list anymore.
            List<string?> serverNames = ServerList.INSTANCE.GridServerList.Rows.Cast<DataGridViewRow>()
                .Select(row => row.Cells[2]?.Value.ToString()).ToList();

            List<string> removedNames = QuickAccessCache.Cache.Keys.Where(serverName => !serverNames.Contains(serverName)).ToList();
            foreach (string serverName in removedNames) QuickAccessCache.Remove(serverName);
        }
    }
}