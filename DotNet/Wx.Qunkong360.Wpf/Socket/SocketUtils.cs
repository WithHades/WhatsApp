using System.Text;
using Xzy.Sockets;

namespace Xzy.EmbeddedApp.WinForm.Socket
{
    public class SocketUtils
    {
        public static void Send<T>(TcpSocketSession client, T t)
        {
            client.Send(ProtoBufHelper.ObjectToBytes<T>(t));
        }

        public static void SendJson(TcpSocketSession client, string t)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(t);
            client.Send(buffer);
        }
    }
}
