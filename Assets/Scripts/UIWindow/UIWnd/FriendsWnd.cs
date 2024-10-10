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
using UnityEngine;
using UnityEngine.UI;
using static UnityEditor.Progress;

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
    private GameObjectPool pool;
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
    private void ClickFriend()
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
                item.SetFriend();
            }
        }
    }
    private void ClickAdd()
    {
        SetWindowVisibility(AddObj, AddBtn.transform);
        ClearFriend(AddContent);
    }
    private void ClickFriendsList()
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
        FriendsItem item = CreateFriends(friendItem,AddContent);
        item.SetSearch(ClearItem);
    }
    private FriendsItem CreateFriends(FriendItem friendItem,GameObject game)
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
        ClickWithDelay(() =>
        {
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
        });
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

    private void ClickClose()
    {
        SetWndState(false);
    }
}
