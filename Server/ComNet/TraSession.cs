/****************************************************
    文件：TraSession
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-11 23:43:41
	功能：Nothing
*****************************************************/

using System;
using System.Net.Sockets;


namespace ComNet
{
    public abstract class TraSession<T> where T : TraMsg
    {
        private Socket socket;
        private Action closeAC;
        private int DownSeconds = 20;
        public int BeatTime = -1;
        private readonly string Lock = "lock";
        #region 接收消息
        public void StartRcvData(Socket socket, Action action)
        {
            try
            {
                this.socket = socket;
                this.closeAC = action;
                BeatTime = DownSeconds;
                OnConnected();
                TraPkg traPkg = new TraPkg();
                socket.BeginReceive//异步接收处理消息
                    (
                     traPkg.headBuff,
                     0,
                    traPkg.headLen,
                    SocketFlags.None,
                    new AsyncCallback(RcvHeadData),
                    traPkg
                    );

            }
            catch (Exception ex)
            {
                TraTool.LogMsg("开始接收消息:" + ex.Message, LogLevel.Error);
            }
        }
        private void RcvHeadData(IAsyncResult async)
        {
            try
            {
                if (async.AsyncState == null)
                {
                    OnDisConnected();
                    Clear();
                    return;
                }
                TraPkg traPkg = (TraPkg)async.AsyncState;
                if (socket == null || socket.Available == 0)//获取缓冲区中的可用字节数
                {
                    OnDisConnected();
                    Clear();
                    return;
                }
                int len = socket.EndReceive(async);//结束接受获取接收的字节长度
                if (len > 0)//长度大于0,才进行数据处理
                {
                    traPkg.headIndex += len;
                    if (traPkg.headIndex < traPkg.headLen)//剩余数据不大于一个头长度时
                    {
                        socket.BeginReceive(//将数据保存，异步等待下一条数据
                            traPkg.headBuff,
                            traPkg.headIndex,
                            traPkg.headLen - traPkg.headIndex,
                            SocketFlags.None,
                            new AsyncCallback(RcvHeadData),//进行回调
                            traPkg//数据接收满后
                            );
                    }
                    else
                    {
                        traPkg.InitBodyBuff();
                        socket.BeginReceive(
                            traPkg.bodyBuff,//接收消息
                            0,
                            traPkg.bodyLen,//接收消息长度
                            SocketFlags.None,
                            new AsyncCallback(RcvBodyData),
                            traPkg//
                            );
                    }
                }
                else
                {
                    OnDisConnected();
                    Clear();
                }
            }
            catch (Exception ex)
            {
                if (ex is ObjectDisposedException)
                {
                    OnDisConnected();
                }
                else
                {
                    TraTool.LogMsg("数据接收报错:" + ex.Message, LogLevel.Error);
                }
            }
        }
        private void RcvBodyData(IAsyncResult ar)
        {
            try
            {
                if (ar.AsyncState != null && socket != null)
                {
                    TraPkg pkg = (TraPkg)ar.AsyncState;
                    int len = socket.EndReceive(ar);//获取数据长度
                    if (len > 0)
                    {
                        pkg.bodyIndex += len;
                        if (pkg.bodyIndex < pkg.bodyLen)//判断当前读取到索引小于长度，则继续等待获取直到获取完整数据
                        {
                            socket.BeginReceive(pkg.bodyBuff,
                                pkg.bodyIndex,
                                pkg.bodyLen,
                                SocketFlags.None,
                                new AsyncCallback(RcvBodyData),
                                pkg);
                        }
                        else
                        {
                            T msg = TraTool.DeSerialize<T>(pkg.bodyBuff);//将数据反序列化
                            if (msg.beat != null)//这是一个心跳包
                            {
                                lock (Lock)
                                {
                                    BeatTime = DownSeconds;
                                }
                            }
                            OnReciveMsg(msg);//进行输出数据
                            pkg.ReSetData();//数据重置
                            socket.BeginReceive(//重新接收头消息
                                pkg.headBuff,
                                0,
                                pkg.headLen,
                                SocketFlags.None,
                                new AsyncCallback(RcvHeadData),
                                pkg);
                        }
                    }
                }
                else
                {
                    OnDisConnected();
                    Clear();
                }
            }
            catch (Exception e)
            {
                if (e is ObjectDisposedException)
                {
                    OnDisConnected();
                }
                else
                {

                    TraTool.LogMsg("接收内容错误:" + e.Message, LogLevel.Error);
                }
            }
        }

        /// <summary>
        /// 检测心跳时间
        /// </summary>
        public void BeatTimer()
        {
            
            lock (Lock)
            {
                if (closeAC == null) return;
                BeatTime--;
                if (BeatTime <= 0)
                {
                    OnDisConnected();
                    Clear();
                    return;

                }
            }
        }
        #endregion
        #region 发送消息
        //释放连接
        public void Shutdown()
        {
            socket.Shutdown(SocketShutdown.Both);


            Clear();
        }
        public void SendMsg(T msg)
        {
            byte[] data = TraTool.PackLenInfo(TraTool.Serialize(msg));//获取数据二进制后设置包头的数据
            SendMsg(data);
        }
        public void SendMsg(byte[] data)
        {
            NetworkStream ns = null;
            try
            {
                if (socket != null)
                {
                    ns = new NetworkStream(socket);
                    if (ns.CanWrite)//检测ns是否可写入数据
                    {
                        ns.BeginWrite(//异步写入数据，写入完成后回调
                            data,//写入的数据
                            0,//起始位置
                            data.Length,//写入长度
                            new AsyncCallback(SendAC),
                            ns
                            );
                    }
                }
            }
            catch (Exception ex)
            {
                TraTool.LogMsg("发送失败:" + ex.Message, LogLevel.Error);
            }
        }
        private void SendAC(IAsyncResult async)
        {
            try
            {
                if (async.AsyncState != null)
                {
                    NetworkStream ns = (NetworkStream)async.AsyncState;//获取异步操作的状态对象
                    ns.EndWrite(async);//结束异步写入
                    ns.Flush();//刷新流，将缓冲区的数据发送给客户端
                    ns.Close();
                }
            }
            catch (Exception ex)
            {
                TraTool.LogMsg("发送失败:" + ex.Message, LogLevel.Error);
            }
        }
        #endregion
        private void Clear()
        {
            if (closeAC != null)
            {
                closeAC();
                closeAC = null;
            }
            if (socket != null)
                socket.Close();
        }
        /// <summary>
        /// 建立连接
        /// </summary>
        protected virtual void OnConnected()
        {
            TraTool.LogMsg("连接成功.", LogLevel.Info);
        }
        protected virtual void OnReciveMsg(T msg)
        {
            TraTool.LogMsg("接收消息", LogLevel.Info);
        }
        /// <summary>
        /// 断开连接
        /// </summary>
        protected virtual void OnDisConnected()
        {
            TraTool.LogMsg("连接结束", LogLevel.Info);
        }
    }
}
