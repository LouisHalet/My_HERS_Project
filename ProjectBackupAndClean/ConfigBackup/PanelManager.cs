using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConfigBackup {
    public class PanelManager {
        List<Panel> ListPanel { get; set; }
        TopPanel TopPanel;
        BottomPanel BottomPanel;
        BackupSetting backupPrivate;
        public BackupSetting backupGlobal
        {
            get { return this.backupPrivate; }
            set { backupPrivate = value; }
        }
        public BackupPanel BackupPanel { get; private set; }
        RestorePanel RestorePanelPrivate;
        public RestorePanel RestorePanel
        {
            get { return this.RestorePanelPrivate; }
            set { RestorePanelPrivate = value; }
        }
        public EmailPanel EmailPanel { get; private set; }
        /// <summary>
        /// Constructs a PanelManager object
        /// Create all panel to display
        /// The panel TopPanel, BottomPanel and BackupPanel is visible
        /// </summary>
        /// <param name="form">This is the form window</param>
        public PanelManager(GeneralConfigurationForm form)
        {
            ListPanel = new List<Panel>();
            backupGlobal = new BackupSetting();
            TopPanel = new TopPanel(form, this, true);
            BottomPanel = new BottomPanel(form, this, true);
            BackupPanel = new BackupPanel(form, this, true);
            RestorePanel = new RestorePanel(form, this, false);
            EmailPanel = new EmailPanel(form, this, false);
            
            ListPanel.Add(BackupPanel);
            ListPanel.Add(RestorePanel);
            ListPanel.Add(EmailPanel);
        }
        /// <summary>
        /// Default constructor
        /// </summary>
        public PanelManager()
        {
            ListPanel = new List<Panel>();
            backupGlobal = new BackupSetting();

        }
        /// <summary>
        /// Makes all panels in the ListPanel invisible
        /// </summary>
        public void hideAll()
        {
            foreach (Panel p in ListPanel)
            {
                p.Visible = false;
            }
        }
        /// <summary>
        /// Makes the panel passed as a parameter visible
        /// </summary>
        /// <param name="p">the panel to be made visible</param>
        public void showPanel(Panel p)
        {
            hideAll();
            p.Visible = true;
        }
        /// <summary>
        /// Clear all fields in the main panels (BackupPanel, RestorePanel, EmailPanel)
        /// </summary>
        public void ClearAllField()
        {
            BackupPanel.clearField();
            RestorePanel.clearField();
            EmailPanel.clearField();
        }
    }


}
