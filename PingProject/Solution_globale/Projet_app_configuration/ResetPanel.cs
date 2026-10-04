using System;
using System.Collections.Generic;
using System.Text;

namespace Projet_app_configuration {
    public class ResetPanel : Panel {
        Label question;
        Button btnYes;
        Button btnNo;
        List<Button> buttons = new List<Button>();
        GeneralConfigurationForm form;
        PanelManager manager;

        /// <summary>
        /// Constructs a ResetPanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// Init all events
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public ResetPanel(GeneralConfigurationForm form, PanelManager manager, bool visible) {
            this.form = form;
            this.manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.FromArgb(235, 240, 248);
            InitPanel();
            form.Controls.Add(this);
            btnYes.Click += (s,e) => OnYes_Click();
            btnNo.Click += (s, e) => OnNo_Click();

        }
        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// It will call the replaceField() method which will replace the fields with the fields from the json file.
        /// </summary>
        private void InitPanel() {
            question = Utils.InitLabel("Do you want to reset the configuration? ", new Point(150, 80), new Font("Arial", 20));
            this.Controls.Add(question);
            this.question.Location = new Point(form.ClientSize.Width/2 - question.Width/2, form.ClientSize.Height / 2 - question.Height / 2 - 50);

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
            var spaceBetween = 100;
            totalSizeButtons += spaceBetween * (buttons.Count - 1);

            var x = (form.ClientSize.Width - totalSizeButtons) / 2;
            foreach (var b in buttons) {
                b.Location = new Point(x, question.Top + question.Height + 50);
                x += b.Width + spaceBetween;
            }
        }
        /// <summary>
        /// Delete the JSON file and clear the configuration fields
        /// </summary>
        private void OnYes_Click() {
            manager.config.DeleteFile();
            manager.config = new ConfigSetting();
            manager.ShowPanel(manager.PingPanel);
            manager.ReplaceField();
            manager.ClearAllField();
        }

        /// <summary>
        /// When the btnConfigPing button is pressed, the PingConfigPanel is set to true via the panelManager.
        /// </summary>
        private void OnNo_Click() {
            manager.ShowPanel(manager.PingPanel);
            
        }
    }
}
