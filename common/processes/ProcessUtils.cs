using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
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
            if (pid == -1) pid = proc.Id;  // If not specified, use the proc id as the pid.
            if (pid == 0) return;  // Can't close the windows idle process
            
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
    }
}