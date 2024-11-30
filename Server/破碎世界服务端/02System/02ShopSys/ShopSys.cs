/****************************************************
    文件：ShopSys
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-27 15:47:20
	功能：商店购买系统
*****************************************************/
using CommonNet;
using ProtoBuf.Meta;
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
    private int SingleRafflePrice = 20; //抽奖价格
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
            DailyTaskSys.Instance.UpdateRewardTask(playerData);
            // 处理购买
            bool result = ProcessPurchase(shopItemCfg, playerData, shopItemCfg.Price, reqShop.buyType, reqShop.count);
            if (result)
            {
                // 处理成功
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
                        crystal = playerData.crystal,
                        dailyTasks = playerData.dailyTasks,
                    };
                }
            }
            else
            {
                // 处理失败
                switch (reqShop.buyType)
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
        else
        {
            msg.err = (int)Error.NotGoodError;
        }
        pack.session.SendMsg(msg);
    }
    /// <summary>
    /// 处理购买
    /// </summary>
    /// <param name="shopItemCfg"></param>
    /// <param name="playerData"></param>
    /// <param name="msg"></param>
    private bool ProcessPurchase(ShopItemCfg shopItemCfg, PlayerData playerData, float Price, BuyType buyType, int count)
    {
        float TotalPrice = Price * count;//购买总价格
        // 使用数组映射货币类型
        float[] currencies = { playerData.aura, playerData.ruvia, playerData.crystal };
        int index = (int)buyType;
        float currencyAmount = currencies[index];

        if (currencyAmount >= TotalPrice) // 判断是否有足够的货币
        {
            // 扣除货币
            switch (buyType)
            {
                case BuyType.aura:
                    playerData.aura -= TotalPrice;
                    ref int auraDailyTask = ref DailyTaskSys.Instance.GetPlayerDailyTask(DailyTaskType.aura, playerData);
                    auraDailyTask += (int)TotalPrice;
                    break;
                case BuyType.ruvia:
                    playerData.ruvia -= TotalPrice;
                    ref int ruviaDailyTask = ref DailyTaskSys.Instance.GetPlayerDailyTask(DailyTaskType.ruvia, playerData);
                    ruviaDailyTask += (int)TotalPrice;
                    break;
                case BuyType.crystal:
                    playerData.crystal -= TotalPrice;
                    ref int crystalDailyTask = ref DailyTaskSys.Instance.GetPlayerDailyTask(DailyTaskType.crystal, playerData);
                    crystalDailyTask += (int)TotalPrice;
                    break;
            }

            // 添加到背包
            AddOrUpdateItemInBag(playerData, shopItemCfg.ID, count);
            ref int DailyTask = ref DailyTaskSys.Instance.GetPlayerDailyTask(Enum.Parse<DailyTaskType>(shopItemCfg.bagType.ToString()), playerData);
            DailyTask += count;
            return true;
        }
        return false;
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

    public void ReqRaffleSingle(MsgPack pack)
    {
        ReqRaffleSingle reqRaffleSingle = pack.gameMsg.reqRaffleSingle;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspRaffleSingle
        };
        List<RaffleItemCfg> raffleItemCfgs = cfgSvc.GetRaffleItemCfgList();//获取抽奖物品配置
        if (raffleItemCfgs != null)
        {
            PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
            RaffleItemCfg randomItem = GetRaffleSingleReward(raffleItemCfgs);//获取奖励物品
            if (randomItem != null)
            {
                // 添加到背包
                ShopItemCfg shopItemCfg = cfgSvc.GetRaffleItemCfgData(randomItem.mShopID);//获取商店物品配置
                // 处理购买
                bool result = ProcessPurchase(shopItemCfg, playerData, SingleRafflePrice, BuyType.aura, randomItem.Count);
                if (result)
                {
                    // 处理成功
                    if (!cacheSvc.UpdatePlayerData(playerData))
                    {
                        msg.err = (int)Error.PerSonError;
                    }
                    else
                    {
                        msg.rspRaffleSingle = new RspRaffleSingle
                        {
                            RaffleId = randomItem.mShopID,
                            Bag = playerData.Bag,
                            aura = playerData.aura,
                            ruvia = playerData.ruvia,
                            crystal = playerData.crystal,

                        };
                    }
                }
                else
                {
                    // 处理失败
                    msg.err = (int)Error.NotAuraError;
                }
            }
        }
        else
        {
            msg.err = (int)Error.RaffleError;//抽奖失败
        }
        pack.session.SendMsg(msg);
    }
    /// <summary>
    /// 获取奖励物品
    /// </summary>
    /// <param name="goodsID"></param>
    /// <returns></returns>
    public RaffleItemCfg GetRaffleSingleReward(List<RaffleItemCfg> raffleItemCfgs)
    {
        // 1. 计算总概率（可选，确保概率总和是 1.0）
        float totalProbability = 0f;
        foreach (var item in raffleItemCfgs)
        {
            totalProbability += item.probability / 100f;
        }

        if (Math.Abs(totalProbability - 1f) > 0.001f)
        {
            Console.WriteLine("Warning: 总概率不为1，可能需要调整概率分配。");
        }

        // 2. 生成一个0到1之间的随机数
        Random random = new Random();
        float randomValue = (float)random.NextDouble(); // [0, 1)
        
        // 3. 根据随机值落入的概率区间选择物品
        float cumulativeProbability = 0f;
        foreach (var item in raffleItemCfgs)
        {
            cumulativeProbability += item.probability / 100f;
            if (randomValue <= cumulativeProbability)
            {
                return item;
            }
        }

        // 4. 如果未命中，返回一个默认值（避免浮点精度问题）
        return raffleItemCfgs[^1]; // 返回最后一个物品的ID
    }

    public void ReqRaffleTan(MsgPack pack)
    {
        ReqRaffleTan reqRaffleTan = pack.gameMsg.reqRaffleTan;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspRaffleTan
        };

        List<RaffleItemCfg> raffleItemCfgs = cfgSvc.GetRaffleItemCfgList(); // 获取抽奖物品配置
        if (raffleItemCfgs == null)
        {
            msg.err = (int)Error.RaffleError; // 抽奖失败
            pack.session.SendMsg(msg);
            return;
        }

        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        if (playerData.aura < SingleRafflePrice * 10)
        {
            msg.err = (int)Error.NotAuraError;
            pack.session.SendMsg(msg);
            return;
        }

        List<RaffleItemCfg> tanItems = new List<RaffleItemCfg>();
        for (int i = 0; i < 10; i++)
        {
            tanItems.Add(GetRaffleSingleReward(raffleItemCfgs));
        }

        if (tanItems == null || tanItems.Count == 0)
        {
            msg.err = (int)Error.RaffleError; // 抽奖失败
            pack.session.SendMsg(msg);
            return;
        }

        foreach (var item in tanItems)
        {
            ShopItemCfg shopItemCfg = cfgSvc.GetRaffleItemCfgData(item.mShopID); // 获取商店物品配置
            bool result = ProcessPurchase(shopItemCfg, playerData, SingleRafflePrice, BuyType.aura, item.Count);
            if (!result)
            {
                msg.err = (int)Error.PerSonError;
                break;
            }
        }

        if (msg.err == 0)
        {
            if (!cacheSvc.UpdatePlayerData(playerData))
            {
                msg.err = (int)Error.PerSonError;
            }
            else
            {
                List<int> raffleList = tanItems.Select(item => item.mShopID).ToList();
                msg.rspRaffleTan = new RspRaffleTan
                {
                    RaffleList = raffleList,
                    Bag = playerData.Bag,
                    aura = playerData.aura,
                    ruvia = playerData.ruvia,
                    crystal = playerData.crystal,
                };
            }
        }

        pack.session.SendMsg(msg);
    }


}