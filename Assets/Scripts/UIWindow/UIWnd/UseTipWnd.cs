using UnityEngine.UI;

public class UseTipWnd : WindowRoot
{
    private Text Use_text;
    private Button Use;
    private Text ItemInfo;
    private Button CloseButton;
    private string m_ButtonContent = "";
    private string m_ItemInfo = "";
    public string ItemInfoContent
    {
        set { m_ItemInfo = value; }
    }
    public string ButtonContent
    {
        set { m_ButtonContent = value; }
    }
    protected override void InitWnd()
    {
        base.InitWnd();
        SetGameObject();
    }
    protected override void SetGameObject()
    {
        CloseButton = GetButton(PathDefine.CloseButton);
        AddListener(CloseButton, ClickClose);
        Use = GetButton(PathDefine.UseButton);
        AddListener(Use, ClickUseButton);
        Use_text = GetText(PathDefine.UseText);
        ItemInfo = GetText(PathDefine.ItemText);
        Use_text.text = m_ButtonContent;
        ItemInfo.text = m_ItemInfo;
    }
    private void ClickUseButton()
    {

    }
    private void ClickClose()
    {
        this.SetWndState(false);
    }

}
