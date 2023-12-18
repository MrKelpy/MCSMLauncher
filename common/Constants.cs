using System;
using System.IO;
using LaminariaCore_Winforms.common;

namespace mcsm.common
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
        public static FileManager FileSystem { get; }
        
        /// <summary>
        /// Check if the MCSMLauncher section exists. If it does, rename it to MCSM.
        /// </summary>
        static Constants()
        {
            // For backwards compatibility, we check if the MCSMLauncher section exists. If it does, rename it to MCSM.
            string oldSection = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "/.MCSMLauncher";
            if (Directory.Exists(oldSection)) Directory.Move(oldSection, oldSection.Replace("MCSMLauncher", "MCSM"));

            // Initialises the file system.
            FileSystem = new FileManager();
        }
    }
}