using Projet_app_configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Projet_app_principale {
    public class PanelManager {
        List<Panel> ListPanel { get; set; }
        public TaskManager taskManager { get; private set; }
        public BottomPanel BottomPanel { get; private set; }
        NoConfigPanel NoConfigPanel;
        public PingPanel PingPanel { get; private set; }
        public ConfigSetting config;
        /// <summary>
        /// Construct the panel manager
        /// Init the TaskManager when the config is ok
        /// </summary>
        /// <param name="form">This is the form window</param>
        public PanelManager(PingMonitoringForm form) {
            ListPanel = new List<Panel>();
            BottomPanel = new BottomPanel(form,this,true);
            config = new ConfigSetting();
            config = config.ReadConfigInJson();
            if(config != null) {
                bool configOk = false;
                if (config.listDevice.Count > 0 && config.listDevice.Count <=8 && !string.IsNullOrEmpty(config.SMTPEmail) && !string.IsNullOrEmpty(config.SMTPHost) && !string.IsNullOrEmpty(config.SMTPPassword) && config.SMTPPort > 0) {
                    configOk = config.listDevice.Any(c => !string.IsNullOrEmpty(c.Name) && !string.IsNullOrEmpty(c.IPAddress) && c.SecondInterval > 0);
                }
                if (configOk) {
                    PingPanel = new PingPanel(form, this, true);
                    taskManager = new TaskManager(config);
                    BindingList<ConfigDevice> devices = GetDevicesList();
                    PingPanel.LoadList(devices);
                    StartPing();
                }
                else {
                    NoConfigPanel = new NoConfigPanel(form, this, true);
                }
            }
            else {
                NoConfigPanel = new NoConfigPanel(form, this, true);
            }
        }

        /// <summary>
        /// Create a BindingList of ConfigDevice with all Device properly initialized
        /// </summary>
        /// <returns>a BindingList of ConfigDevice with all Device properly initialized</returns>
        public BindingList<ConfigDevice> GetDevicesList() {
            BindingList<ConfigDevice> devices = new BindingList<ConfigDevice>();
            foreach(var d in config.listDevice) {
                if (DeviceOK(d)) {
                    d.DeviceStatus = "UNITIATED";
                    devices.Add(d);
                }
            }
            return devices;
        }
        /// <summary>
        /// Check if the param ConfigDevice has a name, IPAddress and a Second Interval > 0;
        /// </summary>
        /// <param name="device">the device to check</param>
        /// <returns>true, if the name, the ip address are not null and not empty, and a second Interval > 0</returns>
        private bool DeviceOK(ConfigDevice device) {
            return !string.IsNullOrEmpty(device.Name) && !string.IsNullOrEmpty(device.IPAddress) && device.SecondInterval > 0;
        }
        /// <summary>
        /// Launch all task into taskManager
        /// </summary>
        public void StartPing() {
            BindingList<ConfigDevice> devices = GetDevicesList();
            if (taskManager != null && devices != null) {
                taskManager.RunAll(devices);
            }
        }
    }
}
