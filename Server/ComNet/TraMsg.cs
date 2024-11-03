/****************************************************
    文件：TraMsg
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-11 23:25:48
	功能：Nothing
*****************************************************/

using CommonNet;
using ProtoBuf;

namespace ComNet
{

    [ProtoContract]
    [ProtoInclude(1, typeof(GameMsg))]
    public abstract class TraMsg
    {
        [ProtoMember(2)]
        public int seq; // 序列

        [ProtoMember(3)]
        public int cmd; // 协议

        [ProtoMember(4)]
        public int err; // 报错

        [ProtoMember(5)]
        public string beat; // 心跳
    }

}
