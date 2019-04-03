namespace Xzy.EmbeddedApp.WinForm
{
    partial class WhatAppForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WhatAppForm));
            this.label1 = new System.Windows.Forms.Label();
            this.txtCount = new System.Windows.Forms.TextBox();
            this.btnStart = new System.Windows.Forms.Button();
            this.openApp = new System.Windows.Forms.OpenFileDialog();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnChose = new System.Windows.Forms.Button();
            this.btnInit = new System.Windows.Forms.Button();
            this.appContainer1 = new Xzy.EmbeddedApp.AppContainer(this.components);
            this.appContainer2 = new Xzy.EmbeddedApp.AppContainer(this.components);
            this.SuspendLayout();
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtCount
            // 
            resources.ApplyResources(this.txtCount, "txtCount");
            this.txtCount.Name = "txtCount";
            // 
            // btnStart
            // 
            resources.ApplyResources(this.btnStart, "btnStart");
            this.btnStart.Name = "btnStart";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // openApp
            // 
            this.openApp.FileName = "openFileDialog1";
            resources.ApplyResources(this.openApp, "openApp");
            // 
            // txtPath
            // 
            resources.ApplyResources(this.txtPath, "txtPath");
            this.txtPath.Name = "txtPath";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // btnChose
            // 
            resources.ApplyResources(this.btnChose, "btnChose");
            this.btnChose.Name = "btnChose";
            this.btnChose.UseVisualStyleBackColor = true;
            this.btnChose.Click += new System.EventHandler(this.btnChose_Click);
            // 
            // btnInit
            // 
            resources.ApplyResources(this.btnInit, "btnInit");
            this.btnInit.Name = "btnInit";
            this.btnInit.UseVisualStyleBackColor = true;
            this.btnInit.Click += new System.EventHandler(this.btnInit_Click);
            // 
            // appContainer1
            // 
            resources.ApplyResources(this.appContainer1, "appContainer1");
            this.appContainer1.AppFilename = "";
            this.appContainer1.AppProcess = null;
            this.appContainer1.Name = "appContainer1";
            // 
            // appContainer2
            // 
            resources.ApplyResources(this.appContainer2, "appContainer2");
            this.appContainer2.AppFilename = "";
            this.appContainer2.AppProcess = null;
            this.appContainer2.Name = "appContainer2";
            // 
            // WhatAppForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.appContainer2);
            this.Controls.Add(this.appContainer1);
            this.Controls.Add(this.btnInit);
            this.Controls.Add(this.btnChose);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.txtCount);
            this.Controls.Add(this.label1);
            this.Name = "WhatAppForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCount;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.OpenFileDialog openApp;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnChose;
        private System.Windows.Forms.Button btnInit;
        private Xzy.EmbeddedApp.AppContainer appContainer1;
        private Xzy.EmbeddedApp.AppContainer appContainer2;
    }
}