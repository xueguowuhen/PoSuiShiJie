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
        switch (entity.currentAniState)
        {
            case AniState.Evade://从闪避状态进入移动状态
                //entity.SetAction((int)AniPlayerState.Move);
                //entity.SetAniCrossFade(AniPlayerState.Move.ToString(), 0.25f);
                //break;
            case AniState.Idle:
                entity.SetAction((int)AniPlayerState.Move);
                break;
            case AniState.TurnBack:
                entity.SetAction((int)AniPlayerState.Move);
                  entity.SetAniCrossFade(AniPlayerState.Move.ToString(), 0.25f);
                break;
            default://从其他状态进入移动状态
                entity.SetAction((int)AniPlayerState.WalkStart);
                entity.SetAniCrossFade(AniPlayerState.WalkStart.ToString(), Constants.AniSpeed);
                break;
        }
        entity.currentAniState = AniState.Move;
        //entity.SetHasInput(true);
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.WalkStart, OnWalkStartEvent);
    }



    public void OnWalkStartEvent(EntityBase entity)
    {
        entity.SetAction((int)AniPlayerState.Move);
        entity.SetAniPlay(AniPlayerState.Move.ToString());
        Debug.Log("OnWalkStartEvent");
    }

    public void Process(EntityBase entity)
    {
        // SetMove(false);
        if (entity.isLocal)
        {
                entity.SetAction((int)AniPlayerState.Move);
            if (entity.GetRunState())
            {
                entity.SetVelocity(Constants.VelocityRun);
            }
            else
            {
                entity.SetVelocity(Constants.VelocityWalk);
            }
        }
    }

    public void Exit(EntityBase entity, params object[] objects)
    {
        //entity.SetHasInput(false);
        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.WalkStart, OnWalkStartEvent);

    }

}