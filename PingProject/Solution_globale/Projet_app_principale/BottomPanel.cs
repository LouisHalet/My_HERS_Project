using Projet_app_configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Projet_app_principale {
    public class BottomPanel : Panel {
        PingMonitoringForm form;
        PanelManager manager;
        List<Button> buttons = new List<Button>();
        Button btnStartConfig;
        Button btnAlwaysOnTop;
        Button btnExit;

        /// <summary>
        /// Constructs a BottomPanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public BottomPanel(PingMonitoringForm form, PanelManager manager, bool visible) {
            this.form = form;
            this.manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Bottom;
            this.Height = 50;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.FromArgb(30, 58, 95);
            InitPanel();
            form.Controls.Add(this);
            btnAlwaysOnTop.Click += (s,e) => OnClickAlwaysOnTop();
            btnStartConfig.Click += (s, e) => OnClickStartConfig();
            btnExit.Click += (s, e) => OnClickExit();
            form.FormClosing += (s, e) => OnFormClosing();

        }

        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// </summary>
        private void InitPanel() {
            btnStartConfig = Utils.InitButton("Start configuration", new Point(290, 12), new Size(100, 25),
                Color.FromArgb(26, 115, 232), Color.White, new Font("Arial", 8), FlatStyle.Flat);
            this.Controls.Add(btnStartConfig);
            btnAlwaysOnTop = Utils.InitButton("Always on top", new Point(630, 12), new Size(100, 25),
                Color.FromArgb(26, 115, 232), Color.White, new Font("Arial", 8), FlatStyle.Flat);
            this.Controls.Add(btnAlwaysOnTop);
            btnExit = Utils.InitButton("Exit", new Point(630, 12), new Size(100, 25),
                Color.FromArgb(26, 115, 232), Color.White, new Font("Arial", 8), FlatStyle.Flat);
            this.Controls.Add(btnExit);
            buttons.Add(btnStartConfig);
            buttons.Add(btnAlwaysOnTop);
            buttons.Add(btnExit);

            PlaceButtons();
        }

        /// <summary>
        /// Position all buttons in the Buttons list as far to the right as possible
        /// with a space between each button
        /// </summary>
        public void PlaceButtons() {
            var totalSizeButtons = 0;
            foreach (var b in buttons) {
                totalSizeButtons += b.Width;
            }
            var spaceBetween = 25;
            totalSizeButtons += spaceBetween * (buttons.Count - 1);

            var x = (form.ClientSize.Width - totalSizeButtons) / 2;
            foreach (var b in buttons) {
                b.Location = new Point(x, 0);
                b.Height = this.Height;
                x += b.Width + spaceBetween;
            }
        }

        /// <summary>
        /// When the btnAlwaysOnTop button is pressed, the panel will be on Always on top.
        /// </summary>
        private void OnClickAlwaysOnTop() {
            form.TopMost = !form.TopMost;
        }

        /// <summary>
        /// When the btnStartConfig button is pressed, the form closed and the configuration app is launched
        /// </summary>
        private void OnClickStartConfig() {
            form.Close();
            OnFormClosing();
            Process.Start(@"..\..\..\..\Projet_app_configuration\bin\Debug\net10.0-windows\Projet_app_configuration.exe");
        }

        /// <summary>
        /// When the btnExit button is pressed, the form closed and all task is closed
        /// </summary>
        private void OnClickExit() {
            form.Close();
            if(manager.PingPanel != null) {
                manager.taskManager.StopAll();
            }
        }

        /// <summary>
        /// When the the form is closed, and all task is closed
        /// </summary>
        private void OnFormClosing() {
            if (manager.PingPanel != null) {
                manager.taskManager.StopAll();
            }
        }
    }
}
