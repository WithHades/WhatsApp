using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using Wx.Qunkong360.Wpf.Implementation;
using Xzy.EmbeddedApp.Model;
using Xzy.EmbeddedApp.Utils;
using Panel = System.Windows.Forms.Panel;
using Point = System.Drawing.Point;
using Path = System.IO.Path;
using Wx.Qunkong360.Wpf.Utils;
using Xzy.EmbeddedApp.WinForm.Socket;
using System.Windows.Media;
using Wx.Qunkong360.Wpf.Views;
using Wx.Qunkong360.Wpf.ViewModels;
using Cj.EmbeddedAPP.BLL;

namespace Wx.Qunkong360.Wpf
{
    /// <summary>
    /// MainView.xaml 的交互逻辑
    /// </summary>
    public partial class MainView
    {
        public static ResourceManager resourceManager;
        public static CultureInfo cultureInfo;

        bool _bootable = true;
        private int _runningGroupIndex = -1;
        private IList<Button> _groupButtons = new List<Button>();
        Dictionary<int, Wx.Qunkong360.Wpf.Abstract.VmModel> di = null;

        public AppOptViewModel AppOptViewModel = new AppOptViewModel();
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
                    btn.Content = resourceManager.GetString("Launch", cultureInfo);
                    btn.IsEnabled = true && _bootable;
                }
            }
            else
            {
                var runningGroupButton = _groupButtons.FirstOrDefault(btn => btn.Tag.ToString() == _runningGroupIndex.ToString());
                if (runningGroupButton != null)
                {
                    runningGroupButton.Content = resourceManager.GetString("Close", cultureInfo);
                }

                var notRunningGroupButtons = _groupButtons.Where(btn => btn.Tag.ToString() != _runningGroupIndex.ToString());

                foreach (var btn in notRunningGroupButtons)
                {
                    btn.IsEnabled = false;
                }
            }
        }

        public MainView()
        {
            InitializeComponent();

            resourceManager = new ResourceManager("Wx.Qunkong360.Wpf.Languages.Res", typeof(MainView).Assembly);

            if (ConfigVals.Lang == 1)
            {
                cultureInfo = CultureInfo.CreateSpecificCulture("zh-cn");
            }
            else if (ConfigVals.Lang == 2)
            {
                cultureInfo = CultureInfo.CreateSpecificCulture("en-us");
            }
        }

        private void MetroWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            Type type = MethodBase.GetCurrentMethod().DeclaringType;
            string nspace = type.Namespace;

            string resourceName = nspace + ".Images.vmbackground.png";
            Stream stream = assembly.GetManifestResourceStream(resourceName);

            container.BackgroundImage = System.Drawing.Image.FromStream(stream);
            container.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;

            btnLaunchWhatsApp.Content = resourceManager.GetString("Launch_WhatsApp", cultureInfo);
            btnCloseWhatsApp.Content = resourceManager.GetString("Close_WhatsApp", cultureInfo);
            //btnSettings.Content = resourceManager.GetString("Settings", cultureInfo);
            btnInstallApk.Content = resourceManager.GetString("Install_APK", cultureInfo);
            btnOperation.Content = resourceManager.GetString("WhatsApp_Operation", cultureInfo);
            btnOperation.Content = resourceManager.GetString("WhatsApp_Operation", cultureInfo);
            btnCloseAll.Content = resourceManager.GetString("Turn_Off_All_Phones", cultureInfo);
            Title = resourceManager.GetString("Product_Name", cultureInfo);

            ProcessUtils.LDPath = ConfigManager.Instance.Config?.LDPath;

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

        private void InitVmGroups()
        {
            for (int row = 0; row < VmManager.Instance.Row; row++)
            {
                Grid panel = new Grid();

                panel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(120, GridUnitType.Pixel), });
                panel.ColumnDefinitions.Add(new ColumnDefinition() { Width = new GridLength(0, GridUnitType.Auto), });

                int endIndex = VmManager.Instance.VmIndexArray[row, VmManager.Instance.Column - 1];
                int endNumber = endIndex == -1 ? VmManager.Instance.MaxVmNumber : endIndex + 1;

                TextBlock label = new TextBlock()
                {
                    VerticalAlignment = VerticalAlignment.Center,
                    Text = string.Format(resourceManager.GetString("Group", cultureInfo), row + 1, $"{VmManager.Instance.VmIndexArray[row, 0] + 1}-{ endNumber}"),
                    Foreground = new SolidColorBrush(Colors.White),
                };

                Button btn = new Button()
                {
                    Content = resourceManager.GetString("Launch", cultureInfo),
                    IsEnabled = _bootable,
                    Tag = row,
                };

                btn.Click += Btn_Click;

                _groupButtons.Add(btn);

                panel.Children.Add(label);
                panel.Children.Add(btn);

                Grid.SetColumn(label, 0);
                Grid.SetColumn(btn, 1);

                listGroups.Items.Add(panel);
            }
        }

        private async void Btn_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;

            try
            {
                if (btn.Content.ToString() == resourceManager.GetString("Launch", cultureInfo))
                {
                    int groupIndex = int.Parse(btn.Tag.ToString());

                    RunningGroupIndex = groupIndex;

                    double tempRow = (double)VmManager.Instance.Column / (double)ConfigVals.RowNums;

                    int _row = (int)Math.Ceiling(tempRow);

                    int[,] insideGroupIndexArray = new int[_row, ConfigVals.RowNums];

                    container.Controls.Clear();

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

                                container.Controls.Add(vmContainer);

                                string currtime = DateTime.Now.ToString("yyyyMMddHHmmssff");
                                string currindex = vmIndex.ToString().PadLeft(4, '0');

                                ProcessUtils.Init(vmIndex, new Simulator()
                                {
                                    Cpu = 1,
                                    Memory = 1024,
                                    Width = 320,
                                    Height = 480,
                                    Dpi = 120,
                                    //Imei = ip + currindex
                                    //Imei = "auto",
                                    Androidid = ip + currindex

                                });

                                    ProcessUtils.Run(vmIndex);
                                    //加入到对应关系集合中
                                    VmManager.Instance.AddVmModel(vmIndex, new Abstract.VmModel() { Imei = ip + currindex, Index = vmIndex ,AndroidId=ip});
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
                                    if (DateTime.Now.Subtract(now1).TotalSeconds > 7)
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
                        }
                    }

                    //string installNewestAppLog = await AppUpgradeHelper.Instance.InstallNewestApp();
                    //LogUtils.Information(installNewestAppLog);
                }
                else
                {
                    btnCloseAll_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                LogUtils.Error($"{ex}");

                MessageBox.Show(ex.Message);
            }
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
                if (ipArray != null && ipArray.Length > 0)
                {
                    for (int i = 0; i < ipArray.Length; i++)
                    {
                        if (ipArray[i].AddressFamily == AddressFamily.InterNetwork)
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

        private async void btnLaunchWhatsApp_Click(object sender, RoutedEventArgs e)
        {
            if (_runningGroupIndex == -1)
            {
                MessageBox.Show(resourceManager.GetString("Error_Please_Launch_A_Vm", cultureInfo));
                return;
            }

            string packagename = "com.whatsapp";
            List<Phonenum> listPhon = new PhonenumBLL().SelectPhoneNumber();

            for (int i = 0; i < VmManager.Instance.Column; i++)
            {
                int vmIndex = VmManager.Instance.VmIndexArray[_runningGroupIndex, i];

                //获取imei、androidid值
                di = new System.Collections.Generic.Dictionary<int, Wx.Qunkong360.Wpf.Abstract.VmModel>(VmManager.Instance._vmModels);

                if (vmIndex != -1)
                {
                    ProcessUtils.AdbOpenApps(vmIndex, packagename);
                }               
                #region 获取登录信息
                List<Simulators> list = SimulatorsBLL.GetSimulatorsList();
                int listcount = list.Count;
                if (listPhon.Count!=0 ) { 
                    AppOptViewModel.simulators.Add(new Simulators()
                    {
                        id = i + listcount + 1,
                        phonenum = listPhon[i].PhoneNum.Substring(1),
                        imei = di[i].Imei,
                        androidid = di[i].AndroidId,
                        created = listPhon[i].Created
                    });
                }
                #endregion
            }

            string installNewestAppLog = await AppUpgradeHelper.Instance.InstallNewestApp();
            LogUtils.Information(installNewestAppLog);
        }

        private void btnCloseWhatsApp_Click(object sender, RoutedEventArgs e)
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

        /// <summary>
        /// 设置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettings_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnInstallApk_Click(object sender, RoutedEventArgs e)
        {
            int GroupIndex = _runningGroupIndex;
            SimulatorView objSimulatorView = new SimulatorView(GroupIndex);
            objSimulatorView.ShowDialog();

            //string apkfile = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}/AppDatas/WhatsApp.apk";

            //if (_runningGroupIndex == -1)
            //{
            //    MessageBox.Show(resourceManager.GetString("Error_Please_Launch_A_Vm", cultureInfo));
            //    return;
            //}

            //for (int i = 0; i < VmManager.Instance.Column; i++)
            //{
            //    int vmIndex = VmManager.Instance.VmIndexArray[_runningGroupIndex, i];

            //    if (vmIndex != -1)
            //    {
            //        int id = ProcessUtils.AdbInstallApp(vmIndex, apkfile);
            //        Thread.Sleep(200);
            //    }
            //}

        }

        private void btnOperation_Click(object sender, RoutedEventArgs e)
           {
            Simulator objSimlator = new Simulator()
            {
                //Androidid= ProcessUtils.Init.,
                //Imei=
            };
            AppOptView appOptView = new AppOptView(AppOptViewModel);
            appOptView.ShowDialog();
            }

        private void btnCloseAll_Click(object sender, RoutedEventArgs e)
        {
            RunningGroupIndex = -1;

            ProcessUtils.QuitAll();
            container.Controls.Clear();
        }

        private void MetroWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ModemSocketClient.Stop();
            SocketServer.StopServer();
            ProcessUtils.QuitAll();
            Application.Current.Shutdown(0);
        }
    }
}
