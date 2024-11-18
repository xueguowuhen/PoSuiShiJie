/****************************************************
    文件：ABTools.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/11 16:16:58
	功能：AB包工具
*****************************************************/

using UnityEditor;
using UnityEngine;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Net;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Threading;
using UnityEditor.PackageManager.Requests;



public class ABTools : EditorWindow
{

    private static string[] platforms = { "Windows", "iOS", "Android" };
    private static int selectedPlatformIndex = 0;
    //private bool showDropdown = false;
    private string ServerIp = "ftp://121.40.86.210:21";
    private static string buildFile;
    private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(3); // 最多同时上传3个文件
    //private static Dictionary<string, string> ListDic = new Dictionary<string, string>();
    [MenuItem("AB包工具/AB包处理")]
    private static void OpenWindow()
    {
        // 创建一个ABTools窗口
        ABTools window = GetWindow<ABTools>("ABTools");
        window.minSize = new Vector2(300, 200);
        window.Show();
    }
    private void OnGUI()
    {
        GUILayout.BeginArea(new Rect(10, 10, 300, 200));

        // 平台选择
        GUILayout.BeginHorizontal();
        GUILayout.Label("平台选择:");
        selectedPlatformIndex = EditorGUILayout.Popup(selectedPlatformIndex, platforms);
        GUILayout.EndHorizontal();
        // 添加一个空白间隔
        GUILayout.Space(10);
        // 资源服务器输入框
        GUILayout.BeginHorizontal();
        GUILayout.Label("资源服务器:");
        ServerIp = GUILayout.TextField(ServerIp, GUILayout.Width(220));
        GUILayout.EndHorizontal();
        // 添加一个空白间隔
        GUILayout.Space(10);
        // 资源服务器输入框
        GUILayout.BeginHorizontal();
        //if (GUILayout.Button("创建对比文件"))
        //{
        //    BuildAllAssetBundles(selectedPlatformIndex);
        //    CreateABCompareFile();
        //}
        //if (GUILayout.Button("将选中的资源保存到Streaming"))
        //{
        //    MoveABToStreamingAssetes();
        //}
        GUILayout.EndHorizontal();
        // 添加一个空白间隔
        GUILayout.Space(10);
        // 资源服务器输入框
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("上传AB包与对比文件"))
        {
            UploadAllABFile(ServerIp);
        }
        GUILayout.EndHorizontal();
        GUILayout.BeginHorizontal();
        if (GUILayout.Button("重新上传失败文件"))
        {
            ReUploadFailABFile(ServerIp);
        }
        GUILayout.EndHorizontal();
        //GUILayout.Space(10);
        //GUILayout.BeginHorizontal();
        //if (GUILayout.Button("重新上传失败文件"))
        //{
        //    ReUploadFailABFile(ServerIp);
        //}
        //GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }

    public static void CreateABCompareFile()
    {
        DirectoryInfo directory = Directory.CreateDirectory(Application.dataPath + "/AssetBundles/" + platforms[selectedPlatformIndex]);
        //获取该目录下的所有文件信息
        FileInfo[] fileInfos = directory.GetFiles();
        //用于存储信息
        string abCompareInfo = "";
        foreach (FileInfo fileInfo in fileInfos)
        {
            if (fileInfo.Extension == "")//判断是否有后缀
            {
                //拼接一个AB包的信息
                abCompareInfo += fileInfo.Name + "#" + fileInfo.Length + "#" + GetMD5(fileInfo.FullName);
                //分隔不同文件信息
                abCompareInfo += "|";
            }
            //Debug.Log("文件名"+fileInfo.Name);
            //Debug.Log("文件后缀" + fileInfo.Extension);
            //Debug.Log("文件后缀" + fileInfo.Length);
        }
        //删除最后的|
        if (abCompareInfo.Length > 0)
        {
            abCompareInfo = abCompareInfo.Substring(0, abCompareInfo.Length - 1);
        }
        File.WriteAllText(Application.dataPath + "/AssetBundles/" + buildFile + "abCompareInfo.txt", abCompareInfo);
        AssetDatabase.Refresh();
        Debug.Log("AB包对比文件生成成功");
    }

    private static void UploadAllABFile(string ServerIp)
    {
        failFiles.Clear();
        DirectoryInfo directory = Directory.CreateDirectory(Application.dataPath + "/../AssetBundles/");
        // 先创建所有目录，然后再上传文件
        ProcessFile(directory, ServerIp, "AssetBundles");
    }
    private static void ReUploadFailABFile(string ServerIp)
    {

        // 先创建所有目录，然后再上传文件
        foreach(var file in failFiles)
        {
            UploadFile(file.filePath, file.fileName, ServerIp);
        }
        failFiles.Clear();
    }
    private static void ProcessFile(DirectoryInfo directory, string ServerIp, string initialPath)
    {

        FileSystemInfo[] fileInfos = directory.GetFileSystemInfos();
        // Step 1: 创建远程目录
        foreach (FileSystemInfo fileInfo in fileInfos)
        {
            if (fileInfo is DirectoryInfo subDirectory)
            {
                // 同步创建远程目录
                CreateDirectoryOnServer(ServerIp, Path.Combine(initialPath, subDirectory.Name));
                // 递归处理子目录
                ProcessFile(subDirectory, ServerIp, Path.Combine(initialPath, subDirectory.Name));
            }
        }

        // Step 2: 上传文件
        foreach (FileSystemInfo fileInfo in fileInfos)
        {
            if (fileInfo is DirectoryInfo)
            {
                // 目录已经创建，不需要上传
                continue;
            }
            else if (!fileInfo.Name.EndsWith(".meta") && !fileInfo.Name.EndsWith(".manifest")) // 判断文件后缀
            {
                // 上传文件
                UploadFile(fileInfo.FullName, Path.Combine(initialPath, fileInfo.Name), ServerIp);
    
            }
        }
    }


    private async static void CreateDirectoryOnServer(string ServerIp, string directoryPath)
    {
        await _semaphore.WaitAsync(); // 等待一个可用的信号量
        await Task.Run(() =>
        {
            try
            {
                try
                {
                    FtpWebRequest request = FtpWebRequest.Create(new Uri(ServerIp + "/morangABLoad/" + directoryPath)) as FtpWebRequest;
                    request.Method = WebRequestMethods.Ftp.MakeDirectory;
                    NetworkCredential credential = new NetworkCredential("121_40_86_210", "QS68h38PA7NWsB7h");
                    request.Credentials = credential;
                    request.Proxy = null;
                    request.KeepAlive = false;
                    using (var response = (FtpWebResponse)request.GetResponse())
                    {
                        Debug.Log("目录创建成功：" + directoryPath);
                    }
                }
                catch (WebException ex)
                {
                    if (ex.Response is FtpWebResponse response && response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        Debug.Log("目录已存在：" + directoryPath);
                    }
                    else
                    {
                        Debug.LogError("目录创建失败：" + ex.Message);
                    }
                }
            }
            finally
            {
                _semaphore.Release(); // 释放信号量
            }
        });
    }

    private static HashSet<string> uploadedFiles = new HashSet<string>();
    private static List<DownFailAB> failFiles = new List<DownFailAB>();
    private async static void UploadFile(string filePath, string fileName, string ServerIp,int count=0)
    {
        if (count >= 2)
        {
            failFiles.Add(new DownFailAB() { filePath = filePath, fileName = fileName });
            Debug.LogError(filePath + "上传失败");
            return;
        }
        // 如果文件已经上传过，直接返回
        if (uploadedFiles.Contains(filePath))
        {
            Debug.Log($"{filePath} 已经上传，跳过此文件。");
            return;
        }
        await _semaphore.WaitAsync(); // 等待一个可用的信号量
        await Task.Run(() =>
        {
            try
            {
                FtpWebRequest request = FtpWebRequest.Create(new Uri(ServerIp + "/morangABLoad/" + fileName)) as FtpWebRequest;
                //设置通信登录
                NetworkCredential credential = new NetworkCredential("121_40_86_210", "QS68h38PA7NWsB7h");
                request.Credentials = credential;
                //请求代理
                request.Proxy = null;
                //请求完毕，是否关闭连接
                request.KeepAlive = false;
                //请求上传
                request.Method = WebRequestMethods.Ftp.UploadFile;
                request.UseBinary = true;
                //获取流对象
                Stream stream = request.GetRequestStream();

                using (FileStream file = File.OpenRead(filePath))
                {
                    //上传内容
                    byte[] bytes = new byte[2048];
                  //  Debug.Log(filePath + "开始上传中");
                    int contentLength = file.Read(bytes, 0, bytes.Length);
                    while (contentLength != 0)
                    {
                        stream.Write(bytes, 0, contentLength);
                        contentLength = file.Read(bytes, 0, bytes.Length);
                    }
                    file.Close();
                    stream.Close();
                }
                Debug.Log(filePath + "上传成功");
                lock (uploadedFiles) // 保证线程安全地更新已上传文件列表
                {
                    uploadedFiles.Add(filePath);
                }
                request.Abort();
            }
            catch
            {
                Debug.LogError(filePath + "上传失败");

                UploadFile(filePath, fileName, ServerIp, count + 1);
                
                //ListDic.Add(filePath, fileName);
            }
            finally { 

                _semaphore.Release(); }
        });

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
    public class DownFailAB
    {
        public string filePath;
        public string fileName;
    }
}