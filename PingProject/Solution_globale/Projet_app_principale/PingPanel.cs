using Projet_app_configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Projet_app_principale {
    public class PingPanel : Panel {
        DataGridView dataGridView;
        PingMonitoringForm form;
        PanelManager manager;

        /// <summary>
        /// Constructs a PingPanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// Init all events
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public PingPanel(PingMonitoringForm form, PanelManager manager, bool visible) {
            this.form = form;
            this.manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.FromArgb(235, 240, 248);
            InitPanel();
            form.Controls.Add(this);
            foreach (ConfigDevice device in manager.config.listDevice) {
                device.onAttributChange += SetColor;
            }


        }
        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// </summary>
        private void InitPanel() {
            dataGridView = new DataGridView();
            dataGridView.ReadOnly = true;
            dataGridView.AllowUserToAddRows = false;
            dataGridView.AllowUserToDeleteRows = false;
            dataGridView.MultiSelect = false;
            dataGridView.BackgroundColor = this.BackColor;
            dataGridView.BorderStyle = BorderStyle.None;
            
            dataGridView.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridView.Enabled = false;

            dataGridView.ScrollBars = ScrollBars.Vertical;

            dataGridView.Size = new Size(form.ClientSize.Width - 25, form.ClientSize.Height - 100);
            CenterGrid();
            dataGridView.AutoGenerateColumns = false;
            
            dataGridView.RowHeadersVisible = false;

            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "Name",
                HeaderText = "Name",
                Name = "Name"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "IPAddress",
                HeaderText = "IP",
                Name = "IP"
            });
            dataGridView.Columns.Add(new DataGridViewTextBoxColumn
            {
                DataPropertyName = "DeviceStatus",
                HeaderText = "Status",
                Name = "Status"
            });
            dataGridView.Columns["Name"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns["IP"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dataGridView.Columns["Status"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            this.Controls.Add(dataGridView);
        }

        /// <summary>
        /// Center the dataDridView
        /// </summary>
        public void CenterGrid() {
            var totalSizeLabel = dataGridView.Width;

            var x = (form.ClientSize.Width - totalSizeLabel) / 2;
            dataGridView.Location = new Point(x, 15);
        }
        /// <summary>
        /// Add the bindingList in data grid view and set its size 
        /// </summary>
        /// <param name="devices">the list of device to add in data grid view</param>
        public void LoadList(BindingList<ConfigDevice> devices) {
            dataGridView.DataSource = devices;
            form.Height = 150 + devices.Count * 30;
            
        }
        /// <summary>
        /// Set the right Backcolor depending on status
        /// FUNCTIONAL -> green
        /// NON-FUNCTIONAL -> red
        /// UNITIATED -> grey
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SetColor(object sender, EventArgs e) {
            if (sender is ConfigDevice device) { 
                foreach (DataGridViewRow r in dataGridView.Rows) {
                if (r.DataBoundItem == device) {
                    string status = r.Cells["Status"].Value.ToString();

                    switch (status) {
                        case "FUNCTIONAL":
                        r.Cells["Status"].Style.BackColor = Color.FromArgb(7, 200, 0);
                        break;
                        case "NON-FUNCTIONAL":
                        r.Cells["Status"].Style.BackColor = Color.FromArgb(248, 50, 0);
                        r.Cells["Status"].Style.ForeColor = Color.White;
                        break;
                        default:
                        r.Cells["Status"].Style.BackColor = Color.FromArgb(146, 160, 160);
                        break;
                    }
                    break;
                }

                }
            }
        }
    }
}
