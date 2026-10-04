using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.Json;

namespace Projet_app_configuration {
    public class ConfigSetting {
        string SMTPHostPrivate;
        public string SMTPHost {
            get { return this.SMTPHostPrivate; }
            set { SMTPHostPrivate = value; }
        }
        int SMTPPortPrivate;
        public int SMTPPort {
            get { return this.SMTPPortPrivate; }
            set { SMTPPortPrivate = value; }
        }
        string SMTPEmailPrivate;

        public string SMTPEmail {
            get { return this.SMTPEmailPrivate; }
            set { SMTPEmailPrivate = value; }
        }
        string SMTPPasswordPrivate;
        public string SMTPPassword {
            get { return this.SMTPPasswordPrivate; }
            set { SMTPPasswordPrivate = value; }
        }
        string EmailSenderUsersPrivate;
        public string EmailSenderUsers {
            get { return this.EmailSenderUsersPrivate; }
            set { EmailSenderUsersPrivate = value; }
        }
        int NBDevicePrivate;
        public int NBDevice {
            get { return this.NBDevicePrivate; }
            set { NBDevicePrivate = value; }
        }
        string LastDateSavePrivate;
        public string LastDateSave {
            get { return this.LastDateSavePrivate; }
            set { LastDateSavePrivate = value; }
        }
        BindingList<ConfigDevice> listDevicePrivate;
        public BindingList<ConfigDevice> listDevice {
            get { return this.listDevicePrivate; }
            set { listDevicePrivate = value; }
        }
        bool EnableSSLPrivate;
        public bool EnableSSL {
            get { return this.EnableSSLPrivate; }
            set { EnableSSLPrivate = value; }
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public ConfigSetting() {
            listDevice = new BindingList<ConfigDevice>();
        }
        /// <summary>
        /// Builds the path to the.json file
        /// </summary>
        /// <returns>le chemin du fichier .json</returns>
        public string GetPath() {
            string json = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..", "..","settings");
            string res = Path.GetFullPath(Path.Combine(json, "config.json"));
            return res;
        }
        /// <summary>
        /// Writes the ConfigSetting object to the config.json file
        /// </summary>
        public void WriteConfigInJson() {
            string text = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(GetPath(), text);
        }
        /// <summary>
        /// Reads the config.json file and creates an instance of the ConfigSetting class
        /// </summary>
        /// <returns>an instance of the ConfigSetting class if the config.json file exists, otherwise null</returns>
        public ConfigSetting ReadConfigInJson() {
            ConfigSetting config = null;
            if (File.Exists(GetPath())) {
                string jsonFile = File.ReadAllText(GetPath());
                config = JsonSerializer.Deserialize<ConfigSetting>(jsonFile);
            }

            return config;
        }
        /// <summary>
        /// Delete the config.json file
        /// </summary>
        public void DeleteFile() {
            File.Delete(GetPath());
        }

    }
}
