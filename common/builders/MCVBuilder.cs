using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public override async Task<int> RunAndCloseSilently(string serverJarPath)
        {
            // Creates a new process to run the server silently, and waits for it to finish.
            Process proc = CommandUtils.RunCommand($"\"{NewServer.INSTANCE.ComboBoxJavaVersion.Text}\\bin\\java\"", $"-jar {serverJarPath} --nogui", Path.GetDirectoryName(serverJarPath));
            Console.AppendText(Logging.LOGGER.Info("Running the server silently... (This may happen more than once!)") + Environment.NewLine);

            // If an error happens, prints it and returns 1.
            if (proc.StandardError.Peek() != -1)
            {
                Console.Clear(); Console.ForeColor = Color.Firebrick;

                while (!proc.StandardError.EndOfStream)
                    Console.AppendText(Logging.LOGGER.Error(await proc.StandardError.ReadLineAsync()) + Environment.NewLine);

                return 1;
            }
            proc.StandardError.Close();
            
            while (!proc.StandardOutput.EndOfStream)
            {
                // Read the output line by line, and wait for the "Done" line to be printed.
                await Task.Delay(100);
                
                string line = await proc.StandardOutput.ReadLineAsync();
                Logging.LOGGER.Info(line);
                if (line != null && !line.Contains("Done")) continue;
                
                Console.AppendText(Logging.LOGGER.Info("Server is DONE, killing process") + Environment.NewLine);
                break;
            }
            
            // Kills the process and disposes it.
            proc.Kill();
            proc.Dispose();

            // Finds the world folder and deletes it.
            List<string> directories = serverJarPath.Split(Path.DirectorySeparatorChar).ToList();
            string serverName = directories[directories.IndexOf("servers") + 1];
            FileSystem.GetFirstSectionNamed("servers").GetFirstSectionNamed(serverName).RemoveSection("world");
            
            Console.AppendText(Logging.LOGGER.Info("Silent run completed.") + Environment.NewLine);
            return 0;
        }
    }
}