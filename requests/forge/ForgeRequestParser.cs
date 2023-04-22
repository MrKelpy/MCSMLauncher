using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MCSMLauncher.requests.forge
{
    /// <summary>
    /// This class takes in a certain scope of Html Nodes and parses them down
    /// in different ways in order to extract useful information from them.
    /// </summary>
    public class ForgeRequestParser : AbstractBaseRequestParser
    {
        
        /// <summary>
        /// Returns the direct download link for a server given its version page
        /// </summary>
        /// <param name="version">The server version</param>
        /// <param name="url">The url of the version page to get the download link from</param>
        /// <returns>The direct download link for the server</returns>
        public override async Task<string> GetServerDirectDownloadLink(string version, string url)
        {
            HtmlDocument document = await AbstractBaseRequestHandler.Handler.LoadFromWebAsync(url);
            HtmlNode downloadsDiv = document.DocumentNode.SelectSingleNode("//div[@class=\"downloads\"]");
            string recommendedForgeVersion = downloadsDiv.SelectSingleNode(downloadsDiv.XPath + "/div/div/small").InnerText.Replace(" ", "");
            string directLink = $"https://maven.minecraftforge.net/net/minecraftforge/forge/{recommendedForgeVersion}/forge-{recommendedForgeVersion}-installer.jar";

            // Gets the response code from the primary direct link
            HttpStatusCode statusCode = HttpStatusCode.NotFound;

            try
            {
                var request = (HttpWebRequest) WebRequest.Create(directLink);
                request.Method = "HEAD";
                statusCode = ((HttpWebResponse) request.GetResponse()).StatusCode;
            }
            catch (WebException) { }  // Ignored, the status code will remain 404

            // Gets the extended version of the recommended forge version, with itself repeated afterwards.
            string extendedVersion = recommendedForgeVersion + "-" + version;
            
            // Return the direct link, or the direct link with an extended version number if the response isn't 200. 
            return statusCode == HttpStatusCode.OK ? directLink : directLink.Replace(recommendedForgeVersion, extendedVersion);
        }
        
        /// <summary>
        /// Parses out the version names and server download URLs from the node and
        /// returns them in the form of a dictionary mapping name:link
        /// </summary>
        /// <param name="baseUrl">The current url of the node</param>
        /// <param name="doc">The HtmlNode to parse</param>
        /// <returns>A Dictionary(string,string) containing the mappings</returns>
        public override Dictionary<string, string> GetVersionUrlMap(string baseUrl, HtmlNode doc)
        {
            Dictionary<string, string> mappings = new Dictionary<string, string>();
            
            // Gets the lists that have hrefs in them and are under the nav-collapsible lists. 
            var lists = from li in doc.SelectNodes("//li")
                where li.ParentNode.HasClass("nav-collapsible") select li;
            
            // Iterates through each list, gets its link and inner text, and adds them to the mappings.
            foreach (HtmlNode list in lists)
            {
                string key = list.SelectSingleNode("a")?.InnerText;
                string value = list.SelectSingleNode("a")?.GetAttributeValue("href", null);

                // There is one special case where the list has the class "elem-active" instead of the href.
                // This is the active element in the list, so we handle it separately when we find it.
                if (list.HasClass("elem-active"))
                {
                    mappings.Add(list.InnerText, baseUrl + $"index_{list.InnerText}.html");
                    continue;
                }
                
                if (value == null || key == null) continue;
                mappings.Add(key, baseUrl + value);
                
                if (key == "1.5.2") break;
            }

            return this.FormatVersionMappings(mappings);
        }
    }
}