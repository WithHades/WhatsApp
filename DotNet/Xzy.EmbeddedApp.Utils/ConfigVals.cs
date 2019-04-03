namespace Xzy.EmbeddedApp.Utils
{
    public static class ConfigVals
    {
        /// <summary>
        /// 每组设备数
        /// </summary>
        public static int GroupNums { get; set; }  //

        /// <summary>
        /// 每行设备数
        /// </summary>
        public static int RowNums { get; set; }

        /// <summary>
        /// 当前语言包
        /// </summary>
        public static int Lang { get; set; }

        /// <summary>
        /// 是否有设备启动中
        /// </summary>
        public static int IsRunning { get; set; } = 0;

        /// <summary>
        /// 最大授权数
        /// </summary>
        public static int MaxNums { get; set; } = 100;

        /// <summary>
        /// 执行中的任务
        /// </summary>
        public static int RuningTask { get; set; } = 0;

        public static string AccessKey { get; } = "ab7345d6094a0af9bcca4fcb3371c007";
    }
}
