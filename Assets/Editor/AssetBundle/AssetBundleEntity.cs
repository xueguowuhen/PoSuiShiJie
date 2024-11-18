/****************************************************
    文件：AssetBundleEntity
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-07-13 23:53:36
	功能：Nothing
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/// <summary>
/// AssetBundle实体
/// </summary>
public class AssetBundleEntity
{
    /// <summary>
    /// 用于打包的时候选定 唯一的Key
    /// </summary>
    public string Key;
    /// <summary>
    /// 名称
    /// </summary>
    public string Name;
    /// <summary>
    /// 标记
    /// </summary>
    public string Tag;

    /// <summary>
    /// 是否是文件夹
    /// </summary>
    public bool IsFolder;
    /// <summary>
    /// 是否初始资源
    /// </summary>
    public bool IsFirstData;
    /// <summary>
    /// 是否被选中
    /// </summary>
    public bool IsChecked;
    private List<string> m_PathList = new List<string>();
    /// <summary>
    /// 路径集合
    /// </summary>
    public List<string> PathList
    {
        get { return m_PathList; }
    }

}