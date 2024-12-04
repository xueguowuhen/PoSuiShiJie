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
        SocketDispatcher.Instance.AddEventListener(CMD.RspCreatePlayer, RspCreatePlayer);
        SocketDispatcher.Instance.AddEventListener(CMD.RspDeletePlayer, RspDeletePlayer);
        SocketDispatcher.Instance.AddEventListener(CMD.RspTransform, RspTransform);
        SocketDispatcher.Instance.AddEventListener(CMD.RspDamage, RspDamage);
        SocketDispatcher.Instance.AddEventListener(CMD.RspState, RspState);
        SocketDispatcher.Instance.AddEventListener(CMD.RspRevive, RspRevive);
        SocketDispatcher.Instance.AddEventListener(CMD.RspEnterPVP, RspEnterPVP);
        SocketDispatcher.Instance.AddEventListener(CMD.RspExitPVP, RspExitPVP);
        SocketDispatcher.Instance.AddEventListener(CMD.RspRecover, RspRecover);
        GameCommon.Log("Init BattleSys");
    }
    /// <summary>
    /// 进入主城 初始化战斗管理器
    /// </summary>
    public void EnterBattleMap(MapCfg mapData)
    {
        GameObject go = GameObject.Find("BattleRoot");

        if (go == null) // 如果不存在
        {
            go = new GameObject
            {
                name = "BattleRoot"
            };
            go.transform.SetParent(GameRoot.Instance.transform);
            battleMgr = go.AddComponent<BattleMgr>();
            battleMgr.Init(mapData);
        }
        else // 如果已经存在
        {
            battleMgr = go.GetComponent<BattleMgr>();
            // 可能需要做其他处理，比如重置或更新 battleMgr
        }


    }
    /// <summary>
    /// 进入主城
    /// </summary>
    public void EnterBattlePVP()
    {
        MapCfg mapData = resSvc.GetMapCfgData(Constants.BattlePVPMapID);
        //if (MainCitySys.instance.mainCityWnd.DownLoadUrl != null)
        //{

        //    DowningSys.instance.StopDownload(MainCitySys.instance.mainCityWnd.DownLoadUrl);
        //    MainCitySys.instance.mainCityWnd.DownLoadUrl = null;
        //}
        GameCommon.Log("异步地图中");
        StartCoroutine(loaderSvc.AsyncLoadScene(mapData.sceneName, () =>
        {
            GameCommon.Log("Enter BattlePVP...");
            //isCreate = true;
            GameRoot.Instance.SetScreenSpaceOverlay();//设置界面
            SetBattleWnd();
            EnterBattleMap(mapData);

            // GameRoot.Instance.SetScreenSpaceOverlay();//设置界面
            //GameObject map = GameObject.Find(PathDefine.MapRoot);
        }, true));
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
    public void SetBattleWnd(bool isShow = true)
    {
        battleWnd.SetWndState(isShow);
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
    public EntityBase GetEntity(int id)
    {
        return battleMgr.GetEntity(id);
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
            GameRoot.Instance.SetPlayerDataList(rspEnterPVP.PlayerDataList);
            MainCitySys.instance.CloseStartGamePVPWnd();
            EnterBattlePVP();
        }
    }


    public void RspExitPVP(GameMsg msg)
    {
        RspExitPVP rspExitPVP = msg.rspExitPVP;
        if (rspExitPVP.isSucc)
        {
            // 退出PVP
            GameCommon.Log("退出PVP成功");
            EnterMainCity();
        }
    }
    public void RspRecover(GameMsg msg)
    {
        RspRecover rspRecover = msg.rspRecover;
        int playerId = rspRecover.id;
        PlayerData playerData = GameRoot.Instance.PlayerData;

        //是自己时返回
        if (playerData.id == playerId)
        {
            playerData.Hp = msg.rspRecover.Hp; // 重新配置血量
            Debug.Log("刷新血量" + playerData.Hp);
            if (rspRecover.isRevive)//是否复活
            {
                battleWnd.ShowFailTipWnd(false);
                battleWnd.RefreshUI();
                GetEntity(playerData.id).canControl = true;
                GetEntity(playerData.id).Idle();
            }
            else//返回主页
            {
                // 刷新UI
                EnterMainCity();
            }
            return;
        }

        // 遍历玩家数据列表
        foreach (PlayerData RemoteplayerData in GameRoot.Instance.PlayerDataList)
        {
            if (RemoteplayerData.id == playerId)
            {
                // Debug.Log("刷新血量" + playerData.Hp);
                RemoteplayerData.Hp = rspRecover.Hp; // 重新配置血量
                                                     //   RefreshUI(); 
                break;
            }
        }
    }
    /// <summary>
    /// 远程创建人物
    /// </summary>
    /// <param name="msg"></param>
    public void RspCreatePlayer(GameMsg msg)
    {
        PlayerData playerData = msg.rspCreatePlayer.playerData;
        if (GameRoot.Instance.PlayerDataList != null)
        {
            foreach (PlayerData data in GameRoot.Instance.PlayerDataList)
            {
                if (data.id == playerData.id)
                {
                    GameCommon.Log("创建人物失败，ID重复");
                    return;
                }
            }

        }
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
            //  GameRoot.Instance.dynamicWnd.RemoveHptemInfo(playerID);
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
                if (rspTransform.isShoolr)//是否平滑
                {
                    player.SetTrans(rspTransform.time, pos, Rot, rspTransform.speed);
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
    /// <summary>
    /// 进入主城
    /// </summary>
    public void EnterMainCity()
    {
        Destroy(battleMgr.cameraPlayerCtrl.gameObject);
        GameObject go = GameObject.Find("BattleRoot");
        if (go != null)
        {
            Destroy(go);
        }
        SetBattleWnd(false);
        GameRoot.Instance.SetScreenSpaceCamera();//设置界面
        MainCitySys.instance.EnterMainCity();
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