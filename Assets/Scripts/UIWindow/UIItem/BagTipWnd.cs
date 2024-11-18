/****************************************************
    文件：BagTipWnd.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/7 21:26:18
    功能：背包信息显示窗口
*****************************************************/
using CommonNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.UI;

public class BagTipWnd : WindowRoot
{
    public Text Name;
    public Image ItemImage;
    public Text mInfo;
    public Image itemPriceImage;
    public Text itemPrice;
    public Button BagBtn;
    public Button closeBtn;
    private BuyType m_BuyType;

    // Start is called before the first frame update
    protected override void InitWnd()
    {
        base.InitWnd();
    }
    protected override void SetGameObject()
    {
        base.SetGameObject();
        AddListener(BagBtn, OnBagBtn);
        AddListener(closeBtn, OnClickClose);
    }
    public void ShowWnd(int id)
    {
        base.SetWndState();
        ItemCfg shopItemCfg = resSvc.GetShopItemCfg(id);
        Name.text = shopItemCfg.mName;
        ComTools.GetItemSprite(shopItemCfg.type, shopItemCfg.mImg, (Texture2D texture) =>
        {
            ItemImage.overrideSprite = texture.CreateSprite();
        });
        mInfo.text = shopItemCfg.mInfo;

        ComTools.GetIconSprite(shopItemCfg.type, (Texture2D texture) =>
        {
            itemPriceImage.overrideSprite = texture.CreateSprite();
        });
        itemPrice.text = shopItemCfg.Price.ToString();
        m_BuyType = (BuyType)shopItemCfg.type;
    }
    /// <summary>
    /// 点击物品按钮
    /// </summary>
    private void OnBagBtn()
    {

    }
    private void OnClickClose()
    {
        SetWndState(false);
    }

    void Start()
    {

    }
    void Update()
    {

    }
}
