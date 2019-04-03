using System;
using Xzy.EmbeddedApp.Model;
using Xzy.EmbeddedApp.WinForm.Abstract;

namespace Xzy.EmbeddedApp.WinForm.Implementation
{
    public class TaskManager : ITaskManager
    {
        public string Message { get; private set; }

        public string[] Paths { get; private set; }

        public int[] MobileIndexs { get; private set; }

        public static byte ColumnCapacity = 4;

        public TaskType TaskType { get; private set; }

        public TaskManager(TaskType taskType, string message, string[] paths, int[] mobileIndexs)
        {
            if (mobileIndexs == null || mobileIndexs.Length == 0)
            {
                throw new ArgumentNullException(nameof(mobileIndexs), "目标虚拟器为空！");
            }

            if (string.IsNullOrEmpty(message) && (paths == null || paths.Length == 0))
            {
                throw new ArgumentNullException(nameof(message) + nameof(paths), "任务内容为空！");
            }


            switch (taskType)
            {
                case TaskType.ImportContacts:
                    break;
                case TaskType.PostMessage:
                case TaskType.SendMessage:


                    if (string.IsNullOrEmpty(message))
                    {
                        throw new ArgumentNullException(nameof(message), "文本消息为空！");
                    }

                    break;
                case TaskType.PostPicture:
                case TaskType.SendPicture:


                    if (paths == null || paths.Length == 0)
                    {
                        throw new ArgumentNullException(nameof(paths), "图片集合为空！");
                    }

                    break;
                case TaskType.PostMessageAndPicture:
                case TaskType.SendMessageAndPicture:

                    if (string.IsNullOrEmpty(message))
                    {
                        throw new ArgumentNullException(nameof(message), "文本消息为空！");
                    }

                    if (paths == null || paths.Length == 0)
                    {
                        throw new ArgumentNullException(nameof(paths), "图片集合为空！");
                    }


                    break;
                case TaskType.SendVideo:
                    if (paths == null || paths.Length == 0)
                    {
                        throw new ArgumentNullException(nameof(paths), "视频集合为空！");
                    }
                    break;
                case TaskType.SendMessageAndVideo:
                    if (string.IsNullOrEmpty(message))
                    {
                        throw new ArgumentNullException(nameof(message), "文本消息为空！");
                    }

                    if (paths == null || paths.Length == 0)
                    {
                        throw new ArgumentNullException(nameof(paths), "视频集合为空！");
                    }
                    break;

                case TaskType.SendAudio:
                    break;
                default:
                    throw new ArgumentNullException(nameof(taskType), "未知的任务类型！");
            }



            TaskType = taskType;
            Message = message;
            Paths = paths;
            MobileIndexs = mobileIndexs;
        }
    }
}
