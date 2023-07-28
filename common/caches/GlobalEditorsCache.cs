#nullable enable
using System.Collections.Generic;
using System.Linq;
using PgpsUtilsAEFC.common;

// ReSharper disable InconsistentNaming

namespace MCSMLauncher.common.caches
{
    /// <summary>
    /// This singleton class implements a global ServerEditors cache, used to avoid having to re-fetch
    /// them every time, to diminish the amount of disk reads per instance.
    /// </summary>
    public class GlobalEditorsCache
    {
        
        /// <summary>
        /// Singleton instance of the GlobalEditorsCache class.
        /// </summary>
        public static GlobalEditorsCache INSTANCE { get; } = new ();
        
        /// <summary>
        /// Singleton enforcing private constructor.
        /// </summary>
        private GlobalEditorsCache() { }

        /// <summary>
        /// A cache of the server editors, used to avoid having to re-fetch them every time.
        /// </summary>
        private List<ServerEditor> ServerEditorsCache { get; } = new ();
        
        /// <summary>
        /// Creates and adds a server editor if it doesn't exist, or gets it from the cache if it does.
        /// </summary>
        /// <param name="serverSection">The server section to match the editor to</param>
        /// <returns>A cached instance of ServerEditor</returns>
        public ServerEditor GetOrCreate(Section serverSection) => Get(serverSection) ?? Add(serverSection);
        
        /// <summary>
        /// Removes a server editor from the cache.
        /// </summary>
        /// <param name="serverName">The name of the server to remove</param>
        public void Remove(string serverName)
        {
            Section serverSection = Constants.FileSystem.GetFirstSectionNamed($"/servers/{serverName}");
            ServerEditor? editorCheck = Get(serverSection);
            if (editorCheck != null) ServerEditorsCache.Remove(editorCheck);
        }
        
        /// <summary>
        /// Gets the server editor from the cache.
        /// </summary>
        /// <param name="serverSection">The server section to match the editor to</param>
        /// <returns>The ServerEditor matching the server name provided</returns>
        private ServerEditor? Get(Section serverSection) =>
            ServerEditorsCache.FirstOrDefault(x => x.ServerSection == serverSection);
        
        /// <summary>
        /// Adds a server editor to the cache, returning it afterwards.
        /// </summary>
        /// <param name="serverSection">The server section to match the editor to</param>
        /// <returns>The ServerEditor instance</returns>
        private ServerEditor Add(Section serverSection)
        {
            // Checks if the server editor already exists in the cache. If so, returns it.
            ServerEditor? editorCheck = Get(serverSection);
            if (editorCheck != null) return editorCheck;

            // Gets the server section and creates the editor.
            ServerEditor editor = new (serverSection);
            
            // Adds the editor to the cache and returns it.
            ServerEditorsCache.Add(editor);
            return editor;
        }
    }
}