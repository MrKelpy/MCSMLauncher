using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common.factories;
using MCSMLauncher.common.models;
using MCSMLauncher.extensions;
using MCSMLauncher.gui;
using MCSMLauncher.requests.content;
using MCSMLauncher.utils;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.common.builders.abstraction
{
    /// <summary>
    /// This interface implements contracts for the methods that should be implemented in each
    /// ServerBuilder.
    /// </summary>
    [SuppressMessage("ReSharper", "StringLiteralTypo")]
    [SuppressMessage("ReSharper", "AccessToDisposedClosure")]
    public abstract class AbstractServerBuilder
    {
        
        /// <summary>
        /// The console object to update with the logs.
        /// </summary>
        protected RichTextBox OutputConsole => NewServer.INSTANCE.RichTextBoxConsoleOutput;

        /// <summary>
        /// The termination code for a server execution, to be used by the processing events
        /// </summary>
        protected int TerminationCode { get; set; } = -1;
        
        /// <summary>
        /// The startup arguments to be used by the methods that start up the server.
        /// Variables:
        ///     > %SERVER_JAR%: The path to the server.jar file
        /// </summary>
        private string StartupArguments { get; set; }

        /// <summary>
        /// Main constructor for the AbstractServerBuilder class, sets the startup arguments
        /// to be used in the server startups, through the child classes.
        /// </summary>
        /// <param name="startupArguments">The start</param>
        protected AbstractServerBuilder(string startupArguments) => this.StartupArguments = startupArguments;
        
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
            RichTextBox console = NewServer.INSTANCE.RichTextBoxConsoleOutput;
            console.Clear();

            // Ensures that there's a clean section for the server to be built on
            console.AppendText(Logging.LOGGER.Info($"Starting the build for a new {serverType} {serverVersion} server named {serverName}.") + Environment.NewLine);
            
            Section serversSection = FileSystem.GetFirstSectionNamed("servers");
            serversSection.RemoveSection(serverName);
            Section currentServerSection = serversSection.AddSection(serverName);
            
            console.AppendText(Logging.LOGGER.Info($"Created a new {serverName} section.") + Environment.NewLine);

            // Gets the direct download link for the server jar based on the version and type
            ServerTypeMappingsFactory multiFactory = new ServerTypeMappingsFactory();
            string downloadLink = multiFactory.GetCacheContentsForType(serverType)[serverVersion];
            string directDownloadLink = await multiFactory.GetParserFor(serverType).GetServerDirectDownloadLink(downloadLink);
            console.AppendText(Logging.LOGGER.Info($"Retrieved the resources for a new \"{serverType}.{serverVersion}\"") + Environment.NewLine);
            
            // Downloads the server jar into the server folder
            console.AppendText(Logging.LOGGER.Info($"Downloading the server.jar...") + Environment.NewLine);
            await FileDownloader.DownloadFileAsync(Path.Combine(currentServerSection.SectionFullPath, "server.jar"), directDownloadLink);
            
            // Gets the server.jar file path and installs the server
            Section serverSection = FileSystem.GetFirstSectionNamed("servers/" + serverName);
            string serverInstallerJar = serverSection.GetAllDocuments().FirstOrDefault(x => x.Contains("server") && x.EndsWith(".jar"));
            string serverJarPath = await InstallServer(serverInstallerJar);
            
            // Creates the server_settings.xml file, marks it as hidden and serializes the ServerInformation object into it
            string serverSettingsPath = Path.Combine(serverSection.SectionFullPath, "server_settings.xml");
            ServerInformation serverInformation = new ServerInformation()
            {
                Type = serverType,
                Version = serverVersion,
                ServerBackupsPath = serverSection.AddSection("backups/server").SectionFullPath,
                PlayerdataBackupsPath = serverSection.AddSection("backups/playerdata").SectionFullPath,
                Port = 25565,
                Ram = 1024,
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
            string eulaPath = serverSection.GetFirstDocumentNamed("eula.txt");

            if (File.Exists(eulaPath))
            {
                console.AppendText(Logging.LOGGER.Info($"Agreeing to the EULA") + Environment.NewLine);
                this.AgreeToEula(eulaPath);
            }
            
            // Runs the server once and closes it, in order to create the rest of the server files.
            // Since this is a proper run now, we've got to check for an available port.
            if (await RunAndCloseSilently(serverJarPath) == 1)
            {
                FileSystem.RemoveSection("servers/" + serverName);
                return;
            }

            await ServerList.INSTANCE.AddServerToListAsync(serverSection);
            console.AppendText(Logging.LOGGER.Info($"Finished building the server.") + Environment.NewLine);
        }

        /// <summary>
        /// Agrees to the eula by replacing the eula=false line in the file to eula=true.
        /// </summary>
        /// <param name="eulaPath">The path to the eula.txt file</param>
        private void AgreeToEula(string eulaPath)
        {
            List<string> lines = FileUtils.ReadFromFile(eulaPath);
            
            // Replaces the eula=false line with eula=true
            int eulaIndex = lines.IndexOf("eula=false");
            if (eulaIndex == -1) return;
            
            lines[eulaIndex] = "eula=true";
            FileUtils.DumpToFile(eulaPath, lines);
        }
        
        /// <summary>
        /// Creates a Java jar process, redirecting its STDOUT and STDERR to process it.
        /// </summary>
        /// <param name="java">The java version path to run</param>
        /// <param name="args">The java args to run the jar with</param>
        /// <param name="workingDirectory">The working directory of the process</param>
        /// <returns>The process started</returns>
        private static Process CreateProcess(string java, string args, string workingDirectory = null)
        {
            // Creates a new process with the command line arguments to run the command, in a hidden
            // window.
            Process proc = new Process();
            ProcessStartInfo startInfo = new ProcessStartInfo 
            { 
                WindowStyle = ProcessWindowStyle.Hidden,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                CreateNoWindow = true,
                UseShellExecute = false,
                WorkingDirectory = workingDirectory ?? Directory.GetCurrentDirectory(),
                FileName = java,
                Arguments = args
            };
            
            // Assigns the startInfo to the process and starts it.
            proc.StartInfo = startInfo;
            return proc;
        }

        /// <summary>
        /// Runs the server once and closes it once it has been initialised. Deletes the world folder
        /// in the end.
        /// This method aims to initialise and build all of the server files in one go.
        /// </summary>
        /// <param name="serverJarPath">The path of the server file to run</param>
        /// <returns>A Task with a code letting the user know if an error happened</returns>
        private async Task<int> RunAndCloseSilently(string serverJarPath)
        {
            // Creates a new process to run the server silently, and waits for it to finish.
            StartupArguments = StartupArguments.Replace("%SERVER_JAR%", serverJarPath);
            Process proc = CreateProcess($"\"{NewServer.INSTANCE.ComboBoxJavaVersion.Text}\\bin\\java\"", StartupArguments, Path.GetDirectoryName(serverJarPath));
            OutputConsole.AppendText(Logging.LOGGER.Info("Running the server silently... (This may happen more than once!)") + Environment.NewLine);
            
            // Gets the server section from the path of the jar being run
            List<string> directories = serverJarPath.Split(Path.DirectorySeparatorChar).ToList();
            string serverName = directories[directories.IndexOf("servers") + 1];
            Section serverSection = FileSystem.GetFirstSectionNamed("servers/" + serverName);
            
            // Gets an available port starting on the one specified, and changes the properties file accordingly
            if (serverSection.GetFirstDocumentNamed("server.properties") != null)
            {
                ServerEditPrompt editServer = new ServerEditPrompt(serverSection);
                Dictionary<string, string> properties = editServer.PropertiesToDictionary();
                int port = properties.ContainsKey("server-port") ? int.Parse(properties["server-port"]) : 25565;

                // Gets an available port starting on the one specified. If it's -1, it means that there are no available ports.
                int availablePort = NetworkUtils.GetNextAvailablePort(port);
                if (availablePort == -1)
                {
                    ProcessErrorMessages("Could not find a port to start the server with! Please change the port in the server properties or free up ports to use.", proc);
                    return 1;
                }

                properties["server-port"] = availablePort.ToString();
                editServer.LoadToProperties(properties);
            }

            // Handles the processing of the STDOUT and STDERR outputs, changing the termination code accordingly.
            proc.OutputDataReceived += (sender, e) => this.ProcessMergedData(sender, e, proc);
            proc.ErrorDataReceived += (sender, e) => this.ProcessMergedData(sender, e, proc);

            // Waits for the termination of the process by the OutputDataReceived event or ErrorDataReceived event.
            proc.Start();
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            await proc.WaitForExitAsync();
            
            // Disposes of the process and checks if the termination code is 1. If so, return 1.
            proc.Dispose();
            serverSection.RemoveSection("world");  // Finds the world folder and deletes it if it exists
            if (TerminationCode * TerminationCode == 1) return 1;
            
            // Completes the run, resetting the termination code
            OutputConsole.AppendText(Logging.LOGGER.Info("Silent run completed.") + Environment.NewLine);
            TerminationCode = -1;
            return 0;
        }

        /// <summary>
        /// Due to the stupidity of early Minecraft logging, capture the STDERR and STDOUT in this method,
        /// and separate them by WARN, ERROR, and INFO messages, calling the appropriate methods.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        /// <param name="proc">The running process of the server</param>
        private void ProcessMergedData(object sender, DataReceivedEventArgs e, Process proc)
        {
            if (e.Data == null || e.Data.Trim().Equals(string.Empty)) return;
            Match matches = Regex.Match(e.Data.Trim(), @"^(?:\[[^\]]+\] \[[^\]]+\]: |[\d-]+ [\d:]+ \[[^\]]+\] )(.+)$", RegexOptions.Multiline);

            try
            {
                string typeSection = matches.Groups[0].Captures[0].Value;
                string message = matches.Groups[1].Captures[0].Value;
                
                if (typeSection.Contains("INFO")) ProcessInfoMessages(message, proc);
                if (typeSection.Contains("WARN")) ProcessWarningMessages(message, proc);
                if (typeSection.Contains("ERROR")) ProcessErrorMessages(message, proc);
                return;

            } catch (ArgumentOutOfRangeException) { }
            
            ProcessOtherMessages(e.Data, proc);
        }

        /// <summary>
        /// Processes any INFO messages received from the server jar.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>0 - The server.jar fired a normal info message</terminationCode>
        protected void ProcessInfoMessages(string message, Process proc)
        {
            TerminationCode = TerminationCode != 1 ? 0 : 1;
            if (message.ToLower().Contains("preparing level")) proc.Kill();
            
            Logging.LOGGER.Info(message);
        }

        /// <summary>
        /// Processes any ERROR messages received from the server jar.
        /// Since we might be updating the console from another thread, we're just going to invoke everything
        /// and that's that.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>1 - The server.jar fired an error. If fired last, stop the build.</terminationCode>
        protected void ProcessErrorMessages(string message, Process proc)
        {
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.SelectionColor = Color.Firebrick; }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.AppendText(Logging.LOGGER.Error("[ERROR] " + message) + Environment.NewLine); }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.SelectionColor = Color.Black; }));
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
        protected void ProcessWarningMessages(string message, Process proc)
        {
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.SelectionColor = Color.OrangeRed; }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.AppendText(Logging.LOGGER.Warn("[WARN] " + message) + Environment.NewLine); }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.SelectionColor = Color.Black; }));
            TerminationCode = TerminationCode != 1 ? 2 : 1;
            
            if (message.Contains("already running on that port")) proc.Kill();
        }
        
        /// <summary>
        /// Processes any undifferentiated messages received from the server jar.
        /// Since we might be updating the console from another thread, we're just going to invoke everything
        /// and that's that.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>2 - The server.jar fired a warning</terminationCode>
        protected void ProcessOtherMessages(string message, Process proc)
        {
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.SelectionColor = Color.Gray; }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.AppendText(Logging.LOGGER.Warn(message) + Environment.NewLine); }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.SelectionColor = Color.Black; }));
            TerminationCode = TerminationCode != 1 && !message.ToLower().Contains("error") ? 3 : 1;
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