using Cj.EmbeddedAPP.BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using MediaElement = System.Windows.Controls.MediaElement;
using System.Windows.Forms;
using System.Windows.Forms.Integration;
using WpfTreeView;
using Xzy.EmbeddedApp.Model;
using Xzy.EmbeddedApp.Utils;
using Xzy.EmbeddedApp.WinForm.Implementation;
using Xzy.EmbeddedApp.WinForm.Tasks;
using Newtonsoft.Json.Linq;
using System.Resources;
using System.Globalization;

namespace Xzy.EmbeddedApp.WinForm
{
    public partial class AppOptForm : Form
    {
        ResourceManager resourceManager;
        CultureInfo cultureInfo;

        #region 发送消息
        class MessageType
        {
            public string Display { get; set; }
            public int Value { get; set; }
        }

        private void tabpage_bathsend_Enter(object sender, EventArgs e)
        {
            btnAddPic.Tag = panelPicThumbnails;
            btnDeletePic.Tag = panelPicThumbnails;

            SetSendMessagesPageStatus();
            InitRunningVmsTreeView(wpfTreeView2);

            InitSendMessageTypes();
        }

        private void InitSendMessageTypes()
        {
            cbMessageTypes.Items.Clear();
            cbMessageTypes.Items.Add(new MessageType() { Display = "纯文本消息", Value = (int)TaskType.SendMessage });
            cbMessageTypes.Items.Add(new MessageType() { Display = "纯图片消息", Value = (int)TaskType.SendPicture });
            cbMessageTypes.Items.Add(new MessageType() { Display = "图文消息", Value = (int)TaskType.SendMessageAndPicture });
            cbMessageTypes.Items.Add(new MessageType() { Display = "纯视频消息", Value = (int)TaskType.SendVideo });
            cbMessageTypes.Items.Add(new MessageType() { Display = "视文消息", Value = (int)TaskType.SendMessageAndVideo });
            cbMessageTypes.Items.Add(new MessageType() { Display = "音频消息", Value = (int)TaskType.SendAudio });

            cbMessageTypes.DisplayMember = "Display";
            cbMessageTypes.ValueMember = "Value";
            cbMessageTypes.SelectedIndex = 0;
        }

        private void SetSendMessagesPageStatus()
        {
            bool enabled = VmManager.Instance.RunningGroupIndex != -1;

            elementHost2.Enabled = enabled;
            rtbMsg.Enabled = enabled;
            panelPicThumbnails.Enabled = enabled;
            panelVideoThumbnails.Enabled = enabled;
            btnClearMsg.Enabled = enabled;
            btnAddPic.Enabled = enabled;
            btnAddVideo.Enabled = enabled;
            btnDeletePic.Enabled = enabled;
            btnDeleteVideo.Enabled = enabled;
            btnAddAudio.Enabled = enabled;
            btnDeleteAudio.Enabled = enabled;
            btnSendMsg.Enabled = enabled;
            cbMessageTypes.Enabled = enabled;
        }

        private void cbMessageTypes_SelectedValueChanged(object sender, EventArgs e)
        {
            MessageType messageType = cbMessageTypes.SelectedItem as MessageType;

            if (messageType == null || VmManager.Instance.RunningGroupIndex == -1)
            {
                return;
            }

            TaskType taskType = (TaskType)messageType.Value;

            switch (taskType)
            {
                case TaskType.ImportContacts:
                    break;
                case TaskType.PostMessage:
                    break;
                case TaskType.PostPicture:
                    break;
                case TaskType.PostMessageAndPicture:
                    break;
                case TaskType.SendMessage:

                    rtbMsg.Enabled = true;
                    btnClearMsg.Enabled = true;
                    //btnSendMsg.Enabled = true;

                    panelPicThumbnails.Enabled = false;
                    panelVideoThumbnails.Enabled = false;
                    panelAudio.Enabled = false;

                    btnAddPic.Enabled = false;
                    btnDeletePic.Enabled = false;
                    btnAddVideo.Enabled = false;
                    btnDeleteVideo.Enabled = false;

                    btnAddAudio.Enabled = false;
                    btnDeleteAudio.Enabled = false;

                    break;
                case TaskType.SendPicture:

                    rtbMsg.Enabled = false;
                    btnClearMsg.Enabled = false;
                    //btnSendMsg.Enabled = true;


                    panelPicThumbnails.Enabled = true;
                    panelVideoThumbnails.Enabled = false;
                    panelAudio.Enabled = false;

                    btnAddPic.Enabled = true;
                    btnDeletePic.Enabled = true;
                    btnAddVideo.Enabled = false;
                    btnDeleteVideo.Enabled = false;
                    btnAddAudio.Enabled = false;
                    btnDeleteAudio.Enabled = false;

                    break;
                case TaskType.SendMessageAndPicture:

                    rtbMsg.Enabled = true;
                    btnClearMsg.Enabled = true;
                    //btnSendMsg.Enabled = true;


                    panelPicThumbnails.Enabled = true;
                    panelVideoThumbnails.Enabled = false;
                    panelAudio.Enabled = false;

                    btnAddPic.Enabled = true;
                    btnDeletePic.Enabled = true;
                    btnAddVideo.Enabled = false;
                    btnDeleteVideo.Enabled = false;
                    btnAddAudio.Enabled = false;
                    btnDeleteAudio.Enabled = false;

                    break;
                case TaskType.SendVideo:

                    rtbMsg.Enabled = false;
                    btnClearMsg.Enabled = false;
                    //btnSendMsg.Enabled = true;


                    panelPicThumbnails.Enabled = false;
                    panelVideoThumbnails.Enabled = true;
                    panelAudio.Enabled = false;

                    btnAddPic.Enabled = false;
                    btnDeletePic.Enabled = false;
                    btnAddVideo.Enabled = true;
                    btnDeleteVideo.Enabled = true;
                    btnAddAudio.Enabled = false;
                    btnDeleteAudio.Enabled = false;

                    break;
                case TaskType.SendMessageAndVideo:

                    rtbMsg.Enabled = true;
                    btnClearMsg.Enabled = true;
                    //btnSendMsg.Enabled = true;


                    panelPicThumbnails.Enabled = false;
                    panelVideoThumbnails.Enabled = true;
                    panelAudio.Enabled = false;

                    btnAddPic.Enabled = false;
                    btnDeletePic.Enabled = false;
                    btnAddVideo.Enabled = true;
                    btnDeleteVideo.Enabled = true;
                    btnAddAudio.Enabled = false;
                    btnDeleteAudio.Enabled = false;

                    break;
                case TaskType.SendAudio:

                    rtbMsg.Enabled = false;
                    btnClearMsg.Enabled = false;
                    //btnSendMsg.Enabled = true;


                    panelPicThumbnails.Enabled = false;
                    panelVideoThumbnails.Enabled = false;
                    panelAudio.Enabled = true;

                    btnAddPic.Enabled = false;
                    btnDeletePic.Enabled = false;
                    btnAddVideo.Enabled = false;
                    btnDeleteVideo.Enabled = false;
                    btnAddAudio.Enabled = true;
                    btnDeleteAudio.Enabled = true;

                    break;
                default:
                    break;
            }
        }


        private void btnClearMsg_Click(object sender, EventArgs e)
        {
            rtbMsg.Text = string.Empty;
        }

        private void btnAddVideo_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "请选择视频";
            openFileDialog.Filter = "视频|*.mp4;*.avi;*.flv;*.wmv";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (panelVideoThumbnails.Controls.Count >0)
                {
                    panelVideoThumbnails.ScrollControlIntoView(panelVideoThumbnails.Controls[0]);
                }

                string[] addedVideoPath = openFileDialog.FileNames;

                int startingIndex = panelVideoThumbnails.Controls.Count;
                double total = panelVideoThumbnails.Controls.Count + addedVideoPath.Length;
                double columnCapacity = TaskManager.ColumnCapacity;

                int totalRow = (int)Math.Ceiling(total / columnCapacity);

                for (int row = 0; row < totalRow; row++)
                {
                    for (int column = 0; column < TaskManager.ColumnCapacity; column++)
                    {
                        int picturePathIndex = row * TaskManager.ColumnCapacity + column;

                        if (picturePathIndex < total && picturePathIndex >= startingIndex)
                        {
                            ElementHost host = new ElementHost()
                            {
                                Name = $"host{picturePathIndex}",
                                BackColor = Color.Gray,
                                Width = 300,
                                Height = 300,
                                //ImageLocation = addedPicPath[picturePathIndex - startingIndex],
                                Location = new Point()
                                {
                                    X = 5 + column * 305,
                                    Y = 5 + row * 305,
                                },
                                //SizeMode = PictureBoxSizeMode.Zoom,
                            };

                            System.Windows.Controls.Grid grid = new System.Windows.Controls.Grid();

                            System.Windows.Controls.CheckBox checkBox = new System.Windows.Controls.CheckBox()
                            {
                                Width = 30,
                                Height = 30,
                                HorizontalAlignment = System.Windows.HorizontalAlignment.Left,
                                VerticalAlignment = System.Windows.VerticalAlignment.Top,
                            };


                            System.Windows.Controls.MediaElement mediaElement = new MediaElement()
                            {
                                Width = 300,
                                Height = 300,
                                //VerticalAlignment = System.Windows.VerticalAlignment.Bottom,
                                //HorizontalAlignment = System.Windows.HorizontalAlignment.Right,
                                Source = new Uri(addedVideoPath[picturePathIndex - startingIndex]),
                            };
                            grid.Children.Add(checkBox);
                            grid.Children.Add(mediaElement);

                            host.Child = grid;

                            panelVideoThumbnails.Controls.Add(host);

                        }
                    }
                }
            }
        }

        private void btnDeleteVideo_Click(object sender, EventArgs e)
        {
            var hosts = from host in panelVideoThumbnails.Controls.Cast<ElementHost>()
                        where host.Child is System.Windows.Controls.Grid &&
                        ((System.Windows.Controls.Grid)host.Child).Children[0] is System.Windows.Controls.CheckBox &&
                      ((System.Windows.Controls.CheckBox)((System.Windows.Controls.Grid)host.Child).Children[0]).IsChecked.Value
                        select host;

            if (hosts.Count() == 0)
            {
                return;
            }

            List<string> tobeDeletedKeys = new List<string>();

            foreach (var h in hosts)
            {
                tobeDeletedKeys.Add(h.Name);
            }

            foreach (var key in tobeDeletedKeys)
            {
                panelVideoThumbnails.Controls.RemoveByKey(key);
            }

            double total = panelVideoThumbnails.Controls.Count;
            double columnCapacity = TaskManager.ColumnCapacity;

            int totalRow = (int)Math.Ceiling(total / columnCapacity);

            for (int row = 0; row < totalRow; row++)
            {
                for (int column = 0; column < TaskManager.ColumnCapacity; column++)
                {
                    int picturePathIndex = row * TaskManager.ColumnCapacity + column;

                    if (picturePathIndex < total)
                    {
                        ElementHost host = panelVideoThumbnails.Controls[picturePathIndex] as ElementHost;

                        host.Location = new Point()
                        {
                            X = 5 + column * 305,
                            Y = 5 + row * 305,
                        };
                    }
                }
            }

        }


        private void btnAddAudio_Click(object sender, EventArgs e)
        {

        }

        private void btnDeleteAudio_Click(object sender, EventArgs e)
        {

        }

        private bool IsValidInput()
        {
            string sRepeatTimes = txtSendTimes.Text.Trim();
            string sMinTimeSpan = txtMinSeconds.Text.Trim();
            string sMaxTimeSpan = txtMaxSeconds.Text.Trim();

            int intRepeatTimes, intMinTimeSpan, intMaxTimeSpan;

            if (!int.TryParse(sRepeatTimes, out intRepeatTimes))
            {
                MessageBox.Show("请输入正确的发送次数！");
                return false;
            }
            else
            {
                if (intRepeatTimes < 1)
                {
                    MessageBox.Show("发送次数不能小于1！");
                    return false;
                }
            }

            if (!int.TryParse(sMinTimeSpan, out intMinTimeSpan))
            {
                MessageBox.Show("请输入正确的时间间隔！");
                return false;
            }
            else
            {
                if (intMinTimeSpan <1)
                {
                    MessageBox.Show("时间间隔起始值不能小于1秒！");
                    return false;
                }
            }

            if (!int.TryParse(sMaxTimeSpan, out intMaxTimeSpan))
            {
                MessageBox.Show("请输入正确的时间间隔！");
                return false;
            }
            else
            {
                if (intMaxTimeSpan < 2)
                {
                    MessageBox.Show("时间间隔结束值不能小于2秒！");
                    return false;
                }
            }

            if (intMinTimeSpan >= intMaxTimeSpan)
            {
                MessageBox.Show("时间间隔起始值不能大于等于结束值！");
                return false;
            }

            return true;
        }

        private void btnSendMsg_Click(object sender, EventArgs e)
        {
            btnSendMsg.Enabled = false;
            try
            {
                if (!IsValidInput())
                {
                    btnSendMsg.Enabled = true;
                    return;
                }

                string[] paths = null;

                var targets = from item in wpfTreeView2.ItemsSourceData.FirstOrDefault().Children
                              where item.IsChecked
                              select (int)item.Id - 1;


                if (targets.Count() == 0)
                {
                    btnSendMsg.Enabled = true;

                    MessageBox.Show("请选择模拟器！");
                    return;
                }

                MessageType messageType = (MessageType)cbMessageTypes.SelectedItem;

                string folderName = string.Empty;

                switch ((TaskType)messageType.Value)
                {
                    case TaskType.ImportContacts:
                        break;
                    case TaskType.PostMessage:
                        break;
                    case TaskType.PostPicture:
                        break;
                    case TaskType.PostMessageAndPicture:
                        break;
                    case TaskType.SendMessage:
                        break;
                    case TaskType.SendPicture:
                    case TaskType.SendMessageAndPicture:

                        folderName = "image";

                        var pictures = from pic in panelPicThumbnails.Controls.Cast<Panel>()
                                       select ((PictureBox)pic.Controls[1]).ImageLocation;

                        paths = pictures.ToArray();

                        break;
                    case TaskType.SendVideo:
                    case TaskType.SendMessageAndVideo:

                        folderName = "video";

                        var videos = from host in panelVideoThumbnails.Controls.Cast<ElementHost>()
                                     select ((MediaElement)(((System.Windows.Controls.Grid)host.Child).Children[1])).Source.OriginalString;

                        paths = videos.ToArray();

                        break;
                    case TaskType.SendAudio:

                        folderName = "audio";


                        break;
                    default:
                        break;
                }

                TaskManager taskManager = new TaskManager((TaskType)messageType.Value, rtbMsg.Text.Trim(), paths, targets.ToArray());


                string dir = DateTime.Now.ToString("yyyyMMddHHmmssffff");

                foreach (int mobileIndex in taskManager.MobileIndexs)
                {
                    var lists = new JArray
                    {

                    };


                    for (int i = 0; i < taskManager.Paths?.Length; i++)
                    {
                        string target = $"/sdcard/qunkong/{folderName}/{dir}/{i + 1}{Path.GetExtension(taskManager.Paths[i])}";

                        ProcessUtils.PushFileToVm(mobileIndex, taskManager.Paths[i], target);

                        lists.Add(target);
                    }

                    var obj = new JObject() { { "tasktype", (int)taskManager.TaskType }, { "txtmsg", taskManager.Message } };

                    obj.Add("list", lists);


                    TaskSch taskSch = new TaskSch()
                    {
                        Bodys = obj.ToString(Formatting.None),
                        MobileIndex = mobileIndex,
                        TypeId = (int)taskManager.TaskType,
                        Status = "waiting",
                        RepeatNums = int.Parse(txtSendTimes.Text.Trim()),
                        RandomMins = int.Parse(txtMinSeconds.Text.Trim()),
                        RandomMaxs = int.Parse(txtMaxSeconds.Text.Trim())
                    };

                    TasksBLL.CreateTask(taskSch);
                }

                Thread taskthread = new Thread(ProessTask);
                taskthread.Start();

                btnSendMsg.Enabled = true;

                MessageBox.Show($"已添加{taskManager.MobileIndexs.Length}条任务");
            }
            catch (Exception ex)
            {
                btnSendMsg.Enabled = true;
                LogUtils.Error($"{ex}");
                MessageBox.Show(ex.Message);
            }
        }

        #endregion



        #region 发送动态
        private void RegisterEvents()
        {
            tabpage_senddynamic.Enter += Tabpage_senddynamic_Enter;
        }

        private void Tabpage_senddynamic_Enter(object sender, EventArgs e)
        {
            btnAddPicture.Tag = panelPictureThumbnails;
            btnDeletePicture.Tag = panelPictureThumbnails;

            SetSendDynamicPageStatus();
            InitRunningVmsTreeView(wpfTreeView1);
        }

        private void SetSendDynamicPageStatus()
        {
            bool enabled = VmManager.Instance.RunningGroupIndex != -1;

            elementHost1.Enabled = enabled;
            rtbMessage.Enabled = enabled;
            panelPictureThumbnails.Enabled = enabled;
            btnClearMessage.Enabled = enabled;
            btnAddPicture.Enabled = enabled;
            btnDeletePicture.Enabled = enabled;
            btnPost.Enabled = enabled;

        }

        /// <summary>
        /// 初始化树结构
        /// </summary>
        /// <param name="wpfTreeView"></param>
        private void InitRunningVmsTreeView(WpfTreeView.WpfTreeView wpfTreeView)
        {
            int runningGroupIndex = VmManager.Instance.RunningGroupIndex;

            if (runningGroupIndex == -1)
            {
                return;
            }

            int groupEndIndex = VmManager.Instance.VmIndexArray[runningGroupIndex, VmManager.Instance.Column - 1];
            int endNumber = groupEndIndex == -1 ? VmManager.Instance.MaxVmNumber : groupEndIndex + 1;

            string firstLevelNodeText = $"第{runningGroupIndex + 1}组 {VmManager.Instance.VmIndexArray[runningGroupIndex, 0] + 1}-{endNumber}";

            List<WpfTreeViewItem> wpfTreeViewItems = new List<WpfTreeViewItem>();

            WpfTreeViewItem topLevelNode = new WpfTreeViewItem()
            {
                Caption = firstLevelNodeText,
                Id = -1,
            };

            wpfTreeViewItems.Add(topLevelNode);

            for (int i = 0; i < VmManager.Instance.Column; i++)
            {
                if (VmManager.Instance.VmIndexArray[runningGroupIndex, i] != -1)
                {
                    WpfTreeViewItem subNode = new WpfTreeViewItem()
                    {
                        Id = VmManager.Instance.VmIndexArray[runningGroupIndex, i] + 1,
                        Caption = $"{VmManager.Instance.VmIndexArray[runningGroupIndex, i] + 1}",
                        ParentId = -1,
                    };

                    wpfTreeViewItems.Add(subNode);
                }
            }

            wpfTreeView.SetItemsSourceData(wpfTreeViewItems, item => item.Caption, item => item.Id, item => item.ParentId);
        }

        private void btnClearMessage_Click(object sender, EventArgs e)
        {
            rtbMessage.Text = string.Empty;
        }

        private void btnAddPicture_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Panel picContainer = btn.Tag as Panel;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "请选择图片";
            openFileDialog.Filter = "图片|*.jpg;*.png;*.jpeg;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (picContainer.Controls.Count > 0)
                {
                    picContainer.ScrollControlIntoView(picContainer.Controls[0]);
                }

                string[] addedPicPath = openFileDialog.FileNames;

                int startingIndex = picContainer.Controls.Count;
                double total = picContainer.Controls.Count + addedPicPath.Length;
                double columnCapacity = TaskManager.ColumnCapacity;

                int totalRow  = (int)Math.Ceiling(total / columnCapacity);

                for (int row = 0; row < totalRow; row++)
                {
                    for (int column = 0; column < TaskManager.ColumnCapacity; column++)
                    {
                        int picturePathIndex = row * TaskManager.ColumnCapacity + column;

                        if (picturePathIndex < total && picturePathIndex >= startingIndex)
                        {

                            Panel panel = new Panel()
                            {
                                Name = $"panel{picturePathIndex}",
                                BackColor = Color.Gray,
                                Width = 300,
                                Height = 300,
                                Location = new Point()
                                {
                                    X = 5 + column * 305,
                                    Y = 5 + row * 305,
                                },
                            };

                            CheckBox checkBox = new CheckBox()
                            {
                                Checked = false,
                                Location = new Point()
                                {
                                    X = 0,
                                    Y = 0,
                                },
                                Width = 30,
                                Height = 30
                            };

                            PictureBox pictureBox = new PictureBox()
                            {
                                BackColor = Color.Gray,
                                Width = 300,
                                Height = 300,
                                ImageLocation = addedPicPath[picturePathIndex - startingIndex],
                                Location = new Point()
                                {
                                    X = 0,
                                    Y = 0,
                                },
                                SizeMode = PictureBoxSizeMode.Zoom,
                            };

                            panel.Controls.Add(checkBox);
                            panel.Controls.Add(pictureBox);

                            picContainer.Controls.Add(panel);
                        }
                    }
                }
            }
        }

        private void btnDeletePicture_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Panel picContainer = btn.Tag as Panel;

            var panels = picContainer.Controls.Cast<Panel>().Where(p => p.Controls[0] is CheckBox && ((CheckBox)p.Controls[0]).Checked);

            if (panels.Count() == 0)
            {
                return;
            }

            List<string> tobeDeletedKeys = new List<string>();

            foreach (var p in panels)
            {
                tobeDeletedKeys.Add(p.Name);
            }

            foreach (var key in tobeDeletedKeys)
            {
                picContainer.Controls.RemoveByKey(key);
            }

            double total = picContainer.Controls.Count;
            double columnCapacity = TaskManager.ColumnCapacity;

            int totalRow = (int)Math.Ceiling(total / columnCapacity);

            for (int row = 0; row < totalRow; row++)
            {
                for (int column = 0; column < TaskManager.ColumnCapacity; column++)
                {
                    int picturePathIndex = row * TaskManager.ColumnCapacity + column;

                    if (picturePathIndex < total)
                    {
                        Panel panel = picContainer.Controls[picturePathIndex] as Panel;

                        panel.Location = new Point()
                        {
                            X = 5 + column * 305,
                            Y = 5 + row * 305,
                        };
                    }
                }
            }

        }

        private void btnPost_Click(object sender, EventArgs e)
        {
            var targets = from item in wpfTreeView1.ItemsSourceData.FirstOrDefault().Children
                          where item.IsChecked
                          select (int)item.Id - 1;


            var pictures = from pic in panelPictureThumbnails.Controls.Cast<Panel>()
                           select ((PictureBox)pic.Controls[1]).ImageLocation;

            string[] paths = pictures.ToArray();
            string message = rtbMessage.Text.Trim();

            TaskType taskType = TaskType.PostMessage;
            string folderName = "image";

            if (paths != null && paths.Length > 0 && !string.IsNullOrEmpty(message))
            {
                taskType = TaskType.PostMessageAndPicture;
            }

            if (paths!=null && paths.Length>0 && string.IsNullOrEmpty(message))
            {
                taskType = TaskType.PostPicture;
            }

            if ((paths == null || paths.Length == 0) && !string.IsNullOrEmpty(message))
            {
                taskType = TaskType.PostMessage;
            }

            TaskManager taskManager = new TaskManager(taskType, message, paths, targets.ToArray());


            string dir = DateTime.Now.ToString("yyyyMMddHHmmssffff");

            foreach (int mobileIndex in taskManager.MobileIndexs)
            {
                var lists = new JArray
                {

                };


                for (int i = 0; i < taskManager.Paths?.Length; i++)
                {
                    string target = $"/sdcard/qunkong/{folderName}/{dir}/{i + 1}{Path.GetExtension(taskManager.Paths[i])}";

                    ProcessUtils.PushFileToVm(mobileIndex, taskManager.Paths[i], target);

                    lists.Add(target);
                }

                var obj = new JObject() { { "tasktype", (int)taskManager.TaskType }, { "txtmsg", taskManager.Message } };

                obj.Add("list", lists);


                TaskSch taskSch = new TaskSch()
                {
                    Bodys = obj.ToString(Formatting.None),
                    MobileIndex = mobileIndex,
                    TypeId = (int)taskManager.TaskType,
                    Status = "waiting",

                };

                TasksBLL.CreateTask(taskSch);
            }

            if (ConfigVals.IsRunning != 1)
            {
                Thread taskthread = new Thread(ProessTask);
                taskthread.Start();
            }

            MessageBox.Show($"已添加{taskManager.MobileIndexs.Length}条任务");

        }

        #endregion









        //选中的设备列表
        private List<int> checkMobiles = new List<int>();

        public object TaskDAL { get; private set; }

        public AppOptForm()
        {
            resourceManager = new ResourceManager("Xzy.EmbeddedApp.WinForm.Languages.Res", typeof(AppOptForm).Assembly);
            if (ConfigVals.Lang == 1)
            {
                cultureInfo = CultureInfo.CreateSpecificCulture("zh-cn");
            }
            else if (ConfigVals.Lang == 2)
            {
                cultureInfo = CultureInfo.CreateSpecificCulture("en-us");
            }

            Text = resourceManager.GetString("WhatsApp_Operation", cultureInfo);

            InitializeComponent();
            RegisterEvents();
        }

        /// <summary>
        /// 导入通讯录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_import_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 进入当前的tab时创建启动列的设备列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tabpage_import_Enter(object sender, EventArgs e)
        {
            createActivityCheckBox();
            tabpage_import.Text = resourceManager.GetString("Import_Address_List", cultureInfo);
            //btn_import.Text = resourceManager.GetString("Import_Address_List", cultureInfo);
            lab_selectmobile.Text = resourceManager.GetString("Select_Vm", cultureInfo);
            lab_phonenummsg.Text = resourceManager.GetString("Enter_Phone_Number", cultureInfo);
            btn_selectfile1.Text = resourceManager.GetString("Select_Path", cultureInfo);
            btn_importfile.Text = resourceManager.GetString("Import", cultureInfo);

        }

        /// <summary>
        /// 创建活动组的设备多选框
        /// </summary>
        public void createActivityCheckBox()
        {
            int X = 35;
            int Y = 80;
            if (VmManager.Instance.RunningGroupIndex != -1)
            {
                //创建第一个全选
                CheckBox check_allmobile_all = new CheckBox();
                check_allmobile_all.Text = resourceManager.GetString("Select_All",cultureInfo);
                check_allmobile_all.Tag = -1;
                check_allmobile_all.Location = new Point(X, Y);
                check_allmobile_all.CheckedChanged += Check_allmobile_all_CheckedChanged;
                panle_importmobiles.Controls.Add(check_allmobile_all);

                Y = Y + 35;
                for (int i = 0; i < VmManager.Instance.Column; i++)
                {
                    //当前启动模拟器
                    int currTmp = VmManager.Instance.VmIndexArray[VmManager.Instance.RunningGroupIndex, i];
                    if (currTmp != -1)
                    {
                        CheckBox check_mobile = new CheckBox();
                        check_mobile.Text = String.Format(resourceManager.GetString("Phone_Num",cultureInfo), currTmp + 1);
                        check_mobile.Tag = currTmp;
                        check_mobile.Location = new Point(X, Y);
                        panle_importmobiles.Controls.Add(check_mobile);
                        check_mobile.CheckedChanged += Check_mobile_CheckedChanged;
                        Y = Y + 35;
                    }
                }
            }
        }
        /// <summary>
        /// 全选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_allmobile_all_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkbox = (CheckBox)sender;
            if (checkbox.Checked)   //全选
            {
                foreach (Control control in panle_importmobiles.Controls)
                {
                    string s = control.GetType().ToString();
                    if (s == "System.Windows.Forms.CheckBox")
                    {
                        CheckBox ck = (CheckBox)control;
                        if (ck.Tag.ToString() != "-1")
                        {
                            /*if (!checkMobiles.Contains(Int32.Parse(ck.Tag.ToString())))
                            {
                                checkMobiles.Add(Int32.Parse(ck.Tag.ToString()));
                            }*/

                            ck.Checked = true;
                        }
                    }
                }
            }
            else
            {
                foreach (Control control in panle_importmobiles.Controls)
                {
                    string s = control.GetType().ToString();
                    if (s == "System.Windows.Forms.CheckBox")
                    {
                        CheckBox ck = (CheckBox)control;
                        if (ck.Tag.ToString() != "-1")
                        {
                            /*if (checkMobiles.Contains(Int32.Parse(ck.Tag.ToString())))
                            {
                                checkMobiles.Remove(Int32.Parse(ck.Tag.ToString()));
                            }*/

                            ck.Checked = false;
                        }
                    }
                }
            }
            checkMobiles.Sort();            
        }

        /// <summary>
        /// 当选事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Check_mobile_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox b1 = (CheckBox)sender;
            int currcheckbox = Int32.Parse(b1.Tag.ToString());
            if(b1.Checked)
            {
                checkMobiles.Add(currcheckbox);
            }
            else
            {
                checkMobiles.Remove(currcheckbox);
            }
            checkMobiles.Sort();            
        }

        /// <summary>
        /// 打开文件选择框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_selectfile1_Click(object sender, EventArgs e)
        {
            string path = string.Empty;
            OpenFileDialog openfile = new OpenFileDialog();
            openfile.Multiselect = false;
            openfile.Title = resourceManager.GetString("Select_Phone_File",cultureInfo);
            openfile.Filter = resourceManager.GetString("Text_File", cultureInfo);
            if (openfile.ShowDialog() == DialogResult.OK)
            {                 
                txt_filepath1.Text = "";
                txt_filepath1.Text = openfile.FileName;
            }
        }

        /// <summary>
        /// 确定导入通讯录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_importfile_Click(object sender, EventArgs e)
        {
            //移动文件到指定的目录
            if (checkMobiles == null || checkMobiles.Count == 0)
            {
                MessageBox.Show("请选择需要导入的手机");
                return;
            }
            List<string> phoneStr = new List<string>();
            if (txt_textphonenum.Text != "")
            {
                phoneStr = txt_textphonenum.Text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.None).ToList();
                phoneStr = phoneStr.Where(s => !string.IsNullOrEmpty(s)).ToList();
            }
            int flag = 0;
            if (txt_filepath1.Text != "")
            {
                StreamReader sr = new StreamReader(txt_filepath1.Text, Encoding.Default);
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    phoneStr.Add(line.ToString());

                    if (phoneStr.Count > 100000)
                    {
                        flag = -1;
                        break;
                    }
                }
            }
            if (flag == -1)
            {
                MessageBox.Show("对不起，单次最多导入数量不能超过100000个号码，请分批次导入");
                return;
            }

            //插入到任务表中
            int sr1 = 21 / 1;
            int onenums = phoneStr.Count / checkMobiles.Count;
            PhonenumBLL phonebll = new PhonenumBLL();
            //TasksBLL taskbll = new TasksBLL();           

            for (int m = 0; m < checkMobiles.Count; m++)
            {
                int res = 0;
                List<string> strIds = new List<string>();
                if (checkMobiles.Count > 1 && m == checkMobiles.Count - 1)
                {
                    onenums = phoneStr.Count;
                }
                for (int i = 0; i < onenums; i++)
                {
                    Phonenum phone = new Phonenum();
                    phone.PhoneNum = phoneStr[i];
                    phone.SimulatorId = checkMobiles[m];
                    phone.Status = 0;   //待导入

                    int nflag = phonebll.InsertPhoneNum(phone);
                    if (nflag > 0)
                    {
                        res++;
                    }
                    strIds.Add(phone.PhoneNum);
                }
                if (strIds != null && strIds.Count > 0)
                {
                    for (int j = 0; j < strIds.Count; j++)
                        phoneStr.Remove(strIds[j]);
                }
                //号码写入文件
                string filepath = CopyPhoneNumsFile(strIds, checkMobiles[m]);

                var lists = new JArray
                {
                };

                if (filepath!="")
                {
                    lists.Add(filepath);
                    var obj = new JObject() { { "tasktype", 1}, { "txtmsg", "" } };
                    obj.Add("list", lists);
                    //插入任务
                    TaskSch tasks = new TaskSch();
                    tasks.TypeId = 1;
                    tasks.Remotes = checkMobiles[m].ToString();
                    tasks.MobileIndex = checkMobiles[m];
                    tasks.Bodys = obj.ToString(Formatting.None);
                    //tasks.Bodys = JsonConvert.SerializeObject(new string[] { "tasktype:1", "txtmsg:", "filepath:"+ filepath, "nums:"+res}); 
                    tasks.Status = "waiting";
                    tasks.ResultVal = "";
                    tasks.RepeatNums = 1;
                    tasks.RandomMins = 5;
                    tasks.RandomMaxs = 12;
                    TasksBLL.CreateTask(tasks);
                }                
            }
            //启动任务列表        
            if (ConfigVals.IsRunning != 1)
            {
                Thread taskthread = new Thread(ProessTask);
                taskthread.Start();
            }

            MessageBox.Show(string.Format("已添加{0}条任务", checkMobiles.Count));
        }

        /// <summary>
        /// 拷贝号码文件到模拟器
        /// </summary>
        /// <returns></returns>
        public string CopyPhoneNumsFile(List<string> strIds,int mobileIndex)
        {
            string res = "";
            if (strIds != null && strIds.Count > 0)
            {
                string filename = DateTime.Now.ToString("yyyyMMddHHmmssffff");

                string path = $"{Application.StartupPath}/PhoneFiles/{filename}.txt";
                FileStream fs = new FileStream(path, FileMode.Create);
                StreamWriter sw = new StreamWriter(fs);

                for(int i=0; i< strIds.Count;i++)
                {
                    sw.Write(strIds[i]+ "\r\n");
                }                
                //清空缓冲区
                sw.Flush();
                //关闭流
                sw.Close();
                fs.Close();
                if(File.Exists(path))
                {
                    //移动到sd卡
                    string target = $"/sdcard/qunkong/txt/{filename}.txt";
                    ProcessUtils.PushFileToVm(mobileIndex, path, target);

                    res = target;
                }                
            }

            return res;
        }

        public void ProessTask()
        {
            ConfigVals.IsRunning = 1;
            TasksSchedule tasks = new TasksSchedule();
            tasks.ProessTask();
        }

        private void btn_starttask_Click(object sender, EventArgs e)
        {
            if(ConfigVals.IsRunning!=1)
            {
                Thread taskthread = new Thread(ProessTask);
                taskthread.Start();
            }
        }

        /// <summary>
        /// 查询任务
        /// </summary>
        public void getTasksList()
        {
            List<TaskSch> list = TasksBLL.GetTasksList("-1", 2, 1000);
            DataTable dt = new DataTable();
            dt.Columns.Add("编号");
            dt.Columns.Add("任务类型");
            dt.Columns.Add("对应手机");
            dt.Columns.Add("任务参数");
            dt.Columns.Add("任务状态");
            dt.Columns.Add("执行结果");
            dt.Columns.Add("任务时间");

            if (list != null && list.Count() > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    string tmp_typename = "";
                    if (list[i].TypeId == 1)
                    {
                        tmp_typename = "导入通讯录";
                    }
                    else if (list[i].TypeId == 2)
                    {
                        tmp_typename = "文本动态";
                    }
                    else if (list[i].TypeId == 3)
                    {
                        tmp_typename = "图片动态";
                    }
                    else if (list[i].TypeId == 4)
                    {
                        tmp_typename = "图文动态";
                    }
                    else if (list[i].TypeId == 5)
                    {
                        tmp_typename = "文本消息";
                    }
                    else if (list[i].TypeId == 6)
                    {
                        tmp_typename = "图文消息";
                    }
                    else if (list[i].TypeId == 7)
                    {
                        tmp_typename = "图文消息";
                    }
                    else if (list[i].TypeId == 8)
                    {
                        tmp_typename = "视频消息";
                    }
                    else if (list[i].TypeId == 9)
                    {
                        tmp_typename = "视频文字消息";
                    }
                    dr[0] = list[i].Id;
                    dr[1] = tmp_typename;
                    dr[2] = list[i].MobileIndex + "号手机";
                    dr[3] = list[i].Bodys;
                    dr[4] = list[i].Status;
                    dr[5] = list[i].ResultVal != "" ? list[i].ResultVal : "";
                    dr[6] = BaseClass.ConvertToTime(list[i].Created);

                    dt.Rows.Add(dr);
                }
            }
            dg_tasktable.DataSource = dt;
        }

        /// <summary>
        /// 查询任务列表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_querytask_Click(object sender, EventArgs e)
        {
            getTasksList();
        }

        private void btn_flashtask_Click(object sender, EventArgs e)
        {
            getTasksList();
            if (ConfigVals.IsRunning != 1)
            {
                Thread taskthread = new Thread(ProessTask);
                taskthread.Start();
            }
        }

        /// <summary>
        /// 删除任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_deletetask_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 清除所有任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_deletealltask_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("是否删除所有任务，删除后无法恢复.", "确认删除吗?", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TasksBLL.DeleteTasks(-1);
                getTasksList();
            }
        }


        #region 昵称修改部分
        private void tabpage_creategroup_Enter(object sender, EventArgs e)
        {
            btn_addimgnick.Tag = panelPictureThumbnails3;
            btn_deleteimgnick.Tag = panelPictureThumbnails3;
            InitRunningVmsTreeView(wpfTreeView3);
        }

        /// <summary>
        /// 清空文本
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_clearnicktext_Click(object sender, EventArgs e)
        {
            text_nickname.Text = "";
            text_intronick.Text = "";
        }        

        /// <summary>
        /// 添加图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_addimgnick_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            Panel picContainer = btn.Tag as Panel;            

            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Multiselect = true;
            openFileDialog.Title = "请选择图片";
            openFileDialog.Filter = "图片|*.jpg;*.png;*.jpeg;*.bmp;*.gif";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if (picContainer.Controls.Count > 0)
                {
                    picContainer.Controls.Clear();
                    //picContainer.ScrollControlIntoView(picContainer.Controls[0]);
                }

                string[] addedPicPath = openFileDialog.FileNames;

                int startingIndex = picContainer.Controls.Count;
                double total = picContainer.Controls.Count + addedPicPath.Length;
                double columnCapacity = TaskManager.ColumnCapacity;

                int totalRow = (int)Math.Ceiling(total / columnCapacity);

                for (int row = 0; row < totalRow; row++)
                {
                    for (int column = 0; column < TaskManager.ColumnCapacity; column++)
                    {
                        int picturePathIndex = row * TaskManager.ColumnCapacity + column;

                        if (picturePathIndex < total && picturePathIndex >= startingIndex)
                        {

                            Panel panel = new Panel()
                            {
                                Name = $"panel{picturePathIndex}",
                                BackColor = Color.Gray,
                                Width = 300,
                                Height = 300,
                                Location = new Point()
                                {
                                    X = 5 + column * 305,
                                    Y = 5 + row * 305,
                                },
                            };

                            CheckBox checkBox = new CheckBox()
                            {
                                Checked = false,
                                Location = new Point()
                                {
                                    X = 0,
                                    Y = 0,
                                },
                                Width = 30,
                                Height = 30
                            };

                            PictureBox pictureBox = new PictureBox()
                            {
                                BackColor = Color.Gray,
                                Width = 300,
                                Height = 300,
                                ImageLocation = addedPicPath[picturePathIndex - startingIndex],
                                Location = new Point()
                                {
                                    X = 0,
                                    Y = 0,
                                },
                                SizeMode = PictureBoxSizeMode.Zoom,
                            };

                            panel.Controls.Add(checkBox);
                            panel.Controls.Add(pictureBox);

                            picContainer.Controls.Add(panel);
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 删除图片
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_deleteimgnick_Click(object sender, EventArgs e)
        {

        }        

        /// <summary>
        /// 确认修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_updatenickname_Click(object sender, EventArgs e)
        {
            string nickname = text_nickname.Text;
            string intro = text_intronick.Text;

            var lists = new JArray
            {
            };
            var obj = new JObject() { { "tasktype", 7 }, { "txtmsg", string.Format("{0}|{1}", nickname, intro) } };
            obj.Add("list", lists);

            var targets = from item in wpfTreeView3.ItemsSourceData.FirstOrDefault().Children
                          where item.IsChecked
                          select (int)item.Id - 1;

            var pictures = from pic in panelPictureThumbnails3.Controls.Cast<Panel>()
                           select ((PictureBox)pic.Controls[1]).ImageLocation;

            string[] paths = pictures.ToArray();
            if(nickname=="" && intro=="" && paths.Count()==0)
            {
                MessageBox.Show($"没有填写需要修改的内容或图片");
                return;
            }

            string dir = DateTime.Now.ToString("yyyyMMddHHmmssffff");                      

            var MobileIndexArr = targets.ToArray();
            int successnums = 0;
            if (MobileIndexArr.Count()>0)
            {
                foreach(int mobile in MobileIndexArr)
                {
                    lists.Clear();
                    if (paths != null && paths.Count() > 0)
                    {
                        string target = $"/sdcard/qunkong/image/{dir}/{Path.GetFileName(paths[0])}";

                        ProcessUtils.PushFileToVm(mobile, paths[0], target);

                        lists.Add(target);
                    }

                    //插入任务
                    TaskSch tasks = new TaskSch();
                    tasks.TypeId = 3;
                    tasks.Remotes = mobile.ToString();
                    tasks.MobileIndex = mobile;
                    tasks.Bodys = obj.ToString(Formatting.None);
                    //tasks.Bodys = JsonConvert.SerializeObject(new string[] { "tasktype:1", "txtmsg:", "filepath:"+ filepath, "nums:"+res}); 
                    tasks.Status = "waiting";
                    tasks.ResultVal = "";
                    tasks.RepeatNums = 1;
                    tasks.RandomMins = 5;
                    tasks.RandomMaxs = 10;
                    TasksBLL.CreateTask(tasks);

                    successnums++;
                }

                if (ConfigVals.IsRunning != 1)
                {
                    Thread taskthread = new Thread(ProessTask);
                    taskthread.Start();
                }
                MessageBox.Show($"成功添加{successnums}个任务。");

            }
            else
            {
                MessageBox.Show("还未选择需要修改的目标手机.");
            }
        }

        #endregion

        
    }
}
