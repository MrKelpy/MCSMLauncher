using System;
using System.Drawing;
using System.Windows.Forms;
using MCSMLauncher.utils;

namespace MCSMLauncher.common
{
    /// <summary>
    /// This class is responsible for acting as a driver between any passed output systems
    /// and the server message output processing system (Mostly handled by the AbstractLoggingMessageProcessing class).
    /// </summary>
    public class MessageProcessingOutputHandler
    {
        
        /// <summary>
        /// The output system to use for the message processing system.<br/>
        /// This may be STDOUT, a TextBox, or any other supported output system.
        /// </summary>
        private object OutputSystem { get; }

        /// <summary>
        /// Initialises the MessageProcessingOutputHandler class with the passed output system being STDOUT.
        /// </summary>
        /// <param name="output">The output system to use</param>
        public MessageProcessingOutputHandler(System.IO.TextWriter output) => this.OutputSystem = output;
        
        /// <summary>
        /// Initialises the MessageProcessingOutputHandler class with the passed output system being a TextBox.
        /// </summary>
        /// <param name="output">The output system to use</param>
        public MessageProcessingOutputHandler(TextBox output) => this.OutputSystem = output;
        
        /// <summary>
        /// Initialises the MessageProcessingOutputHandler class with no output system, meaning that we don't want any
        /// kind of logging to be done.
        /// </summary>
        public MessageProcessingOutputHandler() => this.OutputSystem = null;

        /// <summary>
        /// Decides which method to use to write the message to the output system based on the
        /// type of OutputSystem.
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="color">The color to write it as</param>
        public void Write(string message, Color color = new ())
        {
            switch (this.OutputSystem)
            {
                // Completely ignore the message if the output system is null
                case null: break;
                
                case Type when this.OutputSystem.GetType() == typeof(TextBox):
                    this.InternalWriteToTextBox(message, color);
                    break;
                
                case Type when this.OutputSystem.GetType() == typeof(System.IO.TextWriter):
                    this.InternalWriteToStdout(message, color);
                    break;
            }

        }
        
        /// <summary>
        /// Writes the message to STDOUT respecting the color.
        /// </summary>
        /// <param name="message">The message to be written to stdout</param>
        /// <param name="color">The color to paint the message with</param>
        private void InternalWriteToStdout(string message, Color color)
        {
            System.IO.TextWriter output = (System.IO.TextWriter) OutputSystem;
            output.WriteLine(message, color.IsEmpty ? Color.White : ColorUtils.ClosestConsoleColor(color));
        }

        /// <summary>
        /// Writes the message to a TextBox respecting the color.
        /// </summary>
        /// <param name="message">The message to be written to the text box</param>
        /// <param name="color">The color to paint the message with</param>
        private void InternalWriteToTextBox(string message, Color color)
        {
            TextBox output = (TextBox) OutputSystem;
            output.ForeColor = color.IsEmpty ? Color.Black : color;
            output.AppendText(message);
            output.ResetForeColor();
        }
    }
}