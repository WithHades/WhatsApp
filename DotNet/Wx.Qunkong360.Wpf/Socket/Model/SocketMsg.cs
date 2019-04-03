using ProtoBuf;

namespace Xzy.EmbeddedApp.WinForm.Socket.Model
{
    [ProtoContract]
    public class SocketMsg
    {
        [ProtoMember(1)]
        public SocketCase action { get; set; }
        [ProtoMember(2)]
        public string context { get; set; }
        [ProtoMember(3)]
        public int tasknum { get; set; }
    } 

}
