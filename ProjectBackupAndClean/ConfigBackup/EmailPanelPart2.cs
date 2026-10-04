using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigBackup {
    public partial class EmailPanel : Panel {
        /// <summary>
        /// Replaces the fields in this panel based on the values ​​of the fields in the json file.
        /// </summary>
        public void replaceField()
        {
            if (!string.IsNullOrEmpty(manager.backupGlobal.SMTPHost)) fieldSMTPHost.Text = manager.backupGlobal.SMTPHost;
            if (manager.backupGlobal.SMTPPort > 0) fieldSMTPPort.Value = manager.backupGlobal.SMTPPort;
            if (!string.IsNullOrEmpty(manager.backupGlobal.SMTPEmail)) fieldSMTPEmail.Text = manager.backupGlobal.SMTPEmail;
            if (!string.IsNullOrEmpty(manager.backupGlobal.SMTPPassword)) fieldSMTPPassword.Text = manager.backupGlobal.SMTPPassword;
            if (!string.IsNullOrEmpty(manager.backupGlobal.EmailSenderUsers)) fieldEmailUsers.Text = manager.backupGlobal.EmailSenderUsers;

        }
        /// <summary>
        /// Deletes the content of all fields in the panel.
        /// </summary>
        public void clearField()
        {
            fieldSMTPHost.Text = "";
            fieldSMTPPort.Value = 0;
            fieldSMTPEmail.Text = "";
            fieldSMTPPassword.Text = "";
            fieldEmailUsers.Text = "";
        }
        /// <summary>
        /// Checks if required fields are empty when saving. Empty fields are highlighted in red.
        /// </summary>
        /// <returns>true if all required fields are not empty, false otherwise<returns>
        public bool checkFieldObligation()
        {
            bool checkOK = true;
            if (string.IsNullOrWhiteSpace(fieldSMTPHost.Text))
            {
                checkOK = false;
                fieldSMTPHost.BackColor = Color.Red;
            }
            else
            {
                fieldSMTPHost.BackColor = Color.White;
            }
            if (string.IsNullOrWhiteSpace(fieldSMTPEmail.Text))
            {
                checkOK = false;
                fieldSMTPEmail.BackColor = Color.Red;
            }
            else
            {
                fieldSMTPEmail.BackColor = Color.White;
            }
            if (string.IsNullOrWhiteSpace(fieldSMTPPassword.Text))
            {
                checkOK = false;
                fieldSMTPPassword.BackColor = Color.Red;
            }
            else
            {
                fieldSMTPPassword.BackColor = Color.White;
            }

            if (string.IsNullOrWhiteSpace(fieldEmailUsers.Text))
            {
                checkOK = false;
                fieldEmailUsers.BackColor = Color.Red;
            }
            else
            {
                fieldEmailUsers.BackColor = Color.White;
            }

            return checkOK;
        }
    }
}
