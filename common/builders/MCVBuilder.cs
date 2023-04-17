using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common.builders.abstraction;
using MCSMLauncher.common.factories;
using MCSMLauncher.extensions;
using MCSMLauncher.gui;
using MCSMLauncher.requests.content;
using PgpsUtilsAEFC.common;
using static MCSMLauncher.common.Constants;
using PgpsUtilsAEFC.utils;

namespace MCSMLauncher.common.builders
{
    /// <summary>
    /// This class implements the server building methods for the vanilla releases. 
    /// </summary>
    public class MCVBuilder : AbstractServerBuilder
    {
        /// <summary>
        /// Installs the server.jar file given the path to the server installer. When it finishes,
        /// return the path to the server.jar file.
        /// </summary>
        /// <param name="serverInstallerPath">The path to the installer</param>
        /// <returns>The path to the server.jar file used to run the server.</returns>
        public override Task<string> InstallServer(string serverInstallerPath) => 
            Task.Run(() => serverInstallerPath);

        /// <summary>
        /// Runs the server once and closes it once it has been initialised. Deletes the world folder
        /// in the end.
        /// This method aims to initialise and build all of the server files in one go.
        /// </summary>
        /// <param name="serverJarPath">The path of the server file to run</param>
        /// <returns>A Task to allow the method to be awaited</returns>
        [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
        public override async Task<int> RunAndCloseSilently(string serverJarPath)
        {
            // Creates a new process to run the server silently, and waits for it to finish.
            Process proc = CreateProcess($"\"{NewServer.INSTANCE.ComboBoxJavaVersion.Text}\\bin\\java\"", $"-jar {serverJarPath} --nogui", Path.GetDirectoryName(serverJarPath));
            OutputConsole.AppendText(Logging.LOGGER.Info("Running the server silently... (This may happen more than once!)") + Environment.NewLine);

            // Reads through both of the STDERR and STDOUT outputs, logging them.
            int terminationCode = -1;

            // Handles the processing of the output. When finished, kills the process and return the termination code 0.
            proc.OutputDataReceived += (sender, e) =>
            {
                Logging.LOGGER.Info(e.Data);
                if (e.Data != null && !e.Data.ToLower().Contains("done")) return;
                
                terminationCode = 0;
                if (e.Data != null) proc.Kill();
            };

            // Handles the processing of the error output. When finished, kills the process and return the termination code 1.
            proc.ErrorDataReceived += (sender, e) =>
            {
                if (e.Data.Trim() == string.Empty) return; 
                
                // Updates the console TextBox with the error message. For this, since it is a different thread, we need to invoke the method.
                Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.Clear(); }));
                Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.ForeColor = Color.Firebrick; }));
                Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.AppendText(Logging.LOGGER.Error(e.Data) + Environment.NewLine); }));
                
                terminationCode = 1;
                if (e.Data != null) proc.Kill();
            };
            
            // Waits for the termination of the process by the OutputDataReceived event or ErrorDataReceived event.
            proc.Start();
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            await proc.WaitForExitAsync();
            
            // Disposes of the process and checks if the termination code is 1. If so, return 1.
            proc.Dispose();
            if (terminationCode == 1) return 1;

            // Finds the world folder and deletes it.
            List<string> directories = serverJarPath.Split(Path.DirectorySeparatorChar).ToList();
            string serverName = directories[directories.IndexOf("servers") + 1];
            FileSystem.GetFirstSectionNamed("servers").GetFirstSectionNamed(serverName).RemoveSection("world");
            
            OutputConsole.AppendText(Logging.LOGGER.Info("Silent run completed.") + Environment.NewLine);
            return 0;
        }
    }
}