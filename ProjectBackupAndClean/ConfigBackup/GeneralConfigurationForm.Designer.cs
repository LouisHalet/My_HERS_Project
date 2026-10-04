namespace ConfigBackup {
    partial class GeneralConfigurationForm {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // GeneralConfigurationForm
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(800, 500);
            Name = "GeneralConfigurationForm";
            Text = "Configuration";
            Load += GeneralConfigurationForm_Load;
            ResumeLayout(false);

        }

        #endregion
    }
}

