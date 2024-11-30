/****************************************************
    文件：AssetBundleLoader
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-07-14 20:48:58
	功能：Nothing
*****************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class AssetBundleLoader : IDisposable
{
    private AssetBundle bundle;
    public AssetBundleLoader(string assetBundlePath,bool isFullPath=false)//根据地址获取对应文件，并进行加载
    {
        string fullPath =isFullPath?assetBundlePath: DowningSys.instance.GetLocalFilePath()  + assetBundlePath;

        bundle = AssetBundle.LoadFromFile(fullPath);//从内存区域同步加载AB包

    }
    /// <summary>
    /// 根据资源名加载资源
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="name"></param>
    /// <returns></returns>
    public T LoadAsset<T>(string name) where T : UnityEngine.Object
    {
        if (bundle == null) return default(T);
        //foreach (var assetName in bundle.GetAllAssetNames())
        //{
        //    Debug.Log("Asset in bundle: " + assetName);
        //}
        string newname = name.ToLower();
        return bundle.LoadAsset(newname) as T;
    }
    public UnityEngine.Object LoadAsset(string name)
    {
        return LoadAsset<UnityEngine.Object>(name);
    }
    /// <summary>
    /// 读取本地文件到byte数组中
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public byte[] GetBuffer(string path)
    {

        byte[] buffer = null;
        using (FileStream fs = new FileStream(path, FileMode.Open))
        {
            buffer = new byte[fs.Length];
            fs.Read(buffer, 0, buffer.Length);

        }
        return buffer;
    }
    public void Dispose()
    {
        //Debug.Log("AssetBundleLoader Dispose");
        if (bundle != null) bundle.Unload(false);
    }
}
