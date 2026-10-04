using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigBackup {
    public partial class BackupPanel : Panel {
        /// <summary>
        /// Retrieves the source and destination fields to copy them into the global configuration.
        /// </summary>
        public void getFields()
        {
            manager.backupGlobal.destinationPathBackup = labelPathDestination.Text;
            manager.backupGlobal.sourcePathBackup = labelPathSource.Text;
        }
        /// <summary>
        /// Deletes the content of all fields in the panel.
        /// </summary>
        public void clearField()
        {
            labelPathSource.Text = "";
            labelPathDestination.Text = "";
            fileToExclude.Items.Clear();
        }
        /// <summary>
        /// Checks if required fields are empty when saving. Empty fields are highlighted in red.
        /// </summary>
        /// <returns>true if all required fields are not empty, false otherwise<returns>
        public bool checkFieldObligation()
        {
            bool checkOK = true;
            if (string.IsNullOrWhiteSpace(labelPathSource.Text))
            {
                checkOK = false;
                labelPathSource.BackColor = Color.Red;
            }
            else
            {
                labelPathSource.BackColor = Color.White;
            }
            if (string.IsNullOrWhiteSpace(labelPathDestination.Text))
            {
                checkOK = false;
                labelPathDestination.BackColor = Color.Red;
            }
            else
            {
                labelPathDestination.BackColor = Color.White;
            }

            return checkOK && checkSourceAndDestination;
        }
    }
}
