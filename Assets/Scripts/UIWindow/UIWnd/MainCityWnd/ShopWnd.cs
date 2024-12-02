using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShopWnd : WindowRoot
{
    #region one
    public GameObject Buttons;
    #endregion
    #region two
    public GameObject ShopSlots;
    public GameObject RaffleSlots;
    public GameObject ItemContent;
    public GameObject RaffleContent;
    public GameObject DownLoad;
    public string DownLoadUrl;
    public Image DownLoadImg;
    public Button Close;
    public GameObject pointer;
    private Button All;
    private Button Consume;
    private Button Equip;
    private Button Material;
    private Button Raffle;
    #endregion
    public BuyTipWnd BuyTipWnd;
    public GameObject RaffleTipInfo;
    private List<ItemCfg> allItems = new List<ItemCfg>();
    private GameObjectPool pool;
    private Action onLoadItems;
    protected override void InitWnd()
    {
        base.InitWnd();//
        if (RaffleSlots.activeSelf)
        {
            ClickRaffle();
        }
        if (pool == null)
        {
            SetActive(DownLoad, true);
            onLoadItems = LoadItems;
        }
        else
        {
            LoadItems();

        }
        CloseBuy();
    }
    public void Start()
    {
        StartCoroutine(InitializePointerPosition());
    }
    private IEnumerator InitializePointerPosition()
    {
        // 等待一帧，确保 AllBtn 加载完成
        yield return null;
        Vector3 newPosition = pointer.transform.position;
        newPosition.y = All.transform.position.y;
        pointer.transform.position = newPosition;
    }
    protected override void SetGameObject()
    {
        #region two
        AddListener(Close, CloseShop);
        Raffle = Buttons.transform.Find("Raffle").GetComponent<Button>();
        AddListener(Raffle, ClickRaffle);
        All = Buttons.transform.Find("All").GetComponent<Button>();
        AddListener(All, ClickAll);
        Equip = Buttons.transform.Find("Euqip").GetComponent<Button>();
        AddListener(Equip, ClickEquip);
        Consume = Buttons.transform.Find("consume").GetComponent<Button>();
        AddListener(Consume, ClickConsume);
        Material = Buttons.transform.Find("material").GetComponent<Button>();
        AddListener(Material, ClickMaterial);
        #endregion
        //InitTipInfoWnd();
        InitShopPool();
        SetActive(RaffleSlots, false);
        SetActive(RaffleTipInfo, false);
    }

    private void ClickRaffle()
    {
        SetActive(ItemContent,false);
        SetPointerPosition(Raffle.transform);
        MainCitySys.instance.EndRaffleLua();
    }

    /// <summary>
    /// 初始化商品池
    /// </summary>
    private void InitShopPool()
    {
        DownLoadUrl = PathDefine.ResShopItem;
        loaderSvc.LoadPrefab(PathDefine.ResItem, DownLoadUrl, (GameObject obj) =>
        {
            GameObject gameObject = obj;
            pool = GameObjectPoolManager.Instance.CreatePrefabPool(gameObject);
            pool.MaxCount = 15;
            pool.cullMaxPerPass = 5;
            pool.cullAbove = 15;
            pool.cullDespawned = true;
            pool.cullDelay = 2;
            pool.Init();
            DownLoadUrl = null;
            if (onLoadItems != null)
            {
                onLoadItems();
            }

        }, cache: true, instan: false);
    }
    private void Update()
    {
        if (DownLoadUrl != null)
        {
            
            float progress = DowningSys.instance.GetDownUrlProgress(DownLoadUrl);
            DownLoadImg.fillAmount = progress;
        }
    }

    /// <summary>
    /// 加载商店所有物品
    /// </summary>
    private void LoadItems()
    {
        SetActive(DownLoad, false);
        ClearShop();
        ItemCfg[] ItemsData = GetItemData();//获取所有的物品资源
        allItems = ItemsData.ToList();
        for (int i = 0; i < allItems.Count; i++)
        {
            CreateItem(allItems, i);
        }

    }
    /// <summary>
    /// 加载商店部分物品
    /// </summary>
    /// <param name="itemType">部分物品的类型</param>
    private void RefreshItems(ItemType itemType)
    {
        ClearShop();
        List<ItemCfg> NewItems = allItems.Where(item => item.type == itemType).ToList();
        NewItems = NewItems.Count > 0 ? NewItems : allItems;
        for (int i = 0; i < NewItems.Count; i++)
        {
            CreateItem(NewItems, i);
        }
    }
    /// <summary>
    /// 创建物品
    /// </summary>
    private void CreateItem(List<ItemCfg> shopItems, int i)
    {
        GameObject gameObject = pool.GetObject();
        //设置物品
        ShopItem shopItem = gameObject.GetOrAddComponent<ShopItem>();
        shopItem.SetUI(shopItems[i]);
        gameObject.transform.SetParent(ItemContent.transform, false);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;
        Button button = gameObject.GetComponent<Button>();
        int currentIndex = i;
        button.onClick.AddListener(delegate { ClickBuy(shopItems[currentIndex].ID); });
    }
    private void ClearShop()
    {
        if (pool == null) return;
        if (ItemContent != null)  //清空当前的商店物品
        {
            for (int i = ItemContent.transform.childCount - 1; i >= 0; i--)
            {
                pool.ReturnObject(ItemContent.transform.GetChild(i).gameObject);
            }
        }
    }
    /// <summary>
    /// 点击购买按钮
    /// </summary>
    private void ClickEquip()
    {

        ClickItem(ItemType.equip, Equip.transform);
    }
    /// <summary>
    /// 点击消耗按钮
    /// </summary>
    private void ClickConsume()
    {
        ClickItem(ItemType.consume, Consume.transform);

    }
    /// <summary>
    /// 点击材料按钮
    /// </summary>
    private void ClickMaterial()
    {
        ClickItem(ItemType.material, Material.transform);
    }
    /// <summary>
    /// 刷新指定类型的物品
    /// </summary>
    private void ClickAll()
    {
        ClickItem(ItemType.all, All.transform);
    }
    /// <summary>
    /// 刷新指定类型的物品
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="targetTransform"></param>
    private void ClickItem(ItemType itemType, Transform targetTransform)
    {
        SetActive(ItemContent, true);
        SetActive(RaffleSlots, false);
        RefreshItems(itemType);
        SetPointerPosition(targetTransform);
    }
    public void SetPointerPosition(Transform targetTransform)
    {
        Vector3 newPosition = pointer.transform.position;
        newPosition.y = targetTransform.position.y;
        pointer.transform.position = newPosition;
    }
    /// <summary>
    /// 从资源加载服务获得所有商店物品信息
    /// </summary>
    /// <returns></returns>
    private ItemCfg[] GetItemData()
    {
        return resSvc.GetShopItemCfgData();
    }
    /// <summary>
    /// 关闭商店界面
    /// </summary>
    private void CloseShop()
    {
        MainCitySys.instance.OpenPerson();
        SetWndState(false);
    }
    /// <summary>
    /// 点击购买按钮
    /// </summary>
    /// <param name="id"></param>
    private void ClickBuy(int id)
    {
        BuyTipWnd.ShowWnd(id);
    }
    /// <summary>
    /// 关闭购买提示面板
    /// </summary>
    public void CloseBuy()
    {
        
        BuyTipWnd.SetWndState(false);
    }
    protected override void ClearWnd()
    {
        base.ClearWnd();
        onLoadItems = null;
    }
}
