using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_app_configuration {
    public partial class PingConfigPanel : Panel {
        /// <summary>
        /// Center the dataDridView
        /// </summary>
        public void CenterGrid() {
            var totalSizeLabel = dataGridView.Width;
            var x = (form.ClientSize.Width - totalSizeLabel) / 2;
            var y = nameField.Top + nameField.Height + 40;
            dataGridView.Location = new Point(x, y);
            dataGridView.Height = this.ClientSize.Height - y - 60;

        }

        /// <summary>
        /// Center all label of the liste named labels
        /// The height isn't change
        /// </summary>
        public void CenterLabel() {
            var totalSizeLabel = 0;
            foreach (var l in labels) {
                totalSizeLabel += l.Width;
            }
            var spaceBetween = 250;
            totalSizeLabel += spaceBetween * (labels.Count - 1);

            var x = (form.ClientSize.Width - totalSizeLabel) / 2;
            foreach (var l in labels) {
                l.Location = new Point(x, 80);
                x += l.Width + spaceBetween;
            }
        }

        /// <summary>
        /// Add a new ConfigDevice in data grid view and the list 
        /// </summary>
        private void AddInGridView() {
            ConfigDevice device = new ConfigDevice();
            device.Name = nameField.Text;
            device.IPAddress = (int)IPField1.Value + "." + (int)IPField2.Value + "." + (int)IPField3.Value + "." + (int)IPField4.Value;
            device.SecondInterval = (int)numInterval.Value;
            if (!manager.config.listDevice.Contains(device)) {
                if (CheckIp(device.IPAddress)) {
                    manager.config.listDevice.Add(device);
                    manager.config.NBDevice++;
                    textError.Text = "";
                }
                else {
                    textError.Text = $"Error: This device with ip {device.IPAddress} already exists.";
                }
            }
            else {
                textError.Text = "Error: This device already exists.";
            }
            dataGridView.DataSource = manager.config.listDevice;
            CheckNbDevice();
            manager.UpdateLastDateSave();
            manager.CheckConfig();
            manager.config.WriteConfigInJson();
        }

        /// <summary>
        /// Delete a ConfigDevice selected in data grid view and the list 
        /// </summary>
        private void DeleteInGridView() {
            deviceSelected = dataGridView.SelectedRows[0].DataBoundItem as ConfigDevice;
            manager.config.listDevice.Remove(deviceSelected);
            manager.config.NBDevice--;
            foreach (DataGridViewRow item in this.dataGridView.SelectedRows) {
                dataGridView.Rows.RemoveAt(item.Index);
            }
            this.btnDelete.Visible = false;
            CheckNbDevice();
            manager.UpdateLastDateSave();
            manager.CheckConfig();
            manager.config.WriteConfigInJson();
        }
        /// <summary>
        /// Replace the item selected in data grid view in field name, ip and Interval
        /// </summary>
        private void LoadField() {
            try {
                deviceSelected = dataGridView.SelectedRows[0].DataBoundItem as ConfigDevice;
                if (deviceSelected != null) {
                    this.nameField.Text = deviceSelected.Name;
                    var ipAddress = deviceSelected.IPAddress.Split('.');
                    this.IPField1.Value = Int32.Parse(ipAddress[0]);
                    this.IPField2.Value = Int32.Parse(ipAddress[1]);
                    this.IPField3.Value = Int32.Parse(ipAddress[2]);
                    this.IPField4.Value = Int32.Parse(ipAddress[3]);
                    this.numInterval.Value = deviceSelected.SecondInterval;
                    this.btnDelete.Visible = true;
                    this.btnAdd.Visible = false;
                }
            }
            catch (Exception) {

            }
        }

        /// <summary>
        /// Retrieves the Name,IPAddress,SecondInterval fields to copy them into the global configuration.
        /// </summary>
        private void ApplyChange() {
            if (deviceSelected != null) {
                string ip = (int)IPField1.Value + "." + (int)IPField2.Value + "." + (int)IPField3.Value + "." + (int)IPField4.Value;
                string name = nameField.Text;
                int interval = (int)numInterval.Value;
                if (deviceSelected.Name != name || deviceSelected.IPAddress != ip || deviceSelected.SecondInterval != interval) {
                    ConfigDevice device = new ConfigDevice(name, ip, interval);
                    if (!manager.config.listDevice.Contains(device)) {
                        if (CheckIp(device.IPAddress, deviceSelected)) {
                            deviceSelected.Name = name;
                            deviceSelected.IPAddress = ip;
                            deviceSelected.SecondInterval = interval;
                            textError.Text = "";
                        }
                        else {
                            textError.Text = $"Error: This device with ip {ip} already exists.";
                        }
                    }
                    else {
                        textError.Text = "Error: This device already exists.";
                    }
                }
            }
            dataGridView.Refresh();
            manager.UpdateLastDateSave();
            manager.CheckConfig();
            manager.config.WriteConfigInJson();
        }

        /// <summary>
        /// Makes visible the add button when nbDevice exceed 8 
        /// </summary>
        private void CheckNbDevice() {
            var nbDevice = manager.config.NBDevice;
            this.btnAdd.Visible = nbDevice <= 7;
        }
        /// <summary>
        /// Replaces the fields in this panel based on the values ​​of the fields in the json file.
        /// </summary>
        public void ReplaceField() {
            dataGridView.DataSource = manager.config.listDevice;
            lastSave.Text = manager.config.LastDateSave;
            CheckNbDevice();
            dataGridView.Refresh();


        }
        /// <summary>
        /// Updates the date displayed on the panel
        /// </summary>
        public void RefreshDate() {
            lastSave.Text = manager.config.LastDateSave;
        }
        /// <summary>
        /// Deletes the content of all fields in the panel.
        /// </summary>
        public void ClearField() {
            nameField.Text = "";
            IPField1.Value = 1;
            IPField2.Value = 0;
            IPField3.Value = 0;
            IPField4.Value = 0;
            numInterval.Value = 0;
            textError.Text = "";
            dataGridView.DataSource = null;
            dataGridView.DataSource = manager.config.listDevice;

        }
        /// <summary>
        /// Check if the ip isn't already in the list
        /// </summary>
        /// <param name="ip"></param>
        /// <returns>true, if the ip isn't in the list, false otherwise</returns>
        private bool CheckIp(string ip, ConfigDevice exclude = null) {
            bool IpOk = true;
            foreach (ConfigDevice item in manager.config.listDevice) {
                if (item != exclude && item.IPAddress.Equals(ip)) {
                    IpOk = false;
                }
            }
            return IpOk;
        }
    }
}
