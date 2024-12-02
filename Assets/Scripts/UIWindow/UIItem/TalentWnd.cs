/****************************************************
    文件：TalentWnd.cs
	作者：Kong
    邮箱: 1246958782@qq.com
    日期：2024/10/12 19:15:55
	功能：天赋窗口
*****************************************************/

using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using CommonNet;
using DG.Tweening;

public class TalentWnd : WindowRoot
{

    #region 公开字段与属性
    public Button CloseBtn;
    public Button InfoBtn;
    public RectTransform TalentRect;
    public TalentInfoWnd talentInfoWnd;

    public bool IsSelect
    {
        get { return m_IsSelect; }
        private set { if (value == true && m_IsSelect == false) { RefreshInfoUI(); m_IsSelect = true; } }
    }

    //public bool NeedUpdate
    #endregion

    #region 私有字段
    /// <summary>
    /// 所有天赋数据 不含当前等级数据
    /// </summary>
    private TalentCfg[] cfgs;
    /// <summary>
    /// 所有天赋等级
    /// </summary>
    private Text[] Levels;
    /// <summary>
    /// 所有的天赋按钮
    /// </summary>
    private Button[] Talents;
    /// <summary>
    /// 所有天赋的选择框
    /// </summary>
    private Image[] Select;
    /// <summary>
    /// 玩家数据
    /// </summary>
    private PlayerData playerData;
    /// <summary>
    /// 详情按钮的背景
    /// </summary>
    private Image[] InfobtnBg;
    /// <summary>
    /// 是否有选中的天赋
    /// </summary>
    private bool m_IsSelect;
    /// <summary>
    /// 选中天赋的ID
    /// </summary>
    private int SelectTalentID;

    private Talent CurrTalentData;

    //======logic==========

    /// <summary>
    /// 是否有显示天赋信息
    /// </summary>
    private bool IsShowInfo = false;

    /// <summary>
    /// 记录当前的天赋
    /// </summary>
    private List<int> CurrTalentId;
    /// <summary>
    /// 记录初始的天赋
    /// </summary>
    private List<int> RecodeTalentId;
    #endregion

    #region 基类回调重写
    protected override void InitWnd()
    {
        base.InitWnd();
        Init();
    }
    protected override void ClearWnd()
    {
        base.ClearWnd();
        CloseBtn.onClick.RemoveAllListeners();
        InfoBtn.onClick.RemoveAllListeners();
        if (cfgs.Length > 0) cfgs = null;
        if (Levels.Length > 0) Levels = null;
        if (Talents.Length > 0) Talents = null;
        if (Select.Length > 0) Select = null;
        if (InfobtnBg.Length > 0) InfobtnBg = null;
        if (playerData != null) playerData = null;
    }
    protected override void SetGameObject()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.F))
        {

        }
    }
    #endregion

    private void Init()
    {
        #region Data
        playerData = GameRoot.Instance.PlayerData;
        cfgs = resSvc.GetAllTalentCfgData();
        CurrTalentId = playerData.TalentID;
        RecodeTalentId = new List<int> { CurrTalentId[0], CurrTalentId[1] };
        #endregion

        #region GAMEOBJECT
        Button[] allButtons = gameObject.GetComponentsInChildren<Button>();
        List<Button> talentsbutton = new List<Button>();
        List<Text> Texts = new List<Text>();
        List<Image> Images = new List<Image>();
        foreach (Button button in allButtons)
        {
            if (button.name.Contains("Talent"))
            {
                talentsbutton.Add(button);
                Texts.Add(button.gameObject.GetComponentInChildren<Text>());
                Images.Add(button.gameObject.transform.GetChild(1).GetComponentInChildren<Image>());
            }
        }
        InfobtnBg = new Image[2];
        InfobtnBg[0] = InfoBtn.transform.GetChild(0).GetComponent<Image>();
        InfobtnBg[1] = InfoBtn.transform.GetChild(1).GetComponent<Image>();
        this.Talents = talentsbutton.ToArray();
        this.Levels = Texts.ToArray();
        this.Select = Images.ToArray();
        TalentRect.anchorMax = new Vector2(0.5f, 0.5f);
        #endregion

        #region Event
        for (int i = 0; i < Talents.Length; i++)
        {
            int j = i;
            talentsbutton[j].onClick.RemoveAllListeners();
            talentsbutton[j].onClick.AddListener(() => { BtnTanlentClick(cfgs[j].ID); });
        }
        CloseBtn.onClick.AddListener(BtnCloseHandler);
        InfoBtn.onClick.AddListener(() => { BtnShowInfoHandler(); });
        #endregion

        #region UI

        RefreshInfoUI(false);
        RefreshLevelUI();
        RefreshSelectUI();
        talentInfoWnd.InitUI();
        #endregion

        #region logic
        m_IsSelect = false;
        IsShowInfo = false;
        #endregion
    }

    #region 回调事件
    private void BtnTanlentClick(int TalentId)
    {
        SelectTalentID = TalentId;
        foreach (Talent j in playerData.TalentsData)
        {
            if (j.TalentID == SelectTalentID)
            {
                CurrTalentData = j;
            }
        }
        IsSelect = true;
        foreach (int i in playerData.TalentID)
        {
            if (i == SelectTalentID)
            {

                break;
            }
            else
            {
                ChangeTalent(SelectTalentID);

                break;
            }
        }
    }
    private void BtnShowInfoHandler(float time = 0.3f)
    {
        if (!IsShowInfo)
        {
            //Debug.Log("展开");
            TalentRect.DOAnchorMax(new Vector2(0.4f, 0.5f), time);
            TalentCfg currcfg = resSvc.GetTalentCfgData(SelectTalentID);
            talentInfoWnd.SetUi(currcfg.BackGround, currcfg.Info, currcfg.Name);
            talentInfoWnd.RefreshLevel(CurrTalentData.Level,CurrTalentData.TalentID,currcfg);
            talentInfoWnd.TweenShow(time);
            IsShowInfo = true;
        }
        else
        {
            //Debug.Log("退出");
            TalentRect.DOAnchorMax(new Vector2(0.5f, 0.5f), time);
            talentInfoWnd.TweenQuit(time);
            IsShowInfo = false;
        }
    }
    private void ChangeTalent(int TalentID)
    {
        int temp = TalentID - 50010;
        if (temp > 0) //切换下一行的天赋
        {
            playerData.TalentID[1] = TalentID;
            CurrTalentId[1] = TalentID;
        }
        else //切换上一行的天赋
        {
            playerData.TalentID[0] = TalentID;
            CurrTalentId[0] = TalentID;
        }
        RefreshSelectUI();
        if (IsShowInfo)
        {
            TalentCfg currcfg = resSvc.GetTalentCfgData(TalentID);
            talentInfoWnd.TweenQuit(0.15f);
            talentInfoWnd.SetUi(currcfg.BackGround, currcfg.Info, currcfg.Name);
            talentInfoWnd.RefreshLevel(CurrTalentData.Level,CurrTalentData.TalentID,currcfg);
            talentInfoWnd.TweenShow(0.15f);
        }

    }
    private void BtnCloseHandler()
    {
        bool temp = (!(CurrTalentId[0] == RecodeTalentId[0] && CurrTalentId[1] == RecodeTalentId[1]));

        if (!(CurrTalentId[0] == RecodeTalentId[0] && CurrTalentId[1] == RecodeTalentId[1]) )
        {
            GameMsg gameMsg = new GameMsg()
            {
                cmd = (int)CMD.ReqChangeTalent,
                reqChangeTalent = new ReqChangeTalent()
                {
                    CurrTalents = CurrTalentId,
                },
            };
            netSvc.SendMsg(gameMsg);
        }
        MainCitySys.instance.OpenPerson();
        SetWndState(false);
    }
    #endregion

    #region UI控制相关
    private void RefreshSelectUI()
    {
        foreach (Image m in Select)
        {
            m.gameObject.SetActive(false);
        }
        List<int> SelectTanlentId = playerData.TalentID;
        foreach (int i in SelectTanlentId)
        {
            for (int j = 0; j < cfgs.Length; j++)
            {
                if (i == cfgs[j].ID)
                {
                    Select[j].gameObject.SetActive(true);
                    break;
                }
            }
        }
    }
    private void RefreshLevelUI()
    {
        List<Talent> talents;
        talents = playerData.TalentsData;
        for (int i = 0; i < talents.Count; i++)
        {
            Levels[i].text = talents[i].Level + "/" + cfgs[i].MaxLevel;
        }
    }
    private void RefreshInfoUI(bool show = true)
    {
        if (show)
        {
            foreach (Image i in InfobtnBg)
            {
                i.color = new Color(1, 1, 1, 1);
            }
            InfoBtn.interactable = true;
        }
        else
        {
            foreach (Image i in InfobtnBg)
            {
                i.color = new Color(1, 1, 1, 30 / 255f);
            }
            InfoBtn.interactable = false;
        }

    }

    public void RefreshLevelUp()
    {
        RefreshLevelUI();
        talentInfoWnd.RefreshLevelUp();
    }

    #endregion
}