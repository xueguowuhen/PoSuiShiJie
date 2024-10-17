using CommonNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CfgSvc;
using static Mysqlx.Notice.Warning.Types;


public class TalentSys
{
    private static TalentSys instance = null;
    public static TalentSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TalentSys();
            }
            return instance;
        }
    }
    private CacheSvc cacheSvc;
    private CfgSvc cfgSvc;
    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        cfgSvc = CfgSvc.Instance;
        GameCommon.Log("TalentSys Init Done");
    }

    public void ReqTalentUpHandle(MsgPack gameMsg)
    {
        ReqTalentUp? msg = gameMsg.gameMsg.reqTalentUp;
        int talentid = msg!.TalentId;
        int NextLevel = msg.NextLevel;
        bool needupdate = false;
        GameMsg rspmsg = new GameMsg
        {
            cmd = (int)CMD.RspTalentUp,
            rspTalentUp = new RspTalentUp(),
        };
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(gameMsg.session);
        TalentCfg talentCfg = cfgSvc.GetTalentCfgData(talentid);
        foreach (int i in playerData.TalentID!)
        {
            if (i == talentid) //升级天赋为当前选择天赋
            {
                foreach (Talent j in playerData.TalentsData!)
                    if (j.TalentID == i)
                    {
                        CurrTalentUp(j, talentCfg, playerData);
                        needupdate = true;
                        break;
                    }
                break;
            }
        }
        rspmsg.rspTalentUp.NeedUpdate = needupdate;
        if (cacheSvc.CheckAndUpdateTalentsData(playerData.id, talentid, NextLevel, talentCfg)) //数据库仅更新天赋ID与等级 
        {
            rspmsg.rspTalentUp.IsUpSuccess = true;
            for (int i = 0; i < playerData.TalentsData!.Count; i++)
            {
                if (talentid == playerData.TalentsData[i].TalentID)
                {
                    playerData.TalentsData[i].Level++;
                    break;
                }
            }
            if (needupdate)
            { rspmsg.rspTalentUp.battleData = GetDataFromPlayerData(playerData); }
            else { rspmsg.rspTalentUp.battleData = null; }
            rspmsg.rspTalentUp.talents = playerData.TalentsData;
        }
        else
        {
            rspmsg.rspTalentUp.IsUpSuccess = false;
        }
        gameMsg.session.SendMsg(rspmsg);

    }
    public void ReqChangeTalentHandle(MsgPack gameMsg)
    {
        ReqChangeTalent? msg = gameMsg.gameMsg.reqChangeTalent;
        List<int>? currTalentsID = msg.CurrTalents;
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(gameMsg.session);
        GameMsg rspmsg = new GameMsg()
        {
            cmd = (int)CMD.RspChangeTalent,
            rspChangeTalent = new RspChangeTalent(),
        };
        if (CalcPlayerProp(playerData, currTalentsID!))
        {

            rspmsg.rspChangeTalent.IsChangeSuccess = true;
            rspmsg.rspChangeTalent.playerData = playerData;
            playerData.TalentID = currTalentsID;
        }
        else
        {
            rspmsg.rspChangeTalent.IsChangeSuccess = false;
            rspmsg.rspChangeTalent.playerData = playerData;
        }
        gameMsg.session.SendMsg(rspmsg);
    }
    public bool CalcPlayerProp(PlayerData playerData, List<int> newtalentsId = null!)
    {
        bool UpdateSuccess = true;
        if (newtalentsId != null)
        {
            for (int i = 0; i < playerData.TalentID!.Count; i++) //去除旧天赋数据
            {
                TalentCfg talentCfg = cfgSvc.GetTalentCfgData(playerData.TalentID[i]);
                int level = 1;
                foreach (Talent j in playerData.TalentsData!)
                {
                    if (j.TalentID == talentCfg.ID) { level = j.Level; break; }
                }
                UpdateAttribute(talentCfg.Attribute!, talentCfg, level, playerData, false);
            }
        }
        else //玩家上线获取天赋数据
        {
            newtalentsId = playerData.TalentID!;
        }
        playerData.TalentID = newtalentsId;
        UpdateSuccess = cacheSvc.UpdatePlayerData(playerData);
        for (int i = 0; i < newtalentsId!.Count; i++) //修正最新天赋数据
        {
            TalentCfg talentCfg = cfgSvc.GetTalentCfgData(newtalentsId[i]);
            int level = 1;
            foreach (Talent j in playerData.TalentsData!)
            {
                if (j.TalentID == talentCfg.ID) { level = j.Level; break; }
            }
            UpdateAttribute(talentCfg.Attribute!, talentCfg, level, playerData, true);
        }
        playerData.TalentID = newtalentsId;
        return UpdateSuccess;
    }

    private void CurrTalentUp(Talent talent, TalentCfg talentCfg, PlayerData playerData)
    {
        UpdateAttribute(talentCfg.Attribute!, talentCfg, 1, playerData, true);
    }
    private BattleData GetDataFromPlayerData(PlayerData playerData)
    {
        BattleData data = new BattleData()
        {
            Hp = playerData.Hp,
            Hpmax = playerData.Hpmax,
            Mana = playerData.Mana,
            ManaMax = playerData.ManaMax,
            ad = playerData.ad,
            addef = playerData.addef,
            ap = playerData.ap,
            apdef = playerData.apdef,
            dodge = playerData.dodge,
            critical = playerData.critical,
        };
        return data;
    }
    public void UpdateAttribute(string attribute, TalentCfg talentCfg, int level, PlayerData playerData, bool IsAdd)
    {
        int temp = 1;
        if (IsAdd) { temp = 1; } else { temp = -1; }
        switch (attribute)
        {
            case "Hp":
                { playerData.Hpmax += temp * (int)talentCfg.Value * level; playerData.Hp += temp * (int)talentCfg.Value * level; return; }
            case "addef":
                { playerData.addef += temp * (int)talentCfg.Value * level; return; }
            case "apdef":
                { playerData.apdef += temp * (int)talentCfg.Value * level; return; }
            case "dodge":
                { playerData.dodge += temp * (int)talentCfg.Value * level; return; }
            case "ad":
                { playerData.ad += temp * (int)talentCfg.Value * level; return; }
            case "critical":
                { playerData.critical += temp * (int)talentCfg.Value * level; return; }
            case "ap":
                { playerData.ap += temp * (int)talentCfg.Value * level; return; }
        }
    }
}
