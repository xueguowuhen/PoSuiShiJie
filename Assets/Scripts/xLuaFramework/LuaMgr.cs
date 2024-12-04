/****************************************************
    文件：LuaMgr
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-11-18 21:12:49
	功能：Nothing
*****************************************************/
using System;
using UnityEngine;
using XLua;

public class LuaMgr : MonoBehaviour
{
    public static LuaEnv luaEnv;
    public static LuaMgr Instance;
    [CSharpCallLua]
    public Action<string> loadViewFunc;
    //private OnloadViewCall loadViewFunc;
    public void InitMgr()
    {
        luaEnv = new LuaEnv();
        Instance = this;
        //2.设置xLua的脚本路径
#if DEBUG_ASSETBUNDLE
        luaEnv.DoString(string.Format("package.path = '{0}/?.lua'", Application.persistentDataPath));
#elif UNITY_EDITOR
        luaEnv.DoString(string.Format("package.path = '{0}/?.lua'", Application.dataPath));
#endif
        //luaEnv.DoString(string.Format("package.path = '{0}/?.lua'", Application.dataPath));
    }
    /// <summary>
    /// 执行lua脚本
    /// </summary>
    /// <param name="str"></param>
    public void DoString(string str)
    {
        luaEnv.DoString(str);
    }
    public void LoadView(string type)
    {
        if (loadViewFunc != null)
        {
            loadViewFunc?.Invoke(type);
        }
    }

    [LuaCallCSharp]
    public void InitLoadView(Action<string> loadViewFunc)
    {
        this.loadViewFunc = loadViewFunc;
    }
}
