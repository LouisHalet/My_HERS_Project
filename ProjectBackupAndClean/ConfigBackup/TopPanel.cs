using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigBackup {
    public class TopPanel : Panel {
        Button btnConfigSave;
        Button btnConfigRestore;
        Button btnConfigEmail;

        GeneralConfigurationForm configurationForm;
        PanelManager manager;
        /// <summary>
        /// Constructs a TopPanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public TopPanel(GeneralConfigurationForm form, PanelManager manager, bool visible)
        {
            configurationForm = form;
            this.manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Top;
            this.Height = 50;
            this.BorderStyle = BorderStyle.Fixed3D;
            
            this.BackColor = Color.FromArgb(44, 82, 130);

            initPanel();
            configurationForm.Controls.Add(this);
            btnConfigSave.Click += (s, e) => onBackup_Click();
            btnConfigRestore.Click += (s, e) => onRestore_Click();
            btnConfigEmail.Click += (s, e) => onEmail_Click();

        }
        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// </summary>
        private void initPanel()
        {
            btnConfigSave = new Button()
            {
                Text = "Sauvegarde",
                Location = new Point(50, 12),
                Size = new Size(140, 25),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(26, 115, 232),
                
                Font = new Font("Arial", 10)
            };
            btnConfigSave.FlatStyle = FlatStyle.Flat;
            btnConfigSave.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnConfigSave);
            btnConfigRestore = new Button()
            {
                Text = "Restauration",
                Size = new Size(140, 25),
                BackColor = Color.FromArgb(26, 115, 232),
                ForeColor = Color.White,
                Font = new Font("Arial", 10)
            };
            btnConfigRestore.FlatStyle = FlatStyle.Flat;
            btnConfigRestore.FlatAppearance.BorderSize = 0;
            btnConfigRestore.Location = new Point((configurationForm.Width - btnConfigRestore.Width) / 2, 12);
            this.Controls.Add(btnConfigRestore);

            btnConfigEmail = new Button()
            {
                Text = "Email",
                Size = new Size(140, 25),
                BackColor = Color.FromArgb(26, 115, 232),
                ForeColor = Color.White,
                
                Location = new Point(610, 12),
                Font = new Font("Arial", 10)
            };
            btnConfigEmail.FlatStyle = FlatStyle.Flat;
            btnConfigEmail.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnConfigEmail);
        }
        /// <summary>
        /// A l'appui du bouton btnConfigSave, le BackupPanel passe à true par l'intermédiaire du panelManager
        /// </summary>
        private void onBackup_Click()
        {
            manager.showPanel(manager.BackupPanel);
        }
        /// <summary>
        /// A l'appui du bouton btnConfigRestore, le RestorePanel passe à true par l'intermédiaire du panelManager
        /// </summary>
        private void onRestore_Click()
        {
            manager.showPanel(manager.RestorePanel);
        }
        /// <summary>
        /// A l'appui du bouton btnConfigEmail, le EmailPanel passe à true par l'intermédiaire du panelManager
        /// </summary>
        private void onEmail_Click()
        {
            manager.showPanel(manager.EmailPanel);
        }

    }

}
