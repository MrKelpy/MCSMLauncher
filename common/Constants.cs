using PgpsUtilsAEFC.common;

namespace MCSMLauncher.common
{
    /// <summary>
    /// This class defines constant values that are to be carried across the program.
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// The FileManager instance to use across the project in order to interact with the
        /// files.
        /// </summary>
        public static FileManager FileSystem { get; set; } = new FileManager();
        
        /// <summary>
        /// Defines the vanilla releases versions cache file path
        /// </summary>
        public static readonly string VANILLA_RELEASES_CACHE_FILENAME = "vanilla_releases.cache";
        
        /// <summary>
        /// Defines the vanilla snapshots versions cache file path
        /// </summary>
        public static readonly string VANILLA_SNAPSHOTS_CACHE_FILENAME = "vanilla_snapshots.cache";
        
        /// <summary>
        /// Defines the spigot releases versions cache file path
        /// </summary>
        public static readonly string SPIGOT_RELEASES_CACHE_FILENAME = "spigot_releases.cache";
        
        /// <summary>
        /// Defines the forge releases versions cache file path
        /// </summary>
        public static readonly string FORGE_RELEASES_CACHE_FILENAME = "forge_releases.cache";
    }
}