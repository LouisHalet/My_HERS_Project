using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigBackup {
    public partial class GeneralConfigurationForm : Form {
        PanelManager panelManager;
        public GeneralConfigurationForm()
        {
            this.AutoScaleMode = AutoScaleMode.None;
            InitializeComponent();
            this.Size = new Size(800, 500);
            panelManager = new PanelManager(this);
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
        }

        private void GeneralConfigurationForm_Load(object sender, EventArgs e)
        {

        }
    }
}
