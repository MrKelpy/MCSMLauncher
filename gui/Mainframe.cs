using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common.background;
using PgpsUtilsAEFC.forms.extensions;

namespace MCSMLauncher.gui
{
    /// <summary>
    /// This is the main class of the program, a form containing the main layout, where all of the
    /// others will be switched to.
    /// </summary>
    public partial class Mainframe : Form
    {
        
        /// <summary>
        /// The singleton instance of the Mainframe.
        /// </summary>
        public static Mainframe INSTANCE { get; } = new Mainframe();
        
        /// <summary>
        /// Main constructor for the Mainframe. Loads up the server list. Private to enforce the
        /// singleton pattern.
        /// </summary>
        private Mainframe()
        {
            InitializeComponent();
            this.MainLayout.SetAllFrom(NewServer.INSTANCE.GetLayout());
        }
        
        /// <summary>
        /// Loads up anything that needs to be loaded after the mainframe handle is created.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void Mainframe_Load(object sender, EventArgs e)
        {
            // Updates the server list.
            this.BeginInvoke(new MethodInvoker(delegate { Task.Run(ServerList.INSTANCE.RefreshGridAsync); }));
            
            // Starts any background tasks.
            new Thread(new ServerProcessStateHandler().RunTask) {IsBackground = true}.Start();
        }

        /// <summary>
        /// Switches the current layout into the new server creator's.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void NewServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MainLayout.Contains(NewServer.INSTANCE.RichTextBoxConsoleOutput)) return;
            this.MainLayout.SetAllFrom(NewServer.INSTANCE.GetLayout());
        }

        /// <summary>
        /// Switches the current layout into the Server List's.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private async void ServersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (this.MainLayout.Contains(ServerList.INSTANCE.GridServerList)) return;
            this.MainLayout.SetAllFrom(ServerList.INSTANCE.GetLayout());
            await ServerList.INSTANCE.UpdateAllButtonStatesAsync();
        }

        /// <summary>
        /// Keeps the rest of the forms synchronized size-wise to the mainframe.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void Mainframe_SizeChanged(object sender, EventArgs e) =>
            NewServer.INSTANCE.Size = ServerList.INSTANCE.Size = this.Size;

        /// <summary>
        /// Opens the RadminVPN website in the default browser.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void RadminVPNToolStripMenuItem_Click(object sender, EventArgs e) =>
            Process.Start("https://www.radmin-vpn.com");
    }
}
