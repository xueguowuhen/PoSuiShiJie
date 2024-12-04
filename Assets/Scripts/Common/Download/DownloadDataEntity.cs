using System;

/// <summary>
/// 下载数据的实体
/// </summary>
[Serializable]
public class DownloadDataEntity
{
    /// <summary>
    /// 资源的名称
    /// </summary>

    public string FullName;

    /// <summary>
    /// MD5
    /// </summary>
    public string MD5;

    /// <summary>
    /// 文件大小（K）
    /// </summary>
    public int Size;

    /// <summary>
    /// 是否初始数据
    /// </summary>
    public bool IsFirstData;
}