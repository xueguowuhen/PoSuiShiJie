/****************************************************
    文件：StateEvade
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-26 12:55:28
	功能：闪避状态
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class StateEvade : ISate
{
    public void Enter(EntityBase entity, params object[] objects)
    {
      //  AnimatorStateInfo stateInfo = entity.GetCurrentAniStateInfo();
        //if (stateInfo.IsName(AniPlayerState.TurnBack.ToString()))
        //{
        //    entity.SetAction(Constants.ActionDefault);
        //    entity.TurnBack();
        //    return;
        //}
        entity.SetAniPlay(AniPlayerState.Evade_Front.ToString());
        entity.currentAniState = AniState.Evade;
        AniPlayerState playerState = AniPlayerState.Evade_Front;
        //entity.SetMove(true);
        entity.SetEvade(true);
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.Evade_Front, OnEvadeFrontEvent);

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
    private void OnEvadeFrontEvent(EntityBase entity)
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

    public void Exit(EntityBase entity, params object[] objects)
    {
        //entity.SetMove(false);
        entity.SetEvade(false);
        entity.SetAction(Constants.ActionDefault);

        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.Evade_Front, OnEvadeFrontEvent);
    }

}

