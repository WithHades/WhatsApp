namespace SocketTestApp.WinForm
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.txt_sendmsg = new System.Windows.Forms.TextBox();
            this.txt_receivemsg = new System.Windows.Forms.TextBox();
            this.btn_send = new System.Windows.Forms.Button();
            this.btn_reflsh = new System.Windows.Forms.Button();
            this.txt_ipaddress = new System.Windows.Forms.TextBox();
            this.txt_point = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_logs = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.lab_listenstatus = new System.Windows.Forms.Label();
            this.btn_closecontent = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txt_sendmsg
            // 
            this.txt_sendmsg.Location = new System.Drawing.Point(22, 37);
            this.txt_sendmsg.Multiline = true;
            this.txt_sendmsg.Name = "txt_sendmsg";
            this.txt_sendmsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_sendmsg.Size = new System.Drawing.Size(503, 122);
            this.txt_sendmsg.TabIndex = 0;
            // 
            // txt_receivemsg
            // 
            this.txt_receivemsg.Location = new System.Drawing.Point(22, 208);
            this.txt_receivemsg.Multiline = true;
            this.txt_receivemsg.Name = "txt_receivemsg";
            this.txt_receivemsg.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_receivemsg.Size = new System.Drawing.Size(503, 122);
            this.txt_receivemsg.TabIndex = 1;
            // 
            // btn_send
            // 
            this.btn_send.Location = new System.Drawing.Point(582, 89);
            this.btn_send.Name = "btn_send";
            this.btn_send.Size = new System.Drawing.Size(75, 23);
            this.btn_send.TabIndex = 2;
            this.btn_send.Text = "发送";
            this.btn_send.UseVisualStyleBackColor = true;
            this.btn_send.Click += new System.EventHandler(this.btn_send_Click);
            // 
            // btn_reflsh
            // 
            this.btn_reflsh.Location = new System.Drawing.Point(582, 265);
            this.btn_reflsh.Name = "btn_reflsh";
            this.btn_reflsh.Size = new System.Drawing.Size(75, 23);
            this.btn_reflsh.TabIndex = 3;
            this.btn_reflsh.Text = "打开服务端";
            this.btn_reflsh.UseVisualStyleBackColor = true;
            this.btn_reflsh.Click += new System.EventHandler(this.btn_reflsh_Click);
            // 
            // txt_ipaddress
            // 
            this.txt_ipaddress.Location = new System.Drawing.Point(81, 512);
            this.txt_ipaddress.Name = "txt_ipaddress";
            this.txt_ipaddress.Size = new System.Drawing.Size(143, 21);
            this.txt_ipaddress.TabIndex = 4;
            this.txt_ipaddress.Text = "127.0.0.1";
            // 
            // txt_point
            // 
            this.txt_point.Location = new System.Drawing.Point(265, 512);
            this.txt_point.Name = "txt_point";
            this.txt_point.Size = new System.Drawing.Size(73, 21);
            this.txt_point.TabIndex = 5;
            this.txt_point.Text = "22222";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 515);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 6;
            this.label1.Text = "IP地址：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(232, 515);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "端口";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 182);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 8;
            this.label3.Text = "接收内容";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 9);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 9;
            this.label4.Text = "发送内容";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(20, 355);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 10;
            this.label5.Text = "日志";
            // 
            // txt_logs
            // 
            this.txt_logs.Location = new System.Drawing.Point(22, 376);
            this.txt_logs.Multiline = true;
            this.txt_logs.Name = "txt_logs";
            this.txt_logs.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txt_logs.Size = new System.Drawing.Size(503, 122);
            this.txt_logs.TabIndex = 11;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(369, 512);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 12;
            this.btn_start.Text = "启动连接";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // lab_listenstatus
            // 
            this.lab_listenstatus.AutoSize = true;
            this.lab_listenstatus.BackColor = System.Drawing.SystemColors.Control;
            this.lab_listenstatus.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_listenstatus.ForeColor = System.Drawing.Color.Red;
            this.lab_listenstatus.Location = new System.Drawing.Point(20, 548);
            this.lab_listenstatus.Name = "lab_listenstatus";
            this.lab_listenstatus.Size = new System.Drawing.Size(93, 16);
            this.lab_listenstatus.TabIndex = 13;
            this.lab_listenstatus.Text = "未建立连接";
            // 
            // btn_closecontent
            // 
            this.btn_closecontent.Location = new System.Drawing.Point(450, 512);
            this.btn_closecontent.Name = "btn_closecontent";
            this.btn_closecontent.Size = new System.Drawing.Size(75, 23);
            this.btn_closecontent.TabIndex = 14;
            this.btn_closecontent.Text = "断开连接";
            this.btn_closecontent.UseVisualStyleBackColor = true;
            this.btn_closecontent.Click += new System.EventHandler(this.btn_closecontent_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 632);
            this.Controls.Add(this.btn_closecontent);
            this.Controls.Add(this.lab_listenstatus);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.txt_logs);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_point);
            this.Controls.Add(this.txt_ipaddress);
            this.Controls.Add(this.btn_reflsh);
            this.Controls.Add(this.btn_send);
            this.Controls.Add(this.txt_receivemsg);
            this.Controls.Add(this.txt_sendmsg);
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txt_sendmsg;
        private System.Windows.Forms.TextBox txt_receivemsg;
        private System.Windows.Forms.Button btn_send;
        private System.Windows.Forms.Button btn_reflsh;
        private System.Windows.Forms.TextBox txt_ipaddress;
        private System.Windows.Forms.TextBox txt_point;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_logs;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Label lab_listenstatus;
        private System.Windows.Forms.Button btn_closecontent;
    }
}

