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



public class ABTools : EditorWindow
{

    private string[] platforms = { "PC", "iOS", "Android" };
    private int selectedPlatformIndex = 0;
    //private bool showDropdown = false;
    private string ServerIp = "ftp://120.46.164.125";
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
        if (GUILayout.Button("创建对比文件"))
        {
            BuildAllAssetBundles(selectedPlatformIndex);
            CreateABCompareFile();
        }
        if (GUILayout.Button("将选中的资源保存到Streaming"))
        {
            MoveABToStreamingAssetes();
        }
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
        //GUILayout.BeginHorizontal();
        //if (GUILayout.Button("重新上传失败文件"))
        //{
        //    ReUploadFailABFile(ServerIp);
        //}
        //GUILayout.EndHorizontal();
        //GUILayout.Space(10);
        //GUILayout.BeginHorizontal();
        //if (GUILayout.Button("重新上传失败文件"))
        //{
        //    ReUploadFailABFile(ServerIp);
        //}
        //GUILayout.EndHorizontal();
        GUILayout.EndArea();
    }
    static void BuildAllAssetBundles(int type)
    {
        BuildTarget buildTarget = BuildTarget.NoTarget;
        switch (type)
        {
            case 0:
                buildTarget = BuildTarget.StandaloneWindows;
                buildFile = "PC/";
                break;
            case 1:
                buildTarget = BuildTarget.iOS;
                buildFile = "iOS/";
                break;
            case 2:
                buildTarget = BuildTarget.Android;
                buildFile = "Android/";
                break;
        }
        string assetBundleDirectory = "Assets/AssetBundles/" + buildFile;
        if (!Directory.Exists(assetBundleDirectory))
        {
            Directory.CreateDirectory(assetBundleDirectory);
        }
        BuildPipeline.BuildAssetBundles(assetBundleDirectory,
                                        BuildAssetBundleOptions.ChunkBasedCompression,
                                        buildTarget);
        AssetDatabase.Refresh();
    }
    public static void CreateABCompareFile()
    {
        DirectoryInfo directory = Directory.CreateDirectory(Application.dataPath + "/AssetBundles/" + buildFile);
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
    private static void MoveABToStreamingAssetes()
    {
        UnityEngine.Object[] selectedAsset = Selection.GetFiltered(typeof(UnityEngine.Object), SelectionMode.DeepAssets);
        if (selectedAsset.Length == 0)
        {
            return;
        }
        foreach (UnityEngine.Object asset in selectedAsset)
        {
            string assetPath = AssetDatabase.GetAssetPath(asset);
            string fileName = assetPath.Substring(assetPath.LastIndexOf('/'));
            if (fileName.IndexOf('.') != -1)
            {
                continue;
            }
            AssetDatabase.CopyAsset(assetPath, Application.streamingAssetsPath + fileName);
        }
        DirectoryInfo directory = Directory.CreateDirectory(Application.streamingAssetsPath);
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
        abCompareInfo = abCompareInfo.Substring(0, abCompareInfo.Length - 1);
        File.WriteAllText(Application.streamingAssetsPath + "/abCompareInfo.txt", abCompareInfo);
        AssetDatabase.Refresh();
    }
    private static void UploadAllABFile(string ServerIp)
    {
        DirectoryInfo directory = Directory.CreateDirectory(Application.dataPath + "/AssetBundles/");
        //获取该目录下的所有文件信息
        ProcessFile(directory, ServerIp, "AssetBundles");
    }
    private static void ProcessFile(DirectoryInfo directory, string ServerIp,string initialPath)
    {
        FileSystemInfo[] fileInfos = directory.GetFileSystemInfos();
        //用于存储信息
        //string abCompareInfo = "";
        foreach (FileSystemInfo fileInfo in fileInfos)
        {
            if (fileInfo is DirectoryInfo subDirectory)
            {
                ProcessFile(subDirectory, ServerIp,fileInfo.Name);
            }
            else if (!fileInfo.Name.EndsWith(".meta")&&!fileInfo.Name.EndsWith(".manifest"))//判断是否有后缀
            {
                //Debug.Log(Path.Combine(initialPath, fileInfo.Name));
                UploaFile(fileInfo.FullName, Path.Combine( initialPath, fileInfo.Name), ServerIp);
            }
        }
    }

    /// <summary>
    /// 上传任务 
    /// </summary>                                             
    private async static void UploaFile(string filePath, string fileName, string ServerIp)
    {
        await _semaphore.WaitAsync(); // 等待一个可用的信号量
        await Task.Run(() =>
        {
            try
            {
                FtpWebRequest request = FtpWebRequest.Create(new Uri(ServerIp + "/morangABLoad/" + fileName)) as FtpWebRequest;
                //设置通信登录
                NetworkCredential credential = new NetworkCredential("120_46_164_125", "M8dTPGHs2SWLFdaz");
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
                    Debug.Log(filePath + "开始上传中");
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
            }
            catch
            {
                Debug.Log(filePath + "上传失败");
                //ListDic.Add(filePath, fileName);
            }
            finally { _semaphore.Release(); }
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
}