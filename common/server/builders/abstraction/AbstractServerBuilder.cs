﻿using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MCSMLauncher.common.factories;
using MCSMLauncher.common.models;
using MCSMLauncher.common.processes;
using MCSMLauncher.extensions;
using MCSMLauncher.gui;
using MCSMLauncher.requests.content;
using MCSMLauncher.utils;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.common.server.builders.abstraction
{
    /// <summary>
    /// This interface implements contracts for the methods that should be implemented in each
    /// ServerBuilder.
    /// </summary>
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    public abstract class AbstractServerBuilder : AbstractCommandProcessing
    {
        /// <summary>
        /// Main constructor for the AbstractServerBuilder class, sets the startup arguments
        /// to be used in the server startups, through the child classes.
        /// </summary>
        /// <param name="startupArguments">The start</param>
        protected AbstractServerBuilder(string startupArguments)
        {
            StartupArguments = startupArguments;
            SpecialErrors.Add("Exception handling console input");
            SpecialErrors.Add("Error during early discovery");
            SpecialErrors.Add("Cannot read the array length");
            SpecialErrors.Add("FML appears to be missing any signature data");
            SpecialErrors.Add("Failed to load");
        }

        /// <summary>
        /// The startup arguments to be used by the methods that start up the server.
        /// Variables:
        /// > %SERVER_JAR%: The path to the server.jar file
        /// </summary>
        protected string StartupArguments { get; private set; }

        /// <summary>
        /// Accesses the given file and returns the section based on its parent folder.
        /// </summary>
        /// <param name="filepath">The filepath to get the section for</param>
        /// <returns>A Section object for the directory the file is located in</returns>
        protected Section GetSectionFromFile(string filepath)
        {
            var directories = filepath.Split(Path.DirectorySeparatorChar).ToList();
            var serverName = directories[directories.IndexOf("servers") + 1];
            return FileSystem.GetFirstSectionNamed("servers/" + serverName);
        }

        /// <summary>
        /// Main method for the server building process. Starts off all the operations.
        /// </summary>
        /// <param name="serverName">The name of the server to build</param>
        /// <param name="serverType">The type of server to build</param>
        /// <param name="serverVersion">The version of the server to build</param>
        /// <returns>A Task to allow the method to be awaited</returns>
        public async Task Build(string serverName, string serverType, string serverVersion)
        {
            // Ensures that there's a clean section for the server to be built on
            OutputConsole.Clear();
            OutputConsole.AppendText(
                Logging.LOGGER.Info(
                    $"Starting the build for a new {serverType} {serverVersion} server named {serverName}.") +
                Environment.NewLine);

            var serversSection = FileSystem.GetFirstSectionNamed("servers");
            serversSection.RemoveSection(serverName);
            var currentServerSection = serversSection.AddSection(serverName);

            OutputConsole.AppendText(Logging.LOGGER.Info($"Created a new {serverName} section.") + Environment.NewLine);

            // Gets the direct download link for the server jar based on the version and type
            var multiFactory = new ServerTypeMappingsFactory();
            var downloadLink = multiFactory.GetCacheContentsForType(serverType)[serverVersion];
            var directDownloadLink = await multiFactory.GetParserFor(serverType)
                .GetServerDirectDownloadLink(serverVersion, downloadLink);
            OutputConsole.AppendText(
                Logging.LOGGER.Info($"Retrieved the resources for a new \"{serverType}.{serverVersion}\"") +
                Environment.NewLine);

            // Downloads the server jar into the server folder
            OutputConsole.AppendText(Logging.LOGGER.Info("Downloading the server.jar...") + Environment.NewLine);
            await FileDownloader.DownloadFileAsync(Path.Combine(currentServerSection.SectionFullPath, "server.jar"),
                directDownloadLink);

            // Gets the server.jar file path and installs the server
            var serverSection = FileSystem.GetFirstSectionNamed("servers/" + serverName);
            var serverInstallerJar = serverSection.GetAllDocuments()
                .FirstOrDefault(x => x.Contains("server") && x.EndsWith(".jar"));
            var serverJarPath = await InstallServer(serverInstallerJar);

            // Creates the server_settings.xml file, marks it as hidden and serializes the ServerInformation object into it
            var serverSettingsPath = Path.Combine(serverSection.SectionFullPath, "server_settings.xml");
            var serverInformation = new ServerInformation
            {
                Type = serverType,
                Version = serverVersion,
                ServerBackupsPath = serverSection.AddSection("backups/server").SectionFullPath,
                PlayerdataBackupsPath = serverSection.AddSection("backups/playerdata").SectionFullPath,
                JavaRuntimePath = NewServer.INSTANCE.ComboBoxJavaVersion.Text
            };

            XMLUtils.SerializeToFile<ServerInformation>(serverSettingsPath, serverInformation);
            File.SetAttributes(serverSettingsPath, File.GetAttributes(serverSettingsPath) | FileAttributes.Hidden);

            // Runs the server once and closes it, in order to create the first files.
            if (await RunAndCloseSilently(serverJarPath) == 1)
            {
                FileSystem.RemoveSection("servers/" + serverName);
                return;
            }

            // Agrees to the EULA if it exists, and if it doesn't, skips it.
            var eulaPath = serverSection.GetFirstDocumentNamed("eula.txt");

            if (File.Exists(eulaPath))
            {
                OutputConsole.AppendText(Logging.LOGGER.Info("Agreeing to the EULA") + Environment.NewLine);
                AgreeToEula(eulaPath);
            }

            // Runs the server once and closes it, in order to create the rest of the server files.
            // Since this is a proper run now, we've got to check for an available port.
            if (await RunAndCloseSilently(serverJarPath) == 1)
            {
                FileSystem.RemoveSection("servers/" + serverName);
                return;
            }

            await ServerList.INSTANCE.AddServerToListAsync(serverSection);
            OutputConsole.SelectionColor = Color.LimeGreen;
            OutputConsole.AppendText(Logging.LOGGER.Info("Finished building the server.") + Environment.NewLine);
            OutputConsole.SelectionColor = Color.Black;
        }

        /// <summary>
        /// Agrees to the eula by replacing the eula=false line in the file to eula=true.
        /// </summary>
        /// <param name="eulaPath">The path to the eula.txt file</param>
        private void AgreeToEula(string eulaPath)
        {
            var lines = FileUtils.ReadFromFile(eulaPath);

            // Replaces the eula=false line with eula=true
            var eulaIndex = lines.IndexOf("eula=false");
            if (eulaIndex == -1) return;

            lines[eulaIndex] = "eula=true";
            FileUtils.DumpToFile(eulaPath, lines);
        }

        /// <summary>
        /// Runs the server once and closes it once it has been initialised. Deletes the world folder
        /// in the end.
        /// This method aims to initialise and build all of the server files in one go.
        /// </summary>
        /// <param name="serverJarPath">The path of the server file to run</param>
        /// <returns>A Task with a code letting the user know if an error happened</returns>
        protected virtual async Task<int> RunAndCloseSilently(string serverJarPath)
        {
            // Creates a new process to run the server silently, and waits for it to finish.
            StartupArguments = StartupArguments.Replace("%SERVER_JAR%", serverJarPath);
            OutputConsole.AppendText(
                Logging.LOGGER.Info("Running the server silently... (This may happen more than once!)") +
                Environment.NewLine);

            // Gets the server section from the path of the jar being run, the runtime and creates the process
            var serverSection = GetSectionFromFile(serverJarPath);
            var info = XMLUtils.DeserializeFromFile<ServerInformation>(
                serverSection.GetFirstDocumentNamed("server_settings.xml"));
            var proc = ProcessUtils.CreateProcess($"\"{info.JavaRuntimePath}\\bin\\java\"", StartupArguments,
                Path.GetDirectoryName(serverJarPath));

            // Gets an available port starting on the one specified, and changes the properties file accordingly
            if (new ServerEditor(serverSection).HandlePortForServer(serverSection) == 1)
            {
                ProcessErrorMessages(
                    "Could not find a port to start the server with! Please change the port in the server properties or free up ports to use.",
                    proc);
                return 1;
            }

            // Handles the processing of the STDOUT and STDERR outputs, changing the termination code accordingly.
            proc.OutputDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);
            proc.ErrorDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);

            // Waits for the termination of the process by the OutputDataReceived event or ErrorDataReceived event.
            proc.Start();
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            await proc.WaitForExitAsync();

            // Disposes of the process and checks if the termination code is 1. If so, return 1.
            proc.Dispose();
            serverSection.RemoveSection("world"); // Finds the world folder and deletes it if it exists
            if (TerminationCode * TerminationCode == 1) return 1;

            // Completes the run, resetting the termination code
            OutputConsole.AppendText(Logging.LOGGER.Info("Silent run completed.") + Environment.NewLine);
            TerminationCode = -1;
            return 0;
        }

        /// <summary>
        /// Processes any INFO messages received from the server jar.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>0 - The server.jar fired a normal info message</terminationCode>
        protected override void ProcessInfoMessages(string message, Process proc)
        {
            base.ProcessInfoMessages(message, proc);
            if (message.ToLower().Contains("preparing level")) proc.KillProcessAndChildren();
        }

        /// <summary>
        /// Processes any ERROR messages received from the server jar.
        /// Since we might be updating the console from another thread, we're just going to invoke everything
        /// and that's that.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>1 - The server.jar fired an error. If fired last, stop the build.</terminationCode>
        protected override void ProcessErrorMessages(string message, Process proc)
        {
            base.ProcessErrorMessages(message, proc);
            if (SpecialErrors.StringMatches(message)) return;
            TerminationCode = !message.Contains("server.properties") ? 1 : TerminationCode;
        }

        /// <summary>
        /// Processes any WARN messages received from the server jar.
        /// Since we might be updating the console from another thread, we're just going to invoke everything
        /// and that's that.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>2 - The server.jar fired a warning</terminationCode>
        protected override void ProcessWarningMessages(string message, Process proc)
        {
            base.ProcessWarningMessages(message, proc);
            if (message.Contains("already running on that port")) proc.KillProcessAndChildren();
        }

        /// <summary>
        /// Installs the server.jar file given the path to the server installer. When it finishes,
        /// return the path to the server.jar file.
        /// </summary>
        /// <param name="serverInstallerPath">The path to the installer</param>
        /// <returns>The path to the server.jar file used to run the server.</returns>
        protected abstract Task<string> InstallServer(string serverInstallerPath);
    }
}