/****************************************************
    文件：SocketFailTipWnd.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/11/3 0:10:14
    功能：Nothing
*****************************************************/
using UnityEngine;
using UnityEngine.UI;

public class SocketFailTipWnd : WindowRoot
{
    public Button ExitBtn;
    public Button RecoverSocketBtn;
    public Text txtContent;

    protected override void InitWnd()
    {
        base.InitWnd();
    }
    protected override void SetGameObject()
    {
        base.SetGameObject();
        ExitBtn.onClick.AddListener(OnBtnExitClick);
        RecoverSocketBtn.onClick.AddListener(OnBtnRecoverSocketClick);

    }
    /// <summary>
    /// 网络重连
    /// </summary>
    private void OnBtnRecoverSocketClick()
    {
        netSvc.StartAsClient();
    }
    /// <summary>
    ///  退出游戏
    /// </summary>
    private void OnBtnExitClick()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
Application.Quit();
#endif
    }
}
