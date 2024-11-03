/****************************************************
    文件：DynamicWnd.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/21 22:28:22
	功能：动态显示窗体
*****************************************************/

using CommonNet;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DynamicWnd : WindowRoot
{
    public Animation tipsAni;
    public Transform hpItemRoot;
    public Text tipsText;
    public SocketFailTipWnd socketFailTipWnd;
    private bool tipsShow = false;
    private Queue<string> tipsqueue = new Queue<string>();
    private Dictionary<int, ItemEntityHP> itemDic = new Dictionary<int, ItemEntityHP>();
    protected override void InitWnd()
    {
        base.InitWnd();
        SetSocketFail(false);
        SetGameObject();
    }
    protected override void SetGameObject()
    {
        tipsAni = SetTranFind(PathDefine.TextTips).GetComponent<Animation>();
        //tipsText = SetTranFind(PathDefine.TextTips).GetComponent<Text>();
        hpItemRoot = SetTranFind(PathDefine.hpItemRoot);
    }
    public void AddTips(string tips)
    {
        lock (tipsqueue)
        {
            tipsqueue.Enqueue(tips);
        }
    }
    /// <summary>
    /// 添加血条UI
    /// </summary>
    public void AddHptemInfo(int playerid, Transform trans)
    {
        ItemEntityHP item = null;
        if (itemDic.TryGetValue(playerid, out item))
        {
            return;
        }
        else
        {
            GameObject go = resSvc.LoadPrefab(PathDefine.HPItemPrefab, true);
            go.transform.SetParent(hpItemRoot);
            go.transform.localPosition = new Vector3(0, 0, 0);
            ItemEntityHP itemEntity = go.AddComponent<ItemEntityHP>();
            Transform tran = trans.Find(PathDefine.HpRoot);

            itemEntity.SetItemInfo(tran);
            itemDic.Add(playerid, itemEntity);
        }
    }
    public void RemoveHptemInfo(int playerid)
    {
        if (itemDic.TryGetValue(playerid, out ItemEntityHP item))
        {
            Destroy(item.gameObject);
        }
    }
    public void Update()
    {
        if (tipsqueue.Count > 0 && tipsShow == false)
        {
            lock (tipsqueue)
            {
                string tips = tipsqueue.Dequeue();//出队
                tipsShow = true;
                SetTips(tips);
            }
        }
    }
    public void SetTips(string tips)
    {
        SetActive(tipsAni.gameObject);
        SetText(tipsText, tips);

        AnimationClip clip = tipsAni.GetClip("AniTips");
        tipsAni.Play();
        timerSvc.AddTimeTask((int tid) =>
        {
            SetActive(tipsAni.gameObject, false);
            tipsShow = false;
        }, clip.length, TimeUnit.Second);
    }
    public void SetDodge(int playerid)
    {
        ItemEntityHP itemEntityHP = null;
        if (itemDic.TryGetValue(playerid, out itemEntityHP))
        {
            itemEntityHP.SetDodge();
        }
    }
    public void SetCritical(int playerid, int hurt)
    {
        ItemEntityHP itemEntityHP = null;
        if (itemDic.TryGetValue(playerid, out itemEntityHP))
        {
            itemEntityHP.SetCritical(hurt);
        }
    }
    public void SetHurt(int playerid, int hurt)
    {
        ItemEntityHP itemEntityHP = null;
        if (itemDic.TryGetValue(playerid, out itemEntityHP))
        {
            itemEntityHP.SetHurt(hurt);
        }
    }
    public void SetSocketFail(bool isShow = true)
    {
        socketFailTipWnd.SetWndState(isShow);
    }
}