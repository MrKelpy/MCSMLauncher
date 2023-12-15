using MCSMLauncher.common;

namespace MCSMLauncher.api.server
{
    /// <summary>
    /// This class is responsible for providing an API for various types of server-related operations. <br></br>
    /// It will not directly implement any endpoints, but will instead delegate to other classes.
    /// </summary>
    public class ServerAPI
    {

        /// <returns>
        /// Returns an instance of ServerBuilding so that the user can build the selected server.
        /// </returns>
        /// <param name="serverName">The name of the server to be built</param>
        /// <param name="serverType">The server type to create the server as</param>
        /// <param name="serverVersion">The server version to build the server on</param>
        public ServerBuilder Builder(string serverName, string serverType, string serverVersion)
            => new (serverName, serverType, serverVersion);
        
        /// <returns>
        /// Returns an instance of ServerStarting so that the user can start the selected server.
        /// </returns>
        /// <param name="name">The server name to use in order to locate the server to start</param>
        public ServerStarter Starter(string name) => new (name);
        
        /// <returns>
        /// Returns an instance of ServerEditing so that the user can edit the selected server.
        /// </returns>
        /// <param name="serverName">The name of the server to edit</param>
        public ServerEditing Editor(string serverName) => new (serverName);
        
        /// <returns>
        /// Returns an instance of ServerInteractions so that the user can interact with the selected server.
        /// </returns>
        /// <param name="serverName">The server name of the server to interact with</param>
        public ServerInteractions Interactions(string serverName) => new (serverName);

    }
}