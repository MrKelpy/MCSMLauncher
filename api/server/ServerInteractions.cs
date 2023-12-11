using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using LaminariaCore_General.utils;
using LaminariaCore_Winforms.common;
using MCSMLauncher.common;
using static MCSMLauncher.common.Constants;

namespace MCSMLauncher.api.server
{
    /// <summary>
    /// This class implements a bunch of methods aimed at interacting with the server, be it
    /// receiving or sending data from/to it.
    /// </summary>
    public class ServerInteractions
    {
        /// <summary>
        /// The server editor instance to use for the server interactions.
        /// </summary>
        private ServerEditor Editor { get; }

        /// <summary>
        /// The default buffer capacity for the server output buffer. Exists simply for optimisation
        /// purposes, since adding and removing items from a Queue within capacity is an O(1) operation, whilst
        /// doing so from a Queue without capacity is an O(n) operation.
        /// </summary>
        private const int BufferCapacity = 1000;

        /// <summary>
        /// A dictionary mapping a server to queue of messages received from the server,
        /// acting like a buffer for the server output.
        /// </summary>
        private static Dictionary<string, Queue<string>> ServerOutput => new (BufferCapacity);

        /// <summary>
        /// Main constructor for the ServerInteractions class. Sets the server editor instance to use. <br/>
        /// </summary>
        /// <param name="serverName">The name of the server to interact with</param>
        public ServerInteractions(string serverName)
        {
            Section serverSection = FileSystem.GetFirstSectionNamed(serverName);
            this.Editor = new ServerEditor(serverSection);
        }

        /// <summary>
        /// Adds a message into the ServerOutput buffer, to be processed later by any users of the API. <br/>
        /// When the buffer reaches the maximum capacity, the oldest  message is removed from the buffer.
        /// </summary>
        /// <param name="message">The message to add to the buffer.</param>
        public void AddToOutputBuffer(string message)
        {
            // Remove the oldest messages from the buffer until it frees up a space for the new message.
            if (this.GetOutputBuffer()?.Count >= BufferCapacity)
                for(int i = -1; i < this.GetOutputBuffer().Count - BufferCapacity; i++) this.GetOutputBuffer().Dequeue();
            
            this.GetOutputBuffer()?.Enqueue(message);
        }

        /// <summary>
        /// Clears the output buffer for the server by removing it from the buffer dictionary.
        /// </summary>
        public void ClearOutputBuffer() => ServerOutput.Remove(this.Editor.ServerSection.SimpleName);
        
        /// <returns>
        /// Returns a copy of the output buffer based on the server name, or an empty queue if the
        /// server name is invalid.
        /// </returns>
        public Queue<string> GetOutputBuffer() 
            => new (ServerOutput[this.Editor.ServerSection.SimpleName] ?? new (100));
        
        /// <returns>
        /// Returns the latest message from the output buffer, or null if the buffer is empty.
        /// </returns>
        public string GetLatestOutput() => this.GetOutputBuffer()?.LastOrDefault();

        /// <summary>
        /// Writes a message into the server's stdin. Normally used to send commands.<br/>
        /// </summary>
        /// <param name="message"></param>
        public void WriteToServerStdin(string message)
        {
            Process proc = this.GetServerProcess();
            if (proc == null) return;
            
            using StreamWriter stdin = proc.StandardInput;
            stdin.WriteLine(message);
        }
        
        /// <returns>
        /// Returns the server process associated with the server based on the editor instance. <br/>
        /// 
        /// This method may return a completely different process if the server is not running and the latest
        /// process id has been assigned to another process.
        /// </returns>
        private Process GetServerProcess()
            => ProcessUtils.GetProcessById(Editor.GetServerInformation().CurrentServerProcessID);

    }
}