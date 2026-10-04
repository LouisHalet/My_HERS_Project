using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ConfigBackup {
    public partial class BackupPanel : Panel {
        Label textSource;
        Label textDestination;
        Label textFileToExclude;
        Label textError;
        TextBox labelPathSource;
        TextBox labelPathDestination;
        Button buttonSource;
        Button buttonDestination;
        Button buttonFileToExclude;
        Button buttonFolderToExclude;
        Button deleteLinesListBox;
        CheckedListBox fileToExclude;
        bool checkSourceAndDestination = false;
        GeneralConfigurationForm configurationForm;
        PanelManager manager;
        /// <summary>
        /// Constructs a BackupPanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public BackupPanel(GeneralConfigurationForm form, PanelManager manager, bool visible)
        {
            configurationForm = form;
            this.manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.FromArgb(235, 240, 248);
            initPanel();
            //Déclaration des events
            buttonSource.Click += (s, e) => onSource_Click();
            buttonDestination.Click += (s, e) => onDestination_Click();
            buttonFileToExclude.Click += (s, e) => onFileToExclude_Click();
            buttonFolderToExclude.Click += (s, e) => onFolderToExclude_Click();
            deleteLinesListBox.Click += (s, e) => onDeleteLines_Click();
            //Ajout du panel au form.
            configurationForm.Controls.Add(this);
        }
        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// It will call the replaceField() method which will replace the fields with the fields from the json file.
        /// </summary>
        private void initPanel()
        {
            textSource = new Label
            {
                Text = "Source : *",
                AutoSize = true,
                Location = new Point(30, 70),
                Font = new Font("Arial", 15)
            };
            this.Controls.Add(textSource);
            labelPathSource = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true,
                ReadOnly = true,
                Size = new Size(250, 30),
                Location = new Point(30, 100),
                Font = new Font("Arial", 8)
            };
            this.Controls.Add(labelPathSource);
            buttonSource = new Button
            {
                Text = "Parcourir",
                AutoSize = true,
                Height = 30,
                ForeColor = Color.White,
                BackColor = Color.FromArgb(26, 115, 232),
                Location = new Point(280, 100),
                Font = new Font("Arial", 10)
            };
            buttonSource.FlatStyle = FlatStyle.Flat;
            buttonSource.FlatAppearance.BorderSize = 0;
            this.Controls.Add(buttonSource);
            textDestination = new Label
            {
                Text = "Destination : *",
                AutoSize = true,
                Location = new Point(430, 70),
                Font = new Font("Arial", 15)
            };
            this.Controls.Add(textDestination);
            labelPathDestination = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true,
                ReadOnly = true,
                Size = new Size(250, 30),
                Location = new Point(430, 100),
                Font = new Font("Arial", 8)
            };
            this.Controls.Add(labelPathDestination);
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
            textFileToExclude = new Label
            {
                Text = "Fichier/Dossier à exclure :",
                AutoSize = true,
                Location = new Point(30, 175),
                Font = new Font("Arial", 15)
            };
            this.Controls.Add(textFileToExclude);
            fileToExclude = new CheckedListBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(30, 200),
                Size = new Size(500, 200),
                CheckOnClick = true,
                Font = new Font("Arial", 10)
            };
            this.Controls.Add(fileToExclude);
            buttonFileToExclude = new Button
            {
                Text = "Selectionner un fichier",
                Size = new Size(230, 66),
                Location = new Point(540, 200),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(26, 115, 232),
                Font = new Font("Arial", 10)
            };
            buttonFileToExclude.FlatStyle = FlatStyle.Flat;
            buttonFileToExclude.FlatAppearance.BorderSize = 0;
            this.Controls.Add(buttonFileToExclude);
            buttonFolderToExclude = new Button
            {
                Text = "Selectionner un dossier",
                Size = new Size(230, 66),
                Location = new Point(540, 267),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(26, 115, 232),
                Font = new Font("Arial", 10)
            };
            buttonFolderToExclude.FlatStyle = FlatStyle.Flat;
            buttonFolderToExclude.FlatAppearance.BorderSize = 0;
            this.Controls.Add(buttonFolderToExclude);
            deleteLinesListBox = new Button
            {
                Text = "Supprimer cette ligne",
                Size = new Size(230, 66),
                Location = new Point(540, 334),
                ForeColor = Color.White,
                BackColor = Color.FromArgb(192, 57, 43),
                Font = new Font("Arial", 10)
            };
            deleteLinesListBox.FlatStyle = FlatStyle.Flat;
            deleteLinesListBox.FlatAppearance.BorderSize = 0;
            this.Controls.Add(deleteLinesListBox);
            textError = new Label
            {
                AutoSize = true,
                Location = new Point(175, 135),
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Red,
                Font = new Font("Arial", 10),
                Visible = false
            };
            this.Controls.Add(textError);
            replaceField();
        }
    }
}
