using System.Collections.Generic;
using System.IO;
using LaminariaCore_Winforms.common;
using mcsm.api.server.enumeration;
using mcsm.common;
using mcsm.common.caches;
using mcsm.common.models;
using static mcsm.common.Constants;

namespace mcsm.api.server
{
    /// <summary>
    /// This class is responsible for providing an API for editing server information. (This is made really
    /// easy due to the extra layer of abstraction provided by the ServerEditor class) <br/>
    /// 
    /// The methods defined in this class are essentially just shortcuts for common ServerEditor operations, alongside
    /// with a Raw() method that allows the user to interact with the ServerEditor directly.
    /// </summary>
    public class ServerEditing
    {
        /// <summary>
        /// The server editor that will be used to edit the server information.
        /// </summary>
        private ServerEditor Editor { get; set; }
        
        /// <summary>
        /// The section that represents the server that is being edited.
        /// </summary>
        private Section ServerSection { get; set; }
        
        /// <summary>
        /// Main constructor for the ServerEditing class. Takes in the server editor to edit the server with.
        /// </summary>
        /// <param name="serverName">The server name that will be used to identify the server to edit</param>
        public ServerEditing(string serverName)
        {
            Section serverSection = FileSystem.GetFirstSectionNamed(serverName);
            this.Editor = GlobalEditorsCache.INSTANCE.GetOrCreate(serverSection);
            this.ServerSection = serverSection;
        }
        
        /// <summary>
        /// Gets the raw server editor associated with the server name.
        /// </summary>
        /// <returns>An instance of the ServerEditor to be used.</returns>
        public ServerEditor Raw() => Editor;
        
        /// <summary>
        /// Changes a server's name to the new name provided.
        /// </summary>
        /// <param name="newName">The name to change the server to</param>
        public void ChangeServerName(string newName)
        {
            // Changes the name of the server by moving the server's section to a new path. 
            string newServerSectionPath = Path.GetDirectoryName(ServerSection.SectionFullPath) + "/" + newName;
            Directory.Move(this.ServerSection.SectionFullPath, newServerSectionPath);
            
            // Updates the server section to the new path and updates the server editor.
            GlobalEditorsCache.INSTANCE.Remove(this.ServerSection.SimpleName);
            this.ServerSection = FileSystem.AddSection("servers/" + newName);
            this.Editor = GlobalEditorsCache.INSTANCE.GetOrCreate(this.ServerSection);
        }
        
        /// <summary>
        /// Deletes the server's section and removes the server editor from the cache.
        /// </summary>
        public void DeleteServer()
        {
            GlobalEditorsCache.INSTANCE.Remove(this.ServerSection.SimpleName);
            Directory.Delete(this.ServerSection.SectionFullPath, true);
        }

        /// <summary>
        /// Uses the server editor associated with the server name to update the server settings
        /// based on the map provided.
        /// </summary>
        /// <param name="settings">The dictionary(string,string) containing the settings to be changed</param>
        public void UpdateServerSettings(Dictionary<string, string> settings)
        {
            Editor.UpdateBuffers(settings);
            Editor.FlushBuffers();
        }

        /// <summary>
        /// Gets a copy of the server settings from the server editor associated with the server name, stemming
        /// from its ServerInformation object.
        /// </summary>
        /// <returns>A Dictionary containing the current server settings.</returns>
        public Dictionary<string, string> GetCurrentServerSettings() => Editor.GetBuffersCopy();
        
        /// <returns>
        /// Returns the server section associated with the server name.
        /// </returns>
        public Section GetServerSection() => this.ServerSection;
        
        /// <returns>
        /// Returns the server name associated with the server.
        /// </returns>
        public string GetServerName() => this.GetServerSection().SimpleName;
        
        /// <returns>
        /// Returns the ServerInformation object associated with the server.
        /// </returns>
        public ServerInformation GetServerInformation() => this.Raw().GetServerInformation();

        /// <summary>
        /// Checks the server's properties for a specific logical expression based on the provided ServerLogicChecks enum.
        /// </summary>
        /// <param name="question">The "question" being asked to the server</param>
        /// <returns>Boolean, whether or not the question is True</returns>
        public bool Check(ServerLogicChecks question)
        {
            switch (question)
            {
                case ServerLogicChecks.IsCracked:
                    return !this.Raw().GetFromBuffers<bool>("online-mode");
                
                case ServerLogicChecks.IsSpawnProtectionEnabled:
                    return this.Raw().BuffersContain("spawn-protection") && this.Raw().GetFromBuffers<int>("spawn-protection") > 0;
                
                default: return false;
            }
        }
    }
}