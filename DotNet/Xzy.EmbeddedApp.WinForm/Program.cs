using IWshRuntimeLibrary;
using Squirrel;
using System;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xzy.EmbeddedApp.Utils;
using Xzy.EmbeddedApp.WinForm.Forms;
using Xzy.EmbeddedApp.WinForm.Socket;

namespace Xzy.EmbeddedApp.WinForm
{
    class Program
    {
        public static string accesskey = "ab7345d6094a0af9bcca4fcb3371c007";

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            LogUtils.SetupLogger();

            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);

            Application.ThreadException += Application_ThreadException;

            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (!IsAdministrator())
            {
                MessageBox.Show("请使用管理员权限运行本软件!");
                return;
            }
            //Application.Run(new WhatAppForm());

            HandleStartupArgs(args);

            MakeAppSquirrelAware();

            Task.Run(async () =>
              {
                  await CheckUpdate();
              });

            ModemSocketClient.Connect();
            SocketServer.Init();
            Application.Run(new AuthForm());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            LogUtils.Error($"{exception}");

            MessageBox.Show(exception.Message);
        }

        private static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            LogUtils.Error($"{e.Exception}");

            MessageBox.Show(e?.Exception?.Message);
        }



        /// <summary>
        /// 判断是否是管理员执行
        /// </summary>
        /// <returns></returns>
        private static bool IsAdministrator()
        {
            WindowsIdentity current = WindowsIdentity.GetCurrent();
            WindowsPrincipal windowsPrincipal = new WindowsPrincipal(current);
            return windowsPrincipal.IsInRole(WindowsBuiltInRole.Administrator);
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
                            Application.Exit();
                        },

                        onAppUpdate: v =>
                        {
                            //await CopyConfigFile();
                            LogUtils.Information($"onAppUpdate => 版本号：{v.ToString(3)}");
                            CreateShrotcut();
                            Application.Exit();
                        },

                        onAppUninstall: v =>
                        {
                            LogUtils.Information($"onAppUninstall => 版本号：{v.ToString(3)}");
                            RemoveShortcut();
                            Application.Exit();
                        },

                        onAppObsoleted: v =>
                        {
                            Application.Exit();
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

                        int formsCount = Application.OpenForms.Count;

                        if (formsCount > 0)
                        {
                            Form form = Application.OpenForms[0];

                            form.BeginInvoke(new Action(() =>
                            {
                                UpdatingView updatingView = new UpdatingView();
                                updatingView.ShowDialog();
                            }));
                        }

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
