/****************************************************
    文件：DataProcess.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-08 11:52:50
	功能：资源下载
*****************************************************/

using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Networking.UnityWebRequest;

public class DataProcess : MonoBehaviour
{
    public Action callback;
    private int maxDownloadNum = 3;//最大下载限制
    private int maxDownCount = 5;//最大重新下载次数
    private List<DataDownload> dataDownloads = new List<DataDownload>();
    private List<DataDownload> tmpDataDownloads = new List<DataDownload>();
    private List<DataDownload> FinishDownloads=new List<DataDownload>();
    public string DownSpeed;//下载速度
    public int EndDownCount = 0;//完成下载数
    public void Init()
    {

    }
    public void Update()
    {
        if (tmpDataDownloads.Count > 0)
        {
            for (int i = 0; i < tmpDataDownloads.Count; i++)
            {
                if (tmpDataDownloads[i].downloadCount >= maxDownCount)
                {
                    tmpDataDownloads.RemoveAt(i);
                    i--;
                    Debug.Log("请检查网络");
                    break;
                }
                if (dataDownloads.Count < maxDownloadNum)
                {
                    if (tmpDataDownloads[i].state == DownloadState.Shop) return;
                    dataDownloads.Add(tmpDataDownloads[i]);
                    DataDownload dataDownload = tmpDataDownloads[i];
                    dataDownload.state = DownloadState.Run;
                    dataDownload.coroutineFile = StartCoroutine(GetFileSize(dataDownload, dataDownload.uri, (string Bytes, long count) =>
                    {
                        dataDownload.countByteStr = count;
                        long countByte = count;
                        dataDownload.coroutineDown = StartCoroutine(DwnloadRes(dataDownload, dataDownload.request, dataDownload.fileName, dataDownload.savePath, countByte, dataDownload.action));
                    }));
                }
            }
            // 将tmpDataDownloads中被添加到dataDownloads中的元素删除掉
            tmpDataDownloads.RemoveAll(item => dataDownloads.Contains(item));
        }
    }
    /// <summary>
    /// 下载文件
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="fileName"></param>
    /// <param name="action"></param>
    public int AddDownloadRes(string uri, string savePath, Action action, bool isRe = false, string fileName = null)
    {
        UnityWebRequest request = Get(uri);
        int id = Taskid();
        if (fileName == null)//获取filename的后缀
        {
            fileName = HttpUtility.UrlDecode(Path.GetFileName(uri));
            fileName = GetFileNameFromUrl(fileName);
            Debug.Log(fileName);
        }
        tmpDataDownloads.Add(new DataDownload(id, uri, DownloadState.ilde, request, fileName, savePath, action, isRe, 0));
        return id;
    }
    /// <summary>
    /// 暂停该文件下载
    /// </summary>
    /// <param name="tid"></param>
    public void ShopDownloadRes(int tid)
    {
        bool isShop = false;
        for (int i = 0; i < dataDownloads.Count; i++)
        {
            if (dataDownloads[i].tid == tid)
            {
                tmpDataDownloads.Add(dataDownloads[i]);
                dataDownloads[i].state = DownloadState.Shop;
                dataDownloads[i].request.Abort();
                StopCoroutine(dataDownloads[i].coroutineFile);
                StopCoroutine(dataDownloads[i].coroutineDown);
                dataDownloads.RemoveAt(i);
                isShop = true;
                break;
            }

        }
        if (isShop)
        {
            Debug.Log("暂停成功");
        }
        else
        {
            Debug.Log("暂停失败");
        }
    }
    /// <summary>
    /// 恢复下载
    /// </summary>
    public void ReDownload(int tid)
    {
        bool isShop = false;
        for (int i = 0; i < tmpDataDownloads.Count; i++)
        {
            if (tmpDataDownloads[i].tid == tid)
            {
                if (dataDownloads.Count < maxDownloadNum)
                {
                    tmpDataDownloads[i].state = DownloadState.ilde;
                    tmpDataDownloads[i].request = Get(tmpDataDownloads[i].uri);
                    isShop = true;
                }
                break;
            }
        }
        if (isShop)
        {
            Debug.Log("恢复下载成功");
        }
        else
        {
            Debug.Log("恢复下载失败");
        }
    }
    #region 下载请求
    IEnumerator GetFileSize(DataDownload dataDownload, string url, Action<string, long> callback)
    {
        UnityWebRequest request = Head(url);
        yield return request.SendWebRequest();

        if (request.result == Result.Success)
        {
            string contentLength = request.GetResponseHeader("Content-Length");
            long fileSize;
            string Unit = "KB";
            if (long.TryParse(contentLength, out fileSize))
            {
                long count = fileSize;
                fileSize = fileSize / 1024;
                if (fileSize >= 1024) // 如果速度超过1KB/s
                {
                    fileSize = fileSize / 1024;
                    Unit = "MB";
                }
                Unit = fileSize + Unit;
                DownSpeed = fileSize + Unit;
                callback(Unit, count);
            }
            else
            {
                Debug.LogError("Failed to parse Content-Length header.");
            }
        }
        else
        {
            Debug.LogError("HEAD request failed: " + request.error);
            dataDownload.state = DownloadState.ilde;
            dataDownloads.Remove(dataDownload);
            tmpDataDownloads.Add(dataDownload);
            dataDownload.request = Get(dataDownload.uri);
            dataDownload.downloadCount++;
            yield break;
        }
    }

    IEnumerator DwnloadRes(DataDownload dataDownload, UnityWebRequest webRequest, string fileName, string savePath, long count, Action action)
    {
        // 获取已下载的字节大小
        long prevDownloadedBytes = GetSavedDownloadedBytes(fileName, savePath);
        // 设置请求头部，指定从已下载的字节处开始下载
        if (dataDownload.isRe)
            prevDownloadedBytes = 0;
        webRequest.SetRequestHeader("Range", "bytes=" + prevDownloadedBytes + "-");
        // 设置DownloadHandlerFile，指定保存路径
        if (prevDownloadedBytes != 0)
        {
            webRequest.downloadHandler = new DownloadHandlerFile(savePath + "/" + fileName, true);
        }
        else
        {
            webRequest.downloadHandler = new DownloadHandlerFile(savePath + "/" + fileName, false);
        }
        // 如果字节数下载满了，终止协程
        if (count == prevDownloadedBytes && prevDownloadedBytes != 0)
        {
            float progress = (float)GetSavedDownloadedBytes(fileName, savePath) / count;
           // Debug.LogFormat("Downloading {0}: {1:P1})", fileName, progress);
            dataDownloads.Remove(dataDownload);
            FinishDownloads.Add(dataDownload);
            dataDownload.state = DownloadState.Finish;
            webRequest.Dispose();
            action();
            yield break;
        }
        webRequest.SendWebRequest();
        float prevTime = 0;
        float prevSize = 0;
        while (!webRequest.isDone)
        {
            float progress = (float)GetSavedDownloadedBytes(fileName, savePath) / count;
            float currentTime = Time.time;
            float deltaTime = currentTime - prevTime; //当前时间减去上次时间
            dataDownload.currentCount = prevDownloadedBytes + (long)webRequest.downloadedBytes;
            //Debug.Log(prevDownloadedBytes + (long)webRequest.downloadedBytes);
            float downloadedBytes = webRequest.downloadedBytes;

            float speed = dataDownload.speed = ((downloadedBytes - prevSize) / deltaTime) / 1024;//当前进度-上次进度
            string speedUnit = "KB/s";
            if (speed >= 1024) // 如果速度超过1MB/s
            {
                speed = speed / 1024; // 转换为MB/s
                speedUnit = "MB/s";
            }
            DownSpeed = speed + speedUnit;
            //Debug.LogFormat("Downloading {0}: {1:P1} ({2:0.00} {3})", fileName, progress, speed, speedUnit);

            prevSize = downloadedBytes;
            prevTime = currentTime;
            yield return new WaitForSeconds(1f);
        }
        if (webRequest.isDone)
        {
            float progress = (float)GetSavedDownloadedBytes(fileName, savePath) / count;
            float currentTime = Time.time;
            float deltaTime = currentTime - prevTime; //当前时间减去上次时间
            dataDownload.currentCount = prevDownloadedBytes + (long)webRequest.downloadedBytes;
            float downloadedBytes =  webRequest.downloadedBytes;
            float speed = dataDownload.speed = ((downloadedBytes - prevSize) / deltaTime) / 1024;//当前进度-上次进度
            string speedUnit = "KB/s";
            if (speed >= 1024) // 如果速度超过1MB/s
            {
                speed = speed / 1024; // 转换为MB/s
                speedUnit = "MB/s";
            }
            DownSpeed = speed + speedUnit;
           // Debug.LogFormat("Downloading {0}: {1:P1} ({2:0.00} {3})", fileName, progress, speed, speedUnit);
            dataDownloads.Remove(dataDownload);
            FinishDownloads.Add(dataDownload);
            dataDownload.state = DownloadState.Finish;
            EndDownCount += 1;
            action();
        }

        if (webRequest.result == Result.ConnectionError)
        {
            Debug.Log("DownLoad Error:" + webRequest.error);
            dataDownload.state = DownloadState.ilde;
            dataDownload.request = Get(dataDownload.uri);
            dataDownloads.Remove(dataDownload);
            tmpDataDownloads.Add(dataDownload);
            dataDownload.downloadCount++;
            webRequest.Dispose();
            yield break;
        }

        // 确保 webRequest 在协程结束后被释放
        webRequest.Dispose();

    }
    #endregion
    /// <summary>
    /// 获取已下载的字节大小
    /// </summary>
    /// <param name="fileName"></param>
    /// <param name="savePath"></param>
    /// <returns></returns>
    private long GetSavedDownloadedBytes(string fileName, string savePath)
    {
        string filePath = Path.Combine(savePath, fileName);
        if (File.Exists(filePath))
        {
            FileInfo fileInfo = new FileInfo(filePath);
            return fileInfo.Length;
        }
        return 0;
    }
    /// <summary>
    /// 下载保存
    /// </summary>
    private void DownloadSave(byte[] res, string savePath, string fileName, Action action)
    {
        if (!File.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        string path = savePath + "/" + fileName;
        FileInfo file = new FileInfo(path);
        Stream sw;
        sw = file.Create();
        sw.Write(res, 0, res.Length);
        sw.Close();
        sw.Dispose();
        action();
    }
    public static string GetFileNameFromUrl(string url)
    {
        // 匹配URL中的文件名部分
        Match match = Regex.Match(url, @"([^/?.]+)\.(\w+)(\?\S+)?$", RegexOptions.IgnoreCase);

        if (match.Success)
        {
            // 获取匹配到的文件名和后缀
            string fileName = match.Groups[1].Value;
            string fileExtension = match.Groups[2].Value;

            return fileName + "." + fileExtension;
        }

        return url;
    }
    private int tid = 0;
    private int Taskid()
    {
        tid++;
        return tid;
    }
    public void Clear()
    {
        tid = 0;
        dataDownloads.Clear();
        tmpDataDownloads.Clear();
        FinishDownloads.Clear();
    }
    public void StopAllDownloads()
    {
        foreach (var dataDownload in dataDownloads)
        {
            dataDownload.request.Abort(); // 终止下载请求
        }
        dataDownloads.Clear(); // 清空下载列表
    }
    /// <summary>
    /// 获取总下载字节数
    /// </summary>
    /// <returns></returns>
    public long TotalByte()
    {
        long total = 0;
        foreach (var dataDownload in dataDownloads)
        {
            total +=dataDownload.countByteStr;
        }
        foreach (var dataDownload in FinishDownloads)
        {
            total += dataDownload.countByteStr;
        }
        return total;
    }
    /// <summary>
    /// 获取已下载字节数
    /// </summary>
    /// <returns></returns>
    public long TotalDownByte()
    {
        long total = 0;
        foreach (var dataDownload in dataDownloads)
        {
            total += (long)dataDownload.currentCount;
        }
        foreach (var dataDownload in FinishDownloads)
        {
            total += dataDownload.countByteStr;
        }
        return total;
    }
    /// <summary>
    /// 获取总下载速度
    /// </summary>
    /// <returns></returns>
    public float GetTotalSpeed()
    {
        float total = 0;
        foreach (var dataDownload in dataDownloads)
        {
            total += dataDownload.speed;
        }
        return total;
    }
    public int GetFinishCount()
    {
        return FinishDownloads.Count;
    }
}
public class DataDownload
{
    public int tid;//任务ID
    public string uri;
    public UnityWebRequest request;//下载链接
    public string fileName;
    public string savePath;//保存路径
    public Action action;//回调函数
    public bool isRe;//是否重新下载
    public long countByteStr;//总字节数
    public int downloadCount;//下载次数
    public float speed;
    public float currentCount;//当前字节数
    public DownloadState state;
    public Coroutine coroutineFile;
    public Coroutine coroutineDown;
    public DataDownload(int tid, string uri, DownloadState state, UnityWebRequest request, string fileName, string savePath, Action action, bool isRe, int downloadCount)
    {
        this.tid = tid;
        this.uri = uri;
        this.state = state;
        this.request = request;
        this.fileName = fileName;
        this.savePath = savePath;
        this.action = action;
        this.isRe = isRe;
        this.downloadCount = downloadCount;
    }
}
public enum DownloadState
{
    ilde,//等待时
    Run,//下载中
    Shop,//停止时
    Finish,//完成时
}