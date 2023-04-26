using MCSMLauncher.common.server.starters.abstraction;

namespace MCSMLauncher.common.server.starters
{
    /// <summary>
    /// This class handles everything related to starting vanilla servers.
    /// </summary>
    public class MCVServerStarter : AbstractServerStarter
    {
        /// <summary>
        /// Main constructor for the MCVServerStarter class. Defines the start-up arguments for the server, aswell
        /// as the "other arguments" that are passed to the server.
        /// In this case, there isn't much to do. The server is always called server.jar, and the arguments are
        /// obtained through the server.xml file.
        /// </summary>
        public MCVServerStarter() : base(" ", "-jar %RAM_ARGUMENTS% \"%SERVER_JAR%\"") { }
    }
}