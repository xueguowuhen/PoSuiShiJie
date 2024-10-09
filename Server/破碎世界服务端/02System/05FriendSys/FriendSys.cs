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
        string name = searchFriend.name;
        if (string.IsNullOrEmpty(name))
        {
            gameMsg.err = (int)Error.NotFriendError;//无效的好友名
        }
        else if (name == playerData.name)
        {
            gameMsg.err = (int)Error.FriendMeError;//不能添加自己为好友
        }
        else
        {
            //根据ID查询查找好友是否存在
            FriendItem friendData = cacheSvc.GetPlayerDataByFriendName(name);
            if (friendData == null)
            {
                gameMsg.err = (int)Error.FriendNameError;//该用户不存在
            }
            if (friendData.id == playerData.FriendList.Find(f => f.name == name).id)
            {
                gameMsg.err = (int)Error.FriendExistError;//该好友已经存在
            }
            else if (friendData.id == playerData.AddFriendList.Find(f => f.name == name).id)
            {
                gameMsg.err = (int)Error.FriendRequestExistError;//该好友已经在添加列表中
            }
            else
            {
                gameMsg.rspSearchFriend = new RspSearchFriend()
                {
                    Friend = friendData,
                };

            }
        }
        pack.session.SendMsg(gameMsg);

    }
    public void ReqAddFriend(MsgPack pack)
    {

    }
    public void ReqDelFriend(MsgPack pack)
    {

    }
    public void ReqFriendGift(MsgPack pack)
    {

    }
}

