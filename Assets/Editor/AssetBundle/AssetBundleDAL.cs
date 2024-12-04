/****************************************************
    文件：AssetBundleDAL
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-07-13 23:59:44
	功能：Nothing
*****************************************************/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;

public class AssetBundleDAL
{
    /// <summary>
    /// xml路径
    /// </summary>
    private string m_Path;
    /// <summary>
    /// 返回的数据集合
    /// </summary>
    private List<AssetBundleEntity> m_List = null;

    public AssetBundleDAL(string path)
    {
        m_Path = path;
        m_List = new List<AssetBundleEntity>();
    }
    /// <summary>
    /// 返回xml数据
    /// </summary>
    /// <returns></returns>
    public List<AssetBundleEntity> GetList()
    {
        m_List.Clear();
        //读取Xml 把数据添加到m_List
        if (!File.Exists(m_Path))
        {
            CreateConfig();
        }
        XDocument xDoc = XDocument.Load(m_Path);
        XElement root = xDoc.Root;
        //获取到AssetBundle的节点
        XElement assetBunleNode = root.Element("AssetBundle");
        IEnumerable<XElement> lst = assetBunleNode.Elements("Item");
        int index = 0;
        foreach (XElement item in lst)
        {
            AssetBundleEntity entity = new AssetBundleEntity();
            entity.Key = "key" + ++index;
            entity.Name = item.Attribute("Name").Value;
            entity.Tag = item.Attribute("Tag").Value;
            entity.IsFolder = item.Attribute("IsFolder").Value.Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false;
            entity.IsFirstData = item.Attribute("IsFirstData").Value.Equals("True", StringComparison.OrdinalIgnoreCase) ? true : false;
            IEnumerable<XElement> pathList = item.Elements("Path");
            foreach (XElement path in pathList)
            {
                entity.PathList.Add(path.Attribute("Value").Value);
            }
            m_List.Add(entity);
        }
        return m_List;
    }
    public void CreateConfig()
    {
        XmlDocument doc = new XmlDocument();//创建XML文档对象
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null); doc.AppendChild(dec);
        XmlElement Root = doc.CreateElement("Root");                             //创建根节点，并添加到doc文档中
        doc.AppendChild(Root);
        XmlElement AssetBundle = doc.CreateElement("AssetBundle");
        Root.AppendChild(AssetBundle);
        doc.Save(m_Path);
        Debug.Log("保存成功");

    }

    public void UpdateConfig(List<AssetBundleEntity> lst)
    {
        XDocument xDoc = XDocument.Load(m_Path);
        if (xDoc == null)
        {
            CreateConfig();
        }
        XElement root = xDoc.Root;
        //获取到AssetBundle的节点
        XElement assetBunleNode = root.Element("AssetBundle");
        if (assetBunleNode == null)
        {
            assetBunleNode = new XElement("AssetBundle");
            root.Add(assetBunleNode);
        }
        // 删除所有Item节点，但保留AssetBundle节点
        assetBunleNode.Elements("Item").Remove();

        for (int i = 0; i < lst.Count; i++)
        {
            AssetBundleEntity entity = lst[i];
            XElement Item = new XElement("Item",
                     new XAttribute("Name", entity.Name),
                     new XAttribute("Tag", entity.Tag),
                     new XAttribute("IsFolder", entity.IsFolder),
                     new XAttribute("IsFirstData", entity.IsFirstData),
                     entity.PathList.Select(path => new XElement("Path",
        new XAttribute("Value", path)))
                 );
            assetBunleNode.Add(Item);
        }
        xDoc.Save(m_Path);
    }
}