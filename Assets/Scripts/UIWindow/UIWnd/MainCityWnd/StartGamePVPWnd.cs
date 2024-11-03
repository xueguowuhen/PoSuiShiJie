/****************************************************
    文件：StartGameWnd.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/14 21:35:43
    功能：开始游戏UI界面
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGamePVPWnd : WindowRoot
{
    public Button PVPItemBtn;
    public Button PVEItemBtn;
    public Button exitBtn;

    protected override void InitWnd()
    {
        base.InitWnd();
    }

    protected override void SetGameObject()
    {
        base.SetGameObject();
        AddListener(PVPItemBtn, OnPVPItemBtnClick);
        AddListener(PVEItemBtn, OnPVEItemBtnClick);
        AddListener(exitBtn, OnExitBtnClick);

    }
    /// <summary>
    /// 返回主界面
    /// </summary>
    private void OnExitBtnClick()
    {
        SetWndState(false);
        MainCitySys.instance.EnterMainCityWnd();
    }
    /// <summary>
    /// 加载PVE界面
    /// </summary>
    private void OnPVEItemBtnClick()
    {

    }
    /// <summary>
    /// 加载PVP场景
    /// </summary>
    private void OnPVPItemBtnClick()
    {
        MainCitySys.instance.ClickStartGamePVP();
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
