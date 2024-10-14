/****************************************************
    文件：CacheSvc
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-16 20:31:38
	功能：数据缓存层
*****************************************************/
using CommonNet;
using ComNet;
using MySqlX.XDevAPI;
using System.Collections.Generic;
using 墨染服务端._01Service._01NetSvc;

public class CacheSvc
{
    private static CacheSvc instance = null;
    public static CacheSvc Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CacheSvc();
            }
            return instance;
        }
    }
    private DBMgr dBMgr;
    private Dictionary<ServerSession, PlayerData> onLineSessionDic = new Dictionary<ServerSession, PlayerData>();

    public void Init()
    {
        dBMgr = DBMgr.Instance;
        GameCommon.Log("Cache Init Done");
    }
    public PlayerData? GetPlayerData(string acct, string pass)
    {
        return dBMgr.GetPlayerData(acct, pass);
    }
    /// <summary>
    /// 判断账号是否存在
    /// </summary>
    /// <param name="acct"></param>
    /// <returns></returns>
    public bool Isacct(string acct)
    {
        return dBMgr.QueryPlayerData(acct);
    }
    /// <summary>
    /// 注册账号
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    public bool RegistAcct(string acct, string pass)
    {
        return dBMgr.RegistAcct(acct, pass);
    }
    /// <summary>
    /// 账号上线
    /// </summary>
    /// <param name="session"></param>
    /// <param name="playerData"></param>
    public void AcctOnline(ServerSession session, PlayerData playerData)
    {
        if (!onLineSessionDic.ContainsKey(session))
        {

            onLineSessionDic.Add(session, playerData);
        }
    }
    public FriendItem GetPlayerDataByFriendName(string friendName)
    {
        return dBMgr.GetPlayerDataByFriendName(friendName);
    }
    /// <summary>
    /// 根据id查询在线玩家
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="friendName"></param>
    /// <returns></returns>
    public PlayerData GetPlayerData(string name)
    {
        foreach (PlayerData playerData in onLineSessionDic.Values)
        {
            if (playerData.name == name)
            {
                return playerData;
            }
        }
        return null;
    }
    /// <summary>
    /// 根据id查询玩家
    /// </summary>
    /// <param name="playerId"></param>
    /// <returns></returns>
    public PlayerData GetPlayerData(int playerId)
    {
        foreach (PlayerData playerData in onLineSessionDic.Values)
        {
            if (playerData.id == playerId)
            {
                return playerData;
            }
        }
        return null;
    }
    public PlayerData GetPlayerDataBySession(ServerSession session)
    {
        if (onLineSessionDic.TryGetValue(session, out PlayerData playerData))
        {
            return playerData;
        }
        else
        {
            return null;
        }
    }
    public ServerSession GetSessionByPlayerData(PlayerData playerData)
    {
        if (onLineSessionDic.ContainsValue(playerData))
        {
            foreach (var session in onLineSessionDic)
            {
                if (session.Value == playerData)
                {
                    return session.Key;
                }

            }
        }
        return null;

    }
    public List<PlayerData> GetPlayerData(PlayerData playerData)
    {
        List<PlayerData> sessions = new List<PlayerData>();
        foreach (var sessiondic in onLineSessionDic)
        {
            if (playerData != sessiondic.Value)
            {

                sessions.Add(sessiondic.Value);
            }
        }
        return sessions;
    }
    /// <summary>
    /// 获取其他session并进行广播
    /// </summary>
    /// <returns></returns>
    public void GetSession(ServerSession session, GameMsg msg)
    {

        byte[] bytes = TraTool.PackLenInfo(TraTool.Serialize(msg));
        foreach (ServerSession sessiondic in onLineSessionDic.Keys)
        {
            if (session != sessiondic)
            {

                sessiondic.SendMsg(bytes);
            }
        }
    }
    /// <summary>
    /// 下线处理
    /// </summary>
    public void AcctOutLine(ServerSession server)
    {

        //foreach (var session in onLineSessionDic)
        //{
        //    if (server == session.Key)
        //    {
        //        onLineSessionDic.Remove(server);
        //        break;
        //    }
        //}
        bool scct = onLineSessionDic.Remove(server);
        GameCommon.Log("该账号下线：" + scct);
    }
    public bool CheckName(string name)
    {
        return dBMgr.CheckName(name);
    }
    public bool UpdatePlayerData(PlayerData playerData)
    {
        return dBMgr.UpdatePlayerData(playerData);
    }
    public bool UpdateFriend(FriendItem friendItem)
    {
        return dBMgr.UpdateFriend(friendItem);
    }
}