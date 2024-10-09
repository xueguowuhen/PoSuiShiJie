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
    void Process(EntityBase entity, params object[] objects);
    void Exit(EntityBase entity, params object[] objects);
}
/// <summary>
/// 动画状态枚举
/// </summary>
public enum AniState
{
    None,
    Idle,
    Move,
    Run,
    Attack,
    Hit,
    Die,
}
