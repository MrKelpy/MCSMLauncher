using System.Threading.Tasks;
using mcsm.common.handlers;
using mcsm.common.server.builders.abstraction;

namespace mcsm.common.server.builders
{
    /// <summary>
    /// This class implements the server building methods for the vanilla releases and snapshots.
    /// </summary>
    public class McvBuilder : AbstractServerBuilder
    {
        /// <summary>
        /// Main constructor for the ForgeBuilder class. Defines the start-up arguments for the server.
        /// </summary>
        /// <param name="outputHandler">The output system to use while logging the messages.</param>
        public McvBuilder(MessageProcessingOutputHandler outputHandler) : base("-jar %SERVER_JAR% nogui", outputHandler)
        {
        }

        /// <summary>
        /// Installs the server.jar file given the path to the server installer. When it finishes,
        /// return the path to the server.jar file.
        /// </summary>
        /// <param name="serverInstallerPath">The path to the installer</param>
        /// <returns>The path to the server.jar file used to run the server.</returns>
        protected override Task<string> InstallServer(string serverInstallerPath)
        {
            return Task.Run(() => serverInstallerPath);
        }
    }
}