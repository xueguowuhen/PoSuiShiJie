/****************************************************
    文件：TimerSvc
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-18 15:10:48
	功能：计时服务
*****************************************************/
using System;
using UnityEngine;

public class TimerSvc:MonoBehaviour
{
    public static TimerSvc Instance=null;
    private ShowTimer showTimer;
    public void InitSvc()
    {
        Instance = this;
        showTimer = new ShowTimer();
        showTimer.SetLog((string info) =>
        {
            GameCommon.Log(info);
        });
        GameCommon.Log("Init TimerSvc....");
    }
    public void Update()
    {
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