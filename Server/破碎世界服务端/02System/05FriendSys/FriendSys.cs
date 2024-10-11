/****************************************************
    文件：FriendSys
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-08 13:16:08
	功能：Nothing
*****************************************************/
using CommonNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using 墨染服务端._01Service._01NetSvc;

public class FriendSys
{
    private static FriendSys instance = null;
    public static FriendSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new FriendSys();
            }
            return instance;
        }
    }
    private CacheSvc cacheSvc;
    private CfgSvc cfgSvc;
    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        cfgSvc = CfgSvc.Instance;
        GameCommon.Log("FriendSys Init Done");
    }
    /// <summary>
    /// 搜索好友
    /// </summary>
    /// <param name="pack"></param>
    public void ReqSearchFriend(MsgPack pack)
    {
        ReqSearchFriend searchFriend = pack.gameMsg.reqSearchFriend;
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.RspSearchFriend,
        };
        // 检查好友名有效性
        if (!IsValidFriendRequest(searchFriend.name, playerData, gameMsg))
        {
            pack.session.SendMsg(gameMsg); // 发送错误信息给客户端
            return;
        }
        //查询该好友是否存在
        FriendItem friendData = cacheSvc.GetPlayerDataByFriendName(searchFriend.name);
        if (friendData == null)
        {
            gameMsg.err = (int)Error.FriendNameError;//该用户不存在
        }
        else
        {
            gameMsg.rspSearchFriend = new RspSearchFriend()
            {
                Friend = friendData,
            };
        }
        pack.session.SendMsg(gameMsg);

    }
    /// <summary>
    /// 发送好友申请
    /// </summary>
    /// <param name="pack"></param>
    public void ReqAddFriend(MsgPack pack)
    {
        ReqAddFriend reqAddFriend = pack.gameMsg.reqAddFriend;
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.RspAddFriend,
        };
        string name = reqAddFriend.name;
        // 检查好友名有效性
        if (!IsValidFriendRequest(name, playerData, gameMsg))
        {
            pack.session.SendMsg(gameMsg); // 发送错误信息给客户端
            return;
        }
        //根据ID查询查找好友是否存在
        FriendItem friendData = cacheSvc.GetPlayerDataByFriendName(name);
        if (friendData == null)
        {
            gameMsg.err = (int)Error.FriendNameError;//该用户不存在
            pack.session.SendMsg(gameMsg);
            return;
        }
        ProcessFriendRequest(playerData, friendData, gameMsg);
        pack.session.SendMsg(gameMsg);
    }
    /// <summary>
    /// 验证好友请求的有效性
    /// </summary>
    private bool IsValidFriendRequest(string friendName, PlayerData playerData, GameMsg gameMsg)
    {
        if (string.IsNullOrEmpty(friendName))
        {
            gameMsg.err = (int)Error.NotFriendError; // 无效的好友名
            return false;
        }

        if (friendName.Equals(playerData.name, StringComparison.OrdinalIgnoreCase))
        {
            gameMsg.err = (int)Error.FriendMeError; // 不能添加自己为好友
            return false;
        }

        return true;
    }
    /// <summary>
    /// 处理好友申请
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="friendData"></param>
    /// <param name="gameMsg"></param>
    private void ProcessFriendRequest(PlayerData playerData, FriendItem friendData, GameMsg gameMsg)
    {
        PlayerData targetPlayerData = cacheSvc.GetPlayerData(friendData.name);
        bool isFriendOrRequestPending = friendData.AddFriendList.Contains(playerData.id) || friendData.FriendList.Contains(playerData.id);

        if (targetPlayerData != null) // 好友在线
        {
            HandleOnlineFriendRequest(playerData, targetPlayerData, gameMsg, isFriendOrRequestPending);
        }
        else // 好友离线
        {
            HandleOfflineFriendRequest(playerData, friendData, gameMsg, isFriendOrRequestPending);
        }
    }
    /// <summary>
    /// 处理在线好友申请
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="targetPlayerData"></param>
    /// <param name="gameMsg"></param>
    /// <param name="isFriendOrRequestPending"></param>
    private void HandleOnlineFriendRequest(PlayerData playerData, PlayerData targetPlayerData, GameMsg gameMsg, bool isFriendOrRequestPending)
    {
        if (isFriendOrRequestPending)
        {
            gameMsg.err = (int)Error.FriendRequestExistError; // 该好友已经在申请列表中
            return;
        }

        // 添加到目标好友的申请列表
        targetPlayerData.AddFriendList.Add(new FriendItem
        {
            id = playerData.id,
            name = playerData.name,
            type = playerData.type.ToString(),
            level = playerData.level,
        });

        if (cacheSvc.UpdatePlayerData(targetPlayerData))
        {
            // 通知对方
            NotifyFriendRequest(targetPlayerData);
            gameMsg.rspAddFriend = new RspAddFriend { isSucc = true };
        }
        else
        {
            gameMsg.err = (int)Error.FriendRequestError; // 好友申请失败
        }
    }
    /// <summary>
    /// 通知对方好友申请
    /// </summary>
    /// <param name="targetPlayerData"></param>
    private void NotifyFriendRequest(PlayerData targetPlayerData)
    {
        GameMsg notifyMsg = new GameMsg
        {
            cmd = (int)CMD.RspAddFriend,
            rspAddFriend = new RspAddFriend
            {
                isSucc = false,
                AddFriendList = targetPlayerData.AddFriendList,
                FriendList = targetPlayerData.FriendList,
            }
        };
        ServerSession session = cacheSvc.GetSessionByPlayerData(targetPlayerData);
        session.SendMsg(notifyMsg);
    }
    /// <summary>
    /// 处理离线好友申请
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="friendData"></param>
    /// <param name="gameMsg"></param>
    /// <param name="isFriendOrRequestPending"></param>
    private void HandleOfflineFriendRequest(PlayerData playerData, FriendItem friendData, GameMsg gameMsg, bool isFriendOrRequestPending)
    {
        if (!isFriendOrRequestPending)
        {
            friendData.AddFriendList.Add(playerData.id);
            if (!cacheSvc.UpdateFriend(friendData))
            {
                gameMsg.err = (int)Error.FriendRequestError; // 好友申请失败
            }
            else
            {
                gameMsg.rspAddFriend = new RspAddFriend { isSucc = true };
            }
        }
        else
        {
            gameMsg.err = (int)Error.FriendRequestExistError; // 该好友已经在申请列表中
        }
    }
    /// <summary>
    /// 是否同意好友申请
    /// </summary>
    /// <param name="pack"></param>
    public void ReqFriendAddConfirm(MsgPack pack)
    {
        ReqFriendAddConfirm reqFriendAddConfirm = pack.gameMsg.reqFriendAddConfirm;
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.RspFriendAddConfirm,
        };

        // 删除与 reqFriendAddConfirm.id 相等的所有好友 ID
        playerData.AddFriendList.RemoveAll(f => f.id == reqFriendAddConfirm.id);

        if (reqFriendAddConfirm.isAgree)//同意
        {
            string name = reqFriendAddConfirm.name;
            // 检查好友名有效性
            if (!IsValidFriendRequest(name, playerData, gameMsg))
            {
                pack.session.SendMsg(gameMsg); // 发送错误信息给客户端
                return;
            }
            //根据ID查询查找好友是否存在
            FriendItem friendData = cacheSvc.GetPlayerDataByFriendName(name);
            if (friendData == null)
            {
                gameMsg.err = (int)Error.FriendNameError;//该用户不存在
            }
            else
            {
                PlayerData targetPlayerData = cacheSvc.GetPlayerData(friendData.name);
                bool isRequestPending = friendData.FriendList.Contains(playerData.id);//判断好友列表中是否存在该id

                if (targetPlayerData != null) // 好友在线
                {
                    if (isRequestPending)
                    {
                        gameMsg.err = (int)Error.FriendExistError; // 该好友已经在列表中
                    }
                    else
                    {
                        // 添加到目标好友列表
                        targetPlayerData.FriendList.Add(new FriendItem
                        {
                            id = playerData.id,
                            name = playerData.name,
                            type = playerData.type.ToString(),
                            level = playerData.level,
                        });
                        playerData.FriendList.Add(new FriendItem
                        {
                            id = targetPlayerData.id,
                            name = targetPlayerData.name,
                            type = targetPlayerData.type.ToString(),
                            level = targetPlayerData.level,
                        });
                        if (cacheSvc.UpdatePlayerData(targetPlayerData) && cacheSvc.UpdatePlayerData(playerData))
                        {
                            // 向对方传输更新后的数据
                            GameMsg notifyMsg = new GameMsg
                            {
                                cmd = (int)CMD.RspAddFriend,
                                rspAddFriend = new RspAddFriend
                                {
                                    isSucc = false,
                                    AddFriendList = targetPlayerData.AddFriendList,
                                    FriendList = targetPlayerData.FriendList,
                                }
                            };
                            ServerSession session = cacheSvc.GetSessionByPlayerData(targetPlayerData);
                            session.SendMsg(notifyMsg);
                            gameMsg.rspFriendAddConfirm = new RspFriendAddConfirm
                            {
                                isAgree = true,
                                AddFriendList = playerData.AddFriendList,
                                FriendList = playerData.FriendList,
                            };
                        }
                        else
                        {
                            gameMsg.err = (int)Error.FriendRequestError; // 好友申请失败
                        }
                    }
                }
                else // 好友离线
                {
                    if (!isRequestPending)
                    {
                        friendData.FriendList.Add(playerData.id);
                        playerData.FriendList.Add(new FriendItem
                        {
                            id = friendData.id,
                            name = friendData.name,
                            type = friendData.type.ToString(),
                            level = friendData.level,
                        });
                        if (!cacheSvc.UpdateFriend(friendData) || !cacheSvc.UpdatePlayerData(playerData))
                        {
                            gameMsg.err = (int)Error.FriendRequestError; // 好友申请失败
                        }
                        else
                        {
                            gameMsg.rspFriendAddConfirm = new RspFriendAddConfirm
                            {
                                isAgree = true,
                                AddFriendList = playerData.AddFriendList,
                                FriendList = playerData.FriendList,
                            };
                        }
                    }
                    else
                    {
                        gameMsg.err = (int)Error.FriendRequestExistError; // 该好友已经在申请列表中
                    }
                }
            }
        }
        else//拒绝
        {
            if (!cacheSvc.UpdatePlayerData(playerData))
            {
                gameMsg.err = (int)Error.friendAddConfirmError; // 好友申请失败
            }
            gameMsg.rspFriendAddConfirm = new RspFriendAddConfirm()
            {
                isAgree = false,
                AddFriendList = playerData.AddFriendList,
                FriendList = playerData.FriendList,
            };
        }
        pack.session.SendMsg(gameMsg);
    }
    public void ReqDelFriend(MsgPack pack)
    {

    }
    public void ReqFriendGift(MsgPack pack)
    {

    }
}

