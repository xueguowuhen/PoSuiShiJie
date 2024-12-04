/****************************************************
    文件：ShopItem.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/9/15 18:58:12
    功能：商店界面物品显示
*****************************************************/
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
        ComTools.GetItemSprite(shopItemCfg.type, shopItemCfg.mImg, (Texture2D texture) =>
        {
            ShopImage.overrideSprite = texture.CreateSprite();
        });//物品图标
        ComTools.GetIconSprite(shopItemCfg.type, (Texture2D texture) =>
       {
           PriceImg.overrideSprite = texture.CreateSprite();
       });//购买的图标
    }
    // Update is called once per frame
    void Update()
    {

    }
}
