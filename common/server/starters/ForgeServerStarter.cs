using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MCSMLauncher.common.background;
using MCSMLauncher.common.caches;
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
        /// <param name="editor">The ServerEditor instance to use</param>
        public override async Task Run(Section serverSection, ServerEditor editor)
        {
            string runBatFilepath = PathUtils.NormalizePath(serverSection.GetFirstDocumentNamed("run.bat"));

            // Makes sure the run.bat file exists and removes the "nogui" argument from it.
            if (runBatFilepath == null) throw new FileNotFoundException("run.bat file not found");
            FixRunFile(runBatFilepath);

            // Creates the process and starts it.
            Process proc = ProcessUtils.CreateProcess("cmd.exe", $"/c {runBatFilepath}", serverSection.SectionFullPath);
            proc.OutputDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);
            proc.ErrorDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);

            // Finds the port and IP to start the server with, and starts the server.
            await StartServer(serverSection, proc, editor);
        }

        /// <summary>
        /// Removes the "nogui" argument from the run.bat file, as it causes the server to be hidden.
        /// </summary>
        /// <param name="path">The path to the run.bat filepath</param>
        private void FixRunFile(string path)
        {
            // Reads the run.bat file.
            List<string> lines = FileUtils.ReadFromFile(path);
            
            // Goes through each line, and removes the "nogui" argument from it.
            for (int i = 0; i < lines.Count; i++)
                lines[i] = lines[i].Replace("nogui", "").TrimEnd();
            
            FileUtils.DumpToFile(path, lines);
        }
    }
}