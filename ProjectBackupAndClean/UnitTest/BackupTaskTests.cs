using BackupAndClean;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;
namespace UnitTest {
    [TestClass]
    public sealed class BackupTaskTests {

        [TestMethod]
        public void checkNotEmptyOrNull_Exception_ThrowArgumentNullException()
        {
            Execution form = new Execution();
            TaskManager taskManager = new TaskManager();
            taskManager.backupSetting = new BackupSetting();
            taskManager.backupSetting.sourcePathBackup = "";
            taskManager.backupSetting.destinationPathBackup = "";
            BackupTask backup = new BackupTask(taskManager, form, "testTaskBackup");
            Assert.ThrowsExactly<ArgumentNullException>(() => backup.checkNotEmptyOrNull());
            backup.manager.backupSetting.sourcePathBackup = "\"C:\\Users\\louisNonAdmin\\Documents\\Projet_C#\\3_Depot_final\\ProjectBackupAndClean\\BackupAndClean\\CleanTask.cs\"";
            backup.manager.backupSetting.destinationPathBackup = "";
            Assert.ThrowsExactly<ArgumentNullException>(() => backup.checkNotEmptyOrNull());

        }
        [TestMethod]
        public void checkSourceAndDestinationExist_Exception_ThrowDirectoryNotFoundException()
        {
            Execution form = new Execution();
            TaskManager taskManager = new TaskManager();
            taskManager.backupSetting = new BackupSetting();
            taskManager.backupSetting.sourcePathBackup = "";
            taskManager.backupSetting.destinationPathBackup = "";
            BackupTask backup = new BackupTask(taskManager, form, "testTaskBackup");
            Assert.ThrowsExactly<DirectoryNotFoundException>(() => backup.checkSourceAndDestinationExist());
            // Ne passe pas (logique)
            /*
            backup.manager.backupSetting.sourcePathBackup = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            backup.manager.backupSetting.destinationPathBackup = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            Assert.ThrowsExactly<DirectoryNotFoundException>(() => backup.checkSourceAndDestinationExist());
            */
        }
        [TestMethod]
        public void createDayDirectory_FileExist_True()
        {
            Execution form = new Execution();
            TaskManager taskManager = new TaskManager();
            taskManager.backupSetting = new BackupSetting();
            taskManager.backupSetting.sourcePathBackup = "";
            taskManager.backupSetting.destinationPathBackup = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            BackupTask backup = new BackupTask(taskManager, form, "testTaskBackup");
            backup.createDayDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"));
            Assert.IsTrue(Directory.Exists(backup.manager.backupSetting.destinationPathBackup));
        }

        [TestMethod]
        public void checkSourceAndDestination_Exception_SourceDestinationEqualException()
        {
            Execution form = new Execution();
            TaskManager taskManager = new TaskManager();
            taskManager.backupSetting = new BackupSetting();
            taskManager.backupSetting.sourcePathBackup = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            taskManager.backupSetting.destinationPathBackup = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            BackupTask backup = new BackupTask(taskManager, form, "testTaskBackup");
            Assert.ThrowsExactly<SourceDestinationEqualException>(() => backup.checkSourceAndDestination());
            backup.createDayDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"));
            Assert.ThrowsExactly<SourceDestinationEqualException>(() => backup.checkSourceAndDestination());
        }
        /*
        [TestMethod]
        public async Task run_CopyOk_taskStatuIsReussit()
        {
            Execution form = new Execution();
            TaskManager taskManager = new TaskManager();
            taskManager.backupSetting = new BackupSetting();
            taskManager.backupSetting.sourcePathBackup = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads", "ProjectBackupAndClean");
            taskManager.backupSetting.destinationPathBackup = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            BackupTask backup = new BackupTask(taskManager, form, "testTaskBackup");
            await backup.run();
            Assert.IsTrue(backup.taskStatus.Equals("Réussit"));
        }
        */
    }
}
