/****************************************************
    文件：CfgSvc
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-17 14:34:31
	功能：配置数据服务
*****************************************************/
using ComNet;
using System.Resources;
using System.Xml;
using System.IO;
using Org.BouncyCastle.Ocsp;
using CommonNet;

public class CfgSvc
{
    private static CfgSvc instance = null!;
    public static CfgSvc Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new CfgSvc();
            }
            return instance;
        }
    }
    private string Path = System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName, "ResCfg/");
    public void Init()
    {
        InitCharaterCfg();
        InitTalentCfg();
        InitShopItemCfg();
        InitTaskCfg();
        InitTaskRewardCfg();
        InitTaskDailyCfg();
        GameCommon.Log("CfgSvc Init Done");
    }
    #region 加载人物配置
    private Dictionary<int, personCfg> personDic = new Dictionary<int, personCfg>();
    private void InitCharaterCfg()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Path + "person.xml");//读取文本数据
        //doc.Load(@"ResCfg/person.xml");//读取文本数据
        XmlNodeList nodeList = doc.SelectSingleNode("root")!.ChildNodes;//选择根节点为root的节点
        for (int i = 0; i < nodeList.Count; i++)
        {
            XmlElement ele = (nodeList[i] as XmlElement)!;
            if (ele.GetAttributeNode("ID") == null)
            {
                continue;
            }
            int ID = Convert.ToInt32(ele.GetAttributeNode("ID")!.InnerText);//获取ID中的数据
            personCfg person = new personCfg()
            {
                ID = ID,
            };
            foreach (XmlElement node in nodeList[i]!.ChildNodes)
            {
                switch (node.Name)
                {
                    case "mName":
                        person.type = node.InnerText;
                        break;
                    case "HP":
                        person.HP = int.Parse(node.InnerText);
                        break;
                    case "Mana":
                        person.Mana = int.Parse(node.InnerText);
                        break;
                    case "Power":
                        person.Power = int.Parse(node.InnerText);
                        break;
                    case "aura":
                        person.aura = int.Parse(node.InnerText);
                        break;
                    case "ruvia":
                        person.ruvia = int.Parse(node.InnerText);
                        break;
                    case "crystal":
                        person.crystal = int.Parse(node.InnerText);
                        break;
                    case "ad":
                        person.ad = int.Parse(node.InnerText);
                        break;
                    case "ap":
                        person.ap = int.Parse(node.InnerText);
                        break;
                    case "addef":
                        person.addef = int.Parse(node.InnerText);
                        break;
                    case "adpdef":
                        person.adpdef = int.Parse(node.InnerText);
                        break;
                    case "dodge":
                        person.dodge = int.Parse(node.InnerText);
                        break;
                    case "practice":
                        person.practice = float.Parse(node.InnerText);
                        break;
                    case "critical":
                        person.critical = int.Parse(node.InnerText);
                        break;
                }
            }
            personDic.Add(person.ID, person);
        }
        GameCommon.Log("personCfg Init Done.");
    }
    public personCfg GetPersonCfgData(int id)
    {
        personCfg personCfg = null!;
        if (personDic.TryGetValue(id, out personCfg!))
        {
            return personCfg;
        }
        return null!;
    }
    #endregion
    #region 加载天赋配置
    private Dictionary<int, TalentCfg> TalentDic = new Dictionary<int, TalentCfg>();
    private void InitTalentCfg()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Path + "Talent.xml");//读取文本数据
        //doc.Load(@"ResCfg/TalentList.xml");//读取文本数据
        XmlNodeList nodeList = doc.SelectSingleNode(("root"))!.ChildNodes;//选择根节点为root的节点
        for (int i = 0; i < nodeList.Count; i++)
        {
            XmlElement ele = (nodeList[i] as XmlElement)!;
            if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
            {
                continue;
            }
            int ID = Convert.ToInt32(ele.GetAttributeNode("ID")!.InnerText);//获取ID中的数据
            TalentCfg talentCfg = new TalentCfg()
            {
                ID = ID,
            };
            foreach (XmlElement node in nodeList[i]!.ChildNodes)
            {
                switch (node.Name)
                {
                    case "Attribute":
                        talentCfg.Attribute = node.InnerText;
                        break;
                    case "MaxLevel":
                        talentCfg.MaxLevel = int.Parse(node.InnerText);
                        break;
                    case "Value":
                        talentCfg.Value = float.Parse(node.InnerText);
                        break;
                }
            }
            TalentDic.Add(ID, talentCfg);
        }
    }
    public TalentCfg GetTalentCfgData(int id)
    {
        TalentCfg talentCfg = null!;
        if (TalentDic.TryGetValue(id, out talentCfg!))
        {
            return talentCfg;
        }
        return null!;
    }
    public int[] GetTalentsId()
    {
        int[] temp = new int[TalentDic.Count];
        int j = 0;
        foreach (KeyValuePair<int, TalentCfg> i in TalentDic)
        {
            temp[j] = i.Key;
            j++;
        }
        return temp;
    }
    #endregion
    #region 加载商店物品配置
    private Dictionary<int, ShopItemCfg> ShopItemDic = new Dictionary<int, ShopItemCfg>();
    private void InitShopItemCfg()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Path + "ShopItem.xml");//读取文本数据
        //doc.Load(@"ResCfg/ShopItem.xml");//读取文本数据
        XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
        for (int i = 0; i < nodeList.Count; i++)
        {
            XmlElement ele = nodeList[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
            {
                //GameCommon.Log("ID读取失败");
                continue;
            }
            int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
                                                                           //GameCommon.Log(ID);
            ShopItemCfg shopitemCfg = new ShopItemCfg()
            {
                ID = ID,
            };
            foreach (XmlElement node in nodeList[i].ChildNodes)
            {
                switch (node.Name)
                {
                    case "Price":
                        {
                            shopitemCfg.Price = float.Parse(node.InnerText);
                            break;
                        }
                    case "type":
                        {
                            shopitemCfg.bagType = Enum.Parse<BagType>(node.InnerText);
                            break;
                        }
                }

            }
            ShopItemDic.Add(ID, shopitemCfg);
        }
    }
    public ShopItemCfg GetShopItemCfgData(int id)
    {
        ShopItemCfg shopItemCfg = null;
        if (ShopItemDic.TryGetValue(id, out shopItemCfg))
        {
            return shopItemCfg;
        }
        return null;
    }
    #endregion

    #region 加载任务配置
    private Dictionary<int, TaskCfg> TaskDic = new Dictionary<int, TaskCfg>();
    private void InitTaskCfg()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Path + "guide.xml");//读取文本数据
        XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
        for (int i = 0; i < nodeList.Count; i++)
        {
            XmlElement ele = nodeList[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
            {
                continue;
            }
            int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
            TaskCfg taskCfg = new TaskCfg()
            {
                ID = ID,
            };
            foreach (XmlElement node in nodeList[i].ChildNodes)
            {
                switch (node.Name)
                {
                    case "exp":
                        taskCfg.exp = int.Parse(node.InnerText);
                        break;
                    case "aura":
                        taskCfg.aura = int.Parse(node.InnerText);
                        break;
                }
            }
            TaskDic.Add(ID, taskCfg);
        }
    }
    public TaskCfg GetTaskCfgData(int id)
    {
        TaskCfg taskCfg = null;
        if (TaskDic.TryGetValue(id, out taskCfg))
        {
            return taskCfg;
        }
        return null;
    }
    public int GetTaskCfgOne()
    {
        return TaskDic.FirstOrDefault().Value.ID;
    }
    #endregion

    #region 加载任务奖励配置
    private Dictionary<int, TaskRewardCfg> TaskRewardDic = new Dictionary<int, TaskRewardCfg>();
    private void InitTaskRewardCfg()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Path + "TaskReward.xml");//读取文本数据
        XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
        for (int i = 0; i < nodeList.Count; i++)
        {
            XmlElement ele = nodeList[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
            {
                continue;
            }
            int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
            TaskRewardCfg taskRewardCfg = new TaskRewardCfg()
            {
                ID = ID,
            };
            foreach (XmlElement node in nodeList[i].ChildNodes)
            {
                switch (node.Name)
                {
                    case "Value":
                        taskRewardCfg.Value = int.Parse(node.InnerText);
                        break;
                    case "Reward":
                        string[] strs = node.InnerText.Split('|');
                        taskRewardCfg.rewardItems = new List<TaskRewardItem>();
                        for (int j = 0; j < strs.Length; j++)
                        {
                            string[] Reward = strs[j].Split('#');
                            taskRewardCfg.rewardItems.Add(new TaskRewardItem()
                            {
                                ItemID = int.Parse(Reward[0]),
                                Count = int.Parse(Reward[1]),
                            });
                        }
                        break;
                }
            }
            TaskRewardDic.Add(ID, taskRewardCfg);
        }
    }
    public int GetTaskRewardCfgIndex(int id)
    {
        List<TaskRewardCfg> rewardItems = TaskRewardDic.Values.ToList();
        return rewardItems.FindIndex(x => x.ID == id);
    }

    public TaskRewardCfg GetTaskRewardCfgListData(int index)
    {
        List<TaskRewardCfg> rewardItems = TaskRewardDic.Values.ToList();
        for (int i = 0; i < rewardItems.Count; i++)
        {
            if (rewardItems[i].ID == index)
            {
                return rewardItems[i];
            }
        }
        return null;
    }
    /// <summary>
    /// 根据id获取活跃度
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int GetTaskRewardActive(int id)
    {
        TaskRewardCfg taskRewardCfg = null;
        if (TaskRewardDic.TryGetValue(id, out taskRewardCfg))
        {
            return taskRewardCfg.Value;
        }
        return 0;
    }
    public int GetTaskRewardCount()
    {
        return TaskRewardDic.Count;
    }
    #endregion

    #region 加载每日任务配置
    private Dictionary<int, TaskDailyCfg> TaskDailyDic = new Dictionary<int, TaskDailyCfg>();
    private void InitTaskDailyCfg()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Path + "DailyTask.xml");//读取文本数据
        XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
        for (int i = 0; i < nodeList.Count; i++)
        {
            XmlElement ele = nodeList[i] as XmlElement;
            if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
            {
                continue;
            }
            int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
            TaskDailyCfg taskDailyCfg = new TaskDailyCfg()
            {
                ID = ID,
            };
            foreach (XmlElement node in nodeList[i].ChildNodes)
            {
                switch (node.Name)
                {
                    case "Active":
                        taskDailyCfg.Active = int.Parse(node.InnerText);
                        break;
                    case "Count":
                        taskDailyCfg.Count = int.Parse(node.InnerText);
                        break;
                }
            }
            TaskDailyDic.Add(ID, taskDailyCfg);
        }
    }
    public TaskDailyCfg GetTaskDailyCfgData(int id)
    {
        TaskDailyCfg taskDailyCfg = null;
        if (TaskDailyDic.TryGetValue(id, out taskDailyCfg))
        {
            return taskDailyCfg;
        }
        return null;
    }
    /// <summary>
    /// 根据id获取活跃度
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public int GettTaskDailyActive(int id)
    {
        TaskDailyCfg taskDailyCfg = null;
        if (TaskDailyDic.TryGetValue(id, out taskDailyCfg))
        {
            return taskDailyCfg.Active;
        }
        return 0;
    }
    /// <summary>
    /// 获取任务奖励配置数据
    /// </summary>
    /// <returns></returns>
    public List<DailyTask> GetTaskDailyCfgData()
    {
        List<DailyTask> taskDailyCfgs = new List<DailyTask>();

        foreach (var item in TaskDailyDic)
        {
            taskDailyCfgs.Add(new DailyTask()
            {
                TaskID = item.Value.ID,
                TaskReward = 0,
                TaskFinish = false,
            });
        }
        return taskDailyCfgs;
    }
    #endregion
    public class personCfg : BaseData<personCfg>
    {
        public string? type;
        public int HP;
        public int Mana;
        public int Power;
        public int aura;
        public int ruvia;
        public int crystal;
        public int ad;
        public int ap;
        public int addef;
        public int adpdef;
        public int dodge;
        public float practice;
        public int critical;
    }
    public class TalentCfg : BaseData<TalentCfg>
    {
        public int MaxLevel;
        public string? Attribute; //属性 天赋对应增加的词条(Hp,Atk...)
        public float Value; //基础1级数值(与等级×相关系数 = 对应等级数值)
    }
    public class TaskCfg : BaseData<TaskCfg>
    {
        public int exp;
        public int aura;
    }
    public class TaskRewardCfg : BaseData<TaskRewardCfg>
    {
        public int Value;//每日奖励的目标值
        public List<TaskRewardItem> rewardItems;
    }
    public class TaskRewardItem
    {
        public int ItemID;  //奖励物品ID
        public int Count;  //奖励物品数量
    }
    public class TaskDailyCfg : BaseData<TaskDailyCfg>
    {
        public int Active;
        public int Count;
    }
    public class ShopItemCfg : BaseData<ShopItemCfg>
    {
        public float Price;  //物价价格
        public BagType bagType;  //物品类型
    }
    public class BaseData<T>
    {
        public int ID;
    }
}