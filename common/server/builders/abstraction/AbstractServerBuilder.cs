using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LaminariaCore_General.utils;
using LaminariaCore_Winforms.common;
using MCSMLauncher.common.factories;
using MCSMLauncher.common.models;
using MCSMLauncher.common.processes;
using MCSMLauncher.extensions;
using MCSMLauncher.requests.content;
using MCSMLauncher.ui.graphical;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.common.server.builders.abstraction
{
    /// <summary>
    /// This interface implements contracts for the methods that should be implemented in each
    /// ServerBuilder.
    /// </summary>
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    public abstract class AbstractServerBuilder : AbstractLoggingMessageProcessing
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
        /// Main method for the server building process. Starts off all the operations.
        /// </summary>
        /// <param name="serverName">The name of the server to build</param>
        /// <param name="serverType">The type of server to build</param>
        /// <param name="serverVersion">The version of the server to build</param>
        /// <returns>A Task to allow the method to be awaited</returns>
        public async Task Build(string serverName, string serverType, string serverVersion)
        {
            OutputConsole.Clear();
            OutputConsole.AppendText(Logging.Logger.Info($"Starting the build for a new {serverType} {serverVersion} server named {serverName}.") + Environment.NewLine);

            // Creates the server section (if it already exists, it's deleted and recreated)
            Section allServersSection = FileSystem.GetFirstSectionNamed("servers");
            allServersSection.RemoveSection(serverName);
            Section serverSection = allServersSection.AddSection(serverName);

            OutputConsole.AppendText(Logging.Logger.Info($"Created a new {serverName} section.") + Environment.NewLine);

            // Checks if the server is already downloaded, and if it is, copy it and skip the download
            bool downloadsLookup = TryAddFromDownloads(serverName, serverVersion, serverType);

            if (!downloadsLookup)
            {
                // Gets the direct download link for the server jar based on the version and type
                ServerTypeMappingsFactory multiFactory = new ();
                string downloadLink = multiFactory.GetCacheContentsForType(serverType)[serverVersion];
                string directDownloadLink = await multiFactory.GetParserFor(serverType).GetServerDirectDownloadLink(serverVersion, downloadLink);
                OutputConsole.AppendText(Logging.Logger.Info($"Retrieved the resources for a new \"{serverType}.{serverVersion}\"") + Environment.NewLine);

                // Downloads the server jar into the server folder
                OutputConsole.AppendText(Logging.Logger.Info("Downloading the server.jar...") + Environment.NewLine);
                string downloadPath = Path.Combine(serverSection.SectionFullPath, "server.jar");
                await FileDownloader.DownloadFileAsync(downloadPath, directDownloadLink);
                CopyToDownloads(downloadPath, serverVersion, serverType);
            }
            
            // Gets the server.jar file path and installs the server
            string serverInstallerJar = serverSection.GetAllDocuments().FirstOrDefault(x => x.Contains("server") && x.EndsWith(".jar"));
            string serverJarPath = await InstallServer(serverInstallerJar);

            // Initialises the editor and updates the server settings file
            ServerEditor editor = new (serverSection);
            ServerInformation info = editor.GetServerInformation();

            // Updates the server information with critical information about the server
            info.Type = serverType;
            info.Version = serverVersion;
            info.ServerBackupsPath = serverSection.AddSection("backups/server").SectionFullPath;
            info.PlayerdataBackupsPath = serverSection.AddSection("backups/playerdata").SectionFullPath;
            info.JavaRuntimePath = NewServer.Instance.ComboBoxJavaVersion.Text;
            
            // Updates and flushes the buffers
            editor.UpdateBuffers(info.ToDictionary());
            editor.FlushBuffers();

            // Generates the EULA file (or agrees to it if it already exists, as a failsafe)
            if (GenerateEula(serverSection) == 1)
            {
                OutputConsole.AppendText(Logging.Logger.Error("Failed to generate or agree to the EULA file. You should *never* have reached this point, but here we are.") + Environment.NewLine);
                FileSystem.RemoveSection("servers/" + serverName);
                return; 
            }

            // Runs the server once and closes it, in order to create the server files.
            if (await FirstSetupRun(editor, serverJarPath) == 1)
            {
                FileSystem.RemoveSection("servers/" + serverName);
                return;
            }

            await ServerList.INSTANCE.AddServerToListAsync(editor);
            OutputConsole.SelectionColor = Color.LimeGreen;
            OutputConsole.AppendText(Logging.Logger.Info("Finished building the server.") + Environment.NewLine);
            OutputConsole.SelectionColor = Color.Black;
        }

        /// <summary>
        /// Runs the server once and closes it once it has been initialised. Deletes the world folder
        /// in the end.
        /// This method aims to initialise and build all of the server files in one go.
        /// </summary>
        /// <param name="serverJarPath">The path of the server file to run</param>
        /// <param name="editor">The ServerEditor to use with this run</param>
        /// <returns>A Task with a code letting the user know if an error happened</returns>
        protected virtual async Task<int> FirstSetupRun(ServerEditor editor, string serverJarPath)
        {
            // Creates a new process to run the server silently, and waits for it to finish.
            StartupArguments = StartupArguments.Replace("%SERVER_JAR%", serverJarPath);
            OutputConsole.AppendText(Logging.Logger.Info("Running the server silently... (This may happen more than once!)") + Environment.NewLine);

            // Gets the server section from the path of the jar being run, the runtime and creates the process
            ServerInformation info = editor.GetServerInformation();
            Process proc = ProcessUtils.CreateProcess($"\"{info.JavaRuntimePath}\\bin\\java\"", StartupArguments, Path.GetDirectoryName(serverJarPath));

            // Gets an available port starting on the one specified, and changes the properties file accordingly
            if (editor.HandlePortForServer() == 1)
            {
                ProcessErrorMessages("Could not find a port to start the server with! Please change the port in the server properties or free up ports to use.", proc);
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
            editor.ServerSection.RemoveSection("world"); // Finds the world folder and deletes it if it exists
            
            // The math here is because if nothing happened, it errored with no changes, so the code is -1
            // and we can simply return 1.
            if (TerminationCode * TerminationCode == 1) return 1;

            // Completes the run, resetting the termination code
            OutputConsole.AppendText(Logging.Logger.Info("Silent run completed.") + Environment.NewLine);
            TerminationCode = -1;
            return 0;
        }

        /// <summary>
        /// Accesses the given file and returns the section based on its parent folder.
        /// </summary>
        /// <param name="filepath">The filepath to get the section for</param>
        /// <returns>A Section object for the directory the file is located in</returns>
        protected Section GetSectionFromFile(string filepath)
        {
            List<string> directories = filepath.Split(Path.DirectorySeparatorChar).ToList();
            string serverName = directories[directories.IndexOf("servers") + 1];
            return FileSystem.GetFirstSectionNamed("servers/" + serverName);
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
            if (message.ToLower().Contains("preparing level")) ProcessUtils.KillProcessAndChildren(proc);
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
            if (message.Contains("already running on that port")) ProcessUtils.KillProcessAndChildren(proc);
        }

        /// <summary>
        /// Installs the server.jar file given the path to the server installer. When it finishes,
        /// return the path to the server.jar file.
        /// </summary>
        /// <param name="serverInstallerPath">The path to the installer</param>
        /// <returns>The path to the server.jar file used to run the server.</returns>
        protected abstract Task<string> InstallServer(string serverInstallerPath);

        /// <summary>
        /// Checks if the requested server version has already been downloaded and exists in the "downloads" cold
        /// storage, and if so, copies it to the server's directory, with the name "server.jar".
        /// </summary>
        /// <param name="serverName">The server name to use</param>
        /// <param name="serverVersion">The server version to look for</param>
        /// <param name="serverType">The server type to look for</param>
        /// <returns>Whether the operation was successful or not</returns>
        private bool TryAddFromDownloads(string serverName, string serverVersion, string serverType)
        {
            try
            {
                // If the server.jar file doesn't exist, return false
                Section downloadsSection = FileSystem.AddSection("downloads");
                string serverJarPath = Path.Combine(downloadsSection.SectionFullPath, $"{serverType}-{serverVersion}.jar");
                if (!File.Exists(serverJarPath)) return false;

                // If the server.jar file exists, copy it to the server's directory and return true
                File.Copy(serverJarPath, Path.Combine(FileSystem.GetFirstSectionNamed($"servers/{serverName}").SectionFullPath, "server.jar"), true);
                return true;
            }
            
            catch (IOException e)
            {
                Logging.Logger.Error(e);
                return false;
            }
        }

        /// <summary>
        /// Copies the server.jar file to the downloads cold storage, with the name formatted as "serverType-serverVersion.jar".
        /// </summary>
        /// <param name="filepath">The filepath to copy</param>
        /// <param name="serverVersion">The server version to register it under</param>
        /// <param name="serverType">The server type to register it under</param>
        private void CopyToDownloads(string filepath, string serverVersion, string serverType)
        {
            try
            {
                // Get the downloads section resources
                Section downloadsSection = FileSystem.AddSection("downloads");
                string serverJarPath = Path.Combine(downloadsSection.SectionFullPath, $"{serverType}-{serverVersion}.jar");
                
                // Copy the file to the downloads section
                File.Copy(filepath, serverJarPath, true);
            }
            catch (IOException e) { Logging.Logger.Error(e); }
        }
        
        /// <summary>
        /// Generates an agreed upon EULA file in order to allow the server to start.
        /// </summary>
        /// <param name="serverSection">The server section to generate the file into</param>
        private int GenerateEula(Section serverSection)
        {
            // Gets the eula.txt file path
            string eulaPath = Path.Combine(serverSection.SectionFullPath, "eula.txt");

            // If the file exists, agree to the eula instead.
            if (File.Exists(eulaPath)) return AgreeToEula(eulaPath);

            File.Create(eulaPath).Close();
            FileUtils.AppendToFile(eulaPath, "# This EULA file was generated by MCSM.");
            FileUtils.AppendToFile(eulaPath, "# By using MCSM, you automatically agree to the Minecraft TOS - https://aka.ms/MinecraftEULA");
            FileUtils.AppendToFile(eulaPath, "eula=true" );
            
            return 0;
        }

        /// <summary>
        /// Agrees to the eula by replacing the eula=false line in the file to eula=true.
        /// This should just be used as a failsafe to the eula generation
        /// </summary>
        /// <param name="eulaPath">The path to the eula.txt file</param>
        private int AgreeToEula(string eulaPath)
        {
            List<string> lines = FileUtils.ReadFromFile(eulaPath);

            // Replaces the eula=false line with eula=true
            int eulaIndex = lines.IndexOf("eula=false");
            if (eulaIndex == -1) return 1;

            lines[eulaIndex] = "eula=true";
            FileUtils.DumpToFile(eulaPath, lines);
            return 0;
        }

    }
}