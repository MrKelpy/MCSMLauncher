﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using MCSMLauncher.common.models;
using MCSMLauncher.common.server.builders.abstraction;
using MCSMLauncher.extensions;
using MCSMLauncher.gui;
using MCSMLauncher.utils;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.common.server.builders
{
    /// <summary>
    /// This class implements the server building methods for the forge releases.
    /// </summary>
    public class ForgeBuilder : AbstractServerBuilder
    {
        /// <summary>
        /// Main constructor for the ForgeBuilder class. Defines the start-up arguments for the server.
        /// </summary>
        public ForgeBuilder() : base("-jar -Xmx1024M -Xms1024M %SERVER_JAR% nogui")
        {
        }

        /// <summary>
        /// Installs the server.jar file given the path to the server installer. When it finishes,
        /// return the path to the server.jar file.
        /// </summary>
        /// <param name="serverInstallerPath">The path to the installer</param>
        /// <returns>The path to the server.jar file used to run the server.</returns>
        protected override async Task<string> InstallServer(string serverInstallerPath)
        {
            // Gets the server section from the path of the jar being run, and removes the installer from it
            List<string> directories = serverInstallerPath.Split(Path.DirectorySeparatorChar).ToList();
            string serverName = directories[directories.IndexOf("servers") + 1];
            Section serverSection = FileSystem.GetFirstSectionNamed("servers/" + serverName);

            // Creates the process that will build the server
            Process forgeBuildingProcess =
                ProcessUtils.CreateProcess($"\"{NewServer.INSTANCE.ComboBoxJavaVersion.Text}\\bin\\java\"",
                    $" -jar {serverInstallerPath} --installServer", serverSection.SectionFullPath);

            // Set the output and error data handlers
            forgeBuildingProcess.OutputDataReceived +=
                (sender, e) => this.ProcessMergedData(sender, e, forgeBuildingProcess);
            forgeBuildingProcess.ErrorDataReceived +=
                (sender, e) => this.ProcessMergedData(sender, e, forgeBuildingProcess);
            this.TerminationCode = 0;

            // Start the process
            forgeBuildingProcess.Start();
            forgeBuildingProcess.BeginOutputReadLine();
            forgeBuildingProcess.BeginErrorReadLine();
            await forgeBuildingProcess.WaitForExitAsync();

            // Returns the path to the server.jar file, which in this case, is a forge file.
            serverSection.RemoveDocument("server.jar");
            return serverSection.GetAllDocuments().First(x =>
                Path.GetFileName(x).Contains("forge") && Path.GetFileName(x).EndsWith("jar"));
        }

        /// <summary>
        /// Due to the stupidity of early Minecraft logging, capture the STDERR and STDOUT in this method,
        /// and separate them by WARN, ERROR, and INFO messages, calling the appropriate methods.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        /// <param name="proc">The running process of the server</param>
        protected override void ProcessMergedData(object sender, DataReceivedEventArgs e, Process proc)
        {
            if (e.Data == null || e.Data.Trim().Equals(string.Empty)) return;

            if (e.Data.Contains("ERROR") || e.Data.StartsWith("Exception"))
                this.ProcessErrorMessages(e.Data, proc);
            else if (e.Data.Contains("WARN"))
                this.ProcessWarningMessages(e.Data, proc);
            else if (e.Data.Contains("INFO") || e.Data.Contains("LOADING"))
                this.ProcessInfoMessages(e.Data, proc);
            else
                this.ProcessOtherMessages(e.Data, proc);
        }

        /// <summary>
        /// Processes any INFO messages received from the server jar.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>0 - The server.jar fired a normal info message</terminationCode>
        protected override void ProcessInfoMessages(string message, Process proc)
        {
            this.TerminationCode = this.TerminationCode != 1 ? 0 : 1;

            if (message.ToLower().Contains("preparing level") || message.ToLower().Contains("agree to the eula"))
                proc.KillProcessAndChildren();

            Logging.LOGGER.Info(message);
        }

        /// <summary>
        /// Runs the server once and closes it once it has been initialised. Deletes the world folder
        /// in the end.
        /// This method aims to initialise and build all of the server files in one go.
        /// </summary>
        /// <param name="serverJarPath">The path of the server file to run</param>
        /// <returns>A Task with a code letting the user know if an error happened</returns>
        protected override async Task<int> FirstSetupRun(string serverJarPath)
        {
            // Due to how forge works, we need to generate a run.bat file to run the forge.
            Section serverSection = this.GetSectionFromFile(serverJarPath);
            serverSection.AddDocument("server.properties"); // Adds the server properties just in case

            // Gets the java runtime and creates the run command from it
            ServerInformation info = ServerEditor.GetServerInformation(serverSection);
            string runCommand = $"\"{info.JavaRuntimePath}\\bin\\java\" {this.StartupArguments}";

            // Creates the run.bat file if it doesn't already exist, with simple running params
            string runFilepath = Path.Combine(serverSection.SectionFullPath, "run.bat");

            if (!File.Exists(runFilepath))
                File.WriteAllText(runFilepath, runCommand.Replace("%SERVER_JAR%", serverJarPath));

            // Gets the run.bat file and adds nogui to the end of the java command
            List<string> lines = FileUtils.ReadFromFile(runFilepath);
            int index = lines.IndexOf(lines.FirstOrDefault(x => x.StartsWith("java")));
            int lineIndex = index != -1 ? index : 0;

            if (!lines[lineIndex].Contains("nogui"))
            {
                // Removes the pause statement if there is one
                if (lines.Count > 0 && lines[lines.Count - 1] == "pause") lines.RemoveAt(lines.Count - 1);

                // If the bat file was generated, there's a '%*' that needs to be replaced instead of just
                // adding nogui to the end.
                lines[lineIndex] = lines[lineIndex].Contains("%*")
                    ? lines[lineIndex].Replace("%*", "nogui %*")
                    : lines[lineIndex] + " nogui";

                lines[lineIndex] = lines[lineIndex].Replace("@user_jvm_args.txt", "-Xms1024M -Xmx1024M");
                FileUtils.DumpToFile(runFilepath, lines);
            }

            // Creates the process to be run
            Process proc = ProcessUtils.CreateProcess("cmd.exe", $"/c {runFilepath}", serverSection.SectionFullPath);

            // Gets an available port starting on the one specified, and changes the properties file accordingly
            ServerEditor editor = new ServerEditor(serverSection);
            if (editor.HandlePortForServer() == 1)
            {
                this.ProcessErrorMessages("Could not find a port to start the server with! Please change the port in the server properties or free up ports to use.", proc);
                return 1;
            }

            Dictionary<string, string> properties = editor.LoadProperties();
            properties["level-type"] = @"minecraft\:normal";
            editor.DumpToProperties(properties);

            // Handles the processing of the STDOUT and STDERR outputs, changing the termination code accordingly.
            proc.OutputDataReceived += (sender, e) => this.ProcessMergedData(sender, e, proc);
            proc.ErrorDataReceived += (sender, e) => this.ProcessMergedData(sender, e, proc);

            // Waits for the termination of the process by the OutputDataReceived event or ErrorDataReceived event.
            proc.Start();
            proc.BeginErrorReadLine();
            proc.BeginOutputReadLine();
            await proc.WaitForExitAsync();

            // Disposes of the process and checks if the termination code is 1 or -1. If so, return 1.
            proc.Dispose();
            serverSection.RemoveSection("world"); // Finds the world folder and deletes it if it exists
            
            // The math here is because if nothing happened, it errored with no changes, so the code is -1
            // and we can simply return 1.
            if (this.TerminationCode * this.TerminationCode == 1) return 1;

            // Completes the run, resetting the termination code
            this.OutputConsole.AppendText(Logging.LOGGER.Info("Silent run completed.") + Environment.NewLine);
            this.TerminationCode = -1;
            return 0;
        }
    }
}