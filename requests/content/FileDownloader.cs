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
        /// The HttpClient instance used to download the files.
        /// </summary>
        private static readonly HttpClient Client = new HttpClient();

        /// <summary>
        /// Downloads a file asynchronously from a given URL.
        /// </summary>
        /// <param name="path">The path to save the file into</param>
        /// <param name="url">The direct download URL to download the file from</param>
        /// <returns>A Task to allow the method to be awaited</returns>
        public static async Task DownloadFileAsync(string path, string url)
        {
            Logging.LOGGER.Info($"Preparing to download {url} into {path}");
            using Stream DownloadStream = await Client.GetStreamAsync(url);
            using FileStream fileStream = new FileStream(path, FileMode.CreateNew);
            
            await DownloadStream.CopyToAsync(fileStream);
            Logging.LOGGER.Info($"Finished downloading the content from {url}");
        }
    }
}