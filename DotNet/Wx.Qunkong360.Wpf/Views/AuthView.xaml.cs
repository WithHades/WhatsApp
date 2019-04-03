using Cj.EmbeddedApp.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Security.Cryptography;
using System.Text;
using System.Timers;
using System.Windows;
using Wx.Qunkong360.Wpf.Utils;
using Xzy.EmbeddedApp.Model;
using Xzy.EmbeddedApp.Utils;
using Xzy.EmbeddedApp.WinForm.Utils;

namespace Wx.Qunkong360.Wpf
{
    /// <summary>
    /// AuthView.xaml 的交互逻辑
    /// </summary>
    public partial class AuthView
    {
        ResourceManager resourceManager;
        CultureInfo cultureInfo;

        Timer timer;

        public AuthView()
        {
            InitializeComponent();

            resourceManager = new ResourceManager("Wx.Qunkong360.Wpf.Languages.Res", typeof(AuthView).Assembly);

            string config = $"{Directory.GetParent(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location))}/config.txt";

            if (!File.Exists(config))
            {
                ConfigModel configModel = new ConfigModel()
                {
                    LDPath = "",
                    NoxPath = "",
                    UKey = "",
                };

                string json = JsonConvert.SerializeObject(configModel, Formatting.Indented);

                File.WriteAllText(config, json, Encoding.UTF8);

                MessageBox.Show(resourceManager.GetString("Vm_Path_Not_Set", cultureInfo));
                Close();
            }
            else
            {
                string json = File.ReadAllText(config);
                ConfigManager.Instance.Config = JsonConvert.DeserializeObject<ConfigModel>(json);
            }

            Title = resourceManager.GetString("Authentication", cultureInfo);
            lblAuthTips.Text = resourceManager.GetString("Authenticating", cultureInfo);
            btnRetry.Content = resourceManager.GetString("Retry", cultureInfo);

            timer = new Timer(3000);
            timer.Elapsed += Timer_Elapsed;
            timer.Start();
        }

        private void Timer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Dispatcher.BeginInvoke(new Action(() =>
            {
                AuthLogin();
                timer.Stop();
                timer.Enabled = false;

            }));
        }

        /// <summary>
        /// 启动后验证用户
        /// </summary>
        /// <returns></returns>
        public int AuthUser()
        {
            int flag = 100;
            //检查mysql端口
            bool ispoint = CheckPortisAvailable();
            if (!ispoint)
            {
                return -10;
            }
            //远程鉴权
            bool auth = AuthRemoteCode();
            if (!auth)
            {
                flag = -11;
            }
            return flag;
        }


        /// <summary>
        /// 集中验证
        /// </summary>
        /// <returns></returns>
        public bool AuthLogin()
        {
            bool flag = false;

            int authflag = AuthUser();
            //authflag = 100;
            if (authflag == 100)
            {
                timer.Stop();
                StartUpView startUpView = new StartUpView();
                startUpView.Show();

                Close();
            }
            else
            {
                if (authflag == -10)
                {
                    lblAuthTips.Text = resourceManager.GetString("Start_Database_Failure", cultureInfo);
                }
                if (authflag == -11)
                {
                    lblAuthTips.Text = resourceManager.GetString("Authentication_Failure", cultureInfo);
                }
                btnRetry.Visibility = System.Windows.Visibility.Visible;
            }

            return flag;
        }



        /// <summary>
        /// 远程鉴权
        /// </summary>
        /// <returns></returns>
        public bool AuthRemoteCode()
        {
            lblAuthTips.Text = resourceManager.GetString("Authenticating", cultureInfo);
            bool authflag = false;
            var values = new List<KeyValuePair<string, string>>();

            string url = ConfigurationManager.AppSettings["AuthUrl"];//
            string ukey = ConfigManager.Instance.Config.UKey;//
            string macaddrss = SystemInfoUtils.GetMacAddress();
            string type = "whatsapp";

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long t = Convert.ToInt64(ts.TotalSeconds);

            string key = ConfigVals.AccessKey.Substring(8, 20);

            if (macaddrss == "" || key == "")
            {
                return authflag;
            }
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = Encoding.Default.GetBytes(ukey + macaddrss + type + t.ToString() + key);
            byte[] bytekey = md5.ComputeHash(result);
            string token = BitConverter.ToString(bytekey).Replace("-", "");

            /*values.Add(new KeyValuePair<string, string>("ukey", ukey));
            values.Add(new KeyValuePair<string, string>("macadd", macaddrss));
            values.Add(new KeyValuePair<string, string>("time", t.ToString()));
            values.Add(new KeyValuePair<string, string>("token", token));*/

            HttpClientHelp httpClient = new HttpClientHelp();
            var obj = new JObject() { { "ukey", ukey }, { "macadd", macaddrss }, { "type", type }, { "time", t.ToString() }, { "token", token.ToLower() } };

            authflag = httpClient.PostFunction(url, obj.ToString(Formatting.None));

            return authflag;
        }

        /// <summary>
        /// 检测端口
        /// </summary>
        /// 
        /// <returns></returns>
        public bool CheckPortisAvailable()
        {
            lblAuthTips.Text = resourceManager.GetString("Check_Database_Service", cultureInfo);
            bool connFlag = false;
            connFlag = MySqlHelpers.ConnectionTest(MySqlHelpers.ConnectionString);

            if(connFlag)
            {
                ConfigsBLL bll = new ConfigsBLL();
                Configs configs = bll.GetAllData();
                if (configs != null)
                {
                    if (configs.Lang == 1)
                    {
                        cultureInfo = CultureInfo.CreateSpecificCulture("zh-cn");
                    }
                    else if (configs.Lang == 2)
                    {
                        cultureInfo = CultureInfo.CreateSpecificCulture("en-us");
                    }
                }
            }

            Title = resourceManager.GetString("Authentication", cultureInfo);
            lblAuthTips.Text = resourceManager.GetString("Authenticating", cultureInfo);
            btnRetry.Content = resourceManager.GetString("Retry", cultureInfo);

            return connFlag;
        }



        private void btnRetry_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            lblAuthTips.Text = resourceManager.GetString("Please_Wait", cultureInfo);
            AuthLogin();

        }
    }
}
