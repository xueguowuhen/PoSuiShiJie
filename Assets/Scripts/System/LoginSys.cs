/****************************************************
    文件：LoginSys.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/13 23:8:36
	功能：登录系统
*****************************************************/

using CommonNet;
using System.Collections.Generic;
using UnityEngine;

public class LoginSys : SystemRoot
{
    public static LoginSys instance;
    public LoginWnd loginWnd;
    public RegisterWnd registerWnd;
    public CreateWnd createWnd;
    public DowningWnd downingWnd;
    public StartWnd startWnd;
    public override void InitSyc()
    {
        base.InitSyc();
        instance = this;
        GameCommon.Log("LoginSys Init....");
    }
    public void EnterDowing()
    {
        //暂时不进入资源下载
        //downingWnd.SetWndState(true);
        //临时进入Start登录界面
        startWnd.SetWndState(true);
    }
    public void GameLogin()
    {
        StartCoroutine(resSvc.AsyncLoadScene(Constants.SceneLogin, () =>
        {
            EnterLogin();
        }));
    }

    public void RspRegister(GameMsg msg)
    {
        GameRoot.AddTips("该账号注册成功");
        if (msg.rspRegister.isSucc)
        {
            registerWnd.ClickClose();
        }
    }
    public void RspLogin(GameMsg msg)
    {
        GameRoot.AddTips("登录成功");
        if (msg.rspLogin.playerData.name == null)//判断该账号是否需要进入创建界面
        {
            //GameCommon.Log("创建账号");
            //loginWnd.imgbg.SetActive(false);
            loginWnd.SetWndState(false);
            createWnd.SetWndState();
        }
        else //TODO 进入主页
        {
            GameRoot.Instance.SetPlayerData(msg.rspLogin.playerData);//更新数据
            GameRoot.Instance.SetPlayerDataList(msg.rspLogin.playerList);//更新城镇所有玩家

            //loginWnd.imgbg.SetActive(false);
            loginWnd.SetWndState(false);
            //进入主城
            MainCitySys.instance.EnterMainCity();
        }
    }
    public void RspCreateGame(GameMsg msg)
    {
        GameRoot.AddTips("登录成功");
        GameRoot.Instance.SetPlayerData(msg.rspCreateGame.playerData);//更新数据
        GameRoot.Instance.SetPlayerDataList(msg.rspCreateGame.playerDataList);
        //loginWnd.imgbg.SetActive(false);
        createWnd.SetWndState(false);
        //进入主城
        MainCitySys.instance.EnterMainCity();
    }
    public void ReLoginClick()
    {
        loginWnd.LoginClick = true;
    }
    public void EnterRegis()
    {
        registerWnd.SetWndState();
    }
    public void EnterLogin()
    {
        loginWnd.SetWndState();
    }
    public void EnterStart()
    {
        startWnd.SetWndState();
    }
    public Dictionary<string, ABInfo> GettmpABInfo()
    {
        return downingWnd.tmpABInfo;
    }


}