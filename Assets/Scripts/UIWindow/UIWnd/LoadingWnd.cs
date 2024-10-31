/****************************************************
    文件：LoadingWnd.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/13 22:39:29
	功能：过程加载窗体
*****************************************************/

using CommonNet;
using UnityEngine;
using UnityEngine.UI;

public class LoadingWnd : WindowRoot 
{
    public Text txtTips;
    public Image imageFG;
    public Text txtPrg;

    protected override void InitWnd()
    {
        base.InitWnd();
        SetGameObject();
        SetText(txtTips, "这是一条游戏Tips");
        SetText(txtPrg, "0%");
        imageFG.fillAmount = 0;//前景的进度条
    }
    protected override void SetGameObject()
    {
        //txtTips = SetTranFind(PathDefine.Loading_txtTips).GetComponent<Text>();
        //imageFG = SetTranFind(PathDefine.Loading_imageFG).GetComponent<Image>();
        //txtPrg = SetTranFind(PathDefine.Loading_txtPrg).GetComponent<Text>();
    }
    public void SetProgress(float prg)
    {
        SetText(txtPrg, (int)(prg) + "%");
        imageFG.fillAmount = prg/100;
    }
}