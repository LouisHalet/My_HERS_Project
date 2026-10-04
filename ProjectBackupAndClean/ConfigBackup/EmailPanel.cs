using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigBackup {
    public partial class EmailPanel : Panel {
        Label textSMTPHost;
        Label textSMTPPort;
        Label textSMTPEmail;
        Label textSMTPPassword;
        Label textEmailSenderUsers;
        TextBox fieldSMTPHost;
        NumericUpDown fieldSMTPPort;
        TextBox fieldSMTPEmail;
        TextBox fieldSMTPPassword;
        TextBox fieldEmailUsers;

        GeneralConfigurationForm configurationForm;
        PanelManager manager;
        /// <summary>
        /// Constructs a EmailPanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public EmailPanel(GeneralConfigurationForm form, PanelManager manager, bool visible)
        {
            configurationForm = form;
            this.manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.FromArgb(235, 240, 248);
            initPanel();
            configurationForm.Controls.Add(this);

        }
        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// It will call the replaceField() method which will replace the fields with the fields from the json file.
        /// </summary>
        private void initPanel()
        {
            textSMTPHost = new Label
            {
                Text = "Lien serveur relais : *",
                Size = new Size(250, 30),
                Location = new Point(150, 70),
                Font = new Font("Arial", 15)
            };
            this.Controls.Add(textSMTPHost);
            fieldSMTPHost = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(250, 30),
                Location = new Point(400, 70),
                Font = new Font("Arial", 15)

            };
            this.Controls.Add(fieldSMTPHost);
            textSMTPPort = new Label
            {
                Text = "Port serveur relais : *",
                Size = new Size(250, 30),
                Location = new Point(150, 140),
                Font = new Font("Arial", 15)
            };
            this.Controls.Add(textSMTPPort);
            fieldSMTPPort = new NumericUpDown
            {
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(250, 30),
                Location = new Point(400, 140),
                Font = new Font("Arial", 15),
                Value = 0,
                Minimum = 0,
                Maximum = 65535
                
            };
            this.Controls.Add(fieldSMTPPort);
            textSMTPEmail = new Label
            {
                Text = "Addresse mail envoyeur : *",
                Size = new Size(250, 30),
                Location = new Point(150, 210),
                Font = new Font("Arial", 15)
            };
            this.Controls.Add(textSMTPEmail);
            fieldSMTPEmail = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(250, 30),
                Location = new Point(400, 210),
                Font = new Font("Arial", 15)

            };
            this.Controls.Add(fieldSMTPEmail);
            textSMTPPassword = new Label
            {
                Text = "Mot de passe SMTP : *",
                Size = new Size(250, 30),
                Location = new Point(150, 280),
                Font = new Font("Arial", 15)
            };
            this.Controls.Add(textSMTPPassword);
            fieldSMTPPassword = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(250, 30),
                Location = new Point(400, 280),
                Font = new Font("Arial", 15),
                UseSystemPasswordChar = true

            };
            this.Controls.Add(fieldSMTPPassword);
            textEmailSenderUsers = new Label
            {
                Text = "Addresse mail receveur : *",
                Size = new Size(250, 30),
                Location = new Point(150, 350),
                Font = new Font("Arial", 15)
            };
            this.Controls.Add(textEmailSenderUsers);
            fieldEmailUsers = new TextBox
            {
                BorderStyle = BorderStyle.FixedSingle,
                Size = new Size(250, 30),
                Location = new Point(400, 350),
                Font = new Font("Arial", 15)

            };
            this.Controls.Add(fieldEmailUsers);
            replaceField();
        }
        /// <summary>
        /// Retrieves the SMTPHost, SMTPPort,SMTPEmail,SMTPPassword,EmailSenderUsers fields to copy them into the global configuration.
        /// </summary>
        public void getFields()
        {
            manager.backupGlobal.SMTPHost = fieldSMTPHost.Text;
            manager.backupGlobal.SMTPPort = (int)fieldSMTPPort.Value;
            manager.backupGlobal.SMTPEmail = fieldSMTPEmail.Text;
            manager.backupGlobal.SMTPPassword = fieldSMTPPassword.Text;
            manager.backupGlobal.EmailSenderUsers = fieldEmailUsers.Text;
        }
    }
}
