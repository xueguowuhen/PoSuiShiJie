/****************************************************
    文件：TraPkg
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-12 10:03:20
	功能：数据起始
*****************************************************/


using System;

namespace ComNet
{
    class TraPkg
    {
        public int headLen = 4;
        public byte[] headBuff;//数据被存入
        public int headIndex = 0;//起始索引

        public int bodyLen = 0;
        public byte[] bodyBuff;
        public int bodyIndex = 0;
        public TraPkg()
        {
            headBuff = new byte[4];
        }
        public void InitBodyBuff()
        {
            bodyLen =BitConverter.ToInt32 (headBuff, 0);//将头文件字节转换为整数，用于判断消息字节总长度
            bodyBuff = new byte[bodyLen];
        }
        public void ReSetData()
        {
            headIndex = 0;
            bodyLen = 0;
            bodyBuff=null;
            bodyIndex = 0;
        }

    }
}
