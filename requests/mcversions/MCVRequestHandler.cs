using System;
using HtmlAgilityPack;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// ReSharper disable InconsistentNaming

namespace MCSMLauncher.requests.mcversions.releases
{
    /// <summary>
    /// This class handles every request to the mcversions.net website, and works
    /// together with MCVReleaseRequestParser in order to parse the information in a way that 
    /// returns useful data.
    /// </summary>
    public class MCVRequestHandler : AbstractBaseRequestHandler
    {
        public MCVRequestHandler() : base("https://mcversions.net") {}

        /// <summary>
        /// Accesses the website and parses out all the existent version names mapped
        /// to their download links.
        /// </summary>
        /// <returns>A Dictionary with a VersionName:VersionSite mapping</returns>
        public override async Task<Dictionary<string, string>> GetVersions()
        {
            HtmlDocument document = await Handler.LoadFromWebAsync(this.BaseUrl);

            var itemDivs = from div in document.DocumentNode.Descendants("div")
                where div.HasClass("items") select div;

            return MCVRequestParser.GetVersionUrlMap(this.BaseUrl, itemDivs.ElementAt(0));
        }

        /// <summary>
        /// Accesses the website and parses out all the existent snapshot names mapped
        /// to their download links.
        /// </summary>
        /// <returns>A Dictionary with a SnapshotName:VersionSite mapping</returns>
        public override Dictionary<string, string> GetSnapshots()
        {
            HtmlDocument document = Handler.Load(this.BaseUrl);

            var itemDivs = from div in document.DocumentNode.Descendants("div")
                where div.HasClass("items") select div;

            return MCVRequestParser.GetVersionUrlMap(this.BaseUrl, itemDivs.ElementAt(1));
        }
    }
}