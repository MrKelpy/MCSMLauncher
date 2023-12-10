using System.IO;
using LaminariaCore_Winforms.common;
using MCSMLauncher.common;
using MCSMLauncher.common.caches;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.api.server
{
    /// <summary>
    /// This class is responsible for providing an API for editing server information. (This is made really
    /// easy due to the extra layer of abstraction provided by the ServerEditor class)
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
        
    }
}