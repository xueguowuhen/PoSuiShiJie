/****************************************************
    文件：WindowAnimation
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-09-30 20:43:53
	功能：Nothing
*****************************************************/
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class WindowAnimation : Singleton<WindowAnimation>
{
    private Dictionary<WindowType, WindowRoot> m_dicWindow = new Dictionary<WindowType, WindowRoot>();
    #region StartShowWindow 开始打开窗口
    /// <summary>
    /// 开始打开窗口
    /// </summary>
    /// <param name="window"></param>
    /// <param name="isOpen">是否打开</param>
    public void StartShowWindow(UIAnimation window, bool isOpen)
    {
        switch (window.windowType)
        {
            case WindowType.None:
                //ShowNormal(window, isOpen);
                break;
            case WindowType.FromCenter:
                ShowCenterToBig(window, isOpen);
                break;
            case WindowType.FromTop:
                ShowFromDir(window, 0, isOpen);
                break;
            case WindowType.FromBottom:
                ShowFromDir(window, 1, isOpen);
                break;
            case WindowType.FromLeft:
                ShowFromDir(window, 2, isOpen);
                break;
            case WindowType.FromRight:
                ShowFromDir(window, 3, isOpen);
                break;

        }
    }
    #endregion
    #region ShowCenterToBig 中间变大
    /// <summary>
    /// 中间变大
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="isOpen"></param>
    private void ShowCenterToBig(UIAnimation windowBase, bool isOpen)
    {
        windowBase.gameObject.SetActive(true);
        windowBase.transform.localScale = Vector3.zero;
        //创建一个动画，但不进行播放
        Tweener ts = windowBase.transform.DOScale(Vector3.one, windowBase.duration)
            .SetAutoKill(false).SetEase(windowBase.UIAnimationCurve).Pause().OnRewind(() =>
            {
                //DestroyWindow(windowBase);
            });
        if (isOpen)
        {
            windowBase.transform.DOPlayForward();//开启状态为正向播放
        }
        else
        {
            windowBase.transform.DOPlayBackwards();//反向播放
        }
    }

    /// <summary>
    /// 放大方向
    /// </summary>
    /// <param name="windowBase"></param>
    /// <param name="dirType"></param>
    /// <param name="isOpen"></param>
    private void ShowFromDir(UIAnimation windowBase, int dirType, bool isOpen)
    {
        windowBase.gameObject.SetActive(true);
        Vector3 from = Vector3.zero;
        switch (dirType)
        {
            case 0:
                from = new Vector3(0, 1000, 0);
                break;
            case 1:
                from = new Vector3(0, -1000, 0);
                break;
            case 2:
                from = new Vector3(1800, 0, 0);
                break;
            case 3:
                from = new Vector3(-1800, 0, 0);
                break;
        }
        windowBase.transform.localPosition = from;
        windowBase.transform.DOLocalMove(Vector3.zero, windowBase.duration)
            .SetAutoKill(false).Pause().OnRewind(() =>
            {
                //DestroyWindow(windowBase);
            });
        if (isOpen)
        {
            windowBase.transform.DOPlayForward();//开启状态为正向播放
        }
        else
        {
            windowBase.transform.DOPlayBackwards();//反向播放
        }
    }
    #endregion



    #region DestroyWindow 销毁窗口
    /// <summary>
    /// 销毁窗口
    /// </summary>
    /// <param name="obj"></param>
    private void DestroyWindow(UIAnimation windowBase)
    {
        GameObject.Destroy(windowBase.gameObject);
    }
    #endregion
}
