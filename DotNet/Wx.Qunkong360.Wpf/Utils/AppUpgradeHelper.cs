using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Wx.Qunkong360.Wpf.Implementation;
using Xzy.EmbeddedApp.Utils;

namespace Wx.Qunkong360.Wpf.Utils
{
    public class AppUpgradeHelper
    {
        private AppUpgradeHelper()
        {

        }

        public static readonly AppUpgradeHelper Instance = new AppUpgradeHelper();

        public async Task<string> InstallNewestApp()
        {
            try
            {
                await Task.Delay(2000);

                if (VmManager.Instance.RunningGroupIndex == -1)
                {

                    return MainView.resourceManager.GetString("Error_Please_Launch_A_Vm", MainView.cultureInfo);
                }

                string appFolder = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location), "UpgradeApp");

                if (!Directory.Exists(appFolder))
                {
                    Directory.CreateDirectory(appFolder);
                }

                string appFile = Path.Combine(appFolder, "app-release.apk");

                if (!File.Exists(appFile))
                {
                    return MainView.resourceManager.GetString("No_Updated_App", MainView.cultureInfo);
                }

                for (int i = 0; i < VmManager.Instance.Column; i++)
                {
                    int vmIndex = VmManager.Instance.VmIndexArray[VmManager.Instance.RunningGroupIndex, i];

                    if (vmIndex != -1)
                    {
                        ProcessUtils.AdbInstallApp(vmIndex, appFile);
                        Thread.Sleep(200);
                    }
                }

                File.Delete(appFile);

                return MainView.resourceManager.GetString("Finish_App_Update", MainView.cultureInfo);
            }
            catch (Exception ex)
            {
                LogUtils.Error($"{ex}");
                return MainView.resourceManager.GetString("Failed_App_Update", MainView.cultureInfo);
            }
        }
    }
}
