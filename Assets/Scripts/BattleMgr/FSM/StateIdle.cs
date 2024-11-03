/****************************************************
    文件：StateIdle
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 10:48:21
	功能：待机状态
*****************************************************/
using System.Linq;
using UnityEditor;
using UnityEngine;
public class StateIdle : ISate
{
    public void Enter(EntityBase entity, params object[] objects)
    {
        switch (entity.currentAniState)//设置动画状态
        {
            case AniState.Move:
                entity.SetAniCrossFade(entity.GetWalkOrRunState().ToString(), Constants.AniSpeed);
                entity.SetAction((int)entity.GetWalkOrRunState());
                Debug.Log("进入待机状态"+ entity.GetWalkOrRunState().ToString());
                break;
            default:

                entity.SetAction(Constants.ActionDefault);
                break;
        }
        //初始化
        entity.currentAniState = AniState.Idle;
        //entity.SetHasInput(false);
        entity.SetmoveDistance(Constants.PlayerIdleSpeed);
        entity.SetDir(Vector2.zero);
        #region 待机时，取消所有技能
        entity.skEndCB = -1;
        if (entity.nextSkillID != 0)
        {

            entity.Attack(entity.nextSkillID);

        }
        else
        {
            entity.canRlskill = true;
            entity.SetSkillMoveSate(true);
            if (entity.GetDirInput() != Vector2.zero && entity.isLocal)
            {

                entity.Move();

                entity.SetDir(entity.GetDirInput());
            }
            else
            {

                entity.SetVelocity(Constants.VelocityIdle);
            }
        }
        #endregion
        //添加事件监听
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.RunStop, OnRunStopEvent);
        AnimatorDispatcher.Instance.AddEventListener(AniPlayerState.WalkStop, OnRunStopEvent);
    }

    public void OnRunStopEvent(EntityBase entity)
    {
        entity.SetAction(Constants.ActionDefault);
    }

    public void Process(EntityBase entity)
    {


    }

    public void Exit(EntityBase entity, params object[] objects)
    {
        //移除事件监听
        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.RunStop, OnRunStopEvent);
        AnimatorDispatcher.Instance.RemoveEventListener(AniPlayerState.WalkStop, OnRunStopEvent);
        entity.SetAction(Constants.ActionDefault);
    }
}