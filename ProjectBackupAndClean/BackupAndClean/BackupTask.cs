using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;
using static System.Windows.Forms.DataFormats;

namespace BackupAndClean {
    public partial class BackupTask : MyTask {
        
        Process robotCopyProcess;
        Process killProcess;
        public TaskManager manager { get; private set; }
        Execution form;
        /// <summary>
        /// Creates a BackupTask object with its manager, form, and the name of this task.
        /// </summary>
        /// <param name="manager">either the taskManager</param>
        /// <param name="form">either the execution form</param>
        /// <param name="name">The name of the task</param>
        public BackupTask(TaskManager manager,Execution form,string name) : base(name)
        {
            this.form = form;
            this.manager = manager;
        }
        /// <summary>
        /// Check if the strings `sourcePathBackup` and `destinationPathBackup` are not empty.
        /// </summary>
        /// <exception cref="ArgumentNullException">if sourcePathBackup and destinationPathBackup are empty</exception>
        public void checkNotEmptyOrNull()
        {
            if (string.IsNullOrEmpty(manager.backupSetting.sourcePathBackup) || string.IsNullOrEmpty(manager.backupSetting.destinationPathBackup)) throw new ArgumentNullException();
        }
        /// <summary>
        /// Check if the sourcePathBackup and destinationPathBackup folders exist.
        /// </summary>
        /// <exception cref="DirectoryNotFoundException">The sourcePathBackup or destinationPathBackup folders do not exist.</exception>
        public void checkSourceAndDestinationExist()
        {
            if(!Directory.Exists(manager.backupSetting.sourcePathBackup) || !Directory.Exists(manager.backupSetting.destinationPathBackup)) throw new DirectoryNotFoundException();
        }
        /// <summary>
        /// Create the directory with name of the day of week if he isn't exist
        /// Put to update the destnationPath field
        /// </summary>
        /// <param name="SelectedPath"></param>
        public void createDayDirectory(string SelectedPath)
        {
            DateTime now = DateTime.Now;
            string destinationPath = SelectedPath + "\\" + now.ToString("dddd");
            Directory.CreateDirectory(destinationPath);
            manager.backupSetting.destinationPathBackup = destinationPath;
        }
        /// <summary>
        /// Checks if the destination path is in the source folder. (Prevents recursive and infinite backups)
        /// Checks if the source is equal to the destination
        /// </summary>
        /// <exception cref="SourceDestinationEqualException">if the destination path is in the source folder or if the source is the same as the destination</exception>
        public void checkSourceAndDestination()
        {
            bool StartWith = manager.backupSetting.destinationPathBackup.StartsWith(manager.backupSetting.sourcePathBackup);
            bool Equals = manager.backupSetting.destinationPathBackup.Equals(manager.backupSetting.sourcePathBackup);
            if(StartWith || Equals) throw new SourceDestinationEqualException();
        }
        /// <summary>
        /// Calculates the size of the folder passed as a parameter.
        /// Use the checkDriveFormat() method to check, in the case where the disk is in FAT32, if there are no files larger than 4 GB.
        /// Displays an error message if a format error is detected.
        /// </summary>
        /// <param name="path">The path to the folder to calculate/verify</param>
        /// <returns></returns>
        private long getSizeFolder(string path)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            long sum = 0;
            foreach (FileInfo f in directoryInfo.EnumerateFiles("*", SearchOption.AllDirectories))
            {
                sum += f.Length;

                try
                {
                    checkDriveFormat(f.Length);
                }
                catch (FileFormatException)
                {
                    this.taskStatus = "Echec : Impossible de mettre un fichier de 4 go sur un disque NTFS ";
                    form.panelExecution.errorLabel.Text = "Echec : Impossible de mettre un fichier de 4 go sur un disque NTFS";
                    form.Invoke(() =>
                    {
                        form.panelExecution.statusBackup.Image = Properties.Resources.failure;
                    });
                }

            }
            
            return sum;
        }
        /// <summary>
        /// Calculates the amount of free space on the destination disk.
        /// </summary>
        /// <returns>The size of the free space on the destination disk.</returns>
        private long getSizeAvailableOnDrive()
        {
            long AvailableFreeSpace = 0;
            string drive = manager.backupSetting.destinationPathBackup[0].ToString();
            DriveInfo driveInfo = new DriveInfo(drive);
            AvailableFreeSpace = driveInfo.AvailableFreeSpace;
            return AvailableFreeSpace;
        }
        /// <summary>
        /// Check if the size of the folder to be backed up is smaller than the remaining disk space on the destination disk.
        /// </summary>
        /// <exception cref="NoSpaceInDrive">if the size of the folder to be backed up is smaller than the remaining disk space on the destination disk.</exception>
        private void checkAvailableSize()
        {
            
            if( getSizeFolder(manager.backupSetting.sourcePathBackup) > getSizeAvailableOnDrive()) throw new NoSpaceInDrive();
        }
        /// <summary>
        /// Check if the size passed as a parameter is less than 4 GB, if the destination disk is in FAT32 format.
        /// </summary>
        /// <param name="sizeFile">file size</param>
        /// <exception cref="FileFormatException">if the size passed as a parameter is less than 4 GB, if the destination disk is in FAT32 format</exception>
        private void checkDriveFormat(long sizeFile)
        {
            bool isOK = true;
            string drive = manager.backupSetting.destinationPathBackup[0].ToString();
            DriveInfo driveInfo = new DriveInfo(drive);
            string format = driveInfo.DriveFormat;
            long maxSizeFAT32 = 4000000000L;
            if (format.Equals("FAT32"))
            {
                isOK = sizeFile < maxSizeFAT32;
            }
            if (!isOK) throw new FileFormatException();
        }

        
        
        
    }
}
