using System;
using System.Collections.Generic;
using System.Text;

namespace ConfigBackup {
    public partial class BackupPanel : Panel {

        /// <summary>
        /// When the buttonSource button is pressed, a FolderBrowserDialog opens.
        /// When a path is selected, the method will place that value in the sourcePathBackup of the global config.
        /// A check is performed to compare the source and destination.
        /// If the verification fails, the error message "Erreur : Source et destination ne peut pas être égaux" is displayed.
        /// </summary>
        private void onSource_Click()
        {
            var FileDialog = new FolderBrowserDialog();
            FileDialog.SelectedPath = Environment.CurrentDirectory;
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                labelPathSource.Text = FileDialog.SelectedPath;
                try
                {
                    textError.Visible = false;
                    validateSourceDestination();
                    manager.backupGlobal.sourcePathBackup = FileDialog.SelectedPath;
                }
                catch (Exception)
                {
                    textError.Text = "Erreur : Source et destination ne peut pas être égaux";
                    textError.Visible = true;
                }
            }
        }
        /// <summary>
        /// Pressing the buttonDestination button opens a FolderBrowserDialog.
        /// When a path is selected, the method will place that value in destinationPathBackup of the global config.
        /// It will initialize the dropdown list of the RestorePanel panel using the initContentScrollBar() method.
        /// A check is performed to compare the source and destination.
        /// If the verification fails, the error message "Erreur : Source et destination ne peut pas être égaux" is displayed.
        /// </summary>
        private void onDestination_Click()
        {
            var FileDialog = new FolderBrowserDialog();
            FileDialog.SelectedPath = Environment.CurrentDirectory;
            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                labelPathDestination.Text = FileDialog.SelectedPath;
                try
                {
                    textError.Visible = false;
                    validateSourceDestination();
                    manager.backupGlobal.destinationPathBackup = FileDialog.SelectedPath;
                    manager.RestorePanel.initContentScrollBar();
                }
                catch (SourceDestinationEqualException)
                {
                    textError.Text = "Erreur : Source et destination ne peut pas être égaux";
                    textError.Visible = true;
                }
            }
        }
        /// <summary>
        /// When the buttonFileToExclude is pressed, an OpenFileDialog opens with the selected source folder.
        /// If this form is not completed, the current case is then opened.
        /// When a path is selected, the method will add that value to the pathToIgnore list in the global configuration.
        /// and will then display it in the appropriate field.
        /// This method will check if the selected file is not present in the list.
        /// Otherwise, the path will not be added and an error message will appear. "Erreur : Ce Fichier existe déjà dans la liste" is displayed.
        /// </summary>
        private void onFileToExclude_Click()
        {
            var FileDialog = new OpenFileDialog();
            if (!string.IsNullOrEmpty(labelPathSource.Text))
            {
                FileDialog.FileName = labelPathSource.Text;
            }
            else
            {
                FileDialog.FileName = Environment.CurrentDirectory;
            }

            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!fileToExclude.Items.Contains(FileDialog.FileName))
                {
                    textError.Visible = false;
                    fileToExclude.Items.Add(FileDialog.FileName);
                    manager.backupGlobal.pathToIgnore.Add(FileDialog.FileName);
                }
                else
                {
                    textError.Text = "Erreur : Ce Fichier existe déjà dans la liste";
                    textError.Visible = true;
                }
            }
        }
        /// <summary>
        /// When the deleteLinesListBox button is clicked, the selected elements are
        /// removed from the fileToExclude and pathToIgnore lists (in the global configuration).
        /// </summary>
        private void onDeleteLines_Click()
        {
            List<string> lst = new List<string>();
            foreach (string itemCheked in fileToExclude.CheckedItems)
            {
                lst.Add(itemCheked);
            }
            foreach (string s in lst)
            {
                fileToExclude.Items.Remove(s);
                manager.backupGlobal.pathToIgnore.Remove(s);
            }

        }
        /// <summary>
        /// When the buttonFolderToExclude is pressed, a FolderBrowserDialog opens with the selected source folder.
        /// If this form is not completed, the current case is then opened.
        /// When a path is selected, the method will add that value to the pathToIgnore list in the global configuration.
        /// and will then display it in the appropriate field.
        /// This method will check if the selected file is not present in the list.
        /// Otherwise, the path will not be added and an error message will appear. "Erreur : Ce dossier existe déjà dans la liste" is displayed.
        /// </summary>
        private void onFolderToExclude_Click()
        {
            var FileDialog = new FolderBrowserDialog();
            if (!string.IsNullOrEmpty(labelPathSource.Text))
            {
                FileDialog.SelectedPath = labelPathSource.Text;
            }
            else
            {
                FileDialog.SelectedPath = Environment.CurrentDirectory;
            }

            if (FileDialog.ShowDialog() == DialogResult.OK)
            {
                if (!fileToExclude.Items.Contains(FileDialog.SelectedPath))
                {
                    textError.Visible = false;
                    fileToExclude.Items.Add(FileDialog.SelectedPath);
                    manager.backupGlobal.pathToIgnore.Add(FileDialog.SelectedPath);
                }
                else
                {
                    textError.Text = "Erreur : Ce dossier existe déjà dans la liste";
                    textError.Visible = true;
                }
            }
        }
        /// <summary>
        /// If the source and destination fields are not empty, this function checks if the source and destination are not equal.
        /// </summary>
        /// <exception cref="SourceDestinationEqualException">If the source and destination are the same</exception>
        public void validateSourceDestination()
        {
            if (!string.IsNullOrWhiteSpace(labelPathSource.Text) && !string.IsNullOrWhiteSpace(labelPathDestination.Text))
            {
                if (labelPathSource.Text.Equals(labelPathDestination.Text)) throw new SourceDestinationEqualException();
            }
            checkSourceAndDestination = true;
        }
        /// <summary>
        /// Replaces the fields in this panel based on the values ​​of the fields in the json file.
        /// </summary>
        public void replaceField()
        {
            BackupSetting backup = manager.backupGlobal.readConfigInJson();
            if (backup != null)
            {
                if (!string.IsNullOrEmpty(backup.sourcePathBackup)) labelPathSource.Text = backup.sourcePathBackup;
                if (!string.IsNullOrEmpty(backup.destinationPathBackup)) labelPathDestination.Text = backup.destinationPathBackup;


                if (backup.pathToIgnore.Any())
                {
                    fileToExclude.Items.AddRange(backup.pathToIgnore.ToArray());

                }
                manager.backupGlobal = backup;
                try
                {
                    validateSourceDestination();
                }
                catch (SourceDestinationEqualException)
                {
                    textError.Text = "Erreur : Source et destination ne peut pas être égaux";
                    textError.Visible = true;
                }
            }
        }
    }
}
