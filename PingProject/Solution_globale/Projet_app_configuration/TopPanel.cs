using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_app_configuration {
    public class TopPanel : Panel {
        Button btnConfigPing;
        Button btnConfigEmail;
        List<Button> buttons = new List<Button>();
        PanelManager manager;
        GeneralConfigurationForm form;

        /// <summary>
        /// Constructs a TopPanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public TopPanel(GeneralConfigurationForm form, PanelManager manager, bool visible) {
            this.form = form;
            this.manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Top;
            this.Height = 50;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.FromArgb(44, 82, 130);
            InitPanel();
            this.form.Controls.Add(this);
            btnConfigPing.Click += (s, e) => OnPing_Click();
            btnConfigEmail.Click += (s, e) => OnEmail_Click();

        }

        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// </summary>
        private void InitPanel() {
            btnConfigEmail = Utils.InitButton("Email", new Point(610, 12), new Size(140,25), 
                Color.FromArgb(26, 115, 232), Color.White,  new Font("Arial", 10), FlatStyle.Flat);
            btnConfigEmail.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnConfigEmail);
            btnConfigPing = Utils.InitButton("Config", new Point(310, 12), new Size(140, 25),
                Color.FromArgb(26, 115, 232), Color.White, new Font("Arial", 10), FlatStyle.Flat);
            btnConfigEmail.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnConfigPing);
            buttons.Add(btnConfigPing);
            buttons.Add(btnConfigEmail);
            CenterButtons();
        }
        /// <summary>
        /// Center all button of the list
        /// </summary>
        private void CenterButtons() {
            var totalSizeButtons = 0;
            foreach(var b in buttons) {
                totalSizeButtons += b.Width;
            }
            var spaceBetween = 200;
            totalSizeButtons += spaceBetween * (buttons.Count - 1);

            var x = (form.ClientSize.Width - totalSizeButtons)/2;
            foreach(var b in buttons) {
                b.Location = new Point(x, 0);
                b.Height = this.Height;
                x += b.Width + spaceBetween;
            }
        }
        /// <summary>
        /// When the btnConfigEmail button is pressed, the EmailPanel is set to true via the panelManager.
        /// </summary>
        private void OnEmail_Click() {
            manager.ShowPanel(manager.EmailPanel);
        }

        /// <summary>
        /// When the btnConfigPing button is pressed, the PingConfigPanel is set to true via the panelManager.
        /// </summary>
        private void OnPing_Click() {
            manager.ShowPanel(manager.PingPanel);
        }
    }
    
}
