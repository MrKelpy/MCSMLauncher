using System.IO;
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
        public override void Run(Section serverSection)
        {
            var runBatFilepath = PathUtils.NormalizePath(serverSection.GetFirstDocumentNamed("run.bat"));
            var serverPropertiesPath = serverSection.GetFirstDocumentNamed("server.properties");

            if (runBatFilepath == null) throw new FileNotFoundException("run.bat file not found");
            if (serverPropertiesPath == null) throw new FileNotFoundException("server.properties file not found");

            // Removes the "nogui" argument from the run.bat file.
            var lines = FileUtils.ReadFromFile(runBatFilepath);
            for (var index = 0; index < lines.Count; index++)
                if (lines[index].Contains("nogui"))
                    lines[index] = lines[index].Replace("nogui", "").TrimEnd();

            FileUtils.DumpToFile(runBatFilepath, lines);

            // Gets the server information from the server_settings.xml file or creates a new one with minimal information.
            var info = serverSection.GetFirstDocumentNamed("server_settings.xml") is var settings && settings != null
                ? XMLUtils.DeserializeFromFile<ServerInformation>(settings)
                : new ServerInformation().GetMinimalInformation(serverSection);

            // Builds the startup arguments for the server.
            StartupArguments = StartupArguments.Replace("%SERVER_JAR%", PathUtils.NormalizePath(runBatFilepath))
                .Replace("%RAM_ARGUMENTS%", "-Xmx" + info.Ram + "M -Xms" + info.Ram + "M");

            // Creates the process and starts it.
            var proc = ProcessUtils.CreateProcess("cmd.exe", $"/c {runBatFilepath}", serverSection.SectionFullPath);
            proc.OutputDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);
            proc.ErrorDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);

            // Gets an available port starting on the one specified, and changes the properties file accordingly
            if (new ServerEditor(serverSection).HandlePortForServer() == 1)
            {
                ProcessErrorMessages(
                    "Could not find a port to start the server with! Please change the port in the server properties or free up ports to use.",
                    proc);
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