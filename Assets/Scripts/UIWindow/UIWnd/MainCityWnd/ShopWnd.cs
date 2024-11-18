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
    public GameObject ItemContent;
    public Button Close;
    public GameObject pointer;
    private Button All;
    private Button Consume;
    private Button Equip;
    private Button Material;
    private Button Search;
    #endregion
    public BuyTipWnd BuyTipWnd;
    private List<ItemCfg> allItems = new List<ItemCfg>();
    private GameObjectPool pool;
    private Action onLoadItems;
    protected override void InitWnd()
    {
        base.InitWnd();//
        if (pool == null)
        {
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
        // �ȴ�һ֡��ȷ�� AllBtn �������
        yield return null;
        Vector3 newPosition = pointer.transform.position;
        newPosition.y = All.transform.position.y;
        pointer.transform.position = newPosition;
    }
    protected override void SetGameObject()
    {
        #region two
        AddListener(Close, CloseShop);
        Search = Buttons.transform.Find("Search").GetComponent<Button>();
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
    }
    /// <summary>
    /// ��ʼ����Ʒ��
    /// </summary>
    private void InitShopPool()
    {
        loaderSvc.LoadPrefab(PathDefine.ResItem, PathDefine.ResShopItem, (GameObject obj) =>
        {
            GameObject gameObject = Instantiate(obj);
            pool = GameObjectPoolManager.Instance.CreatePrefabPool(gameObject);
            pool.MaxCount = 15;
            pool.cullMaxPerPass = 5;
            pool.cullAbove = 15;
            pool.cullDespawned = true;
            pool.cullDelay = 2;
            pool.Init();
            if (onLoadItems != null)
            {
                onLoadItems();
            }

        }, cache: true, instan: false);
    }

    /// <summary>
    /// �����̵�������Ʒ
    /// </summary>
    private void LoadItems()
    {
        ClearShop();
        ItemCfg[] ItemsData = GetItemData();//��ȡ���е���Ʒ��Դ
        allItems = ItemsData.ToList();
        for (int i = 0; i < allItems.Count; i++)
        {
            CreateItem(allItems, i);
        }

    }
    /// <summary>
    /// �����̵겿����Ʒ
    /// </summary>
    /// <param name="itemType">������Ʒ������</param>
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
    /// ������Ʒ
    /// </summary>
    private void CreateItem(List<ItemCfg> shopItems, int i)
    {
        GameObject gameObject = pool.GetObject();
        //������Ʒ
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
        if (ItemContent != null)  //��յ�ǰ���̵���Ʒ
        {
            for (int i = ItemContent.transform.childCount - 1; i >= 0; i--)
            {
                pool.ReturnObject(ItemContent.transform.GetChild(i).gameObject);
            }
        }
    }
    /// <summary>
    /// �������ť
    /// </summary>
    private void ClickEquip()
    {
        ClickItem(ItemType.equip, Equip.transform);
    }
    /// <summary>
    /// ������İ�ť
    /// </summary>
    private void ClickConsume()
    {
        ClickItem(ItemType.consume, Consume.transform);

    }
    /// <summary>
    /// ������ϰ�ť
    /// </summary>
    private void ClickMaterial()
    {
        ClickItem(ItemType.material, Material.transform);
    }
    /// <summary>
    /// ˢ��ָ�����͵���Ʒ
    /// </summary>
    private void ClickAll()
    {
        ClickItem(ItemType.all, All.transform);
    }
    /// <summary>
    /// ˢ��ָ�����͵���Ʒ
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="targetTransform"></param>
    private void ClickItem(ItemType itemType, Transform targetTransform)
    {
        RefreshItems(itemType);
        Vector3 newPosition = pointer.transform.position;
        newPosition.y = targetTransform.position.y;
        pointer.transform.position = newPosition;
    }
    /// <summary>
    /// ����Դ���ط����������̵���Ʒ��Ϣ
    /// </summary>
    /// <returns></returns>
    private ItemCfg[] GetItemData()
    {
        return resSvc.GetShopItemCfgData();
    }
    /// <summary>
    /// �ر��̵����
    /// </summary>
    private void CloseShop()
    {
        SetWndState(false);
    }
    /// <summary>
    /// �������ť
    /// </summary>
    /// <param name="id"></param>
    private void ClickBuy(int id)
    {
        BuyTipWnd.ShowWnd(id);
    }
    /// <summary>
    /// �رչ�����ʾ���
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
