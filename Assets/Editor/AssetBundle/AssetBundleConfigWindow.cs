/****************************************************
    文件：AssetBundleWindowConfig
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-07-30 20:31:32
	功能：Nothing
*****************************************************/

using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AssetBundleConfigWindow : EditorWindow
{
    private int tagIndex = 0;
    private List<AssetBundleEntity> m_lst;
    private Dictionary<string, bool> m_dic;
    private string[] m_strArr;
    Vector2 pos;
    private void OnEnable()
    {
        //读取数据
        string xmlPath = Application.dataPath + @"\Editor\AssetBundle\AssetBundleConfig.xml";
        AssetBundleDAL dal = new AssetBundleDAL(xmlPath);
        m_lst = dal.GetList();
        m_strArr = new string[m_lst.Count];
        m_dic = new Dictionary<string, bool>();
        for (int i = 0; i < m_lst.Count; i++)
        {
            m_strArr[i] = m_lst[i].Name;
            m_dic[m_lst[i].Name] = true;
        }

    }
    /// <summary>
    /// 绘制窗口
    /// </summary>
    private void OnGUI()
    {
        #region  按钮行
        GUILayout.BeginHorizontal("box");
        tagIndex = EditorGUILayout.Popup(tagIndex, m_strArr, GUILayout.Width(100));
        if (GUILayout.Button("添加新资源", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnSelectAddConfig;
        }
        if (GUILayout.Button("生成新Xml文件", GUILayout.Width(100)))
        {
            EditorApplication.delayCall = OnSelectUpdateConfig;
        }
        GUILayout.EndHorizontal();
        #endregion
        GUILayout.BeginHorizontal("box");
        GUILayout.Label("包名");
        //GUILayout.Label("标记", GUILayout.Width(100));
        GUILayout.Label("标记", GUILayout.Width(110));
        GUILayout.Label("是否文件夹", GUILayout.Width(130));
        GUILayout.Label("初始资源", GUILayout.Width(130));
        GUILayout.EndHorizontal();
        if (m_lst == null) return;

        GUILayout.BeginVertical();
        pos = EditorGUILayout.BeginScrollView(pos);
        for (int i = 0; i < m_lst.Count; i++)
        {
            AssetBundleEntity entity = m_lst[i];
            GUILayout.BeginHorizontal("box");
            m_dic[entity.Name] = GUILayout.Toggle(m_dic[entity.Name], "", GUILayout.Width(100));
            // 允许编辑 Name 和 Tag
            GUILayout.Label(entity.Name, GUILayout.Width(230));
            entity.Tag = GUILayout.TextField(entity.Tag, GUILayout.Width(100));
            GUILayout.Space(60);
            entity.IsFolder = GUILayout.Toggle(entity.IsFolder, "", GUILayout.Width(30));
            GUILayout.Space(100);
            entity.IsFirstData = GUILayout.Toggle(entity.IsFirstData, "", GUILayout.Width(100));
            //GUILayout.Label(entity.Size.ToString(), GUILayout.Width(100));
            GUILayout.EndHorizontal();
            foreach (string path in entity.PathList.ToList()) // 使用 ToList() 避免直接修改集合
            {
                GUILayout.BeginHorizontal("box");
                GUILayout.Space(40);
                GUILayout.Label(path);

                // 添加删除按钮
                if (GUILayout.Button("删除", GUILayout.Width(50)))
                {
                    entity.PathList.Remove(path); // 从 PathList 中移除路径
                    break; // 路径被删除后跳出当前循环
                }

                GUILayout.EndHorizontal();
            }

        }
        EditorGUILayout.EndScrollView();
        GUILayout.EndVertical();
    }
    /// <summary>
    /// 更新XML文件
    /// </summary>
    private void OnSelectUpdateConfig()
    {
        //需要更新的对象的对象
        List<AssetBundleEntity> lstNeedBuild = new List<AssetBundleEntity>();
        foreach (AssetBundleEntity entity in m_lst)
        {
            if (m_dic[entity.Name])
            {
                lstNeedBuild.Add(entity);
            }
        }
        //读取数据
        string xmlPath = Application.dataPath + @"\Editor\AssetBundle\AssetBundleConfig.xml";
        AssetBundleDAL dal = new AssetBundleDAL(xmlPath);
        dal.UpdateConfig(lstNeedBuild);
        Debug.Log("生成完毕");
    }

    /// <summary>
    ///  将选择的文件进行添加
    /// </summary>
    private void OnSelectAddConfig()
    {
        Object selectedObjects = Selection.activeObject;
        AssetBundleEntity bundleEntity = m_lst[tagIndex];
        if (selectedObjects == null)
        {
            Debug.Log("未选择任何资源");
            return;
        }
        if (bundleEntity.PathList.Contains(AssetDatabase.GetAssetPath(selectedObjects)))
        {
            Debug.Log("该资源已经存在");
            return;
        }
        string path = AssetDatabase.GetAssetPath(selectedObjects).Replace("Assets/", "");
        bundleEntity.PathList.Add(path);
    }

}
public struct AssetEntity
{
    public string path;
    public string name;
}