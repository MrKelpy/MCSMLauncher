using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using LaminariaCore_Winforms.common;
using MCSMLauncher.common;
using MCSMLauncher.common.factories;
using MCSMLauncher.common.server.builders.abstraction;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.api.server
{
    /// <summary>
    /// This class is responsible for providing an API for all types of server building operations
    /// that can be performed.
    /// </summary>
    public class ServerBuilder
    {
        
        /// <summary>
        /// The name of the server to be built.
        /// </summary>
        private string ServerName { get; }
        
        /// <summary>
        /// The minecraft server type to build the server as.
        /// </summary>
        private string ServerType { get; }
        
        /// <summary>
        /// The minecraft server version to build the server on.
        /// </summary>
        private string ServerVersion { get; }
        
        /// <summary>
        /// The section of the filesystem that contains all of the servers.
        /// </summary>
        private Section ServersSection = FileSystem.AddSection("servers");

        /// <summary>
        /// The list of invalid server names, used to check if the server name is valid.
        /// These names are reserved by Windows for special folders.
        /// </summary>
        private List<string> InvalidServerNames { get; } = new ()
        {
            "CON", "PRN", "AUX", "NUL", "COM1", "COM2", "COM3", "COM4", "COM5", "COM6",
            "COM7", "COM8", "COM9", "COM0", "LPT1", "LPT2", "LPT3", "LPT4", "LPT5",
            "LPT6", "LPT7", "LPT8", "LPT9", "LPT0"
        };
        
        /// <summary>
        /// Main constructor for the ServerBuilderAPI class. Takes in the server information in order to interact with the
        /// builders for the server.
        /// </summary>
        /// <param name="serverName">The name of the server to be built</param>
        /// <param name="serverType">The server type to create the server as</param>
        /// <param name="serverVersion">The server version to build the server on</param>
        public ServerBuilder(string serverName, string serverType, string serverVersion)
        {
            this.ServerVersion = serverVersion;
            this.ServerType = serverType;
            this.ServerName = serverName;
        }
        
        /// <summary>
        /// Verifies that the information provided is valid and can be used to build a server.
        /// </summary>
        /// <param name="strict">Whether or not to perform strict validation. If true, will enable checking for the version and type of the server.</param>
        /// <exception cref="ArgumentException">Occurs when any part of the information provided is not fit to build a server.</exception>
        public void VerifyInformation(bool strict = false)
        {
            if (this.ServerName.ToList().Any(Path.GetInvalidPathChars().Contains) || this.ServerName.Contains(' '))
                throw new ArgumentException(@"Invalid characters in server name.");

            if (InvalidServerNames.Any(x => x.ToUpper().Equals(this.ServerName.ToUpper())))
                throw new ArgumentException(@"Invalid server name.");

            if (this.ServersSection.GetAllSections().Any(x => x.SimpleName.ToLower().Equals(this.ServerName.ToLower())))
                throw new ArgumentException(@"A server with that name already exists.");

            // Stops here if strict validation is enabled.
            if (strict) return;  
            
            if (MinecraftVersion.isSemanticVersion(this.ServerVersion))
                throw new ArgumentException(@"Invalid server version.");
            
            if (new ServerTypeMappingsFactory().GetSupportedServerTypes().Contains(this.ServerType))
                throw new ArgumentException(@"Invalid server type.");
        }

        /// <summary>
        /// Runs the server builder with the information provided.
        /// </summary>
        public async void Run()
        {
            AbstractServerBuilder builder = new ServerTypeMappingsFactory().GetBuilderFor(this.ServerType);
            await builder.Build(this.ServerName, this.ServerType, this.ServerVersion);
            // TODO: Adapt Build() to the API System. (It's too closely tied to the GUI)
        }
        
    }
}