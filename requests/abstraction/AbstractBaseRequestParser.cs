using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MCSMLauncher.requests
{
    /// <summary>
    /// This abstract class implements the basic functionality and contracts for the request parsers.
    /// </summary>
    public abstract class AbstractBaseRequestParser
    {

        /// <summary>
        /// Returns the direct download link for a server given its version page
        /// </summary>
        /// <param name="version">The server version</param>
        /// <param name="url">The url of the version page to get the download link from</param>
        /// <returns>The direct download link for the server</returns>
        public abstract Task<string> GetServerDirectDownloadLink(string version, string url);

        /// <summary>
        /// Parses out the version names and server download URLs from the node and
        /// returns them in the form of a dictionary mapping name:link
        /// </summary>
        /// <param name="baseUrl">The current url of the node</param>
        /// <param name="doc">The HtmlNode to parse</param>
        /// <returns>A Dictionary(string,string) containing the mappings</returns>
        public abstract Dictionary<string, string> GetVersionUrlMap(string baseUrl, HtmlNode doc);

        /// <summary>
        /// Formats the version url mappings so every version fits within a three component version key.
        /// </summary>
        /// <param name="mappings">The mappings to format</param>
        /// <returns>The formatted mappings</returns>
        public Dictionary<string, string> FormatVersionMappings(Dictionary<string, string> mappings)
        {
            Dictionary<string, string> formattedMappings = new Dictionary<string, string>();
            
            // Iterates over each mapping in the versions dictionary and formats the versions to fit as a three component ver.
            foreach (var item in mappings)
            {
                
                // If the current key is not a three-component version, add a ".0" to the end of it, as the key of the formatted
                // dictionary
                if (item.Key.Split('.').Length < 3)
                {
                    formattedMappings[item.Key + ".0"] = item.Value;
                    continue;
                }

                // If it is, let it be
                formattedMappings[item.Key] = item.Value;
            }

            return formattedMappings;
        }



    }
}