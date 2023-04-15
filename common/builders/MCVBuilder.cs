using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common.builders.abstraction;
using MCSMLauncher.common.factories;
using MCSMLauncher.extensions;
using MCSMLauncher.gui;
using MCSMLauncher.requests.content;
using MCSMLauncher.utils;
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
        public override async Task RunAndCloseSilently(string serverJarPath)
        {
            // Builds the command to run the server and runs it.
            string cmd = "java -Xmx1024M -Xms1024M -jar " + serverJarPath + " --nogui";
            Console.AppendText(Logging.LOGGER.Info("Running the server silently... (This may happen more than once!)") + Environment.NewLine);
            Process proc = CommandUtils.RunCommand(cmd);
            
            while (!proc.StandardOutput.EndOfStream)
            {
                // Read the output line by line, and wait for the "Done" line to be printed.
                await Task.Delay(100);
                string line = await proc.StandardOutput.ReadLineAsync();
                if (line != null && !line.Contains("Done")) continue;
                
                Console.AppendText(Logging.LOGGER.Info("Server is DONE, killing process") + Environment.NewLine);
                break;
            }
            
            // Waits asynchronously for the process to exit and then disposes it.
            await proc.WaitForExitAsync();
            proc.Dispose();

            // Finds the world folder and deletes it.
            List<string> directories = serverJarPath.Split(Path.DirectorySeparatorChar).ToList();
            string serverName = directories[directories.IndexOf("servers") + 1];
            FileSystem.GetFirstSectionNamed("servers").GetFirstSectionNamed(serverName).RemoveSection("world");
            
            Console.AppendText(Logging.LOGGER.Info("Silent run completed."));
        }
    }
}