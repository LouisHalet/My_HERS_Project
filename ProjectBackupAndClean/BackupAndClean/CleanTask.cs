using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace BackupAndClean {
    public class CleanTask : MyTask {
        Process cleanProcess;
        Process killProcess;
        TaskManager manager;
        Execution form;
        /// <summary>
        /// Creates a CleanTask object with its manager, form, and the name of this task
        /// </summary>
        /// <param name="manager">either the taskManager</param>
        /// <param name="form">either the execution form</param>
        /// <param name="name">The name of the task</param>
        public CleanTask(TaskManager manager, Execution form, string name) : base(name)
        {
            this.form = form;
            this.manager = manager;
        }
        /// <summary>
        /// Executes the cleanup task using ProcessStartInfo and Process for cleanmgr /sagerun:1 command.
        /// Depending on the task status, the pictureBox in the panel changes, and the taskStatus attribute takes the value  ["Réussit", "Echec", "Echec"].
        /// </summary>
        /// <returns></returns>
        public override async Task run()
        {

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo = new ProcessStartInfo();
            form.Invoke(() =>
            {
                form.panelExecution.statusClean.Image = Properties.Resources.gifLoading;
            });
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            startInfo.FileName = "cleanmgr";
            startInfo.Arguments = " /sagerun:1";
            try
            {
                cleanProcess = new Process();
                cleanProcess.StartInfo = startInfo;
                cleanProcess.Start();
                await Task.Run(() => cleanProcess.WaitForExit());
                form.Invoke(() =>
                {
                    form.panelExecution.statusClean.Image = Properties.Resources.Valider;
                    this.taskStatus = "Réussit";
                });
            }
            catch (Exception)
            {
                
                form.panelExecution.statusClean.Image = Properties.Resources.failure;
                this.taskStatus = "Echec";
                
            }
        }
        /// <summary>
        /// Stop the cleaning task and change the task status to "Annulée".
        /// </summary>
        /// <returns></returns>
        public override async Task stop()
        {
            if (cleanProcess != null)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo();
                this.taskStatus = "Annulée";
                startInfo = new ProcessStartInfo();
                startInfo.CreateNoWindow = true;
                startInfo.UseShellExecute = false;
                startInfo.FileName = "taskkill";
                startInfo.Arguments = " /F /Pid " + cleanProcess.Id + " /T";
                killProcess = new Process();
                killProcess.StartInfo = startInfo;
                killProcess.Start();
                await Task.Run(() => killProcess.WaitForExit());
            }
                
        }

    }
}
