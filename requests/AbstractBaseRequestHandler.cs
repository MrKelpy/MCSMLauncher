using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MCSMLauncher.requests
{
    /// <summary>
    /// This abstract class implements all the basic functionality to be used by the special website request handlers
    /// commonly.
    /// </summary>
    public abstract class AbstractBaseRequestHandler
    {
        /// <summary>
        /// The URL to perform the requests to.
        /// </summary>
        protected string BaseUrl { get; }

        public static HtmlWeb Handler { get; set; } = new HtmlWeb();

        /// <summary>
        /// Main constructor for the AbstractBaseRequestHandler class.
        /// Sets the URL and the handler.
        /// </summary>
        /// <param name="url">The URL to look for</param>
        protected AbstractBaseRequestHandler(string url) => this.BaseUrl = url;

        /// <summary>
        /// Accesses the website and parses out all the existent version names mapped
        /// to their direct download links.
        /// </summary>
        /// <returns>A Dictionary with a VersionName:VersionSite mapping</returns>
        public abstract Task<Dictionary<string, string>> GetVersions();
        
        /// <summary>
        /// Accesses the website and parses out all the existent snapshot names mapped
        /// to their direct download links.
        /// </summary>
        /// <returns>A Dictionary with a SnapshotName:VersionSite mapping</returns>
        public abstract Dictionary<string, string> GetSnapshots();
    }
}