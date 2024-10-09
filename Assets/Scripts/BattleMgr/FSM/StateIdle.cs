/****************************************************
    文件：StateIdle
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 10:48:21
	功能：待机状态
*****************************************************/
using UnityEngine;
public class StateIdle : ISate
{
    public void Enter(EntityBase entity, params object[] objects)
    {
        entity.currentAniState = AniState.Idle;
        entity.SetmoveDistance(Constants.PlayerIdleSpeed);
        entity.SetAction(Constants.ActionDefault);
        entity.SetDir(Vector2.zero);
        entity.skEndCB = -1;
    }

    public void Exit(EntityBase entity, params object[] objects)
    {

    }

    public void Process(EntityBase entity, params object[] objects)
    {

        if (entity.nextSkillID != 0)
        {

            entity.Attack(entity.nextSkillID);
        }
        else
        {
            entity.canRlskill = true;
            entity.SetSkillMoveSate(true);
            if (entity.GetDirInput() != Vector2.zero &&entity.isLocal)
            {
                if (entity.GetRunState())
                {
                    entity.Run();
                }
                else
                {
                    entity.Move();
                }
                entity.SetDir(entity.GetDirInput());
            }
            else
            {

                entity.SetVelocity(Constants.VelocityIdle);
            }
        }
    }
}