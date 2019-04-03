namespace Xzy.EmbeddedApp.WinForm
{
    partial class StartUpForm
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
            this.lblProductName = new System.Windows.Forms.Label();
            this.rad_langchina = new System.Windows.Forms.RadioButton();
            this.rad_langenglish = new System.Windows.Forms.RadioButton();
            this.lblLanguage = new System.Windows.Forms.Label();
            this.lblGroupNums = new System.Windows.Forms.Label();
            this.text_groupnums = new System.Windows.Forms.TextBox();
            this.lblRowNums = new System.Windows.Forms.Label();
            this.text_rownums = new System.Windows.Forms.TextBox();
            this.lblAuthorizationStatus = new System.Windows.Forms.Label();
            this.lab_msg = new System.Windows.Forms.Label();
            this.btn_saveparameter = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.btn_start = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblProductName
            // 
            this.lblProductName.AutoSize = true;
            this.lblProductName.Font = new System.Drawing.Font("宋体", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblProductName.ForeColor = System.Drawing.Color.Black;
            this.lblProductName.Location = new System.Drawing.Point(153, 39);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(311, 35);
            this.lblProductName.TabIndex = 0;
            this.lblProductName.Text = "WhatsApp群控系统";
            // 
            // rad_langchina
            // 
            this.rad_langchina.AutoSize = true;
            this.rad_langchina.Location = new System.Drawing.Point(228, 141);
            this.rad_langchina.Name = "rad_langchina";
            this.rad_langchina.Size = new System.Drawing.Size(47, 16);
            this.rad_langchina.TabIndex = 1;
            this.rad_langchina.TabStop = true;
            this.rad_langchina.Text = "中文";
            this.rad_langchina.UseVisualStyleBackColor = true;
            this.rad_langchina.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // rad_langenglish
            // 
            this.rad_langenglish.AutoSize = true;
            this.rad_langenglish.Location = new System.Drawing.Point(336, 141);
            this.rad_langenglish.Name = "rad_langenglish";
            this.rad_langenglish.Size = new System.Drawing.Size(47, 16);
            this.rad_langenglish.TabIndex = 2;
            this.rad_langenglish.TabStop = true;
            this.rad_langenglish.Text = "英文";
            this.rad_langenglish.UseVisualStyleBackColor = true;
            this.rad_langenglish.CheckedChanged += new System.EventHandler(this.rad_CheckedChanged);
            // 
            // lblLanguage
            // 
            this.lblLanguage.AutoSize = true;
            this.lblLanguage.Location = new System.Drawing.Point(226, 112);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(65, 12);
            this.lblLanguage.TabIndex = 3;
            this.lblLanguage.Text = "系统语言：";
            // 
            // lblGroupNums
            // 
            this.lblGroupNums.AutoSize = true;
            this.lblGroupNums.Location = new System.Drawing.Point(226, 186);
            this.lblGroupNums.Name = "lblGroupNums";
            this.lblGroupNums.Size = new System.Drawing.Size(89, 12);
            this.lblGroupNums.TabIndex = 4;
            this.lblGroupNums.Text = "每组显示手机：";
            // 
            // text_groupnums
            // 
            this.text_groupnums.Location = new System.Drawing.Point(228, 207);
            this.text_groupnums.Name = "text_groupnums";
            this.text_groupnums.Size = new System.Drawing.Size(135, 21);
            this.text_groupnums.TabIndex = 5;
            this.text_groupnums.Text = "12";
            // 
            // lblRowNums
            // 
            this.lblRowNums.AutoSize = true;
            this.lblRowNums.Location = new System.Drawing.Point(226, 256);
            this.lblRowNums.Name = "lblRowNums";
            this.lblRowNums.Size = new System.Drawing.Size(89, 12);
            this.lblRowNums.TabIndex = 6;
            this.lblRowNums.Text = "每行显示手机：";
            // 
            // text_rownums
            // 
            this.text_rownums.Location = new System.Drawing.Point(228, 281);
            this.text_rownums.Name = "text_rownums";
            this.text_rownums.Size = new System.Drawing.Size(135, 21);
            this.text_rownums.TabIndex = 7;
            this.text_rownums.Text = "6";
            // 
            // lblAuthorizationStatus
            // 
            this.lblAuthorizationStatus.AutoSize = true;
            this.lblAuthorizationStatus.Location = new System.Drawing.Point(226, 328);
            this.lblAuthorizationStatus.Name = "lblAuthorizationStatus";
            this.lblAuthorizationStatus.Size = new System.Drawing.Size(65, 12);
            this.lblAuthorizationStatus.TabIndex = 8;
            this.lblAuthorizationStatus.Text = "授权状态：";
            // 
            // lab_msg
            // 
            this.lab_msg.AutoSize = true;
            this.lab_msg.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_msg.Location = new System.Drawing.Point(225, 352);
            this.lab_msg.Name = "lab_msg";
            this.lab_msg.Size = new System.Drawing.Size(112, 16);
            this.lab_msg.TabIndex = 9;
            this.lab_msg.Text = "授权手机100部";
            // 
            // btn_saveparameter
            // 
            this.btn_saveparameter.Location = new System.Drawing.Point(152, 401);
            this.btn_saveparameter.Name = "btn_saveparameter";
            this.btn_saveparameter.Size = new System.Drawing.Size(75, 23);
            this.btn_saveparameter.TabIndex = 10;
            this.btn_saveparameter.Text = "保存参数";
            this.btn_saveparameter.UseVisualStyleBackColor = true;
            this.btn_saveparameter.Click += new System.EventHandler(this.btn_saveparameter_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(426, 401);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 11;
            this.btn_close.Text = "关闭";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(288, 401);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 12;
            this.btn_start.Text = "启动群控";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // StartUpForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(778, 461);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_saveparameter);
            this.Controls.Add(this.lab_msg);
            this.Controls.Add(this.lblAuthorizationStatus);
            this.Controls.Add(this.text_rownums);
            this.Controls.Add(this.lblRowNums);
            this.Controls.Add(this.text_groupnums);
            this.Controls.Add(this.lblGroupNums);
            this.Controls.Add(this.lblLanguage);
            this.Controls.Add(this.rad_langenglish);
            this.Controls.Add(this.rad_langchina);
            this.Controls.Add(this.lblProductName);
            this.Name = "StartUpForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WhatsApp群控";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblProductName;
        private System.Windows.Forms.RadioButton rad_langchina;
        private System.Windows.Forms.RadioButton rad_langenglish;
        private System.Windows.Forms.Label lblLanguage;
        private System.Windows.Forms.Label lblGroupNums;
        private System.Windows.Forms.TextBox text_groupnums;
        private System.Windows.Forms.Label lblRowNums;
        private System.Windows.Forms.TextBox text_rownums;
        private System.Windows.Forms.Label lblAuthorizationStatus;
        private System.Windows.Forms.Label lab_msg;
        private System.Windows.Forms.Button btn_saveparameter;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Button btn_start;
    }
}