/****************************************************
    文件：SystemRoot
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-14 9:38:26
	功能：业务模块基类
*****************************************************/
using CommonNet;
using UnityEngine;


public class SystemRoot : MonoBehaviour
{
    protected NetSvc netSvc;    //
    protected ResSvc resSvc;    //
    protected AudioSvc audioSvc;//
    protected TimerSvc timerSvc;
    public virtual void InitSyc()
    {
        netSvc = NetSvc.instance;
        resSvc = ResSvc.instance;
        audioSvc = AudioSvc.instance;
        timerSvc= TimerSvc.Instance;
    }
    public GameObject GameFind(string Path)
    {
         return transform.Find(Path).gameObject;
    }

    public void PlayerPosAndRot(PlayerData playerData, RspTransform rspTransform)
    {
        playerData.Rot_X = rspTransform.Rot_X;
        playerData.Rot_Y = rspTransform.Rot_Y;
        playerData.Rot_Z = rspTransform.Rot_Z;
    }
    public Vector3 GetPlayerPos(PlayerData playerData)
    {
        return new Vector3(playerData.Pos_X, playerData.Pos_Y, playerData.Pos_Z);
    }
    public Quaternion GetPlayerRot(PlayerData playerData)
    {
        return Quaternion.Euler(new Vector3(playerData.Rot_X, playerData.Rot_Y, playerData.Rot_Z));
    }

}
