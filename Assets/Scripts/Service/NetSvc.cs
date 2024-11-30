/****************************************************
    文件：NetSvc.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/13 22:38:16
	功能：网络加载模块
*****************************************************/

using CommonNet;
using ComNet;
using System;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
using UnityEngine.UI;


public class NetSvc : SvcBase<NetSvc>
{

    private readonly static string Lock = "lock";
    private Queue<GameMsg> Msgqueue = new Queue<GameMsg>();
    public TraSocket<ClientSession, GameMsg> client = null;
    private Queue<Action> extcutionActions = new Queue<Action>();
    private int sessionID=-1;
    public override void InitSvc()
    {
        base.InitSvc();
        StartAsClient();
        SocketDispatcher.Instance.AddEventListener(CMD.SystemSessionID, onSystemSessionID);
        GameCommon.Log("NetSvc Init....");
    }

    private void onSystemSessionID(GameMsg msg)
    {
        sessionID = msg.systemSessionID.SessionID;
        GameCommon.Log("sessionID:" + sessionID);
    }

    /// <summary>
    /// 启动客户端
    /// </summary>
    public void StartAsClient()
    {
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
        client.StartAsClient(IPCfg.srvIP, IPCfg.srvPort, sessionID);
    }
    public void SocketConnected(Action action)
    {
        extcutionActions.Enqueue(action);
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
    public void SendMsgAsync(GameMsg msg)
    {
        if (client.session != null)
        {
            client.session.SendMsgAsync(msg);
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

    public override void OnUpdate()
    {
        base.OnUpdate();
        if (Msgqueue.Count > 0)
        {
            lock (Lock)
            {
                GameMsg msg = Msgqueue.Dequeue();
                ProcessMsg(msg);
            }
        }
        if (extcutionActions.Count > 0)
        {
            lock (Lock)
            {
                Action action = extcutionActions.Dequeue();
                action();
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
                case Error.PerSonError:
                    GameRoot.AddTips("该角色不存在");
                    GameCommon.Log("该角色不存在", ComLogType.Error);
                    break;
                case Error.TalentError:
                    GameRoot.AddTips("天赋选择错误");
                    GameCommon.Log("天赋选择错误", ComLogType.Error);
                    break;
                case Error.AcctUpdateError:
                    GameRoot.AddTips("账号信息更新失败");
                    GameCommon.Log("账号信息更新失败", ComLogType.Error);
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
                case Error.TaskIDError://任务id错误
                    GameRoot.AddTips("任务id错误");
                    GameCommon.Log("任务id错误", ComLogType.Error);
                    break;
                case Error.DamageError://造成伤害异常
                    GameRoot.AddTips("造成伤害异常");
                    GameCommon.Log("造成伤害异常", ComLogType.Error);
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
                case Error.FriendRequestError://好友申请失败
                    GameRoot.AddTips("好友申请失败");
                    GameCommon.Log("好友申请失败", ComLogType.Error);
                    break;
                case Error.FriendAddConfirmError://好友拒绝失败
                    GameRoot.AddTips("好友拒绝失败");
                    GameCommon.Log("好友拒绝失败", ComLogType.Error);
                    break;
                case Error.FriendDelError://删除好友失败
                    GameRoot.AddTips("删除好友失败");
                    GameCommon.Log("删除好友失败", ComLogType.Error);
                    break;
                case Error.FriendGiftError://赠送失败
                    GameRoot.AddTips("赠送失败");
                    GameCommon.Log("赠送失败", ComLogType.Error);
                    break;
                case Error.GoldNotEnoughError://货币不足
                    GameRoot.AddTips("货币不足");
                    GameCommon.Log("货币不足", ComLogType.Error);
                    break;
                case Error.NameExistError://名字已存在
                    GameRoot.AddTips("名字已存在");
                    GameCommon.Log("名字已存在", ComLogType.Error);
                    break;
            }
            return;
        }
        SocketDispatcher.Instance.Dispatch((CMD)msg.cmd, msg);

    }
    private void OnDestroy()
    {
        //释放连接
        client.session.Shutdown();

    }
}