/****************************************************
    文件：AnimationEventCtrl.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/25 11:59:37
    功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventCtrl : StateMachineBehaviour
{
    PlayerController player;
    public AniPlayerState playerState;
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AniPlayerState.None == playerState) return;
    }


    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (AniPlayerState.None == playerState) return;
        if (animator.TryGetComponent(out player))
        {
            AnimatorDispatcher.Instance.Dispatch(playerState, BattleSys.instance.GetBattleMgr().entitySelfPlayer);
            
        }
    }
}
