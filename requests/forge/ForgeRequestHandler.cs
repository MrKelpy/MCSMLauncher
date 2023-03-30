using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Threading.Tasks;
using HtmlAgilityPack;
using MCSMLauncher.common;

namespace MCSMLauncher.requests.forge
{
    /// <summary>
    /// This class handles all requests sent into the minecraftforge website, in order to retrieve all
    /// the server versions
    /// </summary>
    public class ForgeRequestHandler : AbstractBaseRequestHandler
    {
        public ForgeRequestHandler() : base("https://files.minecraftforge.net/net/minecraftforge/forge/") {}

        /// <summary>
        /// Accesses the website and parses out all the existent version names mapped
        /// to their direct download links.
        /// </summary>
        /// <returns>A Dictionary with a VersionName:VersionSite mapping</returns>
        public override async Task<Dictionary<string, string>> GetVersions()
        {
            try
            {
                HtmlDocument document = await Handler.LoadFromWebAsync(this.BaseUrl);
                return new ForgeRequestParser().GetVersionUrlMap(this.BaseUrl, document.DocumentNode);
            }
            catch (Exception e)
            {
                Logging.LOGGER.Info("An error happened whilst trying to retrieve the forge versions.");
                Logging.LOGGER.Error(e.Message + "\n" + e.StackTrace);
                return null;
            }
        }

        /// <summary>
        /// Accesses the website and parses out all the existent snapshot names mapped
        /// to their direct download links.
        /// </summary>
        /// <returns>A Dictionary with a SnapshotName:VersionSite mapping</returns>
        public override Task<Dictionary<string, string>> GetSnapshots() => null;
    }
}