namespace Xzy.EmbeddedApp.WinForm.Socket.Model
{
    public class SockMsgJson
    {
        /// <summary>
        /// 动作类型
        /// </summary>
        public SocketCase action { get; set; }

        /// <summary>
        /// 任务编号
        /// </summary>
        public int tasknum { get; set; }

        /// <summary>
        /// 消息内容  {"action":0,"tasknum":0,"context":"865166022988367"}
        /// </summary>
        public string context { get; set; }
    }
}
