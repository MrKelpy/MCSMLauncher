#nullable enable
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using LaminariaCore_Winforms.common;
using mcsm.common.caches;
using mcsm.common.interfaces;
using mcsm.ui.graphical;

namespace mcsm.common.background
{
    /// <summary>
    /// This class implements a background process for checking the state of every server every second, and
    /// making sure that any servers that end up stopping are properly updated in the server list.
    /// </summary>
    public class ServerProcessStateHandler : IBackgroundRunner
    {
        /// <summary>
        /// A flag that marks whether this is the first run of the background task. This is used so that
        /// every server is checked at least once when the program starts, updating its state to the correct one.
        /// </summary>
        private bool FirstRunBypassFlag { get; set; } = true;
        
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
                    // Gets the server name from the row.
                    string? serverName = row.Cells[2]?.Value.ToString();
                    if (serverName == null) continue;
                    
                    // Only update the server button state if the server is running.
                    if (row.Cells[5]?.Value.ToString() is not ("Running" or "Starting" or "Stopping") && !FirstRunBypassFlag) continue;
                    await ServerList.INSTANCE.UpdateServerButtonStateAsync(serverName);
                }
                
                FirstRunBypassFlag = false;
            }
        }
    }
}