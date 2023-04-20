using System;
using System.Diagnostics;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using MCSMLauncher.gui;

namespace MCSMLauncher.common
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
        /// The console object to update with the logs.
        /// </summary>
        protected RichTextBox OutputConsole => NewServer.INSTANCE.RichTextBoxConsoleOutput;
        
        /// <summary>
        /// Due to the stupidity of early Minecraft logging, capture the STDERR and STDOUT in this method,
        /// and separate them by WARN, ERROR, and INFO messages, calling the appropriate methods.
        /// </summary>
        /// <param name="sender">The event sender</param>
        /// <param name="e">The event arguments</param>
        /// <param name="proc">The running process of the server</param>
        protected virtual void ProcessMergedData(object sender, DataReceivedEventArgs e, Process proc)
        {
            if (e.Data == null || e.Data.Trim().Equals(string.Empty)) return;
            Match matches = Regex.Match(e.Data.Trim(), @"^(?:\[[^\]]+\] \[[^\]]+\]: |[\d-]+ [\d:]+ \[[^\]]+\] )(.+)$", RegexOptions.Multiline);

            try
            {
                string typeSection = matches.Groups[0].Captures[0].Value;
                string message = matches.Groups[1].Captures[0].Value;
                
                if (typeSection.Contains("INFO")) ProcessInfoMessages(message, proc);
                if (typeSection.Contains("WARN")) ProcessWarningMessages(message, proc);
                if (typeSection.Contains("ERROR")) ProcessErrorMessages(message, proc);
                return;

            } catch (ArgumentOutOfRangeException) { }
            
            ProcessOtherMessages(e.Data, proc);
        }

        /// <summary>
        /// Processes any INFO messages received from the server jar.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>0 - The server.jar fired a normal info message</terminationCode>
        protected abstract void ProcessInfoMessages(string message, Process proc);

        /// <summary>
        /// Processes any ERROR messages received from the server jar.
        /// Since we might be updating the console from another thread, we're just going to invoke everything
        /// and that's that.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>1 - The server.jar fired an error. If fired last, stop the build.</terminationCode>
        protected abstract void ProcessErrorMessages(string message, Process proc);

        /// <summary>
        /// Processes any WARN messages received from the server jar.
        /// Since we might be updating the console from another thread, we're just going to invoke everything
        /// and that's that.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>2 - The server.jar fired a warning</terminationCode>
        protected abstract void ProcessWarningMessages(string message, Process proc);

        /// <summary>
        /// Processes any undifferentiated messages received from the server jar.
        /// Since we might be updating the console from another thread, we're just going to invoke everything
        /// and that's that.
        /// </summary>
        /// <param name="message">The logging message</param>
        /// <param name="proc">The object for the process running</param>
        /// <terminationCode>2 - The server.jar fired a warning</terminationCode>
        protected abstract void ProcessOtherMessages(string message, Process proc);

    }
}