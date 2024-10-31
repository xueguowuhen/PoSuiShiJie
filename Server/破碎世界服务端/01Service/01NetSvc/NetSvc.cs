/****************************************************
    文件：NetSvc
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-15 21:18:03
	功能：网络服务
*****************************************************/
using CommonNet;
using ComNet;
using 墨染服务端._01Service._01NetSvc;

public class MsgPack
{
    public ServerSession session;
    public GameMsg gameMsg;
    public MsgPack(ServerSession serverSession, GameMsg gameMsg)
    {
        this.session = serverSession;
        this.gameMsg = gameMsg;
    }
}

public class NetSvc
{
    private static NetSvc instance = null!;
    private static readonly string Lock = "lock";
    private Queue<MsgPack> queue = new Queue<MsgPack>();
    private TraSocket<ServerSession, GameMsg> traSocket = null!;
    public static NetSvc Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new NetSvc();
            }
            return instance;
        }
    }
    public void Init()
    {
        TraSocket < ServerSession, GameMsg > socket = new TraSocket<ServerSession, GameMsg>();
        socket.StartAsServer(IPCfg.srvIP, IPCfg.srvPort);
        GameCommon.Log("NetSvc Init Done");
    }
    /// <summary>
    /// 添加消息到队列中
    /// </summary>
    /// <param name="serverSession"></param>
    /// <param name="gameMsg"></param>
    public void AddMsgQue(ServerSession serverSession, GameMsg gameMsg)
    {
        lock (Lock)
        {
            queue.Enqueue(new MsgPack(serverSession, gameMsg));
        }
    }
    public void Update()
    {
        if (queue.Count > 0)
        {
            lock (Lock)
            {
                MsgPack msgPack = queue.Dequeue();
                HandOutMsg(msgPack);
            }
        }
    }
    private void HandOutMsg(MsgPack pack)
    {
        switch ((CMD)pack.gameMsg.cmd)
        {
            case CMD.ReqRegister:
                LoginSys.Instance.ReqRegister(pack);
                break;
            case CMD.ReqLogin:
                LoginSys.Instance.ReqLogin(pack);
                break;
            case CMD.ReqCreateGame:
                LoginSys.Instance.ReqCreateGame(pack);
                break;
            case CMD.ReqShop:
                ShopSys.Instance.ReqShop(pack);
                break;
            case CMD.ReqSearchFriend:
                FriendSys.Instance.ReqSearchFriend(pack);
                break;
            case CMD.ReqAddFriend:
                FriendSys.Instance.ReqAddFriend(pack);
                break;
            case CMD.ReqFriendAddConfirm:
                FriendSys.Instance.ReqFriendAddConfirm(pack);
                break;
            case CMD.ReqDelFriend:
                FriendSys.Instance.ReqDelFriend(pack);
                break;
            case CMD.ReqFriendGift:
                FriendSys.Instance.ReqFriendGift(pack);
                break;
            case CMD.ReqTask:
                TaskSys.Instance.ReqTask(pack);
                break;
            case CMD.ReqDailyTask:
                DailyTaskSys.Instance.ReqDailyTask(pack);
                break;
            case CMD.ReqRewardTask:
                DailyTaskSys.Instance.ReqRewardTask(pack);
                break;
            case CMD.ReqTransform:
                BattleSys.Instance.ReqTransform(pack);
                break;
            case CMD.ReqState:
                BattleSys.Instance.ReqState(pack);
                break;
            case CMD.ReqDamage:
                BattleSys.Instance.ReqDamage(pack);
                break;
            case CMD.ReqRevive:
                BattleSys.Instance.ReqRevive(pack);
                break;
            case CMD.ReqTalentUp:
                TalentSys.Instance.ReqTalentUpHandle(pack);
                break;
            case CMD.ReqChangeTalent:
                TalentSys.Instance.ReqChangeTalentHandle(pack);
                break;
            case CMD.ReqEnterPVP:
                BattleSys.Instance.ReqEnterPVP(pack);
                break;
            case CMD.ReqExitPVP:
                BattleSys.Instance.ReqExitPVP(pack);
                break;
            case CMD.ReqRecover:
                BattleSys.Instance.ReqRecover(pack);
                break;
        }
    }

}