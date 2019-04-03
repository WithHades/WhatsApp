using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SocketTestApp.WinForm
{
    public partial class Form1 : Form
    {
        Socket clientSocket = null;
        private static byte[] result = new byte[1024];
        MemoryStream memStream = null;
        string returnMsg = string.Empty;

        public Form1()
        {
            InitializeComponent();
            //启动socket连接
            //startClient();
        }

        /// <summary>
        /// 启动客户端
        /// </summary>
        public void startClient()
        {
            IPAddress ip = IPAddress.Parse(txt_ipaddress.Text);
            clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            try
            {
                clientSocket.Connect(new IPEndPoint(ip,Int32.Parse(txt_point.Text))); //配置服务器IP与端口   
                txt_logs.AppendText("连接服务器成功\r\n\r\n");
                lab_listenstatus.Text = "正在监听";
                Thread th = new Thread(ReceiveMsg2);
                th.IsBackground = true;
                th.Start();
            }
            catch(Exception ex)
            {
                txt_logs.AppendText("连接服务器失败\r\n\r\n");
                txt_logs.AppendText(ex.Message.ToString()+"\r\n\r\n");
                return;
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        /// <param name="msg"></param>
        void ShowMsg(string msg)
        {
            txt_logs.AppendText(msg+"\r\n\r\n");
        }

        /// <summary>
        /// 接收消息方法2
        /// </summary>
        public void ReceiveMsg2()
        {
            while (true)
            {
                try
                {
                    //接收数据
                    byte[] buffer = new byte[2048];
                    int recCount = 0;
                    //接收返回的字节流
                    recCount = clientSocket.Receive(buffer);
                    if (recCount > 0)
                    {
                        returnMsg = Encoding.ASCII.GetString(buffer, 0, recCount);
                        //returnMsg = Encoding.UTF8.GetString(memStream.GetBuffer(), 0, memStream.GetBuffer().Length);
                    }

                    ShowMsg(returnMsg + "\r\n");
                }
                catch (Exception ex)
                {
                    ShowMsg("error：" + ex.Message + "\r\n\r\n");
                    break;
                }
            }
        }

        /// <summary>
        /// 接收消息
        /// </summary>
        public void ReceiveMsg()
        {            
            while (true)
            {
                try
                {
                    //接收数据
                    byte[] buffer = new byte[2048];
                    int recCount = 0;
                    memStream = new MemoryStream();
                    //接收返回的字节流
                    /*while ((recCount = clientSocket.Receive(buffer)) > 0)
                    {
                        memStream.Write(buffer, 0, recCount);
                    }*/
                    recCount = clientSocket.Receive(buffer);
                    if(recCount>0)
                    {
                        returnMsg = Encoding.UTF8.GetString(buffer, 0, recCount);
                        //returnMsg = Encoding.UTF8.GetString(memStream.GetBuffer(), 0, memStream.GetBuffer().Length);
                    }

                    ShowMsg(returnMsg+"\r\n");
                    MsgJson msg = JsonConvert.DeserializeObject<MsgJson>(returnMsg);
                    
                    ShowMsg(string.Format("tasknum:{0};action:{1};context:{2} \r\n",msg.tasknum,msg.action,msg.context));
                }
                catch (Exception ex)
                {
                    ShowMsg("error："+ex.Message+ "\r\n\r\n");
                    break;
                }
            }
        }
        
        /// <summary>
        /// 发送
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_send_Click(object sender, EventArgs e)
        {
            //客户端给服务器发消息
            if (clientSocket != null)
            {
                try
                {
                    var msgjson = new MsgJson
                    {
                        action = "0",
                        tasknum = "0",
                        context = "192168002132001"
                    };
                    var strJson = JsonConvert.SerializeObject(msgjson);

                    byte[] buffer = Encoding.UTF8.GetBytes(strJson);
                    clientSocket.Send(buffer);
                }
                catch (Exception ex)
                {
                    ShowMsg("error：" + ex.Message + "\r\n\r\n");
                }
            }
        }

        /// <summary>
        /// 启动连接
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_start_Click(object sender, EventArgs e)
        {
            startClient();
        }

        private void btn_closecontent_Click(object sender, EventArgs e)
        {
            if(clientSocket!=null)
            {
                clientSocket.Close();
                lab_listenstatus.Text = "未建立连接";
            }
        }

        private void btn_reflsh_Click(object sender, EventArgs e)
        {
            SocketReceived socketReceived = new SocketReceived();
            socketReceived.Show();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            clientSocket?.Close();
        }
    }
}
