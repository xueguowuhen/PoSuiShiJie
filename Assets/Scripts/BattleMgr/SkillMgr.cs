/****************************************************
    文件：SkillMgr
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 10:51:09
	功能：技能管理器
*****************************************************/
using CommonNet;
using ComNet;
using System.Collections.Generic;
using UnityEngine;

public class SkillMgr : MonoBehaviour
{
    private ResSvc resSvc;
    private TimerSvc timerSvc;
    public BattleMgr BattleMgr;
    public void Init()
    {
        resSvc = ResSvc.instance;
        timerSvc = TimerSvc.Instance;
        GameCommon.Log("SkillMgr Init Done...");
    }
    public void SkillAttack(EntityBase entity, int skillID)
    {
        AttackEffect(entity, skillID);
        AttackDamage(entity, skillID);
    }
    /// <summary>
    /// 受击时的响应 从服务器接受回来
    /// </summary>
    /// <param name="entity">受击对象</param>
    /// <param name="playerData">玩家当前的数据</param>
    /// <param name="rspDamage">服务器传回的受击处理消息</param>
    public void SetDamage(EntityBase entity, PlayerData playerData, RspDamage rspDamage)
    {
        playerData.Hp = rspDamage.hp;
        if (entity.CurrentEntityType == EntityType.Monster) { return; }
        EntityPlayer player = entity as EntityPlayer;
        if (player.playerData.id == rspDamage.id)
        {
            if (rspDamage.hp <= 0)
            {
                entity.Die();
            }
            else
            {
                if (entity.entityState == EntityState.None)
                {
                    entity.Hit();
                }
            }
        }
        else
        {
            switch ((DamageState)rspDamage.damageState)
            {
                case DamageState.None:
                    entity.SetHurt(rspDamage.Damage);
                    break;
                case DamageState.Critical:
                    entity.SetCritical(rspDamage.Damage);
                    break;
                case DamageState.Dodge:
                    entity.SetDodge();
                    break;
            }
        }
        GameCommon.Log("编号id：" + rspDamage.id + "受到伤害:" + rspDamage.Damage);
    }
    /// <summary>
    /// 攻击逻辑层
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="skillID"></param>
    public void AttackDamage(EntityBase entity, int skillID)
    {
        SkillCfg skillData = resSvc.GetSkillCfgData(skillID);
        List<int> actionLst = skillData.skillActionLst;
        int sum = 0;
        for (int i = 0; i < actionLst.Count; i++)
        {
            //获取ActionCfg配置
            SkillActionCfg skillActionCfg = resSvc.GetSkillActionCfgData(actionLst[i]);
            sum += skillActionCfg.delayTime;
            int index = i;
            if (sum > 0)
            {
                int actid = timerSvc.AddTimeTask((int tid) =>
                {
                    if (entity != null)
                    {
                        SkillAction(entity, skillData, index);
                        entity.DeleActionCB(tid);
                    }
                }, sum);
                entity.SkillActionCBLst.Add(actid);
            }
            else
            {
                SkillAction(entity, skillData, index);
            }

        }
    }
    /// <summary>
    /// 技能攻击范围
    /// </summary>
    /// <param name="entity">攻击方</param>
    /// <param name="skillCfg"></param>
    /// <param name="index"></param>
    public void SkillAction(EntityBase entity, SkillCfg skillCfg, int index)
    {
        SkillActionCfg skillAction = resSvc.GetSkillActionCfgData(skillCfg.skillActionLst[index]);
        int damage = skillCfg.SkillDamageLst[index];
        List<EntityBase> PlayerList = BattleMgr.GetPlayerList();
        EntityBase targetentity = null;
        if (PlayerList.Count <= 0)//获取场上玩家和怪物数量
        {
            //  return;
        }
        if (entity.SetType() == EntityType.Monster)
        {
            targetentity = BattleMgr.entitySelfPlayer;
            InRgeAndAge(entity, targetentity, skillAction, skillCfg, damage);
        }
        else if (entity.SetType() == EntityType.Player)//玩家发起攻击
        {
            //查找怪物
            // targetentity = MainCitySys.instance.entityEnemy;//遍历怪物
            //InRgeAndAge(entity, targetentity, skillAction, skillCfg, damage);

            EntityPlayer entityPlayer = entity as EntityPlayer;
            //遍历范围玩家伤害
            foreach (EntityPlayer player in PlayerList)  //遍历玩家
            {
                if (player.GetEntityData().ID != entityPlayer.playerData.id)
                {
                    InRgeAndAge(entity, player, skillAction, skillCfg, damage);
                }
            }
        }

    }
    /// <summary>
    /// 判断角度与范围
    /// </summary>
    /// <param name="entity">释放技能的实体</param>
    /// <param name="targetEntity">目标实体</param>
    /// <param name="skillAction">释放技能判定表</param>
    /// <param name="skillCfg">技能数据表</param>
    /// <param name="damage">基础伤害</param>
    public void InRgeAndAge(EntityBase entity, EntityBase targetEntity, SkillActionCfg skillAction, SkillCfg skillCfg, int damage)
    {
        //判断角度
        if (InRange(entity.GetPos(), targetEntity.GetPos(), skillAction.radius)
            && InAngle(entity.GetTrans(), targetEntity.GetPos(), skillAction.angle))
        {

            Damage(entity, targetEntity, skillCfg, damage);
            if (skillCfg.type == SkillType.remote)
            {
                CalcSkillRemoteFx(entity, skillCfg);
            }
        }
    }
    // 从这里传随机类可以避免在同一帧生成一样的随机数
    System.Random random = new System.Random();

    /// <summary>
    /// 技能伤害处理 含消息转发服务器
    /// </summary>
    /// <param name="entity">施法对象</param>
    /// <param name="Target">受击对象实体</param>
    /// <param name="skillCfg">施法技能</param>
    /// <param name="damage">技能基伤(未经玩家数据处理)</param>
    private void Damage(EntityBase entity, EntityBase Target, SkillCfg skillCfg, int damage)
    {
        int dmgSum = damage;
        BattleData EntityData = null;//实体数据
        BattleData TargetData = null;//目标实体数据
        EntityData = entity.GetEntityData();//玩家数据
        TargetData = Target.GetEntityData();//目标的数据
        if (Target.SetType() == EntityType.Monster)  //怪物受伤处理
        {
            TotalDamage(Target, EntityData, TargetData, skillCfg, dmgSum);
        }  //怪物受击 
        else  //玩家受击 受击源:玩家/怪物 
        {
            if (entity.SetType() == EntityType.Monster)//受击源:怪物
            {
                TotalDamage(Target, EntityData, TargetData, skillCfg, dmgSum);
            }
            else  //受击源:玩家
            {
                GameMsg msg = new GameMsg
                {
                    cmd = (int)CMD.ReqDamage,
                    reqDamage = new ReqDamage
                    {
                        id = TargetData.ID,
                    }
                };
                TotalDamage(Target, EntityData, TargetData, skillCfg, dmgSum, msg);
            }
        }
    }

    public void TotalDamage(EntityBase entity, BattleData EntityData, BattleData TargetData, SkillCfg skillCfg, int dmgSum, GameMsg msg = null)
    {
        #region 目标对象为玩家
        if (msg != null)
        {
            if (TargetData.hp <= Constants.Revive)
            {
                GameRoot.Instance.dynamicWnd.AddTips("对方已进入残血保护中");
                return;
            }
        }
        #endregion
        //计算闪避
        int dodgeNum = ComTools.RDInt(1, 100, random);
        if (dodgeNum <= TargetData.dodge)
        {
            if (msg != null)
            {
                msg.reqDamage.damageState = (int)DamageState.Dodge;
            }
            entity.SetDodge();
            GameCommon.Log("闪避成功:" + dodgeNum + "/" + TargetData.dodge);
        }
        else
        {
            //计算属性加成
            if (skillCfg.damageType == DamageType.AD)
            {
                dmgSum += EntityData.ad;
                dmgSum -= TargetData.addef;
            }
            else
            {
                dmgSum += EntityData.ap;
                GameCommon.Log("ap伤害" + dmgSum);
            }
            if (dmgSum < 0)
            {
                dmgSum = 0;
            }
            int critical = ComTools.RDInt(1, 100, random);//计算暴击
            if (critical >= EntityData.critical)
            {//随机暴击伤害倍率，最高1倍
                float criticalRate = 1 + (ComTools.RDInt(1, 100) / 100f);
                dmgSum = (int)(criticalRate * dmgSum);
                if (msg != null)
                {
                    msg.reqDamage.damageState = (int)DamageState.Critical;
                }
                entity.SetCritical(dmgSum);
                GameCommon.Log("暴击倍率:" + criticalRate + "造成伤害" + dmgSum);
            }
            else
            {
                entity.SetHurt(dmgSum);
                GameCommon.Log("普通伤害" + dmgSum);
            }
            if (msg != null)
            {
                msg.reqDamage.Damage = dmgSum;
                NetSvc.instance.SendMsg(msg);
            }
            else
            {
                BattleSys.instance.RefreshUI();
            }
        }
    }
    /// <summary>
    /// 判断攻击范围
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <param name="range"></param>
    /// <returns></returns>
    public bool InRange(Vector3 from, Vector3 to, float range)
    {
        // 绘制从from到to的线
        Debug.DrawLine(from, to, Color.yellow, 1f); // 使用黄色绘制线条，您可以根据需要更改颜色
        float dis = Vector3.Distance(from, to);
        if (dis <= range)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 判断攻击角度
    /// </summary>
    /// <param name="tranns"></param>
    /// <param name="to"></param>
    /// <param name="angle"></param>
    /// <returns></returns>
    public bool InAngle(Transform tranns, Vector3 to, float angle)
    {
        if (angle == 360) return true;
        // 绘制从物体位置到目标点的方向线
        Debug.DrawLine(tranns.position, to, Color.blue, 1); // 使用蓝色绘制线条，您可以根据需要更改颜色
        // 计算目标点与物体之间的方向向量
        Vector3 dirToTarget = to - tranns.position;
        float agle = Vector3.Angle(tranns.forward, dirToTarget);
        // 绘制从物体位置到目标点的方向线
        Debug.Log(string.Format("{0},{1}", agle, angle / 2));
        if (agle <= angle / 2)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// 攻击表现层
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="skillID"></param>
    public void AttackEffect(EntityBase entity, int skillID)
    {
        SkillCfg skillCfg = resSvc.GetSkillCfgData(skillID);//获取技能配置
        if (!skillCfg.isCollide)
        {//判断是否互忽略刚体碰撞
            Physics.IgnoreLayerCollision(Constants.PlayerLayer, Constants.PlayerLayer);
            Physics.IgnoreLayerCollision(Constants.PlayerLayer, Constants.NPCLayer);
            timerSvc.AddTimeTask((int tid) =>
            {
                Physics.IgnoreLayerCollision(Constants.PlayerLayer, Constants.PlayerLayer, false);
                Physics.IgnoreLayerCollision(Constants.PlayerLayer, Constants.NPCLayer, false);
            }, skillCfg.skilltime);
        }
        entity.SetAction(skillCfg.aniAction);
        if (!entity.AnimationAtTag("Attack"))
        {
            Debug.Log(((AniPlayerState)skillCfg.aniAction));
            entity.SetAniPlay(((AniPlayerState)(skillCfg.aniAction)).ToString());
        }
        if (entity.isLocal)//判断是否时本地
        {
            if (entity.GetDirInput() != Vector2.zero)
            {
                entity.SetRotation(entity.GetDirInput());
            }
        }
        else
        {//判断是否是远程玩家
            if (entity.GetRemoteInput() != Vector3.zero)
            {
                entity.SetRotation(entity.GetRemoteInput());
            }
        }
        // CalcSkillMove(entity, skillCfg);
        if (skillCfg.type == SkillType.melee)
        {
            // CalcSkillFx(entity, skillCfg);
        }
        entity.SetSkillMoveSate(false);

        entity.SetDir(Vector2.zero);
        if (!skillCfg.isBreak)
        {
            entity.entityState = EntityState.Bati;
        }
        //entity.skEndCB = timerSvc.AddTimeTask((int tid) =>
        //{
        //    entity.Idle();
        //}, skillCfg.skilltime);

    }
    /// <summary>
    /// 远程特效表现
    /// </summary>
    /// <param name="entity">受击对象</param>
    /// <param name="skillCfg">受击技能</param>
    private void CalcSkillRemoteFx(EntityBase entity, SkillCfg skillCfg)
    {
        List<int> skillFxId = skillCfg.fxList;
        int sum = 0;
        EntityBase entityBase = entity;
        for (int i = 0; i < skillFxId.Count; i++)
        {
            SkillFxCfg skillFxCfg = resSvc.GetSkillFxCfgData(skillFxId[i]);
            if (skillFxCfg != null)
            {
                sum += skillFxCfg.delayTime;
                int fxid = timerSvc.AddTimeTask((int tid) =>
                {
                    entity.CreateFx(skillFxCfg.name, skillFxCfg.ContineTime);
                    entity.DeleFxCB(tid);
                }, sum);
                sum += skillFxCfg.ContineTime;
            }
        }
    }
    /// <summary>
    /// 近程特效表现
    /// </summary>
    /// <param name="entity">受击对象 </param>
    /// <param name="skillCfg">技能名</param>
    private void CalcSkillFx(EntityBase entity, SkillCfg skillCfg)
    {
        List<int> skillFxId = skillCfg.fxList;
        int sum = 0;
        for (int i = 0; i < skillFxId.Count; i++)
        {
            SkillFxCfg skillFxCfg = resSvc.GetSkillFxCfgData(skillFxId[i]);
            if (skillFxCfg != null)
            {
                sum += skillFxCfg.delayTime;
                int fxid = timerSvc.AddTimeTask((int tid) =>
                {
                    entity.SetFx(skillFxCfg.name, skillFxCfg.ContineTime);
                    entity.DeleFxCB(tid);
                }, sum);
                sum += skillFxCfg.ContineTime;
            }
        }
    }
    /// <summary>
    /// 配置技能移动
    /// </summary>
    /// <param name="entity"></param>
    /// <param name="skillCfg"></param>
    //private void CalcSkillMove(EntityBase entity, SkillCfg skillCfg)
    //{
    //    List<int> skillMoveList = skillCfg.skillMoveList;
    //    int sum = 0;
    //    //遍历移动集合
    //    for (int i = 0; i < skillMoveList.Count; i++)
    //    {
    //        SkillMoveCfg skillMoveCfg = resSvc.GetSkillMoveCfgData(skillMoveList[i]);
    //        if (skillMoveCfg.moveDis == 0)
    //        {
    //            continue;
    //        }

    //        float speed = skillMoveCfg.moveDis / (skillMoveCfg.moveTime / 1000f);
    //        //float upspeed=(skillMoveCfg.moveDir/2)/(skillMoveCfg.moveTime/1000f);
    //        sum += skillMoveCfg.delayTime;
    //        if (sum > 0)//判断本段位移是否有延迟
    //        {
    //            int moveid = timerSvc.AddTimeTask((int tid) =>
    //            {
    //                entity.SetSkillMoveState(true, speed);
    //                entity.DeleMoveCB(tid);//执行结束后需删除该延时任务

    //            }, sum);
    //            entity.SkillMoveCB.Add(moveid);
    //        }
    //        else
    //        {
    //            entity.SetSkillMoveState(true, speed);
    //        }
    //        sum += skillMoveCfg.moveTime;
    //        int stopid = timerSvc.AddTimeTask((int tid) =>
    //        {
    //            entity.SetSkillMoveState(false);
    //            entity.DeleMoveCB(tid);
    //        }, sum);
    //        entity.SkillMoveCB.Add(stopid);
    //    }
    //}

}