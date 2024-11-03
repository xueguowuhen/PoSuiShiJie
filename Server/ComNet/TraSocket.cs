/****************************************************
    文件：ServerSocket
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-11 23:18:47
	功能：Nothing
*****************************************************/
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ComNet
{
    public class TraSocket<T, K>
        where T : TraSession<K>, new()
        where K : TraMsg
    {
        private Socket skt = null;
        public int backlog = 0;
        public T session = null;
        List<T> sessionLst = new List<T>();
        private Dictionary<int, T> sessionDict = new Dictionary<int, T>(); // 用于存储 sessionID 和会话的映射

        // 声明一个定时器
        private Thread heartbeatThread;
        public TraSocket()
        {
            skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);//创建Socket

        }
        #region 服务端接收
        /// <summary>
        /// 服务端绑定
        /// </summary>
        /// <param name="ip"></param>
        /// <param name="port"></param>
        public void StartAsServer(string ip, int port)
        {
            try
            {

                skt.Bind(new IPEndPoint(IPAddress.Parse(ip), port));//绑定ip和端口
                skt.Listen(backlog);//监听
                skt.BeginAccept(new AsyncCallback(ClientConnect), skt);
                TraTool.LogMsg("服务器启动成功\n等待连接中……", LogLevel.Info);
            }
            catch (Exception ex)
            {
                TraTool.LogMsg(ex.Message, LogLevel.Error);
            }

        }
        /// <summary>
        /// 客户端异步连接
        /// </summary>
        public void ClientConnect(IAsyncResult async)
        {
            try
            {
                Socket clientSkt = skt.EndAccept(async);
                // 假设通过一个初始消息接收 sessionID
                int sessionID = ReceiveSessionId(clientSkt); // 获取 ID 的方法
                if (sessionID != -1)//重连
                {
                    if (sessionDict.TryGetValue(sessionID, out var existingSession))
                    {
                        // 是重连，更新现有会话的 Socket
                        existingSession.UpdateSocket(clientSkt); // 更新 Socket
                        TraTool.LogMsg($"客户端重连，sessionID: {sessionID}", LogLevel.Info);
                    }
                    else
                    {
                        // 处理未知 sessionID
                        TraTool.LogMsg($"未知的 sessionID: {sessionID}", LogLevel.Warn);
                    }
                }
                else//新连接
                {
                    T session = new T();
                    sessionLst.Add(session);//添加一个客户端
                    session.StartRcvData(clientSkt, () =>
                    {
                        if (sessionLst.Contains(session))//判断客户端列表中是否存在该客户端
                        {
                            sessionLst.Remove(session);
                        }
                    });

                }
            }
            catch (Exception ex)
            {
                TraTool.LogMsg(ex.Message, LogLevel.Error);
            }
            skt.BeginAccept(new AsyncCallback(ClientConnect), skt);
        }
        private int ReceiveSessionId(Socket clientSkt)
        {
            byte[] buffer = new byte[4]; // 假设 ID 是一个 4 字节的整数
            clientSkt.Receive(buffer);
            return BitConverter.ToInt32(buffer, 0);
        }
        public void AddSession(int sessionID, T session)
        {
            sessionDict.Add(sessionID, session);
        }
        #endregion
        #region 客户端接收
        public void StartAsClient(string ip, int port, int sessionID, bool isReconnect = false)
        {
            try
            {
                if (skt != null && skt.Connected)
                {
                    TraTool.LogMsg("当前 Socket 仍然连接，无法重连。", LogLevel.Warn);
                    return;
                }

                // 如果当前 Socket 已断开或被释放，重新初始化
                if (skt == null || !skt.Connected)
                {
                    skt = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                }
                skt.BeginConnect(new IPEndPoint(IPAddress.Parse(ip), port), asyncResult => ServerConnect(asyncResult, sessionID, isReconnect), skt);
                if (isReconnect)
                {
                    TraTool.LogMsg("正在尝试重连服务器...", LogLevel.Info);
                }
                else
                {
                    TraTool.LogMsg("客户端启动成功\n正在连接服务器……", LogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                TraTool.LogMsg(ex.Message, LogLevel.Error);
            }
        }
        public void ServerConnect(IAsyncResult async, int sessionID, bool isReconnect)
        {
            try
            {
                skt.EndConnect(async);
                session = new T();
                // 连接成功后发送客户端 ID
                SendClientId(sessionID);
                session.StartRcvData(skt, null);
                if (isReconnect)
                {
                    TraTool.LogMsg("重连服务器成功！", LogLevel.Info);
                }
                else
                {
                    TraTool.LogMsg("成功连接到服务器！", LogLevel.Info);
                }
            }
            catch (Exception ex)
            {
                TraTool.LogMsg(ex.Message, LogLevel.Error);
            }
        }
        private void SendClientId(int sessionID)
        {
            // 发送 ID
            byte[] idData = BitConverter.GetBytes(sessionID);
            skt.Send(idData);
        }
        public void Close()
        {
            if (skt != null)
            {
                skt.Close();
            }
        }
        public List<T> GetSesstionLst()
        {
            return sessionLst;
        }
        #endregion
        //发送心跳消息
        //public void SendHeartbeat(K msg)
        //{
        //    if (sessionLst.Count != 0)
        //    {

        //        // 遍历所有客户端会话，发送心跳消息
        //        foreach (var session in sessionLst)
        //        {
        //            session.SendMsg(msg);
        //        }
        //        TraTool.LogMsg("已发送心跳包.");
        //    }
        //}
        public void SetLog(bool log = true, Action<string, int> logCB = null)
        {
            if (log == false)
            {
                TraTool.log = false;
            }
            if (logCB != null)
            {
                TraTool.logCB = logCB;
            }

        }
    }

}
