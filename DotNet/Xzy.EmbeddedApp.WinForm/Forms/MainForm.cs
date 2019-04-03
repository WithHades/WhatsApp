using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Resources;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using Xzy.EmbeddedApp.Model;
using Xzy.EmbeddedApp.Utils;
using Xzy.EmbeddedApp.WinForm.Implementation;
using Xzy.EmbeddedApp.WinForm.Socket;
using Xzy.EmbeddedApp.WinForm.Socket.Model;

namespace Xzy.EmbeddedApp.WinForm
{
    public partial class MainForm : Form
    {
        private bool _bootable = true;
        private int _runningGroupIndex = -1;
        private IList<Button> _groupButtons = new List<Button>();
        //sprivate readonly Simulator _simulator;
        ResourceManager resourceManager;
        CultureInfo cultureInfo;

        public int RunningGroupIndex
        {
            get { return _runningGroupIndex; }
            private set
            {
                _runningGroupIndex = value;

                VmManager.Instance.RunningGroupIndex = value;

                SyncGroupStatus();
            }
        }

        private void SyncGroupStatus()
        {
            if (_runningGroupIndex == -1)
            {
                foreach (var btn in _groupButtons)
                {
                    btn.Text = resourceManager.GetString("Launch", cultureInfo);
                    btn.Enabled = true && _bootable;
                }
            }
            else
            {
                var runningGroupButton = _groupButtons.FirstOrDefault(btn => btn.Tag.ToString() == _runningGroupIndex.ToString());
                if (runningGroupButton!=null)
                {
                    runningGroupButton.Text = resourceManager.GetString("Close", cultureInfo);
                }

                var notRunningGroupButtons = _groupButtons.Where(btn => btn.Tag.ToString() != _runningGroupIndex.ToString());

                foreach (var btn in notRunningGroupButtons)
                {
                    btn.Enabled = false;
                }
            }
        }

        public List<int> processIdList = new List<int>();
        //当前启动的设备集合
        public static List<int> deviceIdList = new List<int>();
        public MainForm()
        {
            resourceManager = new ResourceManager("Xzy.EmbeddedApp.WinForm.Languages.Res", typeof(MainForm).Assembly);
            if (ConfigVals.Lang == 1)
            {
                cultureInfo = CultureInfo.CreateSpecificCulture("zh-cn");
            }
            else if (ConfigVals.Lang == 2)
            {
                cultureInfo = CultureInfo.CreateSpecificCulture("en-us");
            }

            InitializeComponent();
        }

        /// <summary>
        /// 初始化启动设备
        /// </summary>
        private void InitVmGroups()
        {
            

            for (int row = 0; row < VmManager.Instance.Row; row++)
            {
                TableLayoutPanel panel = new TableLayoutPanel()
                {
                    Location = new Point()
                    {
                        X = 5,
                        Y = row == 0 ? 5 : row * (5 + 30)
                    },
                    RowCount = 1,
                    ColumnCount = 2,
                    Height = 30,
                    Width = 250
                };

                panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 100F));
                panel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(SizeType.Percent, 100F));
                panel.RowStyles.Add(new System.Windows.Forms.RowStyle(SizeType.Percent, 100F));

                int endIndex = VmManager.Instance.VmIndexArray[row, VmManager.Instance.Column - 1];
                int endNumber = endIndex == -1 ? VmManager.Instance.MaxVmNumber : endIndex + 1;

                Label label = new Label()
                {
                    Text = string.Format(resourceManager.GetString("Group", cultureInfo), row + 1, $"{VmManager.Instance.VmIndexArray[row, 0] + 1}-{ endNumber}"),
                    TextAlign = ContentAlignment.BottomLeft,
                };

                Button btn = new Button()
                {
                    Text = resourceManager.GetString("Launch",cultureInfo),
                    Enabled = _bootable,
                    Tag = row,
                };

                btn.Click += ButtonOpenDevice_Click;

                _groupButtons.Add(btn);

                panel.Controls.Add(label, 0, 0);
                panel.Controls.Add(btn, 1, 0);

                panel_rightbox.Controls.Add(panel);
            }
        }

        private void ButtonOpenDevice_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn.Text == resourceManager.GetString("Launch", cultureInfo))
            {
                int groupIndex = int.Parse(btn.Tag.ToString());

                RunningGroupIndex = groupIndex;

                double tempRow = (double)VmManager.Instance.Column / (double)ConfigVals.RowNums;

                int _row = (int)Math.Ceiling(tempRow);

                int[,] insideGroupIndexArray = new int[_row, ConfigVals.RowNums];

                panel2.Controls.Clear();

                string ip = GetLocalIP();

                for (int i = 0; i < _row; i++)
                {
                    for (int j = 0; j < ConfigVals.RowNums; j++)
                    {
                        int insideGroupIndex = i * ConfigVals.RowNums + j;

                        if (insideGroupIndex > VmManager.Instance.Column - 1)
                        {
                            continue;
                        }

                        int vmIndex = VmManager.Instance.VmIndexArray[groupIndex, insideGroupIndex];

                        insideGroupIndexArray[i, j] = vmIndex;

                        if (vmIndex != -1)
                        {
                            try
                            {
                                Panel vmContainer = new Panel()
                                {
                                    Width = 320,
                                    Height = 480,
                                    Location = new Point()
                                    {
                                        X = j * (320 + 3) + 3,
                                        Y = i * (480 + 3) + 3,
                                    }
                                };
                                

                                panel2.Controls.Add(vmContainer);

                                string currtime = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                string currindex = vmIndex.ToString().PadLeft(3, '0');

                                ProcessUtils.Init(vmIndex, new Simulator()
                                {
                                    Cpu = 1,
                                    Memory = 1024,
                                    Width = 320,
                                    Height = 480,
                                    Dpi = 240,
                                    Imei = ip + currindex
                                    //Imei = "auto"

                                });

                                ProcessUtils.Run(vmIndex);
                                //加入到对应关系集合中
                                VmManager.Instance.AddVmModel(vmIndex, new Abstract.VmModel() { Imei = ip + currindex,Index = vmIndex });

                                var latestVmProcess = Process.GetProcessesByName("dnplayer").OrderByDescending(p => p.StartTime).FirstOrDefault();

                                if (latestVmProcess == null)
                                {
                                    string error = resourceManager.GetString("Error_No_Vm_Process", cultureInfo);
                                    LogUtils.Error($"{error} vmIndex:{vmIndex}");
                                    throw new Exception(error);
                                }

                                DateTime now1 = DateTime.Now;

                                while (latestVmProcess.MainWindowHandle == IntPtr.Zero)
                                {
                                    if (DateTime.Now.Subtract(now1).TotalSeconds > 5)
                                    {
                                        string error = resourceManager.GetString("Error_Main_Window_Handle_Timeout", cultureInfo);
                                        LogUtils.Error(error);
                                        //MessageBox.Show("轮询模拟器的主窗口句柄超时！");
                                        break;
                                    }
                                }

                                int setParentResult = WinAPIs.SetParent(latestVmProcess.MainWindowHandle, vmContainer.Handle);

                                if (setParentResult != 65552)
                                {
                                    LogUtils.Error($"SetParent result:{setParentResult}, vmIndex:{vmIndex}");
                                }

                                int moveWindowResult = WinAPIs.MoveWindow(latestVmProcess.MainWindowHandle, 0, -35, 320, 515, true);

                                if (moveWindowResult != 1)
                                {
                                    LogUtils.Error($"MoveWindow result:{moveWindowResult}, vmIndex:{vmIndex}");
                                }

                                //Log.Logger.Information($"VmIndex：{vmIndex}, SetParent Result：{setParentResult}, MoveWindow Result：{moveWindowResult}");

                                //btn.Text = "关闭";
                            }
                            catch (Exception ex)
                            {
                                LogUtils.Error($"{ex}");
                                MessageBox.Show(ex.Message);
                                continue;
                            }
                        }
                    }
                }
            }
            else
            {
                ProcessUtils.QuitAll();
                //btn.Text = "开启";
                RunningGroupIndex = -1;
            }
        }


        private void ButtonOpenDevice_Click2(object sender, EventArgs e)
        {
            Button b1 = (Button)sender;
            int x = 15;
            int y = 5;
            b1.Text = "执行中……";
            string[] butnames = b1.Name.Split('_');
            int currgroups = Int32.Parse(butnames[2]);
            if(deviceIdList==null || deviceIdList.Count==0)
            {
                int start = 0;
                int ends = 0;
                //设置当前启动组
                if (currgroups==1)
                {
                    start = 0;
                    ends = start + ConfigVals.GroupNums-1;
                }
                else
                {
                    start = (currgroups - 1) * ConfigVals.GroupNums;
                    ends = start + ConfigVals.GroupNums-1;
                    if(ends>=ConfigVals.MaxNums)
                    {
                        ends = ConfigVals.MaxNums - 1;
                    }
                }
                for(int i= start; i<=ends;i++)
                {
                    deviceIdList.Add(i);
                }
            }
            if (ConfigVals.IsRunning==0)
            {
                if(ProcessUtils.LDPath=="")
                {
                    MessageBox.Show("模拟器启动路径没有设置");
                }
                //开启设备
                if(deviceIdList!=null && deviceIdList.Count > 0)
                {
                    /*Simulator simulator = new Simulator()
                    {
                        Cpu = 1,
                        Memory = 1024,
                        Width = 320,
                        Height = 480,
                        Dpi = 240,
                        Imei = "auto"
                    };   */

                    int point = 0;
                    /*for (int i = 0; i < deviceIdList.Count; i++)
                    {
                        //ProcessUtils.Init(deviceIdList[i], simulator);
                        int id = ProcessUtils.Run(deviceIdList[i]);
                        IntPtr idp = (IntPtr)id;
                        Process[] process = Process.GetProcessesByName("dnplayer");
                        foreach (var p in process)
                        {
                            if (!processIdList.Contains(p.Id))
                            {
                                //Thread.Sleep(500);
                                processIdList.Add(p.Id);
                                var handle = p.MainWindowHandle;


                                while (p.MainWindowHandle == IntPtr.Zero)
                                {

                                }

                                //AppContainer.SetParent(handle, Handle);
                                AppContainer appcon = new AppContainer();
                                appcon.Size = new Size(320, 480);

                                if(point==0)
                                {
                                    appcon.Location = new Point(11, 5);
                                }
                                else
                                {
                                    appcon.Location = new Point(11+(point*320)+10, 5);
                                }
                                
                                panel2.Controls.Add(appcon);

                               Console.WriteLine($"processId:{p.Id}, mainWindowHane:{p.MainWindowHandle}");

                                InsertMobileFormIntoContainer(p.MainWindowHandle, appcon.Handle,0,0,320,480);
                                //AppContainer.SetWindowLong(new HandleRef(appcon, handle), GWL_STYLE, WS_VISIBLE);
                                //Point mobileP = appcon.PointToScreen(new Point(0, 0));

                                //InsertMobileFormIntoContainer(p.MainWindowHandle, appcon.Handle, mobileP.X, mobileP.Y, 320, 480);

                                //AppContainer.SetWindowLong(new HandleRef(appcon, handle), GWL_STYLE, WS_VISIBLE);
                                point++;

                                break;
                            }
                        }
                        Thread.Sleep(200);
                    }*/

            }
                ConfigVals.IsRunning = 1;
                b1.Text = string.Format("关闭第{0}组", currgroups);
                //ProcessUtils.AdbSortDevices();
            }
            else
            {
                //关闭设备
                if (deviceIdList != null && deviceIdList.Count > 0)
                {
                    ProcessUtils.QuitAll();
                }
                deviceIdList.Clear();
                ConfigVals.IsRunning = 0;
                b1.Text = string.Format("开启第{0}组", currgroups);
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        private const int GWL_STYLE = (-16);
        private const int WS_VISIBLE = 0x10000000;
        [DllImport("user32", EntryPoint = "SetWindowLong", SetLastError = true)]
        private static extern uint SetWindowLong(
       IntPtr hwnd,
       int nIndex,
       uint dwNewLong
       );

        /// <summary>
        /// socket发送测试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string str = txt_testmsg.Text;// ["tasktype: 1","filepath:/ sdcard / qunkong / txt / 201806201426300275.txt","nums: 42"]

            if (!SocketServer.AllConnectionKey.IsNull() && SocketServer.AllConnectionKey.Count() > 0)
            {
                foreach (var item in SocketServer.AllConnectionKey)
                {
                    if (!item.Value.IsNull())
                    {
                        var jsonMsg = new SockMsgJson
                        {
                            action = SocketCase.ServerMsg,
                            tasknum = 0,
                            context = str
                        };
                        
                        var strJson = JsonConvert.SerializeObject(jsonMsg);
                        SocketUtils.SendJson(item.Value, strJson);

                        MessageBox.Show("消息发送成功");
                        /*SocketUtils.Send<SocketMsg>(item.Value, msg);
                        SocketUtils.Send<String>(item.Value, "\r\n");*/

                    }
                }
            }            
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            ProcessUtils.QuitAll();
            System.Environment.Exit(0);
        }

        //启动App
        private void btn_startapps_Click(object sender, EventArgs e)
        {
            if (_runningGroupIndex == -1)
            {
                MessageBox.Show(resourceManager.GetString("Error_Please_Launch_A_Vm", cultureInfo));
                return;
            }

            string packagename = "com.whatsapp";

            for (int i = 0; i < VmManager.Instance.Column; i++)
            {
                int vmIndex = VmManager.Instance.VmIndexArray[_runningGroupIndex, i];

                if (vmIndex != -1)
                {
                    ProcessUtils.AdbOpenApps(vmIndex, packagename);
                }
            }
        }

        //关闭全部app
        private void btn_closeapps_Click(object sender, EventArgs e)
        {
            if (_runningGroupIndex == -1)
            {
                MessageBox.Show(resourceManager.GetString("Error_Please_Launch_A_Vm", cultureInfo));
                return;
            }

            string packagename = "com.whatsapp";

            for (int i = 0; i < VmManager.Instance.Column; i++)
            {
                int vmIndex = VmManager.Instance.VmIndexArray[_runningGroupIndex, i];

                if (vmIndex != -1)
                {
                    ProcessUtils.AdbCloseApps(vmIndex, packagename);
                }
            }
        }

        //安装whatsApp
        private void btn_installapp_Click(object sender, EventArgs e)
        {
            string apkfile = $"{Application.StartupPath}/AppDatas/WhatsApp.apk";

            if (_runningGroupIndex == -1)
            {
                MessageBox.Show(resourceManager.GetString("Error_Please_Launch_A_Vm", cultureInfo));
                return;
            }

            for (int i = 0; i < VmManager.Instance.Column; i++)
            {
                int vmIndex = VmManager.Instance.VmIndexArray[_runningGroupIndex, i];

                if (vmIndex != -1)
                {
                    int id = ProcessUtils.AdbInstallApp(vmIndex, apkfile);
                    Thread.Sleep(200);
                }
            }
        }

        /// <summary>
        /// 打开whatsapp相关操作界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_whatsapp_Click(object sender, EventArgs e)
        {
            AppOptForm opt = new AppOptForm();
            opt.StartPosition = FormStartPosition.CenterParent;
            opt.ShowDialog();
        }

        /// <summary>
        /// 打开子窗体 
        /// </summary>
        /// <param name="objFrm"></param>
        private void OpenFrom(Form objFrm)
        {
            //将当前子窗体设置成非顶级控件
            objFrm.TopLevel = false;
            //设置窗体最大化
            objFrm.WindowState = FormWindowState.Maximized;
            //去掉窗体边框
            objFrm.FormBorderStyle = FormBorderStyle.None;
            //指定当前子窗体显示的容器
            objFrm.Parent = panel2;
            //显示窗体
            objFrm.Show();
        }

        /// <summary>
        /// 关闭子窗体
        /// </summary>
        private void CloseFrom()
        {
            foreach (Control item in panel2.Controls)
            {
                if (item is Form objControl)
                {
                    objControl.Close();
                    panel2.Controls.Remove(item);
                }
            }
        }

        /// <summary>
        /// 系统设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_setconfig_Click(object sender, EventArgs e)
        {
            GetLocalIP();
        }

        private void btn_closephone_Click(object sender, EventArgs e)
        {

            ProcessUtils.QuitAll();

            RunningGroupIndex = -1;

        }

        /// <summary>
        /// 将手机虚拟器嵌入某个window容器
        /// </summary>
        /// <param name="mobileFormHandle">手机虚拟器窗口句柄</param>
        /// <param name="containerHandle">window容器窗口句柄</param>
        /// <param name="containerX">window容器窗口相对于屏幕的横坐标</param>
        /// <param name="containerY">window容器窗口相对于屏幕的纵坐标</param>
        /// <param name="containerWidth">window容器窗口宽度</param>
        /// <param name="containerHeight">window容器窗口高度</param>
        private void InsertMobileFormIntoContainer(IntPtr mobileFormHandle, IntPtr containerHandle, int containerX, int containerY, int containerWidth, int containerHeight)
        {
            //IntPtr targetHandle = WinAPIs.FindWindowEx(mobileFormHandle, IntPtr.Zero, "RenderWindow", null);

            //if (targetHandle != IntPtr.Zero)
            //{
                int setParentResult = WinAPIs.SetParent(mobileFormHandle, containerHandle);

                int moveTargetResult = WinAPIs.MoveWindow(mobileFormHandle, 0, 0, containerWidth, containerHeight, true);
            //    int hideMobileFrameResult = WinAPIs.ShowWindow(mobileFormHandle, 0);
            //}
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            btn_startapps.Text = resourceManager.GetString("Launch_WhatsApp", cultureInfo);
            btn_closeapps.Text = resourceManager.GetString("Close_WhatsApp", cultureInfo);
            btn_setconfig.Text = resourceManager.GetString("Settings", cultureInfo);
            btn_installapp.Text = resourceManager.GetString("Install_APK", cultureInfo);
            btn_whatsapp.Text = resourceManager.GetString("WhatsApp_Operation", cultureInfo);
            btn_closephone.Text = resourceManager.GetString("Turn_Off_All_Phones", cultureInfo);
            Text = resourceManager.GetString("Product_Name", cultureInfo);
            button1.Text = resourceManager.GetString("Send_Socket", cultureInfo);

            ProcessUtils.LDPath = ConfigurationManager.AppSettings["LDPath"];

            if (string.IsNullOrEmpty(ProcessUtils.LDPath))
            {
                _bootable = false;
                MessageBox.Show(resourceManager.GetString("Error_Vm_Path_Not_Set", cultureInfo));
            }

            if (!Directory.Exists(ProcessUtils.LDPath))
            {
                _bootable = false;
                MessageBox.Show(resourceManager.GetString("Error_Vm_Path_Not_Exist", cultureInfo));
            }

            InitVmGroups();
        }

        /// <summary>
        /// 获取本机IP
        /// </summary>
        /// <returns></returns>
        private string GetLocalIP()
        {
            IPAddress localIp = null;

            try
            {
                IPAddress[] ipArray;
                ipArray = Dns.GetHostAddresses(Dns.GetHostName());
                if(ipArray!=null && ipArray.Length>0)
                {
                    for(int i=0; i<ipArray.Length;i++)
                    {
                        if(ipArray[i].AddressFamily == AddressFamily.InterNetwork)
                        {
                            localIp = ipArray[i];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            if (localIp == null)
            {
                localIp = IPAddress.Parse("127.0.0.1");
            }
            string ip = localIp.ToString();
            string IpVals = "";
            if (ip != "")
            {
                string[] ips = ip.Split('.');
                for (int i = 0; i < ips.Length; i++)
                {
                    IpVals += ips[i].PadLeft(3, '0');
                }
            }
            return IpVals;
        }
    }
}
