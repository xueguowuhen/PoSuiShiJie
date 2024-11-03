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
        entity.SetAniPlay(AniPlayerState.Evade_Front.ToString());
        entity.currentAniState = AniState.Evade;
        //entity.SetMove(true);
        entity.SetEvade(true);
        entity.EvadeEnd=false;
        entity.SetAction((int)AniPlayerState.Evade_Front);
        entity.canRlskill = false;
        Debug.Log("进入闪避状态");
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.Evade_Front, OnEvadeFrontEvent);
        UIBtnDispatcher.Instance.AddEventListener(PathDefine.btnEvade, OnEvadeBtnEvent);

    }
    public void OnEvadeBtnEvent(EntityBase entity)
    {
        //if (entity.GetCurrentAniStateInfo().normalizedTime >= 0.21f && entity.AnimationAtTag("Evade"))
        //{
        //    entity.Idle();
        //    entity.EvadeEnd = true;
        //}
    }
    public void Process(EntityBase entity)
    {
        //if (entity.GetDirInput() != Vector2.zero)
        //{
        //    entity.SetHasInput(true);
        //}
        //else
        //{
        //    entity.SetHasInput(false);
        //}
        if (entity.GetCurrentAniStateInfo().normalizedTime >= 1f&& entity.AnimationAtTag("Evade"))
        {
            AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.TurnBack, OnEvadeFrontEvent);
            OnEvadeFrontEvent(entity);
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
        entity.canRlskill = true;
     //   entity.SetAction(Constants.ActionDefault);

        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.Evade_Front, OnEvadeFrontEvent);
        UIBtnDispatcher.Instance.RemoveEventListener(PathDefine.btnEvade, OnEvadeBtnEvent);
        // Debug.Log("退出闪避状态");
    }


}

