/****************************************************
    文件：ServerRoot
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-15 20:27:29
	功能：游戏服务端入口
*****************************************************/


using MySqlX.XDevAPI;

public class ServerRoot
{
    private static ServerRoot instance = null;
    public static ServerRoot Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ServerRoot();
            }
            return instance;
        }
    }
    public void Init()
    {
        //数据层
        DBMgr.Instance.Init();
        CacheSvc.Instance.Init();
        CfgSvc.Instance.Init();
        TimerSvc.Instance.Init();
        //服务层
        NetSvc.Instance.Init();
        //业务逻辑层
        LoginSys.Instance.Init();
        ShopSys.Instance.Init();
        TaskSys.Instance.Init();
        BattleSys.Instance.Init();
        FriendSys.Instance.Init();
        
    }
    public void Update()
    {
        NetSvc.Instance.Update();
        TimerSvc.Instance.Update();
    }
    private int SessionID = 0;
    //获取用户ID编号
    public int GetSessionID()
    {
        if (SessionID == int.MinValue)
        {
            SessionID = 0;
        }
        return SessionID += 1;
    }
}

