using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Path = System.IO.Path;
using System.Windows.Shapes;
using Wx.Qunkong360.Wpf.Implementation;
using Xzy.EmbeddedApp.Utils;
using System.IO;

namespace Wx.Qunkong360.Wpf.Views
{
    /// <summary>
    /// SimulatorView.xaml 的交互逻辑
    /// </summary>
    public partial class SimulatorView 
    {
        private ResourceManager resourceManager;
        private CultureInfo cultureInfo;
        bool _bootable = true;
        private int _runningGroupIndex ;
        private IList<Button> _groupButtons = new List<Button>();
        public SimulatorView(int runningGroupIndex)
        {
            InitializeComponent();
           _runningGroupIndex += runningGroupIndex;
            resourceManager = new ResourceManager("Wx.Qunkong360.Wpf.Languages.Res", typeof(AppOptView).Assembly);
            if (ConfigVals.Lang == 1)
            {
                cultureInfo = CultureInfo.CreateSpecificCulture("zh-cn");
            }
            else if (ConfigVals.Lang == 2)
            {
                cultureInfo = CultureInfo.CreateSpecificCulture("en-us");
            }

            Title = resourceManager.GetString("WhatsApp_Operation", cultureInfo);

            GetFileName();
        }
        public void GetFileName()
        {
            string apkfile = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\AppDatas\\";
            if (!Directory.Exists(apkfile))
            {
                Directory.CreateDirectory(apkfile);
            }

            var files = Directory.GetFiles(apkfile, "*.apk");
            int decId = 0;
            int Heightapp = 0;
            int Len2 = 0;
            double WidthappLen = 50;
            foreach (var fullPath in files)
            {
                string filename = System.IO.Path.GetFileName(fullPath);//文件名

                Button btn = new Button
                {
                    Name = "Button"+ decId++,
                    Content = filename,//按钮标题
                    Height = 40  ,//按钮高度
                    Width = (this.Width -100 ) / 4,//按钮宽度
                    HorizontalAlignment = HorizontalAlignment.Left,
                    Margin = new Thickness(WidthappLen + Len2++ * 10 ,Heightapp, 0, 0),//在界面上按钮的位置
                    VerticalAlignment = VerticalAlignment.Top,
                    Visibility = Visibility.Visible
                };
                WidthappLen += btn.Width;//下一个按钮递增的距离
                if (decId % 4 ==0) {//每行4个按钮
                    Heightapp += 100;//每行按钮之间的高度
                    WidthappLen = 50;//每行按钮第一个按钮离左边窗口的距离
                    Len2 = 0;//下一个按钮递增的距离
                }

                btn.Click += new RoutedEventHandler(btnInstall_Click);
                applist.Children.Add(btn);
            }

        }
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

        private void btnLaunchWhatsApp_Click(object sender, RoutedEventArgs e)
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

     


        private void btnInstall_Click(object sender, RoutedEventArgs e)
        {
            string apkfile = $"{Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)}\\AppDatas\\"+ ((Button)sender).Content;

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
    }
}
