using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigBackup {
    public partial class RestorePanel : Panel {
        Label textRestore;
        Label textDestination;
        ComboBox dropDownListDay;
        TextBox fieldPathDestination;
        Label textError;
        Button buttonDestination;
        Button buttonLaunchRestore;
        Label labelRunningRestore;
        PictureBox statusRestore;
        GeneralConfigurationForm configurationForm;
        PanelManager manager;
        bool checkSourceAndDestination = false;
        Process robotCopyProcess;
        /// <summary>
        /// Constructs a RestorePanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public RestorePanel(GeneralConfigurationForm form, PanelManager manager, bool visible)
        {
            configurationForm = form;
            this.manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.FromArgb(235, 240, 248);
            initPanel();
            configurationForm.Controls.Add(this);
            buttonDestination.Click += (s, e) => onDestination_Click();
            buttonLaunchRestore.Click += (s, e) => onLaunch_Click();
        }
        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// It will call the initContentScrollBar() method which will init the dropdown list .
        /// </summary>
        private void initPanel()
        {
            textRestore = new Label
            {
                Text = "Jour à restaurer : *",
                AutoSize = true,
                Location = new Point(30, 70),
                Font = new Font("Arial", 15)
            };
            this.Controls.Add(textRestore);
            dropDownListDay = new ComboBox
            {
                Size = new Size(350, 30),
                Location = new Point(30, 100),
                Font = new Font("Arial", 15),
                
            };
            
            this.Controls.Add(dropDownListDay);
            textDestination = new Label
            {
                Text = "Destination : *",
                AutoSize = true,
                Location = new Point(430, 70),
                Font = new Font("Arial", 15)
            };
            this.Controls.Add(textDestination);
            fieldPathDestination = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(250, 30),
                Location = new Point(430, 100),
                Font = new Font("Arial", 15)

            };
            this.Controls.Add(fieldPathDestination);
            buttonDestination = new Button
            {
                Text = "Parcourir",
                AutoSize = true,
                Height = 30,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(26, 115, 232),
                Location = new Point(680, 100),
                Font = new Font("Arial", 10)
            };
            buttonDestination.FlatStyle = FlatStyle.Flat;
            buttonDestination.FlatAppearance.BorderSize = 0;
            this.Controls.Add(buttonDestination);
            buttonLaunchRestore = new Button()
            {
                Text = "Lancer la restoration",
                Location = new Point(30, 175),
                Size = new Size(730, 50),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(192, 57, 43),
                Font = new Font("Arial", 10)
            };
            buttonLaunchRestore.FlatStyle = FlatStyle.Flat;
            buttonLaunchRestore.FlatAppearance.BorderSize = 0;
            this.Controls.Add(buttonLaunchRestore);
            labelRunningRestore = new Label
            {
                Text = "Restoration",
                TextAlign = ContentAlignment.TopCenter,
                Size = new Size(250, 50),
                Location = new Point(130, 250),
                Font = new Font("Arial", 30)
            };
            this.Controls.Add(labelRunningRestore);
            statusRestore = new PictureBox
            {
                Visible = true,
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(80, 80),
                Location = new Point(420, 230),
            };
            this.Controls.Add(statusRestore);
            textError = new Label
            {
                Text = "Erreur : Source et destination ne peut pas être égaux",
                AutoSize = true,
                Location = new Point(175, 150),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                Font = new Font("Arial", 10),
                Visible = false
            };
            this.Controls.Add(textError);
            initContentScrollBar();
        }
        /// <summary>
        /// Initializes the contents of the dropDownListDay.
        /// If the destinationPathBackup field is empty, the message "Veuillez sélectionner un chemin de destination dans l'onglet Sauvegarde" is placed
        /// If the destinationPathBackup field is not empty but there is no folder named "day" to restore, 
        /// The message "Aucun dossier à restaurer à ce chemin de destination" is displayed.
        /// Alternatively, a list of days to be restored is provided.
        /// When a day is selected, the full path is generated.
        /// </summary>
        public void initContentScrollBar()
        {
            dropDownListDay.Items.Clear();
            List<string> listDay = new List<string>() { "lundi", "mardi","mercredi","jeudi","vendredi","samedi","dimanche"};
            if (string.IsNullOrEmpty(manager.backupGlobal.destinationPathBackup))
            {
                dropDownListDay.Items.Add("Veuillez sélectionner un chemin de destination dans l'onglet Sauvegarde");
            }
            else
            {
                string pathOrigin = manager.backupGlobal.destinationPathBackup;
                foreach (string s in listDay)
                {
                    string path = pathOrigin + "\\" + s;
                    dropDownListDay.ForeColor = Color.Black;
                    if (Directory.Exists(path))
                    {
                        dropDownListDay.Items.Add(s);
                    }
                    path = pathOrigin;
                }
                if(dropDownListDay.Items.Count == 0)
                {
                    dropDownListDay.Items.Add("Aucun dossier à restaurer à ce chemin de destination");
                }
            }
        }
        
    }
}