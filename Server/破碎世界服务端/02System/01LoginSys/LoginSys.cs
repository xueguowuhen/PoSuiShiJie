/****************************************************
    文件：LoginSys
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-15 22:13:24
	功能：登录系统
*****************************************************/
using CommonNet;
using ComNet;
using 墨染服务端._01Service._01NetSvc;
using static CfgSvc;

public class LoginSys
{
    private static LoginSys instance = null;
    public static LoginSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new LoginSys();
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
        GameCommon.Log("Login Init Done");
    }
    /// <summary>
    /// 处理登录请求
    /// </summary>
    /// <param name="pack"></param>
    public void ReqLogin(MsgPack pack)
    {
        ReqLogin reqLogin = pack.gameMsg.reqLogin;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspLogin
        };
        bool isAcct = cacheSvc.Isacct(reqLogin.acct);
        if (isAcct)//存在账号
        {
            PlayerData? playerData = cacheSvc.GetPlayerData(reqLogin.acct, reqLogin.pass);
            if (playerData == null)
            {
                msg.err = (int)Error.LoginInvalidError;
            }
            else
            {
                List<PlayerData> playerDataList = cacheSvc.GetPlayerData(playerData);
                //if (playerDataList.Count > 0)
                //    GameCommon.Log(playerDataList[0].AniState.ToString());
                DailyTaskSys.Instance.UpdateRewardTask(playerData);
                msg.rspLogin = new RspLogin
                {
                    playerData = playerData,
                    playerList = playerDataList,
                };
                cacheSvc.AcctOnline(pack.session, playerData);
                ReqCreatePlayer(pack, playerData);
            }
        }
        else
        {
            msg.err = (int)Error.LoginExistError;
        }
        pack.session.SendMsg(msg);
    }
    /// <summary>
    /// 处理注册请求
    /// </summary>
    /// <param name="pack"></param>
    public void ReqRegister(MsgPack pack)
    {
        ReqRegister reqRegister = pack.gameMsg.reqRegister;
        GameMsg gameMsg = new GameMsg
        {
            cmd = (int)CMD.RspRegister
        };
        bool isAcct = cacheSvc.Isacct(reqRegister.acct);//判断账号是否存在
        if (!isAcct)//账号不存在
        {
            //注册账号
            bool isRegistAcct = cacheSvc.RegistAcct(reqRegister.acct, reqRegister.pass);
            if (isRegistAcct)
            {
                gameMsg.rspRegister = new RspRegister
                {
                    isSucc = isRegistAcct,
                };

            }
            else
            {
                gameMsg.err = (int)Error.RegisterError;
            }
        }
        else
        {
            gameMsg.err = (int)Error.AcctExistError;
        }
        pack.session.SendMsg(gameMsg);

    }
    /// <summary>
    /// 注册角色请求
    /// </summary>
    public void ReqCreateGame(MsgPack pack)
    {
        ReqCreateGame reqCreateGame = pack.gameMsg.reqCreateGame;
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspCreateGame,
        };
        personCfg personCfg = cfgSvc.GetPersonCfgData(reqCreateGame.id);
        TalentCfg talentCount = new TalentCfg();
        if (personCfg == null)
        {
            msg.err = (int)Error.PerSonError;
        }
        else
        {
            //TODO校验玩家名称不可重复
            if (!cacheSvc.CheckName(reqCreateGame.name))
            {
                if (SetPlayerDataTalent(reqCreateGame, msg, playerData, personCfg, talentCount))
                {
                    cacheSvc.AcctOnline(pack.session, playerData);
                    ReqCreatePlayer(pack, playerData);
                }
            }
            else
            {
                msg.err = (int)Error.NameExistError;
            }
        }
        pack.session.SendMsg(msg);
    }
    private bool SetPlayerDataTalent(ReqCreateGame reqCreateGame, GameMsg msg, PlayerData playerData, personCfg personCfg, TalentCfg talentCount)
    {
        //累加天赋数据
        List<int> TalentIDList = reqCreateGame.TalentIDList;
        for (int i = 0; i < TalentIDList.Count; i++)
        {
            TalentCfg talentCfg = cfgSvc.GetTalentCfgData(TalentIDList[i]);
            if (talentCfg == null)
            {
                msg.err = (int)Error.PerSonError;
                break;
            }
            else
            {
                talentCount = GetTalent(talentCount, talentCfg);
                talentCount = GetTalent(talentCount, talentCfg);
            }
        }
        //累加进玩家属性
        personCfg = GetPerson(personCfg, talentCount);
        //更新玩家数据

        playerData.name = reqCreateGame.name;
        playerData.TalentID = TalentIDList;
        playerData.level = 1;
        playerData.type = reqCreateGame.id;
        playerData.Taskid = cfgSvc.GetTaskCfgOne();
        playerData.rewardTask = new RewardTask();
        playerData.rewardTask.TaskProgress = new List<int>(new int[cfgSvc.GetTaskRewardCount()]);//任务进度初始化
        playerData.rewardTask.LastTime = DateTime.Now;
        playerData.dailyTasks = cfgSvc.GetTaskDailyCfgData();
        playerData.FriendList = new List<FriendItem>();
        playerData.AddFriendList = new List<FriendItem>();
        playerData = GetPlayerData(playerData, personCfg);
        if (!cacheSvc.UpdatePlayerData(playerData))
        {
            msg.err = (int)Error.PerSonError;
            return false;
        }
        else
        {
            List<PlayerData> playerDataList = cacheSvc.GetPlayerData(playerData);
            msg.rspCreateGame = new RspCreateGame
            {
                playerData = playerData,
                playerDataList = playerDataList,
            };
            return true;
        }
    }
    /// <summary>
    /// 广播创建角色通知
    /// </summary>
    /// <param name="pack"></param>
    public void ReqCreatePlayer(MsgPack pack, PlayerData playerData)
    {
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspCreatePlayer,
        };
        msg.rspCreatePlayer = new RspCreatePlayer
        {
            playerData = playerData,
        };
        //向其他在线玩家广播创建玩家数据
        cacheSvc.GetSession(pack.session, msg);
    }
    /// <summary>
    /// 广播删除角色通知
    /// </summary>
    /// <param name="pack"></param>
    /// <param name="playerData"></param>
    public void ReqDeletePlayer(MsgPack pack)
    {
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspDeletePlayer,
        };
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        if (playerData != null)
        {

            msg.rspDeletePlayer = new RspDeletePlayer
            {
                PlayerID = playerData.id,
            };
            //向其他玩家广播删除玩家数据
            cacheSvc.GetSession(pack.session, msg);
        }
    }
    public TalentCfg GetTalent(TalentCfg talent, TalentCfg talentCfg)
    {
        talent.HP += talentCfg.HP;
        talent.Mana += talentCfg.Mana;
        talent.Power += talentCfg.Power;
        talent.aura += talentCfg.aura;
        talent.ruvia += talentCfg.ruvia;
        talent.crystal += talentCfg.crystal;
        talent.ad += talentCfg.ad;
        talent.ap += talentCfg.ap;
        talent.addef += talentCfg.addef;
        talent.dodge += talentCfg.dodge;
        talent.practice += talentCfg.practice;
        talent.critical += talentCfg.critical;
        return talent;
    }
    public personCfg GetPerson(personCfg person, TalentCfg talentCfg)
    {
        person.HP += talentCfg.HP;
        person.Mana += talentCfg.Mana;
        person.Power += talentCfg.Power;

        person.aura += talentCfg.aura;
        person.ruvia += talentCfg.ruvia;
        person.crystal += talentCfg.crystal;
        person.ad += talentCfg.ad;
        person.ap += talentCfg.ap;
        person.addef += talentCfg.addef;
        person.dodge += talentCfg.dodge;
        person.practice += talentCfg.practice;
        person.critical += talentCfg.critical;
        return person;
    }

    public PlayerData GetPlayerData(PlayerData playerData, personCfg personCfg)
    {
        playerData.Hp += personCfg.HP;
        playerData.Hpmax += personCfg.HP;
        playerData.ManaMax += personCfg.Mana;
        playerData.Mana += personCfg.Mana;
        playerData.ruvia += personCfg.ruvia;
        playerData.crystal += personCfg.crystal;
        playerData.power += personCfg.Power;
        playerData.powerMax += personCfg.Power;
        playerData.aura += personCfg.aura;
        playerData.ad += personCfg.ad;
        playerData.ap += personCfg.ap;
        playerData.addef += personCfg.addef;
        playerData.dodge += personCfg.dodge;
        playerData.practice += personCfg.practice;
        playerData.critical += personCfg.critical;
        return playerData;
    }
    /// <summary>
    /// 下线操作
    /// </summary>
    /// <param name="server"></param>
    public void ClearOfflineData(ServerSession server)
    {
        ReqDeletePlayer(new MsgPack(server, null));
        cacheSvc.AcctOutLine(server);
    }
}
