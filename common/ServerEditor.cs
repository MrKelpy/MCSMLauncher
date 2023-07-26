using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using MCSMLauncher.common.models;
using MCSMLauncher.utils;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;

namespace MCSMLauncher.common
{
    /// <summary>
    /// Implements a bunch of methods to edit the server files. Specifically the server.properties and
    /// server_settings.xml files.
    /// </summary>
    public class ServerEditor
    {
        /// <summary>
        /// Main constructor for the ServerEditor class. Sets the server section to work with.
        /// </summary>
        /// <param name="serverSection"></param>
        public ServerEditor(Section serverSection)
        {
            ServerSection = serverSection;
        }

        /// <summary>
        /// The server section that the editor will work with.
        /// </summary>
        private Section ServerSection { get; }

        /// <summary>
        /// Reads the properties file and returns a dictionary with the key and value of each line.
        /// </summary>
        /// <returns>A dictionary containing the key:val's of the properties file</returns>
        public Dictionary<string, string> LoadProperties()
        {
            Logging.LOGGER.Info($"Loading properties for {ServerSection.SimpleName}");
            
            Dictionary<string, string> propertiesDictionary = new Dictionary<string, string>();
            string propertiesPath = ServerSection.GetFirstDocumentNamed("server.properties");

            if (propertiesPath == null) return propertiesDictionary;

            // Reads the file line by line, and adds the key and value to the dictionary.
            foreach (string line in FileUtils.ReadFromFile(propertiesPath))
            {
                if (line.StartsWith("#")) continue;
                string[] splitLine = line.Split('=');
                propertiesDictionary[splitLine[0]] = splitLine[1];
            }

            return propertiesDictionary;
        }

        /// <summary>
        /// Deserializes the server_settings.xml file and returns a dictionary with the key and value
        /// of each property.
        /// </summary>
        /// <returns>A dictionary containing the deserialized server_settings.xml</returns>
        public Dictionary<string, string> LoadSettings()
        {
            Logging.LOGGER.Info($"Loading settings for {ServerSection.SimpleName}");
            
            // Creates a new dictionary to store the settings in.
            Dictionary<string, string> settingsDictionary = new Dictionary<string, string>();
            string settingsPath = ServerSection.GetFirstDocumentNamed("server_settings.xml");

            // If the server_settings.xml doesn't exist, return an empty dictionary.
            if (settingsPath == null) return settingsDictionary;

            // If the server_settings.xml file exists, deserialize it and add the values to the dictionary. 
            ServerInformation info = ServerInformation.FromFile(settingsPath);

            info.GetType().GetProperties().ToList()
                .ForEach(x => settingsDictionary[x.Name.ToLower()] = x.GetValue(info)?.ToString() ?? "");

            // Adds the port with the key "baseport" to the dictionary.
            return settingsDictionary;
        }

        /// <summary>
        /// Loads the current form's properties into the server.properties file.
        /// </summary>
        /// <param name="dictionaryToLoad">The dictionary to load into the form</param>
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void DumpToProperties(Dictionary<string, string> dictionaryToLoad)
        {
            Logging.LOGGER.Info($"Updating properties for {ServerSection.SimpleName}");
            
            string propertiesFilepath = Path.Combine(ServerSection.SectionFullPath, "server.properties");
            if (!File.Exists(propertiesFilepath)) File.Create(propertiesFilepath).Close();

            List<string> propertiesFile = FileUtils.ReadFromFile(propertiesFilepath);

            // Iterates through the dictionary and replaces the line in the file with the same key
            for (int i = 0; i < dictionaryToLoad.Count; i++)
            {
                string key = dictionaryToLoad.Keys.ToArray()[i];
                int keyIndex = propertiesFile.FindIndex(x => x.ToLower().Contains(key));
                
                if (keyIndex != -1) propertiesFile[keyIndex] = $"{key}={dictionaryToLoad[key]}";
                else propertiesFile.Add($"{key}={dictionaryToLoad[key]}");
            }

            // Writes the new edited file contents to disk.
            FileUtils.DumpToFile(propertiesFilepath, propertiesFile);
        }

        /// <summary>
        /// Loads a dictionary into the server_settings.xml file, serializing it into a ServerInformation object.
        /// </summary>
        /// <param name="dictionaryToLoad">The dictionary to load into the file</param>
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void DumpToSettings(Dictionary<string, string> dictionaryToLoad)
        {
            Logging.LOGGER.Info($"Updating settings for {ServerSection.SimpleName}");
            
            // Get the path to the server_settings.xml file.
            string settingsFilepath = Path.Combine(ServerSection.SectionFullPath, "server_settings.xml");

            // Loads the information from the form into the ServerInformation object and serializes it again
            ServerInformation serverInformation = GetServerInformation(ServerSection);
            serverInformation.Update(dictionaryToLoad);

            // Writes the new edited file contents to disk.
            serverInformation.ToFile(settingsFilepath);
        }

        /// <summary>
        /// Handles the determination of the server port of a server, based on its defined base
        /// port in the server_settings.xml file.
        /// </summary>
        /// <returns>A code signaling the success of the operation.</returns>
        public int HandlePortForServer()
        {
            Dictionary<string, string> properties = LoadProperties();
            Dictionary<string, string> settings = LoadSettings();
            int port = settings.TryGetValue("baseport", out string setting) ? int.Parse(setting) : 25565;
            if (settings.ContainsKey("server-ip") && properties["server-ip"] != "") return 0;

            // Gets an available port starting on the one specified. If it's -1, it means that there are no available ports.
            int availablePort = NetworkUtils.GetNextAvailablePort(port);
            if (availablePort == -1) return 1;

            properties["server-port"] = availablePort.ToString();
            DumpToProperties(properties);
            return 0;
        }
        
        /// <summary>
        /// Returns the server information based on the server_settings.xml file, or creates a
        /// new one with minimal info.
        /// </summary>
        /// <param name="serverSection">The section to work with</param>
        /// <returns>The new server information instance</returns>
        public static ServerInformation GetServerInformation(Section serverSection)
        {
            // Check if the "server_settings.xml" file exists
            string settings = serverSection.GetFirstDocumentNamed("server_settings.xml");

            // If the file exists, load the server information from it
            if (settings != null) return ServerInformation.FromFile(settings);

            // If the file doesn't exist, create a new one with minimal information
            return new ServerInformation().GetMinimalInformation(serverSection);
        }
    }
}