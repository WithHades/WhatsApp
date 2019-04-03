using System;
using Cj.EmbeddedApp.BLL;
using Xzy.EmbeddedApp.Model;
using System.Windows.Forms;
using Xzy.EmbeddedApp.Utils;
using Xzy.EmbeddedApp.WinForm.Implementation;
using System.Resources;
using System.Globalization;

namespace Xzy.EmbeddedApp.WinForm
{
    public partial class StartUpForm : Form
    {
        ResourceManager resourceManager;
        CultureInfo cultureInfo;

        public StartUpForm()
        {
            this.FormClosing += StartUpForm_FormClosing;
            resourceManager = new ResourceManager("Xzy.EmbeddedApp.WinForm.Languages.Res", typeof(StartUpForm).Assembly);

            InitializeComponent();
            ConfigVals.IsRunning = 0;
            bandInitData();

            //SwitchLanguage();

        }

        private void StartUpForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }

        //启动群控
        private void btn_start_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();
            this.Hide();
            mainform.Show();
        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        //保存配置参数
        private void btn_saveparameter_Click(object sender, EventArgs e)
        {
            int groupCapacity = int.Parse(text_groupnums.Text.Trim());
            int rowCapacity = int.Parse(text_rownums.Text.Trim());

            VmManager.Instance.Initialize(ConfigVals.MaxNums, groupCapacity);

            ConfigsBLL bll = new ConfigsBLL();
            int lang = (rad_langenglish.Checked == true) ? 2 : 1;
            int flag = bll.SaveConfigs(lang, groupCapacity, rowCapacity);
            if (flag > 0)
            {
                ConfigVals.Lang = lang;

                ConfigVals.GroupNums = groupCapacity;
                ConfigVals.RowNums = rowCapacity;
                MessageBox.Show(resourceManager.GetString("Save_Success", cultureInfo));
            }

        }

        /// <summary>
        /// 初始化配置数据
        /// </summary>
        private void bandInitData()
        {
            ConfigsBLL bll = new ConfigsBLL();
            Configs configs = bll.GetAllData();
            if (configs != null)
            {
                text_groupnums.Text = configs.Groupnums.ToString();
                text_rownums.Text = configs.Rownums.ToString();
                if (configs.Lang == 2)
                {
                    rad_langenglish.Checked = true;
                    ConfigVals.Lang = 2;
                }
                else
                {
                    rad_langchina.Checked = true;
                    ConfigVals.Lang = 1;
                }
            }
            else
            {
                text_groupnums.Text = "12";
                text_rownums.Text = "6";
                rad_langchina.Checked = true;
                ConfigVals.Lang = 1;
            }
            //初始化数据
            ConfigVals.GroupNums = Int32.Parse(text_groupnums.Text);
            ConfigVals.RowNums = Int32.Parse(text_rownums.Text);
            VmManager.Instance.Initialize(ConfigVals.MaxNums, ConfigVals.GroupNums);
        }

        private void btn_openmain_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();
            this.Hide();
            mainform.Show();
        }



        private void SwitchLanguage()
        {
            if (rad_langchina.Checked)
            {
                ConfigVals.Lang = 1;
                cultureInfo = CultureInfo.CreateSpecificCulture("zh-cn");
            }
            else if (rad_langenglish.Checked)
            {
                ConfigVals.Lang = 2;
                cultureInfo = CultureInfo.CreateSpecificCulture("en-us");
            }

            lblProductName.Text = resourceManager.GetString("Product_Name", cultureInfo);
            lblLanguage.Text = resourceManager.GetString("System_Language", cultureInfo);
            lblGroupNums.Text = resourceManager.GetString("Group_Capacity", cultureInfo);
            lblRowNums.Text = resourceManager.GetString("Row_Capacity", cultureInfo);
            lblAuthorizationStatus.Text = resourceManager.GetString("Authorization_Status", cultureInfo);
            btn_saveparameter.Text = resourceManager.GetString("Save_Parameters", cultureInfo);
            btn_start.Text = resourceManager.GetString("Launch_Group_Control", cultureInfo);
            btn_close.Text = resourceManager.GetString("Close_Group_Control", cultureInfo);
            rad_langchina.Text = resourceManager.GetString("Chinese", cultureInfo);
            rad_langenglish.Text = resourceManager.GetString("English", cultureInfo);
            lab_msg.Text = string.Format(resourceManager.GetString("Authorized_Number", cultureInfo), ConfigVals.MaxNums);
            Text = resourceManager.GetString("Product_Name", cultureInfo);
        }

        private void rad_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton radioButton = sender as RadioButton;

            if (radioButton != null && radioButton.Checked)
            {
                SwitchLanguage();
            }
        }
    }
}
