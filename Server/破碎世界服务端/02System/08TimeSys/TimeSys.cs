/****************************************************
    文件：TimeSys
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-11-02 16:06:00
	功能：Nothing
*****************************************************/
using CommonNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
public class TimeSys
{
    private static TimeSys instance = null;
    public static TimeSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TimeSys();
            }
            return instance;
        }
    }
    private CacheSvc cacheSvc;
    private CfgSvc cfgSvc;
    private TimerSvc timerSvc;
    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        cfgSvc = CfgSvc.Instance;
        timerSvc = TimerSvc.Instance;
        GameCommon.Log("TimeSys Init Done");
    }
    /// <summary>
    /// 处理客户端请求系统时间
    /// </summary>
    /// <param name="pack"></param>
    public void ReqSystemTimeMessage(MsgPack pack)
    {
        ReqSystemTimeMessage reqSystemTime = pack.gameMsg.reqSystemTimeMessage;
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.RspSystemTimeMessage,
            rspSystemTimeMessage = new RspSystemTimeMessage()
            {
                LocalTime = reqSystemTime.LocalTime,
                ServerTime = timerSvc.GetTimestamp(),
            }
        };
        //long timestamp = timerSvc.GetTimestamp(); // 获取时间戳
        //DateTime serverDateTime = DateTimeOffset.FromUnixTimeMilliseconds(timestamp).DateTime; // 使用 FromUnixTimeMilliseconds
        //string formattedDate = serverDateTime.ToString("yyyy-MM-dd HH:mm:ss");
        //GameCommon.Log("ReqSystemTimeMessage " + formattedDate);

        pack.session.SendMsg(gameMsg);
    }
}