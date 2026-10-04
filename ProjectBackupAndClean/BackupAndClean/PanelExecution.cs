using System;
using System.Collections.Generic;
using System.Text;

namespace BackupAndClean {
    public class PanelExecution : Panel {
        Label labelRunningBackup;
        Label labelTitle;
        public Label errorLabel { get; private set; }
        public PictureBox statusBackup { get; private set; }
        Label labelRunningClean;
        public PictureBox statusClean { get; private set; }
        Button btnExit;
        Execution form;
        /// <summary>
        /// Constructs a PanelExecution object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public PanelExecution(Execution form,  bool visible)
        {
            this.form = form;
            
            this.Visible = visible;
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.Fixed3D;
            initPanel();
            form.Controls.Add(this);
            btnExit.Click += (s, e) => onExit_Click();

        }
        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// </summary>
        private void initPanel()
        {
            labelTitle = new Label
            {
                Text = "Sauvegarde et Nettoyage",
                TextAlign = ContentAlignment.TopCenter,
                Dock = DockStyle.Top,
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(250, 50),
                Font = new Font("Arial", 30)
            };
            this.Controls.Add(labelTitle);
            labelRunningBackup = new Label
            {
                Text = "Sauvegarde",
                TextAlign = ContentAlignment.TopCenter,
                Size = new Size(250, 50),
                Location = new Point(230, 150),
                Font = new Font("Arial", 30)
            };
            this.Controls.Add(labelRunningBackup);
            statusBackup = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(520, 130),
                Visible = true,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.Controls.Add(statusBackup);
            labelRunningClean = new Label
            {
                Text = "Nettoyage",
                TextAlign = ContentAlignment.TopCenter,
                Size = new Size(250, 50),
                Location = new Point(230, 250),
                Font = new Font("Arial", 30)
            };
            this.Controls.Add(labelRunningClean);
            statusClean = new PictureBox
            {
                Size = new Size(80, 80),
                Location = new Point(520, 230),
                Visible = true,
                SizeMode = PictureBoxSizeMode.Zoom
            };
            this.Controls.Add(statusClean);
            errorLabel = new Label
            {
                TextAlign = ContentAlignment.TopCenter,
                Location = new Point(250,350),
                Size = new Size(300, 50),
                ForeColor = Color.Red,
                Font = new Font("Arial", 15)
            };
            this.Controls.Add(errorLabel);
            btnExit = new Button
            {
                Text = "Annuler",
                Size = new Size(140, 25),
                Location = new Point(630, 463),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(26, 115, 232),
                Font = new Font("Arial", 10)
            };
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnExit);
        }
        /// <summary>
        /// When the btnExit button is pressed, all task is stopped and the window closes.
        /// </summary>
        private void onExit_Click()
        {
            form.taskManager.stopAll();
            form.Close();
        }
    }
}
