using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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
        
        public ServerInformation() { }
    }
}