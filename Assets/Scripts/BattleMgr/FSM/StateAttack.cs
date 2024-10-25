/****************************************************
    文件：StateAttack
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 10:49:22
	功能：进入攻击状态
*****************************************************/
public class StateAttack : ISate
{
    public void Enter(EntityBase entity, params object[] objects)
    {
        entity.currentAniState = AniState.Attack;
        entity.curtSkillCfg = ResSvc.instance.GetSkillCfgData((int)objects[0]);
        entity.SetVelocity(Constants.VelocityDefault);
        entity.canRlskill = false;
        entity.SkillAttack((int)objects[0]);
    }

    public void Exit(EntityBase entity, params object[] objects)
    {
        entity.ExitCurtSkill();
    }

    public void OnAnimatorEndEvent(EntityBase entity)
    {
        
    }

    public void Process(EntityBase entity)
    {
        
    }

}