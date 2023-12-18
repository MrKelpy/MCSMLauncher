using System.Collections.Generic;

namespace mcsm.common.caches
{
    /// <summary>
    /// This class is meant to implement a cache to quickly access the server editors through string keys. It
    /// should be used sparingly, since it is a bit consuming.
    /// </summary>
    public class QuickAccessEditorsCache
    {

        /// <summary>
        /// The cache of server editors.
        /// </summary>
        public Dictionary<string, ServerEditor> Cache { get; } = new ();

        /// <summary>
        /// Main constructor for the QuickAccessEditorsCache class.
        /// </summary>
        public QuickAccessEditorsCache() { }
        
        /// <summary>
        /// Gets the server editor from the cache. If it doesn't exist, it returns null.
        /// </summary>
        /// <param name="serverName">The server name bound to the editor to get from the cache</param>
        /// <returns>A ServerEditor instance matching the server name, or null if not found</returns>
        public ServerEditor Get(string serverName) => 
            Cache.ContainsKey(serverName) ? Cache[serverName] : null;
        
        /// <summary>
        /// Adds a server editor to the cache.
        /// </summary>
        /// <param name="editor">The editor to add to the cache</param>
        public void Add(ServerEditor editor) => Cache.Add(editor.ServerSection.SimpleName, editor);
        
        /// <summary>
        /// Removes a server editor from the cache.
        /// </summary>
        /// <param name="serverName">The server name to remove from the cache</param>
        public void Remove(string serverName) => Cache.Remove(serverName);

    }
}