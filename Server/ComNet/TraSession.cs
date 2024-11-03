/****************************************************
    文件：TraSession
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-11 23:43:41
	功能：Nothing
*****************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;


namespace ComNet
{
    public abstract class TraSession<T> where T : TraMsg
    {
        private Socket socket;
        private Action closeAC;
        private int DownSeconds = 20;
        private bool isConnected = false;
        public int BeatTime = -1;
        public int sessionID = -1;
        private const int HoldTimeInSeconds = 180; // 持有连接的时间（3分钟）
        private CancellationTokenSource cancellationTokenSource;

        private readonly string Lock = "lock";
        #region 接收消息
        public void StartRcvData(Socket socket, Action action)
        {
            try
            {
                this.socket = socket;
                this.closeAC = action;
                BeatTime = DownSeconds;
                isConnected=true;
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
        // 更新现有会话的 Socket
        public void UpdateSocket(Socket newSocket)
        {
            // 关闭当前 Socket 的连接
            socket?.Close();
            // 取消持有连接的任务
            cancellationTokenSource?.Cancel();
            // 更新为新 Socket
            this.socket = newSocket;

            // 重新开始接收数据
            StartRcvData(newSocket, closeAC);
        }
        private void RcvHeadData(IAsyncResult async)
        {
            try
            {
                if (async.AsyncState == null)
                {
                    //  OnDisConnected();
                    Clear();
                    return;
                }
                TraPkg traPkg = (TraPkg)async.AsyncState;
                if (socket == null || socket.Connected == false)//获取缓冲区中的可用字节数
                {
                    //  OnDisConnected();
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
                    // OnDisConnected();
                    Clear();
                }
            }
            catch (Exception ex)
            {
                if (ex is ObjectDisposedException)
                {
                    //  OnDisConnected();
                    Clear();
                }
                else
                {
                    Clear();
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
                    //  OnDisConnected();
                    Clear();
                }
            }
            catch (Exception e)
            {
                if (e is ObjectDisposedException)
                {
                    // OnDisConnected();
                    Clear();
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
                if (isConnected==false) return;
                BeatTime--;
                if (BeatTime <= 0)
                {

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
            //socket.Shutdown(SocketShutdown.Both);
            if (socket != null && socket.Connected)
            {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            Clear();
        }
        public void SendMsg(T msg)
        {
            byte[] data = TraTool.PackLenInfo(TraTool.Serialize(msg));//获取数据二进制后设置包头的数据
            SendMsg(data);
        }
        public async void SendMsgAsync(T msg)
        {
            byte[] data = TraTool.PackLenInfo(TraTool.Serialize(msg));

            // 调用异步发送方法
            await Task.Run(() => SendMsg(data));
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
            isConnected = false;
            // 如果已经连接的客户端
            if (closeAC != null)
            {
                TraTool.LogMsg("客户端已断开连接,等待重连中...");
                cancellationTokenSource?.Cancel(); // 取消现有的持有连接任务
                cancellationTokenSource = new CancellationTokenSource();
                Task.Run(() => HoldConnection(cancellationTokenSource.Token)); // 启动持有连接的任务
            }
            else
            {
                OnDisConnected(); // 超时关闭连接
            }
            socket?.Close(); // 确保 Socket 被关闭

        }
        private async Task HoldConnection(CancellationToken cancellationToken)
        {
            // 等待持有时间
            try
            {
                await Task.Delay(HoldTimeInSeconds * 1000, cancellationToken);
            }
            catch (TaskCanceledException)
            {
                // 任务被取消，不执行后续逻辑
                return;
            }

            if (!IsConnectionValid())
            {
                Close(); // 关闭无效连接
            }
        }

        private bool IsConnectionValid()
        {
            try
            {
                // 使用 Poll 方法检查连接状态
                if (socket == null || !socket.Connected)
                {
                    return false; // Socket 未连接
                }

                // 使用 Send 方法发送 0 字节的数据，检查连接状态
                // Poll 方法检查连接是否可读并且没有可用数据
                return !(socket.Poll(1, SelectMode.SelectRead) && socket.Available == 0);
            }
            catch (SocketException)
            {
                return false; // 发生异常，连接无效
            }
            catch (ObjectDisposedException)
            {
                return false; // Socket 被释放，连接无效
            }
        }
        public void Close()
        {
            // 取消持有连接的任务
            cancellationTokenSource?.Cancel();
            OnDisConnected();//超时关闭连接
            if (closeAC != null)
            {
                closeAC();
                closeAC = null;
            }
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
