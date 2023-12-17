using MCSMLauncher.common.models;

namespace MCSMLauncher.ui.console
{
    /// <summary>
    /// This class is responsible for parsing out the commands send from the console
    /// and forwarding them into the ConsoleCommandExecutor class, to be executed.
    /// </summary>
    public static class ConsoleCommandParser
    {
        /// <summary>
        /// Parses the command line arguments into a ConsoleCommand object.
        /// </summary>
        /// <param name="commandLineArguments">The command line arguments to parse</param>
        public static ConsoleCommand Parse(string[] commandLineArguments)
        {
            // Creates the command and arguments for the ConsoleCommand object.
            string command = commandLineArguments[0];
            string[] arguments = new string[commandLineArguments.Length - 1];
            
            // Adds the arguments to the array (skips the first one, which is the command).
            for (int i = 1; i < commandLineArguments.Length; i++)
                arguments[i - 1] = commandLineArguments[i];

            return new ConsoleCommand(command, arguments);
        }
    }
}