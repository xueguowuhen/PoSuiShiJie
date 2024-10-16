/****************************************************
    文件：TalentInfoWnd.cs
	作者：Kong
    邮箱: 1246958782@qq.com
    日期：2024/10/16 12:4:45
	功能：天赋系统下的信息面板
*****************************************************/

using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using CommonNet;


public class TalentInfoWnd : MonoBehaviour
{
    #region 公开字段
    public Text BackGround;
    public Text Effect;
    public Text Name;
    public Text Level;
    public Text LevelUpBtnTxt;
    public Button LevelUpBtn;
    #endregion

    #region 私有字段
    int CurrLevel;
    int SelectId;
    int maxLevel;
    #endregion

    #region 对外开放的方法
    public void SetUi(string background, string effect, string name)
    {
        Name.text = name;
        BackGround.text = "<b>背景</b>:" + background;
        Effect.text = "<b>作用</b>:" + effect;
    }
    public void RefreshLevel(int level, int maxlevel, int talentid)
    {
        Level.text = "当前等级:" + level + "/" + maxlevel;
        CurrLevel = level;
        SelectId = talentid;
        maxLevel = maxlevel;
        if (level == maxlevel)
        {
            LevelUpBtnTxt.text = "最大等级";
            LevelUpBtn.interactable = false;
        }
        else
        {
            LevelUpBtnTxt.text = "升级";
            LevelUpBtn.interactable = true;
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
        RefreshLevel(CurrLevel, maxLevel, SelectId);
    }
    public void InitUI()
    {
        transform.localPosition = new Vector3(1212f, transform.localPosition.y);
        LevelUpBtn.onClick.RemoveAllListeners();
        LevelUpBtn.onClick.AddListener(() => { CurrLevel++; GameMsg gameMsg = new GameMsg { cmd = (int)CMD.ReqTalentUp, reqTalentUp = new ReqTalentUp() { NextLevel = CurrLevel, TalentId = SelectId }, }; Debug.Log(CurrLevel); NetSvc.instance.SendMsg(gameMsg); });
    }
    #endregion
}