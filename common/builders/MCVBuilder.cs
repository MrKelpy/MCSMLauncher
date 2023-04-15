using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common.builders.abstraction;
using MCSMLauncher.common.factories;
using MCSMLauncher.gui;
using MCSMLauncher.requests.content;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;

namespace MCSMLauncher.common.builders
{
    /// <summary>
    /// This class implements the server building methods for the vanilla releases. 
    /// </summary>
    public class MCVBuilder : AbstractServerBuilder
    {
        /// <summary>
        /// Installs the server.jar file given the path to the server installer. When it finishes,
        /// return the path to the server.jar file.
        /// </summary>
        /// <param name="serverInstallerPath">The path to the installer</param>
        /// <returns>The path to the server.jar file used to run the server.</returns>
        public override Task<string> InstallServer(string serverInstallerPath) => 
            Task.Run(() => serverInstallerPath);

        /// <summary>
        /// Runs the server once and closes it once it has been initialised. Deletes the world folder
        /// in the end.
        /// This method aims to initialise and build all of the server files in one go.
        /// </summary>
        /// <param name="serverPath">The path of the server file to run</param>
        /// <returns>A Task to allow the method to be awaited</returns>
        public Task RunAndCloseSilently(string serverPath)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Agrees to the eula by replacing the eula=false line in the file to eula=true.
        /// </summary>
        /// <param name="eulaPath">The path to the eula.txt file</param>
        public void AgreeToEula(string eulaPath)
        {
            throw new System.NotImplementedException();
        }
    }
}