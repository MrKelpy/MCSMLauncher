using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common.background;
using MCSMLauncher.common.models;
using MCSMLauncher.common.processes;
using MCSMLauncher.gui;
using MCSMLauncher.utils;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;

namespace MCSMLauncher.common.server.starters.abstraction
{
    /// <summary>
    /// This abstract class implement the common methods and properties for starting a server across
    /// all the server types.
    /// </summary>
    public abstract class AbstractServerStarter : AbstractCommandProcessing
    {
        /// <summary>
        /// The main constructor for the AbstractServerStarter class. Sets the startup arguments for the server.
        /// Handled variables by default: (Add to startup arguments)<br></br>
        /// > %SERVER_JAR%: The path to the server.jar file.<br></br>
        /// > %RAM_ARGUMENTS%: The arguments to set the maximum and minimum RAM usage.<br></br>
        /// </summary>
        /// <param name="otherArguments">Extra arguments to be added into the run command</param>
        /// <param name="startupArguments">The startup arguments for the server</param>
        protected AbstractServerStarter(string otherArguments, string startupArguments)
        {
            this.StartupArguments = otherArguments + startupArguments;
        }

        /// <summary>
        /// The startup arguments for the server.
        /// </summary>
        protected string StartupArguments { get; set; }

        /// <summary>
        /// Runs the server with the given startup arguments.
        /// </summary>
        /// <param name="serverSection">The section to get the resources from</param>
        public virtual async Task Run(Section serverSection)
        {
            string serverJarPath = serverSection.GetFirstDocumentNamed("server.jar");
            string serverPropertiesPath = serverSection.GetFirstDocumentNamed("server.properties");
            ServerInformation info = ServerEditor.GetServerInformation(serverSection);

            if (serverJarPath == null) throw new FileNotFoundException("server.jar file not found");
            if (serverPropertiesPath == null) throw new FileNotFoundException("server.properties file not found");
            
            // Builds the startup arguments for the server.
            this.StartupArguments = this.StartupArguments
                .Replace("%SERVER_JAR%", PathUtils.NormalizePath(serverJarPath))
                .Replace("%RAM_ARGUMENTS%", "-Xmx" + info.Ram + "M -Xms" + info.Ram + "M");

            // Creates the process and starts it.
            Process proc = ProcessUtils.CreateProcess($"\"{info.JavaRuntimePath}/bin/java\"", this.StartupArguments,
                serverSection.SectionFullPath);
            proc.OutputDataReceived += (sender, e) => this.ProcessMergedData(sender, e, proc);
            proc.ErrorDataReceived += (sender, e) => this.ProcessMergedData(sender, e, proc);

            // Finds the port and IP to start the server with, and starts the server.
            await this.StartServer(serverSection, proc, info);
        }

        /// <summary>
        /// Find the port and IP to start the server with, and start the server.
        /// This method is subject to some higher elapsed run times due to the port mapping.
        /// </summary>
        /// <param name="serverSection">The server section to work with</param>
        /// <param name="proc">The server process to track</param>
        /// <param name="info">The ServerInformation object with the server's info</param>
        protected async Task StartServer(Section serverSection, Process proc, ServerInformation info)
        {
            Logging.LOGGER.Info($"Starting the {serverSection.SimpleName} server...");
            string settings = serverSection.GetFirstDocumentNamed("server_settings.xml");

            // Gets an available port starting on the one specified, and changes the properties file accordingly
            if (new ServerEditor(serverSection).HandlePortForServer() == 1)
            {
                string errorMessage = Logging.LOGGER.Error("Could not find a port to start the server with. Please change the port in the server properties or free up ports to use.");
                this.ProcessErrorMessages(errorMessage, proc);
                return;
            }
            
            /*
             Tries to create the port mapping for the server, and updates the server_settings.xml
             file with the correct ip based on the success of the operation.
             This will inevitably fail if the router does not support UPnP.
            */
            if (info.UPnPOn && await NetworkUtils.TryCreatePortMapping(info.BasePort, info.BasePort))
                info.IPAddress = NetworkUtils.GetExternalIPAddress();

            else info.IPAddress = NetworkUtils.GetLocalIPAddress();

            // Updates the server_settings.xml file with the correct IP prematurely.
            info.ToFile(settings);
            
            // Starts both the process, and the backup handler attached to it.
            proc.Start();
            new Thread(new ServerBackupHandler(serverSection, proc.Id).RunTask).Start();
            
            // Updates the visual elements of the server and logs the start.
            ServerList.INSTANCE.UpdateServerIP(serverSection.SimpleName);
            ServerList.INSTANCE.ForceUpdateServerState(serverSection.SimpleName, "Running");
            Logging.LOGGER.Info($"Started the {serverSection.SimpleName} server on {info.IPAddress}:{info.BasePort}.");
            
            // Records the PID of the process into the server_settings.xml file.
            info.CurrentServerProcessID = proc.Id;
            info.ToFile(settings);
        }
    }
}