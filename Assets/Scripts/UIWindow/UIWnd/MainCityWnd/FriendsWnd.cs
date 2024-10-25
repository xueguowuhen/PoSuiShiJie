/****************************************************
    文件：FriendsWnd.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/8 13:5:53
    功能：好友界面
*****************************************************/
using CommonNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public class FriendsWnd : WindowRoot
{
    public Button FriendBtn;
    public Button AddBtn;
    public Button FriendsListBtn;
    public GameObject FriendObj;
    public GameObject FriendContent;
    public GameObject AddObj;
    public GameObject AddContent;
    public GameObject FriendsListObj;
    public GameObject FriendsListContent;
    public GameObject pointer;
    public InputField FriendName;
    public Button SearchBtn;
    public Button CloseBtn;
    public FriendsTipWnd friendsTipWnd;
    private GameObjectPool pool;
    private bool IsSearch = false;
    protected override void InitWnd()
    {
        base.InitWnd();
        ClickFriend();
    }
    protected override void SetGameObject()
    {
        base.SetGameObject();
        AddListener(FriendBtn, ClickFriend);

        AddListener(AddBtn, ClickAdd);
        AddListener(FriendsListBtn, ClickFriendsList);
        AddListener(SearchBtn, ClickSearch);
        AddListener(CloseBtn, ClickClose);
        InitFriendsPool();
        friendsTipWnd.SetWndState(false);
    }
    /// <summary>
    /// 初始化好友池
    /// </summary>
    private void InitFriendsPool()
    {
        GameObject gameObject = resSvc.LoadPrefab(PathDefine.ResFriendsItem, cache: true, instan: false);
        pool = GameObjectPoolManager.Instance.CreatePrefabPool(gameObject);
        pool.MaxCount = 15;//设置最大缓存数量
        pool.cullMaxPerPass = 5;
        pool.cullAbove = 15;
        pool.cullDespawned = true;
        pool.cullDelay = 2;
        pool.Init();
    }
    /// <summary>
    /// 点击好友按钮
    /// </summary>
    public void ClickFriend()
    {
        SetWindowVisibility(FriendObj, FriendBtn.transform);
        ClearFriend(FriendContent);//清空面板
        //获取好友列表
        List<FriendItem> friendList = GameRoot.Instance.PlayerData.FriendList;
        if (friendList != null && friendList.Count > 0)
        {
            for (int i = 0; i < friendList.Count; i++)
            {
                FriendsItem item = CreateFriends(friendList[i], FriendContent);
                item.SetFriend(friendsTipWnd);
            }
        }
    }
    /// <summary>
    /// 点击添加按钮
    /// </summary>
    private void ClickAdd()
    {
        SetWindowVisibility(AddObj, AddBtn.transform);
        ClearFriend(AddContent);
    }
    /// <summary>
    /// 点击好友请求列表按钮
    /// </summary>
    public void ClickFriendsList()
    {
        SetWindowVisibility(FriendsListObj, FriendsListBtn.transform);
        ClearFriend(FriendsListContent);
        //获取好友列表
        List<FriendItem> friendList = GameRoot.Instance.PlayerData.AddFriendList;
        if (friendList != null && friendList.Count > 0)
        {
            for (int i = 0; i < friendList.Count; i++)
            {
                FriendsItem item = CreateFriends(friendList[i], FriendsListContent);
                item.SetFriendsList();
            }
        }
    }
    public void AddFriend(FriendItem friendItem)
    {
        ClearFriend(AddContent);
        if (friendItem == null)
        {
            return;
        }
        FriendsItem item = CreateFriends(friendItem, AddContent);
        PlayerData playerData = GameRoot.Instance.PlayerData;
        bool isFriend = playerData.FriendList.Any(f => f.id == friendItem.id);
        item.SetSearch(ClearItem, isFriend);

    }
    private FriendsItem CreateFriends(FriendItem friendItem, GameObject game)
    {
        GameObject obj = pool.GetObject();
        obj.transform.SetParent(game.transform, false);
        obj.transform.localScale = Vector3.one;
        FriendsItem item = obj.GetComponent<FriendsItem>();
        item.SetUI(friendItem);
        return item;
    }
    /// <summary>
    /// 点击搜索按钮
    /// </summary>
    private void ClickSearch()
    {
        if (IsSearch)
        {
            GameRoot.AddTips("请稍等，操作频率过快。");
            return;
        }

        string name = FriendName.text;
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("请输入好友名称");
        }
        else
        {
            GameMsg gameMsg = new GameMsg
            {
                cmd = (int)CMD.ReqSearchFriend,
                reqSearchFriend = new ReqSearchFriend
                {
                    name = name
                }
            };
            netSvc.SendMsg(gameMsg);
        }
        IsSearch = true;
        TimerSvc.Instance.AddTimeTask((tid) =>
        {
            IsSearch = false;
        }, 2f);
    }
    /// <summary>
    /// 清空面板
    /// </summary>
    private void ClearFriend(GameObject Content)
    {
        if (Content != null)  //清空当前的商店物品
        {
            for (int i = Content.transform.childCount - 1; i >= 0; i--)
            {
                pool.ReturnObject(Content.transform.GetChild(i).gameObject);
            }
        }
    }
    private void ClearItem(GameObject Obj)
    {
        pool.ReturnObject(Obj);
    }
    /// <summary>
    /// 切换窗口显示
    /// </summary>
    private void SetWindowVisibility(GameObject targetWindow, Transform targetPointer)
    {
        FriendObj.SetActive(false);
        AddObj.SetActive(false);
        FriendsListObj.SetActive(false);
        targetWindow.SetActive(true);
        Vector3 newPosition = pointer.transform.position;
        newPosition.y = targetPointer.position.y;
        pointer.transform.position = newPosition;
    }

    public void RefreshFriends(FriendItem item)
    {
        // 创建一个字典，将需要检查的内容及其对应的操作关联起来
        var actions = new Dictionary<GameObject, UnityAction>
{
    { AddObj.gameObject, () => AddFriend(item) },
    { FriendObj.gameObject, ClickFriend },
    { FriendsListObj.gameObject, ClickFriendsList },
};
        // 遍历字典，执行对应的操作
        foreach (var action in actions)
        {
            if (action.Key.activeSelf)
            {
                action.Value();
            }
        }
    }

    private void ClickClose()
    {
        SetWndState(false);
    }
}
