using System;
using System.IO;
using System.Windows.Forms;
using mcsm.common;
using mcsm.ui.console;
using mcsm.ui.graphical;
using mcsm.utils;
using Microsoft.Win32.SafeHandles;

namespace mcsm
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
                    // Allocates a console for the program to use and gets its handle.
                    if (!NativeMethods.AttachConsole(-1));
                        NativeMethods.AllocConsole();
                        
                    IntPtr stdHandle = NativeMethods.GetStdHandle(-11);
                    SafeFileHandle safeFileHandle = new SafeFileHandle(stdHandle, true);
                    
                    // Opens a stream to the console and sets it as the output stream.
                    FileStream fs = new FileStream(safeFileHandle, FileAccess.Write);
                    StreamWriter sysout = new StreamWriter(fs) {AutoFlush = true};
                    Console.SetOut(sysout);

                    // Executes the command and frees the console.
                    new ConsoleCommandExecutor().ExecuteCommand(ConsoleCommandParser.Parse(args));
                    
                    Application.ApplicationExit += (sender, e) => { NativeMethods.FreeConsole(); };
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