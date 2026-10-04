using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Mail;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TrackBar;

namespace BackupAndClean {
    public class TaskManager {
        List<MyTask> listTask;
        BackupTask backup;
        CleanTask clean; 
        BackupSetting backupSettingPrivate;
        Execution form;
        bool emailIsSend = false;
        bool doShutDown = true;
        public BackupSetting backupSetting {
            get { return backupSettingPrivate; }
            set { backupSettingPrivate = value; }
        }
        /// <summary>
        /// Constructs a TaskManager object by initializing its form parameter with the form as a parameter.
        /// This constructor retrieves information from the config.json file using initBackupSetting().
        /// He creates the two spots and then the spears.
        /// If the file does not exist, the message "Error: Missing .json file" is displayed and the status images change to false.
        /// </summary>
        /// <param name="form">the application form</param>
        public TaskManager(Execution form)
        {
            listTask = new List<MyTask>();
            backupSetting = new BackupSetting();
            this.form = form;
            try
            {
                initBackupSetting();
                backup = new BackupTask(this, form, "backupTask");
                clean = new CleanTask(this, form, "cleanTask");
                listTask.Add(backup);
                listTask.Add(clean);
                runAll();
            }
            catch (ArgumentNullException) 
            {
                form.panelExecution.errorLabel.Text = "Erreur : Fichier .json manquant";
                emailIsSend = true;
                form.Invoke(() =>
                {
                    form.panelExecution.statusBackup.Image = Properties.Resources.failure;
                    form.panelExecution.statusClean.Image = Properties.Resources.failure;
                });
            }
        }
        public TaskManager()
        {
            
        }
        /// <summary>
        /// Retrieves and initializes the backupSetting object from config.json
        /// </summary>
        /// <exception cref="ArgumentNullException">if backupSetting is null (i.e., file does not exist)</exception>
        public void initBackupSetting()
        {
            backupSetting = backupSetting.readConfigInJson();
            if (backupSetting == null) throw new ArgumentNullException();
        }
        /// <summary>
        /// Launch all tasks simultaneously.
        /// When the tasks are finished, an email is sent.
        /// If an error is detected during sending, the message "Error sending email" is displayed.
        /// </summary>
        private async void runAll()
        {
            await Task.WhenAll(backup.run(), clean.run());
            try
            {
                if (!emailIsSend)
                {
                    Email sendEmail = new Email(backupSetting, backup.taskStatus, clean.taskStatus);
                    sendEmail.send();
                    emailIsSend = true;
                }
            }
            catch (Exception)
            {
                form.panelExecution.errorLabel.Text = "Erreur lors de l'envoi de l'email";
                
            }
            if (doShutDown)
                Process.Start("shutdown", "/s /t 4");

        }
        /// <summary>
        /// Stop all tasks.
        /// An email is sent if it hasn't already been done.
        /// </summary>
        public void stopAll()
        {
            doShutDown = false;
            foreach (MyTask task in listTask)
            {
                task.stop();
            }
            if (!emailIsSend)
            {
                Email sendEmail = new Email(backupSetting, backup.taskStatus, clean.taskStatus);
                sendEmail.send();
                emailIsSend = true;
            }
            
        }

    }
}
