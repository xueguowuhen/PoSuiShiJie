using CommonNet;
using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;

public class PersonWnd : WindowRoot
{
    #region one
    public Image headImg;
    public Text TypeName;
    public Text Level;
    //public GameObject itemOne;
    //public RectTransform PersonItem;
    //public GameObject PersonOne;
    //public GameObject Close;
    //public Button BtnClose;
    //public Button BtnPerson;
    #endregion

    #region two
    public Image ExpSlider;
    public Text ExpPercent;
    public Text ExpText;
    public Text PowerText;
    public Image PoweSlider;
    public Text HpText;
    public Image HpSlider;
    public GameObject AttributeContent;
    private GameObjectPool pool;
    public Button BtnClose;
    //public Slider slider;
    //public Slider HpSlider;
    //public Slider MagicSlider;
    #endregion
    protected override void InitWnd()
    {
        base.InitWnd();
        //SetPersonBtn();
        ClearFriend(AttributeContent);
        SetPersonInfo();
    }
    protected override void SetGameObject()
    {
        base.SetGameObject();
        AddListener(BtnClose, ClickClose);
        InitPersonPool();
    }

    /// <summary>
    /// 初始化人物池
    /// </summary>
    private void InitPersonPool()
    {
        GameObject gameObject = resSvc.LoadPrefab(PathDefine.ResAttributeText, cache: true, instan: false);
        pool = GameObjectPoolManager.Instance.CreatePrefabPool(gameObject);
        pool.MaxCount = 15;//设置最大缓存数量
        pool.cullMaxPerPass = 5;
        pool.cullAbove = 15;
        pool.cullDespawned = true;
        pool.cullDelay = 2;
        pool.Init();
    }

    private void SetPersonInfo()
    {
        PlayerData playerData = GameRoot.Instance.PlayerData;
        int headid = playerData.type;
        headImg.sprite = resSvc.GetPersonCfgHard(headid);
        personCfg personCfg = resSvc.GetPersonCfgData(headid);
        TypeName.SetText(personCfg.type, true);
        Level.SetText(string.Format("等级:Lv{0}", playerData.level), true, scrambleMode: DG.Tweening.ScrambleMode.Numerals);
        float Levelexp = ComTools.GetExperienceForLevel(playerData.level, personCfg.BaseExp, personCfg.ExpMul);
        float curExp = playerData.exp;
        float expPercent = curExp / Levelexp;
        ExpSlider.SetImageFillAmount(expPercent, true);
        ExpPercent.SetText(string.Format("{0}%", (int)(expPercent * 100), true));
        ExpText.SetText(string.Format("经验:{0}/{1}", curExp, Levelexp), true);
        PowerText.SetText(string.Format("魔法值:{0}/{1}", playerData.power, playerData.powerMax), true);
        PoweSlider.SetImageFillAmount((float)playerData.power / playerData.powerMax, true);
        HpText.SetText(string.Format("生命值:{0}/{1}", playerData.Hp, playerData.Hpmax), true);
        HpSlider.SetImageFillAmount((float)playerData.Hp / playerData.Hpmax, true);
        DisplayStatus(playerData);
    }

    private void CreateAttribute(GameObject game, string name)
    {
        GameObject obj = pool.GetObject();
        obj.transform.SetParent(game.transform, false);
        obj.transform.localScale = Vector3.one;
        Text txt = obj.GetComponent<Text>();
        txt.SetText(name, true, scrambleMode: DG.Tweening.ScrambleMode.Numerals);

    }
    public void DisplayStatus(PlayerData data)
    {
        string[] statusLines = new string[]
        {
    $"等级:     LV{data.level}",
    $"经验:       {data.exp}",
    //$"生命值:     {data.Hp}",
    //$"生命值上限: {data.Hpmax}",
    //$"法力值:     {data.Mana}",
    //$"法力值上限: {data.ManaMax}",
    $"体力:       {data.power}" ,
    $"体力上限:   {data.powerMax}",
    $"物攻:       {data.ad}",
    $"魔攻:       {data.ap}",
    $"物抗:       {data.addef}",
    $"魔抗:       {data.apdef}",
    $"闪避概率:   {data.dodge}%",
    $"暴击概率:   {data.critical}%",
    $"修炼速度:   {data.practice}",
    $"星晶:       {data.aura}",
    $"云晶:       {data.ruvia}",
    $"彩晶:       {data.crystal}"
        };

        foreach (var line in statusLines) // 使用循环输出状态信息
        {
            CreateAttribute(AttributeContent, line);
        }
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

    /// <summary>
    /// 点击关闭按钮
    /// </summary>
    private void ClickClose()
    {
        SetWndState(false);
    }

}
