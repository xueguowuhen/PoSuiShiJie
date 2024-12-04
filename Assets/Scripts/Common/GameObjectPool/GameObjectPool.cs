/****************************************************
    文件：GameObjectPool
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-01 7:41:19
	功能：对象池管理
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class GameObjectPool : MonoBehaviour // 继承 MonoBehaviour
{
    private Stack<GameObject> poolStack = new Stack<GameObject>();
    /// <summary>
    /// 设置对象池最大容量
    /// </summary>
    public int MaxCount = 10;
    /// <summary>
    /// 是否启动自动清理
    /// </summary>
    public bool cullDespawned = true;
    /// <summary>
    /// 缓存池自动清理但会保留一定数量不清理
    /// </summary>
    public int cullAbove = 5;
    /// <summary>
    /// 每次清理的时间，单位为秒
    /// </summary>
    public float cullDelay = 2f;
    /// <summary>
    /// 每次清理的数量
    /// </summary>
    public int cullMaxPerPass = 2;

    public GameObject prefab; // 保存预制体引用

    // 初始化对象池
    public void Init()
    {
        // 预加载指定数量的对象
        for (int i = 0; i < MaxCount; i++)
        {
            GameObject obj = Object.Instantiate(prefab, transform);
            obj.SetActive(false);
            poolStack.Push(obj);
        }

        // 启动清理协程
        if (cullDespawned)
        {
            StartCullCoroutine(); // 启动清理协程
        }
    }

    // 从对象池获取对象
    public GameObject GetObject()
    {
        if (poolStack.Count > 0)
        {
            GameObject obj = poolStack.Pop();
            obj.SetActive(true);
            return obj;
        }
        else
        {
            return CreateNewObject(); // 创建并返回新对象
        }
    }

    // 将对象放回对象池
    public void ReturnObject(GameObject obj)
    {
        if (obj.transform.parent != transform)
        {
            obj.transform.SetParent(transform);
        }
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localRotation = Quaternion.identity;
        obj.transform.localScale = Vector3.one;

        obj.SetActive(false);
        poolStack.Push(obj);

        // 检查并销毁超出最大容量的对象
        ManagePoolSize();
    }
    // 创建新对象并返回
    private GameObject CreateNewObject()
    {
        GameObject obj = Object.Instantiate(prefab, transform);
        obj.SetActive(true);
        return obj;
    }

    // 管理对象池大小，销毁多余的对象
    private void ManagePoolSize()
    {
        while (poolStack.Count > MaxCount)
        {
            GameObject obj = poolStack.Pop();
            Object.Destroy(obj); // 销毁超出最大容量的对象
        }
    }

    // 启动自动清理协程
    private void StartCullCoroutine()
    {
        StartCoroutine(CullDespawnedObjects()); // 启动清理协程
    }
    // 清空对象池
    public void ClearPool()
    {
        while (poolStack.Count > 0)
        {
            GameObject obj = poolStack.Pop();
            Object.Destroy(obj); // 销毁对象
        }
    }
    // 清理被去除的对象
    private System.Collections.IEnumerator CullDespawnedObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(cullDelay);

            if (poolStack.Count > cullAbove)
            {
                for (int i = 0; i < cullMaxPerPass && poolStack.Count > cullAbove; i++)
                {
                    GameObject obj = poolStack.Pop();
                    Object.Destroy(obj);
                }
            }
        }
    }
}
