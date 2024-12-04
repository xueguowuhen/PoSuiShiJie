/****************************************************
    文件：RegisterWnd.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/13 22:39:19
	功能：注册窗体
*****************************************************/

using CommonNet;
using UnityEngine.UI;

public class RegisterWnd : WindowRoot
{
    public Button btnClose;
    public Button btnRegister;
    //private Animator animator;
    public UIAnimation RegisterAni;
    public InputField userName;
    public InputField password;
    public InputField Repassword;
    protected override void InitWnd()
    {
        base.InitWnd();
        WindowAnimation.Instance.StartShowWindow(RegisterAni, true);
    }
    protected override void SetGameObject()
    {

        AddListener(btnClose, ClickClose);

        AddListener(btnRegister, ClickRegister);
    }
    public void ClickClose()
    {
        WindowAnimation.Instance.StartShowWindow(RegisterAni, false);
        timerSvc.AddTimeTask((int tid) =>
        {
            LoginSys.instance.EnterLogin();
            SetWndState(false);
        }, RegisterAni.duration, TimeUnit.Second);
    }
    public void ClickRegister()
    {
        string acct = userName.text;
        string pass = password.text;
        string repass = Repassword.text;
        if (acct != "" && pass != "" && repass != "")
        {
            if (pass == repass)
            {
                GameMsg msg = new GameMsg
                {
                    cmd = (int)CMD.ReqRegister,
                    reqRegister = new ReqRegister
                    {
                        acct = acct,
                        pass = pass,
                    }
                };
                netSvc.SendMsg(msg);
                //GameCommon.Log("注册成功");
            }
            else
            {
                GameRoot.AddTips("账号或密码有误，请检查后再试");
            }
        }
        else
        {
            GameRoot.AddTips("账号或密码有误，请检查后再试");
        }
    }
}