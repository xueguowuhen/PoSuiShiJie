/****************************************************
    文件：SvcBase
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-11-10 15:33:21
	功能：Nothing
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class SvcBase:MonoBehaviour
{
    public void Update()
    {
        OnUpdate();
    }
    void OnDestroy()
    {
        BeforeOnDestroy();
    }
    public virtual void InitSvc() { }
    
    public virtual void OnUpdate() { }
    public virtual void BeforeOnDestroy() { }
}