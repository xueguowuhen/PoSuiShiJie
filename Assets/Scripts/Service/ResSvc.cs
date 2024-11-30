/****************************************************
    文件：ResSvc
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-14 14:53:41
	功能：资源加载服务
*****************************************************/
using CommonNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ResSvc : SvcBase<ResSvc>
{
   
    //后期可改成模板生成工具
    public override void InitSvc()
    {
        base.InitSvc();
        InitCharaterCfg(PathDefine.personCfg);
        InitTalentCfg(PathDefine.TalentCfg);
        InitRDNameCfg(PathDefine.RdnameCfg);
        InitShopItemCfg(PathDefine.ShopItemCfg);
        InitMapCfg(PathDefine.mapCfg);
        InitTaskCfg(PathDefine.TaskCfg);
        InitTaskDailyCfg(PathDefine.TaskDailyCfg);
        InitTaskRewardCfg(PathDefine.TaskRewardCfg);
        InitRaffleItemCfg(PathDefine.RaffleItemCfg);
        InitSkillCfg(PathDefine.SkillCfg);
        //    InitSkillMoveCfg(PathDefine.SkillMoveCfg);
        InitSkillActionCfg(PathDefine.SkillActionCfg);
        InitSkillFxCfg(PathDefine.SkillFxCfg);
        InitEnemyCfg(PathDefine.EnemyCfg);
        GameCommon.Log("ResSvc Init....");
    }
    public override void OnUpdate()
    {
        base.OnUpdate();

    }


    #region 加载人物配置
    private Dictionary<int, personCfg> personDic = new Dictionary<int, personCfg>();
    private void InitCharaterCfg(string Path)
    {
        TextAsset xml = GetTextAsset(Path);
        if (!xml)
        {
            GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
            XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlElement ele = nodeList[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
                {
                    continue;
                }
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
                personCfg person = new personCfg()
                {
                    ID = ID,
                    NormalAtkList = new List<int>(),
                    SkillList = new List<int>(),
                };
                foreach (XmlElement node in nodeList[i].ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "mName":
                            person.type = node.InnerText;
                            break;
                        case "Hard":
                            person.Hard = node.InnerText;
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
                        case "Intro":
                            person.Intro = node.InnerText;
                            break;
                        case "fight":
                            person.fight = node.InnerText;
                            break;
                        case "Prefab":
                            person.Prefab = node.InnerText;
                            break;
                        case "PreText":
                            person.PreText = int.Parse(node.InnerText);
                            break;
                        case "Skill":
                            string[] str = node.InnerText.Split('|');
                            foreach (string str2 in str)
                            {
                                person.SkillList.Add(int.Parse(str2));
                            }
                            break;
                        case "NormalAtk":
                            string[] strs = node.InnerText.Split('|');
                            foreach (string str2 in strs)
                            {
                                person.NormalAtkList.Add(int.Parse(str2));
                            }
                            break;
                        case "BaseExp":
                            person.BaseExp = int.Parse(node.InnerText);
                            break;
                        case "ExpMul":
                            person.ExpMul = float.Parse(node.InnerText);
                            break;
                    }
                }
                personDic.Add(ID, person);
            }
        }
        GameCommon.Log("personCfg Init Done.");
    }
    /// <summary>
    /// 获取人物配置数据
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public personCfg GetPersonCfgData(int id)
    {
        personCfg personCfg = null;
        if (personDic.TryGetValue(id, out personCfg))
        {
            return personCfg;
        }
        return null;
    }
    /// <summary>
    /// 获取人物头像
    /// </summary>
    /// <param name="id"></param>
    /// <param name="callback"></param>
    public void GetPersonCfgHard(int id, Action<Texture2D> callback)
    {
        personCfg personCfg = null;
        if (personDic.TryGetValue(id, out personCfg))
        {
            Debug.Log(PathDefine.ResHard + personCfg.Hard);
            ComTools.GetImg(personCfg.Hard, callback);
            
        }
    }
    #endregion

    #region 加载天赋配置(弃用)
    //private Dictionary<int, TalentCfg> TalentDic = new Dictionary<int, TalentCfg>();
    //private void InitTalentCfg(string Path)
    //{
    //    TextAsset xml = Resources.Load<TextAsset>(Path);
    //    if (!xml)
    //    {
    //        GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
    //    }
    //    else
    //    {
    //        XmlDocument doc = new XmlDocument();
    //        doc.LoadXml(xml.text);//读取文本数据
    //        XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
    //        for (int i = 0; i < nodeList.Count; i++)
    //        {
    //            XmlElement ele = nodeList[i] as XmlElement;
    //            if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
    //            {
    //                continue;
    //            }
    //            int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
    //            TalentCfg talentCfg = new TalentCfg()
    //            {
    //                ID = ID,
    //            };
    //            foreach (XmlElement node in nodeList[i].ChildNodes)
    //            {
    //                switch (node.Name)
    //                {
    //                    case "mName":
    //                        talentCfg.mName = node.InnerText;
    //                        break;
    //                    case "HP":
    //                        talentCfg.HP = int.Parse(node.InnerText);
    //                        break;
    //                    case "Mana":
    //                        talentCfg.Mana = int.Parse(node.InnerText);
    //                        break;
    //                    case "Power":
    //                        talentCfg.Power = int.Parse(node.InnerText);
    //                        break;
    //                    case "aura":
    //                        talentCfg.aura = int.Parse(node.InnerText);
    //                        break;
    //                    case "ruvia":
    //                        talentCfg.ruvia = int.Parse(node.InnerText);
    //                        break;
    //                    case "crystal":
    //                        talentCfg.crystal = int.Parse(node.InnerText);
    //                        break;
    //                    case "ad":
    //                        talentCfg.ad = int.Parse(node.InnerText);
    //                        break;
    //                    case "ap":
    //                        talentCfg.ap = int.Parse(node.InnerText);
    //                        break;
    //                    case "addef":
    //                        talentCfg.addef = int.Parse(node.InnerText);
    //                        break;
    //                    case "adpdef":
    //                        talentCfg.adpdef = int.Parse(node.InnerText);
    //                        break;
    //                    case "dodge":
    //                        talentCfg.dodge = int.Parse(node.InnerText);
    //                        break;
    //                    case "practice":
    //                        talentCfg.practice = float.Parse(node.InnerText);
    //                        break;
    //                    case "critical":
    //                        talentCfg.critical = int.Parse(node.InnerText);
    //                        break;
    //                    case "quality":
    //                        switch (node.InnerText)
    //                        {
    //                            case "垃圾":
    //                                talentCfg.quality = TalentQuality.garbage;
    //                                break;
    //                            case "普通":
    //                                talentCfg.quality = TalentQuality.ordinary;
    //                                break;
    //                            case "稀有":
    //                                talentCfg.quality = TalentQuality.rare;
    //                                break;
    //                            case "珍惜":
    //                                talentCfg.quality = TalentQuality.cherish;
    //                                break;
    //                            case "史诗":
    //                                talentCfg.quality = TalentQuality.epic;
    //                                break;
    //                            case "传说":
    //                                talentCfg.quality = TalentQuality.legend;
    //                                break;
    //                            case "神话":
    //                                talentCfg.quality = TalentQuality.mythology;
    //                                break;
    //                        }
    //                        break;
    //                    case "tips":
    //                        talentCfg.tips = node.InnerText;
    //                        break;
    //                }
    //            }
    //            TalentDic.Add(ID, talentCfg);
    //        }
    //    }
    //    GameCommon.Log("personCfg Init Done.");
    //}
    //public TalentCfg GetTalentCfgData(int id)
    //{
    //    TalentCfg talentCfg = null;
    //    if (TalentDic.TryGetValue(id, out talentCfg))
    //    {
    //        return talentCfg;
    //    }
    //    return null;
    //}
    //public int GetTalentCount()
    //{
    //    return TalentDic.Count;
    //}
    #endregion

    #region 加载商店物品配置
    private Dictionary<int, ItemCfg> ShopItemDic = new Dictionary<int, ItemCfg>();
    private void InitShopItemCfg(string path)
    {
        //GameCommon.Log("开始加载商店物品配置");
        TextAsset xml = GetTextAsset(path);
        if (!xml)
        {
            //GameCommon.Log("xml文件不存在");
            GameCommon.Log("xml file:" + path + "not exist", ComLogType.Error);
        }
        else
        {
            //GameCommon.Log("xml文件存在");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
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
                ItemCfg shopitemCfg = new ItemCfg()
                {
                    ID = ID,
                };
                foreach (XmlElement node in nodeList[i].ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "mName":
                            {
                                shopitemCfg.mName = node.InnerText;
                                break;
                            }
                        case "mInfo":
                            {
                                shopitemCfg.mInfo = node.InnerText;
                                break;
                            }
                        case "Price":
                            {
                                shopitemCfg.Price = int.Parse(node.InnerText);
                                break;
                            }
                        case "type":
                            {
                                shopitemCfg.type = (ItemType)Enum.Parse(typeof(ItemType), node.InnerText);
                                break;
                            }
                        case "quantity":
                            {
                                shopitemCfg.quantity = int.Parse(node.InnerText);
                                break;
                            }
                        case "Img":
                            {
                                shopitemCfg.mImg = node.InnerText;
                                break;
                            }
                    }

                }
                ShopItemDic.Add(ID, shopitemCfg);
            }
        }
    }
    public ItemCfg[] GetShopItemCfgData()
    {
        List<ItemCfg> items = new List<ItemCfg>();
        foreach (KeyValuePair<int, ItemCfg> i in ShopItemDic)
        {
            items.Add(i.Value);
        }
        return items.ToArray();
    }
    public ItemCfg GetShopItemCfg(int id)
    {
        ItemCfg shopItemCfg = null;
        if (ShopItemDic.TryGetValue(id, out shopItemCfg))
        {
            return shopItemCfg;
        }
        return null;
    }
    #endregion

    #region 随机名字
    private List<string> surnameLst = new List<string>();
    private List<string> manLst = new List<string>();
    private List<string> womanLst = new List<string>();
    private void InitRDNameCfg(string path)
    {
        //获取文本数据
        TextAsset xml = GetTextAsset(path);

        if (!xml)
        {
            GameCommon.Log("xmlm file:" + path + "not exist", ComLogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
            XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlElement ele = nodeList[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)//判断是否能够获取到ID
                {
                    continue;
                }
                //Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);
                foreach (XmlElement e in nodeList[i].ChildNodes)
                {
                    switch (e.Name)
                    {
                        case "surname":
                            surnameLst.Add(e.InnerText);
                            break;
                        case "man":
                            manLst.Add(e.InnerText);
                            break;
                        case "woman":
                            womanLst.Add(e.InnerText);
                            break;
                    }
                }
            }
        }
    }
    /// <summary>
    /// 生成随机名字 
    /// </summary>
    /// <param name="man"></param>
    /// <returns></returns>
    public string GetRDNameData(bool man = false)
    {
        // System.Random r = new System.Random();
        string rdName = surnameLst[ComTools.RDInt(0, surnameLst.Count - 1)];
        if (man)
        {
            rdName += manLst[ComTools.RDInt(0, manLst.Count - 1)];
        }
        else
        {
            rdName += womanLst[ComTools.RDInt(0, womanLst.Count - 1)];
        }

        return rdName;
    }
    #endregion

    #region 加载地图配置
    private Dictionary<int, MapCfg> MapDic = new Dictionary<int, MapCfg>();
    private void InitMapCfg(string Path)
    {
        TextAsset xml = GetTextAsset(Path);
        if (!xml)
        {
            GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
            XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlElement ele = nodeList[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
                {
                    continue;
                }
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
                MapCfg mapCfg = new MapCfg()
                {
                    ID = ID,
                };
                foreach (XmlElement node in nodeList[i].ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "mName":
                            mapCfg.mapName = node.InnerText;
                            break;
                        case "sceneName":
                            mapCfg.sceneName = node.InnerText;
                            break;
                        case "power":
                            mapCfg.power = int.Parse(node.InnerText);
                            break;
                        case "playerBornPos":
                            mapCfg.playerBornPos = GetVector3(node.InnerText);
                            break;
                        case "playerBornRote":
                            mapCfg.playerBornRote = GetVector3(node.InnerText);
                            break;
                        case "monsterLst":
                            mapCfg.monsterLst = node.InnerText;
                            break;
                        case "exp":
                            mapCfg.exp = int.Parse(node.InnerText);
                            break;
                        case "aura":
                            mapCfg.aura = int.Parse(node.InnerText);
                            break;
                        case "CameraFollowAndRotatePos":
                            mapCfg.CameraFollowAndRotatePos = GetVector3(node.InnerText);
                            break;
                        case "CameraFollowAndRotateRote":
                            mapCfg.CameraFollowAndRotateRote = GetVector3(node.InnerText);
                            break;
                        case "CameraUpAndDownPos":
                            mapCfg.CameraUpAndDownPos = GetVector3(node.InnerText);
                            break;
                        case "CameraUpAndDownRote":
                            mapCfg.CameraUpAndDownRote = GetVector3(node.InnerText);
                            break;
                        case "CameraZoomContainerPos":
                            mapCfg.CameraZoomContainerPos = GetVector3(node.InnerText);
                            break;
                        case "CameraZoomContainerRote":
                            mapCfg.CameraZoomContainerRote = GetVector3(node.InnerText);
                            break;
                    }
                }
                MapDic.Add(ID, mapCfg);
            }
        }
    }
    public MapCfg GetMapCfgData(int id)
    {
        MapCfg mapCfg = null;
        if (MapDic.TryGetValue(id, out mapCfg))
        {
            return mapCfg;
        }
        return null;
    }
    #endregion

    #region 加载任务配置
    private Dictionary<int, TaskCfg> TaskDic = new Dictionary<int, TaskCfg>();
    private void InitTaskCfg(string Path)
    {
        TextAsset xml = GetTextAsset(Path);
        if (!xml)
        {
            GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
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
                        case "npcID":
                            taskCfg.npcID = int.Parse(node.InnerText);
                            break;
                        case "dilogArr":
                            taskCfg.dilogArr = node.InnerText;
                            break;
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
        GameCommon.Log("InitTaskCfg Init Done.");
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
    #endregion
    #region 加载任务奖励配置
    private Dictionary<int, TaskRewardCfg> TaskRewardDic = new Dictionary<int, TaskRewardCfg>();
    private void InitTaskRewardCfg(string Path)
    {
        TextAsset xml = GetTextAsset(Path);
        if (!xml)
        {
            GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
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
    }

    public int GetTaskRewardCount()
    {
        return TaskRewardDic.Count;
    }
    public TaskRewardCfg GetTaskRewardCfgData(int id)
    {
        TaskRewardCfg taskRewardCfg = null;
        if (TaskRewardDic.TryGetValue(id, out taskRewardCfg))
        {
            return taskRewardCfg;
        }
        return null;
    }
    public TaskRewardCfg GetTaskRewardCfgListData(int id)
    {
        List<TaskRewardCfg> rewardItems = TaskRewardDic.Values.ToList();
        if (id >= rewardItems.Count)
        {
            return null;
        }
        return rewardItems[id];
    }
    #endregion
    #region 加载每日任务配置
    private Dictionary<int, TaskDailyCfg> TaskDailyDic = new Dictionary<int, TaskDailyCfg>();
    private void InitTaskDailyCfg(string Path)
    {
        TextAsset xml = GetTextAsset(Path);
        if (!xml)
        {
            GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
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
                        case "mTitle":
                            taskDailyCfg.mTitle = node.InnerText;
                            break;
                        case "mTask":
                            taskDailyCfg.mTask = node.InnerText;
                            break;
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
    }
    public int GetTaskDailyCount()
    {
        return TaskDailyDic.Count;
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
    public TaskDailyCfg GetTaskDailyCfgListData(int id)
    {
        List<TaskDailyCfg> rewardItems = TaskDailyDic.Values.ToList();
        if (id >= rewardItems.Count)
        {
            return null;
        }
        return rewardItems[id];
    }
    #endregion
    #region 加载抽奖配置
    private Dictionary<int, RaffleItemCfg> RaffleItemDic = new Dictionary<int, RaffleItemCfg>();
    private void InitRaffleItemCfg(string Path)
    {
        TextAsset xml = GetTextAsset(Path);
        if (!xml)
        {
            GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
            XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlElement ele = nodeList[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
                {
                    continue;
                }
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
                RaffleItemCfg raffleItemCfg = new RaffleItemCfg()
                {
                    ID = ID,
                };
                foreach (XmlElement node in nodeList[i].ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "mShopID":
                            raffleItemCfg.mShopID = int.Parse(node.InnerText);
                            break;
                        case "Count":
                            raffleItemCfg.Count = int.Parse(node.InnerText);
                            break;
                        case "probability":
                            raffleItemCfg.probability = int.Parse(node.InnerText);
                            break;
                    }
                }
                RaffleItemDic.Add(ID, raffleItemCfg);
            }
        }
    }
    /// <summary>
    /// 获取抽奖物品配置
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public ItemCfg GetRaffleItemCfgData(int id)
    {
        ItemCfg shopItemCfg = null;
        if (ShopItemDic.TryGetValue(id, out shopItemCfg))
        {
            return shopItemCfg;
        }
        return null;
    }
    public RaffleItemCfg GetRaffleItemCfgShopId(int ShopID)
    {
        foreach (KeyValuePair<int, RaffleItemCfg> i in RaffleItemDic)
        {
            if (i.Value.mShopID == ShopID)
            {
                return i.Value;
            }
        }
        return null;
    }
    /// <summary>
    /// 获取抽奖物品配置
    /// </summary>
    /// <returns></returns>
    public RaffleItemCfg[] GetRaffleItemCfgData()
    {
        List<RaffleItemCfg> items = new List<RaffleItemCfg>();
        foreach (KeyValuePair<int, RaffleItemCfg> i in RaffleItemDic)
        {
            items.Add(i.Value);
        }
        return items.ToArray();
    }
    #endregion
    #region 加载技能配置
    private Dictionary<int, SkillCfg> SkillDic = new Dictionary<int, SkillCfg>();
    private void InitSkillCfg(string Path)
    {
        TextAsset xml = GetTextAsset(Path);
        if (!xml)
        {
            GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
            XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlElement ele = nodeList[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
                {
                    continue;
                }
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
                SkillCfg skillCfg = new SkillCfg()
                {
                    ID = ID,
                    fxList = new List<int>(),
                    skillMoveList = new List<int>(),
                    skillActionLst = new List<int>(),
                    SkillDamageLst = new List<int>(),
                };
                foreach (XmlElement node in nodeList[i].ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "skillName":
                            skillCfg.mName = node.InnerText;
                            break;
                        case "cdTime":
                            skillCfg.cdTime = int.Parse(node.InnerText);
                            break;
                        case "skillTime":
                            skillCfg.skilltime = int.Parse(node.InnerText);
                            break;
                        case "aniAction":
                            skillCfg.aniAction = int.Parse(node.InnerText);
                            break;
                        case "fx":
                            string[] skFxArr = node.InnerText.Split('|');
                            for (int j = 0; j < skFxArr.Length; j++)
                            {
                                skillCfg.fxList.Add(int.Parse(skFxArr[j]));
                            }
                            break;
                        case "Type":
                            skillCfg.type = (SkillType)int.Parse(node.InnerText);
                            break;
                        case "isCombo":
                            skillCfg.isCombo = node.InnerText.Equals("1");
                            break;
                        case "isCollide":
                            skillCfg.isCollide = node.InnerText.Equals("1");
                            break;
                        case "isBreak":
                            skillCfg.isBreak = node.InnerText.Equals("1");
                            break;
                        case "dmgType":
                            if (node.InnerText.Equals("1"))
                            {
                                skillCfg.damageType = DamageType.AD;
                            }
                            else if (node.InnerText.Equals("2"))
                            {
                                skillCfg.damageType = DamageType.AP;
                            }
                            else
                            {
                                GameCommon.Log("dmgType ERROR");
                            }
                            break;
                        case "skillMoveLst":
                            string[] skMoveArr = node.InnerText.Split('|');
                            for (int j = 0; j < skMoveArr.Length; j++)
                            {
                                skillCfg.skillMoveList.Add(int.Parse(skMoveArr[j]));
                            }
                            break;
                        case "skillActionLst":
                            string[] skActionArr = node.InnerText.Split('|');
                            for (int j = 0; j < skActionArr.Length; j++)
                            {
                                skillCfg.skillActionLst.Add(int.Parse(skActionArr[j]));
                            }
                            break;
                        case "skillDamageLst":
                            string[] skDamageArr = node.InnerText.Split('|');
                            for (int j = 0; j < skDamageArr.Length; j++)
                            {
                                skillCfg.SkillDamageLst.Add(int.Parse(skDamageArr[j]));
                            }
                            break;
                    }
                }
                SkillDic.Add(ID, skillCfg);
            }
        }
        GameCommon.Log("skillCfg Init Done.");
    }
    public SkillCfg GetSkillCfgData(int id)
    {
        SkillCfg skillCfg = null;
        if (SkillDic.TryGetValue(id, out skillCfg))
        {
            return skillCfg;
        }
        return null;
    }
    #endregion

    //#region 加载技能位移配置
    //private Dictionary<int, SkillMoveCfg> SkillMoveDic = new Dictionary<int, SkillMoveCfg>();
    //private void InitSkillMoveCfg(string Path)
    //{
    //    TextAsset xml = GetTextAsset(Path);
    //    if (!xml)
    //    {
    //        GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
    //    }
    //    else
    //    {
    //        XmlDocument doc = new XmlDocument();
    //        doc.LoadXml(xml.text);//读取文本数据
    //        XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
    //        for (int i = 0; i < nodeList.Count; i++)
    //        {
    //            XmlElement ele = nodeList[i] as XmlElement;
    //            if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
    //            {
    //                continue;
    //            }
    //            int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
    //            SkillMoveCfg skillMoveCfg = new SkillMoveCfg()
    //            {
    //                ID = ID,
    //            };
    //            foreach (XmlElement node in nodeList[i].ChildNodes)
    //            {
    //                switch (node.Name)
    //                {
    //                    case "delayTime":
    //                        skillMoveCfg.delayTime = int.Parse(node.InnerText);
    //                        break;
    //                    case "moveTime":
    //                        skillMoveCfg.moveTime = int.Parse(node.InnerText);
    //                        break;
    //                    case "moveDis":
    //                        skillMoveCfg.moveDis = float.Parse(node.InnerText);
    //                        break;
    //                    case "moveDir":
    //                        skillMoveCfg.moveDir = float.Parse(node.InnerText);
    //                        break;
    //                }
    //            }
    //            SkillMoveDic.Add(ID, skillMoveCfg);
    //        }
    //    }
    //    GameCommon.Log("skillmoveCfg Init Done.");
    //}
    //public SkillMoveCfg GetSkillMoveCfgData(int id)
    //{
    //    SkillMoveCfg skillmoveCfg = null;
    //    if (SkillMoveDic.TryGetValue(id, out skillmoveCfg))
    //    {
    //        return skillmoveCfg;
    //    }
    //    return null;
    //}
    //#endregion

    #region 加载技能伤害配置
    private Dictionary<int, SkillActionCfg> SkillActionDic = new Dictionary<int, SkillActionCfg>();
    private void InitSkillActionCfg(string Path)
    {
        TextAsset xml = GetTextAsset(Path);
        if (!xml)
        {
            GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
            XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlElement ele = nodeList[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
                {
                    continue;
                }
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
                SkillActionCfg skillActionCfg = new SkillActionCfg()
                {
                    ID = ID,
                };
                foreach (XmlElement node in nodeList[i].ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "delayTime":
                            skillActionCfg.delayTime = int.Parse(node.InnerText);
                            break;
                        case "radius":
                            skillActionCfg.radius = float.Parse(node.InnerText);
                            break;
                        case "angle":
                            skillActionCfg.angle = float.Parse(node.InnerText);
                            break;
                    }
                }
                SkillActionDic.Add(ID, skillActionCfg);
            }
        }
        GameCommon.Log("skillActionCfg Init Done.");
    }
    public SkillActionCfg GetSkillActionCfgData(int id)
    {
        SkillActionCfg skillActionCfg = null;
        if (SkillActionDic.TryGetValue(id, out skillActionCfg))
        {
            return skillActionCfg;
        }
        return null;
    }
    #endregion

    #region 加载技能特效配置
    private Dictionary<int, SkillFxCfg> SkillFxDic = new Dictionary<int, SkillFxCfg>();
    private void InitSkillFxCfg(string Path)
    {
        TextAsset xml = GetTextAsset(Path);
        if (!xml)
        {
            GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
            XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlElement ele = nodeList[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
                {
                    continue;
                }
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
                SkillFxCfg skillFxCfg = new SkillFxCfg()
                {
                    ID = ID,
                };
                foreach (XmlElement node in nodeList[i].ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "name":
                            skillFxCfg.name = node.InnerText;
                            break;
                        case "delayTime":
                            skillFxCfg.delayTime = int.Parse(node.InnerText);
                            break;
                        case "ContineTime":
                            skillFxCfg.ContineTime = int.Parse(node.InnerText);
                            break;
                    }
                }
                SkillFxDic.Add(ID, skillFxCfg);
            }
        }
        GameCommon.Log("skillFxCfg Init Done.");
    }
    public SkillFxCfg GetSkillFxCfgData(int id)
    {
        SkillFxCfg skillFxCfg = null;
        if (SkillFxDic.TryGetValue(id, out skillFxCfg))
        {
            return skillFxCfg;
        }
        return null;
    }
    #endregion

    #region 加载敌人配置
    private Dictionary<int, EnemyCfg> EnemyDic = new Dictionary<int, EnemyCfg>();
    private void InitEnemyCfg(string Path)
    {
        TextAsset xml = GetTextAsset(Path);
        if (!xml)
        {
            GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
            XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlElement ele = nodeList[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
                {
                    continue;
                }
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
                EnemyCfg enemyCfg = new EnemyCfg()
                {
                    ID = ID,
                    skills = new List<int>(),
                };
                foreach (XmlElement node in nodeList[i].ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "name":
                            {
                                enemyCfg.name = node.InnerText;
                                break;
                            }
                        case "type":
                            {
                                enemyCfg.type = (EnemyType)Enum.Parse(typeof(EnemyType), node.InnerText);
                                break;
                            }
                        case "hp":
                            {
                                enemyCfg.hp = int.Parse(node.InnerText);
                                break;
                            }
                        case "ad":
                            {
                                enemyCfg.ad = int.Parse(node.InnerText);
                                break;
                            }
                        case "addef":
                            {
                                enemyCfg.addef = int.Parse(node.InnerText);
                                break;
                            }
                        case "dodge":
                            {
                                enemyCfg.dodge = int.Parse(node.InnerText);
                                break;
                            }
                        case "critical":
                            {
                                enemyCfg.critical = int.Parse(node.InnerText);
                                break;
                            }
                        case "chasedistance":
                            {
                                enemyCfg.chasedistance = int.Parse(node.InnerText);
                                break;
                            }
                        case "backdistance":
                            {
                                enemyCfg.backdistance = int.Parse(node.InnerText);
                                break;
                            }
                        case "skills":
                            {
                                //string[] skills = node.InnerText.Split('|');
                                //foreach(string skill in skills)
                                //{
                                //    skills.
                                //}
                                break;
                            }
                    }
                }
                EnemyDic.Add(ID, enemyCfg);
            }
        }
    }
    public EnemyCfg GetEnemyData(int id)
    {
        EnemyCfg enemycfg = null;
        if (EnemyDic.TryGetValue(id, out enemycfg))
        {
            return enemycfg;
        }
        return null;
    }
    #endregion

    #region 加载天赋配置
    private Dictionary<int, TalentCfg> TalentDic = new Dictionary<int, TalentCfg>();
    private void InitTalentCfg(string Path)
    {
        TextAsset xml = GetTextAsset(Path);
        if (!xml)
        {
            GameCommon.Log("xml file:" + Path + "not exist", ComLogType.Error);
        }
        else
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml.text);//读取文本数据
            XmlNodeList nodeList = doc.SelectSingleNode(("root")).ChildNodes;//选择根节点为root的节点
            for (int i = 0; i < nodeList.Count; i++)
            {
                XmlElement ele = nodeList[i] as XmlElement;
                if (ele.GetAttributeNode("ID") == null)//判断是否能够读取到ID
                {
                    continue;
                }
                int ID = Convert.ToInt32(ele.GetAttributeNode("ID").InnerText);//获取ID中的数据
                TalentCfg talentCfg = new TalentCfg()
                {
                    ID = ID,
                };
                foreach (XmlElement node in nodeList[i].ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "Name":
                            talentCfg.Name = node.InnerText;
                            break;
                        case "Info":
                            talentCfg.Info = node.InnerText;
                            break;
                        case "MaxLevel":
                            talentCfg.MaxLevel = int.Parse(node.InnerText);
                            break;
                        case "Attribute":
                            talentCfg.Attribute = node.InnerText;
                            break;
                        case "Value":
                            talentCfg.Value = float.Parse(node.InnerText);
                            break;
                        case "BackGround":
                            talentCfg.BackGround = node.InnerText;
                            break;

                    }
                }
                TalentDic.Add(ID, talentCfg);
            }
        }
    }
    public TalentCfg GetTalentCfgData(int id)
    {
        TalentCfg talentCfg = null;
        if (TalentDic.TryGetValue(id, out talentCfg))
        {
            return talentCfg;
        }
        return null;
    }
    public TalentCfg[] GetAllTalentCfgData()
    {
        List<TalentCfg> talents = new List<TalentCfg>();
        foreach (KeyValuePair<int, TalentCfg> i in TalentDic)
        {
            talents.Add(i.Value);
        }
        return talents.ToArray();
    }
    #endregion

    public Vector3 GetVector3(string InnerText)
    {
        string[] valArr = InnerText.Split(',');
        return new Vector3(float.Parse(valArr[0]), float.Parse(valArr[1]), float.Parse(valArr[2]));
    }
    /// <summary>
    /// 获取TextAsset
    /// </summary>
    /// <param name="Path"></param>
    /// <returns></returns>
    public TextAsset GetTextAsset(string path)
    {
#if DEBUG_ASSETBUNDLE
        path = Path.Combine(Application.persistentDataPath, PathDefine.ABDownload, path) + PathDefine.Xml;
        string xmlContent = File.ReadAllText(path);
        TextAsset textAsset = new TextAsset(xmlContent);

        return textAsset;
#elif UNITY_EDITOR
       
        TextAsset textAsset = UnityEditor.AssetDatabase.LoadAssetAtPath<TextAsset>(PathDefine.Download+ path + PathDefine.Xml);
        return textAsset;
#endif
    }
    //任务 //每日任务完成
    //副本（2-3个）（副本实现时间冻结效果）

    //背包(强化，装备) 强化和装备暂无

    //签到 //完成融入了每日任务了
    //成就
    //人物属性（展示打字机效果和经验滚动效果） //TODO需修改滚动效果
    //聊天
    //小地图
    //设置
    //
}
