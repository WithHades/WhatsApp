using Cj.EmbeddedApp.BLL;
using System;
using System.Globalization;
using System.Resources;
using Wx.Qunkong360.Wpf.Implementation;
using Xzy.EmbeddedApp.Model;
using Xzy.EmbeddedApp.Utils;
using System.Linq;
using System.Windows.Controls;
using System.Windows;

namespace Wx.Qunkong360.Wpf
{
    /// <summary>
    /// StartUpView.xaml 的交互逻辑
    /// </summary>
    public partial class StartUpView
    {
        ResourceManager resourceManager;
        CultureInfo cultureInfo;

        public StartUpView()
        {
            InitializeComponent();

            bandInitData();
        }

        /// <summary>
        /// 初始化配置数据
        /// </summary>
        private void bandInitData()
        {
            resourceManager = new ResourceManager("Wx.Qunkong360.Wpf.Languages.Res", typeof(StartUpView).Assembly);

            ConfigsBLL bll = new ConfigsBLL();
            Configs configs = bll.GetAllData();
            if (configs != null)
            {
                var groupCapacityItem = cbGroupCapacity.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == configs.Groupnums.ToString());

                int groupCapacityIndex = cbGroupCapacity.Items.IndexOf(groupCapacityItem);

                cbGroupCapacity.SelectedIndex = groupCapacityIndex;



                var rowCapacityItem = cbRowCapacity.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == configs.Rownums.ToString());

                int rowCapacityIndex = cbRowCapacity.Items.IndexOf(rowCapacityItem);

                cbRowCapacity.SelectedIndex = rowCapacityIndex;


                if (configs.Lang == 2)
                {
                    rbEnglish.IsChecked = true;
                    ConfigVals.Lang = 2;
                }
                else
                {
                    rbChinese.IsChecked = true;
                    ConfigVals.Lang = 1;
                }
            }
            else
            {
                cbGroupCapacity.SelectedIndex = 0;
                cbRowCapacity.SelectedIndex = 0;
                rbChinese.IsChecked = true;
                ConfigVals.Lang = 1;
            }

            //初始化数据
            ConfigVals.GroupNums = Int32.Parse(cbGroupCapacity.SelectionBoxItem.ToString());
            ConfigVals.RowNums = Int32.Parse(cbRowCapacity.SelectionBoxItem.ToString());
            VmManager.Instance.Initialize(ConfigVals.MaxNums, ConfigVals.GroupNums);
        }

        private void SwitchLanguage()
        {
            if (rbChinese.IsChecked.HasValue && rbChinese.IsChecked.Value)
            {
                ConfigVals.Lang = 1;
                cultureInfo = CultureInfo.CreateSpecificCulture("zh-cn");
            }
            else if (rbEnglish.IsChecked.HasValue && rbEnglish.IsChecked.Value)
            {
                ConfigVals.Lang = 2;
                cultureInfo = CultureInfo.CreateSpecificCulture("en-us");
            }

            tbProductName.Text = resourceManager.GetString("Product_Name", cultureInfo);
            tbLanguage.Text = resourceManager.GetString("System_Language", cultureInfo);

            lblGroupCapacity.Text = resourceManager.GetString("Group_Capacity", cultureInfo);

            lblRowCapacity.Text = resourceManager.GetString("Row_Capacity", cultureInfo);
            lblAuthorization.Text = resourceManager.GetString("Authorization_Status", cultureInfo);
            btnSavePaameters.Content = resourceManager.GetString("Save_Parameters", cultureInfo);
            btnLaunchGroupControl.Content = resourceManager.GetString("Launch_Group_Control", cultureInfo);
            btnExit.Content = resourceManager.GetString("Close_Group_Control", cultureInfo);
            rbChinese.Content = resourceManager.GetString("Chinese", cultureInfo);
            rbEnglish.Content = resourceManager.GetString("English", cultureInfo);
            lblAuthorizedNumbers.Text = string.Format(resourceManager.GetString("Authorized_Number", cultureInfo), ConfigVals.MaxNums);
            Title = resourceManager.GetString("Product_Name", cultureInfo);
        }


        private void rbLanguage_Checked(object sender, System.Windows.RoutedEventArgs e)
        {
            SwitchLanguage();
        }

        private void btnSavePaameters_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            int groupCapacity = int.Parse(cbGroupCapacity.SelectionBoxItem.ToString());
            int rowCapacity = int.Parse(cbRowCapacity.SelectionBoxItem.ToString());

            VmManager.Instance.Initialize(ConfigVals.MaxNums, groupCapacity);

            ConfigsBLL bll = new ConfigsBLL();
            int lang = (rbEnglish.IsChecked == true) ? 2 : 1;
            int flag = bll.SaveConfigs(lang, groupCapacity, rowCapacity);
            if (flag > 0)
            {
                ConfigVals.Lang = lang;

                ConfigVals.GroupNums = groupCapacity;
                ConfigVals.RowNums = rowCapacity;
                MessageBox.Show(resourceManager.GetString("Save_Success", cultureInfo));
            }

        }

        private void btnLaunchGroupControl_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            MainView mainView = new MainView();
            mainView.Show();

            Close();
        }

        private void btnExit_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            Close();
        }
    }
}
