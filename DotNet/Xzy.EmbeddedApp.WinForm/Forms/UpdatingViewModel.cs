using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;
using Squirrel;
using Application = System.Windows.Forms.Application;
using Xzy.EmbeddedApp.Utils;
using System.Configuration;

namespace Xzy.EmbeddedApp.WinForm.Forms
{
    public class UpdatingViewModel : BindableBase
    {
        private readonly UpdatingView _updatingView;

        public UpdatingViewModel(UpdatingView updatingView)
        {
            _updatingView = updatingView;

            Message = "请勿关闭程序，正在升级中......\r\n升级完成后会自动启动新版本。";

            UpdatingCommand = new DelegateCommand(UpdatingAsync);
        }

        public ICommand UpdatingCommand { get; set; }

        private string _message;

        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        private string _progress;

        public string Progress
        {
            get { return _progress; }
            set { SetProperty(ref _progress, value); }
        }

        private void UpdatingAsync()
        {
            var scheduler = TaskScheduler.FromCurrentSynchronizationContext();
            Task.Run(async () =>
                {
                    string updateUrl = ConfigurationManager.AppSettings["UpdateUrl"];

                    using (var updateManager = new UpdateManager(updateUrl)
                    )
                    {
                        var ignore = false;
                        Exception lastError = null;
                        while (true)
                        {
                            try
                            {
                                var updateInfo = await updateManager.CheckForUpdate(ignore);
                                if (updateInfo?.ReleasesToApply?.Any() == true)
                                {
                                    var releases = updateInfo.ReleasesToApply;

                                    Message = "正在下载更新，请稍候。";
                                    await updateManager.DownloadReleases(releases, p => Progress = $"{p}%")
                                        .ConfigureAwait(false);

                                    Message = "正在处理更新文件,请稍候。";
                                    await updateManager.ApplyReleases(updateInfo, p => Progress = $"{p}%")
                                        .ConfigureAwait(false);

                                    await updateManager.CreateUninstallerRegistryEntry();
                                }
                                break;
                            }
                            catch (Exception e)
                            {
                               LogUtils.Error($"{e}");
                                lastError = e;

                                if (!ignore)
                                {
                                    ignore = true;
                                    continue;
                                }
                                break;
                            }
                        }

                        if (lastError != null)
                        {
                            throw lastError;
                        }


                        // 可用以下方式简化
                        //var updateInfo = await updateManager.CheckForUpdate().ConfigureAwait(false);
                        //if (updateInfo?.ReleasesToApply?.Any() == true)
                        //{
                        //    this.UpdateTip = "正在更新，请稍候。";
                        //    await updateManager.UpdateApp(p =>
                        //    {
                        //        this.Progress = p;
                        //    });
                        //}
                    }
                })
                .ContinueWith(task =>
                {
                    _updatingView.Close();

                    if (task.Exception != null)
                    {
                        Message = "执行更新时出错。";
                        LogUtils.Error($"{task.Exception}");
                        task.Exception.Handle(_ => true);
                    }
                    else
                    {
                        LogUtils.Information("等待GC完成");
                        GC.WaitForFullGCComplete();

                        LogUtils.Information("退出Application");

                        Application.Exit();

                        LogUtils.Information("重启Application");

                        UpdateManager.RestartApp();
                    }
                }, scheduler);
        }
    }
}
