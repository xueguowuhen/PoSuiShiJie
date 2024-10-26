/****************************************************
    文件：BattleSys.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/5/9 16:48:37
	功能：战斗系统
*****************************************************/

using CommonNet;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BattleSys : SystemRoot
{
    public static BattleSys instance;
    public BattleWnd battleWnd;
    private BattleMgr battleMgr;
    private Vector2 Dir;
    private bool IsRun;

    public override void InitSyc()
    {
        base.InitSyc();
        instance = this;
        GameCommon.Log("Init BattleSys");
    }
    /// <summary>
    /// 进入主城 初始化战斗管理器
    /// </summary>
    public void EnterBattleMap(MapCfg mapData)
    {
        GameObject go = new GameObject
        {
            name = "BattleRoot"
        };
        go.transform.SetParent(GameRoot.Instance.transform);
        battleMgr = go.AddComponent<BattleMgr>();
        battleMgr.Init(mapData);

    }
    /// <summary>
    /// 进入主城
    /// </summary>
    public void EnterBattlePVP()
    {
        MapCfg mapData = resSvc.GetMapCfgData(Constants.BattlePVPMapID);
        GameCommon.Log("异步地图中");
        StartCoroutine(resSvc.AsyncLoadScene(mapData.sceneName, () =>
        {
            GameCommon.Log("Enter BattlePVP...");
            //isCreate = true;
            SetBattleWnd();
            EnterBattleMap(mapData);
           // GameRoot.Instance.SetScreenSpaceOverlay();//设置界面
            //GameObject map = GameObject.Find(PathDefine.MapRoot);
        }, false));
    }

    /// <summary>
    /// 发送进入PVP
    /// </summary>
    public void SendEnterBattlePVP()
    {
        PlayerData playerData = GameRoot.Instance.PlayerData;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.ReqEnterPVP,
            reqEnterPVP = new ReqEnterPVP
            {
                id = playerData.id,
            }
        };
        netSvc.SendMsg(msg);
    }
    public void SetBattleWnd()
    {
        battleWnd.SetWndState();
    }
    public void SetSelfPlayerMoveDir(Vector2 dir, bool IsRun = false)
    {
        Dir = dir;
        this.IsRun = IsRun;
        battleMgr.SetSelfPlayerMove(dir, IsRun);
    }
    public void SetCamMoveDir(Vector2 dir)
    {
        battleMgr.SetCamMove(dir);
    }
    /// <summary>
    /// 普通攻击 
    /// </summary>
    public void ReleaseNormal()
    {
        battleMgr.ReleaseNormal();
    }
    /// <summary>
    /// 技能攻击1
    /// </summary>
    public void ReleaseSkill1()
    {
        battleMgr.ReleaseSkill1();
    }
    /// <summary>
    /// 技能攻击2
    /// </summary>
    public void ReleaseSkill2()
    {
        battleMgr.ReleaseSkill2();
    }
    /// <summary>
    /// 技能攻击3
    /// </summary>
    public void ReleaseSkill3()
    {
        battleMgr.ReleaseSkill3();
    }
    public void Evade()
    {
        battleMgr.Evade();
    }
    public bool CanRlsSkill()
    {
        return battleMgr.CanRlsSkill();
    }
    public Vector2 GetDirInput()
    {
        return Dir;
    }
    public bool GetRunState()
    {
        return IsRun;
    }
    public BattleMgr GetBattleMgr()
    {
        return battleMgr;
    }
    public CharacterController GetCharacterController()
    {
        return battleMgr.characterController;
    }

    public PlayerController GetplayerController()
    {
        return battleMgr.playerController;
    }
    public NavMeshAgent GetNavMesh()
    {
        return battleMgr.navMesh;
    }
    public GameObject Getplayer()
    {
        return battleMgr.player;
    }
    //public List<GameObject> GetPlayerList()
    //{
    //    return MainCitySys.instance.GetPlayerList();
    //}

    public void RspDamage(GameMsg msg)
    {
        RspDamage rspDamage = msg.rspDamage;
        if (battleMgr == null)
        {
            foreach (PlayerData playerData in GameRoot.Instance.PlayerDataList)
            {
                if (playerData.id == rspDamage.id)
                {
                    playerData.Hp = rspDamage.hp;
                }
            }
            return;
        }
        if (GameRoot.Instance.PlayerData.id == rspDamage.id)//是否是自己受伤
        {
            battleMgr.SetDamageState(GameRoot.Instance.PlayerData, rspDamage);
            RefreshUI();

        }
        else
        {
            foreach (PlayerData playerData in GameRoot.Instance.PlayerDataList)
            {
                if (playerData.id == rspDamage.id)
                {

                    battleMgr.SetDamageState(playerData, rspDamage);
                    RefreshUI();
                    break;
                }
            }
        }
    }
    public void RspState(GameMsg msg)
    {
        if (battleMgr != null)
        {

            battleMgr.RspState(msg);
        }
    }
    public void RspEnterPVP(GameMsg msg)
    {
        RspEnterPVP rspEnterPVP = msg.rspEnterPVP;
        if (rspEnterPVP.isSucc)
        {
            // 进入PVP
            GameCommon.Log("进入PVP成功");
            MainCitySys.instance.CloseStartGamePVPWnd();
            EnterBattlePVP();
        }
    }


    public void RspExitPVP(GameMsg msg)
    {

    }
    /// <summary>
    /// 远程创建人物
    /// </summary>
    /// <param name="msg"></param>
    public void RspCreatePlayer(GameMsg msg)
    {
        PlayerData playerData = msg.rspCreatePlayer.playerData;
        //Debug.Log(playerData.id);
        GameRoot.Instance.PlayerDataList.Add(playerData);
        if (battleMgr == null)
        {
            return;
        }
        battleMgr.SetRemoteEntity(playerData);
    }
    /// <summary>
    /// 远程删除人物
    /// </summary>
    /// <param name="msg"></param>
    public void RspDeletePlayer(GameMsg msg)
    {
        int playerID = msg.rspDeletePlayer.PlayerID;
        List<PlayerData> playerDataToRemove = new List<PlayerData>(); // 存储需要移除的玩家数据
                                                                      // 遍历玩家数据列表
        foreach (PlayerData playerData in GameRoot.Instance.PlayerDataList)
        {
            if (playerData.id == playerID)
            {
                playerDataToRemove.Add(playerData); // 将需要移除的玩家数据添加到列表中
                break;
            }
        }
        // 从玩家数据列表中移除需要移除的玩家数据
        foreach (var playerData in playerDataToRemove)
        {
            GameRoot.Instance.PlayerDataList.Remove(playerData);
        }

        // 检查并删除字典中对应 playerID 的玩家对象
        if (battleMgr.RemoteEntityDic.TryGetValue(playerID, out EntityBase player))
        {
            battleMgr.RemoteEntityDic.Remove(playerID);
            GameRoot.Instance.dynamicWnd.RemoveHptemInfo(playerID);
            player.Destroy();
        }
    }
    /// <summary>
    /// 更新玩家旋转
    /// </summary>
    /// <param name="msg"></param>
    public void RspTransform(GameMsg msg)
    {
        int playerID = msg.rspTransform.playerID;
        RspTransform rspTransform = msg.rspTransform;
        if (battleMgr != null)
        {

            // 更新玩家位置和旋转
            if (battleMgr.RemoteEntityDic.TryGetValue(playerID, out EntityBase player))
            {
                Vector3 pos = new Vector3(rspTransform.Pos_X, rspTransform.Pos_Y, rspTransform.Pos_Z);
                Vector3 Rot = new Vector3(rspTransform.Rot_X, rspTransform.Rot_Y, rspTransform.Rot_Z);
                //GameCommon.Log("收到旋转" + rspTransform.Rot_Y);
                if (rspTransform.isShoolr)
                {
                    player.SetTrans(rspTransform.time, pos, Rot);
                }
                else
                {
                    // GameCommon.Log("接收到" + rspTransform.isShoolr + "," + Rot);
                    player.SetRotation(Rot);
                }
            }
        }
    }
    public void RspRevive(GameMsg msg)
    {
        int playerId = msg.rspRevive.id;
        // 遍历玩家数据列表
        foreach (PlayerData playerData in GameRoot.Instance.PlayerDataList)
        {
            if (playerData.id == playerId)
            {
                playerData.Hp = msg.rspRevive.hp; // 重新配置血量
                RefreshUI();
                break;
            }
        }
    }
    public void RefreshUI()
    {
        if (battleWnd.GetWndState())
        {
            battleWnd.RefreshUI();
        }
        if (MainCitySys.instance.mainCityWnd.GetWndState())
        {
            MainCitySys.instance.mainCityWnd.RefreshUI();
        }
    }
}