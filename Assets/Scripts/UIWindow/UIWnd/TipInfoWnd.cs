using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TipInfoWnd : WindowRoot, IPointerEnterHandler, IPointerExitHandler
{
    private string m_content = "";
    public GameObject m_TipItem;
    public Text m_Tip = null;

    public string Content
    {
        set { m_content = value; }
    }
    public Text Tip
    {
        set { m_Tip = value; }
    }
    public GameObject TipItem
    {
        set
        {
            m_TipItem = value;
        }
    }

    /// <summary>
    /// ������ʱ
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        SetText(m_Tip, m_content);
        m_TipItem.transform.position = this.transform.position;
        SetActive(m_TipItem);
    }
    /// <summary>
    /// ����˳�ʱ
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        SetActive(m_TipItem, false);
    }
    //public 
}
