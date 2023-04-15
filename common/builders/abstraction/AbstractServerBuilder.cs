using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common.factories;
using MCSMLauncher.gui;
using MCSMLauncher.requests.content;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.common.builders.abstraction
{
    /// <summary>
    /// This interface implements contracts for the methods that should be implemented in each
    /// ServerBuilder.
    /// </summary>
    public abstract class AbstractServerBuilder
    {
        /// <summary>
        /// Main method for the server building process. Starts off all the operations.
        /// </summary>
        /// <param name="serverName">The name of the server to build</param>
        /// <param name="serverType">The type of server to build</param>
        /// <param name="serverVersion">The version of the server to build</param>
        /// <returns>A Task to allow the method to be awaited</returns>
        public async Task Build(string serverName, string serverType, string serverVersion)
        {
            // Gets the console object to update it with the logs
            TextBox console = NewServer.INSTANCE.TextBoxConsoleOutput;
            console.Clear();

            // Ensures that there's a clean section for the server to be built on
            console.AppendText(Logging.LOGGER.Info($"Starting the build for a new {serverType} {serverVersion} server named {serverName}.") + Environment.NewLine);
            
            Section serversSection = Constants.FileSystem.GetFirstSectionNamed("servers");
            serversSection.RemoveSection(serverName);
            Section currentServerSection = serversSection.AddSection(serverName);
            
            console.AppendText(Logging.LOGGER.Info($"Created a new {serverName} section.") + Environment.NewLine);

            // Gets the direct download link for the server jar based on the version and type
            ServerTypeMappingsFactory multiFactory = new ServerTypeMappingsFactory();
            string downloadLink = multiFactory.GetCacheContentsForType(serverType)[serverVersion];
            string directDownloadLink = await multiFactory.GetParserFor(serverType).GetServerDirectDownloadLink(downloadLink);
            console.AppendText(Logging.LOGGER.Info($"Retrieved the resources for a new \"{serverType}.{serverVersion}\"") + Environment.NewLine);
            
            // Downloads the server jar into the server folder
            console.AppendText(Logging.LOGGER.Info($"Preparing to download the necessary files...") + Environment.NewLine);
            await FileDownloader.DownloadFileAsync(Path.Combine(currentServerSection.SectionFullPath, "server.jar"), directDownloadLink);
            
            // Gets the server.jar file path and installs the server
            Section serverSection = FileSystem.GetFirstSectionNamed("servers").GetFirstSectionNamed(serverName);
            string serverInstallerJar = serverSection.GetAllFiles().FirstOrDefault(x => x.Contains("server") && x.EndsWith(".jar"));
            string serverJarPath = await InstallServer(serverInstallerJar);
            
            // Runs the server once and closes it, in order to create the first files.
            await RunAndCloseSilently(serverJarPath);
            
            // Agrees to the EULA
            console.AppendText(Logging.LOGGER.Info($"Agreeing to the EULA") + Environment.NewLine);
            this.AgreeToEula(FileSystem.GetFirstFileNamed("eula.txt"));

            // Runs the server once and closes it, in order to create the rest of the server files.
            await RunAndCloseSilently(serverJarPath);
            
            console.AppendText(Logging.LOGGER.Info($"Finished building the server.") + Environment.NewLine);
        }

        /// <summary>
        /// Agrees to the eula by replacing the eula=false line in the file to eula=true.
        /// </summary>
        /// <param name="eulaPath">The path to the eula.txt file</param>
        public void AgreeToEula(string eulaPath)
        {
            List<string> lines = FileUtils.ReadFromFile(eulaPath);
            
            // Replaces the eula=false line with eula=true
            for (var i = 0; i < lines.Count; i++)
                if (lines[i].Equals("eula=false")) lines[i] = "eula=true";
            
            FileUtils.DumpToFile(eulaPath, lines);
        }
        
        /// <summary>
        /// Installs the server.jar file given the path to the server installer. When it finishes,
        /// return the path to the server.jar file.
        /// </summary>
        /// <param name="serverInstallerPath">The path to the installer</param>
        /// <returns>The path to the server.jar file used to run the server.</returns>
        public abstract Task<string> InstallServer(string serverInstallerPath);

        /// <summary>
        /// Runs the server once and closes it once it has been initialised. Deletes the world folder
        /// in the end.
        /// This method aims to initialise and build all of the server files in one go.
        /// </summary>
        /// <param name="serverJarPath">The path of the server file to run</param>
        /// <returns>A Task to allow the method to be awaited</returns>
        public abstract Task RunAndCloseSilently(string serverJarPath);
    }
}