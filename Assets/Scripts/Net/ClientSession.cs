/****************************************************
    文件：ClientSession
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-14 9:23:54
	功能：客户端网络会话
*****************************************************/
using CommonNet;
using ComNet;
using System;
public class ClientSession : TraSession<GameMsg>
{
    private int BeatTimes = 15;
    private int tid;
    protected override void OnConnected()
    {
        GameCommon.Log("连接成功");

        tid = TimerSvc.Instance.AddTimeTask((int tid) =>
         {
             //心跳机制
             SendMsg(new GameMsg
             {
                 beat = "heartbeat",
             });
         }, BeatTimes, TimeUnit.Second, 0);
    }
    protected override void OnReciveMsg(GameMsg msg)
    {
        GameCommon.Log("RcvPack CMD:" + ((CMD)msg.cmd).ToString());
        NetSvc.instance.AddMsgQue(msg);
    }
    protected override void OnDisConnected()
    {
        GameCommon.Log("服务器断开连接");
        TimerSvc.Instance.DeleteTimeTask(tid);
    }
}

