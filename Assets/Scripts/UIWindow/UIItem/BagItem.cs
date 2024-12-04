/****************************************************
    文件：BagItem
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-07 18:32:37
	功能：背包物品显示
*****************************************************/
using UnityEngine;
using UnityEngine.UI;

public class BagItem : MonoBehaviour
{
    public GameObject item;
    // Start is called before the first frame update
    public Image ShopImage;
    public Text Name;
    public Text Count;
    //public Image PriceImg;
    //public Text Price;
    void Start()
    {

    }
    public void SetUI(ItemCfg ShopItemCfg, int count)
    {
        item.SetActive(true);
        Name.text = ShopItemCfg.mName;
        Count.text = count.ToString();
        //Price.text = ShopItemCfg.Price.ToString();
        ComTools.GetItemSprite(ShopItemCfg.type, ShopItemCfg.mImg, (Texture2D texture) =>
       {
           ShopImage.overrideSprite = texture.CreateSprite();
       });//物品图标
        //PriceImg.sprite = ComTools.GetIconSprite(ShopItemCfg.type);//购买的图标
    }
    // Update is called once per frame
    void Update()
    {

    }
    /// <summary>
    /// 设置为无效物品
    /// </summary>
    public void SetNone()
    {
        item.SetActive(false);
    }
}