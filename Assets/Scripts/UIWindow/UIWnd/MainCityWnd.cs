/****************************************************
    文件：MainCityWnd.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/25 17:54:2
	功能：主城窗体
*****************************************************/

using CommonNet;
using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MainCityWnd : WindowRoot
{
    #region Head
    //public Image HpProImg;
    //public Text HpText;
    //public Image ManaProImg;
    //public Text ManaText;
    public Image Headimg;
    public Button Head1;
    //public Image Head2img;
    //public Button Head2;
    //public Image Head3img;
    //public Button Head3;
    #endregion
    #region function
    //public Text TaskText;
    public Button StartGameBtn;//开始游戏
    public Button DailyTaskButton;//任务
    //public GameObject TaskPro;
    //public Button SignBtn;
    public Button TalentBtn;//天赋
    public Button ShopBtn;//商店
    //public Button ArenaBtn;
    //public Button MenuBtn;
    public Button BagBtn;//背包
    public Button FriendsBtn;//聊天
    public Button ChatBtn;//聊天

    public Text auraText;//星晶
    public Button auraBtn;
    public Text ruviaText;//云晶
    public Button ruviaBtn;
    public Text crystalText;//彩晶
    public Button crystalBtn;
    //public Button TaskBtn;
    //public Button SkillBtn;
    public Button SettingsBtn;//设置
    //public Animation menuAni;
    #endregion
    #region PlayerController
    //public Image imgTouch;
    //public Image imgDirBg;
    //public Image imgDirPoint;
    #endregion
    private float pointDis;
    private TaskCfg taskCfg;
    protected override void InitWnd()
    {
        base.InitWnd();
        RefreshUI();
    }
    protected override void SetGameObject()
    {
        GameRoot.Instance.SetScreenSpaceCamera();//设置界面
        #region Head
        //HpProImg = GetImg(PathDefine.HpProImg);
        //HpText = GetText(PathDefine.HpText);
        //ManaProImg = GetImg(PathDefine.ManaProImg);
        //ManaText = GetText(PathDefine.ManaText);
        //Headimg = GetImg(PathDefine.Headimg);
        //Head1 = GetButton(PathDefine.Head1);
        AddListener(StartGameBtn, ClickStartGame);
        AddListener(Head1, ClickHead1);
        //Head2img = GetImg(PathDefine.Head2img);
        //Head2 = GetButton(PathDefine.Head2);
        //AddListener(Head2, ClickHead2);
        //Head3img = GetImg(PathDefine.Head3);
        //Head3 = GetButton(PathDefine.Head3);
        //AddListener(Head3, ClickHead3);
        #endregion
        #region function
        //TaskText = GetText(PathDefine.Task);
        //TaskButton = GetButton(PathDefine.Task);
        AddListener(DailyTaskButton, ClickDailyTask);
        //TaskPro =GetGameObject(PathDefine.TaskPro);
        //SignBtn = GetButton(PathDefine.SignBtn);
        //AchivementBtn = GetButton(PathDefine.AchivementBtn);
        //ShopBtn = GetButton(PathDefine.ShopBtn);
        AddListener(FriendsBtn, ClickFriendsBtn);
        AddListener(ShopBtn, ClickShop);
        AddListener(TalentBtn, ClickTalent);
        //ArenaBtn = GetButton(PathDefine.ArenaBtn);
        //AddListener(ArenaBtn, ClickArena);
        //MenuBtn = GetButton(PathDefine.MenuBtn);
        //AddListener(MenuBtn, ClickMenu);
        //BagBtn = GetButton(PathDefine.BagBtn);
        AddListener(BagBtn, ClickBag);
        //ChatBtn = GetButton(PathDefine.ChatBtn);
        //TaskBtn = GetButton(PathDefine.TaskBtn);
        //AddListener(TaskBtn, ClickTask);
        //SkillBtn = GetButton(PathDefine.SkillBtn);
        //SettingsBtn = GetButton(PathDefine.SettingsBtn);
        //menuAni = GetAnimation(PathDefine.menuAni);
        #endregion
        #region PlayerController
        //imgTouch =GetImg(PathDefine.imgTouch);
        //imgDirBg =GetImg(PathDefine.imgDirBg);
        //imgDirPoint =GetImg(PathDefine.imgDirPoint);
        #endregion
    }

    public void RefreshUI()
    {
        PlayerData playerData = GameRoot.Instance.PlayerData;
        auraText.text = playerData.aura.ToString();
        ruviaText.text = playerData.ruvia.ToString();
        crystalText.text = playerData.crystal.ToString();
        int headid = playerData.type;
        Headimg.sprite = resSvc.GetPersonCfgHard(headid);

        //defaultPos=imgDirBg.transform.position;
        //pointDis = Screen.height * 1.0f / Constants.ScreenStandardHeight * Constants.ScreenOPDis;
        //获取任务ID
        //taskCfg = resSvc.GetTaskCfgData(playerData.Taskid);
        //HpText.text = playerData.Hp + "/" + playerData.Hpmax;
        //HpProImg.fillAmount =(float)playerData.Hp /playerData.Hpmax;
        //ManaText.text = playerData.Mana + "/" + playerData.ManaMax;
        //ManaProImg.fillAmount = (float)playerData.Mana / playerData.ManaMax;
        //if (taskCfg != null)
        //{//设置对应的头像

        //}

    }
    /// <summary>
    /// 点击主人物头像
    /// </summary>
    public void ClickHead1()
    {
        //加载面板
        //resSvc.LoadPrefab(PathDefine.PersonWnd);
        MainCitySys.instance.ClickPerson();

    }
    /// <summary>
    /// 点击第二个人物头像
    /// </summary>
    public void ClickHead2()
    {

    }
    /// <summary>
    /// 点击第三个人物头像
    /// </summary>
    public void ClickHead3()
    {

    }
    /// <summary>
    /// 点击商店
    /// </summary>
    public void ClickShop()
    {
        MainCitySys.instance.EnterShop();
    }
    /// <summary>
    /// 点击背包
    /// </summary>
    public void ClickBag()
    {
        MainCitySys.instance.EnterBagWnd();
    }
    /// <summary>
    /// 点击好友系统
    /// </summary>
    private void ClickFriendsBtn()
    {
        MainCitySys.instance.EnterFriendWnd();
    }

    /// <summary>
    /// 聊天
    /// </summary>
    public void ClickChat()
    {

    }
    /// <summary>
    /// 天赋设置
    /// </summary>
    private void ClickTalent()
    {
        MainCitySys.instance.EnterTalentWnd();
    }

    private void ClickDailyTask()
    {
        MainCitySys.instance.EnterDailyTaskWnd();
    }

    /// <summary>
    /// 点击开始游戏界面
    /// </summary>
    private void ClickStartGame()
    {

    }
    //public void ClickArena()
    //{
    //    MainCitySys.instance.ClickArena();
    //}
    //private bool TaskState=true;
    /// <summary>
    /// 点击任务
    /// </summary>
    public void ClickTask()
    {
        //TaskState = !TaskState;
        //SetActive(TaskPro, TaskState);
    }
    //点击自动寻路按钮
    //public void ClickTaskBtn()
    //{
    //    if (taskCfg != null)
    //    {

    //        MainCitySys.instance.RunTask(taskCfg);//进行自动寻路
    //    }
    //    else
    //    {
    //        GameRoot.AddTips("没有更多任务");
    //    }
    //}
    //private bool menuState = true;
    //public void ClickMenu()
    //{
    //    AnimationClip clip = null;
    //    menuState=!menuState;
    //    if (menuState)
    //    {
    //        clip = menuAni.GetClip("MenuEnter");
    //    }
    //    else
    //    {
    //        clip = menuAni.GetClip("MenuExit");
    //    }
    //    menuAni.Play(clip.name);
    //}
    //#region 摇杆触控事件
    //private Vector2 startPos = Vector2.zero;
    //private Vector2 defaultPos=Vector2.zero;
    //private bool IsRun=false;
    //public void RegisterTouchEvts()
    //{
    //    OnClickDown(imgTouch.gameObject, (PointerEventData evt) =>
    //    {
    //        startPos = evt.position;
    //        SetActive(imgDirPoint);
    //        imgDirBg.transform.position = evt.position;
    //    });
    //    OnClickUp(imgTouch.gameObject, (PointerEventData evt) =>
    //    {
    //        imgDirBg.transform.position = defaultPos;
    //        SetActive(imgDirPoint, false);//中心点不可见
    //        imgDirPoint.transform.localPosition = Vector2.zero;
    //        IsRun = false;
    //        MainCitySys.instance.SetMoveDir(Vector2.zero);
    //    });

    //    OnDrag(imgTouch.gameObject, (PointerEventData evt) =>
    //    {
    //        Vector2 dir=evt.position - startPos;
    //        float len = dir.magnitude;//获取向量长度
    //        if (len>pointDis)
    //        {
    //            Vector2 clampDir = Vector2.ClampMagnitude(dir, pointDis);//限制移动的最大距离
    //            imgDirPoint.transform.position = startPos + clampDir;
    //            IsRun = true;
    //        }
    //        else
    //        {
    //            imgDirPoint.transform.position = evt.position;
    //            IsRun=false;
    //        }
    //        MainCitySys.instance.SetMoveDir(dir.normalized,IsRun);
    //    });
    //}
    //#endregion
}