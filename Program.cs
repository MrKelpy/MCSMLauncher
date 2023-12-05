using System;
using System.IO;
using System.Windows.Forms;
using MCSMLauncher.common;
using MCSMLauncher.ui.graphical;

namespace MCSMLauncher
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Logging.Logger.LoggingFilePath = Path.Combine(Constants.FileSystem.AddSection("logs").SectionFullPath, Logging.Logger.LoggingSession + ".log");

            try
            {
                Application.Run(new PreLoadingScreen());
                Application.Run(new LoadingScreen());
                Application.Run(Mainframe.INSTANCE);
            }
            
            // Logs whatever fatal issue happens as a last resource.
            catch (Exception e)
            {
                Logging.Logger.Fatal(@"An unexpected error occured and the program was forced to exit.");
                Logging.Logger.Fatal(e.Message + "\n" + e.StackTrace, LoggingType.File);
            }
        }
    }
}