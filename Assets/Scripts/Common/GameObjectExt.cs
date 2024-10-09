/****************************************************
    文件：GameObjectExt
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-01 0:10:21
	功能：Nothing
*****************************************************/
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
}
