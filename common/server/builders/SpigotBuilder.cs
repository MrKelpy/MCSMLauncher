using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MCSMLauncher.common.server.builders.abstraction;
using MCSMLauncher.ui.graphical;
using Open.Nat;

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
        /// <param name="outputHandler">The output system to use while logging the messages.</param>
        public SpigotBuilder(MessageProcessingOutputHandler outputHandler) : base("-DIReallyKnowWhatIAmDoingISwear=true -jar %SERVER_JAR% nogui", outputHandler)
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
            void Processor() => this.OutputSystem.Write(Logging.Logger.Warn(message) + Environment.NewLine, Color.Gray);
            Mainframe.INSTANCE.Invoke((MethodInvoker) Processor);

            // Figures out whether the server has errored out or not.
            bool isNotError = TerminationCode != 1
                              && !message.ToLower().Split(' ').Contains("error")
                              && !message.ToLower().Split(' ').Contains("unsupported");
                              
            TerminationCode = isNotError ? 3 : 1;
        }
    }
}