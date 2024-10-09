/****************************************************
    文件：StateRun
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-11 13:38:50
	功能：跑步状态
*****************************************************/
using UnityEngine;

public class StateRun : ISate
{
    public void Enter(EntityBase entity, params object[] objects)
    {
        entity.currentAniState = AniState.Run;
    }

    public void Exit(EntityBase entity, params object[] objects)
    {
        entity.SetMove(false);
    }

    public void Process(EntityBase entity, params object[] objects)
    {
        entity.SetVelocity(Constants.VelocityRun,true);
        entity.SetMove(true);
    }
}
