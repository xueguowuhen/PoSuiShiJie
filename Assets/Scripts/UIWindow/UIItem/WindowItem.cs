/****************************************************
    文件：WindowItem
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-09 14:31:44
	功能：Nothing
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


public class WindowItem:MonoBehaviour
{
    private bool canClick = true; // 用于控制点击频率
    private float clickDelay = 1f; // 点击间隔时间

    /// <summary>
    /// 点击间隔控制
    /// </summary>
    /// <param name="action">要执行的操作</param>
    protected void ClickWithDelay(Action action)
    {
        if (!canClick)
        {
            Debug.Log("不能点击");
            return; // 如果不能点击，则退出方法
        }

        canClick = false; // 设置为不可点击
        action.Invoke(); // 执行传入的操作

        // 启动协程来恢复可点击状态
        StartCoroutine(ResetClick());
    }

    // 协程用于在一定时间后恢复按钮状态
    private IEnumerator ResetClick()
    {
        yield return new WaitForSeconds(clickDelay);
        canClick = true; // 恢复为可点击状态
    }
    //设置监听
    protected void AddListener(Button button, UnityAction call)
    {
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(call);
    }
    //设置监听
    protected void AddListener(Toggle toggle, UnityAction<bool> call)
    {
        toggle.onValueChanged.RemoveAllListeners();
        toggle.onValueChanged.AddListener(call);
    }
}


