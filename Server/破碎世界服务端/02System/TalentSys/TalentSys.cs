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
        TalentCfg cfg = cfgSvc.GetTalentCfgData(talentid);
        GameMsg rspmsg = new GameMsg
        {
            cmd = (int)CMD.RspTalentUp,
            rspTalentUp = new RspTalentUp(),
        };
        //RspTalentUp rspmsg = new RspTalentUp();
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(gameMsg.session);
        if (cacheSvc.CheckAndUpdateTalentsData(playerData.id, talentid, level))
        {
            rspmsg.rspTalentUp.IsUpSuccess = true;
        }
        else
        {
            rspmsg.rspTalentUp.IsUpSuccess = false;
        }
        gameMsg.session.SendMsg(rspmsg);

    }
}
