/****************************************************
    文件：GuideWnd.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/28 15:50:14
	功能：任务引导系统
*****************************************************/

using CommonNet;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 对话界面
/// </summary>
public class GuideWnd : WindowRoot
{
    public Button BtnNext;
    public Text TextTalk;
    public Text TextName;
    public RawImage imgIcon;
    public GameObject ChraterGO;
    public Camera ChraterCamera;
    private GameObject player;
    private TaskCfg taskCfg;
    private string[] dilogArr;
    private PlayerData playerData;
    private Transform[] NpcPosTrans;
    private int index;
    protected override void InitWnd()
    {
        base.InitWnd();
        SetGameObject();
        SetTalkData();
    }
    protected override void SetGameObject()
    {
        BtnNext = GetButton(PathDefine.BtnNext);
        AddListener(BtnNext, ClickNextBtn);
        TextTalk = GetText(PathDefine.TextTalk);
        TextName = GetText(PathDefine.TextName);
        imgIcon = GetRawImg(PathDefine.imgIcon);
       // ChraterGO = GameObject.Find(PathDefine.CharaterCamera);
        ChraterGO.transform.localPosition = new Vector3(Constants.GuideCamera_x, Constants.GuideCamera_y, Constants.GuideCamera_Z);
        ChraterGO.transform.localRotation = Quaternion.Euler(Constants.GuideCameraRos_x, Constants.GuideCameraRos_y, Constants.GuideCameraRos_z);
        ChraterGO.transform.localScale = Vector3.zero;
        ChraterCamera = ChraterGO.GetComponent<Camera>();
        ChraterCamera.fieldOfView = 27f;
        taskCfg = MainCitySys.instance.GetTaskCfg();
        index = 1;
        dilogArr = taskCfg.dilogArr.Split('#');
        playerData = GameRoot.Instance.PlayerData;
        player = MainCitySys.instance.GetPlayer();
        NpcPosTrans = MainCitySys.instance.GetNpcTra();
        imgIcon.texture = ChraterCamera.targetTexture;
    }
    public void SetTalkData()//设置对话内容
    {
        string[] talkArr = dilogArr[index].Split('|');

        if (talkArr[0] == PathDefine.PlayerName)
        {
            ChraterGO.transform.SetParent(player.transform, false);
            SetText(TextName, playerData.name);
            ChraterCamera.cullingMask = Constants.CameraMackPlayer;

        }
        else
        {
            //对话NPC
            ChraterGO.transform.SetParent(NpcPosTrans[taskCfg.npcID], false);
            SetText(TextName, talkArr[0]);
            ChraterCamera.cullingMask = Constants.CameraMackNpc;
        }
        SetText(TextTalk, talkArr[1].Replace("$name", playerData.name));
    }
    public void ClickNextBtn()
    {
        index++;
        if (index >= dilogArr.Length)
        {
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqTask,
                reqTask = new ReqTask
                {
                    Taskid = taskCfg.ID
                }
            };
            netSvc.SendMsg(msg);//发送任务数据
            SetWndState(false);//关闭窗口
        }
        else
        {

            SetTalkData();
        }
    }
}