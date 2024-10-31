/****************************************************
    文件：BaseData
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-22 15:52:09
	功能：数据解析
*****************************************************/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
public class personCfg : BaseData<personCfg>
{
    public string type;
    public string Hard;
    public int HP;
    public int Mana;
    public int Power;
    public int aura;
    public int ruvia;
    public int crystal;
    public int ad;
    public int ap;
    public int addef;
    public int adpdef;
    public int dodge;
    public float practice;
    public int critical;
    public string Intro;
    public string fight;
    public string Prefab;
    public int PreText;
    public List<int> NormalAtkList;
    public List<int> SkillList;
    public int BaseExp;
    public float ExpMul;
}
public class MapCfg : BaseData<MapCfg>
{
    public string mapName;
    public string sceneName;
    public int power;
    public Vector3 playerBornPos;
    public Vector3 playerBornRote;
    public string monsterLst;
    public int exp;
    public int aura;
    public Vector3 CameraFollowAndRotatePos;
    public Vector3 CameraFollowAndRotateRote;
    public Vector3 CameraUpAndDownPos;
    public Vector3 CameraUpAndDownRote;
    public Vector3 CameraZoomContainerPos;
    public Vector3 CameraZoomContainerRote;
}
public class TalentCfg : BaseData<TalentCfg>
{
    public string Name; //天赋名字
    public string Info; //天赋作用介绍
    public int MaxLevel; //天赋最大等级
    public string Attribute; //属性 天赋对应增加的词条(Hp,Atk...)
    public string BackGround; //背景介绍
    public float Value; //基础1级数值(与等级×相关系数 = 对应等级数值)
}
public class ItemCfg : BaseData<ItemCfg>
{
    public string mName;  //物品名字
    public string mInfo;  //物品介绍
    public string mImg;   //物品图标
    public float Price;  //物价价格
    public ItemType type;  //物品类型

    public int quantity; //数量

}
public class TaskCfg : BaseData<TalentCfg>
{
    public int npcID;
    public string dilogArr;
    public int aura;
    public int ruvia;
    public int crystal;
    public int exp;
}
public class TaskRewardCfg : BaseData<TaskRewardCfg>
{
    public int Value;//每日奖励的目标值
    public List<TaskRewardItem> rewardItems;
}
public class TaskRewardItem
{
    public int ItemID;  //奖励物品ID
    public int Count;  //奖励物品数量
}
public class TaskDailyCfg : BaseData<TaskDailyCfg>
{
    public string mTitle;//任务标题
    public string mTask;//任务内容
    public int Active;
    public int Count;
}
public class SkillCfg : BaseData<SkillCfg>
{
    public string mName;//技能名
    public int cdTime;//冷却时间
    public int skilltime;//技能持续时间
    public int aniAction;//技能状态
    public List<int> fxList;//技能特效名
    public bool isCombo;//技能类型是否是连招
    public bool isCollide;//判断技能是否被穿透
    public bool isBreak;//判断技能是否允许被打断
    public SkillType type;
    public DamageType damageType;//技能属性
    public List<int> skillMoveList;//技能的多段移动
    public List<int> skillActionLst;//技能多种状态
    public List<int> SkillDamageLst;//技能多段伤害
}
public class SkillFxCfg : BaseData<SkillFxCfg>
{
    public string name;
    public int delayTime;
    public int ContineTime;
}
public class SkillMoveCfg : BaseData<SkillMoveCfg>
{
    public int delayTime;
    public int moveTime;
    public float moveDis;
    public float moveDir;
}
public class SkillActionCfg : BaseData<SkillActionCfg>
{
    public int delayTime;
    public float radius;
    public float angle;
}
public class BaseData<T>
{
    public int ID;
}
public class DataTrans
{
    public double time;
    public Vector3 pos;
    public Vector3 Rot;
}
public class ABInfo
{
    public string name;
    public long size;
    public string md5;
    public ABInfo(string name, long size, string md5)
    {
        this.name = name;
        this.size = size;
        this.md5 = md5;
    }
}
public class EnemyCfg : BaseData<EnemyCfg>
{
    public string name;
    public EnemyType type;
    public int hp;
    public int ad;
    public int ap;
    public int addef;
    public int dodge;
    public int critical;
    public int chasedistance;
    public int backdistance;
    public List<int> skills;  //技能组

}
public class BattleData : BaseData<BattleData>
{
    public int hp;
    public int ad;
    public int ap;
    public int addef;
    public int dodge;
    public int critical;
}
public enum TalentQuality
{
    garbage,//垃圾，灰
    ordinary,//普通，绿
    rare,//稀有，粉
    cherish,//珍惜，蓝
    epic,//史诗，紫
    legend,//传说，黄
    mythology,//神话,红
}
public enum SkillType
{
    /// <summary>
    /// 近战
    /// </summary>
    melee,
    /// <summary>
    /// 远程
    /// </summary>
    remote,
}
public enum ItemType
{
    consume,//消耗品
    equip,//武器,装备
    material,//材料(可堆叠)
    all,//全部
}
public enum EnemyType
{
    None,
    garbage = 1,//垃圾，灰
    ordinary = 2,//普通，绿
    rare = 3,//稀有，粉
    cherish = 4,//珍惜，蓝
    epic = 5,//史诗，紫
    legend = 6,//传说，黄
    mythology = 7,//神话,红
}