using System.Collections.Generic;
using MCSMLauncher.requests;
using MCSMLauncher.requests.mcversions.releases;

namespace MCSMLauncher.common
{
    /// <summary>
    /// This factory class is responsible for creating the request handlers for the different websites, which are based on
    /// the server types selected.
    /// </summary>
    public class RequestHandlerFactory
    {
        /// <summary>
        /// Using the factory design pattern, determines the correct request handler to use based on the server type
        /// provided.
        /// </summary>
        /// <param name="serverType">The server type selected on the GUI</param>
        /// <returns>A child of AbstractBaseRequestHandler implementing </returns>
        public static AbstractBaseRequestHandler GetRequestHandler(string serverType)
        {
            if (serverType.Equals("Vanilla")) return new MCVRequestHandler();
            return null;
        }

    }
}