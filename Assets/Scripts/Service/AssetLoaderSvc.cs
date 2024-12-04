/****************************************************
    文件：AssetLoaderSvc
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-11-07 22:28:54
	功能：Nothing
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;


public class AssetLoaderSvc : SvcBase<AssetLoaderSvc>
{

    private int Playerprogress;
    private AssetBundleManifest manifest;

    private string m_LocalFilePath
    {
        get
        {
            return DowningSys.instance.GetLocalFilePath();
        }
    }
    public override void InitSvc()
    {
        base.InitSvc();

        GameCommon.Log("AssetLoaderSvc Init....");
    }
    /// <summary>
    /// 异步加载
    /// </summary>
    public IEnumerator AsyncLoadScene(string sceneName, Action loaded, bool isAb = false)
    {
        UnloadAssetBundle();
        GameRoot.Instance.loadingWnd.SetWndState();
#if DEBUG_ASSETBUNDLE
        if (isAb)
        {
            string path = string.Format("{0}/ResScenes/{1}.unity3d", PathDefine.ABDownload, sceneName).ToLower();
            string LocalFilePath = m_LocalFilePath;
            string fullPath = Path.Combine(LocalFilePath, path);
            if (!File.Exists(fullPath))
            {
                //如果路径不存在
                DownloadDataEntity entity = DowningSys.instance.GetServerData(path);
                if (entity != null)
                {
                    Debug.LogError("路径不存在，开始下载");
                    DowningSys.instance.DownloadData(entity, () =>
                    {
                        StartCoroutine(LoadAbScene(fullPath, sceneName, loaded));
                    });

                }
                yield break;
            }
            else
            {
                StartCoroutine(LoadAbScene(fullPath, sceneName, loaded));
                yield break;
            }
        }
#endif
        StartCoroutine(LoadScene(sceneName, loaded));
        yield break;
    }
    private AssetBundle sceneBundle;
    private IEnumerator LoadAbScene(string full, string sceneName, Action loaded)
    {
        AssetBundleCreateRequest request = AssetBundle.LoadFromFileAsync(full);
        yield return request;
        sceneBundle = request.assetBundle;

        StartCoroutine(LoadScene(sceneName, loaded));
    }
    private IEnumerator LoadScene(string SceneName, Action loaded)
    {
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(SceneName);
        asyncOperation.allowSceneActivation = false;
        Playerprogress = 0;
        float val = 0;
        int progress;
        while (asyncOperation.progress < 0.9f)
        {
            val = asyncOperation.progress;
            progress = (int)(val * 100);
            //Text.text = progress.ToString();
            while (Playerprogress < progress)
            {
                ++Playerprogress;
                GameRoot.Instance.loadingWnd.SetProgress(Playerprogress);
                yield return new WaitForEndOfFrame();
            }
            yield return null;
        }
        progress = 100;
        while (Playerprogress < progress)
        {
            ++Playerprogress;
            GameRoot.Instance.loadingWnd.SetProgress(Playerprogress);
            yield return new WaitForEndOfFrame();
        }
        //AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        asyncOperation.allowSceneActivation = true;

        /// 等待场景加载完成
        while (!asyncOperation.isDone)
        {
            yield return null;
        }

        if (loaded != null)
        {
            loaded();
        }

        // GameRoot.Instance.loadingWnd.SetWndState(false);
    }
    public void CloseLoadingWnd()
    {
        if (sceneBundle != null)
        {

            sceneBundle.Unload(false);
        }
        GameRoot.Instance.loadingWnd.SetWndState(false);
    }
    /// <summary>
    /// 加载该预制体
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    public void LoadPrefab(string path, string name, Action<GameObject> onCompleted, bool cache = false, bool instan = true)
    {

        LoadOrDownload(path, name, onCompleted);
    }
    /// <summary>
    /// 加载该预制体
    /// </summary>
    /// <param name="path"></param>
    /// <param name="cache"></param>
    /// <returns></returns>
    public void XLuaLoadPrefab(string path, string name, XLuaCustomExport.OnCreate onCompleted, bool cache = false, bool instan = true)
    {

        LoadOrDownload<GameObject>(path, name, null, onCompleted);
    }
    /// <summary>
    /// 镜像的字典
    /// </summary>
    private Dictionary<string, UnityEngine.Object> m_AssetDic = new Dictionary<string, UnityEngine.Object>();
    /// <summary>
    /// 依赖项的字典
    /// </summary>
    private Dictionary<string, AssetBundleLoader> m_DspAssetBundleLoaderDic = new Dictionary<string, AssetBundleLoader>();
    // 一个字典，用于存储正在下载的资源的下载队列
    private Dictionary<string, List<System.Action<UnityEngine.Object>>> downloadingAssets = new Dictionary<string, List<System.Action<UnityEngine.Object>>>();
    private Dictionary<string, List<XLuaCustomExport.OnCreate>> downloadingXLuaAssets = new Dictionary<string, List<XLuaCustomExport.OnCreate>>();
    /// <summary>
    /// 加载或下载资源
    /// </summary>
    /// <param name="path"></param>
    /// <param name="Name"></param>
    /// <param name="onCompleted"></param>
    /// <param name="type">0=Prefab,1=PNG</param>
    public void LoadOrDownload<T>(string path, string Name, System.Action<T> onCompleted = null, XLuaCustomExport.OnCreate onXLuaCompleted = null, byte type = 0)
        where T : UnityEngine.Object
    {
        string NewPath = (Path.Combine(PathDefine.ABDownload, path, Name) + PathDefine.AssetBundle).Replace("\\", "/");
        string NewName = (Path.Combine(PathDefine.ABDownload, path, Name) + PathDefine.Png).Replace("\\", "/");
        //依赖项加载完毕后，加载本地资源
        string fullpath = m_LocalFilePath + NewPath.ToLower();
        // 检查资源是否正在下载
        if (downloadingAssets.ContainsKey(fullpath) || downloadingXLuaAssets.ContainsKey(fullpath))
        {
          //  Debug.Log("资源正在下载中，等待回调");
            // 如果正在下载，将新的回调添加到队列中
            if (onCompleted != null)
            {
                downloadingAssets[fullpath].Add((asset) => onCompleted?.Invoke((T)asset));
            }
            if (onXLuaCompleted != null)
            {
                downloadingXLuaAssets[fullpath].Add(onXLuaCompleted);
            }
            return;
        }

#if DEBUG_ASSETBUNDLE
        LoadManifestBundle();
        string[] allBundles = manifest.GetAllAssetBundles();
        //2.加载该文件的依赖项
        NewPath = NewPath.ToLower();
        string[] arr = manifest.GetAllDependencies(NewPath);
        if (onCompleted != null)
        {
            downloadingAssets[fullpath] = new List<System.Action<UnityEngine.Object>> { (asset) => onCompleted?.Invoke((T)asset) };
        }
        if (onXLuaCompleted != null)
        {
            downloadingXLuaAssets[fullpath] = new List<XLuaCustomExport.OnCreate> { onXLuaCompleted };
        }
        CheckDeps(0, arr, () =>
        {

            //Debug.Log("加载本地资源：" + fullpath);
            if (!File.Exists(fullpath))//如果路径不存在
            {
                //查找该文件在服务器上的路径
                DownloadDataEntity entity = DowningSys.instance.GetServerData(NewPath);
                if (entity != null)
                {
                    Debug.LogError("路径不存在，开始下载");
                    DowningSys.instance.DownloadData(entity, () =>
                    {
                        LoadCallBack(fullpath, Name, arr, onCompleted, onXLuaCompleted);
                    });
                }
            }
            else
            {
                //查找该文件在服务器上的路径
                DownloadDataEntity entity = DowningSys.instance.GetServerData(NewPath);
                if (GetMD5(fullpath) != entity.MD5)
                {
                    Debug.LogError("MD5码不匹配，重新下载");
                    DowningSys.instance.DownloadData(entity, () =>
                    {
                        LoadCallBack(fullpath, Name, arr, onCompleted, onXLuaCompleted);
                    });
                }
                else
                {
                    LoadCallBack(fullpath, Name, arr, onCompleted, onXLuaCompleted);
                }
            }
        });
#elif UNITY_EDITOR

        string newPath = string.Empty;
        switch (type)
        {
            case 0:
                newPath = string.Format("Assets/{0}", NewPath.Replace("assetbundle", "prefab"));
                break;
            case 1:
                newPath = string.Format("Assets/{0}", NewPath.Replace("assetbundle", "png"));
                break;
        }
        if (onCompleted != null)
        {
            onCompleted(UnityEditor.AssetDatabase.LoadAssetAtPath<T>(newPath));

        }
        if (onXLuaCompleted != null)
        {
            onXLuaCompleted(UnityEditor.AssetDatabase.LoadAssetAtPath<GameObject>(newPath));
        }
#endif

    }

    /// <summary>
    /// 加载结束回调
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="fullpath"></param>
    /// <param name="Name"></param>
    /// <param name="arr"></param>
    /// <param name="onCompleted"></param>
    public void LoadCallBack<T>(string fullpath, string Name, string[] arr, Action<T> onCompleted = null, XLuaCustomExport.OnCreate onXLuaCompleted = null)
        where T : UnityEngine.Object
    {
        if (m_AssetDic.ContainsKey(fullpath))//该文件已经下载过添加到字典中
        {
            if (downloadingAssets.ContainsKey(fullpath))
            {
                foreach (var item in downloadingAssets[fullpath])
                {

                    item(m_AssetDic[fullpath] as T);
                }
                downloadingAssets.Remove(fullpath);
            }
            if (downloadingXLuaAssets.ContainsKey(fullpath))
            {

                foreach (var item in downloadingXLuaAssets[fullpath])
                {
                    item(m_AssetDic[fullpath] as GameObject);
                }
                downloadingXLuaAssets.Remove(fullpath);
            }

            return;
        }
        //先加载依赖项
        for (int i = 0; i < arr.Length; i++)
        {
            string depPath = m_LocalFilePath + arr[i];
            if (!m_AssetDic.ContainsKey(depPath))
            {
                AssetBundleLoader loader = new AssetBundleLoader(arr[i]);
                string assetName = Path.GetFileNameWithoutExtension(arr[i]);
                Object obj = loader.LoadAsset(assetName);
                //把依赖项的Loader加入字典
                m_DspAssetBundleLoaderDic[depPath] = loader;
                m_AssetDic[depPath] = obj;
                //loader.Dispose();
            }
        }
        if (!m_AssetDic.ContainsKey(fullpath))
        {
            using (AssetBundleLoader loader = new AssetBundleLoader(fullpath, isFullPath: true))
            {
                //Debug.Log("加载本地资源：" + fullpath);
                if (downloadingAssets.ContainsKey(fullpath))
                {
                    Object obj = loader.LoadAsset<T>(Name);
                    m_AssetDic[fullpath] = obj;
                    foreach (var item in downloadingAssets[fullpath])
                    {
                        item(obj as T);
                    }
                    downloadingAssets.Remove(fullpath);
                }
                if (downloadingXLuaAssets.ContainsKey(fullpath))
                {
                    Object obj = loader.LoadAsset<GameObject>(Name);
                    m_AssetDic[fullpath] = obj;
                    foreach (var item in downloadingXLuaAssets[fullpath])
                    {
                        item(obj as GameObject);
                    }

                    downloadingXLuaAssets.Remove(fullpath);
                }
            }
        }
    }
    /// <summary>
    /// 递归加载依赖项
    /// </summary>
    /// <param name="index"></param>
    /// <param name="deps"></param>
    /// <param name="onCompleted"></param>
    private void CheckDeps(int index, string[] deps, System.Action onCompleted)
    {
        if (deps == null || deps.Length == 0)
        {
            if (onCompleted != null) onCompleted();
            return;
        }
        string fullpath = m_LocalFilePath + deps[index];
        if (!File.Exists(fullpath))//如果路径不存在
        {
            //如果依赖项不存在
            DownloadDataEntity entity = DowningSys.instance.GetServerData(deps[index]);
            if (entity != null)
            {
                Debug.LogError("路径不存在，开始下载");
                DowningSys.instance.DownloadData(entity, () =>
                {
                    index++;
                    if (index == deps.Length)
                    {
                        if (onCompleted != null) onCompleted();
                        return;
                    }
                    CheckDeps(index, deps, onCompleted);
                });
            }
        }
        else
        {
            index++;
            if (index == deps.Length)
            {
                if (onCompleted != null) onCompleted();
                return;
            }
            CheckDeps(index, deps, onCompleted);
        }
    }
    private void DownloadLoad(string fullpath, string Name)
    {

    }
    /// <summary>
    /// 加载依赖文件配置
    /// </summary>
    private void LoadManifestBundle()
    {
        if (manifest != null)
        {
            return;
        }
        string assetName = DowningSys.instance.GetPlatform();
        using (AssetBundleLoader loader = new AssetBundleLoader(assetName))
        {
            manifest = loader.LoadAsset<AssetBundleManifest>("AssetBundleManifest");

        }
    }
    /// <summary>
    /// 卸载资源
    /// </summary>
    /// <param name="path"></param>
    public void UnloadAssetBundle()
    {
        foreach (var item in m_DspAssetBundleLoaderDic)
        {
            item.Value.Dispose();
        }
        m_DspAssetBundleLoaderDic.Clear();
        m_AssetDic.Clear();
    }
    public override void BeforeOnDestroy()
    {
        base.BeforeOnDestroy();
        UnloadAssetBundle();
        manifest = null;
        Debug.Log("AssetLoaderSvc BeforeOnDestroy....");
    }
    /// <summary>
    /// MD5码的获取学习
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    private static string GetMD5(string filePath)
    {
        using (FileStream file = new FileStream(filePath, FileMode.Open))
        {
            //声明一个MD5对象 用于生成MD5码
            MD5 md5 = new MD5CryptoServiceProvider();
            //利用API 得到数据的MD5码 16个字节 数组
            byte[] md5Info = md5.ComputeHash(file);
            file.Close();
            //把16个字节转换为16进制拼接成字符串 为了减少md5码的长度
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < md5Info.Length; i++)
            {
                sb.Append(md5Info[i].ToString("x2"));
            }
            return sb.ToString();
        }
    }
}