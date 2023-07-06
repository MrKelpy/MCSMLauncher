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
        /// The server section that the editor will work with.
        /// </summary>
        private Section ServerSection { get; }
        
        /// <summary>
        /// Main constructor for the ServerEditor class. Sets the server section to work with.
        /// </summary>
        /// <param name="serverSection"></param>
        public ServerEditor(Section serverSection) => ServerSection = serverSection;

        /// <summary>
        /// Reads the properties file and returns a dictionary with the key and value of each line.
        /// </summary>
        /// <returns>A dictionary containing the key:val's of the properties file</returns>
        public Dictionary<string, string> LoadProperties()
        {
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
            Dictionary<string, string> settingsDictionary = new Dictionary<string, string>();
            string settingsPath = ServerSection.GetFirstDocumentNamed("server_settings.xml");

            // If the server_settings.xml doesn't exist, return an empty dictionary.
            if (settingsPath == null) return settingsDictionary;
            
            // If the server_settings.xml file exists, deserialize it and add the values to the dictionary. 
            ServerInformation info = XMLUtils.DeserializeFromFile<ServerInformation>(settingsPath);
            
            info.GetType().GetProperties().Where(x => x.Name.ToLower() != "port")
                .ToList().ForEach(x => settingsDictionary[x.Name.ToLower()] =  x.GetValue(info)?.ToString() ?? "");
            
            // Adds the port with the key "base-port" to the dictionary.
            settingsDictionary.Add("base-port", info.Port.ToString());
            return settingsDictionary;
        }

        /// <summary>
        /// Loads the current form's properties into the server.properties file.
        /// </summary>
        /// <param name="dictionaryToLoad">The dictionary to load into the form</param>
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        public void DumpToProperties(Dictionary<string, string> dictionaryToLoad)
        {
            string propertiesFilepath = Path.Combine(ServerSection.SectionFullPath, "server.properties");
            if (!File.Exists(propertiesFilepath)) File.Create(propertiesFilepath).Close();
            
            List<string> propertiesFile = FileUtils.ReadFromFile(propertiesFilepath);

            // Iterates through the dictionary and replaces the line in the file with the same key
            for (var i = 0; i < dictionaryToLoad.Count; i++)
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
            string settingsFilepath = Path.Combine(ServerSection.SectionFullPath, "server_settings.xml");
            if (!File.Exists(settingsFilepath)) XMLUtils.SerializeToFile<ServerInformation>(settingsFilepath, new ServerInformation().GetMinimalInformation(ServerSection));
            
            // Loads the information from the form into the ServerInformation object and serializes it again
            ServerInformation updatedServerInformation = XMLUtils.DeserializeFromFile<ServerInformation>(settingsFilepath);
            updatedServerInformation.Port = int.Parse(dictionaryToLoad["base-port"]);
            updatedServerInformation.Ram = int.Parse(dictionaryToLoad["ram"]);
            updatedServerInformation.PlayerdataBackupsPath = dictionaryToLoad["playerdatabackupspath"];
            updatedServerInformation.ServerBackupsPath = dictionaryToLoad["serverbackupspath"];
            updatedServerInformation.PlayerdataBackupsOn = bool.Parse(dictionaryToLoad["playerdatabackupson"]);
            updatedServerInformation.ServerBackupsOn = bool.Parse(dictionaryToLoad["serverbackupson"]);
            updatedServerInformation.CurrentServerProcessID = int.Parse(dictionaryToLoad["currentserverprocessid"]);
            updatedServerInformation.JavaRuntimePath = dictionaryToLoad["javaruntimepath"];

            // Writes the new edited file contents to disk.
            File.Delete(settingsFilepath);
            XMLUtils.SerializeToFile<ServerInformation>(settingsFilepath, updatedServerInformation);
        }

        /// <summary>
        /// Handles the determination of the server port of a server, based on its defined base
        /// port in the server_settings.xml file.
        /// </summary>
        /// <param name="serverSection">The server section to work on</param>
        /// <returns>A code signaling the success of the operation.</returns>
        public int HandlePortForServer(Section serverSection)
        {
            Dictionary<string, string> properties = this.LoadProperties();
            Dictionary<string, string> settings = this.LoadSettings();
            int port = settings.TryGetValue("base-port", out var setting) ? int.Parse(setting) : 25565;
            if (settings.ContainsKey("server-ip") && properties["server-ip"] != "") return 0;

            // Gets an available port starting on the one specified. If it's -1, it means that there are no available ports.
            int availablePort = NetworkUtils.GetNextAvailablePort(port);
            if (availablePort == -1) return 1;

            properties["server-port"] = availablePort.ToString();
            this.DumpToProperties(properties);
            return 0;
        }
        
    }
}