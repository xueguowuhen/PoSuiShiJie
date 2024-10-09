using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

public class PersonWnd : WindowRoot
{
    #region one
    public GameObject itemOne;
    public RectTransform PersonItem;
    public GameObject PersonOne;
    public GameObject Close;
    public Button BtnClose;
    public Button BtnPerson;
    #endregion
    #region two
    public GameObject itemTwo;
    public Image image;
    public Text Person;
    public GameObject Talent;
    public Text TalentOne;
    public GameObject arrtBute;
    public Text arrtButeOne;
    public GameObject Experience;
    public Slider slider;
    public Slider HpSlider;
    public Slider MagicSlider;
    #endregion
    protected override void InitWnd()
    {
        base.InitWnd();
        SetPersonBtn();
        Complete();
  
    }
    private void SetPersonBtn()
    {
        #region one
        itemOne = GetGameObject(PathDefine.ItemOne);
        PersonItem = GetGameObject(itemOne, PathDefine.PersonItem).GetComponent<RectTransform>();
        Close = GetGameObject(itemOne, PathDefine.BtnClose);
        BtnClose = Close.GetComponent<Button>();
        BtnClose.onClick.AddListener(OnCloseClick);
        #endregion

        #region two
        itemTwo = GetGameObject(PathDefine.ItemTwo);
        image = GetGameObject(itemTwo, PathDefine.Img).GetComponent<Image>();
        Person = GetGameObject(itemTwo, PathDefine.Person).GetComponent<Text>();
        Talent = GetGameObject(itemTwo,PathDefine.Talent);

        arrtBute = GetGameObject(itemTwo, PathDefine.attribute);

        Experience = GetGameObject(itemTwo, PathDefine.Experience);
        slider = GetGameObject(Experience, PathDefine.Slider).GetComponent<Slider>();
        HpSlider = GetGameObject(Experience, PathDefine.HpSlider).GetComponent<Slider>();
        MagicSlider = GetGameObject(Experience, PathDefine.MagicSlider).GetComponent<Slider>();
        #endregion
    }

    #region 完善信息
    /// <summary>
    /// 把按钮 属性什么的全部初始化出来
    /// </summary>
    private void Complete()
    {
        //人物按钮  天赋
        GameObject obj = null;
        GameObject obj2 = null;
        GameObject obj3 = null;
        for (int i = 0; i < 3; i++)
        {
            obj= resSvc.LoadPrefab("ResPerfer/PersonOne");
            obj.transform.SetParent(PersonItem.transform, false);
            personCfg temp = resSvc.GetPersonCfgData(20001+i);
            obj.GetComponent<Text>().text = temp.type;
            obj2= resSvc.LoadPrefab("ResPerfer/talentOne");
            List<int> talent = GameRoot.Instance.PlayerData.TalentID;
            obj2.transform.SetParent(Talent.transform, false); 
            obj2.GetComponent<Text>().text = resSvc.GetTalentCfgData(talent[i]).mName;   
        }
        BtnPerson = obj.GetComponent<Button>();
        BtnPerson.onClick.AddListener(OnPersonClick);  //三个按钮点击都一样
        //属性
        string[] temp1 = attr();
        for (int i = 0; i < 5; i++)
        {
            obj3= resSvc.LoadPrefab("Resperfer/Hp");
            SetText(obj3, temp1[i]);
            obj3.transform.SetParent(arrtBute.transform, false);
            
        }
    }
    private string[] attr()
    {
        GameCommon.Log(GameRoot.Instance.PlayerData.type.ToString());
        personCfg temp = resSvc.GetPersonCfgData(GameRoot.Instance.PlayerData.type);
        Debug.Log(temp == null);     //随着等级变化而变化
        List<string> temp1 = new List<string>();
        temp1.Add("Hp:"+temp.HP.ToString());
        temp1.Add("Mana:" + temp.Mana.ToString());
        temp1.Add("Power:" + temp.Power.ToString());
        temp1.Add("Ad:" + temp.ad.ToString());
        temp1.Add("Ap:" + temp.ap.ToString());
        return temp1.ToArray();

    }
   
    #endregion

    #region 按钮
    private void OnPersonClick()
    {
        //三个人物之间的跳转 然后跳转后信息可能变化可能不变化
    }
    private void OnCloseClick()
    {
        //关闭面板
        this.SetWndState(false);
        //清空面板
        for(int i = 0; i < PersonItem.childCount; i++)
        {
            Destroy(PersonItem.GetChild(i).gameObject);
        }
        
        for (int i = 0; i < Talent.GetComponent<RectTransform>().childCount; i++)
        {
            Destroy(Talent.GetComponent<RectTransform>().GetChild(i).gameObject);
        }
        for (int i = 0; i < arrtBute.GetComponent<RectTransform>().childCount; i++)
        {
            Destroy(arrtBute.GetComponent<RectTransform>().GetChild(i).gameObject);
        }
    }
    #endregion

}
