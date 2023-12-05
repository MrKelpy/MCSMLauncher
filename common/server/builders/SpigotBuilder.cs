using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common.server.builders.abstraction;
using MCSMLauncher.ui.graphical;

namespace MCSMLauncher.common.server.builders
{
    /// <summary>
    /// This class implements the server building methods for the spigot releases.
    /// </summary>
    public class SpigotBuilder : AbstractServerBuilder
    {
        /// <summary>
        /// Main constructor for the SpigotBuilder class. Defines the start-up arguments for the server.
        /// </summary>
        public SpigotBuilder() : base("-DIReallyKnowWhatIAmDoingISwear=true -jar %SERVER_JAR% nogui")
        {
        }

        /// <summary>
        /// Installs the server.jar file given the path to the server installer. When it finishes,
        /// return the path to the server.jar file.
        /// </summary>
        /// <param name="serverInstallerPath">The path to the installer</param>
        /// <returns>The path to the server.jar file used to run the server.</returns>
        protected override Task<string> InstallServer(string serverInstallerPath)
        {
            return Task.Run(() => serverInstallerPath);
        }

        /// <summary>
        /// Due to the stupidity of early Minecraft logging, capture the STDERR and STDOUT in this method,
        /// and separate them by WARN, ERROR, and INFO messages, calling the appropriate methods.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        /// <param name="proc">The running process of the server</param>
        protected override void ProcessMergedData(object sender, DataReceivedEventArgs e, Process proc)
        {
            if (e.Data == null || e.Data.Trim().Equals(string.Empty)) return;

            if (e.Data.Contains("INFO"))
                ProcessInfoMessages(e.Data, proc);
            else if (e.Data.Contains("WARN"))
                ProcessWarningMessages(e.Data, proc);
            else if (e.Data.Contains("ERROR"))
                ProcessErrorMessages(e.Data, proc);
            else
                ProcessOtherMessages(e.Data, proc);
        }

        /// <summary>
        /// Processes any undifferentiated messages received from the server jar.
        /// Since we might be updating the console from another thread, we're just going to invoke everything
        /// and that's that.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>2 - The server.jar fired a warning</terminationCode>
        protected override void ProcessOtherMessages(string message, Process proc)
        {
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.SelectionColor = Color.Gray; }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate
            {
                OutputConsole.AppendText(Logging.Logger.Warn(message) + Environment.NewLine);
            }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.SelectionColor = Color.Black; }));
            TerminationCode = TerminationCode != 1
                                   && !message.ToLower().Split(' ').Contains("error")
                                   && !message.ToLower().Split(' ').Contains("unsupported")
                ? 3
                : 1;
        }
    }
}