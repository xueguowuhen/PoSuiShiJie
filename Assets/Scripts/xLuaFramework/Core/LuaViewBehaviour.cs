/****************************************************
    文件：LuaViewBehaviour.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/11/13 19:21:25
    功能：映射lua脚本的Awake、Start、Update、OnDestroy方法
*****************************************************/
using System;
using System.Collections;
using UnityEngine;
using XLua;
[LuaCallCSharp]
public class LuaViewBehaviour : MonoBehaviour
{
    [CSharpCallLua]
    public delegate void delLuaAwake(GameObject obj);
    LuaViewBehaviour.delLuaAwake luaAwake;
    [CSharpCallLua]
    public delegate void delLuaStart();
    LuaViewBehaviour.delLuaStart luaStart;
    [CSharpCallLua]
    public delegate void delLuaUpdate();
    LuaViewBehaviour.delLuaUpdate luaUpdate;

    [CSharpCallLua]
    public delegate void delLuaEnable();
    LuaViewBehaviour.delLuaEnable luaEnable;
    [CSharpCallLua]
    public delegate void delLuaDisable();
    LuaViewBehaviour.delLuaEnable luaDisable;

    [CSharpCallLua]
    public delegate void delLuaOnDestroy();
    LuaViewBehaviour.delLuaOnDestroy luaOnDestroy;

    private LuaTable scripEnv;
    private LuaEnv luaEnv;
    void Awake()
    {
        luaEnv = LuaMgr.luaEnv;
        //创建新表
        scripEnv = luaEnv.NewTable();
        //设置元表和__index索引
        //创建了一个新的 mate 表
        LuaTable mate = luaEnv.NewTable();
        //__index 元方法设置为 luaEnv.Global
        mate.Set("__index", luaEnv.Global);
        scripEnv.SetMetaTable(mate);
        //释放 mate 表
        mate.Dispose();
        string prefabName = name;
        //去掉(Clone)，因为克隆的物体会带有(Clone)
        if (prefabName.Contains("(Clone)"))
        {
            prefabName = prefabName.Split(new string[] { "(Clone)" }, System.StringSplitOptions.RemoveEmptyEntries)[0];
        }
        //移除pan_前缀
        prefabName = prefabName.Replace("pan_", "");
        Debug.Log("prefabName:" + prefabName);
        luaAwake = scripEnv.GetInPath<LuaViewBehaviour.delLuaAwake>(prefabName + ".Awake");
        luaEnable = scripEnv.GetInPath<LuaViewBehaviour.delLuaEnable>(prefabName + ".enable");
        luaStart = scripEnv.GetInPath<LuaViewBehaviour.delLuaStart>(prefabName + ".start");
        luaUpdate = scripEnv.GetInPath<LuaViewBehaviour.delLuaUpdate>(prefabName + ".update");
        luaDisable = scripEnv.GetInPath<LuaViewBehaviour.delLuaEnable>(prefabName + ".disable");
        luaOnDestroy = scripEnv.GetInPath<LuaViewBehaviour.delLuaOnDestroy>(prefabName + ".onDestroy");
        scripEnv.Set("self", this);
        if (luaAwake != null)
        {
            luaAwake(gameObject);
        }
    }
    void OnDisable()
    {
        if (luaDisable != null)
        {
            luaDisable();
        }
    }
    void OnEnable()
    {
        if (luaEnable != null)
        {
            luaEnable();
        }
    }
    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }

    }
    void Update()
    {
        if (luaUpdate != null)
        {
            luaUpdate();
        }
    }

    public void YieldAndCallback(object to_yield, Action callback)
    {
        // 开启协程，回调callback
        StartCoroutine(CoBody(to_yield, callback));
    }

    private IEnumerator CoBody(object to_yield, Action callback)
    {
        if (to_yield is IEnumerator)
            yield return StartCoroutine((IEnumerator)to_yield);
        else
            yield return to_yield;
        callback();
    }
    void OnDestroy()
    {
        if (luaOnDestroy != null)
        {
            luaOnDestroy();
        }
        luaOnDestroy = null;
        luaEnable = null;
        luaUpdate = null;
        luaStart = null;
        luaAwake = null;
        scripEnv.Dispose();
    }
}
