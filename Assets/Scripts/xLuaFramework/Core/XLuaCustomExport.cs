/****************************************************
    文件：XLuaCustomExport.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/11/13 10:10:12
    功能：Nothing
*****************************************************/
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using CommonNet;

public static class XLuaCustomExport
{
    [CSharpCallLua]
    public delegate void OnCreate(GameObject obj);
    // Start is called before the first frame update
    [LuaCallCSharp]
    [ReflectionUse]
    public static List<Type> dotween_lua_call_cs_list = new List<Type>()
    {
        typeof(DG.Tweening.AutoPlay),
        typeof(DG.Tweening.AxisConstraint),
        typeof(DG.Tweening.Ease),
        typeof(DG.Tweening.LogBehaviour),
        typeof(DG.Tweening.LoopType),
        typeof(DG.Tweening.PathMode),
        typeof(DG.Tweening.PathType),
        typeof(DG.Tweening.RotateMode),
        typeof(DG.Tweening.ScrambleMode),
        typeof(DG.Tweening.TweenType),
        typeof(DG.Tweening.UpdateType),
        typeof(DG.Tweening.DOTweenModuleUI),
        typeof(DG.Tweening.DOTween),
        typeof(DG.Tweening.DOVirtual),
        typeof(DG.Tweening.EaseFactory),
        typeof(DG.Tweening.Tweener),
        typeof(DG.Tweening.Tween),
        typeof(DG.Tweening.Sequence),
        typeof(DG.Tweening.TweenParams),
        typeof(DG.Tweening.Core.ABSSequentiable),
        typeof(DG.Tweening.Core.TweenerCore<Vector3,Vector3,DG.Tweening.Plugins.Options.VectorOptions>),

        typeof(DG.Tweening.TweenCallback),
        typeof(DG.Tweening.TweenExtensions),
        typeof(DG.Tweening.TweenSettingsExtensions),
        typeof(DG.Tweening.ShortcutExtensions),

    };
    [LuaCallCSharp]
    [ReflectionUse]
    public static List<Type> GameMsg_lua_call_cs_list = new List<Type>()
    {
        typeof(GameMsg),
        typeof(CMD),
        typeof(Error),
    };

}
