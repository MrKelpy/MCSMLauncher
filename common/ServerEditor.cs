#nullable enable
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using LaminariaCore_General.utils;
using LaminariaCore_Winforms.common;
using MCSMLauncher.common.models;
using MCSMLauncher.ui.graphical;
using NetworkUtils = MCSMLauncher.utils.NetworkUtils;

namespace MCSMLauncher.common
{
    /// <summary>
    /// Implements a bunch of methods to edit the server files. Specifically the server.properties and
    /// server_settings.xml files.
    /// This can be considered an abstraction layer over ServerInformation and every interaction with server.properties.
    /// </summary>
    public class ServerEditor
    {
        /// <summary>
        /// Main constructor for the ServerEditor class. Sets the server section to work with.
        /// </summary>
        /// <param name="serverSection">The server section to bind to the editor</param>
        public ServerEditor(Section serverSection)
        {
            ServerSection = serverSection;
            this.SettingsBuffer = LoadSettings();
            this.PropertiesBuffer = LoadProperties();
        }

        /// <summary>
        /// The server section that the editor will work with.
        /// </summary>
        public Section ServerSection { get; }
        
        /// <summary>
        /// A buffer to store the changes made to the setting files. This is useful because it allows
        /// us to cache in changes and then write them all at once.
        /// </summary>
        private Dictionary<string, string> SettingsBuffer { get; }
        
        /// <summary>
        /// A buffer to store the changes made to the property files. This is useful because it allows
        /// us to cache in changes and then write them all at once.
        /// </summary>
        private Dictionary<string, string> PropertiesBuffer { get; }

        /// <summary>
        /// Selects which buffers should be updated based on the given dictionary's keys, and updates them.
        /// This method is specifically written so you don't have to worry about dictionary formats
        /// nor where they go. It's all handled automatically.
        /// </summary>
        /// <param name="dictionary">The dictionary to update the buffers with</param>
        public void UpdateBuffers(Dictionary<string, string> dictionary)
        {
            foreach (KeyValuePair<string, string> item in dictionary)
                
                // Updates the key for the server properties
                if (PropertiesBuffer.ContainsKey(item.Key))
                    PropertiesBuffer[item.Key] = item.Value;

                // Updates the key for the server settings
                else if (SettingsBuffer.ContainsKey(item.Key))
                    SettingsBuffer[item.Key] = item.Value;
                
                // If the ServerInformation class contains the key, update it in the settings buffer.
                else if (typeof(ServerInformation).GetFields().ToList().Exists(field => field.Name.ToLower().Equals(item.Key)))
                    SettingsBuffer[item.Key] = item.Value;

                // If the key doesn't exist in either of the buffers, then it might just be a new key for the properties file.
                else
                {
                    Logging.LOGGER.Warn($"Tried to update non-existent key: '{item.Key}' - Adding to the properties.");
                    PropertiesBuffer.Add(item.Key, item.Value);
                }
        }
        
        /// <summary>
        /// Commits the changes made to the server_settings.xml and server.properties files.
        /// </summary>
        public void FlushBuffers()
        {
            DumpToProperties();
            DumpToSettings();
        }

        /// <summary>
        /// Looks inside both buffers for the given key, and returns the value of the first one found.
        /// </summary>
        /// <param name="key">The key to look for</param>
        /// <returns>The value for the requested key</returns>
        public string? GetFromBuffers(string key)
        {
            if (SettingsBuffer.TryGetValue(key, out string? settings)) return settings;
            if (PropertiesBuffer.TryGetValue(key, out string? properties)) return properties;
            return null;
        }
        
        /// <summary>
        /// Looks inside both buffers for the given key, and returns the value of the first one found, parsed
        /// to the given type parameter.
        /// </summary>
        /// <param name="key">The key to look for</param>
        /// <typeparam name="T">The type that the object should be returned as</typeparam>
        /// <returns>The value for the requested key</returns>
        public T? GetFromBuffers<T>(string key)
        {
            if (SettingsBuffer.TryGetValue(key, out string? settings)) return (T) Convert.ChangeType(settings, typeof(T));
            if (PropertiesBuffer.TryGetValue(key, out string? properties)) return (T) Convert.ChangeType(properties, typeof(T));
            return default;
        }
        
        /// <summary>
        /// Checks if the buffers contain the given key.
        /// </summary>
        /// <param name="key">The key to look for</param>
        /// <returns>Whether or not the buffers contain the specified key</returns>
        public bool BuffersContain(string key) => SettingsBuffer.ContainsKey(key) || PropertiesBuffer.ContainsKey(key);

        /// <summary>
        /// Returns a copy of the properties and settings buffers as a dictionary.
        /// </summary>
        /// <returns>A Dictionary containing a deep copy of the buffers</returns>
        public Dictionary<string, string> GetBuffersCopy() => 
            new (SettingsBuffer.Concat(PropertiesBuffer).ToDictionary(pair => pair.Key, pair => pair.Value));

        /// <summary>
        /// Handles the determination of the server port of a server, based on its defined base
        /// port in the server_settings.xml file.
        /// This method sneakily automatically updates the buffers with the new port.
        /// </summary>
        /// <returns>A code signaling the success of the operation.</returns>
        public int HandlePortForServer()
        {
            int port = BuffersContain("baseport") ? GetFromBuffers<int>("baseport") : 25565;
            if (BuffersContain("server-ip") && GetFromBuffers("server-ip") != "") return 0;

            // Gets an available port starting on the one specified. If it's -1, it means that there are no available ports.
            int availablePort = NetworkUtils.GetNextAvailablePort(port);
            if (availablePort == -1) return 1;

            // Updates and flushes the buffers with the new port.
            PropertiesBuffer["server-port"] = SettingsBuffer["port"] = availablePort.ToString();
            FlushBuffers();
            
            return 0;
        }
        
        /// <summary>
        /// Returns the server information based on the server_settings.xml file, or creates a
        /// new one with minimal info.
        /// This method is used to decrease IO operations, and to reduce the complexity of the code.
        /// Since the ServerInformation instances can be converted to dictionaries, there's no reason to manually create them anymore.
        /// </summary>
        /// <returns>The current server information</returns>
        public ServerInformation GetServerInformation()
        {
            // If settings buffer is not empty, return a new server information instance with the settings buffer
            if (SettingsBuffer.Keys.Count > 0) return new ServerInformation().Update(SettingsBuffer);

            // If it is empty, return a new server information instance with the minimal information
            return new ServerInformation().GetMinimalInformation(ServerSection);
        }
        
        /// <summary>
        /// Reads the properties file and returns a dictionary with the key and value of each line.
        /// </summary>
        /// <returns>A dictionary containing the key:val's of the properties file</returns>
        private Dictionary<string, string> LoadProperties()
        {
            Logging.LOGGER.Info($"Loading properties for {ServerSection.SimpleName}");
            
            // Creates a new dictionary to store the properties.
            Dictionary<string, string> propertiesDictionary = new () { { "server-port", "25565" } };
            string propertiesPath = ServerSection.GetFirstDocumentNamed("server.properties");
            
            // Gets the keys eligible to be edited by the user through the mcsm.
            List<string> propertiesMask = ServerEditPrompt.GetTags();
            
            if (propertiesPath == null) return propertiesDictionary;

            // Reads the file line by line, and adds the key and value to the dictionary.
            foreach (string line in FileUtils.ReadFromFile(propertiesPath))
            {
                if (line.StartsWith("#")) continue;
                string[] splitLine = line.Split(new [] {"="}, 2, StringSplitOptions.None);
                
                // Filters out the keys that are not eligible to be edited by the user.
                if (!propertiesMask.Contains(splitLine[0])) continue;
                propertiesDictionary[splitLine[0]] = splitLine[1];
            }

            return propertiesDictionary;
        }

        /// <summary>
        /// Deserializes the server_settings.xml file and returns a dictionary with the key and value
        /// of each property.
        /// </summary>
        /// <returns>A dictionary containing the deserialized server_settings.xml</returns>
        private Dictionary<string, string> LoadSettings()
        {
            Logging.LOGGER.Info($"Loading settings for {ServerSection.SimpleName}");

            // Gets the path to the server_settings.xml file.
            string settingsPath = ServerSection.GetFirstDocumentNamed("server_settings.xml");

            // If the server_settings.xml doesn't exist, return an empty dictionary.
            if (settingsPath == null)
                return new ServerInformation().GetMinimalInformation(ServerSection).ToDictionary();

            // If the server_settings.xml file exists, deserialize it and return it as a dictionary.
            ServerInformation info = XmlUtils.DeserializeFromFile<ServerInformation>(settingsPath);
            return info.ToDictionary();
        }

        /// <summary>
        /// Loads the current form's properties into the server.properties file.
        /// </summary>
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private void DumpToProperties()
        {
            Logging.LOGGER.Info($"Updating properties for {ServerSection.SimpleName}");
            
            // Gets the path to the server.properties file.
            string propertiesFilepath = Path.Combine(ServerSection.SectionFullPath, "server.properties");
            if (!File.Exists(propertiesFilepath)) File.Create(propertiesFilepath).Close();

            List<string> propertiesFile = FileUtils.ReadFromFile(propertiesFilepath);

            // Iterates through the dictionary and replaces the line in the file with the same key
            for (int i = 0; i < PropertiesBuffer.Count; i++)
            {
                string key = PropertiesBuffer.Keys.ToArray()[i];
                int keyIndex = propertiesFile.FindIndex(x => x.ToLower().Contains(key));
                
                if (keyIndex != -1) propertiesFile[keyIndex] = $"{key}={PropertiesBuffer[key]}";
                else propertiesFile.Add($"{key}={PropertiesBuffer[key]}");
            }

            // Writes the new edited file contents to disk.
            FileUtils.DumpToFile(propertiesFilepath, propertiesFile);
        }

        /// <summary>
        /// Loads a dictionary into the server_settings.xml file, serializing it into a ServerInformation object.
        /// </summary>
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private void DumpToSettings()
        {
            Logging.LOGGER.Info($"Updating settings for {ServerSection.SimpleName}");
            
            // Get the path to the server_settings.xml file.
            string settingsFilepath = Path.Combine(ServerSection.SectionFullPath, "server_settings.xml");
            
            // Writes the ServerInformation into the file
            if (File.Exists(settingsFilepath)) File.Delete(settingsFilepath);
            XmlUtils.SerializeToFile<ServerInformation>(settingsFilepath, GetServerInformation());
            
            // Sets the settings file to hidden if it isn't already.
            File.SetAttributes(settingsFilepath, File.GetAttributes(settingsFilepath) | FileAttributes.Hidden);
        }
    }
}