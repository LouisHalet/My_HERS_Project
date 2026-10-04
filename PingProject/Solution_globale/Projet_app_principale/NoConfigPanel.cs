using Projet_app_configuration;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Projet_app_principale {
    public class NoConfigPanel : Panel{
        Label question;
        Button btnYes;
        Button btnNo;
        List<Button> buttons = new List<Button>();
        PingMonitoringForm form;
        PanelManager manager;

        /// <summary>
        /// Constructs a NoConfigPanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// Init all events
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public NoConfigPanel(PingMonitoringForm form, PanelManager manager, bool visible) {
            this.form = form;
            this.manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.FromArgb(235, 240, 248);
            InitPanel();
            form.Controls.Add(this);
            btnYes.Click += (s, e) => OnYes_Click();
            btnNo.Click += (s, e) => OnNo_Click();

        }/// <summary>
         /// Create all the graphic objects for this panel and add them to it.
         /// </summary>
        private void InitPanel() {
            question = Utils.InitLabel("Non-functional configuration file.\n\n Do you want launch the configuration app ? ", new Point(150, 80), new Font("Arial", 20));
            this.question.AutoSize = false;
            this.question.TextAlign = ContentAlignment.MiddleCenter;
            this.question.Size = new Size(350,200);
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(question);
            this.question.Location = new Point(form.ClientSize.Width / 2 - question.Width / 2, form.ClientSize.Height / 2 - question.Height / 2 - 75);
            
            btnYes = Utils.InitButton("Yes", new Point(290, 12), new Size(140, 50),
                Color.Black, Color.FromArgb(121, 253, 0), new Font("Arial", 15), FlatStyle.Flat);
            this.Controls.Add(btnYes);

            btnNo = Utils.InitButton("No", new Point(290, 12), new Size(140, 50),
               Color.Black, Color.FromArgb(238, 69, 61), new Font("Arial", 15), FlatStyle.Flat);

            this.Controls.Add(btnNo);
            buttons.Add(btnYes);
            buttons.Add(btnNo);
            CenterButtons();

        }
        /// <summary>
        /// Center all button of the list
        /// </summary>
        private void CenterButtons() {
            var totalSizeButtons = 0;
            foreach (var b in buttons) {
                totalSizeButtons += b.Width;
            }
            var spaceBetween = 25;
            totalSizeButtons += spaceBetween * (buttons.Count - 1);

            var x = (form.ClientSize.Width - totalSizeButtons) / 2;
            foreach (var b in buttons) {
                b.Location = new Point(x, question.Top + question.Height + 10);
                x += b.Width + spaceBetween;
            }
        }
        /// <summary>
        /// When the btnYes button is pressed, Launch the configuration app
        /// </summary>
        private void OnYes_Click() {
            form.Close();
            Process.Start(@"..\..\..\..\Projet_app_configuration\bin\Debug\net10.0-windows\Projet_app_configuration.exe");
        }

        /// <summary>
        /// When the btnNo button is pressed, close the form.
        /// </summary>
        private void OnNo_Click() {
            form.Close();
        }
    }
}
