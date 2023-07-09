using System.Diagnostics;
using System.IO;
using System.Threading;
using MCSMLauncher.common.background;
using MCSMLauncher.common.models;
using MCSMLauncher.common.processes;
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
            StartupArguments = otherArguments + startupArguments;
        }

        /// <summary>
        /// The startup arguments for the server.
        /// </summary>
        protected string StartupArguments { get; set; }

        /// <summary>
        /// Other arguments that might be needed for the server.
        /// </summary>
        protected string OtherArguments { get; }

        /// <summary>
        /// Runs the server with the given startup arguments.
        /// </summary>
        /// <param name="serverSection">The section to get the resources from</param>
        public virtual void Run(Section serverSection)
        {
            string serverJarPath = serverSection.GetFirstDocumentNamed("server.jar");
            string serverPropertiesPath = serverSection.GetFirstDocumentNamed("server.properties");
            string settings = serverSection.GetFirstDocumentNamed("server_settings.xml");
            ServerInformation info = GetServerInformation(serverSection);

            if (serverJarPath == null) throw new FileNotFoundException("server.jar file not found");
            if (serverPropertiesPath == null) throw new FileNotFoundException("server.properties file not found");
            
            // Builds the startup arguments for the server.
            StartupArguments = StartupArguments.Replace("%SERVER_JAR%", PathUtils.NormalizePath(serverJarPath))
                .Replace("%RAM_ARGUMENTS%", "-Xmx" + info.Ram + "M -Xms" + info.Ram + "M");

            // Creates the process and starts it.
            Process proc = ProcessUtils.CreateProcess($"\"{info.JavaRuntimePath}/bin/java\"", StartupArguments,
                serverSection.SectionFullPath);
            proc.OutputDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);
            proc.ErrorDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);

            // Gets an available port starting on the one specified, and changes the properties file accordingly
            if (new ServerEditor(serverSection).HandlePortForServer() == 1)
            {
                ProcessErrorMessages("Could not find a port to start the server with! Please change the port in the server properties or free up ports to use.", proc);
                return;
            }

            // Starts both the process, and the backup handler attached to it.
            proc.Start();
            new Thread(new ServerBackupHandler(serverSection, proc.Id).RunTask).Start();

            // Records the PID of the process into the server_settings.xml file.
            info.CurrentServerProcessID = proc.Id;
            info.ToFile(settings);
        }

        /// <summary>
        /// Returns the server information based on the server_settings.xml file, or creates a
        /// new one with minimal info.
        /// </summary>
        /// <param name="serverSection">The section to work with</param>
        /// <returns>The new server information instance</returns>
        protected ServerInformation GetServerInformation(Section serverSection)
        {
            // Check if the "server_settings.xml" file exists
            string settings = serverSection.GetFirstDocumentNamed("server_settings.xml");

            // If the file exists, load the server information from it
            if (settings != null) return ServerInformation.FromFile(settings);

            // If the file doesn't exist, create a new one with minimal information
            return new ServerInformation().GetMinimalInformation(serverSection);
        }
    }
}