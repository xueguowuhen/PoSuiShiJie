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
        Debug.Log("进入转身状态");
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.TurnBack, OnTurnBackEvent);
    }

    private void OnTurnBackEvent(EntityBase entity)
    {
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
        if (entity.GetDirInput() != Vector2.zero)
        {
            entity.SetHasInput(true);
        }
        else
        {
            entity.SetHasInput(false);
        }
    }
    public void Exit(EntityBase entity, params object[] objects)
    {
        entity.SetAction(Constants.ActionDefault);
        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.TurnBack, OnTurnBackEvent);
    }

}
