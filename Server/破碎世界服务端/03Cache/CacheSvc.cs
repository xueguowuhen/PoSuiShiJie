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
using static CfgSvc;

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
    private Dictionary<PlayerData, ServerSession> onBattelPVPSessionDic = new Dictionary<PlayerData, ServerSession>();
    public void Init()
    {
        dBMgr = DBMgr.Instance;
        GameCommon.Log("Cache Init Done");
    }
    public PlayerData? GetPlayerData(string acct, string pass)
    {
        PlayerData? playerData = dBMgr.GetPlayerData(acct, pass); //玩家原始数据

        if (playerData!.TalentID != null &&playerData.rewardTask!=null&& TalentSys.Instance.CalcPlayerProp(playerData!))//叠加的天赋数据
        {

        }
        return playerData;
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
        foreach (var sessiondic in onLineSessionDic)
        {
            if (sessiondic.Value.id == playerData.id)
            {
                LoginSys.Instance.ClearOfflineData(sessiondic.Key);//账号下线
                break;
            }
        }
        if (!onLineSessionDic.ContainsKey(session))
        {

            onLineSessionDic.Add(session, playerData);
        }
    }
    /// <summary>
    /// 进入战斗房间
    /// </summary>
    /// <param name="playerData"></param>
    public void AcctEnterBattelPVP(PlayerData playerData, ServerSession session)
    {
        if (!onBattelPVPSessionDic.ContainsKey(playerData))
        {
            onBattelPVPSessionDic.Add(playerData, session);
        }

    }
    /// <summary>
    /// 退出战斗房间
    /// </summary>
    /// <param name="playerData"></param>
    public void AcctExitBattelPVP(PlayerData playerData)
    {
        if (onBattelPVPSessionDic.ContainsKey(playerData))
        {
            onBattelPVPSessionDic.Remove(playerData);
        }
    }
    public void AcctExitBattelPVP(ServerSession session)
    {
        if (onBattelPVPSessionDic.ContainsValue(session))
        {
            onBattelPVPSessionDic.Remove(GetBattlePlayerDataBySession(session));
        }
    }
    /// <summary>
    /// 根据好友名称查询好友数据
    /// </summary>
    /// <param name="session"></param>
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
    /// <summary>
    /// 根据session获取战斗玩家数据
    /// </summary>
    /// <param name="session"></param>
    /// <returns></returns>
    public PlayerData GetBattlePlayerDataBySession(ServerSession session)
    {
        foreach (PlayerData playerData in onBattelPVPSessionDic.Keys)
        {
            if (onBattelPVPSessionDic[playerData] == session)
            {
                return playerData;
            }
        }
        return null;
    }
    /// <summary>
    /// 获取战斗房间session并进行广播
    /// </summary>
    /// <param name="session"></param>
    /// <param name="msg"></param>
    public void GetBattleSession(ServerSession session, GameMsg msg)
    {

        byte[] bytes = TraTool.PackLenInfo(TraTool.Serialize(msg));
        foreach (ServerSession sessiondic in onBattelPVPSessionDic.Values)
        {
            if (session != sessiondic)
            {

                sessiondic.SendMsg(bytes);
            }
        }
    }
    /// <summary>
    /// 根据玩家数据获取session
    /// </summary>
    /// <param name="playerData"></param>
    /// <returns></returns>
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
        bool scct = onLineSessionDic.Remove(server);
        GameCommon.Log("该账号下线：" + scct);
    }
    /// <summary>
    /// 检查名字是否存在
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool CheckName(string name)
    {
        return dBMgr.CheckName(name);
    }
    /// <summary>
    /// 更新玩家数据
    /// </summary>
    /// <param name="playerData"></param>
    public bool UpdatePlayerData(PlayerData playerData)
    {
        return dBMgr.UpdatePlayerData(playerData);
    }
    /// <summary>
    /// 更新好友数据
    /// </summary>
    /// <param name="friendItem"></param>
    /// <returns></returns>
    public bool UpdateFriend(FriendItem friendItem)
    {
        return dBMgr.UpdateFriend(friendItem);
    }
    /// <summary>
    /// 检查并升级天赋
    /// </summary>
    /// <returns></returns>
    public bool CheckAndUpdateTalentsData(int id, int talentID, int talentLevel, TalentCfg talentCfg, int aura)
    {
        return dBMgr.CheckAndUpdateTalent(id, talentID, talentLevel, talentCfg, aura);
    }
}