/****************************************************
    文件：StateMove
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 10:48:40
	功能：移动状态
*****************************************************/
using UnityEngine;
public class StateMove : ISate
{
    public void Enter(EntityBase entity, params object[] objects)
    {
        entity.currentAniState = AniState.Move;

    }

    public void Exit(EntityBase entity, params object[] objects)
    {
        entity.SetMove(false);
    }

    public void Process(EntityBase entity, params object[] objects)
    {
        entity.SetVelocity(Constants.VelocityWalk);
        entity.SetMove(true);
    }
}