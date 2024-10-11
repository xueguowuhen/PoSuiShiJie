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
    private string Path = System.IO.Path.Combine(Directory.GetParent(Directory.GetCurrentDirectory())!.Parent!.Parent!.FullName,"ResCfg/");
    public void Init()
    {
        InitCharaterCfg();
        InitTalentCfg();
        InitShopItemCfg();
        InitTaskCfg();
        GameCommon.Log("CfgSvc Init Done");
    }
    #region 加载人物配置
    private Dictionary<int, personCfg> personDic = new Dictionary<int, personCfg>();
    private void InitCharaterCfg()
    {
        XmlDocument doc = new XmlDocument();
        doc.Load(Path+"person.xml");//读取文本数据
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
        doc.Load(Path + "TalentList.xml");//读取文本数据
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
                    case "HP":
                        talentCfg.HP = int.Parse(node.InnerText);
                        break;
                    case "Mana":
                        talentCfg.Mana = int.Parse(node.InnerText);
                        break;
                    case "Power":
                        talentCfg.Power = int.Parse(node.InnerText);
                        break;
                    case "aura":
                        talentCfg.aura = int.Parse(node.InnerText);
                        break;
                    case "ruvia":
                        talentCfg.ruvia = int.Parse(node.InnerText);
                        break;
                    case "crystal":
                        talentCfg.crystal = int.Parse(node.InnerText);
                        break;
                    case "ad":
                        talentCfg.ad = int.Parse(node.InnerText);
                        break;
                    case "ap":
                        talentCfg.ap = int.Parse(node.InnerText);
                        break;
                    case "addef":
                        talentCfg.addef = int.Parse(node.InnerText);
                        break;
                    case "adpdef":
                        talentCfg.adpdef = int.Parse(node.InnerText);
                        break;
                    case "dodge":
                        talentCfg.dodge = int.Parse(node.InnerText);
                        break;
                    case "practice":
                        talentCfg.practice = float.Parse(node.InnerText);
                        break;
                    case "critical":
                        talentCfg.critical = int.Parse(node.InnerText);
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
    public int GetTalentCount()
    {
        return TalentDic.Count;
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
        //doc.Load(@"ResCfg/guide.xml");//读取文本数据
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
    public class personCfg : BaseData<personCfg>
    {
        public string type;
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
    public class TalentCfg : BaseData<personCfg>
    {
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
    public class TaskCfg:BaseData<TaskCfg>
    {
        public int exp;
        public int aura;
    }
    public class ShopItemCfg : BaseData<ShopItemCfg>
    {
        public float Price;  //物价价格

    }
    public class BaseData<T>
    {
        public int ID;
    }
}