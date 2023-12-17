using System;
using System.Configuration;
using System.Linq;
using System.Reflection;
using MCSMLauncher.common;
using MCSMLauncher.common.handlers;
using MCSMLauncher.common.models;
using Medallion.Shell;

namespace MCSMLauncher.ui.console
{
    /// <summary>
    /// This class is responsible for taking commands through its methods and executing API calls
    /// to the backend.
    ///
    /// The methods created in here must use the provided API to interact with the backend, and their signature
    /// must be public void Command_(Capitalised Command Name) (ConsoleCommand command).
    /// </summary>
    public class ConsoleCommandExecutor
    {
        /// <summary>
        /// The output handler to use when executing commands.
        /// </summary>
        private readonly MessageProcessingOutputHandler OutputHandler = new (Console.Out);
        
        /// <summary>
        /// Using reflection, accesses all the methods within this class and tries to run the one matching
        /// the command name.
        /// If not found, write the help message.
        /// </summary>
        public void ExecuteCommand(ConsoleCommand command)
        {
            Logging.Logger.Info($"Executing command {command.Command} with arguments {command.Arguments}");
            
            try
            { 
                // Makes the first letter of the command name a capital letter to force-comply with the naming convention.
                string commandName = char.ToUpper(command.Command[0]) + command.Command.Substring(1);

                MethodInfo method = this.GetType().GetMethods()
                    .FirstOrDefault(method => method.Name == "Command_" + commandName.Replace("-", "_"));
                
                // If the method exists, run it and return.
                if (method != null)
                {
                    method.Invoke(this, new object[] {command});
                    return;
                }
                
                // If the method does not exist, write the help message.
                OutputHandler.Write($"Command '{command.Command}' not found. Use 'help' for a list of possible commands.");
            }
            
            // If the method throws an exception, try to expose the inner exception.
            catch (TargetInvocationException e)
            {
                if (e.InnerException != null) throw e.InnerException;
                throw;
            }
        }

        /// <summary>
        /// Shows a list of all the commands available, alongside their description and usage.
        /// </summary>
        private void Command_Help(ConsoleCommand command)
        {
            MethodInfo[] methods = this.GetType().GetMethods();
            OutputHandler.Write("MCSM Help Menu:");
            
            foreach (MethodInfo method in methods)
            {
                // If the method does not start with "Command_", then it is not a command.
                if (!method.Name.StartsWith("Command_")) continue;
                
                // Gets the command name and removes the "Command_" prefix.
                string commandName = method.Name.Substring(8);
                string description = ConfigurationManager.AppSettings.Get(method.Name + "_Description");
                string usage = ConfigurationManager.AppSettings.Get(method.Name + "_Usage");
                
                // Writes the command name, description and usage.
                string descriptionSpacing = new string(' ', 30 - commandName.Length);
                OutputHandler.Write($"- {commandName}{descriptionSpacing}{description}");
                OutputHandler.Write($"Usage: {usage}" + Environment.NewLine);
            }
        }





    }
}