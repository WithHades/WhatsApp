using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace SocketTestApp.WinForm
{
    public partial class SocketReceived : Form
    {
        private static byte[] result = new byte[1024];
        static Socket serverSocket;

        static Dictionary<string, Socket> clientConnectionItems = new Dictionary<string, Socket> { };
        public SocketReceived()
        {
            InitializeComponent();

        }

        /// <summary>
        /// 开始监听
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_start_Click(object sender, EventArgs e)
        {

            IPAddress ip = IPAddress.Parse(txt_ipaddress.Text);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(new IPEndPoint(ip, Int32.Parse(txt_port.Text)));  //绑定IP地址：端口  
            serverSocket.Listen(10);
            //通过Clientsoket发送数据  
            Thread myThread = new Thread(ListenClientConnect);
            myThread.Start();
            lab_showmsg.Text = "开始监听……";
        }

        /// <summary>  
        /// 监听客户端连接  
        /// </summary>  
        private void ListenClientConnect()
        {
            Socket connection = null;
            while (true)
            {

                try
                {
                    connection = serverSocket.Accept();
                }
                catch (Exception ex)
                {
                    //提示套接字监听异常     
                    Console.WriteLine(ex.Message);
                    break;
                }
                //获取客户端的IP和端口号  
                IPAddress clientIP = (connection.RemoteEndPoint as IPEndPoint).Address;
                int clientPort = (connection.RemoteEndPoint as IPEndPoint).Port;
                //connection.Send(Encoding.UTF8.GetBytes("连接成功"));



                string remoteEndPoint = connection.RemoteEndPoint.ToString();

                txt_lives.BeginInvoke(new Action(() =>
                {
                    txt_lives.AppendText("成功与" + remoteEndPoint + "客户端建立连接！\t\n");
                }));


                //添加客户端信息
                clientConnectionItems.Add(remoteEndPoint, connection);
                IPEndPoint netpoint = connection.RemoteEndPoint as IPEndPoint;
                //创建一个通信线程
                ParameterizedThreadStart pts = new ParameterizedThreadStart(ReceiveMessage);
                Thread receiveThread = new Thread(pts);
                receiveThread.IsBackground = true;
                receiveThread.Start(connection);
            }
        }

        /// <summary>  
        /// 接收消息  
        /// </summary>  
        /// <param name="clientSocket"></param>  
        private void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    //通过clientSocket接收数据  
                    int receiveNumber = myClientSocket.Receive(result);

                    if (receiveNumber == 0)
                    {
                        continue;
                    }
                    else
                    {
                        writeMsg(string.Format("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.UTF8.GetString(result, 0, receiveNumber)));
                    }
                }
                catch (Exception ex)
                {
                    writeMsg(ex.Message);
                    myClientSocket.Shutdown(SocketShutdown.Both);
                    myClientSocket.Close();
                    break;
                }
            }
        }

        void writeMsg(string text)
        {
            txt_received.BeginInvoke(new Action(() =>
            {
                txt_received.AppendText(text + "\r\n");
            }));
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            serverSocket.Close();
            lab_showmsg.Text = "未监听";
        }

        //发送消息
        private void btn_sendmsg_Click(object sender, EventArgs e)
        {
            if(txt_sendmsg.Text=="")
            {
                MessageBox.Show("发送消息不能为空.");
                return;
            }

            try
            {
                if (clientConnectionItems.Count > 0)
                {
                    foreach (var item in clientConnectionItems)
                    {
                        item.Value.Send(Encoding.UTF8.GetBytes(txt_sendmsg.Text+"\r\n"));
                    }
                }
            }
            catch(Exception ex)
            {
                txt_lives.AppendText($"发送消息异常{ex.Message}");
            }

        }

        private void SocketReceived_FormClosing(object sender, FormClosingEventArgs e)
        {
            serverSocket.Close();
            Application.Exit();
        }
    }



}
