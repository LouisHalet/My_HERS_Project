namespace Projet_app_configuration {
    public partial class GeneralConfigurationForm : Form {
        PanelManager panelManager;
        public GeneralConfigurationForm() {
            InitializeComponent();
            panelManager = new PanelManager(this);
        }
    }
}
