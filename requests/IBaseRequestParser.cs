﻿using System.Collections.Generic;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace MCSMLauncher.requests
{
    /// <summary>
    /// This interface implements the basic functionality and contracts for the request parsers.
    /// </summary>
    public interface IBaseRequestParser
    {

        /// <summary>
        /// Returns the direct download link for a server given its version page
        /// </summary>
        /// <param name="url">The url of the version page to get the download link from</param>
        /// <returns>The direct download link for the server</returns>
        Task<string> GetServerDirectDownloadLink(string url);
        
        /// <summary>
        /// Parses out the version names and server download URLs from the node and
        /// returns them in the form of a dictionary mapping name:link
        /// </summary>
        /// <param name="baseUrl">The current url of the node</param>
        /// <param name="doc">The HtmlNode to parse</param>
        /// <returns>A Dictionary(string,string) containing the mappings</returns>
        Dictionary<string, string> GetVersionUrlMap(string baseUrl, HtmlNode doc);
    }
}