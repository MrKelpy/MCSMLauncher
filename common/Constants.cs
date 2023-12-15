using LaminariaCore_Winforms.common;

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
        public static FileManager FileSystem { get; } = new ();
    }
}