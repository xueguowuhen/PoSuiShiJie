/****************************************************
    文件：TraMsg
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-11 23:25:48
	功能：Nothing
*****************************************************/

namespace ComNet
{

    public abstract class TraMsg
    {
        public int seq;//序列
        public int cmd;//协议
        public int err;//报错
        public string beat;
    }
}
