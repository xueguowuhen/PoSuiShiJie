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
    public Button ManageBtn;//法师
    private Image ManageImg;//法师头像
    public Button FighterBtn;//战士
    private Image FighterImg;//战士头像

    #endregion
    #region TwoWnd
    public Button CreateBtn;
    public InputField GameName;
    public Button NameRandBtn;
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
        AddListener(btn_close, ClickClose);
        #region OneWnd
        AddListener(ManageBtn, ClickManage);
        ManageImg = GetImg(ManageBtn.gameObject);
        AddListener(FighterBtn, ClickFighter);
        FighterImg = GetImg(FighterBtn.gameObject);
        resSvc.GetPersonCfgHard(Constants.ManageID, (Texture2D texture) =>
        {
            ManageImg.overrideSprite = texture.CreateSprite();
        });
        resSvc.GetPersonCfgHard(Constants.VirtualID, (Texture2D texture) =>
       {
           FighterImg.overrideSprite = texture.CreateSprite();
       });

        #endregion
        #region TwoWnd
        AddListener(CreateBtn, ClickGamePlay);

        AddListener(NameRandBtn, SetRDNameData);

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
    }

    /// <summary>
    /// 设置人物按钮状态
    /// </summary>
    public void SetPersonState(Button button)
    {
        ManageBtn.interactable = true;
        FighterBtn.interactable = true;
        button.interactable = false;
    }
    /// <summary>
    /// 设置信息按钮状态
    /// </summary>
    /// <param name="button"></param>
    public void SetInfoState(Button button = null, GameObject gameObject = null)
    {
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
            GameMsg msg = new GameMsg()
            {
                cmd = (int)CMD.ReqCreateGame,
                reqCreateGame = new ReqCreateGame
                {
                    id = PersonID,
                    name = GameName.text,
                }
            };
            netSvc.SendMsg(msg);
            //发送创建角色信息
        }
    }
    //关闭返回登录界面
    public void ClickClose()
    {
        LoginSys.instance.EnterLogin();
        SetWndState(false);
    }
}