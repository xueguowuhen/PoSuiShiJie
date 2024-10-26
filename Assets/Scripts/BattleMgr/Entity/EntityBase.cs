/****************************************************
    文件：EntityBase
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 9:27:12
	功能：逻辑实体基类
*****************************************************/
using UnityEngine;
using System.Collections.Generic;
using CommonNet;

public abstract class EntityBase
{
    /// <summary>
    /// 当前动画状态
    /// </summary>
    public AniState currentAniState = AniState.None;
    /// <summary>
    /// 实体状态
    /// </summary>
    public EntityState entityState = EntityState.None;

    public EntityType CurrentEntityType = EntityType.None;
    public BattleData battleData = null;
    public StateMgr stateMgr = null;
    public SkillMgr skillMgr = null;
    protected Controller controller = null;
    public BattleMgr battleMgr = null;
    public SkillCfg curtSkillCfg;
    public int skEndCB = -1;

    public bool canRlskill = true;
    /// <summary>
    /// 判断是否是本地
    /// </summary>
    public bool isLocal = false;
    public bool canControl = true;// 是否允许被移动
    public List<int> SkillMoveCB = new List<int>();
    public List<int> SkillFxCB = new List<int>();
    public List<int> SkillActionCBLst = new List<int>();
    public Queue<int> comboQue = new Queue<int>();
    public int nextSkillID;

    public int CurtScene; //玩家当前的场景 :主城 副本 pvp...
                          //不同场景释放技能遍历对象不同
    #region 逻辑状态切换
    public void Idle()
    {
        stateMgr.ChangeStates(this, AniState.Idle, null);
    }
    /// <summary>
    /// 切换到移动状态
    /// </summary>
    /// <param name="isRun"></param>
    public void Move()
    {
        stateMgr.ChangeStates(this, AniState.Move);
    }
    /// <summary>
    /// 切换到闪避状态
    /// </summary>
    public void Evade()
    {
        if (currentAniState == AniState.TurnBack) return;
        stateMgr.ChangeStates(this, AniState.Evade);
    }
    public void TurnBack()
    {
        stateMgr.ChangeStates(this, AniState.TurnBack);
    }
    public void Attack(int SkillID)
    {
        stateMgr.ChangeStates(this, AniState.Attack, SkillID);
    }
    public void Hit()
    {
        stateMgr.ChangeStates(this, AniState.Hit, null);
    }
    public void Die()
    {
        stateMgr.ChangeStates(this, AniState.Die, null);
    }
    #endregion
    public virtual void SetCtrl(Controller Controller)
    {
        this.controller = Controller;
    }
    //public virtual void SetEntityData<T>(BaseData<T> data)
    //{
    //    //子类覆写
    //}
    /// <summary>
    /// 返回实体数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public virtual BattleData GetEntityData()
    {
        // 你的代码
        return null;
    }
    public virtual Vector2 GetDirInput()
    {
        return Vector2.zero;
    }
    public virtual bool GetRunState()
    {
        return false;
    }
    public virtual void SetDir(Vector2 dir)
    {
        if (controller != null)
        {
            // 检查当前方向与上一次方向的差距
            float angleDifference = Vector2.SignedAngle(controller.Dir, dir);
            if (Mathf.Abs(angleDifference) > 90f)
            {
                // SetMove(false);
                TurnBack();
                //  Debug.Log("转向");
            }
            controller.Dir = dir;
        }
    }
    public virtual void SetTrans(double time, Vector3 Pos, Vector3 Rot)
    {
        if (controller != null)
        {

            controller.SetTrans(time, Pos, Rot);
        }
    }
    public virtual void SetDodge()
    {
    }
    public virtual void SetCritical(int hurt)
    {
    }
    public virtual void SetHurt(int hurt)
    {

    }
    public virtual EntityType SetType()
    {
        return CurrentEntityType;
    }
    #region 获取表现层数据
    public virtual Vector3 GetPos()
    {
        if (controller != null)
        {
            return controller.transform.position;
        }
        return Vector3.zero;
    }
    public virtual GameObject GetGameObject()
    {
        if (controller != null)
        {
            return controller.gameObject;
        }
        return null;
    }
    public virtual Controller GetController()
    {
        if (controller != null)
        {
            return controller;
        }
        return null;
    }
    public virtual Transform GetTrans()
    {
        if (controller != null)
        {
            return controller.transform;
        }
        return null;
    }
    public virtual Vector3 GetRemoteInput()
    {
        return controller.eulerAngles;
    }
    public virtual void SetRotation(Vector2 dir)
    {
        if (controller != null)
        {
            controller.SetAtkRotationLocal(dir);
        }
    }
    public virtual void SetRotation(Vector3 dir)
    {
        if (controller != null)
        {
            controller.SetAtkRotationRetemo(dir);
        }
    }
    #endregion
    public virtual void SkillAttack(int skillID)
    {
        skillMgr.SkillAttack(this, skillID);
    }
    public virtual void SetDamage(PlayerData playerData, RspDamage rspDamage)
    {
        skillMgr.SetDamage(this, playerData, rspDamage);
    }
    public virtual void SetSkillMoveSate(bool isSkillMove)
    {
        canControl = isSkillMove;
        if (controller != null)
        {
            controller.isSkillRos = isSkillMove;
        }
    }
    /// <summary>
    /// 取出技能连招
    /// </summary>
    public virtual void ExitCurtSkill()
    {
        canControl = true;
        if (curtSkillCfg != null)
        {
            if (curtSkillCfg.isBreak)
            {
                entityState = EntityState.None;
            }
            double nowAtkTime = TimerSvc.Instance.GetNwTime();
            double lastAtkTime = BattleSys.instance.GetBattleMgr().nowAtkTime;
            if (comboQue.Count > 0 && nowAtkTime - lastAtkTime < Constants.ComboSpace)
            {
                nextSkillID = comboQue.Dequeue();
            }
            else
            {
                comboQue.Clear();
                nextSkillID = 0;
            }
        }
        //SetAction(Constants.ActionDefault);
    }
    /// <summary>
    /// 设置移动动画
    /// </summary>
    /// <param name="Velocity"></param>
    /// <param name="IsRun"></param>
    public virtual void SetVelocity(float Velocity)
    {
        if (controller != null)
        {
            controller.SetVelocity(Velocity);
        }
    }
    public virtual AniPlayerState GetWalkOrRunState()
    {
        return controller.GetWalkOrRunState();
    }
    //public virtual void SetMove(bool isMove)
    //{
    //    if (controller != null)
    //    {
    //        controller.isMove = isMove;
    //    }
    //}
    public virtual void SetHasInput(bool hasInput)
    {
        if (controller != null)
        {
            controller.SetHasInput(hasInput);
        }

    }
    public virtual bool GetHasInput()
    {
        if (controller != null)
        {
            return controller.GetHasInput();
        }
        return false;

    }
    public AnimationClip[] GetAniClips()
    {
        if (controller != null)
        {
            return controller.animator.runtimeAnimatorController.animationClips;
        }
        return null;
    }
    public void SetAniCrossFade(string name, float time)
    {
        if (controller != null)
        {
            controller.SetAniCrossFade(name, time);
        }
    }
    public void SetEvade(bool isEvade)
    {
        if (controller != null)
        {
            controller.SetEvade(isEvade);
        }
    }
    public void SetAniPlay(string name)
    {
        if (controller != null)
        {
            controller.SetAniPlay(name);
        }
    }
    public AnimatorStateInfo GetCurrentAniStateInfo()
    {
        if (controller != null)
        {
            return controller.animator.GetCurrentAnimatorStateInfo(0);
        }
        return default;

    }
    /// <summary>
    /// 设置状态动画
    /// </summary>
    /// <param name="act"></param>
    public virtual void SetAction(int act)
    {
        if (controller != null)
        {
            controller.SetAction(act);
        }
    }
    public virtual AniPlayerState GetAniState()
    {
        if (controller != null)
        {
            return controller.GetAction();
        }
        return AniPlayerState.IdleAFK;
    }
    public virtual void SetmoveDistance(float moveDistance)
    {
        if (controller != null)
        {
            //controller.moveDistance = moveDistance;
        }
    }
    public virtual void SetFx(string name, float destroy = 0)
    {
        if (controller != null)
        {
            controller.SetFx(name, destroy);
        }
    }
    public virtual void CreateFx(string name, float destroy = 0)
    {
        if (controller != null)
        {
            controller.CreateFx(name, destroy);
        }
    }
    public virtual void SetSkillMoveState(bool move, float skillSpeed = 0f)
    {
        if (controller != null)
        {
            controller.SetSkillMoveState(move, skillSpeed);
        }
    }
    /// <summary>
    /// 删除移动回调
    /// </summary>
    /// <param name="tid"></param>
    public virtual void DeleMoveCB(int tid)
    {
        int index = -1;
        for (int i = 0; i < SkillMoveCB.Count; i++)
        {
            if (tid == SkillMoveCB[i])
            {
                index = i;
                break;
            }
        }
        if (index != -1)
        {
            SkillMoveCB.RemoveAt(index);
        }
    }
    /// <summary>
    /// 删除特效回调
    /// </summary>
    /// <param name="tid"></param>
    public virtual void DeleFxCB(int tid)
    {
        int index = -1;
        for (int i = 0; i < SkillFxCB.Count; i++)
        {
            if (tid == SkillFxCB[i])
            {
                index = i;
                break;
            }
        }
        if (index != -1)
        {
            SkillFxCB.RemoveAt(index);
        }

    }
    public virtual void DeleActionCB(int tid)
    {
        int index = -1;
        for (int i = 0; i < SkillActionCBLst.Count; i++)
        {
            if (tid == SkillActionCBLst[i])
            {
                index = i;
                break;
            }
        }
        if (index != -1)
        {
            SkillActionCBLst.RemoveAt(index);
        }
    }
    public virtual void DelePlayerCB()
    {
        for (int i = 0; i < SkillActionCBLst.Count; i++)
        {
            int tid = SkillActionCBLst[i];
            TimerSvc.Instance.DeleteTimeTask(tid);
        }
        for (int i = 0; i < SkillMoveCB.Count; i++)
        {
            int tid = SkillMoveCB[i];
            TimerSvc.Instance.DeleteTimeTask(tid);
        }
        //清空连招
        if (nextSkillID != 0 || comboQue.Count > 0)
        {
            nextSkillID = 0;
            comboQue.Clear();
            battleMgr.lastAtkTime = 0;
            battleMgr.comboIndex = 0;
        }
        //清空技能表现回调
        if (skEndCB != -1)
        {
            TimerSvc.Instance.DeleteTimeTask(skEndCB);
            skEndCB = -1;
        }
        SkillMoveCB.Clear();
        SkillActionCBLst.Clear();
    }
    public virtual void Destroy()
    {
        if (controller != null)
        {
            controller.DestroySelf();
        }
    }
    public virtual void TickAILogic()
    {

    }
}