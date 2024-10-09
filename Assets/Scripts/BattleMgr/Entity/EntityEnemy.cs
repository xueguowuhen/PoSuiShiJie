
/****************************************************
    文件：EntityEnemy.cs
	作者：Kong
    邮箱: 1785275942@qq.com
    日期：2024/6/18 9:23:38
	功能：敌人逻辑实体
*****************************************************/

using CommonNet;
using UnityEngine;

public class EntityEnemy : EntityBase
{
    #region 逻辑数据 后面可以由配置表配置？
    private float checkTime = 1f; //间隔x秒检测一次距玩家的长度
    private float checkCount = 0; //初始化计时器
    private float CheckBackTime = 3f;  //每3秒检测 是否需要脱战
    private float CheckBackCount = 0f;
    private int CheckIdleTime = 15;
    private int CheckIdleCount = 0;

    private float AtkTimeCount = 0;
    #endregion
    public EnemyCfg EnemyData;
    /// <summary>
    /// 怪物出生点
    /// </summary>
    public Vector3 BornPos = new Vector3(-45.15f, 0.52f, 43.34f); //后续由地图配置表设置
    private float BattleDis = 10;  //安全范围 决定了怪物追击到人物后 想要攻击时 保持多远的距离 以及进入战斗状态的距离
    private bool NeedBack = false;
    public void Init(EnemyCfg enemyCfg)
    {
        this.EnemyData = enemyCfg;
        currentAniState = AniState.Idle;
    }
    #region 技能相关 todo
    //private float MinDisSkill = 2; //获取怪物配置表中的所有技能后 自动赋值
    //private float MaxDisSkill = 3;
    //多技能实现思路: 根据怪物的所有技能配置表 获取最远距离的技能与最短技能的距离
    //检测攻击是 根据距离远近与该技能的释放频次 决定是否是释放远距离攻击或是再次追击敌人
    #endregion
    private float ChaseDis = 15;  //追击范围  该范围是基于怪物当前位置
    private float BackDis = 35;  //脱战范围 该范围是基于怪物出生点范围
    public bool RunAi = true;
    private float dis;
    private SkillCfg PreSkill; //预释放技能 根据条件切换 达成直接释放
    //怪物逻辑:不定时间(0~1秒内) 检测距离玩家的距离 检测是否进入追击范围
    public override void TickAILogic()
    {
        controller.SetLogic(currentAniState);//测试怪物状态
        if (!RunAi) //AI总开关
        {
            return;
        }
        #region 时间计算
        float delta = Time.deltaTime;
        checkCount += delta;
        CheckBackCount += delta;
        #endregion
        #region 由时间/数据 判断当前状态 切换逻辑状态
        bool CanFind = checkCount < checkTime || NeedBack; //判断是否可以察觉玩家
        bool CanBack = CheckBackCount > CheckBackTime || NeedBack; //判断是否满足返回
        if (CanFind)
        {
        }  //检测玩家距离 追击与战斗
        else//允许感知玩家
        {
            RefreshAiFind();
            if (currentAniState == AniState.Attack) return;
            if (dis > ChaseDis) //距离过远脱战
            {
                RemoteIdle();
            }
            else if (dis < 3f) //近距离 IDLE+Attack
            {
                AtkTimeCount += delta;
                AttackNear();
            }
            else if (dis < BattleDis) //中距离 Move+Attack
            {
                AtkTimeCount += delta;
                AttackMove();
            }
            else  //追击
            {
                Chase();
            }
        }
        if (CanBack)//检查返回时间
        {
            if (CalcDisFromBorn() > BackDis ||(CheckIdleCount >= CheckIdleTime && CalcDisFromBorn() > 3))
            {
                GotoBorn();
            }
            else if (CalcDisFromBorn() < 2) //在出生点附近 挂机
            {
                NeedBack = false;
                CheckIdleCount = 0;
                Idle();
            }
            if (!NeedBack)
            {
                CheckBackCount = 0;
                NeedBack = false;
            }
        } //检测是否可以脱战
        #endregion
    }

    private void GotoBorn()
    {
        SetDir(new Vector2(BornPos.x - GetPos().x, BornPos.z - GetPos().z).normalized);
        Run();
        NeedBack = true;
    }
    /// <summary>
    /// 追击
    /// </summary>
    private void Chase()
    {
        SetDir(CalcTarget());
        Run();
        CheckIdleCount = 0;
    }
    /// <summary>
    /// 近距离的攻击
    /// </summary>
    private void AttackNear()
    {
        Idle();
        SetDir(CalcTarget());
        CheckIdleCount = 0;
        Attack(false);
    }
    /// <summary>
    /// 移动的攻击
    /// </summary>
    private void AttackMove()
    {

        SetDir(CalcTarget());
        Move();
        Attack(true);
    }
    /// <summary>
    /// 距离过远的IDLE(脱战)
    /// </summary>
    public void RemoteIdle()
    {
        Idle();
        if (CalcDisFromBorn() > 2) { CheckIdleCount++; }
    }
    /// <summary>
    /// 攻击
    /// </summary>
    /// <param name="ismove">是否处于移动</param>
    public void Attack(bool ismove)
    {

        if (ismove)
        {
            if (AtkTimeCount > 0.3f)
            {
                ReleaseAttck();
                AtkTimeCount = 0;
            }
            return;
        }
        else
        {
            if (AtkTimeCount < 0.08f)
            {
                return;
            }
        }
        ReleaseAttck();
        AtkTimeCount = 0;
    }
    /// <summary>
    /// 刷新AI察觉玩家
    /// </summary>
    public void RefreshAiFind()
    {
        checkCount = 0;//重置时间
        checkTime = ComTools.RDInt(0, 10) * 1.0f * 100 / 1000;//随机检查时间
        dis = CalcDis();//计算目标距离
    }
    /// <summary>
    /// 怪物释放技能
    /// </summary>
    public void ReleaseAttck()
    {
        int rdskill = ComTools.RDInt(1, 10);
        if (rdskill > 5)
        {
            Attack(3001);
        }
        else
        {
            Attack(3002);
        }
    }

    #region 辅助工具 计算与玩家(后面可以替换成目标/最近的玩家)之间的数据
    public Vector2 CalcTarget()  //计算出朝向玩家的方向
    {
        EntityBase entityPlayer = battleMgr.entitySelfPlayer;
        return new Vector2(entityPlayer.GetPos().x - GetPos().x, entityPlayer.GetPos().z - GetPos().z).normalized;
    }
    /// <summary>
    /// 计算与目标的距离
    /// </summary>
    /// <returns></returns>
    public float CalcDis()
    {
        EntityBase entityPlayer = battleMgr.entitySelfPlayer;
        return Vector3.Distance(entityPlayer.GetPos(), GetPos());
    }
    public float CalcDisFromBorn()
    {
        return Vector3.Distance(GetPos(), BornPos);
    }
    #endregion
    public override void SetHurt(int hurt)
    {
        EnemyData.hp -= hurt;
        Debug.Log(hurt);
        if (EnemyData.hp < 0)
        {
            Die();
            RunAi = false;
        }
        else
        {
            Hit();
            RunAi = false;

            TimerSvc.Instance.AddTimeTask((id) => { RunAi = true; }, 1000);
        }
        Debug.Log(EnemyData.hp);

    }
    public override void SetCritical(int hurt)
    {
        EnemyData.hp -= hurt;
        Debug.Log(hurt);
        if (EnemyData.hp < 0)
        {
            Die();
            RunAi = false;
        }
        else
        {
            Hit();
            RunAi = false;

            TimerSvc.Instance.AddTimeTask((id) => { RunAi = true; }, 1000);
        }
        Debug.Log(EnemyData.hp);
    }
    public override void ExitCurtSkill()
    {
        currentAniState = AniState.Idle;
    }
    public override BattleData GetEntityData()
    {
        return new BattleData()
        {
            ID = EnemyData.ID,
            ad = EnemyData.ad,
            hp = EnemyData.hp,
            ap = EnemyData.ap,
            addef = EnemyData.addef,
            dodge = EnemyData.dodge,
            critical = EnemyData.critical,
        };
    }
}
