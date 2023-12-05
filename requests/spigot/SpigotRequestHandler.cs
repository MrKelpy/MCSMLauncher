using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MCSMLauncher.common;
using MCSMLauncher.requests.abstraction;

namespace MCSMLauncher.requests.spigot
{
    /// <summary>
    /// This class handles all requests sent to the getbukkit website, in order to retrieve the
    /// spigot versions.
    /// </summary>
    public class SpigotRequestHandler : AbstractBaseRequestHandler
    {
        public SpigotRequestHandler() : base("https://getbukkit.org/download/spigot")
        {
        }

        /// <summary>
        /// Accesses the website and parses out all the existent version names mapped
        /// to their direct download links.
        /// </summary>
        /// <returns>A Dictionary with a VersionName:VersionSite mapping</returns>
        public override async Task<Dictionary<string, string>> GetVersions()
        {
            try
            {
                HtmlDocument document = await Handler.LoadFromWebAsync(BaseUrl);

                IEnumerable<HtmlNode> columnDiv = from div in document.DocumentNode.Descendants("div")
                    where div.HasClass("col-md-12")
                    select div;

                return new SpigotRequestParser().GetVersionUrlMap(BaseUrl, columnDiv.ElementAt(0));
            }
            catch (Exception e)
            {
                Logging.Logger.Info("An error happened whilst trying to retrieve the spigot versions.");
                Logging.Logger.Error(e, LoggingType.File);
                return null;
            }
        }
    }
}