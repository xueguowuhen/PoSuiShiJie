/****************************************************
    文件：StateDie
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-14 14:14:58
	功能：死亡状态
*****************************************************/
using UnityEngine;
using UnityEngine.Rendering;

public class StateDie : ISate
{
    public void Enter(EntityBase entity, params object[] objects)
    {
        entity.currentAniState = AniState.Die;
        entity.DelePlayerCB();
        entity.SetAction((int)AniPlayerState.Death);
        if (entity.isLocal)
        {
            entity.canControl = false;//停止移动
            TimerSvc.Instance.AddTimeTask((int tid) =>
            {
                entity.canControl = true;
                entity.Idle();
                BattleSys.instance.battleWnd.SetWndState(false);
                MainCitySys.instance.mainCityWnd.SetWndState(true);
            }, Constants.Revive, TimeUnit.Second);
        }
    }

    public void Exit(EntityBase entity, params object[] objects)
    {

    }

    public void OnAnimatorEndEvent(EntityBase entity)
    {
        
    }

    public void Process(EntityBase entity)
    {
       
    }
}