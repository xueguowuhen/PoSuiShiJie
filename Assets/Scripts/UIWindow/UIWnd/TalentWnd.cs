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
    private TalentCfg[] cfgs;
    private Text[] Levels;
    private Button[] Talents;
    private PlayerData playerData;
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
            if (button.name.Contains("talent"))
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