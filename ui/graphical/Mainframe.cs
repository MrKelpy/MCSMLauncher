using System;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LaminariaCore_Winforms.forms.extensions;
using MCSMLauncher.common.background;

// ReSharper disable InconsistentNaming

namespace MCSMLauncher.ui.graphical
{
    /// <summary>
    /// This is the main class of the program, a form containing the main layout, where all of the
    /// others will be switched to.
    /// </summary>
    public partial class Mainframe : Form
    {
        /// <summary>
        /// Main constructor for the Mainframe. Loads up the server list. Private to enforce the
        /// singleton pattern.
        /// </summary>
        private Mainframe()
        {
            InitializeComponent();
            MainLayout.SetAllFrom(NewServer.INSTANCE.GetLayout());
        }

        /// <summary>
        /// The singleton instance of the Mainframe.
        /// </summary>
        public static Mainframe INSTANCE { get; } = new ();

        /// <summary>
        /// Loads up anything that needs to be loaded after the mainframe handle is created.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private async void Mainframe_Load(object sender, EventArgs e)
        {
            Text += @" v" + ConfigurationManager.AppSettings.Get("Version.App");

            // Updates the server list.
            await Task.WhenAll(ServerList.INSTANCE.RefreshGridAsync());

            // Starts any background tasks.
            new Thread(new ServerProcessStateHandler().RunTask) { IsBackground = true }.Start();
        }

        /// <summary>
        /// Switches the current layout into the new server creator's.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void NewServerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainLayout.Contains(NewServer.INSTANCE.RichTextBoxConsoleOutput)) return;
            MainLayout.SetAllFrom(NewServer.INSTANCE.GetLayout());
        }

        /// <summary>
        /// Switches the current layout into the Server List's.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ServersToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MainLayout.Contains(ServerList.INSTANCE.GridServerList)) return;
            MainLayout.SetAllFrom(ServerList.INSTANCE.GetLayout());
        }

        /// <summary>
        /// Keeps the rest of the forms synchronized size-wise to the mainframe.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void Mainframe_SizeChanged(object sender, EventArgs e)
        {
            NewServer.INSTANCE.Size = ServerList.INSTANCE.Size = Size;
        }

        /// <summary>
        /// Opens the RadminVPN website in the default browser.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void RadminVPNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://www.radmin-vpn.com");
        }

        /// <summary>
        /// Opens the GitHub repository wiki in the default browser.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void WikiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("https://github.com/MrKelpy/MCSMLauncher/wiki");
        }
    }
}