using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace BackupAndClean {
    public class BackupSetting {

        string sourcePathBackupPrivate;
        public string sourcePathBackup
        {
            get { return this.sourcePathBackupPrivate; }
            set { sourcePathBackupPrivate = value; }
        }
        string destinationPathPrivate;
        public string destinationPathBackup
        {
            get { return this.destinationPathPrivate; }
            set { destinationPathPrivate = value; }
        }
        List<string> pathToIgnorePrivate;
        public List<string> pathToIgnore
        {
            get { return this.pathToIgnorePrivate; }
            set { pathToIgnorePrivate = value; }
        }
        string sourcePathRestorePrivate;
        public string sourcePathRestore
        {
            get { return this.sourcePathRestorePrivate; }
            set { sourcePathRestorePrivate = value; }
        }
        string destinationPathRestorePrivate;
        public string destinationPathRestore
        {
            get { return this.destinationPathRestorePrivate; }
            set { destinationPathRestorePrivate = value; }
        }
        string SMTPHostPrivate;
        public string SMTPHost
        {
            get { return this.SMTPHostPrivate; }
            set { SMTPHostPrivate = value; }
        }
        int SMTPPortPrivate;
        public int SMTPPort
        {
            get { return this.SMTPPortPrivate; }
            set { SMTPPortPrivate = value; }
        }
        string SMTPEmailPrivate;

        public string SMTPEmail
        {
            get { return this.SMTPEmailPrivate; }
            set { SMTPEmailPrivate = value; }
        }
        string SMTPPasswordPrivate;
        public string SMTPPassword
        {
            get { return this.SMTPPasswordPrivate; }
            set { SMTPPasswordPrivate = value; }
        }
        string EmailSenderUsersPrivate;
        public string EmailSenderUsers
        {
            get { return this.EmailSenderUsersPrivate; }
            set { EmailSenderUsersPrivate = value; }
        }
        
        /// <summary>
        /// Default constructor
        /// </summary>
        public BackupSetting()
        {
            pathToIgnore = new List<string>();
        }
        /// <summary>
        /// Constructor of copy
        /// </summary>
        /// <param name="backup"></param>
        public BackupSetting(BackupSetting backup)
        {
            this.sourcePathBackup = backup.sourcePathBackup;
            this.destinationPathBackup = backup.destinationPathBackup;
            foreach (string i in backup.pathToIgnore)
            {
                this.pathToIgnore.Add(i);
            }
            this.sourcePathRestore = backup.sourcePathRestore;
            this.destinationPathRestore = backup.destinationPathRestore;
            this.SMTPHost = backup.SMTPHost;
            this.SMTPPort = backup.SMTPPort;
            this.SMTPEmail = backup.SMTPEmail;
            this.SMTPPassword = backup.SMTPPassword;
            this.EmailSenderUsers = backup.EmailSenderUsers;
        }
        /// <summary>
        /// Builds the path to the.json file
        /// </summary>
        /// <returns>le chemin du fichier .json</returns>
        public string getPath()
        {
            string json = Path.Combine(AppDomain.CurrentDomain.BaseDirectory,"..", "..", "..", "..");
            string res = Path.GetFullPath(Path.Combine(json, "config.json"));
            return res;
        }
        /// <summary>
        /// Writes the BackupSetting object to the config.json file
        /// </summary>
        public async Task writeConfigInJson()
        {
            string text = JsonSerializer.Serialize(this);
            await File.WriteAllTextAsync(getPath(), text);
        }
        /// <summary>
        /// Reads the config.json file and creates an instance of the BackupSetting class
        /// </summary>
        /// <returns>an instance of the BackupSetting class if the config.json file exists, otherwise null</returns>
        public BackupSetting readConfigInJson()
        {
            BackupSetting config = null;
            if (File.Exists(getPath()))
            {
                string jsonFile = File.ReadAllText(getPath());
                config = JsonSerializer.Deserialize<BackupSetting>(jsonFile);
            }

            return config;
        }
        /// <summary>
        /// Delete the config.json file
        /// </summary>
        public void deleteFile()
        {
            File.Delete(getPath());
        }

    }
}
