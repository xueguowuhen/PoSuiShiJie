/****************************************************
    文件：FriendSys
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-08 13:16:08
	功能：Nothing
*****************************************************/
using CommonNet;
using MySqlX.XDevAPI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        // 使用新的方法查找好友
        FriendItem friendData;
        // 验证好友请求的有效性
        if (!isValidFriendRequest(pack.session, searchFriend.name, playerData, gameMsg, out friendData))
        {
            return;
        }
        gameMsg.rspSearchFriend = new RspSearchFriend()
        {
            Friend = friendData,
        };

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
        // 使用新的方法查找好友
        FriendItem friendData;
        // 验证好友请求的有效性
        if (!isValidFriendRequest(pack.session, name, playerData, gameMsg, out friendData))
        {
            return;
        }
        //判断好友是否在线
        PlayerData targetPlayerData = cacheSvc.GetPlayerData(friendData.name);
        //判断是否已经是好友或者已经发送好友申请
        bool isFriendOrRequestPending = friendData.AddFriendList.Contains(playerData.id) || friendData.FriendList.Contains(playerData.id);
        if (isFriendOrRequestPending)
        {
            gameMsg.err = (int)Error.FriendRequestExistError; // 该好友已经在申请列表中
        }
        else
        {
            if (targetPlayerData != null) // 好友在线
            {
                HandleOnlineFriendRequest(playerData, targetPlayerData, gameMsg);
            }
            else // 好友离线
            {
                HandleOfflineFriendRequest(playerData, friendData, gameMsg);
            }

        }
        pack.session.SendMsg(gameMsg);
    }

    /// <summary>
    /// 验证好友请求的有效性
    /// </summary>
    private bool isValidFriendRequest(ServerSession session, string friendName, PlayerData playerData, GameMsg gameMsg, out FriendItem friendData)
    {
        friendData = cacheSvc.GetPlayerDataByFriendName(friendName);
        if (string.IsNullOrEmpty(friendName))
        {
            gameMsg.err = (int)Error.NotFriendError; // 无效的好友名
            session.SendMsg(gameMsg); // 发送错误信息给客户端
            return false;
        }

        if (friendName.Equals(playerData.name, StringComparison.OrdinalIgnoreCase))
        {
            gameMsg.err = (int)Error.FriendMeError; // 不能添加自己为好友
            session.SendMsg(gameMsg); // 发送错误信息给客户端
            return false;
        }
        if (friendData == null)
        {
            gameMsg.err = (int)Error.FriendNameError; // 该用户不存在
            session.SendMsg(gameMsg);
            return false; // 返回查找失败
        }
        return true;
    }
    /// <summary>
    /// 处理在线好友申请
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="targetPlayerData"></param>
    /// <param name="gameMsg"></param>
    /// <param name="isFriendOrRequestPending"></param>
    private void HandleOnlineFriendRequest(PlayerData playerData, PlayerData targetPlayerData, GameMsg gameMsg)
    {
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
                crystal=targetPlayerData.crystal,
                aura=targetPlayerData.aura,
                ruvia=targetPlayerData.ruvia,
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
    private void HandleOfflineFriendRequest(PlayerData playerData, FriendItem friendData, GameMsg gameMsg)
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
        #region 处理同意
        if (reqFriendAddConfirm.isAgree)//同意
        {
            string name = reqFriendAddConfirm.name;
            // 使用新的方法查找好友
            FriendItem friendData;
            // 验证好友请求的有效性
            if (!isValidFriendRequest(pack.session, name, playerData, gameMsg, out friendData))
            {
                return;
            }
            PlayerData targetPlayerData = cacheSvc.GetPlayerData(friendData.name);
            bool isRequestPending = friendData.FriendList.Contains(playerData.id);//判断好友列表中是否存在该id
            if (isRequestPending)
            {
                gameMsg.err = (int)Error.FriendExistError; // 该好友已经在申请列表中
                pack.session.SendMsg(gameMsg);
                return;
            }
            playerData.FriendList.Add(new FriendItem
            {
                id = friendData.id,
                name = friendData.name,
                type = friendData.type.ToString(),
                level = friendData.level,
            });
            if (targetPlayerData != null) // 好友在线
            {
                AddFriendsToList(playerData, targetPlayerData, gameMsg);
            }
            else // 好友离线
            {
                AddFriendWhenOffline(playerData, friendData, gameMsg);
            }
        }
        #endregion
        #region 处理拒绝
        else //拒绝
        {
            if (!cacheSvc.UpdatePlayerData(playerData))
            {
                gameMsg.err = (int)Error.FriendAddConfirmError; // 好友申请失败
            }
            gameMsg.rspFriendAddConfirm = new RspFriendAddConfirm()
            {
                isAgree = false,
                AddFriendList = playerData.AddFriendList,
                FriendList = playerData.FriendList,
            };
        }
        #endregion
        pack.session.SendMsg(gameMsg);
    }
    /// <summary>
    /// 添加在线好友到好友列表
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="targetPlayerData"></param>
    /// <param name="gameMsg"></param>
    private void AddFriendsToList(PlayerData playerData, PlayerData targetPlayerData, GameMsg gameMsg)
    {
        // 添加到目标好友列表
        targetPlayerData.FriendList.Add(new FriendItem
        {
            id = playerData.id,
            name = playerData.name,
            type = playerData.type.ToString(),
            level = playerData.level,
        });

        if (cacheSvc.UpdatePlayerData(targetPlayerData) && cacheSvc.UpdatePlayerData(playerData))
        {
            NotifyFriendRequest(targetPlayerData);
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
    /// <summary>
    /// 添加离线到好友列表
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="friendData"></param>
    /// <param name="gameMsg"></param>
    private void AddFriendWhenOffline(PlayerData playerData, FriendItem friendData, GameMsg gameMsg)
    {
        friendData.FriendList.Add(playerData.id);

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
    /// <summary>
    /// 删除好友
    /// </summary>
    /// <param name="pack"></param>
    public void ReqDelFriend(MsgPack pack)
    {
        ReqDelFriend reqDelFriend = pack.gameMsg.reqDelFriend;
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.RspDelFriend,
        };
        string name = reqDelFriend.name;
        // 使用新的方法查找好友
        FriendItem friendData;
        // 验证好友请求的有效性
        if (!isValidFriendRequest(pack.session, name, playerData, gameMsg, out friendData))
        {
            return;
        }
        bool isFriendOrRequestPending = friendData.FriendList.Contains(playerData.id) && playerData.FriendList.Any(f=>f.id==friendData.id);
        //该ID是否在好友列表中
        if (isFriendOrRequestPending)
        {
            //判断好友是否在线
            PlayerData targetPlayerData = cacheSvc.GetPlayerData(friendData.name);
            //删除好友
            playerData.FriendList.RemoveAll(f => f.id == friendData.id);
            if (targetPlayerData != null) // 好友在线
            {
                // 添加到目标好友列表
                targetPlayerData.FriendList.RemoveAll(f => f.id == playerData.id);

                if (cacheSvc.UpdatePlayerData(targetPlayerData) && cacheSvc.UpdatePlayerData(playerData))
                {
                    NotifyFriendRequest(targetPlayerData);
                    gameMsg.rspDelFriend = new RspDelFriend()
                    {
                        FriendList = playerData.FriendList,
                    };
                }
                else
                {
                    gameMsg.err = (int)Error.FriendDelError; // 好友删除失败
                }
            }
            else // 好友离线
            {
                //删除好友
                friendData.FriendList.RemoveAll(f => f == playerData.id);

                if (!cacheSvc.UpdateFriend(friendData) || !cacheSvc.UpdatePlayerData(playerData))
                {
                    gameMsg.err = (int)Error.FriendDelError; // 好友删除失败
                }
                else
                {
                    gameMsg.rspDelFriend = new RspDelFriend()
                    {
                        FriendList = playerData.FriendList,
                    };
                }
            }
        }
        else
        {
            gameMsg.err = (int)Error.FriendRequestError; // 好友申请失败
        }
        pack.session.SendMsg(gameMsg);
    }
    /// <summary>
    /// 赠送好友礼物
    /// </summary>
    /// <param name="pack"></param>
    public void ReqFriendGift(MsgPack pack)
    {
        ReqFriendGift reqFriendGift = pack.gameMsg.reqFriendGift;
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.RspFriendGift,
        };
        ref float gold = ref GetPlayerdaGold(playerData, reqFriendGift);
        if (gold < reqFriendGift.count)
        {
            gameMsg.err = (int)Error.GoldNotEnoughError; // 货币不足
            pack.session.SendMsg(gameMsg);
            return;
        }
        string name = reqFriendGift.name;
        // 使用新的方法查找好友
        FriendItem friendData;
        // 验证好友请求的有效性
        if (!isValidFriendRequest(pack.session, name, playerData, gameMsg, out friendData))
        {
            return;
        }
        bool isFriendOrRequestPending = friendData.FriendList.Contains(playerData.id);
        //该ID是否在好友列表中
        if (isFriendOrRequestPending)
        {
            //判断好友是否在线
            PlayerData targetPlayerData = cacheSvc.GetPlayerData(friendData.name);
            gold -= reqFriendGift.count;
            //赠送礼物
            if (targetPlayerData != null) // 好友在线
            {
                ref float targetGold = ref GetPlayerdaGold(targetPlayerData, reqFriendGift);
                targetGold += reqFriendGift.count;
                if (cacheSvc.UpdatePlayerData(targetPlayerData) && cacheSvc.UpdatePlayerData(playerData))
                {
                    NotifyFriendRequest(targetPlayerData);
                    gameMsg.rspFriendGift = new RspFriendGift()
                    {
                        aura = playerData.aura,
                        ruvia = playerData.ruvia,
                        crystal = playerData.crystal,
                        Bag = playerData.Bag,
                    };
                }
                else
                {
                    gameMsg.err = (int)Error.FriendGiftError; // 赠送礼物失败
                }
            }
            else // 好友离线
            {
                ref float targetGold = ref GetPlayerdaGold(friendData, reqFriendGift);
                targetGold+= reqFriendGift.count;

                if (!cacheSvc.UpdateFriend(friendData) || !cacheSvc.UpdatePlayerData(playerData))
                {
                    gameMsg.err = (int)Error.FriendGiftError; // 赠送礼物失败
                }
                else
                {
                    gameMsg.rspFriendGift = new RspFriendGift()
                    {
                        aura = playerData.aura,
                        ruvia = playerData.ruvia,
                        crystal = playerData.crystal,
                        Bag = playerData.Bag,
                    };
                }
            }
        }
        else
        {
            gameMsg.err = (int)Error.FriendNameError; // 好友申请失败
        }
        pack.session.SendMsg(gameMsg);
    }
    private ref float GetPlayerdaGold(PlayerData playerData, ReqFriendGift reqFriendGift)
    {
        switch (reqFriendGift.buyType)
        {
            case BuyType.aura: // 星晶
                return ref playerData.aura;
            case BuyType.ruvia: // 云晶
                return ref playerData.ruvia;
            case BuyType.crystal: // 彩晶
                return ref playerData.crystal;
        }
        return ref playerData.crystal;
    }
    private ref float GetPlayerdaGold(FriendItem friendItem, ReqFriendGift reqFriendGift)
    {
        switch (reqFriendGift.buyType)
        {
            case BuyType.aura: // 星晶
                return ref friendItem.aura;
            case BuyType.ruvia: // 云晶
                return ref friendItem.ruvia;
            case BuyType.crystal: // 彩晶
                return ref friendItem.crystal;
        }
        return ref friendItem.crystal;
    }
}

