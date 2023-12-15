using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCSMLauncher.utils
{
    /// <summary>
    /// Temporary class for extension methods for FileUtil until I decide to update the library.
    /// </summary>
    public static class FileUtilExtensions
    {

        /// <summary>
        /// Blocks the thread waiting for a file to be released by another process and returns
        /// it as a FileStream when it eventually is.
        /// </summary>
        /// <param name="path">The path of the file to get</param>
        /// <param name="timeout">A maximum timeout in milliseconds</param>
        public static async Task<FileStream> WaitForFileAsync(string path, int timeout = 10000)
        {
            DateTime maxTimeout = DateTime.Now.AddMilliseconds(timeout);

            while (DateTime.Now <= maxTimeout)
            {
                try
                {
                    // Tries to open the file, if it fails, continue the loop until the timeout is reached.
                    Stream stream = new FileStream(path, FileMode.Open);
                    return (FileStream)stream;
                }
                catch (IOException) { await Task.Delay(100); }
            }

            throw new TimeoutException("The file could not be accessed within the specified timeout.");
        }
    }
}