/****************************************************
    文件：GameObjectExt
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-01 0:10:21
	功能：Nothing
*****************************************************/
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public static class GameObjectExt
{
    /// <summary>
    /// 获取或创建组件
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="str"></param>
    /// <returns></returns>
    public static T GetOrAddComponent<T>(this GameObject str) where T : MonoBehaviour
    {
        T t = str.GetComponent<T>();
        if (t == null)
        {
            t = str.AddComponent<T>();
        }

        return t;
    }
    public static void SetText(this Text txtObj, string text, bool isAnimation = false, ScrambleMode scrambleMode = ScrambleMode.None)
    {
        if (txtObj != null)
        {
            if (isAnimation)
            {
                txtObj.text = "";
                txtObj.DOText(text, .2f, scrambleMode: scrambleMode);
            }
            else
            {
                txtObj.text = text;
            }
        }
    }


    /// <summary>
    /// 设置 Text 对象的文本内容，支持数字递增动画。
    /// </summary>
    /// <param name="txtObj">需要设置文本的 Text 对象。</param>
    /// <param name="endValue">数字递增的目标值（如100）。</param>
    /// <param name="isAnimation">是否启用动画。</param>
    /// <param name="duration">动画时长。</param>
    public static void SetText(this Text txtObj, int endValue, bool isAnimation = false, float duration = .2f)
    {
        if (txtObj != null)
        {
            if (isAnimation)
            {
                // 从 0 开始递增到目标值 endValue
                DOTween.To(() => 0, x => txtObj.text = x.ToString(), endValue, duration)
                    .SetEase(Ease.Linear);  // 设置缓动方式为线性（线性增加）
            }
            else
            {
                // 直接设置为目标值
                txtObj.text = endValue.ToString();
            }
        }
    }
    /// <summary>
    /// 设置 Text 对象的文本内容，支持数字递增动画。
    /// </summary>
    /// <param name="txtObj">需要设置文本的 Text 对象。</param>
    /// <param name="endValue">数字递增的目标值（如100）。</param>
    /// <param name="isAnimation">是否启用动画。</param>
    /// <param name="duration">动画时长。</param>
    public static void SetImageFillAmount(this Image txtObj, float endValue, bool isAnimation = false, float duration = .2f)
    {
        if (txtObj != null)
        {
            if (isAnimation)
            {
                // 从 0 开始递增到目标值 endValue
                DOTween.To(() => 0, x => txtObj.fillAmount = x, endValue, duration)
                    .SetEase(Ease.Linear);  // 设置缓动方式为线性（线性增加）
            }
            else
            {
                // 直接设置为目标值
                txtObj.fillAmount = endValue;
            }
        }
    }
    /// <summary>
    /// 检查当前动画片段是否是指定Tag
    /// </summary>
    /// <param name="animator"></param>
    /// <param name="tagName"></param>
    /// <param name="indexLayer"></param>
    /// <returns></returns>
    public static bool AnimationAtTag(this Animator animator, string tagName, int indexLayer = 0)
    {
        return animator.GetCurrentAnimatorStateInfo(indexLayer).IsTag(tagName);


    }
}
