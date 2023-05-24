using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Management;

namespace MCSMLauncher.utils
{
    /// <summary>
    /// This class implements a bunch of methods to manipulate processes
    /// </summary>
    public static class ProcessUtils
    {
        
        /// <summary>
        /// Kill a process, and all of its children, grandchildren, etc.
        /// </summary>
        /// <param name="proc">The process where this method is being called from</param>
        /// <param name="pid">Process ID.</param>
        [SuppressMessage("ReSharper", "PossibleInvalidCastExceptionInForeachLoop")]
        public static void KillProcessAndChildren(this Process proc, int pid = -1)
        {
            try
            {
                if (pid == -1) pid = proc.Id; // If not specified, use the proc id as the pid.
                if (pid == 0) return; // Can't close the windows idle process
                
            // If the process is already closed, return.
            } catch (InvalidOperationException) { return; }

            // Get all the process management objects for the PID specified.
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("Select * From Win32_Process Where ParentProcessID=" + pid);
            
            // Run this method recursively on every children of the proc
            foreach (ManagementObject managementObject in searcher.Get()) 
                KillProcessAndChildren(null, Convert.ToInt32(managementObject["ProcessID"]));
            
            try { Process.GetProcessById(pid).Kill(); }
            
            // Ignored, process already exiting.
            catch (ArgumentException ) {}
            catch (Win32Exception) { }
        }
        
        /// <summary>
        /// Creates a Java jar process, redirecting its STDOUT and STDERR to process it.
        /// </summary>
        /// <param name="java">The java version path to run</param>
        /// <param name="args">The java args to run the jar with</param>
        /// <param name="workingDirectory">The working directory of the process</param>
        /// <returns>The process started</returns>
        public static Process CreateProcess(string java, string args, string workingDirectory = null)
        {
            // Creates a new process with the command line arguments to run the command, in a hidden
            // window.
            Process proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo 
            { 
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory(),
                FileName = java,
                Arguments = args
            };
            
            // Assigns the startInfo to the process and starts it.
            proc.StartInfo = startInfo;
            return proc;
        }
        
        /// <summary>
        /// Gets a process object from its ID if it is still running.
        /// </summary>
        /// <param name="id">The ID of the process to get</param>
        /// <returns>The Process, if it is still running. Else, null</returns>
        public static Process GetProcessById(int id) {
            return Process.GetProcesses().FirstOrDefault(x => x.Id == id);
        }
    }
}