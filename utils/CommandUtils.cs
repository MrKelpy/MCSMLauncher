using System.Diagnostics;

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
        /// <param name="cmd">The command to run</param>
        /// <returns>The process started</returns>
        public static Process RunCommand(string cmd)
        {
            // Creates a new process with the command line arguments to run the command, in a hidden
            // window.
            Process process = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo 
            { 
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                FileName = "cmd.exe",
                Arguments = $"/C {cmd}"
            };
            
            // Assigns the startInfo to the process and starts it.
            process.StartInfo = startInfo;
            process.Start();
            return process;
        }
        
    }
}