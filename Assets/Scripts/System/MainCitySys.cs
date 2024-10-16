/****************************************************
    文件：MainCitySys
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-25 20:27:24
	功能：主城窗体
*****************************************************/
using CommonNet;
using System.Collections.Generic;
using System.Xml.Xsl;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
public class MainCitySys : SystemRoot
{
    #region 公开字段/属性
    public static MainCitySys instance;
    public MainCityWnd mainCityWnd;
    public CharacterController characterController;
    public PlayerController playerController;
    public PersonWnd personWnd;
    public ShopWnd shop;
    public BagWnd bagWnd;
    public GuideWnd guideWnd;
    public FriendsWnd friendsWnd;
    public TalentWnd talentWnd;
    public DailyTaskWnd dailyTaskWnd;
    public TaskCfg GetTaskCfg()
    {
        return taskCfg;
    }
    public GameObject GetPlayer()
    {
        return player;
    }

    public Transform[] GetNpcTra()
    {
        return NpcPosTrans;
    }
    #endregion

    #region 私有字段
    private TaskCfg taskCfg;
    private NavMeshAgent navMesh;
    private Transform[] NpcPosTrans;
    private GameObject player;
    #endregion
    public override void InitSyc()
    {
        base.InitSyc();
        instance = this;
        GameCommon.Log("Init MainCitySys...");
    }
    /// <summary>
    /// 进入主城
    /// </summary>
    public void EnterMainCity()
    {
        MapCfg mapData = resSvc.GetMapCfgData(Constants.MainCityMapID);
        GameCommon.Log("异步地图中");
        StartCoroutine(resSvc.AsyncLoadScene(mapData.sceneName, () =>
        {
            GameCommon.Log("Enter MainCity...");
            //isCreate = true;
            mainCityWnd.SetWndState();
            GameObject map = GameObject.Find(PathDefine.MapRoot);
            //NpcPosTrans = map.GetComponent<MainCityMap>().NpcPosTrans;
            //BattleSys.instance.EnterBattleMap(mapData);
            //characterController = BattleSys.instance.GetCharacterController();
            //playerController = BattleSys.instance.GetplayerController();
            //navMesh = BattleSys.instance.GetNavMesh();
            //player = BattleSys.instance.Getplayer();
            //#region 敌人测试
            //Enemy = map.GetComponent<MainCityMap>().GetEnemy();
            //EntityEnemy CityEnemy = new EntityEnemy
            //{
            //    stateMgr = BattleSys.instance.GetBattleMgr().GetStateMgr(),
            //    battleMgr = BattleSys.instance.GetBattleMgr(),
            //    skillMgr = BattleSys.instance.GetBattleMgr().GetSkillMgr(),
            //    CurrentEntityType=EntityType.Monster,
            //};
            //CityEnemy.SetCtrl(Enemy.GetComponent<EnemyController>());
            //entityEnemy = CityEnemy;
            //entityEnemy.Init(resSvc.GetEnemyData(1001));
            //#endregion
        }, false));
    }

    #region 服务器消息处理

    #region RspShop 购买成功
    public void RspShop(GameMsg msg)
    {
        GameRoot.AddTips("购买成功");
        RspShop rspShop = msg.rspShop;
        PlayerData playerData = GameRoot.Instance.PlayerData;
        playerData.Bag = rspShop.Bag;
        playerData.aura = rspShop.aura;
        playerData.ruvia = rspShop.ruvia;
        playerData.crystal = rspShop.crystal;
        playerData.dailyTasks = rspShop.dailyTasks;
        RefreshUI();
    }
    #endregion

    #region RspTask 接收任务信息
    /// <summary>
    /// 接收任务信息
    /// </summary>
    /// <param name="msg"></param>
    public void RspTask(GameMsg msg)
    {
        RspTask rspTask = msg.rspTask;
        PlayerData playerData = GameRoot.Instance.PlayerData;
        playerData.aura = rspTask.aura;
        playerData.exp = rspTask.exp;
        playerData.level = rspTask.lv;
        playerData.Taskid = rspTask.Taskid;
        RefreshUI();
        GameRoot.AddTips("已完成该任务");
    }
    #endregion

    #region RspSearchFriend 搜索好友
    /// <summary>
    /// 搜索好友
    /// </summary>
    /// <param name="msg"></param>
    public void RspSearchFriend(GameMsg msg)
    {
        GameRoot.AddTips("已找到该好友");
        FriendItem friendItem = msg.rspSearchFriend.Friend;
        FriendReFresh(friendItem);
    }
    #endregion

    #region RspAddFriend 添加好友信息
    /// <summary>
    /// 添加好友信息
    /// </summary>
    /// <param name="msg"></param>
    public void RspAddFriend(GameMsg msg)
    {
        bool isSuccess = msg.rspAddFriend.isSucc;
        if (isSuccess)
        {
            GameRoot.AddTips("好友申请已发送");
        }
        else
        {
            PlayerData playerData = GameRoot.Instance.PlayerData;
            playerData.aura = msg.rspAddFriend.aura;
            playerData.ruvia = msg.rspAddFriend.ruvia;
            playerData.crystal = msg.rspAddFriend.crystal;
            playerData.AddFriendList = msg.rspAddFriend.AddFriendList;
            playerData.FriendList = msg.rspAddFriend.FriendList;
            // GameRoot.AddTips("好友申请发送失败");
            RefreshUI();
            FriendReFresh();
        }

    }
    #endregion

    #region RspFriendGift 赠送好友道具
    /// <summary>
    /// 赠送好友道具
    /// </summary>
    /// <param name="msg"></param>
    public void RspFriendGift(GameMsg msg)
    {
        RspFriendGift rspFriendGift = msg.rspFriendGift;
        GameRoot.AddTips("赠送成功");
        PlayerData playerData = GameRoot.Instance.PlayerData;
        playerData.Bag = rspFriendGift.Bag;
        playerData.aura = rspFriendGift.aura;
        playerData.ruvia = rspFriendGift.ruvia;
        playerData.crystal = rspFriendGift.crystal;
        RefreshUI();
    }
    #endregion

    #region RspFriendGift 好友申请确认
    /// <summary>
    /// 好友申请确认
    /// </summary>
    /// <param name="msg"></param>
    public void RspFriendAddConfirm(GameMsg msg)
    {
        RspFriendAddConfirm rspFriendAddConfirm = msg.rspFriendAddConfirm;
        PlayerData playerData = GameRoot.Instance.PlayerData;
        playerData.AddFriendList = rspFriendAddConfirm.AddFriendList;
        playerData.FriendList = rspFriendAddConfirm.FriendList;
        if (rspFriendAddConfirm.isAgree)
        {
            GameRoot.AddTips("好友申请已同意");

        }
        else
        {
            GameRoot.AddTips("好友申请已拒绝");
        }
        FriendReFresh();
    }
    #endregion

    #region RspDelFriend 删除好友信息
    /// <summary>
    /// 删除好友信息
    /// </summary>
    /// <param name="msg"></param>
    public void RspDelFriend(GameMsg msg)
    {
        RspDelFriend rspDelFriend = msg.rspDelFriend;
        PlayerData playerData = GameRoot.Instance.PlayerData;
        playerData.FriendList = rspDelFriend.FriendList;
        GameRoot.AddTips("删除好友成功");
        FriendReFresh();
    }
    #endregion

    #region RspRewardTask 领取奖励

    public void RspRewardTask(GameMsg msg)
    {
        RspRewardTask rspRewardTask = msg.rspRewardTask;
        PlayerData playerData = GameRoot.Instance.PlayerData;
        playerData.rewardTask = rspRewardTask.rewardTask;
        playerData.Bag=rspRewardTask.Bag;
        RefreshTaskUI();
    }
    #endregion

    #region RspDailyTask 领取活跃度
    public void RspDailyTask(GameMsg msg)
    {
        RspDailyTask rspDailyTask = msg.rspDailyTask;
        PlayerData playerData = GameRoot.Instance.PlayerData;
        playerData.dailyTasks = rspDailyTask.dailyTasks;
        RefreshTaskUI();
    }
    #endregion

    #region RspTalentUpHandler 处理天赋升级的服务器响应
    /// <summary>
    /// 处理天赋升级的服务器响应
    /// </summary>
    public void RspTalentUpHandler(GameMsg msg)
    {
        RspTalentUp rspTalentUp = msg.rspTalentUp;
        Debug.Log(rspTalentUp.IsUpSuccess);
        if (rspTalentUp.IsUpSuccess)
        {
            GameRoot.Instance.PlayerData.TalentsData = rspTalentUp.talents;
            talentWnd.NeewUpdate = true;
            talentWnd.RefreshLevelUp();
        }
        else
        {
            Debug.LogError("数据异常,升级失败");
        }
    } 
    #endregion

    public void RspTalentChangeHandler(GameMsg msg)
    {
        RspChangeTalent rspChangeTalent = msg.rspChangeTalent;
        if (rspChangeTalent.IsChangeSuccess)
        {
            //Debug.Log("更新成功");
            GameRoot.Instance.PlayerData = rspChangeTalent.playerData;
        }
        else
        {
            //Debug.Log("更新失败");
            Debug.LogError("数据更新失败");
        }
    }

    #endregion

    #region 玩家移动相关
    public void SetMoveDir(Vector2 dir, bool IsRun = false)
    {
        BattleSys.instance.SetSelfPlayerMoveDir(dir, IsRun);
        StopNavTask();
    }

    private bool isNavGuide = false;
    //public void RunTask(TaskCfg taskCfg)
    //{
    //    if (taskCfg != null)
    //    {
    //        this.taskCfg = taskCfg;
    //    }
    //    //解析任务ID
    //    if (taskCfg.npcID != -1)
    //    {
    //        float dis = Vector3.Distance(playerController.transform.position, NpcPosTrans[taskCfg.npcID].position);
    //        if (dis < 0.5f)
    //        {
    //            //关闭寻路，打开聊天窗体
    //            StopNavTask();
    //            OpenGuideWnd();
    //        }
    //        else
    //        {
    //            characterController.enabled = false;
    //            //playerController.SetAtkRotationLocal(Vector3.zero);
    //            playerController.SetNavMeshRot(true);
    //            navMesh.enabled = true;
    //            isNavGuide = true;
    //            navMesh.speed = Constants.PlayerRunSpeed;

    //            navMesh.SetDestination(NpcPosTrans[taskCfg.npcID].position);
    //            playerController.SetVelocity(Constants.VelocityRun, true);
    //        }
    //    }
    //}

    //判断是否到达导航点
    //private void IsArriveNavPos()
    //{
    //    float dis = Vector3.Distance(playerController.transform.position, NpcPosTrans[taskCfg.npcID].position);
    //    if (dis < 0.5f)//判断距离任务NPC的距离是否小于0.5f
    //    {
    //        StopNavTask();
    //        OpenGuideWnd();
    //    }
    //}
    public void StopNavTask()
    {
        if (isNavGuide)
        {
            navMesh.isStopped = true;
            navMesh.enabled = false;
            characterController.enabled = true;
            playerController.SetNavMeshRot(false);
            //Vector3 pos = characterController.transform.rotation.eulerAngles;
            //characterController.transform.rotation= Quaternion.identity;
            //playerController.SetAtkRotationLocal(pos);
            //GameCommon.Log(pos.ToString());
            isNavGuide = false;
            playerController.SetVelocity(Constants.VelocityIdle);
        }
    }
    #endregion

    #region 引擎回调
    public void Update()
    {
        //if (isNavGuide)
        //{
        //    IsArriveNavPos();
        //    playerController.SetCam();

        //}

        //#region 敌人测试
        //if (entityEnemy != null)
        //{
        //    entityEnemy.TickAILogic();
        //}
        //#endregion


    }
    #endregion

    //public List<GameObject> GetPlayerList()
    //{
    //    List<GameObject> list = new List<GameObject>();
    //    foreach (GameObject gameObject in RemotePlayerDic.Values)
    //    {
    //        list.Add(gameObject);
    //    }
    //    return list;
    //}

    public void OpenGuideWnd()
    {
        guideWnd.SetWndState();
    }
    public void RefreshUI()
    {
        if (mainCityWnd.gameObject.activeSelf)
        {
            mainCityWnd.RefreshUI();
        }
    }
    public void RefreshTaskUI()
    {
        if (dailyTaskWnd.gameObject.activeSelf)
        {
            dailyTaskWnd.RefreshTask();
        }
    }

    #region 主城系统对外开放的UI点击事件处理

    /// <summary>
    /// 商店界面
    /// </summary>
    public void EnterShop()
    {
        shop.SetWndState();
    }
    /// <summary>
    /// 背包界面
    /// </summary>
    public void EnterBagWnd()
    {
        bagWnd.SetWndState();
    }
    /// <summary>
    /// 每日任务界面
    /// </summary>
    public void EnterDailyTaskWnd()
    {
        dailyTaskWnd.SetWndState();
    }
    /// <summary>
    /// 人物界面
    /// </summary>
    public void ClickPerson()
    {
        personWnd.SetWndState();
    }
    public void EnterFriendWnd()
    {
        friendsWnd.SetWndState();
    }
    /// <summary>
    /// TODO修改刷新UI
    /// </summary>
    /// <param name="item"></param>
    public void FriendReFresh(FriendItem item=null)
    {
        friendsWnd. RefreshFriends(item);
    }

    public void EnterTalentWnd()
    {
        talentWnd.SetWndState();
    }

    public void ClickArena()
    {
        mainCityWnd.SetWndState(false);
        BattleSys.instance.SetBattleWnd();
    }
    #endregion
}