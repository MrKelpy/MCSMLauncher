using System;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MCSMLauncher.api.server;
using MCSMLauncher.common.factories;
using MCSMLauncher.common.handlers;
using MCSMLauncher.common.server.starters.abstraction;
using MCSMLauncher.extensions;

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
        /// The server interactions API to use to interact with the server
        /// </summary>
        private ServerInteractions InteractionsAPI { get; }


        /// <summary>
        /// Main constructor for the ServerStarterThreadRunner class. 
        /// </summary>
        /// <param name="editor">The server editor to be used</param>
        public ServerStarterThreadRunner(ServerEditor editor)
        {
            this.InteractionsAPI = new ServerAPI().Interactions(editor.ServerSection.SimpleName);
            this.Editor = editor;
        }

        /// <summary>
        /// Starts a new thread to run the minecraft server, complete with a pipe to enable sending messages
        /// to the server.
        /// </summary>
        /// <param name="outputSystem">The output system to use in order to log the messages</param>
        public void StartThread(MessageProcessingOutputHandler outputSystem) =>
            new Thread(() => this.InternalStartThread(outputSystem)) { IsBackground = false }.Start();

        /// <summary>
        /// Starts the server and the infinite cycle of listening to messages from the server
        /// until the process stops.
        /// </summary>
        /// <param name="outputSystem">The output system to use in order to log the messages</param>
        private async void InternalStartThread(MessageProcessingOutputHandler outputSystem)
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
            
            using NamedPipeServerStream pipeServer = new("piped" + this.Editor.ServerSection.SimpleName, PipeDirection.In,
                1, PipeTransmissionMode.Message, PipeOptions.WriteThrough | PipeOptions.Asynchronous, 1024, 1024, pipeRules);

            // Starts listening for a connection to the pipe server
            pipeServer.BeginWaitForConnection(HandleConnectionCallback, pipeServer);
            Logging.Logger.Info("Initialised pipe connection for server " + Editor.ServerSection.SimpleName, LoggingType.File);

            // Runs the server until the process it is contained in exits.
            this.MinecraftServer.WaitForExit();
            InteractionsAPI.ClearOutputBuffer();
            pipeServer.Close();
        }

        /// <summary>
        /// Handles the callback for the pipe server, which is called whenever a message is sent through the pipe.
        /// This should be used within the asynchronous method BeginWaitForConnection. 
        /// </summary>
        /// <param name="data">The async result data containing the server and the information</param>
        private async void HandleConnectionCallback(IAsyncResult data)
        {
            NamedPipeServerStream pipeServer = (NamedPipeServerStream) data.AsyncState;

            try
            {
                // Stops waiting for a connection, and closes the pipe if the server is not connected.
                pipeServer.EndWaitForConnection(data);

                // Gets the next message sent through the pipe
                string message = await this.GetNextPipedMessage(pipeServer);
                Logging.Logger.Info("Received message from pipe: " + message, LoggingType.File);

                // If the message is empty, return, otherwise write it to the server's stdin
                if (pipeServer.IsConnected) pipeServer.Disconnect();
                if (string.IsNullOrEmpty(message)) return;
                this.WriteToServerStdin(message);
            }

            // If an IOException is thrown, log it and restart the pipe server.
            catch (IOException e)
            {
                Logging.Logger.Error(e);
            }

            // If an ObjectDisposedException is thrown, log it and restart the pipe server.
            catch (ObjectDisposedException e)
            {
                Logging.Logger.Error(e);
            }

            // If an OutOfMemoryException is thrown, log it and restart the pipe server.
            catch (OutOfMemoryException e)
            {
                Logging.Logger.Error(e);
            }

            // Restarts the pipe server.
            try { pipeServer.BeginWaitForConnection(HandleConnectionCallback, pipeServer); }
            catch (IOException) {}
            catch (ObjectDisposedException) {}
        }

        /// <summary>
        /// Waits for the message the connection is sending through, returning it in the form of a string.
        /// </summary>
        /// <param name="server">The server to use to receive the messages</param>
        private async Task<string> GetNextPipedMessage(NamedPipeServerStream server)
        {
            using StreamReader reader = new StreamReader(server, Encoding.UTF8, true, 1024, true);
            return await reader.ReadLineAsync();
        }
        
        /// <summary>
        /// Writes a message into the minecraft server's stdin.
        /// </summary>
        /// <param name="message">The message to be written</param>
        private async void WriteToServerStdin(string message)
        {
            try
            {
                using StreamWriter writer = this.MinecraftServer.StandardInput;
                await writer.WriteLineAsync(message);
                Logging.Logger.Info("Sent message to server: <<" + message + ">>", LoggingType.File);
            }
            
            catch (Exception e) { Logging.Logger.Error(e); }
        }
        
    }
}