/****************************************************
    文件：RaffleItem
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-11-26 10:10:38
	功能：Nothing
*****************************************************/
using UnityEngine;
using UnityEngine.UI;
using XLua;
[LuaCallCSharp]
public class RaffleItem : WindowItem
{
    public Text ItemCount;
    public Image ItemIcon;
    public Text ItemName;
    public int mShopID;
    public void SetUI(RaffleItemCfg raffleItem)
    {
        mShopID = raffleItem.mShopID;
        ItemCount.text = raffleItem.Count.ToString();
        ItemCfg shopItemCfg = resSvc.GetRaffleItemCfgData(raffleItem.mShopID);
        ItemName.text = shopItemCfg.mName;
        ComTools.GetItemSprite(shopItemCfg.type, shopItemCfg.mImg, (Texture2D texture) =>
        {
            ItemIcon.overrideSprite = texture.CreateSprite();
        });//物品图标
    }
}
