/****************************************************
    文件：ShopItem.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/9/15 18:58:12
    功能：商店界面物品显示
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    // Start is called before the first frame update
    public Image ShopImage;
    public Text Name;
    public Text Count;
    public Image PriceImg;
    public Text Price;
    void Start()
    {

    }
    public void SetUI(ItemCfg shopItemCfg)
    {
        Name.text = shopItemCfg.mName;
        Count.text = "∞";
        Price.text = shopItemCfg.Price.ToString();
        ShopImage.sprite = ComTools.GetItemSprite(shopItemCfg.type, shopItemCfg.mImg);//物品图标
        PriceImg.sprite = ComTools.GetIconSprite(shopItemCfg.type);//购买的图标
    }
    // Update is called once per frame
    void Update()
    {

    }
}
