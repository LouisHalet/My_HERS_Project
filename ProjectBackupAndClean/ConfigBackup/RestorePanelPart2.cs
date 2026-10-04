using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ConfigBackup {
    public partial class RestorePanel : Panel {
        /// <summary>
        /// Pressing the buttonDestination button opens a FolderBrowserDialog.
        /// When a path is selected, the method will place that value in destinationPathRestore of the global config.
        /// A check is performed to compare the source and destination.
        /// If the verification fails, the error message "Erreur : Source et destination ne peut pas être égaux" is displayed.
        /// </summary>
        private void onDestination_Click()
        {
            var FileDialog = new FolderBrowserDialog();
            FileDialog.SelectedPath = Environment.CurrentDirectory;
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                fieldPathDestination.Text = FileDialog.SelectedPath;
                try
                {
                    textError.Visible = false;
                    validateSourceDestination();
                    manager.backupGlobal.destinationPathRestore = FileDialog.SelectedPath;
                    checkSourceAndDestination = true;
                }
                catch (SourceDestinationEqualException)
                {
                    textError.Visible = true;

                }
                catch (InvalidDaySelectionException)
                {
                }
            }
        }
        /// <summary>
        /// Retrieves the source and destination fields to copy them into the global configuration.
        /// </summary>
        public void getFields()
        {
            manager.backupGlobal.destinationPathRestore = fieldPathDestination.Text;
            if (dropDownListDay.SelectedIndex != -1)
            {
                string daySelected = dropDownListDay.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(daySelected) && !daySelected.Equals("Aucun dossier à restaurer à ce chemin de destination") && !daySelected.Equals("Veuillez sélectionner un chemin de destination dans l'onglet Sauvegarde"))
                {
                    manager.backupGlobal.sourcePathRestore = manager.backupGlobal.destinationPathBackup + "\\" + dropDownListDay.SelectedItem.ToString();
                }
                else
                {
                    manager.backupGlobal.sourcePathRestore = "";
                }
            }
            else
            {
                manager.backupGlobal.sourcePathRestore = "";
            }
        }
        /// <summary>
        /// If the source and destination fields are not empty, this function checks if the source and destination are not equal.
        /// </summary>
        /// <exception cref="SourceDestinationEqualException">If the source and destination are the same</exception>
        /// <exception cref="InvalidDaySelectionException">If the day selected is invalid</exception>
        public void validateSourceDestination()
        {
            if (dropDownListDay.SelectedIndex != -1)
            {
                string daySelected = dropDownListDay.SelectedItem.ToString();
                if (!string.IsNullOrEmpty(daySelected) && !daySelected.Equals("Aucun dossier à restaurer à ce chemin de destination") && !daySelected.Equals("Veuillez sélectionner un chemin de destination dans l'onglet Sauvegarde"))
                {
                    if (!string.IsNullOrWhiteSpace(fieldPathDestination.Text))
                    {
                        if ((manager.backupGlobal.destinationPathBackup + "\\" + dropDownListDay.SelectedItem.ToString()).Equals(fieldPathDestination.Text)) throw new SourceDestinationEqualException();
                        else checkSourceAndDestination = true;
                    }
                }
                else
                {
                    throw new InvalidDaySelectionException();
                }
            }
            else
            {
                throw new InvalidDaySelectionException();
            }
        }
        /// <summary>
        /// Deletes the content of all fields in the panel.
        /// </summary>
        public void clearField()
        {
            fieldPathDestination.Text = "";
            dropDownListDay.Items.Clear();

        }
        /// <summary>
        /// With the support of buttonLaunchRestore, the method checks if the source and destination are indeed different.
        /// If everything is valid, the runRestore method is triggered and performs the restoration.
        /// Otherwise, empty or invalid fields are displayed in red.
        /// </summary>
        private async void onLaunch_Click()
        {
            bool checkOK = true;
            try
            {
                textError.Visible = false;
                validateSourceDestination();
                dropDownListDay.BackColor = Color.White;
                fieldPathDestination.BackColor = Color.White;
            }
            catch (SourceDestinationEqualException)
            {
                textError.Visible = true;
                checkOK = false;

            }
            catch (InvalidDaySelectionException)
            {
                checkOK = false;
                dropDownListDay.BackColor = Color.Red;

            }
            if (string.IsNullOrWhiteSpace(fieldPathDestination.Text))
            {
                checkOK = false;
                fieldPathDestination.BackColor = Color.Red;
            }
            else
            {
                fieldPathDestination.BackColor = Color.White;
            }
            if (checkOK)
                await Task.WhenAll(runRestore());
            else
                this.statusRestore.Image = Properties.Resources.failure;
        }
        /// <summary>
        /// Execute the robocopy command to trigger the restoration of the day selected in the dropdown list.
        /// A GIF is launched during the restoration and an image is displayed instead when the task is completed or fails.
        /// </summary>
        /// <returns></returns>
        public async Task runRestore()
        {
            getFields();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            this.statusRestore.Image = Properties.Resources.gifLoading;

            startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "robocopy";
            startInfo.Arguments = "\"" + manager.backupGlobal.sourcePathRestore + "\" " +
                      "\"" + manager.backupGlobal.destinationPathRestore + "\"" + " /e /mir ";
            try
            {
                robotCopyProcess = new Process();
                robotCopyProcess.StartInfo = startInfo;
                robotCopyProcess.Start();
                await Task.Run(() => robotCopyProcess.WaitForExit());
                this.statusRestore.Image = Properties.Resources.Valider;
            }
            catch (Exception)
            {
                this.statusRestore.Image = Properties.Resources.failure;
            }
        }
    }
}
