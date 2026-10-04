namespace Projet_app_principale {
    public partial class PingMonitoringForm : Form {
        PanelManager panelManager;
        public PingMonitoringForm() {
            InitializeComponent();
            this.Size = new Size(400, 500);
            panelManager = new PanelManager(this);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            

        }
    }
}
