namespace Xzy.EmbeddedApp.WinForm
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_testmsg = new System.Windows.Forms.TextBox();
            this.txt_socketlogs = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.btn_closephone = new System.Windows.Forms.Button();
            this.btn_whatsapp = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel_rightbox = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btn_setconfig = new System.Windows.Forms.Button();
            this.btn_installapp = new System.Windows.Forms.Button();
            this.btn_closeapps = new System.Windows.Forms.Button();
            this.btn_startapps = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txt_testmsg);
            this.panel1.Controls.Add(this.txt_socketlogs);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.btn_closephone);
            this.panel1.Controls.Add(this.btn_whatsapp);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1244, 68);
            this.panel1.TabIndex = 0;
            // 
            // txt_testmsg
            // 
            this.txt_testmsg.Location = new System.Drawing.Point(3, 11);
            this.txt_testmsg.Multiline = true;
            this.txt_testmsg.Name = "txt_testmsg";
            this.txt_testmsg.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_testmsg.Size = new System.Drawing.Size(296, 45);
            this.txt_testmsg.TabIndex = 4;
            // 
            // txt_socketlogs
            // 
            this.txt_socketlogs.Location = new System.Drawing.Point(841, 12);
            this.txt_socketlogs.Multiline = true;
            this.txt_socketlogs.Name = "txt_socketlogs";
            this.txt_socketlogs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_socketlogs.Size = new System.Drawing.Size(370, 45);
            this.txt_socketlogs.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(695, 7);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 50);
            this.button1.TabIndex = 2;
            this.button1.Text = "发送socket";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btn_closephone
            // 
            this.btn_closephone.Location = new System.Drawing.Point(523, 7);
            this.btn_closephone.Name = "btn_closephone";
            this.btn_closephone.Size = new System.Drawing.Size(130, 50);
            this.btn_closephone.TabIndex = 1;
            this.btn_closephone.Text = "关闭全部手机";
            this.btn_closephone.UseVisualStyleBackColor = true;
            this.btn_closephone.Click += new System.EventHandler(this.btn_closephone_Click);
            // 
            // btn_whatsapp
            // 
            this.btn_whatsapp.Location = new System.Drawing.Point(305, 7);
            this.btn_whatsapp.Name = "btn_whatsapp";
            this.btn_whatsapp.Size = new System.Drawing.Size(130, 50);
            this.btn_whatsapp.TabIndex = 0;
            this.btn_whatsapp.Text = "WhatsApp操作";
            this.btn_whatsapp.UseVisualStyleBackColor = true;
            this.btn_whatsapp.Click += new System.EventHandler(this.btn_whatsapp_Click);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1008, 616);
            this.panel2.TabIndex = 1;
            // 
            // panel_rightbox
            // 
            this.panel_rightbox.AutoScroll = true;
            this.panel_rightbox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel_rightbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_rightbox.Location = new System.Drawing.Point(0, 0);
            this.panel_rightbox.Name = "panel_rightbox";
            this.panel_rightbox.Size = new System.Drawing.Size(232, 616);
            this.panel_rightbox.TabIndex = 2;
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.btn_setconfig);
            this.panel3.Controls.Add(this.btn_installapp);
            this.panel3.Controls.Add(this.btn_closeapps);
            this.panel3.Controls.Add(this.btn_startapps);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 684);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1244, 76);
            this.panel3.TabIndex = 3;
            // 
            // btn_setconfig
            // 
            this.btn_setconfig.Location = new System.Drawing.Point(620, 8);
            this.btn_setconfig.Name = "btn_setconfig";
            this.btn_setconfig.Size = new System.Drawing.Size(130, 50);
            this.btn_setconfig.TabIndex = 6;
            this.btn_setconfig.Text = "设置";
            this.btn_setconfig.UseVisualStyleBackColor = true;
            this.btn_setconfig.Click += new System.EventHandler(this.btn_setconfig_Click);
            // 
            // btn_installapp
            // 
            this.btn_installapp.Location = new System.Drawing.Point(770, 8);
            this.btn_installapp.Name = "btn_installapp";
            this.btn_installapp.Size = new System.Drawing.Size(130, 50);
            this.btn_installapp.TabIndex = 5;
            this.btn_installapp.Text = "安装APK";
            this.btn_installapp.UseVisualStyleBackColor = true;
            this.btn_installapp.Click += new System.EventHandler(this.btn_installapp_Click);
            // 
            // btn_closeapps
            // 
            this.btn_closeapps.Location = new System.Drawing.Point(196, 9);
            this.btn_closeapps.Name = "btn_closeapps";
            this.btn_closeapps.Size = new System.Drawing.Size(130, 50);
            this.btn_closeapps.TabIndex = 4;
            this.btn_closeapps.Text = "关闭WhatsApp";
            this.btn_closeapps.UseVisualStyleBackColor = true;
            this.btn_closeapps.Click += new System.EventHandler(this.btn_closeapps_Click);
            // 
            // btn_startapps
            // 
            this.btn_startapps.Location = new System.Drawing.Point(32, 9);
            this.btn_startapps.Name = "btn_startapps";
            this.btn_startapps.Size = new System.Drawing.Size(130, 50);
            this.btn_startapps.TabIndex = 3;
            this.btn_startapps.Text = "启动WhatsApp";
            this.btn_startapps.UseVisualStyleBackColor = true;
            this.btn_startapps.Click += new System.EventHandler(this.btn_startapps_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 68);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel_rightbox);
            this.splitContainer1.Size = new System.Drawing.Size(1244, 616);
            this.splitContainer1.SplitterDistance = 1008;
            this.splitContainer1.TabIndex = 4;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.ClientSize = new System.Drawing.Size(1244, 760);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "whatsApp群控";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btn_whatsapp;
        private System.Windows.Forms.Button btn_closephone;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel_rightbox;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button btn_startapps;
        private System.Windows.Forms.Button btn_closeapps;
        private System.Windows.Forms.Button btn_installapp;
        private System.Windows.Forms.Button btn_setconfig;
        private System.Windows.Forms.SplitContainer splitContainer1;
        public System.Windows.Forms.TextBox txt_socketlogs;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        public System.Windows.Forms.TextBox txt_testmsg;
    }
}