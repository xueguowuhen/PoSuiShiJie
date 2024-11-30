/****************************************************
    文件：WindowItem
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-09 14:31:44
	功能：窗体子类
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class WindowItem : MonoBehaviour
{
    protected ResSvc resSvc;
    protected AudioSvc audioSvc;
    protected NetSvc netSvc;
    protected TimerSvc timerSvc;
    protected AssetLoaderSvc loaderSvc;
    public void Awake()
    {
        OnAwake();
    }


    public void Start()
    {
        OnStart();
    }

    public void Update()
    {
        OnUpdate();
    }

    public void OnDestroy()
    {
        BeforeOnDestroy();
    }
    protected virtual void OnAwake()
    {
        resSvc = ResSvc.Instance;
        audioSvc = AudioSvc.Instance;
        netSvc = NetSvc.Instance;
        timerSvc = TimerSvc.Instance;
        loaderSvc = AssetLoaderSvc.Instance;
    }

    protected virtual void OnStart()
    {

    }
    protected virtual void OnUpdate()
    {

    }
    protected virtual void BeforeOnDestroy()
    {
        resSvc = null;
        audioSvc = null;
        netSvc = null;
        timerSvc = null;
    }

    //设置监听
    protected void AddListener(Button button, UnityAction call)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(call);
    }
    //设置监听
    protected void AddListener(Toggle toggle, UnityAction<bool> call)
    {
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(call);
    }
}


