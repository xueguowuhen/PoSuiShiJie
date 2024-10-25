/****************************************************
    文件：StateMove
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 10:48:40
	功能：移动状态
*****************************************************/
using System;
using UnityEngine;
public class StateMove : ISate
{

    public void Enter(EntityBase entity, params object[] objects)
    {
        entity.currentAniState = AniState.Move;
        entity.SetAction((int)AniPlayerState.WalkStart);
        entity.SetAniCrossFade(AniPlayerState.WalkStart.ToString(), Constants.AniSpeed);
        entity.SetHasInput(true);
        entity.SetMove(true);
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.WalkStart, OnWalkStartEvent);
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.TurnBack, OnTurnBackEvent);
    }

    private void OnTurnBackEvent(EntityBase entity)
    {

        entity.SetAction(Constants.ActionDefault);

        //  entity.SetAction((int)AniPlayerState.WalkStart);
        //entity.SetAniCrossFade(AniPlayerState.WalkStart.ToString(), Constants.AniSpeed);

    }

    public void OnWalkStartEvent(EntityBase entity)
    {
        entity.SetAction(Constants.ActionDefault);
    }

    public void Process(EntityBase entity)
    {

        if (entity.GetRunState())
        {
            entity.SetVelocity(Constants.VelocityRun);
        }
        else
        {
            entity.SetVelocity(Constants.VelocityWalk);
        }

    }
    public void Exit(EntityBase entity, params object[] objects)
    {
        entity.SetMove(false);

        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.WalkStart, OnWalkStartEvent);
        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.TurnBack, OnTurnBackEvent);
    }

}