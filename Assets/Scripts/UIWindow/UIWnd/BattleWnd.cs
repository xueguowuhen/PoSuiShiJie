/****************************************************
    文件：BattleWnd.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/5/9 16:47:25
	功能：战斗界面窗体
*****************************************************/

using CommonNet;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BattleWnd : WindowRoot
{
    #region Head
    public Image HpProImg;
    public Text HpText;
    public Image ManaProImg;
    public Text ManaText;
    public Image Headimg;
    public Button Head1;
    public Image Head2img;
    public Button Head2;
    public Image Head3img;
    public Button Head3;
    #endregion
    #region Function
    public Button btnNormal;
    public Button btnSkill;
    public Image SkillCD1;
    public Text SkillTxt1;
    public Button btnSkil2;
    public Image SkillCD2;
    public Text SkillTxt2;
    public Button btnSkil3;
    public Image SkillCD3;
    public Text SkillTxt3;

    public Button BtnQuitBattle;
    #endregion
    #region PlayerController
    public Image imgTouch;
    public Image imgDirBg;
    public Image imgDirPoint;
    #endregion
    private float pointDis;
    private TaskCfg taskCfg;
    protected override void InitWnd()
    {
        base.InitWnd();
        SetGameObject();
        RegisterTouchEvts();
        RefreshUI();
    }
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            ClickNormal();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            ClickSkill1();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            ClickSkill2();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ClickSkill3();
        }
        float dalta = Time.deltaTime;
        if (isSk1CD)
        {
            sk1FillCount += dalta;
            if (sk1FillCount >= sk1CDTime)//记录时间大于冷却时间则结束冷却
            {
                isSk1CD = false;
                SetActive(SkillCD1, false);
                sk1FillCount = 0;
            }
            else
            {
                SkillCD1.fillAmount = 1 - sk1FillCount / sk1CDTime;
            }
            sk1NumCount += dalta;
            if (sk1NumCount >= 1)
            {
                sk1NumCount -= 1;
                sk1Num -= 1;
                SetText(SkillTxt1, sk1Num);
            }
        }
        if (isSk2CD)
        {
            sk2FillCount += dalta;
            if (sk2FillCount >= sk2CDTime)//记录时间大于冷却时间则结束冷却
            {
                isSk2CD = false;
                SetActive(SkillCD2, false);
                sk2FillCount = 0;
            }
            else
            {
                SkillCD2.fillAmount = 1 - sk2FillCount / sk2CDTime;
            }
            sk2NumCount += dalta;
            if (sk2NumCount >= 1)
            {
                sk2NumCount -= 1;
                sk2Num -= 1;
                SetText(SkillTxt2, sk2Num);
            }
        }
        if (isSk3CD)
        {
            sk3FillCount += dalta;
            if (sk3FillCount >= sk3CDTime)//记录时间大于冷却时间则结束冷却
            {
                isSk3CD = false;
                SetActive(SkillCD3, false);
                sk3FillCount = 0;
            }
            else
            {
                SkillCD3.fillAmount = 1 - sk3FillCount / sk3CDTime;
            }
            sk3NumCount += dalta;
            if (sk3NumCount >= 1)
            {
                sk3NumCount -= 1;
                sk3Num -= 1;
                SetText(SkillTxt3, sk3Num);
            }
        }
    }
    protected override void SetGameObject()
    {
        #region Head
        HpProImg = GetImg(PathDefine.HpProImg);
        HpText = GetText(PathDefine.HpText);
        ManaProImg = GetImg(PathDefine.ManaProImg);
        ManaText = GetText(PathDefine.ManaText);
        Headimg = GetImg(PathDefine.Headimg);
        Head1 = GetButton(PathDefine.Head1);
        AddListener(Head1, ClickHead1);
        Head2img = GetImg(PathDefine.Head2img);
        Head2 = GetButton(PathDefine.Head2);
        AddListener(Head2, ClickHead2);
        Head3img = GetImg(PathDefine.Head3);
        Head3 = GetButton(PathDefine.Head3);
        AddListener(Head3, ClickHead3);
        #endregion
        #region Function
        btnNormal = GetButton(PathDefine.btnNormal);
        AddListener(btnNormal, ClickNormal);
        btnSkill = GetButton(PathDefine.btnSkill1);
        SkillCD1 = GetImg(PathDefine.SkillCD1);
        SkillTxt1 = GetText(PathDefine.SkillTxt1);

        SkillCD2 = GetImg(PathDefine.SkillCD2);
        btnSkil2 = GetButton(PathDefine.btnSkill2);
        AddListener(btnSkil2, ClickSkill2);
        SkillTxt2 = GetText(PathDefine.SkillTxt2);
        AddListener(btnSkill, ClickSkill1);

        btnSkil3 = GetButton(PathDefine.btnSkill3);
        AddListener(btnSkil3, ClickSkill3);
        SkillCD3 = GetImg(PathDefine.SkillCD3);
        SkillTxt3 = GetText(PathDefine.SkillTxt3);

        BtnQuitBattle = GetButton(PathDefine.BtnQuitBattle);
        AddListener(BtnQuitBattle, ClickQuitBattle);
        #endregion
        #region PlayerController
        imgTouch = GetImg(PathDefine.imgTouch);
        imgDirBg = GetImg(PathDefine.imgDirBg);
        imgDirPoint = GetImg(PathDefine.imgDirPoint);
        #endregion
    }
    public void RefreshUI()
    {
        PlayerData playerData = GameRoot.Instance.PlayerData;
        defaultPos = imgDirBg.transform.position;
        pointDis = Screen.height * 1.0f / Constants.ScreenStandardHeight * Constants.ScreenOPDis;
        HpText.text = playerData.Hp + "/" + playerData.Hpmax;
        HpProImg.fillAmount = (float)playerData.Hp / playerData.Hpmax;
        ManaText.text = playerData.Mana + "/" + playerData.ManaMax;
        ManaProImg.fillAmount = (float)playerData.Mana / playerData.ManaMax;
        List<int> SkillList = resSvc.GetPersonCfgData(playerData.type).SkillList;
        for(int i = 0; i < SkillList.Count; i++)
        {
            switch(i)
            {
                case 0:
                    sk1CDTime = resSvc.GetSkillCfgData(SkillList[i]).cdTime/1000.0f;
                    break;
                case 1:
                    sk2CDTime = resSvc.GetSkillCfgData(SkillList[i]).cdTime / 1000.0f;
                    break;
                case 2:
                    sk3CDTime = resSvc.GetSkillCfgData(SkillList[i]).cdTime / 1000.0f;
                    break;
            }
        }
        //sk1CDTime =resSvc.
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
    private Vector2 startPos = Vector2.zero;
    private Vector2 defaultPos = Vector2.zero;
    private bool IsRun = false;
    public void ClickNormal()
    {
        BattleSys.instance.ReleaseNormal();
    }
    private bool isSk1CD = false;//记录处于CD状态
    private float sk1CDTime;//CD时间
    private int sk1Num;
    private float sk1FillCount = 0;//记录填充
    private float sk1NumCount = 0;//记录时间
    public void ClickSkill1()
    {
        if (isSk1CD == false && GetCanRlsSkill())
        {
            BattleSys.instance.ReleaseSkill1();
            isSk1CD = true;//设置技能冷却
            SetActive(SkillCD1);
            SkillCD1.fillAmount = 1;
            sk1Num = (int)sk1CDTime;
            SetText(SkillTxt1, sk1Num);
        }
    }
    private bool isSk2CD = false;//记录处于CD状态
    private float sk2CDTime;//CD时间
    private int sk2Num;
    private float sk2FillCount = 0;//记录填充
    private float sk2NumCount = 0;//记录时间
    public void ClickSkill2()
    {
        if (isSk2CD == false && GetCanRlsSkill())
        {
            BattleSys.instance.ReleaseSkill2();
            isSk2CD = true;//设置技能冷却
            SetActive(SkillCD2);
            SkillCD2.fillAmount = 1;
            sk2Num = (int)sk2CDTime;
            SetText(SkillTxt2, sk2Num);
        }
    }
    private bool isSk3CD = false;//记录处于CD状态
    private float sk3CDTime;//CD时间
    private int sk3Num;
    private float sk3FillCount = 0;//记录填充
    private float sk3NumCount = 0;//记录时间
    public void ClickSkill3()
    {
        if (isSk3CD == false && GetCanRlsSkill())
        {
            BattleSys.instance.ReleaseSkill3();
            isSk3CD = true;//设置技能冷却
            SetActive(SkillCD3);
            SkillCD3.fillAmount = 1;
            sk3Num = (int)sk3CDTime;
            SetText(SkillTxt3, sk3Num);
        }
    }
    public void RegisterTouchEvts()
    {
        OnClickDown(imgTouch.gameObject, (PointerEventData evt) =>
        {
            startPos = evt.position;
            SetActive(imgDirPoint);
            imgDirBg.transform.position = evt.position;
        });
        OnClickUp(imgTouch.gameObject, (PointerEventData evt) =>
        {
            imgDirBg.transform.position = defaultPos;
            SetActive(imgDirPoint, false);//中心点不可见
            imgDirPoint.transform.localPosition = Vector2.zero;
            IsRun = false;
            BattleSys.instance.SetSelfPlayerMoveDir(Vector2.zero);
        });

        OnDrag(imgTouch.gameObject, (PointerEventData evt) =>
        {
            Vector2 dir = evt.position - startPos;
            float len = dir.magnitude;//获取向量长度
            if (len > pointDis)
            {
                Vector2 clampDir = Vector2.ClampMagnitude(dir, pointDis);//限制移动的最大距离
                imgDirPoint.transform.position = startPos + clampDir;
                IsRun = true;
            }
            else
            {
                imgDirPoint.transform.position = evt.position;
                IsRun = false;
            }
            BattleSys.instance.SetSelfPlayerMoveDir(dir.normalized, IsRun);
        });
    }
    public bool GetCanRlsSkill()
    {
        return BattleSys.instance.CanRlsSkill();
    }
    public void ClickQuitBattle()
    {
        SetWndState(false);
        MainCitySys.instance.mainCityWnd.SetWndState(true);
        EntityBase player = BattleSys.instance.GetBattleMgr().entitySelfPlayer;
        player.canControl = true;
        player.Idle();
    }
}