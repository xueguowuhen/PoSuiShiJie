/****************************************************
    文件：ItemEntityHP
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-18 14:46:59
	功能：生物显示
*****************************************************/
using UnityEngine;
using UnityEngine.UI;

public class ItemEntityHP : MonoBehaviour
{
    #region UI Define
    public Animation criticalAni;
    public Text textCritical;
    public Animation DodgeAni;
    public Text textDodge;
    public Animation hpAni;
    public Text textHP;
    #endregion
    public Transform rootTrans;
    public RectTransform rect;

    private void Update()
    {
        float scaleRate = 1.0f * Constants.ScreenStandardHeight / Screen.height;
        //世界坐标转屏幕坐标
        Vector3 screenPos = Camera.main.WorldToScreenPoint(rootTrans.position);

        rect.anchoredPosition = scaleRate * screenPos;
    }
    public void SetItemInfo(Transform trans)
    {
        rootTrans = trans;
        rect = transform.GetComponent<RectTransform>();
        Transform txtCritical = transform.Find(PathDefine.txtCritical);
        criticalAni = txtCritical.GetComponent<Animation>();
        textCritical = txtCritical.GetComponent<Text>();
        Transform txtDodge = transform.Find(PathDefine.txtDodge);
        DodgeAni = txtDodge.GetComponent<Animation>();
        textDodge = txtDodge.GetComponent<Text>();
        Transform txtHp = transform.Find(PathDefine.txtHp);
        hpAni = txtHp.GetComponent<Animation>();
        textHP = txtHp.GetComponent<Text>();

    }
    public void SetCritical(int critical)
    {
        criticalAni.Stop();
        textCritical.text = "暴击" + critical;
        criticalAni.Play();
    }
    public void SetDodge()
    {
        DodgeAni.Stop();
        textDodge.text = "闪避";
        DodgeAni.Play();
    }
    public void SetHurt(int hurt)
    {
        hpAni.Stop();
        textHP.text = "-" + hurt;
        hpAni.Play();
    }
}