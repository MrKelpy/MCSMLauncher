using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MCSMLauncher.common.factories;
using MCSMLauncher.common.handlers;
using MCSMLauncher.common.server.starters.abstraction;

namespace MCSMLauncher.common.server.starters.threads
{
    /// <summary>
    /// This class is responsible for providing a NamedPipe server that allows processes to communicate directly
    /// with the minecraft server. <br/>
    ///
    /// This is needed because the redirected output stream is not able to be read from if we get the process
    /// externally, so the usage of sockets is necessary to setup a server that can directly communicate
    /// with the initialised process.
    /// </summary>
    public class ServerStarterThreadRunner
    {
        
        /// <summary>
        /// The minecraft server process to interact with.
        /// </summary>
        private Process MinecraftServer { get; set; }
        
        /// <summary>
        /// The server editor associated with the minecraft server to run
        /// </summary>
        private ServerEditor Editor { get; }

        /// <summary>
        /// Main constructor for the ServerStarterThreadRunner class. 
        /// </summary>
        /// <param name="editor">The server editor to be used</param>
        public ServerStarterThreadRunner(ServerEditor editor) => this.Editor = editor;
         
        /// <summary>
        /// Starts a new thread to run the minecraft server, complete with a pipe to enable sending messages
        /// to the server.
        /// </summary>
        /// <param name="outputSystem">The output system to use in order to log the messages</param>
        public void StartThread(MessageProcessingOutputHandler outputSystem) =>
            new Thread(() => this.InternalStartThread(outputSystem)).Start();

        /// <summary>
        /// Starts the server and the infinite cycle of listening to messages from the server
        /// until the process stops.
        /// </summary>
        /// <param name="outputSystem">The output system to use in order to log the messages</param>
        public async void InternalStartThread(MessageProcessingOutputHandler outputSystem)
        {
            // Gets the server starter to be used to run the server
            string serverType = this.Editor.GetServerInformation().Type;
            AbstractServerStarter serverStarter =
                new ServerTypeMappingsFactory().GetStarterFor(serverType, outputSystem);

            this.MinecraftServer = await serverStarter.Run(this.Editor); // Actually runs the server

            // Starts up the pipe server to listen to messages from the client
            PipeSecurity pipeRules = new PipeSecurity();
            pipeRules.AddAccessRule(new PipeAccessRule("Everyone", PipeAccessRights.ReadWrite | PipeAccessRights.CreateNewInstance, AccessControlType.Allow));
            pipeRules.AddAccessRule(new PipeAccessRule("SYSTEM", PipeAccessRights.FullControl, AccessControlType.Allow));
            
            using NamedPipeServerStream pipeServer = new("piped" + this.Editor.ServerSection.SimpleName,
                PipeDirection.In, 1, PipeTransmissionMode.Message, PipeOptions.WriteThrough, 1024, 1024, pipeRules);

            // Runs the server until the process it is contained in exits.
            while (!this.MinecraftServer.HasExited)
            {
                // Gets the next message sent through the pipe
                string message = this.GetNextPipedMessage(pipeServer);
                pipeServer.Disconnect();

                if (message == null) break;
                if (message == "") continue;

                this.WriteToServerStdin(message);
            }
        }

        /// <summary>
        /// Writes a message into the minecraft server's stdin.
        /// </summary>
        /// <param name="message">The message to be written</param>
        private async void WriteToServerStdin(string message)
        {
            using StreamWriter writer = this.MinecraftServer.StandardInput;
            await writer.WriteLineAsync(message);
        }

        /// <summary>
        /// Waits for a connection, and subsequently for the message it is sending through, returning it
        /// in the form of a string.
        /// </summary>
        /// <param name="server">The server to use to receive the messages</param>
        private string GetNextPipedMessage(NamedPipeServerStream server)
        {
            try
            {
                server.WaitForConnection(); // Blocks the thread until a connection is established.

                // Blocks the thread for 5 seconds, or until a message is sent through the pipe.
                using StreamReader reader = new StreamReader(server, Encoding.UTF8, true, 1024, true);
                return reader.ReadLine();
            }

            // If either an IOException or an OutOfMemoryException is thrown, return an empty string.
            catch (IOException e)
            {
                Logging.Logger.Error(e);
                return string.Empty;
            }

            catch (OutOfMemoryException e)
            {
                Logging.Logger.Error(e);
                return "NoMemory";
            }
            
            // If the pipe is disposed, disconnect the client.
            catch (ObjectDisposedException)
            { if (server.IsConnected) server.Disconnect(); }

            return null;
        }
        
    }
}