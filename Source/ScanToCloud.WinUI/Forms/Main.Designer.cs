namespace ScanToCloud.WinUI.Forms
{
    partial class Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnSetupSource = new System.Windows.Forms.Button();
            this.cmbSources = new System.Windows.Forms.ComboBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnNewDocument = new System.Windows.Forms.Button();
            this.btnAddScan = new System.Windows.Forms.Button();
            this.btnAddImage = new System.Windows.Forms.Button();
            this.btnSave = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flpContentItems = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            resources.ApplyResources(this.groupBox1, "groupBox1");
            this.groupBox1.Controls.Add(this.btnSetupSource);
            this.groupBox1.Controls.Add(this.cmbSources);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.TabStop = false;
            // 
            // btnSetupSource
            // 
            resources.ApplyResources(this.btnSetupSource, "btnSetupSource");
            this.btnSetupSource.Name = "btnSetupSource";
            this.btnSetupSource.UseVisualStyleBackColor = true;
            this.btnSetupSource.Click += new System.EventHandler(this.btnSetupSource_Click);
            // 
            // cmbSources
            // 
            resources.ApplyResources(this.cmbSources, "cmbSources");
            this.cmbSources.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSources.FormattingEnabled = true;
            this.cmbSources.Name = "cmbSources";
            this.cmbSources.SelectedIndexChanged += new System.EventHandler(this.cmbSources_SelectedIndexChanged);
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            resources.ApplyResources(this.toolStrip1, "toolStrip1");
            this.toolStrip1.Name = "toolStrip1";
            // 
            // btnNewDocument
            // 
            resources.ApplyResources(this.btnNewDocument, "btnNewDocument");
            this.btnNewDocument.Name = "btnNewDocument";
            this.btnNewDocument.UseVisualStyleBackColor = true;
            this.btnNewDocument.Click += new System.EventHandler(this.btnNewDocument_Click);
            // 
            // btnAddScan
            // 
            resources.ApplyResources(this.btnAddScan, "btnAddScan");
            this.btnAddScan.Name = "btnAddScan";
            this.btnAddScan.UseVisualStyleBackColor = true;
            this.btnAddScan.Click += new System.EventHandler(this.btnAddScan_Click);
            // 
            // btnAddImage
            // 
            resources.ApplyResources(this.btnAddImage, "btnAddImage");
            this.btnAddImage.Name = "btnAddImage";
            this.btnAddImage.UseVisualStyleBackColor = true;
            this.btnAddImage.Click += new System.EventHandler(this.btnAddImage_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.UseVisualStyleBackColor = true;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // groupBox2
            // 
            resources.ApplyResources(this.groupBox2, "groupBox2");
            this.groupBox2.Controls.Add(this.flpContentItems);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.TabStop = false;
            // 
            // flpContentItems
            // 
            this.flpContentItems.AllowDrop = true;
            resources.ApplyResources(this.flpContentItems, "flpContentItems");
            this.flpContentItems.Name = "flpContentItems";
            this.flpContentItems.DragDrop += new System.Windows.Forms.DragEventHandler(this.flpContentItems_DragDrop);
            this.flpContentItems.DragEnter += new System.Windows.Forms.DragEventHandler(this.flpContentItems_DragEnter);
            // 
            // Main
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.btnAddImage);
            this.Controls.Add(this.btnAddScan);
            this.Controls.Add(this.btnNewDocument);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.groupBox1);
            this.Name = "Main";
            this.Load += new System.EventHandler(this.Main_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox cmbSources;
        private System.Windows.Forms.Button btnSetupSource;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.Button btnNewDocument;
        private System.Windows.Forms.Button btnAddScan;
        private System.Windows.Forms.Button btnAddImage;
        private System.Windows.Forms.Button btnSave;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flpContentItems;
    }
}