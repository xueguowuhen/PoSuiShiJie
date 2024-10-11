/****************************************************
    文件：NetSvc.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/13 22:38:16
	功能：网络加载模块
*****************************************************/

using CommonNet;
using ComNet;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;


public class NetSvc : MonoBehaviour
{
    public static NetSvc instance;
    private readonly static string Lock = "lock";
    private Queue<GameMsg> Msgqueue = new Queue<GameMsg>();
    public TraSocket<ClientSession, GameMsg> client = null;
    public void InitSyc()
    {
        instance = this;
        client = new TraSocket<ClientSession, GameMsg>();
        client.SetLog(true, (string msg, int lv) =>
        {
            switch (lv)
            {
                case 0:
                    msg = "Log:" + msg;
                    Debug.Log(msg);
                    break;
                case 1:
                    msg = "Warn:" + msg;
                    Debug.LogWarning(msg);
                    break;
                case 2:
                    msg = "Error" + msg;
                    Debug.LogError(msg);
                    break;
                case 3:
                    msg = "Info:" + msg;
                    Debug.Log(msg);
                    break;
            }
        });
        client.StartAsClient(IPCfg.srvIP, IPCfg.srvPort);
        GameCommon.Log("NetSvc Init....");
    }
    public void SendMsg(GameMsg msg)
    {
        if (client.session != null)
        {
            client.session.SendMsg(msg);
        }
        else
        {
            GameCommon.Log("发送错误");
        }
    }
    public void AddMsgQue(GameMsg msg)
    {
        lock (Lock)
        {
            Msgqueue.Enqueue(msg);
        }
    }
    public void Update()
    {
        if (Msgqueue.Count > 0)
        {
            lock (Lock)
            {
                GameMsg msg = Msgqueue.Dequeue();
                ProcessMsg(msg);
            }
        }
    }
    private void ProcessMsg(GameMsg msg)
    {
        if (msg.err != (int)Error.None)
        {
            switch ((Error)msg.err)
            {
                case Error.RegisterError:
                    //账号注册失败
                    GameRoot.AddTips("账号注册失败");
                    GameCommon.Log("账号注册失败", ComLogType.Error);
                    break;
                case Error.AcctExistError:
                    GameRoot.AddTips("账号已存在");
                    GameCommon.Log("账号已存在", ComLogType.Error);
                    break;
                case Error.LoginExistError:
                    GameRoot.AddTips("不存在该账号");
                    LoginSys.instance.ReLoginClick();
                    GameCommon.Log("不存在该账号", ComLogType.Error);
                    break;
                case Error.LoginInvalidError:
                    GameRoot.AddTips("账号或密码无效");
                    LoginSys.instance.ReLoginClick();
                    GameCommon.Log("账号或密码无效", ComLogType.Error);
                    break;
                case Error.NotGoodError:
                    GameRoot.AddTips("没有该物品购买失败");
                    GameCommon.Log("没有该物品ID", ComLogType.Error);
                    break;
                case Error.NotAuraError:
                    GameRoot.AddTips("星晶不足购买失败");
                    GameCommon.Log("星晶不足购买失败", ComLogType.Error);
                    break;
                case Error.NotRuviaError:
                    GameRoot.AddTips("云晶不足购买失败");
                    GameCommon.Log("云晶不足购买失败", ComLogType.Error);
                    break;
                case Error.NotCrystalError:
                    GameRoot.AddTips("彩晶不足购买失败");
                    GameCommon.Log("彩晶不足购买失败", ComLogType.Error);
                    break;
                case Error.NotFriendError://好友不存在
                    GameRoot.AddTips("好友不存在");
                    GameCommon.Log("好友不存在", ComLogType.Error);
                    break;
                case Error.FriendMeError://不能添加自己为好友
                    GameRoot.AddTips("不能添加自己为好友");
                    GameCommon.Log("不能添加自己为好友", ComLogType.Error);
                    break;
                case Error.FriendNameError://该用户不存在
                    GameRoot.AddTips("该用户不存在");
                    GameCommon.Log("该用户不存在", ComLogType.Error);
                    break;
                case Error.FriendRequestExistError://已经申请过该好友
                    GameRoot.AddTips("已经申请过该好友");
                    GameCommon.Log("已经申请过该好友", ComLogType.Error);
                    break;
                case Error.FriendExistError://好友已存在
                    GameRoot.AddTips("好友已存在");
                    GameCommon.Log("好友已存在", ComLogType.Error);
                    break;
            }
            return;
        }
        switch ((CMD)msg.cmd)
        {
            case CMD.RspRegister:
                LoginSys.instance.RspRegister(msg);
                break;
            case CMD.RspLogin:
                LoginSys.instance.RspLogin(msg);
                break;
            case CMD.RspCreateGame:
                LoginSys.instance.RspCreateGame(msg);
                break;
            case CMD.RspShop:
                MainCitySys.instance.RspShop(msg);
                break;
            case CMD.RspTask:
                MainCitySys.instance.RspTask(msg);
                break;
            case CMD.RspSearchFriend:
                MainCitySys.instance.RspSearchFriend(msg);
                break;
            case CMD.RspAddFriend:
                MainCitySys.instance.RspAddFriend(msg);
                break;
            case CMD.RspFriendGift:
                MainCitySys.instance.RspFriendGift(msg);
                break;
            case CMD.RspFriendAddConfirm:
                MainCitySys.instance.RspFriendAddConfirm(msg);
                break;
            case CMD.RspDelFriend:
                MainCitySys.instance.RspDelFriend(msg);
                break;

            case CMD.RspCreatePlayer:
                BattleSys.instance.RspCreatePlayer(msg);
                break;
            case CMD.RspDeletePlayer:
                BattleSys.instance.RspDeletePlayer(msg);
                break;
            case CMD.RspTransform:
                BattleSys.instance.RspTransform(msg);
                break;
            case CMD.RspDamage:
                BattleSys.instance.RspDamage(msg);
                break;
            case CMD.RspState:
                BattleSys.instance.RspState(msg);
                break;
            case CMD.RspRevive:
                BattleSys.instance.RspRevive(msg);
                break;
        }
    }
    private void OnDestroy()
    {
        //释放连接
        client.session.Shutdown();
    }
}