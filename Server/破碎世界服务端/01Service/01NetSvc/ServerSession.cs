/****************************************************
    文件：ServerSession
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-15 21:11:43
	功能：Nothing
*****************************************************/
using CommonNet;
using ComNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 墨染服务端._01Service._01NetSvc
{
    public class ServerSession : TraSession<GameMsg>
    {
        public int tid;
        protected override void OnConnected()
        {
            if (sessionID == -1)
            {
                sessionID = ServerRoot.Instance.GetSessionID();
                NetSvc.Instance.AddSession(sessionID, this);
            }
            //NetSvc.Instance.ClearQueue();
            GameCommon.Log("连接成功.");
            TimerSvc.Instance.DeleteTimeTask(tid);
            //建立心跳包发送
            tid = TimerSvc.Instance.AddTimeTask((int tid) =>
             {
                 if (BeatTime >= 0)
                 {
                     BeatTimer();
                 }
             }, 1, TimeUnit.Second, 0);
            SendMsg(new GameMsg()
            {
                cmd = (int)CMD.SystemSessionID,
                systemSessionID = new SystemSessionID()
                {
                    SessionID = sessionID
                }
            });
        }

        protected override void OnReciveMsg(GameMsg msg)
        {
            if (msg.beat != null)
            {

                GameCommon.Log("SessionID:" + sessionID + "接收到该心跳包");
            }
            else
            {

                GameCommon.Log("SessionID:" + sessionID + "RcvPack CMD:" + ((CMD)msg.cmd).ToString());
                //TraTool.LogMsg("Server Response:" + msg.cmd);
                NetSvc.Instance.AddMsgQue(this, msg);
            }
        }

        protected override void OnDisConnected()
        {
            LoginSys.Instance.ClearOfflineData(this);
            TimerSvc.Instance.DeleteTimeTask(tid);
            GameCommon.Log("客户端关闭连接.");
        }
    }
}
