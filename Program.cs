using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common;
using MCSMLauncher.gui;

namespace MCSMLauncher
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Logging.LOGGER.LoggingFilePath = Path.Combine(Constants.FileSystem.AddSection("logs").SectionFullPath, Logging.LOGGER.LoggingSession + ".log");

            try
            {
                Application.Run(new LoadingScreen());
                Application.Run(new Mainframe());
            }
            // Logs whatever fatal issue happens as a last resource.
            catch (Exception e)
            {
                Logging.LOGGER.Fatal($@"An unexpected error occured and the program was forced to exit.");
                Logging.LOGGER.Fatal(e.StackTrace, LoggingType.FILE);
            }
        }
    }
}
