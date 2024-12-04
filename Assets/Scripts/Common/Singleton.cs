/****************************************************
    文件：Singleeton.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/6/24 15:56:16
	功能：单例基类
*****************************************************/

using System;
public class Singleton<T> : IDisposable where T : new()
{
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
}