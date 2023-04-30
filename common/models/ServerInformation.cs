using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using PgpsUtilsAEFC.common;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.common.models
{
    /// <summary>
    /// This is a serializable class that acts as a model to store information about a server, and
    /// is able to be serialized to XML.
    /// </summary>
    [SuppressMessage("ReSharper", "CommentTypo")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    [Serializable]
    public class ServerInformation
    {
        /// <summary>
        /// The version of the server.
        /// </summary>
        public string Version { get; set; }
        
        /// <summary>
        /// The type of the server.
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// The amount of RAM to allocate to the server.
        /// </summary>
        public int Ram { get; set; } = 1024;

        /// <summary>
        /// The base port to try to use for the server.
        /// </summary>
        public int Port { get; set; } = 25565;
        
        /// <summary>
        /// The path to the directory where the backups should be stored at.
        /// </summary>
        public string ServerBackupsPath { get; set; }
        
        /// <summary>
        /// The path to the directory where the playerdata backups should be stored at.
        /// </summary>
        public string PlayerdataBackupsPath { get; set; }

        /// <summary>
        /// Whether or not to create server backups whilst running the server
        /// </summary>
        public bool ServerBackupsOn { get; set; } = true;

        /// <summary>
        /// Whether or not to create playerdata backups whilst running the server
        /// </summary>
        public bool PlayerdataBackupsOn { get; set; } = true;

        /// <summary>
        /// The path to the java runtime to use for the server.
        /// </summary>
        public string JavaRuntimePath { get; set; } = "java";
        
        /// <summary>
        /// The currently running server process. If the server is not running, this will be -1.
        /// This is used to check if the server is currently running or not, and is only meant to be
        /// interacted with programatically.
        /// </summary>
        public int CurrentServerProcessID { get; set; } = -1;
        
        /// <summary>
        /// Empty constructor for the ServerInformation class. This is required for serialization.
        /// </summary>
        public ServerInformation() { }

        /// <summary>
        /// Creates a new ServerInformation object with the bare minimum information required to run a server.
        /// </summary>
        /// <param name="serverSection">The server section to get the information from</param>
        /// <returns>A ServerInformation instance of an unknown server with the minimal information required to run it.</returns>
        public ServerInformation GetMinimalInformation(Section serverSection)
        {
            return new ServerInformation
            {
                Type = "unknown",
                Version = "??.??.??",
                ServerBackupsPath = serverSection.AddSection("backups/server").SectionFullPath,
                PlayerdataBackupsPath = serverSection.AddSection("backups/playerdata").SectionFullPath,
            };
        }
    }
}