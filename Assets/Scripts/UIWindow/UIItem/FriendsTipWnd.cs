/****************************************************
    文件：FriendsTipWnd.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/11 14:28:42
    功能：Nothing
*****************************************************/
using CommonNet;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.UI;

public class FriendsTipWnd : WindowRoot
{
    #region One

    public Image GoodsImage;
    public Image Goods2Image;
    #endregion

    #region Two
    public Button Decrease;
    public Text Goods2Price;
    public InputField Goods2Count;
    public Button Increase;
    public Button Gift;
    public Button Close;
    #endregion
    private int m_ItemId;
    private int quantity = 1;
    private BuyType m_BuyType;
    private string m_Name;
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
        AddListener(Gift, OnClickGift);
        AddListener(Close, OnClickClose);
    }
    public void ShowWnd(int id, string name, BuyType buyType)
    {
        base.SetWndState();
        m_BuyType = buyType;
        m_ItemId = id;
        m_Name = name;
        quantity = 1;
        UpdateQuantityText();

        ComTools.GetIconSprite(buyType, (Texture2D texture) =>
       {
           Goods2Image.sprite = GoodsImage.sprite = texture.CreateSprite();
       });
        //BuyImag.sprite = Buy2Imag.sprite = ComTools.GetIconSprite(shopItemCfg.type);
        Goods2Price.text = (quantity).ToString();
        //m_BuyType = (BuyType)shopItemCfg.type;
    }
    private void OnClickGift()//点击礼物按钮
    {

        string id = m_ItemId.ToString();
        string quantity = this.quantity.ToString();
        GameMsg gameMsg = new GameMsg
        {
            cmd = (int)CMD.ReqFriendGift,
            reqFriendGift = new ReqFriendGift
            {

                buyType = m_BuyType,
                id = int.Parse(id),
                name = m_Name,
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
        Goods2Price.text = (quantity).ToString();
    }
}
