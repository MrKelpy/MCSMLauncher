using System.Collections.Generic;
using System.Linq;

namespace MCSMLauncher.common.models
{
    /// <summary>
    /// This class is responsible for holding the information about a console command.  
    /// </summary>
    public class ConsoleCommand
    {
        /// <summary>
        /// The command in itself.
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// The arguments for the command.
        /// </summary>
        public string[] Arguments { get; set; }
        
        /// <summary>
        /// Main constructor for the ConsoleCommand class. Sets the command and arguments.
        /// </summary>
        /// <param name="command">The command to be stored (normally the first argument passed into stdin)</param>
        /// <param name="arguments">
        /// The arguments to be used. Note that the arguments may be fields, such as "--(field) [value]",
        /// so using the raw arguments may not be the best option.
        /// </param>
        public ConsoleCommand(string command, string[] arguments)
        {
            Command = command;
            Arguments = arguments;
        }
        
        /// <summary>
        /// Returns the string value of the next argument after the given field, that is searched for
        /// automatically based on a name.
        /// </summary>
        /// <param name="field">The field name to search for</param>
        /// <returns>Either the field value or null if not found</returns>
        public string GetValueForField(string field)
        {
            int fieldIndex = this.Arguments.ToList().IndexOf("--" + field);
            if (fieldIndex == -1) return null;
            
            return this.Arguments[fieldIndex + 1];
        }
        
        /// <summary>
        /// Returns the string value of the argument modifier, the value present after the "=" sign.
        /// </summary>
        /// <param name="argument">The argument to get the value for</param>
        /// <returns>Either the modifier value or null if not found</returns>
        public string GetArgumentModifier(string argument) => 
            argument.Contains("=") ? argument?.Split('=')[1] : null;
    }
}