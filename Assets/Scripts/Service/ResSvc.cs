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
using System.Xml;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ResSvc : MonoBehaviour
{
    public static ResSvc instance = null;
    private int Playerprogress;
    private AssetBundle assetBundle;
    private AssetBundleManifest manifest;
    private Dictionary<string, AssetBundle> loadedAssetBundles = new Dictionary<string, AssetBundle>();
    //后期可改成模板生成工具
    public void InitSyc()
    {
        instance = this;
        InitCharaterCfg(PathDefine.personCfg);
        InitTalentCfg(PathDefine.TalentCfg);
        InitRDNameCfg(PathDefine.RdnameCfg);
        InitShopItemCfg(PathDefine.ShopItemCfg);
        InitMapCfg(PathDefine.mapCfg);
        InitTaskCfg(PathDefine.TaskCfg);
        InitSkillCfg(PathDefine.SkillCfg);
        InitSkillMoveCfg(PathDefine.SkillMoveCfg);
        InitSkillActionCfg(PathDefine.SkillActionCfg);
        InitSkillFxCfg(PathDefine.SkillFxCfg);
        InitEnemyCfg(PathDefine.EnemyCfg);
        GameCommon.Log("ResSvc Init....");
    }
    private void Update()
    {
    }
    private Dictionary<string, GameObject> GameObjectDic = new Dictionary<string, GameObject>();

    /// <summary>
    /// 异步加载
    /// </summary>
    public IEnumerator AsyncLoadScene(string sceneName, Action loaded, bool isAB = false)
    {
        GameRoot.Instance.loadingWnd.SetWndState();
        if (isAB)
        {
            string platform = LoginSys.instance.downingWnd.GetPlatform();
            string assset = Path.Combine(Application.persistentDataPath, platform);
            //加载主包
            if (assetBundle == null)
            {
                assetBundle = AssetBundle.LoadFromFile(Path.Combine(assset, platform));
                manifest = assetBundle.LoadAsset<AssetBundleManifest>(PathDefine.AssetBundleManifest);
            }
            //根据包名加载依赖
            string[] strings = manifest.GetAllDependencies(sceneName.ToLower());
            for (int i = 0; i < strings.Length; i++)
            {
                AssetBundle bundle;
                string path;
                if (LoginSys.instance.GettmpABInfo().ContainsKey(strings[i]))
                {
                    path = Path.Combine(Application.streamingAssetsPath, strings[i]);
                }
                else
                {
                    path = Path.Combine(assset, strings[i]);
                }
                AssetBundleCreateRequest Request = AssetBundle.LoadFromFileAsync(path);
                yield return Request;
                bundle = Request.assetBundle;
                loadedAssetBundles.Add(path, bundle);
            }
            // 异步加载场景包
            string bundlePath = Path.Combine(assset, sceneName.ToLower());
            AssetBundleCreateRequest bundleRequest = AssetBundle.LoadFromFileAsync(bundlePath);
            yield return bundleRequest;
            AssetBundle sceneBundle = bundleRequest.assetBundle;
            if (sceneBundle == null)
            {
                Debug.LogError($"Failed to load AssetBundle: {bundlePath}");
                yield break;
            }
        }
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = false;
        Playerprogress = 0;
        float val = 0;
        int progress;
        while (asyncOperation.progress < 0.9f)
        {
            val = asyncOperation.progress;
            progress = (int)(val * 100);
            //Text.text = progress.ToString();
            while (Playerprogress < progress)
            {
                ++Playerprogress;
                GameRoot.Instance.loadingWnd.SetProgress(Playerprogress);
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
        progress = 100;
        while (Playerprogress < progress)
        {
            ++Playerprogress;
            GameRoot.Instance.loadingWnd.SetProgress(Playerprogress);
            yield return new WaitForEndOfFrame();
        }
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = true;

        /// 等待场景加载完成
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        if (loaded != null)
        {
            loaded();
        }
        GameRoot.Instance.loadingWnd.SetWndState(false);
    }

    /// <summary>
    /// 加载该预制体
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    public GameObject LoadPrefab(string path, bool cache = false, bool instan = true)
    {
        GameObject prefab = null;
        if (!GameObjectDic.TryGetValue(path, out prefab))
        {
            prefab = Resources.Load<GameObject>(path);
            if (cache)
            {
                GameObjectDic.Add(path, prefab);
            }
        }
        GameObject gameObject = null;
        if (prefab != null)
        {
            if (instan)
            {
                gameObject = Instantiate(prefab);
            }
            else
            {
                return prefab;
            }
        }
        return gameObject;
    }
    /// <summary>
    /// 从ab包中加载资源
    /// </summary>
    /// <param name="path">包名</param>
    /// <param name="name">资源名</param>
    /// <param name="cache"></param>
    /// <returns></returns>
    public GameObject ABLoadPrefab(string path, string name, bool cache = false)
    {
        GameObject prefab = null;
        string platform = LoginSys.instance.downingWnd.GetPlatform();
        string assset = Path.Combine(Application.persistentDataPath, platform);
        AssetBundle bundle = null;
        //加载主包
        if (assetBundle == null)
        {
            assetBundle = AssetBundle.LoadFromFile(Path.Combine(assset, platform));
            manifest = assetBundle.LoadAsset<AssetBundleManifest>(PathDefine.AssetBundleManifest);
        }
        //根据报名加载依赖
        string[] strings = manifest.GetAllDependencies(path);
        for (int i = 0; i < strings.Length; i++)
        {
            string paths = Path.Combine(assset, strings[i]);
            AssetBundle asset = AssetBundle.LoadFromFile(paths);
            loadedAssetBundles.Add(paths, asset);
        }
        path = Path.Combine(assset, path);
        if (!GameObjectDic.TryGetValue(path, out prefab))
        {
            // 检查AssetBundle是否已经加载
            if (!loadedAssetBundles.TryGetValue(path, out bundle))
            {
                AssetBundle asset = AssetBundle.LoadFromFile(path);
                prefab = asset.LoadAsset(name, typeof(GameObject)) as GameObject;//指定加载类型
                loadedAssetBundles.Add(path, asset);
            }
            else
            {
                prefab = bundle.LoadAsset(name, typeof(GameObject)) as GameObject;
            }
            if (cache)
            {
                GameObjectDic.Add(path, prefab);
            }
        }
        GameObject gameObject = null;
        if (prefab != null)
        {
            gameObject = Instantiate(prefab);
        }
        return gameObject;
    }


    #region 加载人物配置
    private Dictionary<int, personCfg> personDic = new Dictionary<int, personCfg>();
    private void InitCharaterCfg(string Path)
    {
        TextAsset xml = Resources.Load<TextAsset>(Path);
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
    public personCfg GetPersonCfgData(int id)
    {
        personCfg personCfg = null;
        if (personDic.TryGetValue(id, out personCfg))
        {
            return personCfg;
        }
        return null;
    }
    public Sprite GetPersonCfgHard(int id)
    {
        personCfg personCfg = null;
        if (personDic.TryGetValue(id, out personCfg))
        {
            Debug.Log(PathDefine.ResHard + personCfg.Hard);
            Sprite sprite = Resources.Load<Sprite>(PathDefine.ResHard + personCfg.Hard);
            return sprite;
        }
        return null;
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
        TextAsset xml = Resources.Load<TextAsset>(path);
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
        TextAsset xml = Resources.Load<TextAsset>(path);

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
        TextAsset xml = Resources.Load<TextAsset>(Path);
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
                        case "mainCamPos":
                            mapCfg.mainCamPos = GetVector3(node.InnerText);
                            break;
                        case "mainCamRote":
                            mapCfg.mainCamRote = GetVector3(node.InnerText);
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
        TextAsset xml = Resources.Load<TextAsset>(Path);
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
        GameCommon.Log("personCfg Init Done.");
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

    #region 加载技能配置
    private Dictionary<int, SkillCfg> SkillDic = new Dictionary<int, SkillCfg>();
    private void InitSkillCfg(string Path)
    {
        TextAsset xml = Resources.Load<TextAsset>(Path);
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

    #region 加载技能位移配置
    private Dictionary<int, SkillMoveCfg> SkillMoveDic = new Dictionary<int, SkillMoveCfg>();
    private void InitSkillMoveCfg(string Path)
    {
        TextAsset xml = Resources.Load<TextAsset>(Path);
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
                SkillMoveCfg skillMoveCfg = new SkillMoveCfg()
                {
                    ID = ID,
                };
                foreach (XmlElement node in nodeList[i].ChildNodes)
                {
                    switch (node.Name)
                    {
                        case "delayTime":
                            skillMoveCfg.delayTime = int.Parse(node.InnerText);
                            break;
                        case "moveTime":
                            skillMoveCfg.moveTime = int.Parse(node.InnerText);
                            break;
                        case "moveDis":
                            skillMoveCfg.moveDis = float.Parse(node.InnerText);
                            break;
                        case "moveDir":
                            skillMoveCfg.moveDir = float.Parse(node.InnerText);
                            break;
                    }
                }
                SkillMoveDic.Add(ID, skillMoveCfg);
            }
        }
        GameCommon.Log("skillmoveCfg Init Done.");
    }
    public SkillMoveCfg GetSkillMoveCfgData(int id)
    {
        SkillMoveCfg skillmoveCfg = null;
        if (SkillMoveDic.TryGetValue(id, out skillmoveCfg))
        {
            return skillmoveCfg;
        }
        return null;
    }
    #endregion

    #region 加载技能伤害配置
    private Dictionary<int, SkillActionCfg> SkillActionDic = new Dictionary<int, SkillActionCfg>();
    private void InitSkillActionCfg(string Path)
    {
        TextAsset xml = Resources.Load<TextAsset>(Path);
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
        TextAsset xml = Resources.Load<TextAsset>(Path);
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
        TextAsset xml = Resources.Load<TextAsset>(Path);
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
        TextAsset xml = Resources.Load<TextAsset>(Path);
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
    //任务
    //副本（2-3个）（副本实现时间冻结效果）

    //背包(强化，装备) 强化和装备暂无

    //签到
    //成就
    //人物属性（展示打字机效果和经验滚动效果）
    //聊天
    //小地图
    //设置
    //
}
