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

public class TalentWnd : WindowRoot
{

    #region 公开字段
    public Button CloseBtn;
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
    /// 玩家数据
    /// </summary>
    private PlayerData playerData;
    /// <summary>
    /// 是否有选中的天赋
    /// </summary>
    private bool IsSelect = false;
    /// <summary>
    /// 选中天赋的ID
    /// </summary>
    private int SelectID = 0;
    #endregion

    #region 基类回调重写
    protected override void InitWnd()
    {
        base.InitWnd();
        cfgs = resSvc.GetAllTalentCfgData();
        Init();
    }
    protected override void ClearWnd()
    {
        base.ClearWnd();
        if (cfgs.Length > 0) cfgs = null;
        if (Levels.Length > 0) Levels = null;
        if (Talents.Length > 0) Talents = null;
        if (playerData != null) playerData = null;
    } 
    protected override void SetGameObject()
    {

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetKeyDown(KeyCode.F))
        {
            //netSvc.SendMsg
            Debug.Log("按下测试");
            GameMsg reqmsg = new GameMsg()
            {
                cmd = (int)CMD.ReqTalentUp,
                reqTalentUp = new ReqTalentUp
                {
                    TalentId = 50001,
                    NextLevel = 2,
                }
            };
            netSvc.SendMsg(reqmsg);
            //Debug.Log(netSvc.client.session.)
        }
    }
    //override 
    #endregion

    private void Init()
    {
        #region Data
        playerData = GameRoot.Instance.PlayerData;
        #endregion

        #region GAMEOBJECT
        Button[] allButtons = gameObject.GetComponentsInChildren<Button>();
        List<Button> talentsbutton = new List<Button>();
        List<Text> Texts = new List<Text>();
        foreach (Button button in allButtons)
        {
            if (button.name.Contains("Talent"))
            {
                talentsbutton.Add(button);
                Texts.Add(button.gameObject.GetComponentInChildren<Text>());
            }
        }
        this.Talents = talentsbutton.ToArray();
        this.Levels = Texts.ToArray();
        CloseBtn.onClick.AddListener(() => { SetWndState(false); });
        #endregion

        #region UI
        List<Talent> talents;
        talents = playerData.TalentsData;
        for (int i = 0; i < talents.Count; i++)
        {
            Levels[i].text = talents[i].Level + "/" + cfgs[i].MaxLevel;
        }
        #endregion
    }





}