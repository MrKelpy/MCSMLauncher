using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using mcsm.common;
using mcsm.requests.abstraction;

// ReSharper disable InconsistentNaming

namespace mcsm.requests.mcversions.snapshots
{
    /// <summary>
    /// This class handles every request to the mcversions.net website, and works
    /// together with MCVReleaseRequestParser in order to parse the information in a way that
    /// returns useful data.
    /// </summary>
    public class MCVSnapshotsRequestHandler : AbstractBaseRequestHandler
    {
        public MCVSnapshotsRequestHandler() : base("https://mcversions.net")
        {
        }

        /// <summary>
        /// Accesses the website and parses out all the existent version names mapped
        /// to their download links.
        /// </summary>
        /// <returns>A Dictionary with a VersionName:VersionSite mapping</returns>
        public override async Task<Dictionary<string, string>> GetVersions()
        {
            try
            {
                HtmlDocument document = await Handler.LoadFromWebAsync(BaseUrl);

                IEnumerable<HtmlNode> itemDivs = from div in document.DocumentNode.Descendants("div")
                    where div.HasClass("items")
                    select div;

                return new McvRequestParser().GetVersionUrlMap(BaseUrl, itemDivs.ElementAt(1));
            }
            catch (Exception e)
            {
                Logging.Logger.Info("An error happened whilst trying to retrieve the vanilla snapshot versions.");
                Logging.Logger.Error(e, LoggingType.File);
                return null;
            }
        }
    }
}