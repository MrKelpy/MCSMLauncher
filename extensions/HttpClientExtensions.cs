using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace MCSMLauncher.extensions
{
    /// <summary>
    /// This class implements a bunch of extension methods for the HttpClient class
    /// </summary>
    public static class HttpClientExtensions
    {
        /// <summary>
        /// Asynchronously accesses the content of the given URL, returning it as a stream, allowing for
        /// the passing of a CancellationToken.
        /// </summary>
        /// <param name="client">The HttpClient instance to work with</param>
        /// <param name="url">The URL to send the request to</param>
        /// <param name="token">The cancellation token to use as a cancelling condition</param>
        /// <returns>A Task, promising a stream</returns>
        public static async Task<Stream> GetStreamAsync(this HttpClient client, string url, CancellationToken token)
        {
            HttpResponseMessage response = await client.GetAsync(url, token);
            return await response.Content.ReadAsStreamAsync();
            
        }
    }
}