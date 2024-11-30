/****************************************************
    文件：TimerSvc
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-18 15:10:48
	功能：计时服务
*****************************************************/
using CommonNet;
using System;
using UnityEngine;

public class TimerSvc: SvcBase<TimerSvc>
{
    private ShowTimer showTimer;

    #region 系统时间函数
    public long ServerTime = 0;
    public float CheckServerTime = 0;
    public int PingValue = 0;
    /// <summary>
    /// 获取当前服务器时间Socket
    /// </summary>
    /// <returns></returns>
    public long GetCurrServerTime()
    {

        long elapsedMilliseconds = (long)((Time.realtimeSinceStartup - CheckServerTime) * 1000);
        return elapsedMilliseconds + ServerTime;
    }
    #endregion
    public override void InitSvc()
    {
        base.InitSvc();
        showTimer = new ShowTimer();
        showTimer.SetLog((string info) =>
        {
            GameCommon.Log(info);
        });
        SocketDispatcher.Instance.AddEventListener(CommonNet.CMD.RspSystemTimeMessage, OnRspSystemTimeMessage);
        GameCommon.Log("Init TimerSvc....");
    }
    /// <summary>
    /// 发送本地时间
    /// </summary>
    public void SendLocalTime()
    {
        CheckServerTime = Time.realtimeSinceStartup;

        GameMsg msg=(new GameMsg
        {
            cmd = (int)CMD.ReqSystemTimeMessage,
            reqSystemTimeMessage = new ReqSystemTimeMessage
            {
                LocalTime =CheckServerTime * 1000,
            }
        }
        );
        NetSvc.Instance.SendMsg(msg);
    }
    private void OnRspSystemTimeMessage(GameMsg msg)
    {
        RspSystemTimeMessage rspSystemTime = msg.rspSystemTimeMessage;
        float localTime = rspSystemTime.LocalTime;
        long serverTime = rspSystemTime.ServerTime;
        //（客户端当前时刻-客户端发送时刻）获取到一趟往返的延迟/2即可得到单趟延迟
        PingValue = (int)((Time.realtimeSinceStartup * 1000 - localTime) * 0.5);//ping值0.5
        //(服务器时间-单趟延迟时间)服务器发送时间已经是客户端发送时间+延迟时间的总和，减去延迟时间，才能得到正确的客户端发送时刻的服务器时间
        ServerTime = serverTime -PingValue;//客户端计算出来的服务器时间
        DateTime serverDateTime = DateTimeOffset.FromUnixTimeMilliseconds(GetCurrServerTime()).DateTime; // 使用 FromUnixTimeMilliseconds
        string formattedDate = serverDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        GameCommon.Log("服务器时间:" + formattedDate + ", 延迟时间:" +(PingValue));
    }

    public override void OnUpdate()
    {
        base.OnUpdate();
        showTimer.Update();
    }
    public int AddTimeTask(Action<int> callback, double delay, TimeUnit timeUnit = TimeUnit.Millisecond, int count = 1)
    {
        return showTimer.AddTimeTask(callback, delay, timeUnit, count);
    }
    public void DeleteTimeTask(int tid)
    {
         showTimer.DeleteTimeTask(tid);
    }
    public double GetNwTime()
    {
        return showTimer.GetMillisecondsTime();
    }
}