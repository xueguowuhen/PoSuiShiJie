/****************************************************
    文件：ShopSys
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-27 15:47:20
	功能：商店购买系统
*****************************************************/
using CommonNet;
using static CfgSvc;

public class ShopSys
{
    private static ShopSys instance = null;
    public static ShopSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new ShopSys();
            }
            return instance;
        }
    }
    private CacheSvc cacheSvc;
    private CfgSvc cfgSvc;
    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        cfgSvc = CfgSvc.Instance;
        GameCommon.Log("ShopSys Init Done");
    }
    public void ReqShop(MsgPack pack)
    {
        ReqShop reqShop = pack.gameMsg.reqShop;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspShop
        };
        ShopItemCfg shopItemCfg = cfgSvc.GetShopItemCfgData(reqShop.id);//获取商店物品配置
        if (shopItemCfg != null)
        {
            PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
            BuyType buyType = reqShop.buyType;
            float count = shopItemCfg.Price * reqShop.count;//购买总价格
            // 处理购买
            ProcessPurchase(buyType, count, playerData, reqShop, msg);
        }
        else
        {
            msg.err = (int)Error.NotGoodError;
        }
        pack.session.SendMsg(msg);
    }
    /// <summary>
    /// 处理购买
    /// </summary>
    /// <param name="buyType"> 购买类型 </param>
    /// <param name="count"> 购买总价格 </param>
    /// <param name="playerData"> 玩家数据 </param>
    /// <param name="reqShop"> 请求购买数据 </param>
    /// <param name="msg"> 返回消息 </param>
    private void ProcessPurchase(BuyType buyType, float count, PlayerData playerData, ReqShop reqShop, GameMsg msg)
    {
        // 使用数组映射货币类型
        float[] currencies = { playerData.aura, playerData.ruvia, playerData.crystal };
        int index = (int)buyType;
        float currencyAmount = currencies[index];

        if (currencyAmount >= count) // 判断是否有足够的货币
        {
            // 扣除货币
            switch (buyType)
            {
                case BuyType.aura:
                    playerData.aura -= count;
                    break;
                case BuyType.ruvia:
                    playerData.ruvia -= count;
                    break;
                case BuyType.crystal:
                    playerData.crystal -= count;
                    break;
            }

            // 添加到背包
            AddOrUpdateItemInBag(playerData, reqShop.id, reqShop.count);

            if (!cacheSvc.UpdatePlayerData(playerData))
            {
                msg.err = (int)Error.PerSonError;
            }
            else
            {
                msg.rspShop = new RspShop
                {
                    Bag = playerData.Bag,
                    aura = playerData.aura,
                    ruvia = playerData.ruvia,
                    crystal = playerData.crystal
                };
            }
        }
        else
        {
            switch (buyType)
            {
                case BuyType.aura:
                    msg.err = (int)Error.NotAuraError;
                    break;
                case BuyType.ruvia:
                    msg.err = (int)Error.NotRuviaError;
                    break;
                case BuyType.crystal:
                    msg.err = (int)Error.NotCrystalError;
                    break;
            }
        }
    }
    /// <summary>
    /// 添加或更新背包物品
    /// </summary>
    /// <param name="playerData"></param>
    /// <param name="goodsID"></param>
    /// <param name="count"></param>
    public void AddOrUpdateItemInBag(PlayerData playerData, int goodsID, int count)
    {
        if (playerData.Bag == null)
        {
            playerData.Bag = new List<BagList>(); // 确保背包不为空
        }

        // 查找是否已经有了该物品
        var existingItem = playerData.Bag.FirstOrDefault(bagList => bagList.GoodsID == goodsID);

        if (existingItem != null)
        {
            // 更新数量
            existingItem.count += count;
        }
        else
        {
            // 添加新物品
            playerData.Bag.Add(new BagList
            {
                GoodsID = goodsID,
                count = count,
            });
        }
    }


}