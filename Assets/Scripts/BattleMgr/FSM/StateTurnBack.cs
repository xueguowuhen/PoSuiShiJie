/****************************************************
    文件：StateTurnBack
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-26 18:33:59
	功能：Nothing
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StateTurnBack : ISate
{
    public void Enter(EntityBase entity, params object[] objects)
    {
        entity.currentAniState = AniState.TurnBack;
        entity.SetAction((int)AniPlayerState.TurnBack);
        entity.SetAniPlay(AniPlayerState.TurnBack.ToString());
        //entity.SetHasInput(true);
        entity.canRlskill = false;
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.TurnBack, OnTurnBackEvent);
    }

    private void OnTurnBackEvent(EntityBase entity)
    {
        entity.SetAction(Constants.ActionDefault);
        if (entity.GetDirInput() != Vector2.zero)
        {
            entity.Move();
            entity.SetDir(entity.GetDirInput());
        }
        else
        {
            entity.Idle();
            entity.SetAniCrossFade(AniPlayerState.IdleAFK.ToString(), Constants.AniSpeed);
        }
    }
    public void Process(EntityBase entity)
    {
        if (entity.GetCurrentAniStateInfo().normalizedTime >= 1f && entity.AnimationAtTag("TurnBack"))
        {
            AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.TurnBack, OnTurnBackEvent);
            OnTurnBackEvent(entity);
        }
        //if (entity.GetDirInput() != Vector2.zero)
        //{
        //    entity.SetHasInput(true);
        //}
        //else
        //{
        //    entity.SetHasInput(false);
        //}

        entity.SetVelocity(Constants.VelocityRun);

        entity.SetDir(entity.GetDirInput());

    }
    public void Exit(EntityBase entity, params object[] objects)
    {

        //entity.SetAction(Constants.ActionDefault);
        entity.canRlskill = true;
        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.TurnBack, OnTurnBackEvent);
    }

}
