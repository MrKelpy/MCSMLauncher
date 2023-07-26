using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using MCSMLauncher.common;

namespace MCSMLauncher.requests.content
{
    /// <summary>
    /// This class implements every method related to downloading files asynchronously from the
    /// internet.
    /// </summary>
    public class FileDownloader
    {
        /// <summary>
        /// Downloads a file asynchronously from a given URL.
        /// </summary>
        /// <param name="path">The path to save the file into</param>
        /// <param name="url">The direct download URL to download the file from</param>
        /// <returns>A Task to allow the method to be awaited</returns>
        public static async Task DownloadFileAsync(string path, string url)
        {
            try
            {
                HttpClient Client = new HttpClient();

                Logging.LOGGER.Info($"Preparing to download {url} into {path}");
                using Stream downloadStream = await Client.GetStreamAsync(url);
                using FileStream fileStream = new FileStream(path, FileMode.CreateNew);

                await downloadStream.CopyToAsync(fileStream);
                Logging.LOGGER.Info($"Finished downloading the content from {url}");
            }
            // If the task ended up being cancelled due to a time out, throw an exception.
            catch (TaskCanceledException)
            {
                throw new TimeoutException("Request timed out");
            }
        }
    }
}