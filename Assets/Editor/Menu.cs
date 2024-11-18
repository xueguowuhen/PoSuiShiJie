/****************************************************
    文件：Menu.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/7/13 22:27:29
	功能：Nothing
*****************************************************/

using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class Menu
{
    [MenuItem("Tools/设置")]
    public static void Settings()
    {
        SettingWindow win = (SettingWindow)EditorWindow.GetWindow(typeof(SettingWindow));
        win.titleContent = new GUIContent("全局设置");
        win.Show();
    }
    [MenuItem("Tools/生成AB文件")]
    public static void AssetBundleConfig()
    {
        AssetBundleConfigWindow win = EditorWindow.GetWindow<AssetBundleConfigWindow>();
        win.titleContent = new GUIContent("生成AB包配置文件");
        win.Show();
    }
    [MenuItem("Tools/打包")]
    public static void AssetBundleCreate()
    {
        AssetBundleWindow win = EditorWindow.GetWindow<AssetBundleWindow>();
        win.titleContent = new GUIContent("AssetBundle打包");
        win.Show();
    }
    [MenuItem("Tools/拷贝到StreamingAssets")]
    public static void AssetBundleCopyToStreamingAssets()
    {
        string toPath = Application.streamingAssetsPath + "/AssetBundles";
        if (Directory.Exists(toPath))
        {
            Directory.Delete(toPath, true);
        }
        Directory.CreateDirectory(toPath);
        IOUtil.CopyDirectory(Application.persistentDataPath, toPath);
        AssetDatabase.Refresh();
    }
}