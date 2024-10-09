/****************************************************
    文件：LoginWnd.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/13 22:39:6
	功能：登录窗体
*****************************************************/

using CommonNet;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LoginWnd : WindowRoot
{
    public Button btnClose;
    public Button btnLogin;
    public Button btnRegis;
    //UI动画重新配置，暂时不启用
    //public Animator animator;
    public UIAnimation LoginAni;
    public Toggle toggle;
    public InputField userName;
    public InputField password;
    public GameObject imgbg;
    public bool LoginClick = true;
    protected override void InitWnd()
    {
        base.InitWnd();
        //SetGameObject();
        //获取本地存储的账号密码
        if (PlayerPrefs.HasKey("Acct") && PlayerPrefs.HasKey("Pass"))
        {
            userName.text = PlayerPrefs.GetString("Acct");
            password.text = PlayerPrefs.GetString("Pass");
        }
        else
        {
            userName.text = "";
            password.text = "";
        }
        WindowAnimation.Instance.StartShowWindow(LoginAni, true);
    }
    protected override void SetGameObject()
    {
        AddListener(btnClose, ClickClose);
        AddListener(btnLogin,ClickLogin);
        AddListener(btnRegis,ClickRegis);
        //SetActive(imgbg);
    }
    protected override void ClearWnd()
    {
        base.ClearWnd();
    }
    public void ClickClose()
    {
        WindowAnimation.Instance.StartShowWindow(LoginAni, false);
        timerSvc.AddTimeTask((int tid) =>
        {
            LoginSys.instance.EnterStart();
            SetWndState(false);
        }, LoginAni.duration, TimeUnit.Second);
    }
    /// <summary>
    /// 登录游戏
    /// </summary>
    public void ClickLogin()
    {
        string acct = userName.text;
        string pass = password.text;
        if (acct != "" && pass != "")
        {
            if (toggle.isOn)
            {
                //GameCommon.Log("保存成功");
                PlayerPrefs.SetString("Acct", acct);
                PlayerPrefs.SetString("Pass", pass);
            }
            else
            {
                GameRoot.AddTips("保存失败");
            }
            //GameCommon.Log("登录成功");
            if (LoginClick)
            {
                GameMsg msg = new GameMsg
                {
                    cmd = (int)CMD.ReqLogin,
                    reqLogin = new ReqLogin
                    {
                        acct = acct,
                        pass = pass,
                    }
                };
                netSvc.SendMsg(msg);
                LoginClick = false;
            }
        }
        else
        {
            GameRoot.AddTips("登录失败");
        }
    }
    public void ClickRegis()
    {
        WindowAnimation.Instance.StartShowWindow(LoginAni, false);
        timerSvc.AddTimeTask((int tid) =>
        {
            LoginSys.instance.EnterRegis();
            SetWndState(false);
        }, LoginAni.duration, TimeUnit.Second);

    }
}