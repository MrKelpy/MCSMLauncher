using System.Threading.Tasks;
using MCSMLauncher.common.server.builders.abstraction;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.common.server.builders
{
    /// <summary>
    /// This class implements the server building methods for the vanilla releases and snapshots. 
    /// </summary>
    public class MCVBuilder : AbstractServerBuilder
    {
        /// <summary>
        /// Installs the server.jar file given the path to the server installer. When it finishes,
        /// return the path to the server.jar file.
        /// </summary>
        /// <param name="serverInstallerPath">The path to the installer</param>
        /// <returns>The path to the server.jar file used to run the server.</returns>
        protected override Task<string> InstallServer(string serverInstallerPath) => 
            Task.Run(() => serverInstallerPath);

        /// <summary>
        /// Main constructor for the ForgeBuilder class. Defines the start-up arguments for the server.
        /// </summary>
        public MCVBuilder() : base("-jar %SERVER_JAR% nogui") { }
    }
}