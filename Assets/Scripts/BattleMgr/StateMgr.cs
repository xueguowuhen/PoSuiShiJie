/****************************************************
    文件：StateMgr
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 10:51:24
	功能：状态管理器
*****************************************************/
using System.Collections.Generic;
using UnityEngine;
using CommonNet;
using System;
public class StateMgr : MonoBehaviour
{
    private Dictionary<AniState, ISate> FSM = new Dictionary<AniState, ISate>();
    public void Init()
    {
        FSM.Add(AniState.Idle, new StateIdle());
        FSM.Add(AniState.Move, new StateMove());
        FSM.Add(AniState.Run, new StateRun());
        FSM.Add(AniState.Attack, new StateAttack());
        FSM.Add(AniState.Hit, new StateHit());
        FSM.Add(AniState.Die, new StateDie());
        GameCommon.Log("StateMgr Init Done...");
    }
    /// <summary>
    /// 状态切换
    /// </summary>
    public void ChangeStates(EntityBase entity, AniState targerState, params object[] argas)
    {
        if (entity.currentAniState == targerState)
        {
            return;
        }
        if (FSM.ContainsKey(targerState))
        {
            if (entity.isLocal)
            {
                EntityPlayer entityPlayer=entity as EntityPlayer;
                GameMsg msg = new GameMsg
                {
                    cmd = (int)CMD.ReqState,
                    reqPlayerState = new ReqPlayerState
                    {
                        PlayerID = entityPlayer.playerData.id,
                        AniState = (int)targerState,
                    }
                };
                if (argas != null)
                {
                    
                    if (argas.Length>0)
                    {

                        msg.reqPlayerState.SkillID = (int)argas[0];
                    }
                }
                //GameCommon.Log(Enum.GetName(typeof(AniState), targerState));
                NetSvc.instance.SendMsg(msg);
            }
            if (entity.currentAniState != AniState.None)
            {
                FSM[entity.currentAniState].Exit(entity, argas);
            }
            FSM[targerState].Enter(entity, argas);
            FSM[targerState].Process(entity, argas);
        }
    }
}
