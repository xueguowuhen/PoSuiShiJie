/****************************************************
    文件：Listener
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-26 11:20:32
	功能：UI事件监听
*****************************************************/
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Listener : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IDragHandler
{
    public Action<object> onClick;
    public Action<PointerEventData> onClickDown;
    public Action<PointerEventData> onClickUp;
    public Action<PointerEventData> onDrag;
    public object args;
    public void OnDrag(PointerEventData eventData)
    {
        if (onDrag != null) onDrag(eventData);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (onClick != null) onClick(eventData);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (onClickDown != null) onClickDown(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (onClickUp != null) onClickUp(eventData);
    }
}