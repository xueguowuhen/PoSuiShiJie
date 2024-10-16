using CommonNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CfgSvc;


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
        int talentid = msg.TalentId;
        int level = msg.NextLevel;
        //TalentCfg cfg = cfgSvc.GetTalentCfgData(talentid);
        GameMsg rspmsg = new GameMsg
        {
            cmd = (int)CMD.RspTalentUp,
            rspTalentUp = new RspTalentUp(),
        };
        //RspTalentUp rspmsg = new RspTalentUp();
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(gameMsg.session);
        TalentCfg talentCfg = cfgSvc.GetTalentCfgData(talentid);
        if (cacheSvc.CheckAndUpdateTalentsData(playerData.id, talentid, level, talentCfg))
        {
            rspmsg.rspTalentUp.IsUpSuccess = true;
            //数据库更新成功 服务端数据更新
            for (int i = 0; i < playerData.TalentsData.Count; i++)
            {
                if (talentid == playerData.TalentsData[i].TalentID)
                {
                    playerData.TalentsData[i].Level++;
                    break;
                }
            }
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
        PlayerData newplayerdata = CalcPlayerProp(playerData, currTalentsID);
        if (cacheSvc.UpdatePlayerData(newplayerdata))
        {
            rspmsg.rspChangeTalent.IsChangeSuccess = true;
            rspmsg.rspChangeTalent.playerData = newplayerdata;
            //Console.WriteLine("修改完成");
        }
        else
        {
            rspmsg.rspChangeTalent.IsChangeSuccess = false;
            rspmsg.rspChangeTalent.playerData = playerData;
            //Console.WriteLine("修改失败");
        }
        gameMsg.session.SendMsg(rspmsg);
    }
    public PlayerData CalcPlayerProp(PlayerData playerData, List<int> newalentsId)
    {
        for (int i = 0; i < playerData.TalentID.Count; i++) //去除旧天赋数据
        {
            TalentCfg talentCfg = cfgSvc.GetTalentCfgData(playerData.TalentID[i]);
            int level = 1;
            foreach (Talent j in playerData.TalentsData)
            {
                if (j.TalentID == talentCfg.ID) { level = j.Level;break; }
            }
            switch (talentCfg.Attribute)
            {
                case "Hp":
                    { playerData.Hpmax -= (int)talentCfg.Value * level; break; }
                case "addef":
                    { playerData.addef -= (int)talentCfg.Value * level; break; }
                case "apdef":
                    { playerData.apdef -= (int)talentCfg.Value * level; break; }
                case "dodge":
                    { playerData.dodge -= (int)talentCfg.Value * level; break; }
                case "ad":
                    { playerData.ad -= (int)talentCfg.Value * level; break; }
                case "critical":
                    { playerData.critical -= (int)talentCfg.Value * level; break; }
                case "ap":
                    { playerData.ap -= (int)talentCfg.Value * level; break; }
            }
        }
        for (int i = 0; i < newalentsId.Count; i++) //修正最新天赋数据
        {
            TalentCfg talentCfg = cfgSvc.GetTalentCfgData(newalentsId[i]);
            int level = 1;
            foreach (Talent j in playerData.TalentsData)
            {
                if (j.TalentID == talentCfg.ID) { level = j.Level; break; }
            }
            switch (talentCfg.Attribute)
            {
                case "Hp":
                    { playerData.Hpmax += (int)talentCfg.Value * level; break; }
                case "addef":
                    { playerData.addef += (int)talentCfg.Value * level; break; }
                case "apdef":
                    { playerData.apdef += (int)talentCfg.Value * level; break; }
                case "dodge":
                    { playerData.dodge += (int)talentCfg.Value * level; break; }
                case "ad":
                    { playerData.ad += (int)talentCfg.Value * level; break; }
                case "critical":
                    { playerData.critical += (int)talentCfg.Value * level; break; }
                case "ap":
                    { playerData.ap += (int)talentCfg.Value * level; break; }
            }
        }
        playerData.TalentID = newalentsId;
        return playerData;
    }
}
