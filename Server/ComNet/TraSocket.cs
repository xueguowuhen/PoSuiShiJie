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
                T session = new T();
                sessionLst.Add(session);//添加一个客户端
                session.StartRcvData(clientSkt, () =>
                {
                    if (sessionLst.Contains(session))//判断客户端列表中是否存在该客户端
                    {
                        sessionLst.Remove(session);
                    }
                });
                //// 初始化心跳检测线程
                //heartbeatThread = new Thread(session.BeatTimer);
                //heartbeatThread.Start();
            }
            catch (Exception ex)
            {
                TraTool.LogMsg(ex.Message, LogLevel.Error);
            }
            skt.BeginAccept(new AsyncCallback(ClientConnect), skt);
        }
        #endregion
        #region 客户端接收
        public void StartAsClient(string ip, int port)
        {
            try
            {
                skt.BeginConnect(new IPEndPoint(IPAddress.Parse(ip), port), new AsyncCallback(ServerConnect), skt);//绑定ip和端口
                TraTool.LogMsg("客户端启动成功\n正在连接服务器……", LogLevel.Info);
            }
            catch (Exception ex)
            {
                TraTool.LogMsg(ex.Message, LogLevel.Error);
            }
        }
        public void ServerConnect(IAsyncResult async)
        {
            try
            {
                skt.EndConnect(async);
                session = new T();
                session.StartRcvData(skt, null);
            }
            catch (Exception ex)
            {
                TraTool.LogMsg(ex.Message, LogLevel.Error);
            }
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
        public void SendHeartbeat(K msg)
        {
            if (sessionLst.Count != 0)
            {

                // 遍历所有客户端会话，发送心跳消息
                foreach (var session in sessionLst)
                {
                    session.SendMsg(msg);
                }
                TraTool.LogMsg("已发送心跳包.");
            }
        }
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
