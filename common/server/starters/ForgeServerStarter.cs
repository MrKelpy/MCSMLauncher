using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MCSMLauncher.common.background;
using MCSMLauncher.common.models;
using MCSMLauncher.common.server.starters.abstraction;
using MCSMLauncher.utils;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;

namespace MCSMLauncher.common.server.starters
{
    /// <summary>
    /// This class handles everything related to starting Forge servers.
    /// </summary>
    public class ForgeServerStarter : AbstractServerStarter
    {
        /// <summary>
        /// Main constructor for the ForgeServerStarter class. Defines the start-up arguments for the server, as well
        /// as the "other arguments" that are passed to the server.
        /// </summary>
        public ForgeServerStarter() : base(" ", "-jar %RAM_ARGUMENTS% \"%SERVER_JAR%\"")
        {
        }

        /// <summary>
        /// Overriden method from the AbstractServerStarter class. Runs the server with the given startup arguments, under
        /// the "run.bat" file.
        /// </summary>
        /// <param name="serverSection">The section of the server to be run</param>
        public override async Task Run(Section serverSection)
        {
            string runBatFilepath = PathUtils.NormalizePath(serverSection.GetFirstDocumentNamed("run.bat"));
            string serverPropertiesPath = serverSection.GetFirstDocumentNamed("server.properties");
            string settings = serverSection.GetFirstDocumentNamed("server_settings.xml");

            if (runBatFilepath == null) throw new FileNotFoundException("run.bat file not found");
            if (serverPropertiesPath == null) throw new FileNotFoundException("server.properties file not found");

            // Removes the "nogui" argument from the run.bat file.
            List<string> lines = FileUtils.ReadFromFile(runBatFilepath);
            for (int index = 0; index < lines.Count; index++)
            {
                const string noguiArgument = "nogui";
                if (lines[index].Contains(noguiArgument))
                    lines[index] = lines[index].Replace(noguiArgument, "").TrimEnd();
            }

            FileUtils.DumpToFile(runBatFilepath, lines);

            // Gets the server information from the server_settings.xml file or creates a new one with minimal information.
            ServerInformation info = ServerEditor.GetServerInformation(serverSection);
            
            // Builds the startup arguments for the server.
            StartupArguments = StartupArguments
                .Replace("%SERVER_JAR%", PathUtils.NormalizePath(runBatFilepath))
                .Replace("%RAM_ARGUMENTS%", "-Xmx" + info.Ram + "M -Xms" + info.Ram + "M");

            // Creates the process and starts it.
            Process proc = ProcessUtils.CreateProcess("cmd.exe", $"/c {runBatFilepath}", serverSection.SectionFullPath);
            proc.OutputDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);
            proc.ErrorDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);

            // Finds the port and IP to start the server with, and starts the server.
            await StartServer(serverSection, proc, info);
        }
    }
}