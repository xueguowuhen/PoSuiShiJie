/****************************************************
    文件：DowingWnd.cs
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
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class DowningWnd : WindowRoot
{
    public Text txtTips;
    public Text txtSpeed;//下载速度
    public Text txtProgress;//下载进度
    public Image imageFG;
    public Text txtPrg;
    private DataProcess dataProcess;
    private Dictionary<string, ABInfo> remoteABInfo = new Dictionary<string, ABInfo>();
    public Dictionary<string, ABInfo> tmpABInfo = new Dictionary<string, ABInfo>();
    private Dictionary<string, ABInfo> localABInfo = new Dictionary<string, ABInfo>();
    private Dictionary<string, long> localInfo = new Dictionary<string, long>();
    private List<string> downloadList = new List<string>();
    private bool IsDown = false;
    private bool ReDown = true;//判断是否需要重新下载ab包
    private string abCompareInfo_Cache;
    private string DownPath;
    private string platform;
    public Text text;
    //进入页面自动对比加载资源文件
    protected override void InitWnd()
    {
        base.InitWnd();
        SetGameObject();
        SetText(txtSpeed, "");
        SetText(txtProgress, "");
        imageFG.fillAmount = 0;
#if UNITY_EDITOR||UNITY_STANDALONE_WIN
        platform = PathDefine.PC;
#elif UNITY_ANDROID
        platform = PathDefine.Android;
#endif
        DownLoadAB();
    }
    public void Update()
    {
        if (IsDown)
        {
            float speed = dataProcess.GetTotalSpeed();
            string speedUnit = "KB/s";
            if (speed >= 1024) // 如果速度超过1MB/s
            {
                speed = speed / 1024; // 转换为MB/s
                speedUnit = "MB/s";
            }
            SetText(txtSpeed, "速度" + speed.ToString("F2") + speedUnit);
            SetText(txtProgress, "文件进度" + dataProcess.GetFinishCount() + "/" + downloadList.Count);
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
        dataProcess = gameObject.GetOrAddComponent<DataProcess>();
    }
    /// <summary>
    /// 下载AB包对比文件
    /// </summary>
    private void DownLoadAB()
    {
        //SetText(txtTips, "正在检查资源更新中...");
        SetText(txtTips, platform);
        DownPath = Path.Combine(Application.persistentDataPath , platform);
        dataProcess.AddDownloadRes(Path.Combine("http://120.46.164.125/morangABLoad/",platform,"abCompareInfo.txt"), DownPath, () =>
        {
            ABCompare();
            //text.text = "下载成功";
        }, true, PathDefine.abCompareInfo_TMP);
    }
    /// <summary>
    /// 进行AB包资源对比
    /// </summary>
    private void ABCompare()
    {
        SetText(txtTips, "正在更新资源中...");

        string abCompareInfo_TMP = Path.Combine( DownPath , PathDefine.abCompareInfo_TMP);
        abCompareInfo_Cache = Path.Combine(DownPath,  PathDefine.abCompareInfo_Cache);
        string abCompareInfo = Path.Combine(DownPath,  PathDefine.abCompareInfo);
        // 校验缓存MD5
        CheckCacheMd5(abCompareInfo_Cache, abCompareInfo_TMP);
        // 加载对比文件
        LoadCompareFiles(abCompareInfo, abCompareInfo_TMP);
        //校验本地ab包资源是否下载完整
        DirectoryInfo directoryInfo = Directory.CreateDirectory(DownPath);
        //获取该目录下的所有文件信息
        FileInfo[] fileInfos = directoryInfo.GetFiles();
        //用于存储信息
        foreach (FileInfo fileInfo in fileInfos)
        {
            if (fileInfo.Extension == "")//判断是否有后缀
            {
                //拼接一个AB包的信息
                localInfo.Add(fileInfo.Name, fileInfo.Length);
            }
        }
        foreach (string abName in remoteABInfo.Keys)
        {
            if (!localABInfo.ContainsKey(abName))//只添加本地客户端没有的AB包资源
            {
                downloadList.Add(abName);
            }
            else
            {//判断相同的AB包资源的md5码是否相等,检验该资源是否需要下载更新
                if (localABInfo[abName].md5 != remoteABInfo[abName].md5)
                {

                    downloadList.Add(abName);
                }
                else if (localInfo.ContainsKey(abName))
                {
                    if (localInfo[abName] != remoteABInfo[abName].size)
                    {
                        downloadList.Add(abName);
                    }
                }
                localABInfo.Remove(abName);//将共有的AB包删除，留下本地独有的AB包
            }
            if (tmpABInfo.ContainsKey(abName))//在缓存列表中，且md5不等则默认以云端为主
            {
                if (tmpABInfo[abName].md5 != remoteABInfo[abName].md5)
                {
                    tmpABInfo.Remove(abName);
                }
            }
        }
        foreach (var abName in localABInfo.Keys)
        {//检测缓存中是否额外存在一份相同的AB包资源，存在则删除，避免出现资源重复
            if (File.Exists(Path.Combine( DownPath + abName)))
            {
                File.Delete(Path.Combine(DownPath + abName));
            }
        }
        dataProcess.Clear();
        if (downloadList.Count <= 0)
        {
            EnterLogin();
            return;
        }
        foreach (var item in downloadList)
        {

            dataProcess.AddDownloadRes(Path.Combine("http://120.46.164.125/morangABLoad/", platform , item), DownPath, () =>
            {
                if (item.Equals(downloadList[downloadList.Count - 1]))
                {
                    File.WriteAllText(abCompareInfo, File.ReadAllText(abCompareInfo_TMP));
                    File.Delete(abCompareInfo_TMP);
                    File.Delete(abCompareInfo_Cache);
                    IsDown = false;
                    EnterLogin();
                }
            }, ReDown);
        }
        IsDown = true;
    }
    private void CheckCacheMd5(string cachePath, string tmpPath)
    {
        if (File.Exists(cachePath) && File.Exists(tmpPath))
        {
            string cacheMd5 = ComputeMD5(cachePath);
            string tmpMd5 = ComputeMD5(tmpPath);
            if (cacheMd5 == tmpMd5)
            {
                ReDown = false;
            }
        }
    }
    private void LoadCompareFiles(string abCompareInfo, string abCompareInfo_TMP)
    {
        if (File.Exists(abCompareInfo_TMP))
        {
            GetCompareList(abCompareInfo_TMP, remoteABInfo);
            CopyCompare(abCompareInfo_TMP, abCompareInfo_Cache);
        }
        if (File.Exists(abCompareInfo))
        {
            GetCompareList(abCompareInfo, localABInfo);
            LoadTmpABInfo();
        }
        else
        {
            LoadLocalABInfo();
        }
       // text.text=tmpABInfo.Count.ToString();
    }
    private void LoadTmpABInfo()
    {
        string abCompareInfos = Path.Combine(Application.streamingAssetsPath, PathDefine.abCompareInfo);
#if UNITY_EDITOR||UNITY_STANDALONE_WIN
        GetCompareList(abCompareInfos, tmpABInfo);
#elif UNITY_ANDROID
       LoadABCompareInfoForAndroid(abCompareInfos, tmpABInfo);
#endif
    }

    private void LoadLocalABInfo()
    {
        string abCompareInfos = Path.Combine(Application.streamingAssetsPath, PathDefine.abCompareInfo);
#if UNITY_EDITOR||UNITY_STANDALONE_WIN
        GetCompareList(abCompareInfos, localABInfo);
#elif UNITY_ANDROID
        LoadABCompareInfoForAndroid(abCompareInfos, localABInfo);
#endif
        tmpABInfo = localABInfo;
    }

    public void EnterLogin()
    {
        LoginSys.instance.GameLogin();
        SetWndState(false);
    }
    public void LoadABCompareInfoForAndroid(string abCompareInfos, Dictionary<string, ABInfo> tepInfo)
    {
        using (UnityWebRequest request = UnityWebRequest.Get(abCompareInfos))
        {
            request.SendWebRequest();
            while (true)
            {
                if (request.downloadHandler.isDone)
                {
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.LogError($"Failed to load AB Compare Info: {request.error}");
                        
                    }
                    else
                    {
                        string abCompareInfosContent = request.downloadHandler.text;
                        GetCompareList(abCompareInfosContent, tepInfo, true);
                    }
                    break;
                }
            }

        }
    }

    /// <summary>
    /// 解析对比文件信息
    /// </summary>
    /// <param name="infoPath"></param>
    /// <param name="localABinfo"></param>
    public void GetCompareList(string infoPath, Dictionary<string, ABInfo> localABinfo, bool isRead = false)
    {
        string info;
        if (isRead)
        {
            info = infoPath;
        }
        else
        {
            info = File.ReadAllText(infoPath);
        }
        string[] sts = info.Split('|');
        string[] infos = null;
        localABinfo.Clear();
        for (int i = 0; i < sts.Length; i++)
        {
            infos = sts[i].Split('#');
            localABinfo.Add(infos[0], new ABInfo(infos[0], long.Parse(infos[1]), infos[2]));
        }

    }
    public void CopyCompare(string infoPath, string remotePath)
    {
        File.Copy(infoPath, remotePath, true);
    }
    /// <summary>
    /// 获取资源的md5码
    /// </summary>
    /// <param name="filePath"></param>
    /// <returns></returns>
    public static string ComputeMD5(string filePath)
    {
        using (var md5 = MD5.Create())
        {
            using (var stream = File.OpenRead(filePath))
            {
                byte[] hash = md5.ComputeHash(stream);
                return BitConverter.ToString(hash).Replace("-", "").ToLowerInvariant();
            }
        }
    }
    public string GetPlatform()
    {
        return platform;
    }
}