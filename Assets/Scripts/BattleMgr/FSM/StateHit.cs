/****************************************************
    文件：StateHit
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 10:49:01
	功能：进入受击状态
*****************************************************/
using UnityEngine;
public class StateHit : ISate
{
    public void Enter(EntityBase entity, params object[] objects)
    {
        entity.currentAniState = AniState.Hit;
        entity.DelePlayerCB();
        entity.SetDir(Vector2.zero);
        entity.SetAction((int)AniPlayerState.Hit);
        entity.canRlskill = false;
        //TimerSvc.Instance.AddTimeTask((int tid) =>
        //{
        //    //entity.SetAction(Constants.ActionDefault);
        //    entity.Idle();
        //}, (int)(GetHitAniLen(entity) * 1000));
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.Hit, OnHitEvent);
    }
    public void OnHitEvent(EntityBase entity)
    {
        entity.Idle();
    }
    public void Exit(EntityBase entity, params object[] objects)
    {
        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.Hit, OnHitEvent);
    }
    public void Process(EntityBase entity)
    {

    }


    //private float GetHitAniLen(EntityBase entity)
    //{
    //    AnimationClip[] clips = entity.GetAniClips();
    //    for (int i = 0; i < clips.Length; i++)
    //    {
    //        string clipName = clips[i].name;
    //        if (clipName.Contains("hit") ||
    //            clipName.Contains("Hit") ||
    //            clipName.Contains("HIT"))
    //        {
    //            return clips[i].length;
    //        }
    //    }
    //    return 1;
    //}
}