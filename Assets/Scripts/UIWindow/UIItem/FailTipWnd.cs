/****************************************************
    文件：FailTipWnd.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/31 16:15:13
    功能：失败提示界面
*****************************************************/
using CommonNet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FailTipWnd : WindowRoot
{
    public Button btnBackHome;
    public Button btnRevice;
    public Text txtContent;

    protected override void InitWnd()
    {
        base.InitWnd();
    }
    protected override void SetGameObject()
    {
        base.SetGameObject();
        btnBackHome.onClick.AddListener(OnBtnBackHomeClick);
        btnRevice.onClick.AddListener(OnBtnReviceClick);
    
    }
    /// <summary>
    /// 返回首页按钮点击事件
    /// </summary>
    private void OnBtnBackHomeClick()
    {
       PlayerData playerData=  GameRoot.Instance.PlayerData;
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.ReqRecover,
            reqRecover = new ReqRecover()
            {
                id = playerData.id,
                isRevive = false,
            }
        };
        netSvc.SendMsg(gameMsg);
    }
    /// <summary>
    /// 复活按钮点击事件
    /// </summary>
    private void OnBtnReviceClick()
    {
        PlayerData playerData = GameRoot.Instance.PlayerData;
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.ReqRecover,
            reqRecover = new ReqRecover()
            {
                id = playerData.id,
                isRevive = true,
            }
        };
        netSvc.SendMsg(gameMsg);
    }
}
