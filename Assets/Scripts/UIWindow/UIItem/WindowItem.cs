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


