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
using XLua;
[LuaCallCSharp]
public class GameRoot : MonoBehaviour
{
    public static GameRoot Instance = null;
    public LoadingWnd loadingWnd;
    public DynamicWnd dynamicWnd;
    private Transform canvas;
    public Camera uiCamera;

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
        TimerSvc timerSvc = gameObject.GetOrAddComponent<TimerSvc>();
        timerSvc.InitSvc();
        NetSvc net = gameObject.GetOrAddComponent<NetSvc>();
        net.InitSvc();
        AssetLoaderSvc assetLoader = gameObject.GetOrAddComponent<AssetLoaderSvc>();
        assetLoader.InitSvc();
        //业务模块初始化
        DowningSys downing = gameObject.GetOrAddComponent<DowningSys>();
        downing.InitSyc();
        LoginSys login = gameObject.GetOrAddComponent<LoginSys>();
        login.InitSyc();
        MainCitySys mainCity = gameObject.GetOrAddComponent<MainCitySys>();
        mainCity.InitSyc();
        BattleSys battleSys = gameObject.GetOrAddComponent<BattleSys>();
        battleSys.InitSyc();
        dynamicWnd.SetWndState();//设置动态界面状态
        timerSvc.SendLocalTime();//发送本地时间
        downing.EnterDowning();
    }
    public void ResSvcInit()
    {
        ResSvc resSvc = gameObject.GetComponent<ResSvc>();
        resSvc.InitSvc();
    }
    /// <summary>
    /// XLua初始化
    /// </summary>
    public void XLuaRootInit()
    {
        GameObject XLuaRoot = new GameObject("XLuaRoot");
        XLuaRoot.transform.SetParent(transform);
        xLuaRoot xLuaRoot = XLuaRoot.GetOrAddComponent<xLuaRoot>();
        xLuaRoot.Init();
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
        canvas.worldCamera = uiCamera;
        // uiCamera.fieldOfView = Constants.CreatefieldOfView;
    }
    /// <summary>
    /// 设置canvas覆盖模式
    /// </summary>
    public void SetScreenSpaceOverlay()
    {
        Canvas canvas = this.canvas.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.worldCamera = uiCamera;
        Camera.main.fieldOfView = Constants.CamerafieldOfView;
    }
    public static void SetSocketFail(bool IsShow)
    {
        Instance.dynamicWnd.SetSocketFail(IsShow);
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