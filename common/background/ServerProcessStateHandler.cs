using System.Threading;
using System.Windows.Forms;
using MCSMLauncher.common.interfaces;
using MCSMLauncher.gui;

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
        public void RunTask()
        {
            while (true) // This specific background task should run forever.
            {
                Thread.Sleep(1 * 1000); // 1 second of cooldown between each check.

                foreach (DataGridViewRow row in ServerList.INSTANCE.GridServerList.Rows)
                {
                    // Targets only the servers that are running.
                    if (row.Cells[5].Value?.ToString() != "Running") continue;

                    // Updates the server state.
                    var serverName = row.Cells[2]?.Value.ToString();
                    ServerList.INSTANCE.UpdateServerButtonState(serverName);
                }
            }
        }
    }
}