using System;
using System.IO;
using System.Windows.Forms;
using MCSMLauncher.common;
using MCSMLauncher.common.models;
using MCSMLauncher.ui.console;
using MCSMLauncher.ui.graphical;

namespace MCSMLauncher
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Logging.Logger.LoggingFilePath = Path.Combine(Constants.FileSystem.AddSection("logs").SectionFullPath, Logging.Logger.LoggingSession + ".log");

            try
            {
                // If there are any arguments, then the program will not run the graphical interface.
                if (args.Length > 0)
                {
                    ConsoleCommand command = ConsoleCommandParser.Parse(args);
                    return;
                }
                    
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