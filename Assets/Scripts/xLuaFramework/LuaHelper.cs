/****************************************************
    文件：LuaHelper
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-11-19 16:51:33
	功能：Nothing
*****************************************************/
using CommonNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;
[LuaCallCSharp]
public class LuaHelper:Singleton<LuaHelper>
{
    public MainCitySys MainCitySys
    {
        get
        {
            return MainCitySys.instance;
        }
    }
    public AssetLoaderSvc AssetLoaderSvc
    {
        get
        {
            return AssetLoaderSvc.Instance;
        }
    }
    public ResSvc ResSvc
    {
        get
        {
            return ResSvc.Instance;
        }
    }
    public GameRoot GameRoot
    {
        get
        {
            return GameRoot.Instance;
        }
    }
    public GameObjectPoolManager GameObjectPoolManager
    {
        get
        {
            return GameObjectPoolManager.Instance;
        }
    }
    public NetSvc NetSvc
    {
        get
        {
            return NetSvc.Instance;
        }
    }
    /// <summary>
    /// 添加Lua监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="callback"></param>
    public void AddEventListener(CMD protoCode, SocketDispatcher.OnActionHandler callback)
    {
        SocketDispatcher.Instance.AddEventListener(protoCode, callback);
    }
    /// <summary>
    /// 移除Lua监听
    /// </summary>
    /// <param name="protoCode"></param>
    /// <param name="callback"></param>
    public void RemoveEventListener(CMD protoCode, SocketDispatcher.OnActionHandler callback)
    {
        SocketDispatcher.Instance.RemoveEventListener(protoCode, callback);
    }
}