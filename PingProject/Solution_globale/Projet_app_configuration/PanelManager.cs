using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Channels;

namespace Projet_app_configuration {
    public class PanelManager {
        List<Panel> ListPanel { get; set; }
        TopPanel TopPanel;
        public BottomPanel BottomPanel { get; set; }
        public PingConfigPanel PingPanel { get; private set; }
        public EmailPanel EmailPanel { get; private set; }
        public ResetPanel ResetPanel { get; private set; }
        ConfigSetting configPrivate;
        public ConfigSetting config {
            get { return this.configPrivate; }
            set { configPrivate = value; }
        }
        public bool configOk { get; private set; }

        /// <summary>
        /// Construct the panel manager
        /// </summary>
        /// <param name="form">This is the form window</param>
        public PanelManager(GeneralConfigurationForm form) {
            ListPanel = new List<Panel>();
            config = new ConfigSetting();
            TopPanel = new TopPanel(form, this, true);
            BottomPanel = new BottomPanel(form, this, true);
            PingPanel = new PingConfigPanel(form, this, true);
            EmailPanel = new EmailPanel(form, this, false);
            ResetPanel = new ResetPanel(form, this, false);
            ReplaceField();
            ListPanel.Add(PingPanel);
            ListPanel.Add(EmailPanel);
            ListPanel.Add(ResetPanel);
        }

        /// <summary>
        /// Makes all panels in the ListPanel invisible
        /// </summary>
        public void HideAll() {
            foreach (Panel p in ListPanel) {
                p.Visible = false;
            }
        }
        /// <summary>
        /// Makes the panel passed as a parameter visible
        /// </summary>
        /// <param name="p">the panel to be made visible</param>
        public void ShowPanel(Panel p) {
            HideAll();
            p.Visible = true;
        }
        /// <summary>
        /// Refresh the last Date Save on diferent panel 
        /// </summary>
        public void UpdateLastDateSave() {
            config.LastDateSave = "Last change saved on: " + DateTime.Now.ToString("H:mm dd'/'MM'/'yyyy");
            PingPanel.RefreshDate();
            EmailPanel.RefreshDate();
            BottomPanel.btnReset.Visible = true;

        }
        /// <summary>
        /// Replace the value in json in all field on the diferent panel
        /// Manage display on some buttons
        /// </summary>
        public void ReplaceField() {
            ConfigSetting configuration = config.ReadConfigInJson();
            if (configuration != null) {
                this.config = configuration;
                BottomPanel.btnReset.Visible = true;
                CheckConfig();
                BottomPanel.PlaceButtons();
                PingPanel.ReplaceField();
                EmailPanel.ReplaceField();
            }
            else {
                BottomPanel.btnReset.Visible = false;
                BottomPanel.btnExitAndLauch.Visible = false;
                BottomPanel.PlaceButtons();
                PingPanel.RefreshDate();
                EmailPanel.RefreshDate();
            }
        }

        /// <summary>
        /// Clear all field on diferent panel
        /// </summary>
        public void ClearAllField() {
            CheckConfig();
            PingPanel.ClearField();
            EmailPanel.ClearField();
        }

        /// <summary>
        /// Check the config for displays some buttons
        /// </summary>
        /// <returns></returns>
        public void CheckConfig() {
            configOk = false;
            if(config.listDevice.Count > 0 && config.listDevice.Count <= 8 && !string.IsNullOrEmpty(config.SMTPEmail) && !string.IsNullOrEmpty(config.SMTPHost) && !string.IsNullOrEmpty(config.SMTPPassword) && config.SMTPPort > 0) {
                configOk = config.listDevice.Any(c => !string.IsNullOrEmpty(c.Name) && !string.IsNullOrEmpty(c.IPAddress) && c.SecondInterval > 0);
            }
            BottomPanel.btnExitAndLauch.Visible = configOk;
            BottomPanel.PlaceButtons();
            
        }
    }
}
