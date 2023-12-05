using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using LaminariaCore_General.utils;
using MCSMLauncher.ui.graphical;

namespace MCSMLauncher.common.processes
{
    /// <summary>
    /// This class implements all the base methods for command processing events
    /// to be implemented by the derived classes.
    /// </summary>
    public abstract class AbstractCommandProcessing
    {
        /// <summary>
        /// The termination code for a server execution, to be used by the processing events
        /// </summary>
        protected int TerminationCode { get; set; } = -1;

        /// <summary>
        /// A collection of errors to handle differently in the processing methods
        /// </summary>
        protected ErrorCollection SpecialErrors { get; } = new ();

        /// <summary>
        /// The console object to update with the logs.
        /// </summary>
        protected RichTextBox OutputConsole => NewServer.Instance.RichTextBoxConsoleOutput;

        /// <summary>
        /// Due to the stupidity of early Minecraft logging, capture the STDERR and STDOUT in this method,
        /// and separate them by WARN, ERROR, and INFO messages, calling the appropriate methods.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        /// <param name="proc">The running process of the server</param>
        protected virtual void ProcessMergedData(object sender, DataReceivedEventArgs e, Process proc)
        {
            // If the lines are invalid or empty, return. 
            if (e.Data == null || e.Data.Trim().Equals(string.Empty)) return;

            // Try to match the line with the regex, expecting groups.
            Match matches = Regex.Match(e.Data.Trim(), @"^(?:\[[^\]]+\] \[[^\]]+\]: |[\d-]+ [\d:]+ \[[^\]]+\] )(.+)$",
                RegexOptions.Multiline);

            try
            {
                // Get the type section and the message from the regex groups.
                string typeSection = matches.Groups[0].Captures[0].Value;
                string message = matches.Groups[1].Captures[0].Value;

                // If the message is an error and not registered as a special error, handle it as such.
                if ((!SpecialErrors.StringMatches(typeSection) && typeSection.Contains("ERROR")) ||
                    typeSection.Contains("Exception"))
                    ProcessErrorMessages(message, proc);

                // Handle the warning messages.
                else if (typeSection.Contains("WARN"))
                    ProcessWarningMessages(message, proc);

                // Handle the info messages.
                else if (typeSection.Contains("INFO")) ProcessInfoMessages(message, proc);
            }
            catch (ArgumentOutOfRangeException)
            {
                // ignored
            }

            // Handle any other messages that don't fit the above criteria.
            ProcessOtherMessages(e.Data, proc);
        }

        /// <summary>
        /// Processes any INFO messages received from the server jar.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>0 - The server.jar fired a normal info message</terminationCode>
        protected virtual void ProcessInfoMessages(string message, Process proc)
        {
            TerminationCode = TerminationCode != 1 ? 0 : 1;

            // In some versions the EULA message is sent as INFO, so handle it here too
            if (message.ToLower().Contains("agree to the eula")) proc.KillProcessAndChildren();
            Logging.Logger.Info(message);
        }

        /// <summary>
        /// Processes any ERROR messages received from the server jar.
        /// Since we might be updating the console from another thread, we're just going to invoke everything
        /// and that's that.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>1 - The server.jar fired an error. If fired last, stop the build.</terminationCode>
        protected virtual void ProcessErrorMessages(string message, Process proc)
        {
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate
            {
                OutputConsole.SelectionColor = Color.Firebrick;
            }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate
            {
                OutputConsole.AppendText(Logging.Logger.Error("[ERROR] " + message) + Environment.NewLine);
            }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.SelectionColor = Color.Black; }));
        }

        /// <summary>
        /// Processes any WARN messages received from the server jar.
        /// Since we might be updating the console from another thread, we're just going to invoke everything
        /// and that's that.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>2 - The server.jar fired a warning</terminationCode>
        protected virtual void ProcessWarningMessages(string message, Process proc)
        {
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate
            {
                OutputConsole.SelectionColor = Color.OrangeRed;
            }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate
            {
                OutputConsole.AppendText(Logging.Logger.Warn("[WARN] " + message) + Environment.NewLine);
            }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.SelectionColor = Color.Black; }));
            TerminationCode = TerminationCode != 1 ? 2 : 1;
        }

        /// <summary>
        /// Processes any undifferentiated messages received from the server jar.
        /// Since we might be updating the console from another thread, we're just going to invoke everything
        /// and that's that.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>3 - The server.jar logged other messages</terminationCode>
        protected virtual void ProcessOtherMessages(string message, Process proc)
        {
            // Agreeing to the EULA is a special case, so we're going to handle it here.
            if (message.ToLower().Contains("agree to the eula")) proc.KillProcessAndChildren();

            // If the message contains the word "error" in it, we're going to assume it's an error.
            if (message.ToLower().Split(' ').Any(x => x.StartsWith("error")))
            {
                ProcessErrorMessages(message, proc);
                return;
            }

            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.SelectionColor = Color.Gray; }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate
            {
                OutputConsole.AppendText(Logging.Logger.Warn(message) + Environment.NewLine);
            }));
            Mainframe.INSTANCE.Invoke(new MethodInvoker(delegate { OutputConsole.SelectionColor = Color.Black; }));
            TerminationCode = TerminationCode != 1 ? 3 : 1;
        }
    }
}