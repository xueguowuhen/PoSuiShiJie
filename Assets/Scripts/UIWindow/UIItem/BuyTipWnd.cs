using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CommonNet;
using System.Diagnostics;

public class BuyTipWnd : WindowRoot
{
    #region One
    public Image GoodsImage;
    public Image Goods2Image;
    public Text Name;//物品名称
    public Image BuyImag;
    public Image Buy2Imag;
    public Text GoodsPrice;
    public Text GoodsmInfo;
    #endregion

    #region Two
    public Button Decrease;
    public Text Goods2Price;
    public InputField Goods2Count;
    public Button Increase;
    public Button Buy;
    public Button Close;
    #endregion
    private int m_ItemId;
    private int quantity = 1;
    private float Price;
    private BuyType m_BuyType;
    //public 
    protected override void InitWnd()
    {
        base.InitWnd();
    }
    protected override void SetGameObject()
    {
        base.SetGameObject();
        AddListener(Decrease, OnClickDecrease);
        AddListener(Increase, OnClickIncrease);
        AddListener(Buy, OnClickBuy);
        AddListener(Close, OnClickClose);
    }
    public void ShowWnd(int id)
    {
        base.SetWndState();
        quantity = 1;
        UpdateQuantityText();
        ItemCfg shopItemCfg = resSvc.GetShopItemCfg(id);
        Name.text = shopItemCfg.mName;
        GoodsmInfo.text = shopItemCfg.mInfo.ToString();
        GoodsPrice.text = shopItemCfg.Price.ToString();
        Goods2Image.sprite = GoodsImage.sprite = ComTools.GetItemSprite(shopItemCfg.type, shopItemCfg.mImg);
        BuyImag.sprite = Buy2Imag.sprite = ComTools.GetIconSprite(shopItemCfg.type);
        m_ItemId = shopItemCfg.ID;
        Price = shopItemCfg.Price;
        Goods2Price.text = (quantity * Price).ToString();
        m_BuyType = (BuyType)shopItemCfg.type;
    }
    private void OnClickBuy()//点击购买
    {
        string id = m_ItemId.ToString();
        string quantity = this.quantity.ToString();
        GameMsg gameMsg = new GameMsg
        {
            cmd = (int)CMD.ReqShop,
            reqShop = new ReqShop
            {
                buyType = m_BuyType,
                id = int.Parse(id),
                count = int.Parse(quantity),
            }
        };
        netSvc.SendMsg(gameMsg);
        base.SetWndState(false);
    }
    private void OnClickClose()  //关闭按钮
    {
        base.SetWndState(false);
    }
    private void OnClickDecrease() //点击减少
    {
        if (quantity >= 2)
        {
            quantity--;
            UpdateQuantityText();
        }
    }
    private void OnClickIncrease()  //点击增加
    {
        quantity++;
        UpdateQuantityText();
    }
    private void UpdateQuantityText()
    {
        Goods2Count.text = quantity.ToString();
        Goods2Price.text = (quantity * Price).ToString();
    }
}
