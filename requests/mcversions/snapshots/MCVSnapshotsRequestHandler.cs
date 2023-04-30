using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Threading.Tasks;
using MCSMLauncher.common;

// ReSharper disable InconsistentNaming

namespace MCSMLauncher.requests.mcversions
{
    /// <summary>
    /// This class handles every request to the mcversions.net website, and works
    /// together with MCVReleaseRequestParser in order to parse the information in a way that 
    /// returns useful data.
    /// </summary>
    public class MCVSnapshotsRequestHandler : AbstractBaseRequestHandler
    {
        public MCVSnapshotsRequestHandler() : base("https://mcversions.net") {}

        /// <summary>
        /// Accesses the website and parses out all the existent version names mapped
        /// to their download links.
        /// </summary>
        /// <returns>A Dictionary with a VersionName:VersionSite mapping</returns>
        public override async Task<Dictionary<string, string>> GetVersions()
        {
            try
            {
                HtmlDocument document = await Handler.LoadFromWebAsync(this.BaseUrl);

                var itemDivs = from div in document.DocumentNode.Descendants("div")
                    where div.HasClass("items")
                    select div;

                return new MCVRequestParser().GetVersionUrlMap(this.BaseUrl, itemDivs.ElementAt(1));
            }
            catch (Exception e)
            {
                Logging.LOGGER.Info("An error happened whilst trying to retrieve the vanilla snapshot versions.");
                Logging.LOGGER.Error(e.Message + "\n" + e.StackTrace, LoggingType.FILE);
                return null;
            }
        }
    }
}