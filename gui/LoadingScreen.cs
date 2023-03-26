using System.Windows.Forms;using MCSMLauncher.common;
using PgpsUtilsAEFC.common;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.gui
{
    /// <summary>
    /// This form serves as a loading screen for the launcher, which gets displayed as the launcher prepares
    /// to be used.
    /// </summary>
    public partial class LoadingScreen : Form
    {
        /// <summary>
        /// Main constructor for the LoadingScreen form. Aside from initializing the form, it also centers it to the screen,
        /// ensures the existence of the versioncache folder, and updates the cache with any new versions that are not already
        /// registered.
        /// </summary>
        public LoadingScreen()
        {
            InitializeComponent();
            CenterToScreen();
            FileSystem.AddSection("versioncache");
        }

        private void UpdateVersionCache()
        {
            
        }
    }
}