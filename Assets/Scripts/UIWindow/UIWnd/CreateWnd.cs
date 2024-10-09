/****************************************************
    文件：CreateWnd.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/21 22:28:36
	功能：创建角色窗体
*****************************************************/

using CommonNet;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class CreateWnd : WindowRoot
{
    public Button btn_close;
    private GameObject Bg;
    #region OneWnd
    //public GameObject OneWnd;
    public Button ManageBtn;//法师
    private Image ManageImg;//法师头像
    public Button FighterBtn;//战士
    private Image FighterImg;//战士头像
    //public Button GodBtn;
    //public GameObject Presons;
    //public GameObject BaseMain;
    //public GameObject TwoMain;
    //public GameObject IntroRank;
    //public Button BaseBtn;
    //public Button AttributeBtn;
    //public Button NextBtn;
    //public Text NameText;
    //public Text IntroText;
    //public Text fightText;
    #endregion
    #region TwoWnd
    //public GameObject TwoWnd;
    public Button CreateBtn;
    public InputField GameName;
    public Button NameRandBtn;
    //public GameObject TalentList;
    //public Button TalentBtn;
    //public Button LastBtn;
    #endregion
    private int PersonID;
    private List<int> TalentIdList = new List<int>();
    protected override void InitWnd()
    {
        base.InitWnd();
        SetGameObject();
        ClickManage();
        //ClearUIWnd();
        //ClickLast();
        //ClickBase();
        //ClickManage();
        //SetRDNameData();
    }
    protected override void SetGameObject()
    {
       // Bg = GetGameObject(PathDefine.Bg);
        //btn_close = GetButton(PathDefine.CreateWndbtn_close);
        AddListener(btn_close, ClickClose);
        //GameRoot.Instance.SetScreenSpaceCamera();
        #region OneWnd
        //OneWnd = GetGameObject(PathDefine.OneWnd);
        #region 选择人物按钮
        //ManageBtn = GetButton(OneWnd, PathDefine.ManageBtn);
        AddListener(ManageBtn, ClickManage);
        ManageImg = GetImg(ManageBtn.gameObject);
        //FighterBtn = GetButton(OneWnd, PathDefine.VirtualBtn);
        AddListener(FighterBtn, ClickFighter);
        FighterImg = GetImg(FighterBtn.gameObject);
        ManageImg.sprite = resSvc.GetPersonCfgHard(Constants.ManageID);
        FighterImg.sprite = resSvc.GetPersonCfgHard(Constants.VirtualID);
        //GodBtn = GetButton(OneWnd, PathDefine.GodBtn);
        //AddListener(GodBtn, ClickGod);
        #endregion
        //BaseBtn = GetButton(OneWnd, PathDefine.BaseBtn);
        //AddListener(BaseBtn, ClickBase);
        //AttributeBtn = GetButton(OneWnd, PathDefine.AttributeBtn);
        //AddListener(AttributeBtn, ClickAttribute);
        //NextBtn = GetButton(OneWnd, PathDefine.NextBtn);
        //AddListener(NextBtn, ClickNext);
        //Presons = GetGameObject(OneWnd, PathDefine.Presons);
        //BaseMain = GetGameObject(OneWnd, PathDefine.BaseMain);
        //NameText = GetText(BaseMain, PathDefine.NameText);
        //IntroText = GetText(BaseMain, PathDefine.IntroText);
        //fightText = GetText(BaseMain, PathDefine.fightText);

        //TwoMain = GetGameObject(OneWnd, PathDefine.TwoMain);
        //IntroRank = GetGameObject(TwoMain, PathDefine.IntroRank);
        #endregion
        #region TwoWnd
        //TwoWnd = GetGameObject(PathDefine.TwoWnd);
        //CreateBtn = GetButton(TwoWnd, PathDefine.CreateBtn);
        AddListener(CreateBtn, ClickGamePlay);
        //GameName = SetTranFind(TwoWnd, PathDefine.GameName).GetComponent<InputField>();
        //NameRandBtn = GetButton(TwoWnd, PathDefine.NameRandBtn);
        AddListener(NameRandBtn, SetRDNameData); 
        //TalentList = GetGameObject(TwoWnd, PathDefine.TalentList);
        //TalentBtn = GetButton(TwoWnd, PathDefine.TalentBtn);
        //AddListener(TalentBtn, ClickTalent);
        //LastBtn = GetButton(TwoWnd, PathDefine.LastBtn);
        //AddListener(LastBtn, ClickLast);
        #endregion
    }
    /// <summary>
    /// 清除UI窗体
    /// </summary>
    public void ClearUIWnd()
    {
        //for (int i = 0; i < transform.childCount; i++)
        //{
        //    transform.GetChild(i).gameObject.SetActive(false);
        //}
        //SetInfoState(null, BaseMain);

    }
    /// <summary>
    /// 点击法师
    /// </summary>
    public void ClickManage()
    {
        SetPersonState(ManageBtn);
        PersonID = Constants.ManageID;
        //SetPerson();
    }
    /// <summary>
    /// 点击战士
    /// </summary>
    public void ClickFighter()
    {
        SetPersonState(FighterBtn);
        PersonID = Constants.VirtualID;
        //SetPerson();
    }
    /// <summary>
    /// 点击神道者
    /// </summary>
    //public void ClickGod()
    //{
    //    SetPersonState(GodBtn);
    //    PersonID = Constants.GodID;
    //    SetPerson();
    //}
    /// <summary>
    /// 点击信息
    /// </summary>
    //public void ClickBase()
    //{
    //    SetInfoState(BaseBtn, BaseMain);
    //}
    /// <summary>
    /// 查看属性
    /// </summary>
    //public void ClickAttribute()
    //{
    //    SetInfoState(AttributeBtn, TwoMain);

    //}

    /// <summary>
    /// 设置人物按钮状态
    /// </summary>
    public void SetPersonState(Button button)
    {
        ManageBtn.interactable = true;
        FighterBtn.interactable = true;
        //GodBtn.interactable = true;
        button.interactable = false;
    }
    /// <summary>
    /// 设置信息按钮状态
    /// </summary>
    /// <param name="button"></param>
    public void SetInfoState(Button button = null, GameObject gameObject = null)
    {
        //BaseBtn.interactable = true;
        //AttributeBtn.interactable = true;
        //SetActive(BaseMain, false);
        //SetActive(TwoMain, false);
        if (button != null)
        {
            button.interactable = false;
        }
        if (gameObject != null)
        {

            SetActive(gameObject);
        }
    }
    /// <summary>
    /// 配置人物
    /// </summary>
    //private void SetPerson()
    //{
    //    personCfg personCfg = resSvc.GetPersonCfgData(PersonID);
    //    for (int i = 0; i < Presons.transform.childCount; i++)
    //    {
    //        Destroy(Presons.transform.GetChild(i).gameObject);

    //    }
    //    GameObject prefab = resSvc.ABLoadPrefab(personCfg.Prefab, PathDefine.Main + personCfg.Prefab);
    //    prefab.transform.SetParent(Presons.transform, false);//设置人物预制体
    //    SetInfo(personCfg);
    //    SetAttr(personCfg);

    //}
    /// <summary>
    /// 配置信息
    /// </summary>
    /// <param name="personCfg"></param>
    //private void SetInfo(personCfg personCfg)
    //{
    //    SetText(NameText, personCfg.type);
    //    SetText(IntroText, personCfg.Intro);
    //    SetText(fightText, personCfg.fight);
    //}
    /// <summary>
    /// 配置属性
    /// </summary>
    //private void SetAttr(personCfg personCfg)
    //{//设置属性预制体

    //    int count = 0;
    //    for (int i = 0; i < IntroRank.transform.childCount; i++)
    //    {
    //        count++;
    //        Text RankItem = IntroRank.transform.GetChild(i).GetComponent<Text>();
    //        SetActive(RankItem);
    //        SetPreText(RankItem, i, personCfg);
    //    }
    //    while (count < personCfg.PreText)
    //    {
    //        GameObject prefab = resSvc.LoadPrefab(PathDefine.ResIntroItem);
    //        prefab.transform.SetParent(IntroRank.transform, false);//设置人物预制体
    //        Text text = GetText(prefab.transform);
    //        SetPreText(text, count, personCfg);
    //        count++;
    //    }
    //}
    //private void SetPreText(Text RankItem, int i, personCfg personCfg)
    //{
    //    switch (i)
    //    {
    //        case 0:
    //            SetText(RankItem, "名称:" + personCfg.type);
    //            break;
    //        case 1:
    //            SetText(RankItem, "体魄:" + personCfg.HP);
    //            break;
    //        case 2:
    //            SetText(RankItem, "灵力:" + personCfg.Mana);
    //            break;
    //        case 3:
    //            SetText(RankItem, "体力:" + personCfg.Power);
    //            break;
    //        case 4:
    //            SetText(RankItem, "灵石:" + personCfg.aura);
    //            break;
    //        case 5:
    //            SetText(RankItem, "真气:" + personCfg.ad);
    //            break;
    //        case 6:
    //            SetText(RankItem, "神识:" + personCfg.ap);
    //            break;
    //        case 7:
    //            SetText(RankItem, "真气抗性:" + personCfg.addef);
    //            break;
    //        case 8:
    //            SetText(RankItem, "神识抗性:" + personCfg.adpdef);
    //            break;
    //        case 9:
    //            SetText(RankItem, "身法:" + personCfg.dodge);
    //            break;
    //        case 10:
    //            SetText(RankItem, "悟性:" + personCfg.practice);
    //            break;
    //        case 11:
    //            SetText(RankItem, "真我:" + personCfg.critical);
    //            break;
    //        default:
    //            Destroy(RankItem);
    //            break;
    //    }
    //}

    //private void ClickNext()
    //{
    //    SetMuchWnd(false);
    //}
    //private void ClickLast()
    //{
    //    SetMuchWnd(true);
    //}
    /// <summary>
    /// 设置页面显示
    /// </summary>
    /// <param name="isShow"></param>
    //private void SetMuchWnd(bool isShow)
    //{
    //    SetActive(Bg);
    //    SetActive(OneWnd, isShow);
    //    SetActive(TwoWnd, !isShow);
    //    GameCommon.Log(GameName.text);
    //    if (TwoWnd.activeSelf && TalentIdList.Count == 0)
    //    {
    //        SetTalenItem();
    //    }
    //}
    //private void ClickTalent()
    //{
    //    SetTalenItem();
    //}
    /// <summary>
    /// 随机不同状态
    /// </summary>
    //public void SetTalenItem()
    //{
    //    for (int i = 0; i < TalentList.transform.childCount; i++)
    //    {
    //        Destroy(TalentList.transform.GetChild(i).gameObject);
    //    }
    //    TalentIdList.Clear();
    //    List<int> numbers = GetRandomNumber(30001, 30010, 9);
    //    for (int i = 0; i < numbers.Count; i++)
    //    {
    //        GameObject prefab = resSvc.LoadPrefab(PathDefine.ResTalentItem);//加载天赋按钮
    //        prefab.transform.SetParent(TalentList.transform, false);
    //        TalentCfg talentCfg = resSvc.GetTalentCfgData(numbers[i]);//获取天赋配置
    //        #region 设置介绍面板
    //        Transform TalentItemimg = SetTranFind(TwoWnd, PathDefine.TalentItemimg);
    //        if (TalentItemimg == null)//查找是否存在介绍面板
    //        {
    //            TalentItemimg = resSvc.LoadPrefab(PathDefine.ResTalentimg).transform;
    //            TalentItemimg.SetParent(TwoWnd.transform, false);
    //            TalentItemimg.name = PathDefine.TalentItemimg;
    //            SetActive(TalentItemimg, false);
    //        }
    //        TalentItemWnd ItemWnd = prefab.GetComponent<TalentItemWnd>();
    //        ItemWnd.TalentItem = TalentItemimg.gameObject;
    //        ItemWnd.TalentBtn = GetText(TalentItemimg.gameObject, PathDefine.Talentxt);
    //        ItemWnd.TalentTips = talentCfg.tips;
    //        ItemWnd.talentQuality = talentCfg.quality;
    //        #endregion
    //        AddListener(GetToggle(prefab), (bool isOn) =>
    //        {
    //            SetTalent(GetToggle(prefab), talentCfg);
    //        });
    //        SetText(SetTranFind(prefab, PathDefine.TalentItemText), Constants.Color(talentCfg.mName, SetColor(talentCfg.quality)));
    //    }
    //}
    /// <summary>
    /// 获取颜色
    /// </summary>
    /// <param name="talentQuality"></param>
    /// <returns></returns>
    //public TxtColor SetColor(TalentQuality talentQuality)
    //{
    //    switch (talentQuality)
    //    {
    //        case TalentQuality.garbage:
    //            return TxtColor.Grey;
    //        case TalentQuality.ordinary:
    //            return TxtColor.Green;
    //        case TalentQuality.rare:
    //            return TxtColor.Pink;
    //        case TalentQuality.epic:
    //            return TxtColor.Purple;
    //        case TalentQuality.legend:
    //            return TxtColor.Yellow;
    //        case TalentQuality.mythology:
    //            return TxtColor.Red;
    //        case TalentQuality.cherish:
    //            return TxtColor.Blue;
    //        default:
    //            return TxtColor.White;
    //    }
    //}

    /// <summary>
    /// 进行名字随机
    /// </summary>
    public void SetRDNameData()
    {
        GameName.text = resSvc.GetRDNameData(false);
    }
    /// <summary>
    /// 进行游戏登录
    /// </summary>
    public void ClickGamePlay()
    {
        string pattern = @"^[a-zA-Z0-9\u4e00-\u9fa5]+$"; // 匹配英文大小写、数字和中文的正则表达式
        if (!Regex.IsMatch(GameName.text, pattern))
        {
            GameRoot.AddTips("用户名违法，请重新输入");
            SetRDNameData();//重新随机名字
        }
        else
        {

            //GameRoot.AddTips("注册成功");
            GameMsg msg = new GameMsg()
            {
                cmd = (int)CMD.ReqCreateGame,
                reqCreateGame =new ReqCreateGame
                {
                    id = PersonID,
                    name= GameName.text,
                    TalentIDList = TalentIdList,
                }
            };
            netSvc.SendMsg(msg);
        }
    }
    //public void SetTalent(Toggle toggle, TalentCfg talentCfg)
    //{
    //    if (toggle.isOn)//被选中时
    //    {
    //        if (TalentIdList.Count >= Constants.MaxSelect)
    //        {
    //            GameRoot.AddTips("已选择最大3个天赋");
    //            toggle.isOn = false;
    //        }
    //        else
    //        {
    //            SetText(SetTranFind(toggle.gameObject, PathDefine.TalentItemText), Constants.Color(talentCfg.mName, TxtColor.White));
    //            TalentIdList.Add(talentCfg.ID);
    //        }
    //    }
    //    else
    //    {
    //        TalentIdList.Remove(talentCfg.ID);
    //        SetText(SetTranFind(toggle.gameObject, PathDefine.TalentItemText), Constants.Color(talentCfg.mName, SetColor(talentCfg.quality)));
    //    }
    //}
    /// <summary>
    /// 随机不同天赋
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    //public List<int> GetRandomNumber(int min, int max, int count)
    //{
    //    if (max - min + 1 < count)
    //    {
    //        GameCommon.Log("随机函数错误");
    //        return null;
    //    }
    //    List<int> list = new List<int>();
    //    System.Random random = new System.Random();
    //    while (list.Count < count)
    //    {
    //        int number = random.Next(min, max + 1);
    //        if (!list.Contains(number))
    //        {
    //            list.Add(number);
    //        }
    //    }
    //    return list;

    //}
    //关闭返回登录界面
    public void ClickClose()
    {
        LoginSys.instance.EnterLogin();
        SetWndState(false);
    }
}