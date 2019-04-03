using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Xzy.EmbeddedApp.Model
{
    public class Tasks
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 任务类型
        /// </summary>
        public int TypeId { get; set; }

        /// <summary>
        /// 客户端标识
        /// </summary>
        public string Remotes { get; set; }

        /// <summary>
        /// 手机编号
        /// </summary>
        public int MobileIndex { get; set; }

        /// <summary>
        /// 参数主体json
        /// </summary>
        public string Bodys { get; set; }

        /// <summary>
        /// 执行状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 执行结果
        /// </summary>
        public string ResultVal { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public int Created { get; set; }
    }
}
