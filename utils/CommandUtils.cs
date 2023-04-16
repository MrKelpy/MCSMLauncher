using System.Diagnostics;
using System.IO;

namespace MCSMLauncher.utils
{
    /// <summary>
    /// This class aims to provide a bunch of methods useful to running CLI commands
    /// through the application.
    /// </summary>
    public class CommandUtils
    {

        /// <summary>
        /// Runs a command in the CMD application.
        /// </summary>
        /// <param name="procname">The name of the process to be run</param>
        /// <param name="cmd">The command to run</param>
        /// <param name="workingDirectory">The working directory of the process</param>
        /// <returns>The process started</returns>
        public static Process RunCommand(string procname, string cmd, string workingDirectory = null)
        {
            // Creates a new process with the command line arguments to run the command, in a hidden
            // window.
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo 
            { 
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory(),
                FileName = procname,
                Arguments = cmd
            };
            
            // Assigns the startInfo to the process and starts it.
            process.StartInfo = startInfo;
            process.Start();
            return process;
        }
        
    }
}