using IWshRuntimeLibrary;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using Xzy.EmbeddedApp.Utils;
using System.Linq;
using Squirrel;
using Xzy.EmbeddedApp.WinForm.Socket;

namespace Wx.Qunkong360.Wpf
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            LogUtils.SetupLogger();

            Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            HandleStartupArgs(e.Args);

            MakeAppSquirrelAware();

            Task.Run(async () =>
            {
                await CheckUpdate();
            });

            //ModemSocketClient.Connect();
            SocketServer.Init();
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            LogUtils.Error($"{exception}");

            MessageBox.Show(exception.Message);

        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            LogUtils.Error($"{e.Exception}");

            MessageBox.Show(e?.Exception?.Message);
        }

        private static void HandleStartupArgs(string[] args)
        {
            foreach (string arg in args)
            {
                if (arg.Contains("uninstall"))
                {
                    RemoveShortcut();
                }
            }
        }


        private static void CreateShrotcut()
        {
            try
            {
                string location = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                    "群控360.lnk");

                LogUtils.Debug($"create shortcut => {location}");

                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(location);

                string currentVersionExeFullName = Process.GetCurrentProcess().MainModule.FileName;

                DirectoryInfo curDirectoryInfo = new DirectoryInfo(currentVersionExeFullName);

                string lastLevelDirName = curDirectoryInfo.Parent.ToString();
                string exeName = Path.GetFileName(currentVersionExeFullName);

                string lastLevelPath = Path.Combine(lastLevelDirName, exeName);

                string targetDir = currentVersionExeFullName.Replace(lastLevelPath, string.Empty);

                string targetExeFullName = Path.Combine(targetDir, exeName);

                shortcut.TargetPath = targetExeFullName;

                LogUtils.Debug($"target path => {shortcut.TargetPath}");
                shortcut.Save();

            }
            catch (Exception ex)
            {
                LogUtils.Error($"CreateShrotcut => {ex}");
            }
        }

        private static void RemoveShortcut()
        {
            try
            {
                DirectoryInfo desktopDirectoryInfo =
                    new DirectoryInfo(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory));

                if (desktopDirectoryInfo.GetFiles("*.lnk")
                    .Any(file => file.Name == "群控360.lnk"))
                {
                    LogUtils.Debug($"RemoveShortcut => has shortcut on desktop");
                    System.IO.File.Delete(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
                        "群控360.lnk"));
                }
                else
                {
                    LogUtils.Debug($"RemoveShortcut => no shortcut on desktop");
                }
            }
            catch (Exception ex)
            {
                LogUtils.Error($"RemoveShortcut => {ex}");
            }
        }

        private static void MakeAppSquirrelAware()
        {
            try
            {
                string updateUrl = ConfigurationManager.AppSettings["UpdateUrl"];
                using (var mgr = new UpdateManager(updateUrl))
                {
                    SquirrelAwareApp.HandleEvents(
                        onInitialInstall: v =>
                        {
                            LogUtils.Information($"onInitialInstall => 版本号：{v.ToString(3)}");
                            CreateShrotcut();
                            Current.Shutdown();
                        },

                        onAppUpdate: v =>
                        {
                            //await CopyConfigFile();
                            LogUtils.Information($"onAppUpdate => 版本号：{v.ToString(3)}");
                            CreateShrotcut();
                            Current.Shutdown();
                        },

                        onAppUninstall: v =>
                        {
                            LogUtils.Information($"onAppUninstall => 版本号：{v.ToString(3)}");
                            RemoveShortcut();
                            Current.Shutdown();
                        },

                        onAppObsoleted: v =>
                        {
                            Current.Shutdown();
                        },

                        onFirstRun: () =>
                        {
                            LogUtils.Information("onFirstRun =>");
                            CreateShrotcut();
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                LogUtils.Error($"MakeAppSquirrelAware => {ex}");
            }
        }

        private static async Task<bool> CheckUpdate()
        {
            var hasUpdate = false;

            try
            {
                string updateUrl = ConfigurationManager.AppSettings["UpdateUrl"];
                using (var updateMgr = new UpdateManager(updateUrl))
                {
                    var updateInfo = await updateMgr.CheckForUpdate();
                    if (updateInfo != null && updateInfo.ReleasesToApply?.Any() == true)
                    {
                        // 包含更新
                        hasUpdate = true;
                    }
                }

                if (hasUpdate)
                {
                    //LogUtils.Information($"是否包含更新：{hasUpdate}");

                    //var updateMsg = "检测到有新版本，是否升级？";
                    //var result = MessageBox.Show(updateMsg, "更新提示", MessageBoxButtons.YesNo);
                    //LogUtils.Information($"是否选择更新：{result}");
                    //if (result != DialogResult.Yes)
                    //{
                    //    LogUtils.Debug("【refuse to update】");
                    //}
                    //else
                    //{
                    LogUtils.Debug("【ready to update】");

                    await Current.Dispatcher.BeginInvoke(new Action(() =>
                     {
                         UpdatingView updatingView = new UpdatingView();
                         updatingView.ShowDialog();

                     }));

                    return true;
                    //}
                }
            }
            catch (Exception ex)
            {
                LogUtils.Error($"CheckUpdate => {ex}");
            }
            return false;
        }

    }
}
