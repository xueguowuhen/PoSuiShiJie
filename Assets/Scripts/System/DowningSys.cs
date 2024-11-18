/****************************************************
    文件：DowningSys
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-11-07 21:29:47
	功能：下载系统
*****************************************************/
using CommonNet;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DowningSys : SystemRoot
{
    public static DowningSys instance;
    public DowningWnd downingWnd;
    private List<DownloadDataEntity> m_ServerDataList;
    private DataProcess dataProcess;
    public override void InitSyc()
    {
        base.InitSyc();
        instance = this;

        GameCommon.Log("DowningSys Init....");
    }
    public void EnterDowning()
    {
        //暂时不进入资源下载
        downingWnd.SetWndState();
    }
    public List<DownloadDataEntity> GetServerDataList()
    {
        return m_ServerDataList;
    }
    public void SetServerDataList(List<DownloadDataEntity> serverDataList)
    {
        m_ServerDataList = serverDataList;
    }
    public DataProcess GetDataProcess()
    {
        if (dataProcess == null)
        {
            dataProcess = gameObject.GetComponent<DataProcess>();
        }
        return dataProcess;
    }
    public string GetLocalFilePath()
    {
        return downingWnd.GetLocalFilePath();
    }
    public void DownloadData(DownloadDataEntity Entity,Action callback)
    {
         downingWnd.DownloadData(Entity, callback);
    }
    public string GetPlatform()
    {
        return downingWnd.GetPlatform();
    }
    /// <summary>
    /// 根据路径获取服务器端数据
    /// </summary>
    /// <param name="path"></param>
    /// <returns></returns>
    public DownloadDataEntity GetServerData(string path)
    {
        if (m_ServerDataList == null) return null;

        for (int i = 0; i < m_ServerDataList.Count; i++)
        {
            
            if (path.Replace("/", "\\").Equals(m_ServerDataList[i].FullName, StringComparison.CurrentCultureIgnoreCase))
            {
                GameCommon.Log("GetServerData:" + m_ServerDataList[i].FullName);
                return m_ServerDataList[i];
            }
        }

        return null;
    }
}