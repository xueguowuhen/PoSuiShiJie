/****************************************************
    文件：BattleSys
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-07 18:28:13
	功能：战斗同步系统
*****************************************************/
using CommonNet;
using ComNet;
using System.Diagnostics;
using 墨染服务端._01Service._01NetSvc;

public class BattleSys
{
    private static BattleSys instance = null;
    public static BattleSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BattleSys();
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
        GameCommon.Log("BattleSys Init Done");
    }
    public void ReqEnterPVP(MsgPack pack)
    {
        ReqEnterPVP reqEnterPVP = pack.gameMsg.reqEnterPVP;
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        GameMsg gameMsg = new GameMsg
        {
            cmd = (int)CMD.RspEnterPVP,
        };
        if (reqEnterPVP.id != playerData.id)
        {
            gameMsg.err = (int)Error.EnterPVPError;
            pack.session.SendMsg(gameMsg);
            return;
        }
        //加入房间
        cacheSvc.AcctEnterBattelPVP(playerData, pack.session);
        //向其他玩家广播进入PVP
        ReqCreatePlayer(pack, playerData);
        gameMsg.rspEnterPVP = new RspEnterPVP
        {
            isSucc = true,
            PlayerDataList = cacheSvc.GetPlayerData(playerData),
        };
        pack.session.SendMsg(gameMsg);
    }
    public void ReqExitPVP(MsgPack pack)
    {
        ReqExitPVP reqExitPVP = pack.gameMsg.reqExitPVP;
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        GameMsg gameMsg = new GameMsg
        {
            cmd = (int)CMD.RspExitPVP,
        };
        if (reqExitPVP.id != playerData.id)
        {
            gameMsg.err = (int)Error.ExitPVPError;
            pack.session.SendMsg(gameMsg);
            return;
        }
        //向其他玩家广播退出PVP
        ReqDeletePlayer(pack);

        gameMsg.rspExitPVP = new RspExitPVP
        {
            isSucc = true,
        };
        pack.session.SendMsg(gameMsg);
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
        PlayerData playerData = cacheSvc.GetBattlePlayerDataBySession(pack.session);
        if (playerData != null)
        {

            msg.rspDeletePlayer = new RspDeletePlayer
            {
                PlayerID = playerData.id,
            };
            //向其他玩家广播删除玩家数据
            cacheSvc.GetBattleSession(pack.session, msg);
            //退出房间
            cacheSvc.AcctExitBattelPVP(playerData);
        }
    }
    public void ReqTransform(MsgPack pack)
    {
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        ReqTransform reqTransform = pack.gameMsg.reqTransform;
        if (playerData == null)
        {
            return;
        }
        //GameCommon.Log("targetVelocity"+playerData)
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspTransform,
            rspTransform = new RspTransform
            {
                playerID = playerData.id,
                time = reqTransform.time,
                isShoolr = reqTransform.isShoolr,
                Pos_X = reqTransform.Pos_X,
                Pos_Y = reqTransform.Pos_Y,
                Pos_Z = reqTransform.Pos_Z,
                Rot_X = reqTransform.Rot_X,
                Rot_Y = reqTransform.Rot_Y,
                Rot_Z = reqTransform.Rot_Z,
            }
        };
        GameCommon.Log("位置是：" + reqTransform.Pos_X + "," + reqTransform.Pos_Y + "," + reqTransform.Pos_Z);
        GameCommon.Log("旋转是：" + reqTransform.Rot_X + "," + reqTransform.Rot_Y + "," + reqTransform.Rot_Z);

        playerData.Pos_X = reqTransform.Pos_X;
        playerData.Pos_Y = reqTransform.Pos_Y;
        playerData.Pos_Z = reqTransform.Pos_Z;
        playerData.Rot_X = reqTransform.Rot_X;
        playerData.Rot_Y = reqTransform.Rot_Y;
        playerData.Rot_Z = reqTransform.Rot_Z;
        //向其他玩家广播删除玩家数据
        cacheSvc.GetSession(pack.session, msg);
    }
    public void ReqState(MsgPack pack)
    {
        ReqPlayerState reqPlayerState = pack.gameMsg.reqPlayerState;
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspState,
            rspPlayerState = new RspPlayerState
            {
                PlayerID = reqPlayerState.PlayerID,
                AniState = reqPlayerState.AniState,
                SkillID = reqPlayerState.SkillID,
            }
        };
        playerData.SkillID = reqPlayerState.SkillID;
        playerData.AniState = reqPlayerState.AniState;
        GameCommon.Log("玩家" + playerData.id + "的状态是：" + playerData.AniState + "," + playerData.SkillID);
        cacheSvc.GetBattleSession(pack.session, msg);

    }
    public void ReqDamage(MsgPack pack)
    {
        ReqDamage reqDamage = pack.gameMsg.reqDamage;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspDamage,
        };
        if (reqDamage.Damage < 0)//伤害异常
        {
            msg.err = (int)Error.DamageError;
        }
        else
        {

            PlayerData playerData = cacheSvc.GetPlayerData(reqDamage.id);
            if (playerData == null)
            {
                msg.err = (int)Error.PerSonError;
            }
            else
            {
                playerData.Hp -= reqDamage.Damage;
                if (playerData.Hp <= 0)
                {
                    playerData.Hp = 0;
                }
                if (!cacheSvc.UpdatePlayerData(playerData))
                {
                    msg.err = (int)Error.PerSonError;
                }
                else
                {
                    msg.rspDamage = new RspDamage
                    {
                        id = playerData.id,
                        hp = playerData.Hp,
                        damageState = reqDamage.damageState,
                        Damage = reqDamage.Damage,
                    };
                    //向其他玩家广播伤害数据
                    cacheSvc.GetSession(pack.session, msg);
                }
            }
        }
    }
    public void ReqRevive(MsgPack pack)
    {
        ReqRevive revive = pack.gameMsg.reqRevive;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspRevive,
        };
        PlayerData playerData = cacheSvc.GetPlayerData(revive.id);
        if (playerData == null)
        {
            msg.err = (int)Error.PerSonError;
        }
        else
        {
            playerData.Hp = 1;
            if (!cacheSvc.UpdatePlayerData(playerData))
            {
                msg.err = (int)Error.PerSonError;
            }
            else
            {
                msg.rspDamage = new RspDamage
                {
                    id = playerData.id,
                    hp = playerData.Hp,
                };
                //向其他玩家广播血量
                cacheSvc.GetSession(pack.session, msg);
            }
        }
    }
}
