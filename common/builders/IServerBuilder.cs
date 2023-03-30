using System.Threading.Tasks;

namespace MCSMLauncher.common.builders
{
    /// <summary>
    /// This interface implements contracts for the methods that should be implemented in each
    /// ServerBuilder.
    /// </summary>
    public interface IServerBuilder
    {
        /// <summary>
        /// Main method for the server building process. Starts off all the operations.
        /// </summary>
        /// <param name="serverName">The name of the server to build</param>
        /// <param name="serverType">The type of server to build</param>
        /// <param name="serverVersion">The version of the server to build</param>
        /// <returns>A Task to allow the method to be awaited</returns>
        Task Build(string serverName, string serverType, string serverVersion);

        /// <summary>
        /// Runs the server once and closes it once it has been initialised. Deletes the world folder
        /// in the end.
        /// This method aims to initialise and build all of the server files in one go.
        /// </summary>
        /// <param name="serverPath">The path of the server file to run</param>
        /// <returns>A Task to allow the method to be awaited</returns>
        Task RunAndCloseSilently(string serverPath);

        /// <summary>
        /// Agrees to the eula by replacing the eula=false line in the file to eula=true.
        /// </summary>
        /// <param name="eulaPath">The path to the eula.txt file</param>
        void AgreeToEula(string eulaPath);
    }
}