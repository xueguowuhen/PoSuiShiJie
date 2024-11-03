/****************************************************
    文件：GameCommon
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-14 9:29:20
	功能：客户端服务端共用工具类
*****************************************************/

using ComNet;

public enum ComLogType
{
    Log = 0,
    Warn = 1,
    Error = 2,
    Info = 3,
}
public class GameCommon
{
    /// <summary>
    /// 封装函数log
    /// </summary>
    /// <param name="msg"></param>
    /// <param name="tp"></param>
    public static void Log(string msg = "", ComLogType tp = ComLogType.Log)
    {
        LogLevel lv = (LogLevel)tp;
        TraTool.LogMsg(msg, lv);
    }
    
}