using System;
using System.Collections.Generic;
using System.Text;


namespace Projet_app_configuration {
    public class EmailPanel : Panel {
        Label lastSave;
        Label textSMTPHost;
        Label textSMTPPort;
        Label textSMTPEmail;
        Label textSMTPPassword;
        Label textEmailSenderUsers;
        Label textCheckBox;

        TextBox fieldSMTPHost;
        NumericUpDown fieldSMTPPort;
        TextBox fieldSMTPEmail;
        TextBox fieldSMTPPassword;
        TextBox fieldEmailUsers;
        CheckBox checkBox;

        GeneralConfigurationForm form;
        PanelManager manager;

        /// <summary>
        /// Constructs a EmailPanel object by setting the Panel's characteristics
        /// such as its size and background color.
        /// It will create the objects of this panel which are initialized with the initPanel method.
        /// Init all events
        /// </summary>
        /// <param name="form">This is the form window</param>
        /// <param name="manager">This is the Panel Manager of this form.</param>
        /// <param name="visible">If visible is set to true, the panel will be displayed as soon as it is created, otherwise, false.</param>
        public EmailPanel(GeneralConfigurationForm form, PanelManager manager, bool visible) {
            this.form = form;
            this.manager = manager;
            this.Visible = visible;
            this.Dock = DockStyle.Fill;
            this.BorderStyle = BorderStyle.Fixed3D;
            this.BackColor = Color.FromArgb(235, 240, 248);
            InitPanel();
            form.Controls.Add(this);

            this.fieldSMTPHost.Validated += (s, e) => ApplyChange();
            this.fieldSMTPPort.Validated += (s, e) => ApplyChange();
            this.fieldSMTPEmail.Validated += (s, e) => ApplyChange();
            this.fieldSMTPPassword.Validated += (s, e) => ApplyChange();
            this.fieldEmailUsers.Validated += (s, e) => ApplyChange();
            checkBox.CheckedChanged += (s, e) => {
                checkBox.Text = checkBox.Checked ? "✓" : "";
                ApplyChange();
            };

            this.MouseClick += (s, e) => this.form.ValidateChildren();
        }
        /// <summary>
        /// Create all the graphic objects for this panel and add them to it.
        /// It will call the replaceField() method which will replace the fields with the fields from the json file.
        /// </summary>
        private void InitPanel() {
            var font = new Font("Arial", 11);
            var spaceBetween = 50;
            lastSave = Utils.InitLabel("Last change saved on : hh:mm jj/mm/aaaa", new Point(form.ClientSize.Width - 325, 52), new Font("Arial", 10));
            this.Controls.Add(lastSave);


            var size = new Size(250, 30);
            textSMTPHost = Utils.InitLabel("Relay server link : ", new Point(125, 110),font);
            this.Controls.Add(textSMTPHost);

            fieldSMTPHost = Utils.InitTextBox(BorderStyle.FixedSingle, new Point(450, textSMTPHost.Top), size, font);
            this.Controls.Add(fieldSMTPHost);

            textSMTPPort = Utils.InitLabel("Relay server port : ", new Point(125, fieldSMTPHost.Top + spaceBetween), font);
            this.Controls.Add(textSMTPPort);

            fieldSMTPPort = Utils.InitNumericUpDown(BorderStyle.FixedSingle, new Point(450, textSMTPPort.Top), size, font, 0, 0, 65535);
            this.Controls.Add(fieldSMTPPort);

            textSMTPEmail = Utils.InitLabel("Sender's email address : ", new Point(125, fieldSMTPPort.Top + spaceBetween), font);
            this.Controls.Add(textSMTPEmail);

            fieldSMTPEmail = Utils.InitTextBox(BorderStyle.FixedSingle, new Point(450, textSMTPEmail.Top), size, font);
            this.Controls.Add(fieldSMTPEmail);

            textSMTPPassword = Utils.InitLabel("SMTP password : ", new Point(125, fieldSMTPEmail.Top + spaceBetween), font);
            this.Controls.Add(textSMTPPassword);

            fieldSMTPPassword = Utils.InitTextBox(BorderStyle.FixedSingle, new Point(450, textSMTPPassword.Top), size, font);
            fieldSMTPPassword.UseSystemPasswordChar = true;
            this.Controls.Add(fieldSMTPPassword);

            textEmailSenderUsers = Utils.InitLabel("Recipient's email address : ", new Point(125, fieldSMTPPassword.Top + spaceBetween), font);
            this.Controls.Add(textEmailSenderUsers);

            fieldEmailUsers = Utils.InitTextBox(BorderStyle.FixedSingle, new Point(450, textEmailSenderUsers.Top), size, font);
            this.Controls.Add(fieldEmailUsers);

            textCheckBox  = Utils.InitLabel("Enable SSL : ", new Point(125, fieldEmailUsers.Top + spaceBetween), font);
            this.Controls.Add(textCheckBox);

            checkBox = new CheckBox();
            checkBox.Location = new Point(450, textCheckBox.Top);
            checkBox.Appearance = Appearance.Button;
            checkBox.AutoSize = false;
            checkBox.Font = new Font("Arial", 16, FontStyle.Bold);
            checkBox.Size = new Size(35, 35);
            this.Controls.Add(checkBox);

            ReplaceField();
        }

        /// <summary>
        /// Retrieves the SMTPHost,SMTPPort,SMTPEmail,SMTPPassword,EmailSenderUsers and EnableSSL fields to copy them into the global configuration.
        /// </summary>
        private void ApplyChange() {
            manager.config.SMTPHost = fieldSMTPHost.Text;
            manager.config.SMTPPort = (int)fieldSMTPPort.Value;
            manager.config.SMTPEmail = fieldSMTPEmail.Text;
            manager.config.SMTPPassword = fieldSMTPPassword.Text;
            manager.config.EmailSenderUsers = fieldEmailUsers.Text;
            manager.config.EnableSSL = checkBox.Checked;
            manager.CheckConfig();
            manager.config.WriteConfigInJson();
        }
        /// <summary>
        /// Replaces the fields in this panel based on the values ​​of the fields in the json file.
        /// </summary>
        public void ReplaceField() {
            if (!string.IsNullOrEmpty(manager.config.SMTPHost)) fieldSMTPHost.Text = manager.config.SMTPHost;
            if (manager.config.SMTPPort > 0) fieldSMTPPort.Value = manager.config.SMTPPort;
            if (!string.IsNullOrEmpty(manager.config.SMTPEmail)) fieldSMTPEmail.Text = manager.config.SMTPEmail;
            if (!string.IsNullOrEmpty(manager.config.SMTPPassword)) fieldSMTPPassword.Text = manager.config.SMTPPassword;
            if (!string.IsNullOrEmpty(manager.config.EmailSenderUsers)) fieldEmailUsers.Text = manager.config.EmailSenderUsers;
            if (checkBox.Checked != manager.config.EnableSSL) checkBox.Checked = manager.config.EnableSSL;
            lastSave.Text = manager.config.LastDateSave;

        }
        /// <summary>
        /// Updates the date displayed on the panel
        /// </summary>
        public void RefreshDate() {
            lastSave.Text = manager.config.LastDateSave;
        }

        /// <summary>
        /// Deletes the content of all fields in the panel.
        /// </summary>
        public void ClearField() {
            fieldSMTPHost.Text = "";
            fieldSMTPPort.Value = 0;
            fieldSMTPEmail.Text = "";
            fieldSMTPPassword.Text = "";
            fieldEmailUsers.Text = "";
            checkBox.Checked = false;
        }

    }
}