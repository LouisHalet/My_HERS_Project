using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Projet_app_configuration {
    public class BottomPanel : Panel {
        GeneralConfigurationForm Form;
        PanelManager Manager;
        List<Button> Buttons = new List<Button>();
        public Button btnReset;
        Button btnExit;
        public Button btnExitAndLauch;

        /// <summary>
        /// Constructs a BottomPanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public BottomPanel(GeneralConfigurationForm form, PanelManager manager, bool visible) {
            this.Form = form;
            this.Manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Bottom;
            this.Height = 50;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.FromArgb(30, 58, 95);
            InitPanel();
            form.Controls.Add(this);
            btnExit.Click += (s, e) => OnExit_Click();
            btnReset.Click += (s, e) => OnReset_Click();
            btnExitAndLauch.Click += (s, e) => OnExitAndLaunch_Click();
        }

        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// </summary>
        private void InitPanel() {
            btnReset = Utils.InitButton("Reset", new Point(290, 12), new Size(140, 25),
                Color.FromArgb(26, 115, 232), Color.White, new Font("Arial", 10), FlatStyle.Flat);
            this.Controls.Add(btnReset);
            btnExit = Utils.InitButton("Exit", new Point(630, 12), new Size(140, 25),
                Color.FromArgb(26, 115, 232), Color.White, new Font("Arial", 10), FlatStyle.Flat);
            this.Controls.Add(btnExit);
            btnExitAndLauch = Utils.InitButton("Leave and start monitoring", new Point(630, 12), new Size(140, 25),
                Color.FromArgb(26, 115, 232), Color.White, new Font("Arial", 10), FlatStyle.Flat);
            this.Controls.Add(btnExitAndLauch);
            Buttons.Add(btnReset);
            Buttons.Add(btnExit);
            Buttons.Add(btnExitAndLauch);

            PlaceButtons();
        }

        /// <summary>
        /// Position all buttons in the Buttons list as far to the right as possible
        /// with a space between each button
        /// </summary>
        public void PlaceButtons() {
            var totalSizeButtons = 0;
            var cpt = 0;
            foreach (var b in Buttons) {
                if (b.Visible) {
                    totalSizeButtons += b.Width;
                    cpt++;
                }
            }

            var spaceBetween = 50;
            if(cpt>0)
                totalSizeButtons += spaceBetween * (cpt-1);
            var x = Form.ClientSize.Width - totalSizeButtons - 20;

            foreach (var b in Buttons) {
                if (b.Visible) {
                    b.Location = new Point(x, 12);
                    x += b.Width + spaceBetween;
                }  
            }
        }

        /// <summary>
        /// When the btnReset button is pressed, the ResetPanel is set to true via the panelManager.
        /// </summary>
        private void OnReset_Click() {
            Manager.ShowPanel(Manager.ResetPanel);
        }

        /// <summary>
        /// When the btnExit button is pressed, the form closed and the configuration is saved in the json file.
        /// </summary>
        private void OnExit_Click() {
            Manager.UpdateLastDateSave();
            Manager.CheckConfig();
            Manager.config.WriteConfigInJson();
            Form.Close();
        }

        /// <summary>
        /// When the btnExitAndLaunch button is pressed, the form closed and the configuration is saved in the json file.
        /// the main app is launched
        /// </summary>
        private void OnExitAndLaunch_Click() {
            Manager.UpdateLastDateSave();
            Manager.CheckConfig();
            Manager.config.WriteConfigInJson();
            Form.Close();
            Process.Start(@"..\..\..\..\Projet_app_principale\bin\Debug\net10.0-windows\Projet_app_principale.exe");
        }

    }
}
