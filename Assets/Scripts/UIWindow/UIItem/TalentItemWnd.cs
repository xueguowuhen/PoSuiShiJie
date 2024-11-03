/****************************************************
    文件：TalentItemWnd.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/23 17:17:52
	功能：Nothing
*****************************************************/

using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TalentItemWnd : WindowRoot, IPointerEnterHandler,IPointerExitHandler
{
    public string TalentTips;
    public GameObject TalentItem;
    public TalentQuality talentQuality;
    public Text TalentBtn;
    /// <summary>
    /// 鼠标进入时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetText(TalentBtn, TalentTips);
        TalentItem.transform.position =this.transform.position;
        SetActive(TalentItem);
    }
    /// <summary>
    /// 鼠标退出时
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        SetActive(TalentItem, false);
    }
}