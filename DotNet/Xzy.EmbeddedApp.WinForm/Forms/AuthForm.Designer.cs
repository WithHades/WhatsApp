namespace Xzy.EmbeddedApp.WinForm.Forms
{
    partial class AuthForm
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
            this.lab_authmsg = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btn_retry = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lab_authmsg
            // 
            this.lab_authmsg.AutoSize = true;
            this.lab_authmsg.Font = new System.Drawing.Font("宋体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_authmsg.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.lab_authmsg.Location = new System.Drawing.Point(45, 118);
            this.lab_authmsg.Name = "lab_authmsg";
            this.lab_authmsg.Size = new System.Drawing.Size(109, 19);
            this.lab_authmsg.TabIndex = 0;
            this.lab_authmsg.Text = "请稍候……";
            // 
            // timer1
            // 
            this.timer1.Interval = 1000000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btn_retry
            // 
            this.btn_retry.Location = new System.Drawing.Point(390, 113);
            this.btn_retry.Name = "btn_retry";
            this.btn_retry.Size = new System.Drawing.Size(75, 23);
            this.btn_retry.TabIndex = 1;
            this.btn_retry.Text = "重试";
            this.btn_retry.UseVisualStyleBackColor = true;
            this.btn_retry.Visible = false;
            this.btn_retry.Click += new System.EventHandler(this.btn_retry_Click);
            // 
            // AuthForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(564, 307);
            this.Controls.Add(this.btn_retry);
            this.Controls.Add(this.lab_authmsg);
            this.Name = "AuthForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "启动验证";
            this.Shown += new System.EventHandler(this.AuthForm_Shown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lab_authmsg;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btn_retry;
    }
}