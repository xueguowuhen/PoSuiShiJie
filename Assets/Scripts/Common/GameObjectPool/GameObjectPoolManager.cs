using System.Collections.Generic;
using UnityEngine;

public class GameObjectPoolManager : MonoBehaviour
{
    private Dictionary<string, GameObjectPool> pools = new Dictionary<string, GameObjectPool>();

    // 单例模式
    private static GameObjectPoolManager instance;

    public static GameObjectPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                // 在场景中创建一个新的 GameObject 来挂载 GameObjectPoolManager
                GameObject poolManagerObject = new GameObject("GameObjectPoolManager");
                instance = poolManagerObject.AddComponent<GameObjectPoolManager>();
                DontDestroyOnLoad(poolManagerObject); // 保持它不被销毁
            }
            return instance;
        }
    }

    /// <summary>
    /// 创建对象池
    /// </summary>
    public GameObjectPool CreatePrefabPool(GameObject prefab)
    {
        string poolName = prefab.name;

        // 检查字典中是否已有该对象池
        if (pools.TryGetValue(poolName, out GameObjectPool pool))
        {
            Debug.LogWarning($"对象池 '{poolName}' 已经存在。");
            return pool; // 如果已存在，直接返回
        }

        GameObject poolObject = new GameObject(prefab.name + "_Pool");
        poolObject.transform.SetParent(this.transform, false); // 将新对象设置为 GameObjectPoolManager 的子物体
        pool = poolObject.AddComponent<GameObjectPool>(); // 将 GameObjectPool 组件挂载到新对象上
        poolObject.transform.localScale = Vector3.one;
        poolObject.transform.localRotation = Quaternion.identity;
        //  poolObject.transform.localPosition = Vector3.zero;
        pool.prefab = prefab;
        pools[poolName] = pool; // 将新池添加到字典中

        return pool; // 返回新创建的对象池
    }

    // 获取指定 prefab 的对象池
    public GameObjectPool GetPool(GameObject prefab)
    {
        string poolName = prefab.name;

        // 检查字典中是否已有该对象池
        if (!pools.TryGetValue(poolName, out GameObjectPool pool))
        {
            // 创建新的 GameObject 并挂载 GameObjectPool 组件
            GameObject poolObject = new GameObject(poolName); // 创建一个新的 GameObject
            poolObject.transform.parent = this.transform; // 将新对象设置为 GameObjectPoolManager 的子物体
            pool = poolObject.AddComponent<GameObjectPool>(); // 将 GameObjectPool 组件挂载到新对象上
            pools[poolName] = pool; // 将新池添加到字典中
        }

        return pool; // 返回对象池
    }

    // 可选：清理所有对象池
    public void ClearAllPools()
    {
        foreach (var pool in pools.Values)
        {
            pool.ClearPool(); // 可能需要在 GameObjectPool 中实现 ClearPool 方法
        }

        pools.Clear();
    }
    // 可选：清理指定 prefab 的对象池
    public void ClearPool(GameObject prefab)
    {
        string poolName = prefab.name;

        if (pools.TryGetValue(poolName, out GameObjectPool pool))
        {
            pool.ClearPool();
            pools.Remove(poolName);
        }
    }
    // 可选：Unity的Start方法，可以用于初始化逻辑
    private void Start()
    {
        // 可以在这里添加其他初始化逻辑
    }
}
