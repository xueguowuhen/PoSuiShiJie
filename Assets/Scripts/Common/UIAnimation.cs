/****************************************************
    文件：UIAnimation
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-09-30 20:53:54
	功能：Nothing
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class UIAnimation:MonoBehaviour
{
    public WindowType windowType;
    public float duration;
    /// <summary>
    /// UI动画曲线
    /// </summary>
    public AnimationCurve UIAnimationCurve = new AnimationCurve(new Keyframe(0f, 0f, 0f, 1f), new Keyframe(1f, 1f, 1f, 0f));
}
