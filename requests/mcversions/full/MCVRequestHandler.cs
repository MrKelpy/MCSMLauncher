﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MCSMLauncher.common;
using MCSMLauncher.requests.abstraction;

// ReSharper disable InconsistentNaming

namespace MCSMLauncher.requests.mcversions.full
{
    /// <summary>
    /// This class handles every request to the mcversions.net website, and works
    /// together with MCVReleaseRequestParser in order to parse the information in a way that
    /// returns useful data.
    /// </summary>
    public class MCVRequestHandler : AbstractBaseRequestHandler
    {
        public MCVRequestHandler() : base("https://mcversions.net")
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
                HtmlDocument document = await Handler.LoadFromWebAsync(this.BaseUrl);

                IEnumerable<HtmlNode> itemsDiv = from div in document.DocumentNode.Descendants("div")
                    where div.HasClass("items")
                    select div;

                return new MCVRequestParser().GetVersionUrlMap(this.BaseUrl, itemsDiv.ElementAt(0));
            }
            catch (Exception e)
            {
                Logging.LOGGER.Info("An error happened whilst trying to retrieve the vanilla versions.");
                Logging.LOGGER.Error(e.Message + "\n" + e.StackTrace, LoggingType.FILE);
                return null;
            }
        }
    }
}