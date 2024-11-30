/****************************************************
    文件：DowningWnd.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/5/27 13:53:29
	功能：ab包资源更新
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


public class DowningWnd : WindowRoot
{
    #region UI控件
    public Text txtTips;
    public Text txtSpeed;//下载速度
    public Text txtProgress;//下载进度
    public Image imageFG;
    public Text txtPrg;
    #endregion
    public string LocalFilePath; //本地资源路径

    private DataProcess dataProcess;
    // private Dictionary<string, ABInfo> remoteABInfo = new Dictionary<string, ABInfo>();
    public Dictionary<string, ABInfo> tmpABInfo = new Dictionary<string, ABInfo>();
    private bool IsDown = false;
    #region 下载相关
    /// <summary>
    /// 本地版本文件路径
    /// </summary>
    private string m_LoaclVersionPath;
    /// <summary>
    /// 平台名
    /// </summary>
    private string platform;
    /// <summary>
    /// StreamingAssets路径
    /// </summary>
    private string m_StreamingAssetsPath;
    /// <summary>
    /// 版本文件名
    /// </summary>
    private string m_VersionFileName = "VersionFile.txt";
    /// <summary>
    /// 下载地址
    /// </summary>
    public static string DownloadBaseUrl = "http://121.40.86.210/morangABLoad/AssetBundles/";
    private List<DownloadDataEntity> m_LocalDataList;
    private List<DownloadDataEntity> m_NeedDownloadDataList = new List<DownloadDataEntity>();
    #endregion
    protected override void InitWnd()
    {
        base.InitWnd();
        SetText(txtSpeed, "");
        SetText(txtProgress, "");
        imageFG.fillAmount = 0;
        LocalFilePath = Application.persistentDataPath + "/";
#if UNITY_EDITOR || UNITY_STANDALONE_WIN
        platform = PathDefine.Windows;
#elif UNITY_ANDROID
        platform = PathDefine.Android;
#endif
        InitStreamingAssets();
    }
    public void Update()
    {
        if (IsDown)//开始下载时
        {
            float speed = dataProcess.GetTotalSpeed();
            string speedUnit = "KB/s";
            if (speed >= 1024) // 如果速度超过1MB/s
            {
                speed = speed / 1024; // 转换为MB/s
                speedUnit = "MB/s";
            }
            SetText(txtSpeed, "速度" + speed.ToString("F2") + speedUnit);
            SetText(txtProgress, "文件进度" + dataProcess.GetFinishCount() + "/" + m_NeedDownloadDataList.Count);
            long DownByte = dataProcess.TotalDownByte();
            long TotalByte = dataProcess.TotalByte();
            if (TotalByte != 0)
            {
                float pross = (float)DownByte / TotalByte;
                SetText(txtPrg, ((float)pross * 100).ToString("F2") + "%");
                //imageFG.fillAmount = pross;
            }
        }
    }
    protected override void SetGameObject()
    {
        //txtSpeed = GetText(PathDefine.Downing_txtSpeed);
        //txtProgress = GetText(PathDefine.Downing_txtProgress);
        //imageFG = GetImg(PathDefine.Downing_imageFG);
        //txtPrg = GetText(PathDefine.Downing_txtPrg);
        //txtTips = GetText(PathDefine.Downing_txtTips);
        dataProcess = GameRoot.Instance.gameObject.GetOrAddComponent<DataProcess>();
    }
    /// <summary>
    /// 第一步：初始化资源
    /// </summary>
    public void InitStreamingAssets()
    {
#if DEBUG_ASSETBUNDLE
        m_LoaclVersionPath = Path.Combine(LocalFilePath, m_VersionFileName).Replace("\\", "/");
        //判断本地persistentData是否已经有资源
        if (File.Exists(m_LoaclVersionPath))
        {
            //如果有资源 则检查更新
            InitCheckVersion();
        }
        else
        {
            //如果没有资源 先读取本地文件初始化 然后再检查更新

            m_StreamingAssetsPath = Application.streamingAssetsPath + "/AssetBundles/";
#if UNITY_ANDROID && !UNITY_EDITOR
            m_StreamingAssetsPath = "file:///" + Application.streamingAssetsPath + "/AssetBundles/";
#endif
            string versionFileUrl = m_StreamingAssetsPath + m_VersionFileName;

            StartCoroutine(ReadStreamingAssetVersionFile(versionFileUrl, OnReadStreamingAssetOver));

        }
#elif UNITY_EDITOR
        EnterLogin();
#endif

    }
    /// <summary>
    /// Streaming读取初始资源目录的版本文件
    /// </summary>
    /// <param name="fileUrl"></param>
    /// <param name="onReadStreamingAssetOver"></param>
    /// <returns></returns>
    private IEnumerator ReadStreamingAssetVersionFile(string fileUrl, Action<string> onReadStreamingAssetOver)
    {
        //UISceneInitCtrl.Instance.SetProgress("正在准备进行资源初始化", 0);

        using (UnityWebRequest request = UnityWebRequest.Get(fileUrl))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                onReadStreamingAssetOver(Encoding.UTF8.GetString(request.downloadHandler.data));
            }
            else
            {
                onReadStreamingAssetOver("");
            }
        }
    }

    /// <summary>
    /// 读取版本文件完毕
    /// </summary>
    /// <param name="obj"></param>
    private void OnReadStreamingAssetOver(string content)
    {
        StartCoroutine(InitStreamingAssetList(content));
    }
    /// <summary>
    /// 初始化资源清单
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    private IEnumerator InitStreamingAssetList(string content)
    {
        if (string.IsNullOrEmpty(content))//如果本地文件为空，直接检测版本
        {
            InitCheckVersion();
            yield break;
        }

        string[] arr = content.Split('\n');

        //循环解压
        for (int i = 0; i < arr.Length; i++)
        {
            string[] arrInfo = arr[i].Split(' ');

            string fileUrl = arrInfo[0]; //短路径

            yield return StartCoroutine(AssetLoadToLocal(m_StreamingAssetsPath + fileUrl, LocalFilePath + fileUrl));

            float value = (i + 1) / (float)arr.Length;
            SetText(txtTips, (string.Format("初始化资源不消耗流量 {0}/{1}", i + 1, arr.Length)));
        }

        //解压版本文件
        yield return StartCoroutine(AssetLoadToLocal(m_StreamingAssetsPath + m_VersionFileName, LocalFilePath + m_VersionFileName));

        //最后再次更新文件
        InitCheckVersion();
    }
    /// <summary>
    /// 解压m_StreamingAssetsPath文件到本地persistentDataPath
    /// </summary>
    /// <param name="fileUrl"></param>
    /// <param name="toPath"></param>
    /// <returns></returns>
    private IEnumerator AssetLoadToLocal(string fileUrl, string toPath)
    {
        using (UnityWebRequest www = UnityWebRequest.Get(fileUrl))
        {
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.Success)
            {
                int lastIndexOf = toPath.LastIndexOf('\\');
                if (lastIndexOf != -1)
                {
                    string localPath = toPath.Substring(0, lastIndexOf); // 除去文件名以外的路径

                    if (!Directory.Exists(localPath))
                    {
                        Directory.CreateDirectory(localPath);
                    }
                }

                byte[] fileData = www.downloadHandler.data;

                using (FileStream fs = new FileStream(toPath, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(fileData, 0, fileData.Length);
                }

                Debug.Log("文件下载并保存成功：" + toPath);
            }
            else
            {
                Debug.LogError("文件下载失败：" + www.error);
            }
        }

    }
    /// <summary>
    /// 下载AB包对比文件
    /// </summary>
    private void InitCheckVersion()
    {
        SetText(txtTips, "正在检查资源更新中...");
        //   SetText(txtTips, platform);
        dataProcess.AddDownloadRes(Path.Combine(DownloadBaseUrl, platform, platform), LocalFilePath, () =>
        {
            dataProcess.AddVersion(Path.Combine(DownloadBaseUrl, platform, m_VersionFileName), OnInitVersionCallBack);
        });

    }
    /// <summary>
    /// 初始化版本文件回调
    /// </summary>
    /// <param name="obj"></param>
    private void OnInitVersionCallBack(string conText)
    {
        SetText(txtTips, "正在更新资源中...");
        List<DownloadDataEntity> serverDownloadData = PackDownloadData(conText);
        DowningSys.instance.SetServerDataList(serverDownloadData);
        if (File.Exists(m_LoaclVersionPath))
        {
            //如果本地存在版本文件 则和服务器端的进行对比
            //服务器端数据
            Dictionary<string, string> serverDic = PackDownloadDataDic(serverDownloadData);
            //本地数据
            string content = IOUtil.GetFileText(m_LoaclVersionPath);
            Dictionary<string, string> clientDic = PackDownloadDataDic(content);
            m_LocalDataList = PackDownloadData(content);

            //1.新加的初始资源
            for (int i = 0; i < serverDownloadData.Count; i++)
            {
                //如果文件是初始资源且不在本地客户端资源中
                if (serverDownloadData[i].IsFirstData && !clientDic.ContainsKey(serverDownloadData[i].FullName))
                {
                    m_NeedDownloadDataList.Add(serverDownloadData[i]); //加入下载列表
                }
            }
            //2.对比已经下载过的，但是有更新的资源
            foreach (var item in clientDic)
            {
                DownloadDataEntity entity = GetDownloadData(item.Key, serverDownloadData);
                if (entity == null)
                {
                    continue;
                }
                if(!File.Exists(Path.Combine(LocalFilePath, entity.FullName).Replace("\\", "/")))
                {
                    m_NeedDownloadDataList.Add(entity);
                    continue;
                }
                //如果MD5不一致
                if ((serverDic.ContainsKey(item.Key) && serverDic[item.Key] != item.Value)|| GetMD5(Path.Combine(LocalFilePath, entity.FullName).Replace("\\", "/"))!= entity.MD5)
                {
                    
                    if (entity != null)
                    {
                        Debug.Log("发现资源更新：" + entity.FullName);
                        m_NeedDownloadDataList.Add(entity);
                    }
                }
            }

        }
        else
        {
            //如果本地没有版本文件 则初始资源就是需要下载的文件
            for (int i = 0; i < serverDownloadData.Count; i++)
            {
                if (serverDownloadData[i].IsFirstData)
                {
                    m_NeedDownloadDataList.Add(serverDownloadData[i]);
                }
            }

        }

        //拿到下载列表 m_NeedDownloadDataList 进行下载

        if (m_NeedDownloadDataList.Count == 0)
        {
            SetText(txtTips, "资源更新完毕");
            IsDown = false;
            EnterLogin();
            return;
        }
        foreach (var item in m_NeedDownloadDataList)
        { //下载资源
            string localFilePath = CreateFile(item);

            dataProcess.AddDownloadRes(Path.Combine(DownloadBaseUrl, platform, item.FullName.Replace("\\", "/")), localFilePath, () =>
            {
                ModifyLocalData(item);
                if (item.Equals(m_NeedDownloadDataList[m_NeedDownloadDataList.Count - 1]))
                {
                    IsDown = false;
                    EnterLogin();
                }
            }, isRe:true);
        }
        IsDown = true;
    }
    public void DownloadData(DownloadDataEntity entity, Action callback)
    {
        string localFilePath = CreateFile(entity);
        dataProcess.AddDownloadRes(Path.Combine(DownloadBaseUrl, platform, entity.FullName.Replace("\\", "/")), localFilePath, () =>
        {
            ModifyLocalData(entity);
            callback();
        });
    }
    public float GetDownUrlProgress(string url)
    {
        return dataProcess.GetDownUrlProgress(url);
    }
    /// <summary>
    /// 若没有该文件夹则创建
    /// </summary>
    public string CreateFile(DownloadDataEntity item)
    {
        int lastIndex = item.FullName.LastIndexOf('\\');
        string localFilePath = LocalFilePath;
        if (lastIndex > -1)
        {
            //短路径 用于创建文件夹
            string path = item.FullName.Substring(0, lastIndex);

            //得到本地路径
            localFilePath = (LocalFilePath + path).Replace("\\", "/");

            if (!Directory.Exists(localFilePath))
            {
                Directory.CreateDirectory(localFilePath);
            }
        }
        return localFilePath;
    }
    /// <summary>
    /// 根据资源名称 获取资源实体
    /// </summary>
    /// <param name="fullName"></param>
    /// <param name="lst"></param>
    /// <returns></returns>
    private DownloadDataEntity GetDownloadData(string fullName, List<DownloadDataEntity> lst)
    {
        for (int i = 0; i < lst.Count; i++)
        {
            if (lst[i].FullName.Equals(fullName, StringComparison.CurrentCultureIgnoreCase))
            {
                return lst[i];
            }
        }
        return null;
    }
    /// <summary>
    /// 修改本地文件
    /// </summary>
    /// <param name="entity"></param>
    public void ModifyLocalData(DownloadDataEntity entity)
    {
        if (m_LocalDataList == null)
        {
            m_LocalDataList = new List<DownloadDataEntity>();
        }
        bool isExists = false;

        for (int i = 0; i < m_LocalDataList.Count; i++)
        {
            if (m_LocalDataList[i].FullName.Equals(entity.FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                m_LocalDataList[i].MD5 = entity.MD5;
                m_LocalDataList[i].Size = entity.Size;
                m_LocalDataList[i].IsFirstData = entity.IsFirstData;
                isExists = true;
                break;
            }
        }

        if (!isExists)
        {
            m_LocalDataList.Add(entity);
        }

        SavaLoaclVersion();
    }
    /// <summary>
    /// 保存本地版本文件
    /// </summary>
    private void SavaLoaclVersion()
    {
        StringBuilder sbContent = new StringBuilder();

        for (int i = 0; i < m_LocalDataList.Count; i++)
        {
            sbContent.AppendLine(string.Format("{0} {1} {2} {3}", m_LocalDataList[i].FullName, m_LocalDataList[i].MD5, m_LocalDataList[i].Size, m_LocalDataList[i].IsFirstData ? 1 : 0));
        }

        IOUtil.CreateTextFile(m_LoaclVersionPath, sbContent.ToString());
    }
    /// <summary>
    /// 下载完毕初始化登录界面
    /// </summary>
    public void EnterLogin()
    {
        GameRoot.Instance.ResSvcInit();
        GameRoot.Instance.XLuaRootInit();
        LoginSys.instance.EnterStart();
        SetWndState(false);
    }

    public string GetPlatform()
    {
        return platform;
    }


    public string GetLocalFilePath()
    {
        return LocalFilePath;
    }
    /// <summary>
    /// 封装字典
    /// </summary>
    /// <param name="lst"></param>
    /// <returns></returns>
    public Dictionary<string, string> PackDownloadDataDic(List<DownloadDataEntity> lst)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();

        for (int i = 0; i < lst.Count; i++)
        {
            dic[lst[i].FullName] = lst[i].MD5;
        }

        return dic;
    }

    /// <summary>
    /// 封装字典
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public Dictionary<string, string> PackDownloadDataDic(string content)
    {
        Dictionary<string, string> dic = new Dictionary<string, string>();

        string[] arrLines = content.Split('\n');
        for (int i = 0; i < arrLines.Length; i++)
        {
            string[] arrData = arrLines[i].Split(' ');
            if (arrData.Length == 4)
            {
                dic[arrData[0]] = arrData[1];
            }
        }
        return dic;
    }

    /// <summary>
    /// 根据文本内容 封装下载数据列表
    /// </summary>
    /// <param name="content"></param>
    /// <returns></returns>
    public List<DownloadDataEntity> PackDownloadData(string content)
    {
        List<DownloadDataEntity> lst = new List<DownloadDataEntity>();

        string[] arrLines = content.Split('\n');
        for (int i = 0; i < arrLines.Length; i++)
        {
            string[] arrData = arrLines[i].Split(' ');
            if (arrData.Length == 4)
            {
                DownloadDataEntity entity = new DownloadDataEntity();
                entity.FullName = arrData[0];
                entity.MD5 = arrData[1];
                entity.Size = int.Parse(arrData[2]);
                entity.IsFirstData = int.Parse(arrData[3]) == 1;
                lst.Add(entity);
            }
        }

        return lst;
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