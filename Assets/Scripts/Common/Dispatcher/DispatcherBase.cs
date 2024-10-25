/****************************************************
    文件：DispatcherBase
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-25 12:37:33
	功能：
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DispatcherBase<T,P,X>: IDisposable
    where T : new()
{
    #region 单例
    private static T instance;
    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new T();
            }
            return instance;
        }
    }

    public virtual void Dispose()
    {

    }
    #endregion
    public delegate void OnActionHandler(P param);

    public Dictionary<X,List< OnActionHandler>> dic = new Dictionary<X, List<OnActionHandler>>();

    #region AddEventListener 添加监听
    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="btnKey"></param>
    /// <param name="handler"></param>
    public void AddEventListener(X Key, OnActionHandler handler)
    {
        if (dic.ContainsKey(Key))
        {
            dic[Key].Add(handler);
        }
        else
        {
            List<OnActionHandler> Lsthandler = new List<OnActionHandler>
            {
                handler
            };
            dic[Key] = Lsthandler;
        }
    }
    #endregion
    #region RemoveEventListener 移出监听
    /// <summary>
    /// 移出监听
    /// </summary>
    /// <param name="btnKey"></param>
    /// <param name="handler"></param>
    public void RemoveEventListener(X Key, OnActionHandler handler)
    {
        if (dic.ContainsKey(Key))
        {
            List<OnActionHandler> lstHandler = dic[Key];
            lstHandler.Remove(handler);
            if (lstHandler.Count == 0)
            {
                dic.Remove(Key);
            }
        }
    }
    #endregion

    #region Dispatch 派发按钮点击
    /// <summary>
    /// 派发按钮点击 
    /// </summary>
    /// <param name="actionID"></param>
    /// <param name="param"></param>
    public void Dispatch(X Key, P p)
    {
        if (dic.ContainsKey(Key))
        {
            List<OnActionHandler> lstHandler = dic[Key];//获取监听集合
            if (lstHandler != null && lstHandler.Count > 0)//存在对象正在监听
            {
                for (int i = 0; i < lstHandler.Count; i++)
                {
                    if (lstHandler[i] != null)
                    {
                        lstHandler[i](p);
                    }
                }
            }
        }
    }
    /// <summary>
    /// 派发按钮点击 
    /// </summary>
    /// <param name="actionID"></param>
    /// <param name="param"></param>
    public void Dispatch(X Key, object value)
    {
        Dispatch(Key, null);
    }
    #endregion

}

