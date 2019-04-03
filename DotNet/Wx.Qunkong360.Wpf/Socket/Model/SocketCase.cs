namespace Xzy.EmbeddedApp.WinForm.Socket.Model
{
    public enum SocketCase
    {
        /// <summary>
        /// 客户端初始化用户名，imei号
        /// </summary>
        Init = 0,
        /// <summary>
        /// 心跳包
        /// </summary>
        Heartbeat = 1,
        /// <summary>
        /// 客户端发送来的消息
        /// </summary>
        ClientMsg = 2,
        /// <summary>
        /// 给客户端发送消息
        /// </summary>
        ServerMsg=3,

        /// <summary>
        /// 客户端更新任务状态
        /// </summary>
        ClientTask=4,
        /// <summary>
        /// 客户端更新手机状态
        /// </summary>
        ClientPhone=5,
        /// <summary>
        /// 客户端上报手机号码
        /// </summary>
        ClientInitPhone=6
        

    }
}
