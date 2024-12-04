/****************************************************
    文件：EntityPlayer
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 9:27:25
	功能：玩家逻辑实体
*****************************************************/
using CommonNet;
using UnityEngine;
public class EntityPlayer : EntityBase
{
    public PlayerData playerData;
    public override Vector2 GetDirInput()
    {
        return battleMgr.GetDirInput();
    }
    public override void SetHurt(int hurt)
    {
        playerData.Hp -= hurt;
        Hit();
        GameRoot.Instance.dynamicWnd.SetHurt(playerData.id, hurt);
    }
    public override void SetCritical(int hurt)
    {
        Debug.Log(hurt);
        playerData.Hp -= hurt;
        Hit();
        GameRoot.Instance.dynamicWnd.SetCritical(playerData.id, hurt);
    }
    public override void SetDodge()
    {
        //闪避表现
        GameRoot.Instance.dynamicWnd.SetDodge(playerData.id);
    }
    public override bool GetRunState()
    {
        return battleMgr.GetRunState();
    }
    //public override void SetEntityData<T>(BaseData<T> data)
    //{
    //    //base.SetEntityData(data);
    //}
    public override BattleData GetEntityData()
    {

        return new BattleData()
        {
            ID = playerData.id,
            hp = playerData.Hp,
            ap = playerData.ap,
            ad = playerData.ad,
            addef = playerData.addef,
            dodge = playerData.dodge,
            critical = playerData.critical,
        };
    }
}
