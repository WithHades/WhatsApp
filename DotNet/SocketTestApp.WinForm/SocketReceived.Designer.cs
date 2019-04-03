namespace SocketTestApp.WinForm
{
    partial class SocketReceived
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txt_lives = new System.Windows.Forms.TextBox();
            this.txt_received = new System.Windows.Forms.TextBox();
            this.btn_start = new System.Windows.Forms.Button();
            this.btn_close = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_ipaddress = new System.Windows.Forms.TextBox();
            this.txt_port = new System.Windows.Forms.TextBox();
            this.lab_showmsg = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_sendmsg = new System.Windows.Forms.TextBox();
            this.btn_sendmsg = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(42, 119);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "当前连接：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(42, 227);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "收到消息：";
            // 
            // txt_lives
            // 
            this.txt_lives.Location = new System.Drawing.Point(113, 116);
            this.txt_lives.Multiline = true;
            this.txt_lives.Name = "txt_lives";
            this.txt_lives.Size = new System.Drawing.Size(448, 89);
            this.txt_lives.TabIndex = 2;
            // 
            // txt_received
            // 
            this.txt_received.Location = new System.Drawing.Point(113, 227);
            this.txt_received.Multiline = true;
            this.txt_received.Name = "txt_received";
            this.txt_received.Size = new System.Drawing.Size(448, 89);
            this.txt_received.TabIndex = 3;
            // 
            // btn_start
            // 
            this.btn_start.Location = new System.Drawing.Point(367, 32);
            this.btn_start.Name = "btn_start";
            this.btn_start.Size = new System.Drawing.Size(75, 23);
            this.btn_start.TabIndex = 4;
            this.btn_start.Text = "开始监听";
            this.btn_start.UseVisualStyleBackColor = true;
            this.btn_start.Click += new System.EventHandler(this.btn_start_Click);
            // 
            // btn_close
            // 
            this.btn_close.Location = new System.Drawing.Point(471, 32);
            this.btn_close.Name = "btn_close";
            this.btn_close.Size = new System.Drawing.Size(75, 23);
            this.btn_close.TabIndex = 5;
            this.btn_close.Text = "停止监听";
            this.btn_close.UseVisualStyleBackColor = true;
            this.btn_close.Click += new System.EventHandler(this.btn_close_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(42, 37);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 6;
            this.label3.Text = "IP地址：";
            // 
            // txt_ipaddress
            // 
            this.txt_ipaddress.Location = new System.Drawing.Point(101, 34);
            this.txt_ipaddress.Name = "txt_ipaddress";
            this.txt_ipaddress.Size = new System.Drawing.Size(130, 21);
            this.txt_ipaddress.TabIndex = 7;
            this.txt_ipaddress.Text = "192.168.2.62";
            // 
            // txt_port
            // 
            this.txt_port.Location = new System.Drawing.Point(247, 34);
            this.txt_port.Name = "txt_port";
            this.txt_port.Size = new System.Drawing.Size(67, 21);
            this.txt_port.TabIndex = 8;
            this.txt_port.Text = "22222";
            // 
            // lab_showmsg
            // 
            this.lab_showmsg.AutoSize = true;
            this.lab_showmsg.Font = new System.Drawing.Font("黑体", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lab_showmsg.ForeColor = System.Drawing.Color.Red;
            this.lab_showmsg.Location = new System.Drawing.Point(363, 75);
            this.lab_showmsg.Name = "lab_showmsg";
            this.lab_showmsg.Size = new System.Drawing.Size(72, 19);
            this.lab_showmsg.TabIndex = 9;
            this.lab_showmsg.Text = "未启动";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(42, 350);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 12);
            this.label4.TabIndex = 10;
            this.label4.Text = "发送消息：";
            // 
            // txt_sendmsg
            // 
            this.txt_sendmsg.Location = new System.Drawing.Point(113, 347);
            this.txt_sendmsg.Multiline = true;
            this.txt_sendmsg.Name = "txt_sendmsg";
            this.txt_sendmsg.Size = new System.Drawing.Size(448, 89);
            this.txt_sendmsg.TabIndex = 11;
            // 
            // btn_sendmsg
            // 
            this.btn_sendmsg.Location = new System.Drawing.Point(247, 455);
            this.btn_sendmsg.Name = "btn_sendmsg";
            this.btn_sendmsg.Size = new System.Drawing.Size(75, 23);
            this.btn_sendmsg.TabIndex = 12;
            this.btn_sendmsg.Text = "发送消息";
            this.btn_sendmsg.UseVisualStyleBackColor = true;
            this.btn_sendmsg.Click += new System.EventHandler(this.btn_sendmsg_Click);
            // 
            // SocketReceived
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(601, 662);
            this.Controls.Add(this.btn_sendmsg);
            this.Controls.Add(this.txt_sendmsg);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lab_showmsg);
            this.Controls.Add(this.txt_port);
            this.Controls.Add(this.txt_ipaddress);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btn_close);
            this.Controls.Add(this.btn_start);
            this.Controls.Add(this.txt_received);
            this.Controls.Add(this.txt_lives);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "SocketReceived";
            this.Text = "SocketReceived";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SocketReceived_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_lives;
        private System.Windows.Forms.TextBox txt_received;
        private System.Windows.Forms.Button btn_start;
        private System.Windows.Forms.Button btn_close;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_ipaddress;
        private System.Windows.Forms.TextBox txt_port;
        private System.Windows.Forms.Label lab_showmsg;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_sendmsg;
        private System.Windows.Forms.Button btn_sendmsg;
    }
}