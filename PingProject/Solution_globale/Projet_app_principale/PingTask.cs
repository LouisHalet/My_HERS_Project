using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;

namespace Projet_app_principale {
    
    public class PingTask {
        List<Task> listTask;
        List<string> oldStatus;
        BindingList<ConfigDevice> listDevice;
        TaskManager taskManager;
        bool execTask;
        /// <summary>
        /// Init a Ping task. Init the list listTask and oldStatus.
        /// </summary>
        /// <param name="taskManager">The taskManager</param>
        public PingTask(TaskManager taskManager) {
            listTask = new List<Task>();
            oldStatus = new List<string>();
            this.taskManager = taskManager;
            execTask = true;
        }
        /// <summary>
        /// Create the list with current statuses and prepare the task list including the Ping task
        /// </summary>
        /// <param name="devices">BindingList of ConfigDevice with all Device properly initialized</param>
        private void PrepareTask(BindingList<ConfigDevice> devices) {
            this.listDevice = devices;
            foreach(var d in devices) {
                oldStatus.Add(d.DeviceStatus);
                listTask.Add(PingTaskAsync(d));
            }
        }

        /// <summary>
        /// Run all Ping tasks
        /// </summary>
        /// <param name="devices"></param>
        /// <returns></returns>
        public async Task Run(BindingList<ConfigDevice> devices) {
            PrepareTask(devices);
            await Task.WhenAll(listTask);
        }

        /// <summary>
        /// Create a task Ping with the device in parameter
        /// Change the status of device when ping response arrived.
        /// If Succes it's FUNCTIONAL, NON-FUNCTIONAL otherwise
        /// A delay is established between each ping.
        /// </summary>
        /// <param name="device"></param>
        /// <returns></returns>
        private async Task PingTaskAsync(ConfigDevice device) {
            try {
                Ping pingSender = new Ping();
                while (execTask) {
                    PingReply pingReply = await pingSender.SendPingAsync(device.IPAddress);
                    if(pingReply.Status == IPStatus.Success) {
                        device.DeviceStatus = "FUNCTIONAL";
                        await CkeckSendEmail(device);
                    }
                    else {
                        device.DeviceStatus = "NON-FUNCTIONAL";
                        await CkeckSendEmail(device);
                    }
                    if(execTask)
                        await Task.Delay(device.SecondInterval*1000);
                }
            }
            catch (SocketException e) {

            }
            catch (PingException) {

            }
        }

        /// <summary>
        /// Check the change of status and determine for send email.
        /// </summary>
        /// <param name="device"></param>
        private async Task CkeckSendEmail(ConfigDevice device) {
            bool sendEmail = false;
            int devicePosition = listDevice.IndexOf(device);
            if(devicePosition >= 0) {
                string oldStatusDevice = oldStatus[devicePosition];
                if (oldStatusDevice.Equals("UNITIATED") || oldStatusDevice.Equals("FUNCTIONAL")) {
                    switch (device.DeviceStatus) {
                        case "FUNCTIONAL":
                            sendEmail = false;
                        break;
                        case "NON-FUNCTIONAL":
                            sendEmail = true;
                        break;
                        default:
                        break;
                    }
                }else {
                    switch (device.DeviceStatus) {
                        case "FUNCTIONAL":
                        sendEmail = true;
                        break;
                        case "NON-FUNCTIONAL":
                        sendEmail = false;
                        break;
                        default:
                        break;
                    }
                }
                oldStatus[devicePosition] = device.DeviceStatus;
            }
            if (sendEmail) {
                taskManager.SendEmail(device.Name,device.IPAddress,device.DeviceStatus);
            }
        }
        /// <summary>
        /// Stop all of task ping.
        /// </summary>
        public void Stop() {
            execTask = false;
            listTask.Clear();
            oldStatus.Clear();
        }

    }
}
