using System.Diagnostics;
using System.IO;
using MCSMLauncher.common.models;
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
        /// The startup arguments for the server.
        /// </summary>
        protected string StartupArguments { get; set; }
        
        /// <summary>
        /// Other arguments that might be needed for the server.
        /// </summary>
        protected string OtherArguments { get;  }
        
        /// <summary>
        /// The main constructor for the AbstractServerStarter class. Sets the startup arguments for the server.
        /// Handled variables by default: (Add to startup arguments)<br></br>
        ///     > %SERVER_JAR%: The path to the server.jar file.<br></br>
        ///     > %RAM_ARGUMENTS%: The arguments to set the maximum and minimum RAM usage.<br></br>
        /// </summary>
        /// <param name="otherArguments">Extra arguments to be added into the run command</param> 
        /// <param name="startupArguments">The startup arguments for the server</param>
        protected AbstractServerStarter(string otherArguments, string startupArguments) => StartupArguments = otherArguments + startupArguments;

        /// <summary>
        /// Runs the server with the given startup arguments.
        /// </summary>
        /// <param name="serverSection">The section to get the resources from</param>
        public void Run(Section serverSection)
        {
            string serverJarPath = serverSection.GetFirstDocumentNamed("server.jar");
            string serverPropertiesPath = serverSection.GetFirstDocumentNamed("server.properties");

            if (serverJarPath == null) throw new FileNotFoundException("server.jar file not found");
            if (serverPropertiesPath == null) throw new FileNotFoundException("server.properties file not found");
            
            // Gets the server information from the server_settings.xml file or creates a new one with minimal information.
            ServerInformation info = serverSection.GetFirstDocumentNamed("server_settings.xml") is var settings && settings != null 
                ? XMLUtils.DeserializeFromFile<ServerInformation>(settings) 
                : new ServerInformation().GetMinimalInformation(serverSection);

            // Builds the startup arguments for the server.
            StartupArguments = StartupArguments.Replace("%SERVER_JAR%", PathUtils.NormalizePath(serverJarPath))
                .Replace("%RAM_ARGUMENTS%", "-Xmx" + info.Ram + "M -Xms" + info.Ram + "M");

            // Creates the process and starts it.
            Process proc = ProcessUtils.CreateProcess($"\"{info.JavaRuntimePath}/bin/java\"", StartupArguments, serverSection.SectionFullPath);
            proc.OutputDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);
            proc.ErrorDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);
            
            // Gets an available port starting on the one specified, and changes the properties file accordingly
            if (new ServerEditor(serverSection).HandlePortForServer(serverSection) == 1)
            {
                ProcessErrorMessages("Could not find a port to start the server with! Please change the port in the server properties or free up ports to use.", proc);
                return;
            }
            
            proc.Start();
            
            // Records the PID of the process into the server_settings.xml file.
            info.CurrentServerProcessID = proc.Id;
            if (settings != null) File.Delete(settings);
            XMLUtils.SerializeToFile<ServerInformation>(settings, info);
        }
    }
}