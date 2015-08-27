namespace ScanToCloud.WinUI.Forms
{
    partial class WebAuthentication
    {
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebAuthentication));
            this.wbrMain = new System.Windows.Forms.WebBrowser();
            this.chkSaveLogin = new System.Windows.Forms.CheckBox();
            this.btnContinue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // wbrMain
            // 
            resources.ApplyResources(this.wbrMain, "wbrMain");
            this.wbrMain.Name = "wbrMain";
            this.wbrMain.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.wbrMain_Navigated);
            this.wbrMain.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.wbrMain_Navigating);
            // 
            // chkSaveLogin
            // 
            resources.ApplyResources(this.chkSaveLogin, "chkSaveLogin");
            this.chkSaveLogin.Name = "chkSaveLogin";
            this.chkSaveLogin.UseVisualStyleBackColor = true;
            // 
            // btnContinue
            // 
            resources.ApplyResources(this.btnContinue, "btnContinue");
            this.btnContinue.Name = "btnContinue";
            this.btnContinue.UseVisualStyleBackColor = true;
            this.btnContinue.Click += new System.EventHandler(this.btnContinue_Click);
            // 
            // WebAuthentication
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnContinue);
            this.Controls.Add(this.chkSaveLogin);
            this.Controls.Add(this.wbrMain);
            this.Name = "WebAuthentication";
            this.Load += new System.EventHandler(this.WebAuthentication_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser wbrMain;
        private System.Windows.Forms.CheckBox chkSaveLogin;
        private System.Windows.Forms.Button btnContinue;
    }
}