/****************************************************
    文件：TalentInfoWnd.cs
	作者：Kong
    邮箱: 1246958782@qq.com
    日期：2024/10/16 12:4:45
	功能：天赋系统下的信息面板
*****************************************************/

using CommonNet;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;


public class TalentInfoWnd : MonoBehaviour
{
    #region 公开字段
    public Text BackGround;
    public Text Effect;
    public Text Name;
    public Text Level;
    public Text LevelUpBtnTxt;
    public Text AuraTxt;
    public Button LevelUpBtn;
    #endregion

    #region 私有字段
    int CurrLevel;  //当前选中天赋等级
    int SelectId;   //当前选中天赋的ID
    int maxLevel;  //当前选中天赋的最大等级
    TalentCfg SelectCfg; //选中天赋的配置信息
    PlayerData playerData;
    #endregion

    #region 对外开放的方法
    public void SetUi(string background, string effect, string name)
    {
        Name.text = name;
        BackGround.text = "<b>背景</b>:" + background;
        Effect.text = "<b>作用</b>:" + effect;
    }
    public void RefreshLevel(int level, int talentid, TalentCfg talentCfg)
    {
        SelectCfg = talentCfg;
        maxLevel = talentCfg.MaxLevel;
        Level.text = "当前等级:" + level + "/" + maxLevel;
        CurrLevel = level;
        SelectId = talentid;
        int needaura = level * 100;


        if (level == maxLevel)
        {
            LevelUpBtnTxt.text = "最大等级";
            LevelUpBtn.interactable = false;
        }
        else
        {
            if (needaura <= playerData.aura)
            { LevelUpBtn.interactable = true; AuraTxt.text = "<color=black>" + (level * 100).ToString() + "/" + (playerData.aura).ToString() + "</color>"; }
            else { LevelUpBtn.interactable = false; AuraTxt.text = "<color=red>" + (level * 100).ToString() + "/" + (playerData.aura).ToString() + "</color>"; }
            LevelUpBtnTxt.text = "升级";
        }
    }
    public void TweenShow(float time)
    {
        transform.localPosition = new Vector3(1212f, transform.localPosition.y);
        transform.DOLocalMoveX(731f, time);
    }
    public void TweenQuit(float time)
    {
        transform.DOLocalMoveX(1212f, time);
    }
    public void RefreshLevelUp()
    {
        RefreshLevel(CurrLevel, maxLevel, SelectCfg);
    }
    public void InitUI()
    {
        playerData = GameRoot.Instance.PlayerData;
        transform.localPosition = new Vector3(1212f, transform.localPosition.y);
        LevelUpBtn.onClick.RemoveAllListeners();
        LevelUpBtn.onClick.AddListener(SendMsg);
    }

    private void SendMsg()
    {
        playerData.aura -= CurrLevel * 100;
        CurrLevel++;
        GameMsg gameMsg = new GameMsg
        {
            cmd = (int)CMD.ReqTalentUp,
            reqTalentUp = new ReqTalentUp()
            { NextLevel = CurrLevel, TalentId = SelectId },
        };
        NetSvc.Instance.SendMsg(gameMsg);
    }
    #endregion
}