using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Projet_app_principale {
    public class TaskManager {
        PingTask pingTask;
        ConfigSetting config;
        bool ssl;
        /// <summary>
        /// Create a task Manager
        /// </summary>
        /// <param name="configDevice">the global configuration</param>
        public TaskManager(ConfigSetting configDevice) {
            config = configDevice;
            pingTask = new PingTask(this);
            this.ssl = config.EnableSSL;
        }
        /// <summary>
        /// Run and wait all task on async
        /// </summary>
        /// <param name="devices">BindingList of ConfigDevice with all Device properly initialized</param>
        public async void RunAll(BindingList<ConfigDevice> devices) {
            await Task.WhenAll(pingTask.Run(devices));
            
        }
        /// <summary>
        /// Create Email object and send one.
        /// </summary>
        /// <param name="name">The name of device</param>
        /// <param name="ipAddress">The ip Address of device</param>
        /// <param name="status">The status of device</param>
        public async Task SendEmail(string name, string ipAddress, string status) {
            Email email = new Email(config, name, ipAddress, status,ssl);
            await email.Send();
        }

        /// <summary>
        /// Stop all of task ping.
        /// </summary>
        public void StopAll() {
            pingTask.Stop();
        }
    }
}
