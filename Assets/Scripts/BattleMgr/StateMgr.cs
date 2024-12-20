/****************************************************
    文件：StateMgr
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 10:51:24
	功能：状态管理器
*****************************************************/
using CommonNet;
using System;
using System.Collections.Generic;
using UnityEngine;
public class StateMgr : MonoBehaviour
{

    private Dictionary<AniState, ISate> FSM = new Dictionary<AniState, ISate>();
    private List<EntityBase> entities = new List<EntityBase>();
    public void Init()
    {
        FSM.Add(AniState.Idle, new StateIdle());
        FSM.Add(AniState.Move, new StateMove());
        FSM.Add(AniState.Evade, new StateEvade());
        FSM.Add(AniState.TurnBack, new StateTurnBack());
        FSM.Add(AniState.Attack, new StateAttack());
        FSM.Add(AniState.Hit, new StateHit());
        FSM.Add(AniState.Die, new StateDie());
        GameCommon.Log("StateMgr Init Done...");

    }
    /// <summary>
    /// 每帧执行
    /// </summary>
    public void Update()
    {
        // 遍历所有实体并更新其状态
        foreach (var entity in entities)
        {
            if (entity != null && FSM.ContainsKey(entity.currentAniState))
            {
                FSM[entity.currentAniState].Process(entity); // 调用当前状态的Process方法
            }
        }
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
        if (!entities.Contains(entity))
        {
            entities.Add(entity);
        }
        if (FSM.ContainsKey(targerState))
        {
            if (entity.isLocal)
            {
                EntityPlayer entityPlayer = entity as EntityPlayer;
                GameMsg msg = new GameMsg
                {
                    cmd = (int)CMD.ReqState,
                    reqPlayerState = new ReqPlayerState
                    {
                        PlayerID = entityPlayer.playerData.id,
                        AniState = (int)targerState,
                        LocalTime = TimerSvc.Instance.GetCurrServerTime(),
                    }
                };
                if (argas != null)
                {

                    if (argas.Length > 0)
                    {

                        msg.reqPlayerState.SkillID = (int)argas[0];
                    }
                }
                GameCommon.Log(Enum.GetName(typeof(AniState), targerState));
                NetSvc.Instance.SendMsg(msg);
            }
            //     Debug.Log("上次状态" + entity.currentAniState.ToString());
            if (entity.currentAniState != AniState.None)
            {
                FSM[entity.currentAniState].Exit(entity, argas);
            }
            FSM[targerState].Enter(entity, argas);
            //   Debug.Log("当前状态" + targerState.ToString());
        }
    }
}
