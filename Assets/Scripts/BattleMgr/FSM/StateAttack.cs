/****************************************************
    文件：StateAttack
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 10:49:22
	功能：进入攻击状态
*****************************************************/
using System;

public class StateAttack : ISate
{
    public void Enter(EntityBase entity, params object[] objects)
    {

        entity.currentAniState = AniState.Attack;
        entity.curtSkillCfg = ResSvc.instance.GetSkillCfgData((int)objects[0]);
        entity.SetVelocity(Constants.VelocityDefault);
        entity.canRlskill = false;
        entity.SkillAttack((int)objects[0]);
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.Attack_Normal_01, OnAttack_Normal_End);
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.Attack_Normal_02, OnAttack_Normal_End);
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.Attack_Normal_03, OnAttack_Normal_End);
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.Attack_Normal_04, OnAttack_Normal_End);
    }

    private void OnAttack_Normal_End(EntityBase entity)
    {
        entity.Idle();
    }

    public void Exit(EntityBase entity, params object[] objects)
    {
        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.Attack_Normal_01, OnAttack_Normal_End);
        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.Attack_Normal_02, OnAttack_Normal_End);
        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.Attack_Normal_03, OnAttack_Normal_End);
        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.Attack_Normal_04, OnAttack_Normal_End);
        entity.ExitCurtSkill();
    }

    public void OnAnimatorEndEvent(EntityBase entity)
    {
        
    }

    public void Process(EntityBase entity)
    {
        
    }

}