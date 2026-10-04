namespace BackupAndClean {
    public partial class Execution : Form {
        public TaskManager taskManager { get; private set; }
        public PanelExecution panelExecution { get; private set; }
        public Execution()
        {
            InitializeComponent();
            this.TopMost = true;
            panelExecution = new PanelExecution(this, true);
        }
        private void ExecutionForm_Load(object sender, EventArgs e)
        {
            taskManager = new TaskManager(this);
        }
        private void Execution_FormClosing(object sender, FormClosingEventArgs e)
        {
            taskManager.stopAll();
        }
    }
}
