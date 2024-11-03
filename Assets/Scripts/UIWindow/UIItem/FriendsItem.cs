/****************************************************
    文件：FriendsItem.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/9 14:13:2
    功能：Nothing
*****************************************************/
using CommonNet;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FriendsItem : WindowItem
{
    public Image headImage;
    public Text nameText;
    public Text levelText;
    public Button OneBtn;
    public Text OneText;
    public Button TwoBtn;
    public Text TwoText;
    private int friendId;
    private Action<GameObject> CancelAction;
    private float lastClickTime = 0f; // 上一次点击的时间
    private float clickInterval = 2f; // 两次点击的间隔时间
    private bool IsDel = false;
    private bool IsAdd = false;
    private FriendsTipWnd friendsTipWnd;
    public void SetUI(FriendItem info)
    {
        friendId = info.id;
        headImage.sprite = ResSvc.instance.GetPersonCfgHard(int.Parse(info.type));
        nameText.text = info.name;
        levelText.text = info.level.ToString();
    }
    /// <summary>
    /// 设置为好友
    /// </summary>
    public void SetFriend(FriendsTipWnd friendsTipWnd)
    {
        this.friendsTipWnd = friendsTipWnd;
        AddListener(OneBtn, ClickGift);
        OneText.text = "赠送彩晶";
        TwoText.text = "删除好友";
        AddListener(TwoBtn, ClickDel);
    }
    /// <summary>
    /// 删除好友
    /// </summary>
    private void ClickDel()
    {
        if (IsDel)
        {
            GameRoot.AddTips("请稍等，操作频率过快。");
            return;
        }

        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.ReqDelFriend,
            reqDelFriend = new ReqDelFriend()
            {
                id = friendId,
                name = nameText.text,
            }
        };
        NetSvc.instance.SendMsg(gameMsg);
        IsDel = true;
        TimerSvc.Instance.AddTimeTask((tid) =>
        {
            IsDel = false;
        }, 2f);

    }
    /// <summary>
    /// 赠送彩晶
    /// </summary>
    private void ClickGift()
    {

        friendsTipWnd.SetWndState();
        friendsTipWnd.ShowWnd(friendId, nameText.text, BuyType.crystal);
    }

    /// <summary>
    /// 设置为搜索
    /// </summary>
    public void SetSearch(Action<GameObject> CancelAction, bool isSelf = false)
    {
        if (isSelf)
        {
            OneBtn.interactable = false;
        }
        else
        {
            OneBtn.interactable = true;
        }
        AddListener(OneBtn, ClickAdd);
        OneText.text = "添加好友";
        TwoText.text = "取消";
        AddListener(TwoBtn, ClickCancel);
        this.CancelAction = CancelAction;
    }

    /// <summary>
    /// 设置为申请
    /// </summary>
    public void SetFriendsList()
    {
        AddListener(OneBtn, ClickAgree);
        OneText.text = "同意";
        TwoText.text = "拒绝";
        AddListener(TwoBtn, ClickRefuse);
    }
    /// <summary>
    /// 拒绝好友申请
    /// </summary>
    private void ClickRefuse()
    {
        if (Time.time - lastClickTime < clickInterval) // 检查时间间隔
        {
            GameRoot.AddTips("请稍等，操作频率过快。");
            return;
        }

        lastClickTime = Time.time; // 更新上一次点击时间
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.ReqFriendAddConfirm,
            reqFriendAddConfirm = new ReqFriendAddConfirm()
            {
                id = friendId,
                name = nameText.text,
                isAgree = false,
            }
        };
        NetSvc.instance.SendMsg(gameMsg);

    }

    private void ClickAgree()
    {
        if (Time.time - lastClickTime < clickInterval) // 检查时间间隔
        {
            GameRoot.AddTips("请稍等，操作频率过快。");
            return;
        }

        lastClickTime = Time.time; // 更新上一次点击时间
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.ReqFriendAddConfirm,
            reqFriendAddConfirm = new ReqFriendAddConfirm()
            {
                id = friendId,
                name = nameText.text,
                isAgree = true,
            }
        };
        NetSvc.instance.SendMsg(gameMsg);

    }
    /// <summary>
    /// 点击取消
    /// </summary>
    private void ClickCancel()
    {
        CancelAction(gameObject);
    }
    /// <summary>
    /// 点击添加好友
    /// </summary>
    private void ClickAdd()
    {
        if (IsAdd)
        {
            GameRoot.AddTips("请稍等，操作频率过快。");
            return;
        }

        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.ReqAddFriend,
            reqAddFriend = new ReqAddFriend()
            {
                id = friendId,
                name = nameText.text,
            }
        };
        NetSvc.instance.SendMsg(gameMsg);
        IsAdd = true;
        TimerSvc.Instance.AddTimeTask((tid) =>
        {
            IsAdd = false;
        }, 2f);
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
