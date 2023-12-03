using System;
using System.IO;
using LaminariaCore_General.utils;

namespace MCSMLauncher.common
{
    /// <summary>
    /// The logging type to be used in the logging methods.
    /// </summary>
    public enum LoggingType
    {
        CONSOLE,
        FILE,
        ALL
    }

    /// <summary>
    /// This is a custom logging class that implements a bunch of methods that are useful for logging purposes, with
    /// file logging support. Since this class is a singleton, the LOGGER property must be used to get the instance.
    /// </summary>
    public class Logging
    {
        /// <summary>
        /// Main constructor for the logging class, initializes the logging path.
        /// </summary>
        private Logging()
        {
            LoggingFilePath = Path.Combine(".", LoggingSession + ".log");
        }

        /// <summary>
        /// The logging instance to use in the program.
        /// </summary>
        public static Logging LOGGER { get; set; } = new Logging();

        /// <summary>
        /// The filepath for the logging file.
        /// </summary>
        public string LoggingFilePath { get; set; }

        /// <summary>
        /// The logging format for the console logs.
        /// </summary>
        public string ConsoleLoggingFormat { get; set; } = "[%DATE%] [%LEVEL%]: %MESSAGE%";

        /// <summary>
        /// The logging format for the console logs.
        /// </summary>
        public string FileLoggingFormat { get; set; } = "[%DATE%] [%LEVEL%]: %MESSAGE%";

        /// <summary>
        /// The current logging session, based on the current date.
        /// </summary>
        public string LoggingSession { get; set; } = DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss");

        /// <summary>
        /// Logs a message in a specified way, according to the set format, at the DEBUG level.
        /// </summary>
        /// <param name="message">The message to be logged</param>
        /// <param name="loggingType">The type of logging to be performed</param>
        public string Debug(string message, LoggingType loggingType = LoggingType.ALL)
        {
            return _internalLog(message, "DEBUG", loggingType);
        }

        /// <summary>
        /// Logs a message in a specified way, according to the set format, at the INFO level.
        /// </summary>
        /// <param name="message">The message to be logged</param>
        /// <param name="loggingType">The type of logging to be performed</param>
        public string Info(string message, LoggingType loggingType = LoggingType.ALL)
        {
            return _internalLog(message, "INFO", loggingType);
        }

        /// <summary>
        /// Logs a message in a specified way, according to the set format, at the WARN level.
        /// </summary>
        /// <param name="message">The message to be logged</param>
        /// <param name="loggingType">The type of logging to be performed</param>
        public string Warn(string message, LoggingType loggingType = LoggingType.ALL)
        {
            return _internalLog(message, "WARN", loggingType);
        }

        /// <summary>
        /// Logs a message in a specified way, according to the set format, at the ERROR level.
        /// </summary>
        /// <param name="message">The message to be logged</param>
        /// <param name="loggingType">The type of logging to be performed</param>
        public string Error(string message, LoggingType loggingType = LoggingType.ALL)
        {
            return _internalLog(message, "ERROR", loggingType);
        }
        
        /// <summary>
        /// Logs an error in a specified way, according to the set format, at the ERROR level.
        /// </summary>
        /// <param name="err">The error to be logged</param>
        /// <param name="loggingType">The type of logging to be performed</param>
        public string Error(Exception err, LoggingType loggingType = LoggingType.ALL)
        {
            return _internalLog(err.Message + '\n' + err.StackTrace, "ERROR", loggingType);
        }
        
        /// <summary>
        /// Logs a message in a specified way, according to the set format, at the FATAL level.
        /// </summary>
        /// <param name="message">The message to be logged</param>
        /// <param name="loggingType">The type of logging to be performed</param>
        public string Fatal(string message, LoggingType loggingType = LoggingType.ALL)
        {
            return _internalLog(message, "FATAL", loggingType);
        }
        
        /// <summary>
        /// Logs an error in a specified way, according to the set format, at the FATAL level.
        /// </summary>
        /// <param name="err">The error to be logged</param>
        /// <param name="loggingType">The type of logging to be performed</param>
        public string Fatal(Exception err, LoggingType loggingType = LoggingType.ALL)
        {
            return _internalLog(err.Message + '\n' + err.StackTrace, "FATAL", loggingType);
        }

        /// <summary>
        /// Logs the message based on the format, level, and logging type specified.
        /// </summary>
        /// <param name="message">The message to be logged</param>
        /// <param name="level">The level of logging to use</param>
        /// <param name="loggingType">The type of logging, either in a file, console, or both.</param>
        private string _internalLog(string message, string level, LoggingType loggingType)
        {
            try
            {
                string[] preparedStrings = _buildFormats(message, level);
                FileUtils.EnsurePath(LoggingFilePath);

                if (loggingType == LoggingType.FILE || loggingType == LoggingType.ALL)
                    FileUtils.AppendToFile(LoggingFilePath, preparedStrings[1]);

                if (loggingType == LoggingType.CONSOLE || loggingType == LoggingType.ALL)
                    Console.WriteLine(preparedStrings[0]);
            }
            catch (IOException err)
            {
                // ignored
            }

            return message;
        }

        /// <summary>
        /// Builds both the console logging string and the file logging string
        /// </summary>
        /// <param name="message">The message to be displayed in the log</param>
        /// <param name="level">The level of the log</param>
        /// <returns>A string[] with both the console and file strings</returns>
        private string[] _buildFormats(string message, string level)
        {
            string[] formats = new string[2];

            formats[0] = ConsoleLoggingFormat.Clone().ToString()
                .Replace("%DATE%", DateTime.Now.ToString("F"))
                .Replace("%LEVEL%", level)
                .Replace("%MESSAGE%", message);

            formats[1] = FileLoggingFormat.Clone().ToString()
                .Replace("%DATE%", DateTime.Now.ToString("F"))
                .Replace("%LEVEL%", level)
                .Replace("%MESSAGE%", message);

            return formats;
        }
    }
}