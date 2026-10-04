using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BackupAndClean {
    public partial class BackupTask : MyTask {
        /// <summary>
        /// Prepares the ProcessStartInfo object before executing the command.
        /// </summary>
        /// <returns>the ProcessStartInfo object prepare</returns>
        public ProcessStartInfo prepareProcessStartInfo()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            List<string> listFolder = new List<string>();
            List<string> listFile = new List<string>();
            StringBuilder stringFolder = new StringBuilder();
            StringBuilder stringFile = new StringBuilder();
            startInfo = new ProcessStartInfo();
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "robocopy";
            if (manager.backupSetting.pathToIgnore.Any())
            {
                foreach (string s in manager.backupSetting.pathToIgnore)
                {
                    if (Directory.Exists(s)) listFolder.Add(s);
                    else if (File.Exists(s)) listFile.Add(s);
                }
                foreach (string s in listFolder)
                {
                    stringFolder.Append("\"");
                    stringFolder.Append(s);
                    stringFolder.Append("\" ");
                }
                foreach (string s in listFile)
                {
                    stringFile.Append("\"");
                    stringFile.Append(s);
                    stringFile.Append("\" ");
                }
                startInfo.Arguments = "\"" + manager.backupSetting.sourcePathBackup + "\" " +
                    "\"" + manager.backupSetting.destinationPathBackup + "\"" + " /e /xo /xd " + stringFolder.ToString() +
                    " /xf " + stringFile.ToString();
            }
            else
            {
                startInfo.Arguments = "\"" + manager.backupSetting.sourcePathBackup + "\" " +
                                        "\"" + manager.backupSetting.destinationPathBackup + "\"" + " /e /xo ";
            }
            return startInfo;
        }
        /// <summary>
        /// Execute these test methods before running the robocopy command
        /// (checkNotEmptyOrNull, checkSourceAndDestinationExist, checkSourceAndDestination, checkAvailableSize)
        /// Create (if it doesn't exist) the destination file named with the day's name using the createDayDirectory() method
        /// A custom error message is sent and displayed when a check fails.
        /// Execute the robocopy command to copy the source files/folders to the destination, ignoring
        /// or not the files/folders in the pathToIgnore list of the global configuration
        /// A GIF is launched during the running process, and an image is displayed instead when the task is completed or fails.
        /// </summary>
        /// <returns></returns>
        public override async Task run()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            try
            {
                checkNotEmptyOrNull();
                createDayDirectory(manager.backupSetting.destinationPathBackup);
                checkSourceAndDestinationExist();
                checkSourceAndDestination();
                checkAvailableSize();
                form.Invoke(() =>
                {
                    form.panelExecution.statusBackup.Image = Properties.Resources.gifLoading;
                });
                startInfo = prepareProcessStartInfo();
                await execCommand(startInfo);
            }
            catch (ArgumentNullException)
            {
                form.panelExecution.errorLabel.Text = "Echec : La source et la detination ne peut pas être vide";
                this.taskStatus = "Echec : La source et la detination ne peut pas être vide";
                form.Invoke(() =>
                {
                    form.panelExecution.statusBackup.Image = Properties.Resources.failure;
                });
            }
            catch (DirectoryNotFoundException)
            {
                form.panelExecution.errorLabel.Text = "Echec : Le dossier source ou destination n'existe pas";
                this.taskStatus = "Echec : Le dossier source ou destination n'existe pas";
                form.Invoke(() =>
                {
                    form.panelExecution.statusBackup.Image = Properties.Resources.failure;
                });
            }
            catch (SourceDestinationEqualException)
            {
                form.panelExecution.errorLabel.Text = "Echec : Le dossier source ne peut être égale à la destination";
                this.taskStatus = "Echec : Le dossier source ne peut être égale à la destination";
                form.Invoke(() =>
                {
                    form.panelExecution.statusBackup.Image = Properties.Resources.failure;
                });
            }
            catch (NoSpaceInDrive)
            {
                form.panelExecution.errorLabel.Text = "Echec : Espace disque trop petit";
                this.taskStatus = "Echec : Espace disque trop petit";
                form.Invoke(() =>
                {
                    form.panelExecution.statusBackup.Image = Properties.Resources.failure;
                });
            }
        }
        /// <summary>
        /// Executes the robocopy command using the ProcessStartInfo passed as a parameter
        /// </summary>
        /// <param name="startInfo">ProcessStartInfo linked to the command</param>
        /// <returns></returns>
        public async Task execCommand(ProcessStartInfo startInfo)
        {

            try
            {
                robotCopyProcess = new Process();
                robotCopyProcess.StartInfo = startInfo;
                robotCopyProcess.Start();
                await Task.Run(() => robotCopyProcess.WaitForExit());
                int exitCode = robotCopyProcess.ExitCode;

                if (exitCode < 7)
                {
                    this.taskStatus = "Réussit";
                    form.Invoke(() =>
                    {
                        form.panelExecution.statusBackup.Image = Properties.Resources.Valider;
                    });
                }
                else
                {
                    this.taskStatus = "Echec";

                    form.Invoke(() =>
                    {
                        form.panelExecution.statusBackup.Image = Properties.Resources.failure;
                    });
                }
            }

            catch (Exception)
            {
                this.taskStatus = "Echec";

                form.panelExecution.statusBackup.Image = Properties.Resources.failure;

            }
        }
        
        /// <summary>
        /// Stop the backup task and change the task status to “Cancelled”
        /// </summary>
        /// <returns></returns>
        public override async Task stop()
        {
            if(robotCopyProcess != null)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                if (string.IsNullOrEmpty(taskStatus))
                {
                    this.taskStatus = "Annulée";
                }
                startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.FileName = "taskkill";
                startInfo.Arguments = " /F /Pid " + robotCopyProcess.Id + " /T";
                killProcess = new Process();
                killProcess.StartInfo = startInfo;
                killProcess.Start();
                await Task.Run(() => killProcess.WaitForExit());
            }
            
        }
    }
}
