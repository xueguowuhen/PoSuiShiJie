/****************************************************
    文件：LuaRoot
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-11-18 20:59:45
	功能：Nothing
*****************************************************/
using System;
using System.Collections.Generic;

using UnityEngine;

public class xLuaRoot : MonoBehaviour
{
    public static xLuaRoot Instance;
    public void Init()
    {
        //这里写初始化代码
        Instance = this;
        LuaMgr luaMgr= gameObject.AddComponent<LuaMgr>();
        luaMgr.InitMgr();
      //  DontDestroyOnLoad(gameObject);
        //这里执行第一个lua脚本
        LuaMgr.Instance.DoString("require 'Download/xLuaLogic/Main'");
    }

}
