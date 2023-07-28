#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
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
        /// A cache of the server editors, used to avoid having to re-fetch them every time.
        /// </summary>
        private List<ServerEditor> ServerEditorsCache { get; set; } = new List<ServerEditor>();
        
        /// <summary>
        /// Runs the background task.
        /// </summary>
        public async void RunTask()
        {
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
                    ServerEditor editor = GetFromCache(serverName) ?? AddToCache(serverName);
                    await ServerList.INSTANCE.UpdateServerButtonStateAsync(editor);
                }
            }
        }
        
        /// <summary>
        /// Gets the server editor from the cache.
        /// </summary>
        /// <param name="serverName">The server name to look for</param>
        /// <returns>The ServerEditor matching the server name provided</returns>
        private ServerEditor? GetFromCache(string serverName) =>
            ServerEditorsCache.FirstOrDefault(x => x.ServerSection.SimpleName.Equals(serverName));
        
        /// <summary>
        /// Adds a server editor to the cache, returning it afterwards.
        /// </summary>
        /// <param name="serverName">The server name to match the editor to</param>
        /// <returns>The ServerEditor instance</returns>
        private ServerEditor AddToCache(string serverName)
        {
            // Gets the server section and creates the editor.
            Section serverSection = Constants.FileSystem.GetFirstSectionNamed($"servers/{serverName}");
            ServerEditor editor = new (serverSection);
            
            // Adds the editor to the cache and returns it.
            ServerEditorsCache.Add(editor);
            return editor;
        }
    }
}