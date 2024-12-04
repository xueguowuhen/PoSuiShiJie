/****************************************************
    文件：AssetBundleWindow.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/7/14 16:34:6
	功能：Nothing
*****************************************************/

using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEditor;
using UnityEngine;
/// <summary>
/// AssetBundle管理窗口
/// </summary>
public class AssetBundleWindow : EditorWindow
{
    private AssetBundleDAL dal;
    private List<AssetBundleEntity> m_lst;
    private Dictionary<string, bool> m_dic;
    private int TagIndex
    {
        get { return tagIndex; }
        set
        {
            if (tagIndex != value)
            {
                switch (value)
                {

                    case 0:
                        foreach (AssetBundleEntity entity in m_lst)
                        {
                            m_dic[entity.Key] = true;
                        }
                        break;
                    case 1://Scene
                        foreach (AssetBundleEntity entity in m_lst)
                        {
                            m_dic[entity.Key] = entity.Tag.Equals("Scene", StringComparison.CurrentCultureIgnoreCase);
                        }
                        break;
                    case 2://人物
                        foreach (AssetBundleEntity entity in m_lst)
                        {
                            m_dic[entity.Key] = entity.Tag.Equals("Role", StringComparison.CurrentCultureIgnoreCase);
                        }
                        break;
                    case 3://特效
                        foreach (AssetBundleEntity entity in m_lst)
                        {
                            m_dic[entity.Key] = entity.Tag.Equals("Effect", StringComparison.CurrentCultureIgnoreCase);
                        }
                        break;
                    case 4://声音
                        foreach (AssetBundleEntity entity in m_lst)
                        {
                            m_dic[entity.Key] = entity.Tag.Equals("Audio", StringComparison.CurrentCultureIgnoreCase);
                        }
                        break;
                    case 5://None
                        foreach (AssetBundleEntity entity in m_lst)
                        {
                            m_dic[entity.Key] = false;
                        }
                        break;
                }
            }
            tagIndex = value;
        }
    }
    //生成目标平台
    private int tagIndex = 0;//标记的索引
    private string[] arrBuildTarget = { "Windows", "Android", "iOS" };
    private int selectBuildTargetIndex = -1;
#if UNITY_STANDALONE_WIN
    private BuildTarget target = BuildTarget.StandaloneWindows;
    private int buildTargetIndex = 0;
#elif UNITY_ANDROID
    private BuildTarget target = BuildTarget.Android;
    private int buildTargetIndex = 1;
#elif UNITY_IPHONE
    private BuildTarget target = BuildTarget.iOS;
    private int buildTargetIndex = 2;
#endif
    Vector2 pos;
    private void OnEnable()
    {
        string xmlPath = Application.dataPath + @"\Editor\AssetBundle\AssetBundleConfig.xml";
        AssetBundleDAL dal = new AssetBundleDAL(xmlPath);
        m_lst = dal.GetList();
        m_dic = new Dictionary<string, bool>();
        for (int i = 0; i < m_lst.Count; i++)
        {
            m_dic[m_lst[i].Key] = true;
        }
    }
    /// <summary>
    /// 绘制窗口
    /// </summary>
    private void OnGUI()
    {
        if (m_lst == null) return;
        #region  按钮行
        GUILayout.BeginHorizontal("box");
        TagIndex = EditorGUILayout.Popup(TagIndex, AssetBundleSetting.arrTag, GUILayout.Width(100));
        selectBuildTargetIndex = EditorGUILayout.Popup(buildTargetIndex, arrBuildTarget, GUILayout.Width(100));
        if (selectBuildTargetIndex != buildTargetIndex)
        {
            buildTargetIndex = selectBuildTargetIndex;
            EditorApplication.delayCall = OnSelectTargetCallBack;
        }
        if (GUILayout.Button("清空AB包标签", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnClearAssetBundleName;
        }
        if (GUILayout.Button("保存设置", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnSaveAssetBundleCallBack;
        }

        if (GUILayout.Button("清空AssetBundle包", GUILayout.Width(130)))
        {
            EditorApplication.delayCall = OnClearAssetBundleCallBack;
        }
        if (GUILayout.Button("打AssetBundle包", GUILayout.Width(130)))
        {
            EditorApplication.delayCall = OnAssetBundleCallBack;
        }

        if (GUILayout.Button("拷贝数据", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnCopyDataTableBundleName;
        }
        if (GUILayout.Button("生成版本文件", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnCreateVersionFileCallBack; ;
        }
        EditorGUILayout.Space();
        GUILayout.EndHorizontal();
        #endregion

        GUILayout.BeginHorizontal("box");
        GUILayout.Label("包名");
        GUILayout.Label("标记", GUILayout.Width(100));
        //GUILayout.Label("保存路径", GUILayout.Width(200));
        GUILayout.Label("文件夹", GUILayout.Width(210));
        GUILayout.Label("初始资源", GUILayout.Width(210));
        GUILayout.EndHorizontal();

        GUILayout.BeginVertical();
        pos = EditorGUILayout.BeginScrollView(pos);
        for (int i = 0; i < m_lst.Count; i++)
        {
            AssetBundleEntity entity = m_lst[i];
            GUILayout.BeginHorizontal("box");
            m_dic[entity.Key] = GUILayout.Toggle(m_dic[entity.Key], "", GUILayout.Width(100));
            GUILayout.Label(entity.Name);
            GUILayout.Label(entity.Tag, GUILayout.Width(100));
            GUILayout.Label(entity.IsFolder.ToString(), GUILayout.Width(200));
            GUILayout.Label(entity.IsFirstData.ToString(), GUILayout.Width(200));
            //GUILayout.Label(entity.Size.ToString(), GUILayout.Width(100));
            GUILayout.EndHorizontal();
            foreach (string path in entity.PathList)
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Space(40);
                GUILayout.Label(path);
                GUILayout.EndHorizontal();
            }
        }
        EditorGUILayout.EndScrollView();
        GUILayout.EndVertical();
    }
    /// <summary>
    /// 选定Target回调
    /// </summary>
    private void OnSelectTargetCallBack()
    {
        switch (buildTargetIndex)
        {
            case 0:
                target = BuildTarget.StandaloneWindows;
                break;
            case 1:
                target = BuildTarget.Android;
                break;
            case 2:
                target = BuildTarget.iOS;
                break;
        }
    }
    /// <summary>
    /// 保存设置
    /// </summary>
    private void OnSaveAssetBundleCallBack()
    {
        //需要打包的对象
        List<AssetBundleEntity> lst = new List<AssetBundleEntity>();
        foreach (AssetBundleEntity entity in m_lst)
        {
            if (m_dic[entity.Key])
            {
                entity.IsChecked = true;
                lst.Add(entity);
            }
            else
            {
                entity.IsChecked = false;
                lst.Add(entity);
            }
        }
        //循环设置文件夹 包括子文件夹的项
        for (int i = 0; i < lst.Count; i++)
        {
            AssetBundleEntity entity = lst[i];
            if (entity.IsFolder)
            {
                //如果这个节点配置的是文件夹，则需要进行遍历
                //需要把路径变成绝对路径
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                }
                SaveFolderSettings(folderArr, !entity.IsChecked);
            }
            else
            {
                //如果不是文件夹，只需要设置项
                string[] folderArr = new string[entity.PathList.Count];
                for (int j = 0; j < entity.PathList.Count; j++)
                {
                    folderArr[j] = Application.dataPath + "/" + entity.PathList[j];
                    SaveFileSetting(folderArr[j], !entity.IsChecked);
                }
            }
        }
        Debug.Log("打包完毕");
    }
    private void SaveFolderSettings(string[] folderArr, bool isSetNull)
    {
        foreach (string folderPath in folderArr)
        {
            //1.先看这个文件夹下的文件
            string[] arrFile = Directory.GetFiles(folderPath);//文件夹下的文件
            //2.对文件进行设置
            foreach (string filePath in arrFile)
            {
                //进行设置
                SaveFileSetting(filePath, isSetNull);
            }
            //3.看这个文件夹下的子文件夹
            string[] arrFolder = Directory.GetDirectories(folderPath);//文件夹下的文件夹
            SaveFolderSettings(arrFolder, isSetNull);

        }
    }
    private void SaveFileSetting(string filePath, bool isSetNull)
    {
        FileInfo file = new FileInfo(filePath);
        //忽略后缀带有.meta的文件
        if (!file.Extension.Equals(".meta", StringComparison.CurrentCultureIgnoreCase))
        {
            //文件都在Assets下面,可以获取文件的相对位置.Assets前面的内容则截取掉
            int index = filePath.IndexOf("Assets/", StringComparison.CurrentCultureIgnoreCase);
            string newPath = filePath.Substring(index);
            //替换文件名和后缀为空
            string fileName = newPath.Replace("Assets/", "").Replace(file.Extension, "");
            //获取文件后缀
            string variant = file.Extension.Equals(".unity", StringComparison.CurrentCultureIgnoreCase) ? "unity3d" : "assetbundle";
            //获取指定资源的导入器
            AssetImporter importer = AssetImporter.GetAtPath(newPath);
            //设置资源的 AssetBundle 名称和变体。将文件进行标记
            importer.SetAssetBundleNameAndVariant(fileName, variant);
            if (isSetNull)
            {
                //该资源被设置为空，表示无需打包
                importer.SetAssetBundleNameAndVariant(null, null);
            }
            //保存并重新导入
            importer.SaveAndReimport();
        }
    }
    /// <summary>
    /// 打包回调
    /// </summary>
    private void OnAssetBundleCallBack()
    {
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];//根据平台建立不同路径
        if (!Directory.Exists(toPath))
        {
            Directory.CreateDirectory(toPath);
        }
        //打包方法
        BuildPipeline.BuildAssetBundles(toPath, BuildAssetBundleOptions.None, target);
    }
    public void OnClearAssetBundleName()
    {
        // 获取项目中的所有资源文件
        string[] assetPaths = AssetDatabase.GetAllAssetPaths();

        foreach (string assetPath in assetPaths)
        {
            // 忽略.meta文件
            if (assetPath.EndsWith(".meta", StringComparison.CurrentCultureIgnoreCase))
            {
                continue;
            }

            // 获取指定资源的导入器
            AssetImporter importer = AssetImporter.GetAtPath(assetPath);
            if (importer != null)
            {
                // 清空 AssetBundle 名称和变体
                importer.SetAssetBundleNameAndVariant(null, null);

                // 打印调试信息，确认已清空该文件的 AssetBundle 名称
                Debug.Log($"已清空 AssetBundle 名称: {assetPath}");
            }
        }

        // 刷新 AssetDatabase，以确保所有更改被应用
        AssetDatabase.Refresh();

        Debug.Log("所有 AssetBundle 名称已清空");
    }
    /// <summary>
    /// 清空回调
    /// </summary>
    public void OnClearAssetBundleCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];
        if (Directory.Exists(path))
        {
            Directory.Delete(path, true);
        }
        Debug.Log("清空完毕");
    }
    public void OnCopyDataTableBundleName()
    {

        string fromPath = Application.dataPath + "/Download/ResCfg";
        string toPath = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex] + "/Download/ResCfg";
        if (Directory.Exists(toPath))
        {
            Directory.Delete(toPath, true);
        }
        IOUtil.CopyDirectory(fromPath, toPath);
        string fromPath2 = Application.dataPath + "/Download/xLuaLogic";
        string toPath2 = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex] + "/Download/xLuaLogic";
        if (Directory.Exists(toPath2))
        {
            Directory.Delete(toPath2, true);
        }
        IOUtil.CopyDirectory(fromPath2, toPath2);
        Debug.Log("拷贝完毕");
    }
    /// <summary>
    /// 生成版本文件
    /// </summary>
    private void OnCreateVersionFileCallBack()
    {
        string path = Application.dataPath + "/../AssetBundles/" + arrBuildTarget[buildTargetIndex];
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }

        string strVersionFilePath = path + "/VersionFile.txt"; //版本文件路径

        //如果版本文件存在 则删除
        IOUtil.DeleteFile(strVersionFilePath);

        StringBuilder sbContent = new StringBuilder();

        DirectoryInfo directory = new DirectoryInfo(path);

        //拿到文件夹下所有文件
        FileInfo[] arrFiles = directory.GetFiles("*", SearchOption.AllDirectories);

        for (int i = 0; i < arrFiles.Length; i++)
        {
            FileInfo file = arrFiles[i];
            string fullName = file.FullName; //全名 包含路径扩展名

            //相对路径
            string name = fullName.Substring(fullName.IndexOf(arrBuildTarget[buildTargetIndex]) + arrBuildTarget[buildTargetIndex].Length + 1);
            //if(name.Equals(arrBuildTarget[buildTargetIndex], StringComparison.CurrentCultureIgnoreCase))
            //{
            //    continue;
            //}
            string md5 = GetMD5(fullName); //文件的MD5
            if (md5 == null) continue;

            string size = Math.Ceiling(file.Length / 1024f).ToString(); //文件大小

            bool isFirstData = false; //是否初始数据
            bool isBreak = false;

            for (int j = 0; j < m_lst.Count; j++)//遍历配置列表
            {
                foreach (string xmlPath in m_lst[j].PathList)//遍历配置列表中的路径
                {
                    string tempPath = xmlPath;
                    if (xmlPath.IndexOf(".") != -1)//如果配置路径带有后缀
                    {
                        //如果配置路径带有后缀，则截取掉后缀
                        tempPath = xmlPath.Substring(0, xmlPath.IndexOf("."));
                        tempPath = tempPath.Replace("/", "\\");
                    }
                    if (name.IndexOf(tempPath, StringComparison.CurrentCultureIgnoreCase) != -1)
                    {
                        //如果文件名包含配置路径，则认为是初始数据
                        isFirstData = m_lst[j].IsFirstData;
                        //Debug.Log("该文件"+fullName+"是初始数据"+isFirstData);
                        isBreak = true;
                        break;
                    }
                }
                if (isBreak) break;
            }

            if (name.IndexOf("ResCfg") != -1 || name.IndexOf("xLuaLogic") != -1)
            {
                isFirstData = true;
            }
            if (name.Equals(arrBuildTarget[buildTargetIndex], StringComparison.CurrentCultureIgnoreCase))
            {
                isFirstData = true;
            }
            string strLine = string.Format("{0} {1} {2} {3}", name, md5, size, isFirstData ? 1 : 0);
            sbContent.AppendLine(strLine);
        }

        IOUtil.CreateTextFile(strVersionFilePath, sbContent.ToString());
        Debug.Log("创建版本文件成功");
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