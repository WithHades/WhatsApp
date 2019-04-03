using Squirrel;
using System;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xzy.EmbeddedApp.Utils;

namespace Xzy.EmbeddedApp.WinForm.Forms
{
    public partial class UpdatingForm : Form
    {
        public UpdatingForm()
        {
            InitializeComponent();
        }

        private void UpdatingForm_Load(object sender, EventArgs e)
        {
            var sheduler = TaskScheduler.FromCurrentSynchronizationContext();

            Task.Run(async () =>
           {
               string updateUrl = ConfigurationManager.AppSettings["UpdateUrl"];
               using (var updateManager = new UpdateManager(updateUrl))
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
                               BeginInvoke(new Action(async () =>
                               {
                                   var releases = updateInfo.ReleasesToApply;

                                   lblUpdatingMsg.Text = "正在下载更新，请稍候......";

                                   await updateManager.DownloadReleases(releases, p => progressBar1.Value = p);

                                   lblUpdatingMsg.Text = "正在处理文件，请稍候......";
                                   await updateManager.ApplyReleases(updateInfo, p => progressBar1.Value = p);

                                   await updateManager.CreateUninstallerRegistryEntry();
                               }));
                           }

                           break;
                       }
                       catch (Exception ex)
                       {
                           LogUtils.Error($"{ex}");
                           lastError = ex;

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
               }
           }).ContinueWith(task=>
           {
               GC.WaitForFullGCComplete();

               Close();

               LogUtils.Information("关闭UpdatingForm");

               Application.Exit();

               LogUtils.Information("退出Application");

               UpdateManager.RestartApp();

           });
        }
    }
}
