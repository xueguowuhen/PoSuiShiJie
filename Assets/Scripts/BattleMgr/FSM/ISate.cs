/****************************************************
    文件：ISate.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/5/10 10:46:14
	功能：状态接口
*****************************************************/

using UnityEngine;

public interface ISate 
{
    void Enter(EntityBase entity, params object[] objects);
    void Process(EntityBase entity);

    void Exit(EntityBase entity, params object[] objects);

   // void OnAnimatorEndEvent(EntityBase entity,AniPlayerState playerState,int layerIndex);
}
/// <summary>
/// 动画状态枚举
/// </summary>
public enum AniState
{
    None,
    Idle,
    Move,
    Attack,
    Hit,
    Die,
    Evade,//闪避
    TurnBack,//转身
}
