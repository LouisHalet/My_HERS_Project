using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigBackup {
    public class BottomPanel : Panel {
        Button btnRAZ;
        Button btnSaveGlobal;
        Button btnExit;
        Label labelRequirement;
        GeneralConfigurationForm configurationForm;
        PanelManager manager;

        /// <summary>
        /// Constructs a BottomPanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public BottomPanel(GeneralConfigurationForm form, PanelManager manager, bool visible)
        {
            configurationForm = form;
            this.manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Bottom;
            this.Height = 50;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.FromArgb(30, 58, 95);
            initPanel();
            configurationForm.Controls.Add(this);

            btnExit.Click += (s, e) => onExit_Click();
            btnSaveGlobal.Click += (s, e) => onSave_Click();
            btnRAZ.Click += (s, e) => onRAZ_Click();

        }
        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// </summary>
        private void initPanel()
        {
            btnRAZ = new Button()
            {
                Text = "Remise à zéro",
                Location = new Point(290, 12),
                Size = new Size(140, 25),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(26, 115, 232),
                Font = new Font("Arial", 10)
            };
            btnRAZ.FlatStyle = FlatStyle.Flat;
            btnRAZ.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnRAZ);
            btnSaveGlobal = new Button()
            {
                Text = "Enregistrer",
                Size = new Size(140, 25),
                Location = new Point(460, 12),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(26, 115, 232),
                Font = new Font("Arial", 10)
            };
            btnSaveGlobal.FlatStyle = FlatStyle.Flat;
            btnSaveGlobal.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnSaveGlobal);
            btnExit = new Button
            {
                Text = "Annuler",
                Size = new Size(140, 25),
                Location = new Point(630, 12),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(26, 115, 232),
                Font = new Font("Arial", 10)
            };
            btnExit.FlatStyle = FlatStyle.Flat;
            btnExit.FlatAppearance.BorderSize = 0;
            this.Controls.Add(btnExit);
            labelRequirement = new Label()
            {
                Text = "* champs obligatoires",
                ForeColor = Color.Red,
                AutoSize = true,
                Location = new Point(30, 15),
                Font = new Font("Arial", 15)
            };
            this.Controls.Add(labelRequirement);
        }
        /// <summary>
        /// When the btnExit button is pressed, the window closes and the configuration is not saved.
        /// </summary>
        private void onExit_Click()
        {
            configurationForm.Close();
        }
        /// <summary>
        /// Pressing the btnSaveGlobal button starts the saving of all configuration fields to the JSON file.
        /// </summary>
        private void onSave_Click()
        {
            manager.EmailPanel.getFields();
            manager.BackupPanel.getFields();
            manager.RestorePanel.getFields();
            bool backupOk = manager.BackupPanel.checkFieldObligation();
            bool emailOk = manager.EmailPanel.checkFieldObligation();
            if (backupOk && emailOk)
            {
                manager.backupGlobal.writeConfigInJson();
            }
            
        }
        /// <summary>
        /// Delete the JSON file and clear the configuration fields
        /// </summary>
        private void onRAZ_Click()
        {
            manager.backupGlobal.deleteFile();
            manager.backupGlobal = new BackupSetting();
            manager.ClearAllField();
        }
    }
}
