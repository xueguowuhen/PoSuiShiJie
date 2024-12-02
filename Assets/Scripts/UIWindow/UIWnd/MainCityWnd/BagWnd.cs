/****************************************************
    文件：BagWnd.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/26 22:3:27
	功能：背包管理系统
*****************************************************/

using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using CommonNet;
using System.Linq;
using System;
using System.Reflection;
using System.Collections;

public class BagWnd : WindowRoot
{
    #region Bag
    public Button AllBtn;
    public Button EquipBtn;
    public Button materialBtn;
    public Button consumeBtn;
    public Button SearchBtn;
    public Button CloseBtn;
    public GameObject BagContent;
    #endregion
    public BagTipWnd BagTipWnd; //物品提示信息面板
    private GameObject UseTipWnd; //物品选择信息面板
    private GameObjectPool pool;
    public GameObject pointer;
    private Dictionary<ItemCfg, int> allDic = new Dictionary<ItemCfg, int>();
    private Action onLoadItem;
    public GameObject DownLoad;
    public string DownLoadUrl;
    public Image DownLoadImg;
    protected override void InitWnd()
    {
        base.InitWnd();
        if (pool == null)
        {
            SetActive(DownLoad, true);
            onLoadItem =LoadItems;
        }
        else
        {
            LoadItems();
        }
        CloseBag();
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
        newPosition.y = AllBtn.transform.position.y;
        pointer.transform.position = newPosition;
    }
    protected override void SetGameObject()
    {
        #region Bag
        AddListener(AllBtn, ClickAll);
        AddListener(EquipBtn, ClickEquip);
        AddListener(materialBtn, ClickMaterial);
        AddListener(consumeBtn, ClickConsume);
        AddListener(CloseBtn, ClickClose);
        //BagContent = GetGameObject(PathDefine.BagContent);
        //tipInfoWnd = resSvc.LoadPrefab(PathDefine.ResTipInfoWnd);
        //tipInfoWnd.transform.SetParent(this.transform, false);
        //tipInfoWnd.SetActive(false);
        //UseTipWnd = resSvc.LoadPrefab(PathDefine.ResUseTipWnd);
        //UseTipWnd.transform.SetParent(this.transform, false);
        //UseTipWnd.GetComponent<UseTipWnd>().SetWndState(false);
        #endregion
        InitBagPool();
    }
    private void InitBagPool()
    {
        DownLoadUrl = PathDefine.ResBagItem;
        loaderSvc.LoadPrefab(PathDefine.ResItem, DownLoadUrl, (GameObject go) =>
       {
           GameObject gameObject = go;
           pool = GameObjectPoolManager.Instance.CreatePrefabPool(gameObject);
           pool.MaxCount = 15;
           pool.cullMaxPerPass = 5;
           pool.cullAbove = 15;
           pool.cullDespawned = true;
           pool.cullDelay = 2;
           DownLoadUrl=null;
           pool.Init();
           if (onLoadItem!= null)
           {
               onLoadItem();
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
    private void ClearBag()
    {
        if (pool == null) return;
        if (BagContent != null)  //清空当前的商店物品
        {
            for (int i = BagContent.transform.childCount - 1; i >= 0; i--)
            {
                pool.ReturnObject(BagContent.transform.GetChild(i).gameObject);
            }
        }
    }

    /// <summary>
    /// 加载商店所有物品
    /// </summary>
    private void LoadItems()
    {
        SetActive(DownLoad, false);
        ClearBag();
        allDic.Clear();
        List<BagList> bagList = GameRoot.Instance.PlayerData.Bag;//获取所有的背包资源
        for (int i = 0; i < bagList.Count; i++)
        {
            int bagID = bagList[i].GoodsID;
            ItemCfg shopItemCfg = resSvc.GetShopItemCfg(bagID);
            if (shopItemCfg == null)
            {
                continue;
            }
            allDic.Add(shopItemCfg, bagList[i].count);
            CreateItem(shopItemCfg, bagList[i].count);
        }
        FillBagItem(bagList.Count);
    }
    private void FillBagItem(int currentCount)
    {
        int itemsToAdd = 20 - currentCount; // 计算需要补充的数量
        for (int i = 0; i < itemsToAdd; i++)
        {
            // 假设 CreateItem 支持批量创建
            CreateNoneItem();
        }

    }

    /// <summary>
    /// 刷新指定类型的物品
    /// </summary>
    /// <param name="itemType"></param>
    /// <param name="targetTransform"></param>
    private void ClickItem(ItemType itemType, Transform targetTransform)
    {
        RefreshItems(itemType);
        Vector3 newPosition = pointer.transform.position;
        newPosition.y = targetTransform.position.y;
        pointer.transform.position = newPosition;
       // Debug.Log(pointer.transform.position);
    }

    /// <summary>
    /// 加载商店部分物品
    /// </summary>
    /// <param name="itemType">部分物品的类型</param>
    private void RefreshItems(ItemType itemType)
    {
        ClearBag();
        // 添加计数器
        int count = 0;
        // 判断是否有符合条件的物品
        if (allDic.Keys.Any(item => item.type == itemType))
        {
            // 将符合条件的物品添加到新字典中
            foreach (var item in allDic.Keys.Where(item => item.type == itemType))
            {
                CreateItem(item, allDic[item]);
                count++;
            }
        }
        else
        {
            if (itemType == ItemType.all)
            {
                // 如果没有符合条件的物品，遍历全部物品
                foreach (var item in allDic.Keys)
                {
                    CreateItem(item, allDic[item]);
                    count++;
                }
            }
        }
        FillBagItem(count);
    }
    private void CreateNoneItem()
    {
        GameObject gameObject = pool.GetObject();
        //设置物品
        BagItem bagItem = gameObject.GetOrAddComponent<BagItem>();
        bagItem.SetNone();
        gameObject.transform.SetParent(BagContent.transform, false);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;
    }
    /// <summary>
    /// 加载背包列表
    /// </summary>
    private void CreateItem(ItemCfg bagItemCfgs, int count)
    {
        GameObject gameObject = pool.GetObject();
        //设置物品
        BagItem bagItem = gameObject.GetOrAddComponent<BagItem>();
        bagItem.SetUI(bagItemCfgs, count);
        gameObject.transform.SetParent(BagContent.transform, false);
        gameObject.transform.localPosition = Vector3.zero;
        gameObject.transform.localScale = Vector3.one;
        Button button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(delegate { ClickBag(bagItemCfgs.ID); });
        //List<BagList> LoadItems;
        //if (Items.TryGetValue(CurrentType, out LoadItems))//根据背包当前状态获取内容
        //{
        //    BagList[] BagItems = LoadItems.ToArray();
        //    for (int i = 0; i < LoadItems.Count; i++)
        //    {
        //        ShopItemCfg shopItemCfg = resSvc.GetShopItemCfg(BagItems[i].GoodsID); //背包物品数据
        //        if (shopItemCfg.type == ItemType.euqip)
        //        {
        //            //加载出装备时的特殊处理
        //            for (int j = 0; j < BagItems[i].count; j++)
        //            {
        //                GameObject EquipItem = resSvc.LoadPrefab(PathDefine.ResBagItem); //背包物品预制体
        //                SetBagItemData(EquipItem, shopItemCfg);
        //                //加载多个装备出来
        //            }
        //            continue;
        //        }
        //        GameObject OtherItem = resSvc.LoadPrefab(PathDefine.ResBagItem); //背包物品预制体
        //        SetBagItemData(OtherItem, shopItemCfg, BagItems[i].count);
        //    }
        //}
    }
    private void ClickClose()
    {
        MainCitySys.instance.OpenPerson();
        SetWndState(false);
    }
    private void ClickAll()
    {
        ClickItem(ItemType.all, AllBtn.transform);
    }
    private void ClickMaterial()
    {
        ClickItem(ItemType.material, materialBtn.transform);
    }
    private void ClickEquip()
    {
        ClickItem(ItemType.equip, EquipBtn.transform);
    }
    private void ClickConsume()
    {
        ClickItem(ItemType.consume, consumeBtn.transform);
    }

    private void ClickItem(ItemCfg shopItemCfg, Transform transform)
    {

        //switch (shopItemCfg.type)
        //{
        //    case ItemType.consume:
        //        {
        //            SetUseTipWnd("使用", shopItemCfg.mInfo, transform);
        //            break;
        //        }
        //    case ItemType.equip:
        //        {
        //            SetUseTipWnd("装备", shopItemCfg.mInfo, transform);
        //            break;
        //        }
        //    case ItemType.material:
        //        {
        //            SetUseTipWnd("强化", shopItemCfg.mInfo, transform);
        //            break;
        //        }
        //}
    }
    /// <summary>
    /// 点击购买按钮
    /// </summary>
    /// <param name="id"></param>
    private void ClickBag(int id)
    {
        BagTipWnd.ShowWnd(id);
    }
    /// <summary>
    /// 关闭购买提示面板
    /// </summary>
    public void CloseBag()
    {
        BagTipWnd.SetWndState(false);
    }
    private void SetUseTipWnd(string button, string info, Transform transform)
    {
        UseTipWnd temp = UseTipWnd.GetComponent<UseTipWnd>();
        temp.ButtonContent = button;
        temp.ItemInfoContent = info;
        UseTipWnd.GetComponent<UseTipWnd>().SetWndState();
        //UseTipWnd.transform.SetParent();
    }
    protected override void ClearWnd()
    {
        base.ClearWnd();
        onLoadItem=null;
    }

}