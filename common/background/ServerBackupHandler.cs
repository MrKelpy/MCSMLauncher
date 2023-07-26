using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading;
using Ionic.Zip;
using MCSMLauncher.common.interfaces;
using MCSMLauncher.utils;
using PgpsUtilsAEFC.common;
using PgpsUtilsAEFC.utils;

namespace MCSMLauncher.common.background
{
    /// <summary>
    /// Handles the backup creation for a given running server. This thread will bind itself to a running
    /// process, and frequently check for its status, stopping only when the process does.
    /// </summary>
    public class ServerBackupHandler : IBackgroundRunner
    {
        /// <summary>
        /// Main constructor for the ServerBackupHandler, sets the ServerSection and pid properties
        /// </summary>
        /// <param name="serverSection">The server section to work with</param>
        /// <param name="pid">The process ID, for status checking purposes</param>
        public ServerBackupHandler(Section serverSection, int pid)
        {
            this.ServerSection = serverSection;
            this.ProcessID = pid;
        }

        /// <summary>
        /// The server section bound to the handler instance, used to access the files to backup, as well as
        /// the settings.
        /// </summary>
        private Section ServerSection { get; }

        /// <summary>
        /// The Process ID of the running server, used to check if it still running.
        /// </summary>
        private int ProcessID { get; }

        /// <summary>
        /// Runs the thread until the bound process stops.
        /// </summary>
        public void RunTask()
        {
            Logging.LOGGER.Info($"Starting backup thread for server: '{this.ServerSection}'");
            
            // Loads the settings from the server section.
            Dictionary<string, string> settings = new ServerEditor(this.ServerSection).LoadSettings();
            bool serverBackupsEnabled = !settings.ContainsKey("serverbackupson") || bool.Parse(settings["serverbackupson"]);
            bool playerdataBackupsEnabled = !settings.ContainsKey("playerdatabackupson") || bool.Parse(settings["playerdatabackupson"]);

            // If neither of the backups are activated, stop the thread to save resources.
            if (!playerdataBackupsEnabled && !serverBackupsEnabled) return;
            
            // Get the server and playerdata backups paths, and create them if they don't exist.
            string serverBackupsPath = PathUtils.NormalizePath(settings["serverbackupspath"]);
            string playerdataBackupsPath = PathUtils.NormalizePath(settings["playerdatabackupspath"]);

            // Creates initial backups regardless of the current time.
            if (playerdataBackupsEnabled) CreatePlayerdataBackup(playerdataBackupsPath, this.ServerSection);
            if (serverBackupsEnabled) CreateServerBackup(serverBackupsPath, this.ServerSection);

            // Until the process is no longer active, keep creating backups.
            while (ProcessUtils.GetProcessById(this.ProcessID)?.ProcessName is var procName &&
                   (procName == "java" || procName == "cmd"))
            {
                DateTime now = DateTime.Now;

                // Creates a server backup if the current hour is divisible by 2 (every 2 hours)
                if (serverBackupsEnabled && now.Hour % 2 == 0 && now.Minute == 0)
                    CreateServerBackup(serverBackupsPath, this.ServerSection);

                // Creates a playerdata backup if the current min is divisible by 5 (every 5 minutes)
                if (playerdataBackupsEnabled && now.Minute % 5 == 0)
                    CreatePlayerdataBackup(playerdataBackupsPath, this.ServerSection);

                Thread.Sleep(1 * 1000 * 60); // Sleeps for a minute
            }
        }

        /// <summary>
        /// Creates a server backup by zipping the entirety of the server section into the
        /// specified server backups path.
        /// </summary>
        /// <param name="backupsPath">The server backups path</param>
        /// <param name="serverSection">The server section to use</param>
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private static void CreateServerBackup(string backupsPath, Section serverSection)
        {
            try
            {
                string backupName = DateTime.Now.ToString("yyyy-MM-dd.HH.mm.ss") + ".zip";
                if (!Directory.Exists(backupsPath)) Directory.CreateDirectory(backupsPath);

                ZipDirectory(serverSection.SectionFullPath, Path.Combine(backupsPath, backupName));
                Logging.LOGGER.Info($"Backed up {serverSection.Name} Server to {backupsPath}");
            }
            catch (Exception e)
            {
                Logging.LOGGER.Info("Failed to create server backup for server: " + serverSection.Name);
                Logging.LOGGER.Error(e);
            }
        }

        /// <summary>
        /// Creates a playerdata backup by zipping the world/playerdata files into the specified
        /// playerdata backups path.
        /// </summary>
        /// <param name="backupsPath">The playerdata backups path</param>
        /// <param name="serverSection">The server section to use</param>
        [SuppressMessage("ReSharper", "StringLiteralTypo")]
        private static void CreatePlayerdataBackup(string backupsPath, Section serverSection)
        {
            try
            {
                string backupName = DateTime.Now.ToString("yyyy-MM-dd.HH.mm.ss") + ".zip";
                if (!Directory.Exists(backupsPath)) Directory.CreateDirectory(backupsPath);

                // Gets all the 'playerdata' folders in the server, excluding the backups folder.
                List<Section> filteredSections = serverSection.GetSectionsNamed("playerdata")
                    .Where(section => !backupsPath.Contains(section.Name))
                    .ToList();

                // Creates a playerdata backup for every world in the server.
                foreach (Section section in filteredSections)
                {
                    string backupFileName =
                        $"{Path.GetFileName(Path.GetDirectoryName(section.SectionFullPath))}-{backupName}";
                    string backupFilePath = Path.Combine(backupsPath, backupFileName);

                    ZipDirectory(section.SectionFullPath, backupFilePath);
                    Logging.LOGGER.Info($"Backed up {serverSection.Name}.{section.Name} Playerdata to {backupsPath}");
                }

            }
            catch (Exception e)
            {
                Logging.LOGGER.Error($"Failed to create playerdata backup for server: {serverSection.Name}");
                Logging.LOGGER.Error(e);
            }
        }

        /// <summary>
        /// Zips an entire directory into another location.
        /// </summary>
        /// <param name="directory">The directory to use for zipping</param>
        /// <param name="destination">The destination, including the final filename for the zip.</param>
        private static void ZipDirectory(string directory, string destination)
        {
            // Creates and opens the zipping file, using a resource manager
            using ZipFile zipper = new ZipFile(destination);
            zipper.ZipErrorAction = ZipErrorAction.Skip;  // Skips any errors that may occur during the zipping process.

            // Adds every file into the zip file, parsing their path to exclude the root directory path.
            foreach (string file in Directory.GetFiles(directory, "*.*", SearchOption.AllDirectories))
            {
                string relativePath = file.Substring(directory.Length + 1);
                if (relativePath.Contains("backups") || relativePath.Contains(".lock")) continue;
                zipper.AddFile(file, Path.GetDirectoryName(relativePath));
            }

            zipper.Save();
        }
    }
}