using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
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
        public override async Task Run(Section serverSection)
        {
            string runBatFilepath = PathUtils.NormalizePath(serverSection.GetFirstDocumentNamed("run.bat"));
            ServerEditor editor = GlobalEditorsCache.INSTANCE.GetOrCreate(serverSection);
            ServerInformation info = editor.GetServerInformation();
            
            // Makes sure the run.bat file exists, and gets the command from it.
            if (runBatFilepath == null) throw new FileNotFoundException("run.bat file not found");
            string startupArguments = GetRunCommandArguments(runBatFilepath, editor);
            
            // Creates the process through the extracted arguments
            Process proc = ProcessUtils.CreateProcess($"\"{info.JavaRuntimePath}\\bin\\java\"", startupArguments, serverSection.SectionFullPath);
            proc.OutputDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);
            proc.ErrorDataReceived += (sender, e) => ProcessMergedData(sender, e, proc);

            // Finds the port and IP to start the server with, and starts the server.
            await StartServer(serverSection, proc, editor);
        }

        /// <summary>
        /// Gets the run command template from the run.bat file and creates the run command string
        /// from it. 
        /// </summary>
        /// <param name="path">The path to the run.bat filepath</param>
        /// <param name="editor">The editor to use alongside the </param>
        private string GetRunCommandArguments(string path, ServerEditor editor)
        {
            // Reads the run.bat file and gets the server info
            List<string> lines = FileUtils.ReadFromFile(path);
            ServerInformation info = editor.GetServerInformation();
            
            // Gets the template line from the run.bat file.
            string templateLine = lines.Find(line => line.StartsWith("%JAVA%"));
            if (templateLine == null) throw new InvalidDataException("run.bat file is invalid");
            
            // returns the template line key arguments with the actual arguments.
            return templateLine.Replace("%JAVA%", "")
                .Replace("%RAM%", info.Ram.ToString())
                .Replace("nogui", "")
                .Trim();
        }
    }
}