using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MCSMLauncher.requests.mcversions
{
    /// <summary>
    /// This class takes in a certain scope of Html Nodes and parses them down
    /// in different ways in order to extract useful information from them.
    /// </summary>
    public class MCVRequestParser : IBaseRequestParser
    {

        /// <summary>
        /// Returns the direct download link for a server given its mcversions page
        /// </summary>
        /// <param name="url">The url of the mcversions page to get the download link from</param>
        /// <returns>The direct download link for the server</returns>
        public async Task<string> GetServerDirectDownloadLink(string url)
        {
            HtmlNode node = (await AbstractBaseRequestHandler.Handler.LoadFromWebAsync(url)).DocumentNode;
            
            string directLink = node.Descendants("a").First(x => x.HasClass("text-xs")).Attributes["href"].Value;
            return directLink.ToLower().Contains("server") ? directLink : null;
        }

        /// <summary>
        /// Parses out the version names and server download URLs from the node and
        /// returns them in the form of a dictionary mapping name:link
        /// </summary>
        /// <param name="baseUrl">The current url of the node</param>
        /// <param name="doc">The HtmlNode to parse</param>
        /// <returns>A Dictionary(string,string) containing the mappings</returns>
        public Dictionary<string, string> GetVersionUrlMap(string baseUrl, HtmlNode doc)
        {
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            
            // Gets all the "item" elements in the html, which contain the name and link.
            var items = from item in doc.Descendants("div")
                where item.HasClass("item") select item;
            
            foreach (var item in items)
            {
                // Skips the item if it is just an advertisement, and not a version.
                if (item.GetAttributeValue("id", null) == null) continue;

                // Extracts both the (name from the id) and the download link.
                string name = item.GetAttributeValue("id", null);
                string link = item.SelectSingleNode($"//*[@id=\"{name}\"]/div[2]/a").GetAttributeValue("href", null);
                string directLink = baseUrl + link;

                mappings.Add(name, directLink);
                
                // Since there's no version with a server past 1.2.1, just break once we get to it.
                if (name.Equals("1.2.1")) break;
            }

            return mappings;
        }
    }
}