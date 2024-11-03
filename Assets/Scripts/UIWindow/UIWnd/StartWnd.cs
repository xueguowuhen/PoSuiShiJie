/****************************************************
    文件：StartWnd.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/9/10 13:33:37
    功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartWnd : WindowRoot
{
    public Button LoginBtn;
    public Button ExitBtn;
    public Button SettingBtn;
    private UIAnimation LoginAni;
    protected override void InitWnd()
    {
        base.InitWnd();
        WindowAnimation.Instance.StartShowWindow(LoginAni, true);
    }
    protected override void SetGameObject()
    {
        base.SetGameObject();
        AddListener(LoginBtn, LoginBtnClick);
        AddListener(ExitBtn, ExitBtnClick);
        AddListener(SettingBtn, SettingBtnClick);
        LoginAni=LoginBtn.GetComponent<UIAnimation>();
    }
    protected override void ClearWnd()
    {
        base.ClearWnd();
        WindowAnimation.Instance.StartShowWindow(LoginAni, false);
    }
    /// <summary>
    /// 登录游戏，进入登录场景
    /// </summary>
    private void LoginBtnClick()
    {
        //进入登录界面
        LoginSys.instance.GameLogin();
        SetWndState(false);
    }
    /// <summary>
    /// 退出暂无
    /// </summary>
    private void ExitBtnClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }
    /// <summary>
    /// 设置暂无
    /// </summary>
    private void SettingBtnClick()
    {

    }
}
