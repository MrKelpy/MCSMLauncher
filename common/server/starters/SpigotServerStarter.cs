﻿using MCSMLauncher.common.handlers;
using MCSMLauncher.common.server.starters.abstraction;

namespace MCSMLauncher.common.server.starters
{
    /// <summary>
    /// This class handles everything related to starting spigot servers.
    /// </summary>
    public class SpigotServerStarter : AbstractServerStarter
    {
        /// <summary>
        /// Main constructor for the SpigotServerStarter class. Defines the start-up arguments for the server, as well
        /// as the "other arguments" that are passed to the server.
        /// </summary>
        /// <param name="outputHandler">The output system to use while logging the messages.</param>
        public SpigotServerStarter(MessageProcessingOutputHandler outputHandler) : base(" ",
            "-DIReallyKnowWhatIAmDoingISwear=true -jar %RAM_ARGUMENTS% \"%SERVER_JAR%\"", outputHandler)
        {
        }
    }
}