/****************************************************
    文件：BattleMgr
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 9:29:52
	功能：战斗管理器
*****************************************************/
using CommonNet;
using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Reflection;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class BattleMgr : MonoBehaviour
{
    private ResSvc resSvc;
    private AudioSvc audioSvc;
    private StateMgr stateMgr;
    private SkillMgr skillMgr;
    private MapCfg mapCfg;
    public EntityPlayer entitySelfPlayer;
    public CharacterController characterController;
    public PlayerController playerController;
    public CameraPlayerCtrl cameraPlayerCtrl;
    public Dictionary<int, EntityBase> RemoteEntityDic = new Dictionary<int, EntityBase>();
    public GameObject player;
    public NavMeshAgent navMesh;
    private Vector2 dir;
    /// <summary>
    /// 初始化地图
    /// </summary>
    public void Init(MapCfg mapData, Action cb = null)
    {
        resSvc = ResSvc.instance;
        audioSvc = AudioSvc.instance;
        stateMgr = gameObject.AddComponent<StateMgr>();
        stateMgr.Init();
        skillMgr = gameObject.AddComponent<SkillMgr>();
        skillMgr.BattleMgr = this;
        skillMgr.Init();
        LoadPlayer(mapData);
        entitySelfPlayer.Idle();//创建完成后进入idle状态
        //RemotePlayer();
    }
    #region 玩家与远程玩家初始化操作
    private void LoadPlayer(MapCfg mapData)
    {
        PlayerData playerData = GameRoot.Instance.PlayerData;
        personCfg personCfg = resSvc.GetPersonCfgData(playerData.type);
        player = resSvc.LoadPrefab(PathDefine.ResPerson + personCfg.Prefab);
        // player = resSvc.ABLoadPrefab(personCfg.Prefab, personCfg.Prefab); ;
        // 在移动之前禁用角色控制器
        characterController = player.GetComponent<CharacterController>();
        //相机初始化
        cameraPlayerCtrl = GameObject.Find("CameraFollowAndRotate").GetOrAddComponent<CameraPlayerCtrl>();
        cameraPlayerCtrl.Init(mapData, player.transform);
        //人物初始化
        playerController = player.GetComponent<PlayerController>();
        characterController.enabled = false;
        player.transform.position = mapData.playerBornPos;
        // 在移动之后启用角色控制器
        characterController.enabled = true;
        playerController.transform.localEulerAngles = mapData.playerBornRote;
        playerController.isLocal = true;
        playerController.Init();
        playerController.RemotePlayerId = playerData.id;
        //初始化自动寻路
        navMesh = player.GetComponent<NavMeshAgent>();
        navMesh.enabled = false;
        //初始化生物UI
        //GameRoot.Instance.dynamicWnd.AddHptemInfo(playerData.id, player.transform);
        entitySelfPlayer = new EntityPlayer
        {
            stateMgr = stateMgr,
            skillMgr = skillMgr,
            playerData = GameRoot.Instance.PlayerData,
            isLocal = true,
            CurrentEntityType = EntityType.Player,
            battleMgr = this,
        };
        entitySelfPlayer.SetCtrl(playerController);
    }
    /// <summary>
    /// 加载远程人物
    /// </summary>
    private void RemotePlayer()
    {
        foreach (PlayerData playerData in GameRoot.Instance.PlayerDataList)
        {
            SetRemoteEntity(playerData);
        }
    }
    public void SetRemoteEntity(PlayerData playerData)
    {
        personCfg personCfg = resSvc.GetPersonCfgData(playerData.type);
        GameObject player = resSvc.LoadPrefab(PathDefine.ResPerson + personCfg.Prefab);

        PlayerController controller = player.GetComponent<PlayerController>();
        controller.RemotePlayerId = playerData.id;

        // 在移动之前禁用角色控制器
        CharacterController charTroller = player.GetComponent<CharacterController>();
        charTroller.enabled = false;
        player.transform.position = GetPlayerPos(playerData);
        // 在移动之后启用角色控制器
        charTroller.enabled = true;
        controller.transform.rotation = GetPlayerRot(playerData);
        controller.Init();
        navMesh = player.GetComponent<NavMeshAgent>();
        navMesh.enabled = false;
        BattleMgr battleMgr = BattleSys.instance.GetBattleMgr();
        //初始化生物UI
        GameRoot.Instance.dynamicWnd.AddHptemInfo(playerData.id, player.transform);
        EntityPlayer entityPlayer = new EntityPlayer
        {
            stateMgr = battleMgr.GetStateMgr(),
            skillMgr = battleMgr.GetSkillMgr(),
            CurrentEntityType = EntityType.Player,
            battleMgr = battleMgr,
            playerData = playerData,
        };
        entityPlayer.SetCtrl(controller);
        if (!RemoteEntityDic.ContainsKey(playerData.id))
        {
            RemoteEntityDic.Add(playerData.id, entityPlayer);
        }
        //远程人物初始化状态
        AniState aniState = (AniState)playerData.AniState;
        string AniName = Enum.GetName(typeof(AniState), aniState);
        //Debug.Log(AniName);
        MethodInfo mi = entityPlayer.GetType().GetMethod(AniName);
        if (mi != null)
        {
            if (AniName == "Attack")
            {
                object[] objects = new object[] { playerData.SkillID };
                mi.Invoke(entityPlayer, objects);
            }
            else
            {
                mi.Invoke(entityPlayer, null);
            }
        }
    }
    #endregion
    #region 玩家操作控制
    public void SetSelfPlayerMove(Vector2 dir, bool IsRun = false)
    {
        this.dir = dir;
        if (entitySelfPlayer.canControl == false)
        {
            return;
        }
        AniState aniState = entitySelfPlayer.currentAniState;
        if (aniState == AniState.Idle || aniState == AniState.Move)
        {
            if (dir == Vector2.zero)
            {

                //设置玩家移动
                entitySelfPlayer.Idle();
            }
            else
            {
                entitySelfPlayer.Move();

            }
            entitySelfPlayer.SetDir(dir);
        }
    }
    public void SetCamMove(Vector2 dir)
    {
        cameraPlayerCtrl.SetDir(dir);
    }
    #endregion
    #region 玩家攻击操作
    public double lastAtkTime = 0;
    public int comboIndex = 0;
    public double nowAtkTime;
    /// <summary>
    /// 普通攻击 
    /// </summary>
    public void ReleaseNormal()
    {
        PlayerData pd = GameRoot.Instance.PlayerData;
        personCfg personCfg = resSvc.GetPersonCfgData(pd.type);
        List<int> NormalAtkList = personCfg.NormalAtkList;
        AniState aniState = entitySelfPlayer.currentAniState;
        if (entitySelfPlayer.currentAniState == AniState.Attack)
        {
            //获取当前时间
            nowAtkTime = TimerSvc.Instance.GetNwTime();
            if (nowAtkTime - lastAtkTime < Constants.ComboSpace && lastAtkTime != 0)
            {
                if (NormalAtkList[comboIndex] != NormalAtkList[NormalAtkList.Count - 1])
                {
                    comboIndex += 1;
                    entitySelfPlayer.comboQue.Enqueue(NormalAtkList[comboIndex]);
                    lastAtkTime = nowAtkTime;
                }
                else
                {
                    comboIndex = 0;//最后一次连招则重置
                    lastAtkTime = 0;
                }
            }
        }

        else if (aniState == AniState.Idle || aniState == AniState.Move)
        {
            comboIndex = 0;
            lastAtkTime = TimerSvc.Instance.GetNwTime();
            entitySelfPlayer.Attack(NormalAtkList[comboIndex]);
        }

    }
    /// <summary>
    /// 技能攻击1
    /// </summary>
    public void ReleaseSkill1()
    {
        AniState aniState = entitySelfPlayer.currentAniState;
        if (aniState == AniState.Idle || aniState == AniState.Move)
        {

            PlayerData pd = GameRoot.Instance.PlayerData;

            personCfg personCfg = resSvc.GetPersonCfgData(pd.type);
            if (personCfg != null)
            {
                int skillID = personCfg.SkillList[0];
                entitySelfPlayer.Attack(skillID);
            }
        }
    }
    /// <summary>
    /// 技能攻击2
    /// </summary>
    public void ReleaseSkill2()
    {
        AniState aniState = entitySelfPlayer.currentAniState;
        if (aniState == AniState.Idle || aniState == AniState.Move)
        {

            PlayerData pd = GameRoot.Instance.PlayerData;

            personCfg personCfg = resSvc.GetPersonCfgData(pd.type);
            if (personCfg != null)
            {
                int skillID = personCfg.SkillList[1];
                entitySelfPlayer.Attack(skillID);
            }
        }
    }
    /// <summary>
    /// 技能攻击3
    /// </summary>
    public void ReleaseSkill3()
    {
        AniState aniState = entitySelfPlayer.currentAniState;
        if (aniState == AniState.Idle || aniState == AniState.Move)
        {

            PlayerData pd = GameRoot.Instance.PlayerData;

            personCfg personCfg = resSvc.GetPersonCfgData(pd.type);
            if (personCfg != null)
            {
                int skillID = personCfg.SkillList[2];
                entitySelfPlayer.Attack(skillID);
            }
        }
    }
    /// <summary>
    /// 闪避
    /// </summary>
    public void Evade()
    {
        if (entitySelfPlayer.currentAniState == AniState.Attack) return;
        entitySelfPlayer.Evade();
    }
    #endregion
    /// <summary>
    /// 获取场上玩家数量
    /// </summary>
    /// <returns></returns>
    public List<EntityBase> GetPlayerList()
    {
        List<EntityBase> list = new List<EntityBase>();
        foreach (EntityBase Player in RemoteEntityDic.Values)
        {
            list.Add(Player);
        }
        //list.Add(player.transform.GetChild(0).gameObject);
        return list;
    }
    public EntityBase GetEntity(int playerid)
    {
        EntityBase entity = null;
        if (RemoteEntityDic.TryGetValue(playerid, out entity))
        {
            return entity;
        }
        if (entitySelfPlayer.playerData.id == playerid)
        {
            return entitySelfPlayer;
        }
        return null;
    }
    /// <summary>
    /// 返回玩家状态
    /// </summary>
    /// <param name="msg"></param>
    public void RspState(GameMsg msg)
    {
        RspPlayerState rspState = msg.rspPlayerState;
        foreach (EntityPlayer Player in RemoteEntityDic.Values)
        {
            if (Player.playerData.id == rspState.PlayerID)//根据id切换状态
            {
                // Player.playerData.AniState = rspState.AniState;
                AniState aniState = (AniState)rspState.AniState;
                string AniName = Enum.GetName(typeof(AniState), aniState);
                MethodInfo mi = Player.GetType().GetMethod(AniName);
                if (mi != null)
                {
                    if (AniName == "Attack")
                    {

                        object[] objects = new object[] { rspState.SkillID };
                        mi.Invoke(Player, objects);
                    }
                    else
                    {
                        mi.Invoke(Player, null);
                    }
                }
            }
        }
    }
    public StateMgr GetStateMgr()
    {
        return stateMgr;
    }
    /// <summary>
    /// 设置技能限制
    /// </summary>
    /// <returns></returns>
    public bool CanRlsSkill()
    {
        return entitySelfPlayer.canRlskill;
    }
    public SkillMgr GetSkillMgr()
    {
        return skillMgr;
    }
    public Vector2 GetDirInput()
    {
        return BattleSys.instance.GetDirInput();
    }
    public bool GetRunState()
    {
        return BattleSys.instance.GetRunState();
    }
    //public List<GameObject> GetPlayerList()
    //{
    //    return BattleSys.instance.GetPlayerList();
    //}
    public void SetDamageState(PlayerData playerData, RspDamage rspDamage)
    {
        if (RemoteEntityDic.TryGetValue(playerData.id, out var entityPlayer))
        {
            entityPlayer.SetDamage(playerData, rspDamage);
        }
        else
        {
            entitySelfPlayer.SetDamage(playerData, rspDamage);
        }
    }
    public Vector3 GetPlayerPos(PlayerData playerData)
    {
        return new Vector3(playerData.Pos_X, playerData.Pos_Y, playerData.Pos_Z);
    }
    public Quaternion GetPlayerRot(PlayerData playerData)
    {
        return Quaternion.Euler(new Vector3(playerData.Rot_X, playerData.Rot_Y, playerData.Rot_Z));
    }
}
