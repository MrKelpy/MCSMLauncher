using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common;
using MCSMLauncher.requests;

namespace MCSMLauncher.gui
{
    /// <summary>
    /// This singleton form handles any operations related to the creation of a new server.
    /// </summary>
    public partial class NewServer : Form
    {
        /// <summary>
        /// The instance of the class to use, matching the singleton model.
        /// </summary>
        public static NewServer INSTANCE { get; } = new NewServer();

        /// <summary>
        /// Main constructor for the NewServer form. Private in order to enforce the usage
        /// of the instance declared above.
        /// </summary>
        private NewServer()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Returns the layout of the current form, so that it can be added to another form.
        /// </summary>
        /// <returns>A Panel representing this form's layout.</returns>
        public Panel GetLayout() => this.NewServerLayout;

        /// <summary>
        /// Unlocks or changes the contents of the server version box based on the selected
        /// server type.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        private void ComboBoxServerType_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Gets the request handler for the selected server type
            AbstractBaseRequestHandler handler = RequestHandlerFactory.GetRequestHandler(ComboBoxServerType.Text);

            // Prepares the server version box for the new server type version list
            ComboServerVersion.Enabled = true;
            ComboServerVersion.Items.Clear();
            
            // Adds the versions and snapshots (if applicable) to the server version box.
        }
    }
}
