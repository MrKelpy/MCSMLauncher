﻿using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using LaminariaCore_General.utils;
using LaminariaCore_Winforms.common;
using MCSMLauncher.common.background;
using MCSMLauncher.common.handlers;
using MCSMLauncher.common.models;
using MCSMLauncher.ui.graphical;
using MCSMLauncher.utils;

namespace MCSMLauncher.common.server.starters.abstraction
{
    /// <summary>
    /// This abstract class implement the common methods and properties for starting a server across
    /// all the server types.
    /// </summary>
    public abstract class AbstractServerStarter : AbstractLoggingMessageProcessing
    {
        /// <summary>
        /// The main constructor for the AbstractServerStarter class. Sets the startup arguments for the server.
        /// Handled variables by default: (Add to startup arguments)<br></br>
        /// > %SERVER_JAR%: The path to the server.jar file.<br></br>
        /// > %RAM_ARGUMENTS%: The arguments to set the maximum and minimum RAM usage.<br></br>
        /// </summary>
        /// <param name="otherArguments">Extra arguments to be added into the run command</param>
        /// <param name="startupArguments">The startup arguments for the server</param>
        /// <param name="outputHandler">The output system to use while logging the messages.</param>
        protected AbstractServerStarter(string otherArguments, string startupArguments, MessageProcessingOutputHandler outputHandler) : base(outputHandler)
        {
            StartupArguments = otherArguments + startupArguments;
        }

        /// <summary>
        /// The startup arguments for the server.
        /// </summary>
        private string StartupArguments { get; set; }

        /// <summary>
        /// Runs the server with the given startup arguments.
        /// </summary>
        /// <param name="editor">The ServerEditor instance to use</param>
        /// <returns>The process that was just created</returns>
        public virtual async Task<Process> Run(ServerEditor editor)
        {
            // Get the server.jar and server.properties paths.
            Section serverSection = editor.ServerSection;
            string serverJarPath = serverSection.GetFirstDocumentNamed("server.jar");
            ServerInformation info = editor.GetServerInformation();
            
            if (serverJarPath == null) throw new FileNotFoundException("server.jar file not found");
            
            // Builds the startup arguments for the server.
            StartupArguments = StartupArguments
                .Replace("%SERVER_JAR%", PathUtils.NormalizePath(serverJarPath))
                .Replace("%RAM_ARGUMENTS%", "-Xmx" + info.Ram + "M -Xms" + info.Ram + "M");

            string javaPath = info.JavaRuntimePath != "java" ? $"\"{info.JavaRuntimePath}/bin/java\"" : info.JavaRuntimePath;
            if (!info.UseGUI) StartupArguments += " nogui";
            
            // Creates the process and starts it.
            Process proc = ProcessUtils.CreateProcess(javaPath, StartupArguments, serverSection.SectionFullPath);

            proc.OutputDataReceived += (sender, e) => RedirectMessageProcessing(sender, e, proc, serverSection.SimpleName);
            proc.ErrorDataReceived += (sender, e) => RedirectMessageProcessing(sender, e, proc, serverSection.SimpleName);

            // Finds the port and IP to start the server with, and starts the server.
            await StartServer(serverSection, proc, editor);
            return proc;
        }

        /// <summary>
        /// Find the port and IP to start the server with, and start the server.
        /// This method is subject to some higher elapsed run times due to the port mapping.
        /// </summary>
        /// <param name="serverSection">The server section to work with</param>
        /// <param name="proc">The server process to track</param>
        /// <param name="editor">The editor instance to use to interact with the files</param>
        protected async Task StartServer(Section serverSection, Process proc, ServerEditor editor)
        {
            Logging.Logger.Info($"Starting the {serverSection.SimpleName} server...");

            // Gets an available port starting on the one specified, automatically update and flush the buffers.
            if (editor.HandlePortForServer() == 1)
            {
                string errorMessage = Logging.Logger.Error("Could not find a port to start the server with. Please change the port in the server properties or free up ports to use.");
                ProcessErrorMessages(errorMessage, proc);
                ServerList.INSTANCE.ForceUpdateServerState(serverSection.SimpleName, "Start");
                return;
            }
            
            /*
             Tries to create the port mapping for the server, and updates the server_settings.xml
             file with the correct ip based on the success of the operation.
             This will inevitably fail if the router does not support UPnP.
            */
            ServerInformation info = editor.GetServerInformation();

            if (info.UPnPOn && await NetworkUtilExtensions.TryCreatePortMapping(info.Port, info.Port))
                info.IPAddress = NetworkUtils.GetExternalIPAddress();

            else info.IPAddress = NetworkUtils.GetLocalIPAddress();
            
            // Sets up the process to be hidden and not create a window.
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.StartInfo.CreateNoWindow = true;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.UseShellExecute = false;

            // Starts both the process, and the backup handler attached to it, and records the process ID.
            proc.Start();
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            new Thread(new ServerBackupHandler(editor, proc.Id).RunTask) { IsBackground = false }.Start();
            info.CurrentServerProcessID = proc.Id;
            
            // Updates and flushes the buffers, writing the changes to the files.
            editor.UpdateBuffers(info.ToDictionary());
            editor.FlushBuffers(); 
            
            // Updates the visual elements of the server and logs the start.
            ServerList.INSTANCE.UpdateServerIP(editor);
            ServerList.INSTANCE.ForceUpdateServerState(serverSection.SimpleName, "Running");
            Logging.Logger.Info($"Running {serverSection.SimpleName} on {info.IPAddress}:{info.Port}.");
        }
    }
}
