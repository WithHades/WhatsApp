using Cj.EmbeddedApp.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using Xzy.EmbeddedApp.Utils;
using Xzy.EmbeddedApp.WinForm.Utils;

namespace Xzy.EmbeddedApp.WinForm.Forms
{
    public partial class AuthForm : Form
    {
        public AuthForm()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 加载完成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AuthForm_Shown(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000 * 3;
        }

        /// <summary>
        /// 定时器事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            AuthLogin();
            timer1.Enabled = false;
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
                StartUpForm startUp = new StartUpForm();
                this.Hide();
                startUp.Show();
            }
            else
            {
                if (authflag == -10)
                {
                    lab_authmsg.Text = "数据库服务启动失败，请先启动数据库服务";
                }
                if(authflag == -11)
                {
                    lab_authmsg.Text = "远程鉴权失败，请联系客户经理";
                }
                btn_retry.Visible = true;
            }

            return flag;
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
            if(!auth)
            {
                flag = -11;
            }
            return flag;
        }

        /// <summary>
        /// 远程鉴权
        /// </summary>
        /// <returns></returns>
        public bool AuthRemoteCode()
        {
            lab_authmsg.Text = "远程鉴权中……";
            bool authflag = false;
            var values = new List<KeyValuePair<string, string>>();

            string url = ConfigurationManager.AppSettings["AuthUrl"]; ;//
            string ukey = ConfigurationManager.AppSettings["UKey"]; ;//
            string macaddrss = SystemInfoUtils.GetMacAddress();

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long t = Convert.ToInt64(ts.TotalSeconds);

            string key = ConfigVals.AccessKey.Substring(8, 20);
            
            if (macaddrss == "" || key=="")
            {
                return authflag;
            }
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] result = Encoding.Default.GetBytes(ukey + macaddrss + t.ToString() + key);
            byte[] bytekey = md5.ComputeHash(result);
            string token = BitConverter.ToString(bytekey).Replace("-", "");           

            /*values.Add(new KeyValuePair<string, string>("ukey", ukey));
            values.Add(new KeyValuePair<string, string>("macadd", macaddrss));
            values.Add(new KeyValuePair<string, string>("time", t.ToString()));
            values.Add(new KeyValuePair<string, string>("token", token));*/

            HttpClientHelp httpClient = new HttpClientHelp();
            var obj = new JObject() { { "ukey", ukey }, { "macadd", macaddrss }, { "time", t.ToString() }, { "token", token.ToLower() } };
            
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
            lab_authmsg.Text = "正在检测数据库服务……";
            bool connFlag = false;
            connFlag = MySqlHelpers.ConnectionTest(MySqlHelpers.ConnectionString);            

            return connFlag;
        }                

        /// <summary>
        /// 连接重试
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_retry_Click(object sender, EventArgs e)
        {
            lab_authmsg.Text = "请稍候……";
            AuthLogin();
        }
    }
}
