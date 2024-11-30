/****************************************************
    文件：SocketDispatcher
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-11-02 15:22:36
	功能：Nothing
*****************************************************/
using CommonNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLua;

public class SocketDispatcher: IDisposable
{
    #region 单例
    private static SocketDispatcher instance;
    public static SocketDispatcher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SocketDispatcher();
            }
            return instance;
        }
    }

    public virtual void Dispose()
    {

    }
    #endregion
    [CSharpCallLua]
    //按钮点击事件委托原型
    public delegate void OnActionHandler(GameMsg buffer);

    public Dictionary<CMD, List<OnActionHandler>> dic = new Dictionary<CMD, List<OnActionHandler>>();
    #region AddEventListener 添加监听
    /// <summary>
    /// 添加监听
    /// </summary>
    /// <param name="btnKey"></param>
    /// <param name="handler"></param>
    public void AddEventListener(CMD Key, OnActionHandler handler)
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
    public void RemoveEventListener(CMD Key, OnActionHandler handler)
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
    public void Dispatch(CMD Key, GameMsg buffer)
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
                        lstHandler[i](buffer);
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
    public void Dispatch(CMD Key)
    {
        Dispatch(Key, null);
    }
    #endregion
}