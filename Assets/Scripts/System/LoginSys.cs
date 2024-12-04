/****************************************************
    文件：LoginSys.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/13 23:8:36
	功能：登录系统
*****************************************************/

using CommonNet;

public class LoginSys : SystemRoot
{
    public static LoginSys instance;
    public LoginWnd loginWnd;
    public RegisterWnd registerWnd;
    public CreateWnd createWnd;

    public StartWnd startWnd;
    public override void InitSyc()
    {
        base.InitSyc();
        instance = this;
        SocketDispatcher.Instance.AddEventListener(CMD.RspRegister, RspRegister);
        SocketDispatcher.Instance.AddEventListener(CMD.RspLogin, RspLogin);
        SocketDispatcher.Instance.AddEventListener(CMD.RspCreateGame, RspCreateGame);
        GameCommon.Log("LoginSys Init....");
    }

    public void GameLogin()
    {
        StartCoroutine(loaderSvc.AsyncLoadScene(Constants.SceneLogin, () =>
        {
            EnterLogin();
            loaderSvc.CloseLoadingWnd();
        }));
    }

    public void RspRegister(GameMsg msg)
    {
        GameRoot.AddTips("该账号注册成功");
        if (msg.rspRegister.isSucc)
        {
            registerWnd.ClickClose();
        }
    }
    public void RspLogin(GameMsg msg)
    {
        GameRoot.AddTips("登录成功");
        if (msg.rspLogin.playerData.name == null)//判断该账号是否需要进入创建界面
        {
            loginWnd.SetWndState(false);
            createWnd.SetWndState();
        }
        else //TODO 进入主页
        {
            GameRoot.Instance.SetPlayerData(msg.rspLogin.playerData);//更新数据
            GameRoot.Instance.SetPlayerDataList(msg.rspLogin.playerList);//更新城镇所有玩家

            //loginWnd.imgbg.SetActive(false);
            loginWnd.SetWndState(false);
            //进入主城
            MainCitySys.instance.EnterMainCity();
        }
    }
    public void RspCreateGame(GameMsg msg)
    {
        GameRoot.AddTips("登录成功");
        GameRoot.Instance.SetPlayerData(msg.rspCreateGame.playerData);//更新数据
        GameRoot.Instance.SetPlayerDataList(msg.rspCreateGame.playerDataList);
        //loginWnd.imgbg.SetActive(false);
        createWnd.SetWndState(false);
        //进入主城
        MainCitySys.instance.EnterMainCity();
    }
    public void ReLoginClick()
    {
        loginWnd.LoginClick = true;
    }
    public void EnterRegis()
    {
        registerWnd.SetWndState();
    }
    public void EnterLogin()
    {
        loginWnd.SetWndState();
    }
    public void EnterStart()
    {
        startWnd.SetWndState();
    }
    //public Dictionary<string, ABInfo> GettmpABInfo()
    //{
    //    return downingWnd.tmpABInfo;
    //}


}