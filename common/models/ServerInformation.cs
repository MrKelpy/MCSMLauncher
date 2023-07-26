using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using MCSMLauncher.utils;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;

namespace MCSMLauncher.common.models
{
    /// <summary>
    /// This is a serializable class that acts as a model to store information about a server, and
    /// is able to be serialized to XML.
    /// </summary>
    [SuppressMessage("ReSharper", "CommentTypo")]
    [SuppressMessage("ReSharper", "IdentifierTypo")]
    [Serializable]
    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class ServerInformation
    {
        /// <summary>
        /// Empty constructor for the ServerInformation class. This is required for serialization.
        /// </summary>
        public ServerInformation()
        {
        }

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
        public int BasePort { get; set; } = 25565;
        
        /// <summary>
        /// The port that the server will actually use, after evaluation.
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// The IP Address used to connect to the server.
        /// This will either be the local IP address or the public IP address, depending on whether
        /// the UPnP setup was successful or not.
        /// Defaults to the local IP address.
        /// </summary>
        public string IPAddress { get; set; } = NetworkUtils.GetLocalIPAddress();
        
        /// <summary>
        /// Whether or not to use UPnP to try to open the port on the router.
        /// </summary>
        public bool UPnPOn { get; set; } = false;

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
                UPnPOn = false
            };
        }

        /// <summary>
        /// Updates the ServerInformation object with the information from the specified dictionary.
        /// </summary>
        /// <param name="updateDict">The dictionary to </param>
        public ServerInformation Update(Dictionary<string, string> updateDict)
        {
            // Act as a de-serializer for the ServerInformation object, matching all the dictionary keys to the fields
            foreach (FieldInfo field in typeof(ServerInformation).GetFields())
            {
                // Get the value from the dictionary, or null if it doesn't exist
                string value = updateDict.TryGetValue(field.Name.ToLower(), out string val) ? val : null;
                
                if (value != null)
                    field.SetValue(this, Convert.ChangeType(value, field.FieldType));
            }

            // Return the updated ServerInformation object for chaining
            return this;
        }

        /// <summary>
        /// Converts the ServerInformation object to a dictionary.
        /// </summary>
        /// <returns>A dictionary with the ServerInformation data</returns>
        public Dictionary<string, string> ToDictionary()
        {
            // The dictionary to use as the serialization dictionary
            Dictionary<string, string> dict = new Dictionary<string, string>();

            // Act as a serializer for the ServerInformation object, matching all the fields to the dictionary keys
            foreach (FieldInfo field in typeof(ServerInformation).GetFields())
                dict.Add(field.Name.ToLower(), field.GetValue(this).ToString());

            return dict;
        }
    }
}