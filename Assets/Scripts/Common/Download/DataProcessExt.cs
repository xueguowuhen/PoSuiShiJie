/****************************************************
    文件：DataProcessExt
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-11-07 12:36:03
	功能：Nothing
*****************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static UnityEngine.Networking.UnityWebRequest;
using UnityEngine.Networking;
using UnityEngine;

public partial class DataProcess
{
    /// <summary>
    /// 添加下载版本
    /// </summary>
    /// <param name="uri"></param>
    /// <param name="action"></param>
    public void AddVersion(string uri, Action<string> action)
    {
        StartCoroutine(DownloadVersion(uri, action));

    }
    /// <summary>
    /// 下载文件但不保存到本地读取数据
    /// </summary>
    public IEnumerator DownloadVersion(string uri, Action<string> action)
    {
        UnityWebRequest request = Get(uri);
        request.SendWebRequest();
        float timeOut = Time.time;
        float progress = request.downloadProgress;
        //等待下载完成
        while (request != null && !request.isDone)
        {
            if (progress < request.downloadProgress)
            {
                timeOut = Time.time;
                progress = request.downloadProgress;
            }

            if ((Time.time - timeOut) > 5f)
            {
                Debug.Log("下载超时" + uri);
                yield break;
            }
        }
        yield return request;
        if (request.result == Result.Success)
        {
            action(request.downloadHandler.text);
        }
        else
        {
            Debug.Log("下载失败" + uri);
        }
    }
}
