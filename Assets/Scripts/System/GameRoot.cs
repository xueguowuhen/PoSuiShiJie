/****************************************************
    文件：GameRoot.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/13 22:35:18
	功能：游戏启动入口
*****************************************************/

using CommonNet;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance = null;
    public LoadingWnd loadingWnd;
    public DynamicWnd dynamicWnd;
    private Transform canvas;
    private void Start()
    {
        Instance = this;
        Screen.SetResolution(1920, 1080, false);
        Application.runInBackground = true;
        DontDestroyOnLoad(this);
        GameCommon.Log("Game Start....");
        ClearUIRoot();
        Init();
    }
    private void ClearUIRoot()
    {
        canvas = transform.Find("Canvas");
        for (int i = 0; i < canvas.childCount; i++)
        {
            canvas.GetChild(i).gameObject.SetActive(false);//把子物体变为false
        }
    }
    private void Init()
    {
        TimerSvc timerSvc = gameObject.GetComponent<TimerSvc>();
        timerSvc.InitSvc();
        NetSvc net = gameObject.GetComponent<NetSvc>();
        net.InitSyc();
        ResSvc resSvc = gameObject.GetComponent<ResSvc>();
        resSvc.InitSyc();
        //业务模块初始化
        LoginSys login = gameObject.GetComponent<LoginSys>();
        login.InitSyc();
        MainCitySys mainCity = gameObject.GetComponent<MainCitySys>();
        mainCity.InitSyc();
        BattleSys battleSys = gameObject.GetComponent<BattleSys>();
        battleSys.InitSyc(); 
        dynamicWnd.SetWndState();
        //进入登录界面
        login.EnterDowing();
    }
    public static void AddTips(string tips)
    {
        Instance.dynamicWnd.AddTips(tips);
    }

    /// <summary>
    /// 设置canvas相机模式
    /// </summary>
    public void SetScreenSpaceCamera()
    {
        Canvas canvas = this.canvas.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceCamera;
        canvas.worldCamera = Camera.main;
        Camera.main.fieldOfView = Constants.CreatefieldOfView;
    }
    /// <summary>
    /// 设置canvas覆盖模式
    /// </summary>
    public void SetScreenSpaceOverlay()
    {
        Canvas canvas = this.canvas.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        Camera.main.fieldOfView = Constants.CamerafieldOfView;
    }

    private PlayerData playerData = null;
    public PlayerData PlayerData
    { get { return playerData; } set { playerData = value; } }
    private List<PlayerData> playerDataList = null;
    public List<PlayerData> PlayerDataList
    {
        get { return playerDataList; }
        set { playerDataList = value; }
    }

    public void SetPlayerData(PlayerData playerData)
    {
        this.playerData = playerData;
    }
    public void SetPlayerData(CommonNet.BattleData battleData)
    {
        this.playerData.Hp = battleData.Hp;
        this.playerData.Hpmax = battleData.Hpmax;
        this.playerData.critical = battleData.critical;
        this.playerData.Mana = battleData.Mana;
        this.playerData.ManaMax = battleData.ManaMax;
        this.playerData.ad = battleData.ad;
        this.playerData.addef = battleData.addef;
        this.playerData.ap = battleData.ap;
        this.playerData.apdef = battleData.apdef;
        this.playerData.dodge = battleData.dodge;
    }
    public void SetPlayerDataList(List<PlayerData> playerDataList)
    {
        this.playerDataList = playerDataList;
    }

}