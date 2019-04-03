using System;
using System.Collections.Generic;
using System.Threading;
using Cj.EmbeddedAPP.BLL;
using Xzy.EmbeddedApp.Model;
using Xzy.EmbeddedApp.Utils;
using Xzy.EmbeddedApp.WinForm.Socket;

namespace Xzy.EmbeddedApp.WinForm.Tasks
{
    public class TasksSchedule
    {
        /// <summary>
        /// 任务处理
        /// </summary>
        /// <returns></returns>
        public int ProessTask()
        {
            int res = 0;
            //TasksBLL tasksbll = new TasksBLL();
            int resnum = TasksBLL.CountByStatus();
            int intval = 0;
            Random r = new Random();
            while (resnum > 0)
            {
                intval++;
                List<TaskSch> list = TasksBLL.GetTasksList("waiting",1);
                if (list != null && list.Count > 0)
                {
                    foreach(var item in list)
                    {
                        string mobileIndex = "";
                        mobileIndex = item.MobileIndex.ToString();  //需要匹配出对应的客户端标识

                        int flag = 0;
                        for (int i = 0; i < item.RepeatNums; i++)
                        {
                            flag = SocketServer.SendTaskInstruct(Int32.Parse(mobileIndex), item.TypeId, item.Id, item.Bodys);
                            int waittime = r.Next(item.RandomMins, item.RandomMaxs);
                            Thread.Sleep(waittime);
                        }
                        
                        TasksBLL.UpdateTaskStatus(item.Id, flag);
                        resnum--;
                    }
                }
                if(resnum<=0)
                {
                    resnum = TasksBLL.CountByStatus();
                }
            }
            ConfigVals.IsRunning = 0;
            return res;
        }

        /// <summary>
        /// 更新任务执行结果
        /// </summary>
        /// <param name="tasknum"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public int UpdateTaskResval(int tasknum,string status)
        {
            int flag = TasksBLL.UpdateTaskRes(tasknum, status);
            return flag;
        }

        /// <summary>
        /// 更新号码发送结果
        /// </summary>
        /// <param name="phone"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public int UpdatePhoneStatus(int phone,int response)
        {
            int res = PhonenumBLL.updatePhoneStatus(phone, response);
            return res;
        }
    }
}
