using System;

namespace MCSMLauncher.common
{
    /// <summary>
    /// This class represents a Minecraft version. Contains methods related to parsing and sorting the version
    /// strings.
    /// </summary>
    public class MinecraftVersion : IComparable<MinecraftVersion>
    {
        /// <summary>
        /// The version to use within the class operations
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Main constructor for the MinecraftVersion class. Loads up the version from the raw version string.
        /// </summary>
        /// <param name="rawVersion"></param>
        public MinecraftVersion(string rawVersion)
        {
            Version = rawVersion.Replace("?", "0");
            Version = rawVersion.Split('.').Length is var str && str < 3 && str != 1 ? $"{Version}.0" : Version;
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer
        /// that indicates whether the current instance precedes, follows, or occurs in the same position in
        /// the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared.
        /// value < 0 => This instance precedes <paramref name="other"/> in the sort order.
        /// value == 0 => This instance occurs in the same position in the sort order as <paramref name="other" />.
        /// value > 0 => This instance follows <paramref name="other"/> in the sort order.
        /// </returns>
        public int CompareTo(MinecraftVersion other)
        {
            // The version 00.00.00 is the lowest version that there can be.
            if (this.Version.Equals("00.00.00")) return -1;
            if (other.Version.Equals("00.00.00")) return 1;
            
            // If the versions are the same, then they are equal.
            if (this.Version == other.Version) return 0;
            
            // Tries to compare the two version with the version API.
            try { return new Version(this.Version).CompareTo(new Version(other.Version)); }
            catch (ArgumentException) { } // There was an issue, needs further evaluation.
            
            // If the current version is the issue, then this one follows the other one.
            try { var _ = new Version(this.Version); }
            catch (ArgumentException) { return -1; } 

            // If the other version is the issue, this this one precedes it.
            return 1;
        }
    }
}