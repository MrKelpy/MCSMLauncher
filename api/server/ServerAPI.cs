using MCSMLauncher.common;

namespace MCSMLauncher.api.server
{
    /// <summary>
    /// This class is responsible for providing an API for various types of server-related operations. <br></br>
    /// It will not directly implement any endpoints, but will instead delegate to other classes.
    /// </summary>
    public class ServerAPI
    {

        /// <summary>
        /// Returns an instance of ServerBuilding so that the user can build the selected server.
        /// </summary>
        /// <param name="serverName">The name of the server to be built</param>
        /// <param name="serverType">The server type to create the server as</param>
        /// <param name="serverVersion">The server version to build the server on</param>
        /// <returns>The ServerBuilding instance</returns>
        public ServerBuilder Builder(string serverName, string serverType, string serverVersion)
            => new (serverName, serverType, serverVersion);
        
        /// <summary>
        /// Returns an instance of ServerStarting so that the user can start the selected server.
        /// </summary>
        /// <param name="name">The server name to use in order to locate the server to start</param>
        /// <returns>The ServerStarting instance</returns>
        public ServerStarter Starter(string name) => new (name);
        
        /// <summary>
        /// Returns an instance of ServerEditing so that the user can edit the selected server.
        /// </summary>
        /// <param name="serverName">The name of the server to edit</param>
        /// <returns>The ServerEditing instance</returns>
        public ServerEditing Editor(string serverName) => new (serverName);

    }
}