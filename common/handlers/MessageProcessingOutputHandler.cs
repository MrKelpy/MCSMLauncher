﻿using System.Drawing;
using System.Windows.Forms;
using MCSMLauncher.ui.graphical;
using MCSMLauncher.utils;

namespace MCSMLauncher.common.handlers
{
    /// <summary>
    /// This class is responsible for acting as a driver between any passed output systems
    /// and the server message target processing system (Mostly handled by the AbstractLoggingMessageProcessing class).
    /// </summary>
    public class MessageProcessingOutputHandler
    {
        /// <summary>
        /// The target output system to use for the message processing system.<br/>
        /// This may be STDOUT, a RichTextBox, or any other supported output system.
        /// </summary>
        public object TargetSystem { get; }

        /// <summary>
        /// Initialises the MessageProcessingOutputHandler class with the passed target output system being STDOUT.
        /// </summary>
        /// <param name="target">The output system to use</param>
        public MessageProcessingOutputHandler(System.IO.TextWriter target) => this.TargetSystem = target;
        
        /// <summary>
        /// Initialises the MessageProcessingOutputHandler class with the passed target output system being a RichTextBox.
        /// </summary>
        /// <param name="target">The output system to use</param>
        public MessageProcessingOutputHandler(RichTextBox target) => this.TargetSystem = target;
        
        /// <summary>
        /// Initialises the MessageProcessingOutputHandler class with no target output system, meaning that we don't want any
        /// kind of logging to be done.
        /// </summary>
        public MessageProcessingOutputHandler() => this.TargetSystem = null;

        /// <summary>
        /// Decides which method to use to write the message to the target output system based on the
        /// type of TargetSystem.
        /// </summary>
        /// <param name="message">The message to write</param>
        /// <param name="color">The color to write it as</param>
        public void Write(string message, Color color = new ())
        {
            switch (this.TargetSystem)
            {
                case var _ when this.TargetSystem?.GetType() == typeof(RichTextBox):
                    this.InternalWriteToTextBox(message, color);
                    break;
                
                case var _ when this.TargetSystem?.GetType() == typeof(System.IO.TextWriter):
                    this.InternalWriteToStdout(message, color);
                    break;
                
                // Explicitly ignore null cases (no target system) for clarity
                case null: break;
            }
        }
        
        /// <summary>
        /// Writes the message to STDOUT respecting the color.
        /// </summary>
        /// <param name="message">The message to be written to stdout</param>
        /// <param name="color">The color to paint the message with</param>
        private void InternalWriteToStdout(string message, Color color)
        {
            System.IO.TextWriter output = (System.IO.TextWriter) TargetSystem;
            output.WriteLine(message, color.IsEmpty ? Color.White : ColorUtils.ClosestConsoleColor(color));
        }

        /// <summary>
        /// Writes the message to a RichTextBox respecting the color.
        /// </summary>
        /// <param name="message">The message to be written to the text box</param>
        /// <param name="color">The color to paint the message with</param>
        private void InternalWriteToTextBox(string message, Color color)
        {
            void Process()
            {
                RichTextBox output = (RichTextBox) TargetSystem;
                output.SelectionColor = color.IsEmpty ? Color.Black : color;
                output.AppendText(message);
                output.SelectionColor = Color.Black;
            }

            Mainframe.INSTANCE.Invoke((MethodInvoker) Process);
        }
    }
}