using ComNet;
using ProtoBuf;
namespace CommonNet
{
[ProtoContract]
    public class GameMsg : TraMsg
    {
        #region 登录注册相关
        [ProtoMember(6)]
        public ReqLogin? reqLogin;//请求登录
        [ProtoMember(7)]
        public RspLogin? rspLogin;
        [ProtoMember(8)]
        public ReqRegister? reqRegister;//请求注册
        [ProtoMember(9)]
        public RspRegister? rspRegister;
        [ProtoMember(10)]
        public ReqCreateGame? reqCreateGame; //玩家创建角色请求
        [ProtoMember(11)]
        public RspCreateGame? rspCreateGame;
        #endregion
        #region 商店背包相关
        [ProtoMember(12)]
        public ReqShop? reqShop;//商店请求
        [ProtoMember(13)]
        public RspShop? rspShop;
        [ProtoMember(14)]
        public ReqBag? reqBag;//背包请求
        [ProtoMember(15)]
        public RspBag? rspBag;
        #endregion
        [ProtoMember(16)]
        public ReqTask? reqTask;//任务请求
        [ProtoMember(17)]
        public RspTask? rspTask;

        [ProtoMember(18)]
        public RspCreatePlayer? rspCreatePlayer;
        [ProtoMember(19)]
        public RspDeletePlayer? rspDeletePlayer;
        [ProtoMember(20)]
        public ReqEnterPVP? reqEnterPVP;
        [ProtoMember(21)]
        public RspEnterPVP? rspEnterPVP;
        [ProtoMember(22)]
        public ReqExitPVP? reqExitPVP;
        [ProtoMember(23)]
        public RspExitPVP? rspExitPVP;
        [ProtoMember(24)]
        public ReqTransform? reqTransform;
        [ProtoMember(25)]
        public RspTransform? rspTransform;
        [ProtoMember(26)]
        public ReqPlayerState? reqPlayerState;//玩家状态请求
        [ProtoMember(27)]
        public RspPlayerState? rspPlayerState;
        [ProtoMember(28)]
        public ReqDamage? reqDamage;
        [ProtoMember(29)]
        public RspDamage? rspDamage;
        [ProtoMember(30)]
        public ReqRevive? reqRevive;
        [ProtoMember(31)]
        public RspRevive? rspRevive;
        [ProtoMember(32)]
        public ReqRecover? reqRecover;
        [ProtoMember(33)]
        public RspRecover? rspRecover;
        #region 好友请求与响应
        // 新增的好友请求与响应
        [ProtoMember(34)]
        public ReqSearchFriend? reqSearchFriend; // 好友搜索请求
        [ProtoMember(35)]
        public RspSearchFriend? rspSearchFriend; // 好友搜索响应
        [ProtoMember(36)]
        public ReqAddFriend? reqAddFriend; // 好友申请请求
        [ProtoMember(37)]
        public RspAddFriend? rspAddFriend; // 好友申请响应
        [ProtoMember(38)]
        public ReqDelFriend? reqDelFriend; // 好友删除请求
        [ProtoMember(39)]
        public RspDelFriend? rspDelFriend; // 好友删除响应
        [ProtoMember(40)]
        public ReqFriendGift? reqFriendGift; // 好友赠送请求
        [ProtoMember(41)]
        public RspFriendGift? rspFriendGift; // 好友赠送响应
        [ProtoMember(42)]
        public ReqFriendAddConfirm? reqFriendAddConfirm; // 好友添加确认请求
        [ProtoMember(43)]
        public RspFriendAddConfirm? rspFriendAddConfirm; // 好友添加确认响应
        #endregion
        #region 每日任务的请求与响应
        [ProtoMember(44)]
        public ReqDailyTask? reqDailyTask; // 领取每日任务奖励请求
        [ProtoMember(45)]
        public RspDailyTask? rspDailyTask; // 领取每日任务奖励响应
        [ProtoMember(46)]
        public ReqRewardTask? reqRewardTask; // 领取活跃奖励任务请求
        [ProtoMember(47)]
        public RspRewardTask? rspRewardTask; // 领取活跃奖励任务响应
        #endregion
        #region 系统相关
        [ProtoMember(48)]
        public HeartbeatMessage? heartbeatMessage;
        [ProtoMember(49)]
        public ReqSystemTimeMessage? reqSystemTimeMessage;
        [ProtoMember(50)]
        public RspSystemTimeMessage? rspSystemTimeMessage;
        [ProtoMember(51)]
        public SystemSessionID? systemSessionID;
        #endregion
        #region 天赋相关请求与响应
        [ProtoMember(52)]
        public ReqTalentUp? reqTalentUp;
        [ProtoMember(53)]
        public RspTalentUp? rspTalentUp;
        [ProtoMember(54)]
        public ReqChangeTalent? reqChangeTalent;
        [ProtoMember(55)]
        public RspChangeTalent? rspChangeTalent;
        #endregion
    }
    #region 系统相关
    #region 心跳机制请求
[ProtoContract]
    public class HeartbeatMessage
    {
        [ProtoMember(1)]
        public string Heartbeat = "Heartbeat";
    }
    #endregion
    #region 系统时间请求
[ProtoContract]
    public class ReqSystemTimeMessage
    {
        [ProtoMember(1)]
        public float LocalTime;
    }
[ProtoContract]
    public class RspSystemTimeMessage
    {
        [ProtoMember(1)]
        public float LocalTime;
        [ProtoMember(2)]
        public long ServerTime;
    }
[ProtoContract]
    public class SystemSessionID
    {
        [ProtoMember(1)]
        public int SessionID;
    }
    #endregion
    #endregion
    #region 登录注册相关
[ProtoContract]
    public class ReqLogin
    {
        [ProtoMember(1)]
        public string? acct;
        [ProtoMember(2)]
        public string? pass;
    }
[ProtoContract]
    public class RspLogin//玩家个人数据
    {
        [ProtoMember(1)]
        public PlayerData? playerData;
        [ProtoMember(2)]
        public List<PlayerData>? playerList;//其他玩家id数据
    }

[ProtoContract]
    public class ReqRegister
    {
        [ProtoMember(1)]
        public string? acct;
        [ProtoMember(2)]
        public string? pass;
    }
[ProtoContract]
    public class RspRegister
    {
        [ProtoMember(1)]
        public bool isSucc;
    }
[ProtoContract]
    public class ReqCreateGame
    {
        [ProtoMember(1)]
        public int id;
        [ProtoMember(2)]
        public string? name;

    }
[ProtoContract]
    public class RspCreateGame
    {
        [ProtoMember(1)]
        public PlayerData? playerData;
        [ProtoMember(2)]
        public List<PlayerData>? playerDataList;
    }
    #endregion

    #region 城镇系统请求

    #region 商店相关请求与响应
[ProtoContract]
    public class ReqShop
    {
        [ProtoMember(1)]
        public BuyType buyType;
        [ProtoMember(2)]
        public int id;
        [ProtoMember(3)]
        public int count;
    }
[ProtoContract]
    public class RspShop
    {
        [ProtoMember(1)]
        public List<BagList> Bag;
        [ProtoMember(2)]
        public float aura;
        [ProtoMember(3)]
        public float ruvia;
        [ProtoMember(4)]
        public float crystal;
        [ProtoMember(5)]
        public List<DailyTask> dailyTasks;//每日任务数据
    }
[ProtoContract]
    public class RspBag
    {
        [ProtoMember(1)]
        public PlayerData playerData;
    }

    #endregion


    #region 好友请求与响应

[ProtoContract]
    public class ReqSearchFriend//好友搜索
    {
        [ProtoMember(1)]
        public string name;
    }
[ProtoContract]
    public class RspSearchFriend
    {
        [ProtoMember(1)]
        public FriendItem Friend;//好友数据
    }
[ProtoContract]
    public class ReqAddFriend//好友申请
    {
        [ProtoMember(1)]
        public int id;//对方id
        [ProtoMember(2)]
        public string name;//对方名字
    }
[ProtoContract]
    public class RspAddFriend
    {
        [ProtoMember(1)]
        public float aura;//角色星晶
        [ProtoMember(2)]
        public float ruvia;//角色云晶
        [ProtoMember(3)]
        public float crystal; //角色彩晶
        [ProtoMember(4)]
        public List<FriendItem> FriendList;//好友列表
        [ProtoMember(5)]
        public List<FriendItem> AddFriendList;//好友申请列表
        [ProtoMember(6)]
        public bool isSucc;//是否申请成功
    }
[ProtoContract]
    public class ReqDelFriend//好友删除
    {
        [ProtoMember(1)]
        public int id;//对方id
        [ProtoMember(2)]
        public string name;//对方名字
    }
[ProtoContract]
    public class RspDelFriend
    {
        [ProtoMember(1)]
        public List<FriendItem> FriendList;//好友列表
    }
[ProtoContract]
    public class ReqFriendGift//好友赠送
    {
        [ProtoMember(1)]
        public int id;//好友id
        [ProtoMember(2)]
        public string name;//好友名字
        [ProtoMember(3)]
        public BuyType buyType;//赠送类型
        [ProtoMember(4)]
        public int count;//数量
    }
[ProtoContract]
    public class RspFriendGift
    {
        [ProtoMember(1)]
        public List<BagList> Bag;//背包数据
        [ProtoMember(2)]
        public float aura;//角色星晶
        [ProtoMember(3)]
        public float ruvia;//角色云晶
        [ProtoMember(4)]
        public float crystal; //角色彩晶
    }
[ProtoContract]
    public class ReqFriendAddConfirm//好友添加确认
    {
        [ProtoMember(1)]
        public int id;//对方id
        [ProtoMember(2)]
        public string name;//对方名字
        [ProtoMember(3)]
        public bool isAgree;//true同意，false拒绝
    }
    /// <summary>
    /// 好友添加确认响应
    /// </summary>
[ProtoContract]
    public class RspFriendAddConfirm
    {
        [ProtoMember(1)]
        public bool isAgree;//同意或失败
        [ProtoMember(2)]
        public List<FriendItem> FriendList;//好友列表
        [ProtoMember(3)]
        public List<FriendItem> AddFriendList;//好友申请列表
    }
    #endregion

    #region 每日任务相关请求与响应
[ProtoContract]
    public class ReqTask
    {
        [ProtoMember(1)]
        public int Taskid;
    }
[ProtoContract]
    public class RspTask
    {
        [ProtoMember(1)]
        public int Taskid;
        [ProtoMember(2)]
        public float aura;
        [ProtoMember(3)]
        public int lv;
        [ProtoMember(4)]
        public int exp;
    }
[ProtoContract]
    public class ReqDailyTask//根据ID领取奖励
    {
        [ProtoMember(1)]
        public int TaskID;

    }
[ProtoContract]
    public class RspDailyTask
    {
        [ProtoMember(1)]
        public List<DailyTask> dailyTasks;//每日任务数据
    }
[ProtoContract]
    public class ReqRewardTask//根据ID领取活跃奖励任务
    {
        [ProtoMember(1)]
        public int RewardTaskID;
    }
[ProtoContract]
    public class RspRewardTask
    {
        [ProtoMember(1)]
        public RewardTask rewardTask;
        [ProtoMember(2)]
        public List<BagList>? Bag;//背包数据
    }
[ProtoContract]
    public class RewardTask
    {
        [ProtoMember(1)]
        public List<int>? TaskProgress;//每日任务领取进度0为未领取，1为领取第一个奖励
        [ProtoMember(2)]
        public DateTime LastTime;//上次更新时间
    }
    #endregion

    #region 天赋相关请求与响应

    /// <summary>
    /// 天赋升级请求
    /// </summary>
[ProtoContract]
    public class ReqTalentUp
    {
        [ProtoMember(1)]
        public int TalentId;
        [ProtoMember(2)]
        public int NextLevel;
    }
    /// <summary>
    /// 天赋升级响应
    /// </summary>
[ProtoContract]
    public class RspTalentUp
    {
        [ProtoMember(1)]
        public bool IsUpSuccess;
        //public int ruvia; //升级后剩余的云晶 用于比对客户端数据
        [ProtoMember(2)]
        public List<Talent>? talents; //最新的天赋数据
        [ProtoMember(3)]
        public BattleData? battleData;
        [ProtoMember(4)]
        public bool NeedUpdate; //需要更新战斗数据
    }
    /// <summary>
    /// 天赋切换请求
    /// </summary>
[ProtoContract]
    public class ReqChangeTalent
    {
        [ProtoMember(1)]
        public List<int>? CurrTalents;
        //public BattleData battleData;
    }

    /// <summary>
    /// 天赋切换响应
    /// </summary>
[ProtoContract]
    public class RspChangeTalent
    {
        [ProtoMember(1)]
        public bool IsChangeSuccess;
        [ProtoMember(2)]
        public PlayerData? playerData;
    }
    #endregion
    #endregion

    #region PVP战斗请求

[ProtoContract]
    public class ReqEnterPVP
    {
        [ProtoMember(1)]
        public int id;
    }
[ProtoContract]
    public class RspEnterPVP
    {
        [ProtoMember(1)]
        public bool isSucc;
        [ProtoMember(2)]
        public List<PlayerData> PlayerDataList;
    }
[ProtoContract]
    public class ReqExitPVP
    {
        [ProtoMember(1)]
        public int id;
    }
[ProtoContract]
    public class RspExitPVP
    {
        [ProtoMember(1)]
        public bool isSucc;
    }
    #endregion


    #region 战斗同步请求

[ProtoContract]
    public class ReqTransform
    {
        [ProtoMember(1)]
        public int playerID;
        [ProtoMember(2)]
        public long time;
        [ProtoMember(3)]
        public bool isShoolr;
        [ProtoMember(4)]
        public float Pos_X;
        [ProtoMember(5)]
        public float Pos_Y;
        [ProtoMember(6)]
        public float Pos_Z;
        [ProtoMember(7)]
        public float Rot_X;
        [ProtoMember(8)]
        public float Rot_Y;
        [ProtoMember(9)]
        public float Rot_Z;
        [ProtoMember(10)]
        public float speed;
    }
[ProtoContract]
    public class RspTransform
    {
        [ProtoMember(1)]
        public int playerID;
        [ProtoMember(2)]
        public long time;
        [ProtoMember(3)]
        public bool isShoolr;
        [ProtoMember(4)]
        public float Pos_X;
        [ProtoMember(5)]
        public float Pos_Y;
        [ProtoMember(6)]
        public float Pos_Z;
        [ProtoMember(7)]
        public float Rot_X;
        [ProtoMember(8)]
        public float Rot_Y;
        [ProtoMember(9)]
        public float Rot_Z;
        [ProtoMember(10)]
        public float speed;
    }

[ProtoContract]
    public class ReqPlayerState
    {
        [ProtoMember(1)]
        public int PlayerID;
        [ProtoMember(2)]
        public int AniState;
        [ProtoMember(3)]
        public int SkillID;
        [ProtoMember(4)]
        public long LocalTime;
    }
[ProtoContract]
    public class RspPlayerState
    {
        [ProtoMember(1)]
        public int PlayerID;
        [ProtoMember(2)]
        public int AniState;
        [ProtoMember(3)]
        public int SkillID;
        [ProtoMember(4)]
        public long LocalTime;
    }
[ProtoContract]
    public class RspCreatePlayer
    {
        [ProtoMember(1)]
        public PlayerData playerData;
    }
[ProtoContract]
    public class RspDeletePlayer
    {
        [ProtoMember(1)]
        public int PlayerID;

    }
[ProtoContract]
    public class ReqDamage
    {
        [ProtoMember(1)]
        public int id;
        [ProtoMember(2)]
        public int damageState;
        [ProtoMember(3)]
        public int Damage;
    }
[ProtoContract]
    public class RspDamage
    {
        [ProtoMember(1)]
        public int id;
        [ProtoMember(2)]
        public int hp;
        [ProtoMember(3)]
        public int damageState;
        [ProtoMember(4)]
        public int Damage;
    }
    /// <summary>
    /// 玩家复活请求并返回主页
    /// </summary>
[ProtoContract]
    public class ReqRecover
    {
        [ProtoMember(1)]
        public int id;
        [ProtoMember(2)]
        public bool isRevive;//True复活，False死亡返回主页
    }
    /// <summary>
    /// 玩家复活响应并返回主页
    /// </summary>
[ProtoContract]
    public class RspRecover
    {
        [ProtoMember(1)]
        public int id;
        [ProtoMember(2)]
        public bool isRevive;
        [ProtoMember(3)]
        public int Hp;//生命
        [ProtoMember(4)]
        public int Hpmax;//生命上限
    }
[ProtoContract]
    public class ReqRevive
    {
        [ProtoMember(1)]
        public int id;
    }

[ProtoContract]
    public class RspRevive
    {
        [ProtoMember(1)]
        public int id;
        [ProtoMember(2)]
        public int hp;
    }

    #endregion

    #region 自定义数据/枚举
[ProtoContract]
    public class ReqBag//使用资源
    {
        [ProtoMember(1)]
        public int id;
        [ProtoMember(2)]
        public int count;
    }
[ProtoContract]
    public class PlayerData
    {
        [ProtoMember(1)]
        public int id;
        [ProtoMember(2)]
        public string? name;//角色名称
        [ProtoMember(3)]
        public int type;//角色类型
        [ProtoMember(4)]
        public int exp;//角色经验
        [ProtoMember(5)]
        public int level;//角色等级
        [ProtoMember(6)]
        public int power;//角色体力
        [ProtoMember(7)]
        public int powerMax;//体力上限
        [ProtoMember(8)]
        public float aura;//角色星晶
        [ProtoMember(9)]
        public float ruvia;//角色云晶
        [ProtoMember(10)]
        public float crystal; //角色彩晶
        [ProtoMember(11)]
        public int Hp;//生命
        [ProtoMember(12)]
        public int Hpmax;//生命上限
        [ProtoMember(13)]
        public int Mana;//法力
        [ProtoMember(14)]
        public int ManaMax;//法力上限
        [ProtoMember(15)]
        public int ad;//物攻
        [ProtoMember(16)]
        public int ap;//魔攻
        [ProtoMember(17)]
        public int addef;//物抗
        [ProtoMember(18)]
        public int apdef;//魔抗
        [ProtoMember(19)]
        public int dodge;//闪避概率
        [ProtoMember(20)]
        public int critical;//暴击概率
        [ProtoMember(21)]
        public float practice;//修炼速度
        [ProtoMember(22)]
        public RewardTask? rewardTask;//每日任务进度
        [ProtoMember(23)]
        public List<DailyTask>? dailyTasks;//每日任务数据
        [ProtoMember(24)]
        public List<int>? TalentID;//当前选中的天赋ID
        [ProtoMember(25)]
        public List<Talent>? TalentsData; //所有天赋数据
        [ProtoMember(26)]
        public List<BagList>? Bag;//背包数据
        [ProtoMember(27)]
        public List<FriendItem>? FriendList;//好友列表
        [ProtoMember(28)]
        public List<FriendItem>? AddFriendList;//好友申请列表
        [ProtoMember(29)]
        public int Taskid;
        [ProtoMember(30)]
        public int SkillID;
        [ProtoMember(31)]
        public int AniState;
        [ProtoMember(32)]
        public float Pos_X;
        [ProtoMember(33)]
        public float Pos_Y;
        [ProtoMember(34)]
        public float Pos_Z;
        [ProtoMember(35)]
        public float Rot_X;
        [ProtoMember(36)]
        public float Rot_Y;
        [ProtoMember(37)]
        public float Rot_Z;
    }
[ProtoContract]
    public class BagList
    {
        [ProtoMember(1)]
        public int GoodsID;
        [ProtoMember(2)]
        public int count;
    }

    /// <summary>
    /// 战斗相关数据
    /// </summary>
[ProtoContract]
    public class BattleData
    {
        [ProtoMember(1)]
        public int Hp;//生命
        [ProtoMember(2)]
        public int Hpmax;//生命上限
        [ProtoMember(3)]
        public int Mana;//法力
        [ProtoMember(4)]
        public int ManaMax;//法力上限
        [ProtoMember(5)]
        public int ad;//物攻
        [ProtoMember(6)]
        public int ap;//魔攻
        [ProtoMember(7)]
        public int addef;//物抗
        [ProtoMember(8)]
        public int apdef;//魔抗
        [ProtoMember(9)]
        public int dodge;//闪避概率
        [ProtoMember(10)]
        public int critical;//暴击概率
    }

[ProtoContract]
    public class Talent
    {
        [ProtoMember(1)]
        public int TalentID;
        [ProtoMember(2)]
        public int Level;
    }
[ProtoContract]
    public class FriendItem
    {
        [ProtoMember(1)]
        public int id;
        [ProtoMember(2)]
        public string? type;
        [ProtoMember(3)]
        public string? name;
        [ProtoMember(4)]
        public int level;
        [ProtoMember(5)]
        public float aura;//角色星晶
        [ProtoMember(6)]
        public float ruvia;//角色云晶
        [ProtoMember(7)]
        public float crystal; //角色彩晶
        [ProtoMember(8)]
        public List<int>? FriendList;//好友id列表
        [ProtoMember(9)]
        public List<int>? AddFriendList;//好友id申请列表
    }

[ProtoContract]
    public class DailyTask
    {
        [ProtoMember(1)]
        public int TaskID;//每日任务id
        [ProtoMember(2)]
        public int TaskReward;//任务进度
        [ProtoMember(3)]
        public bool TaskFinish;//任务完成状态
    }

[ProtoContract]
    public enum BuyType
    {
        /// <summary>
        /// 星晶购买补给
        /// </summary>
        aura,
        /// <summary>
        /// 云晶购买装备
        /// </summary>
        ruvia,//角色云晶
        /// <summary>
        /// 彩晶购买材料
        /// </summary>
        crystal, //角色彩晶
    }
[ProtoContract]
    public enum DamageState
    {
        None = 0,
        Critical = 1,//暴击
        Dodge = 2//闪避
    }
[ProtoContract]
    public enum BagType
    {
        consume,//消耗品
        equip,//装备
        material,//材料
    }
[ProtoContract]
    public enum DailyTaskType
    {
        /// <summary>
        /// 星晶购买补给
        /// </summary>
        aura,
        /// <summary>
        /// 云晶购买装备
        /// </summary>
        ruvia,//角色云晶
        /// <summary>
        /// 彩晶购买材料
        /// </summary>
        crystal, //角色彩晶
        consume,//消耗品
        equip,//装备
        material,//材料
    }
    #endregion
[ProtoContract]
    public enum CMD
    {
        None = 0,
        heartbeatMessage = 1,
        ReqSystemTimeMessage = 2,
        RspSystemTimeMessage = 3,
        SystemSessionID=4,
        ReqLogin = 101,//登录相关请求
        RspLogin = 102,//
        ReqRegister = 103,//注册相关请求
        RspRegister = 104,
        ReqCreateGame = 105,//创建角色请求
        RspCreateGame = 106,//

        ReqShop = 107,//商店
        RspShop = 108,
        ReqBag = 109,//背包
        RspBag = 110,
        ReqTask = 111,//任务
        RspTask = 112,
        RspCreatePlayer = 113,//创建角色
        RspDeletePlayer = 114,//删除角色
        // 新增的好友功能命令
        ReqSearchFriend = 123,//好友搜索请求
        RspSearchFriend = 124,//好友搜索响应
        ReqAddFriend = 125,//好友申请请求
        RspAddFriend = 126,//好友申请响应
        ReqDelFriend = 127,//好友删除请求
        RspDelFriend = 128,//好友删除响应
        ReqFriendGift = 129,//好友赠送请求
        RspFriendGift = 130,//好友赠送响应
        ReqFriendAddConfirm = 131,//好友添加确认请求
        RspFriendAddConfirm = 132,//好友添加确认响应
        ReqDailyTask = 133,//领取每日任务奖励请求
        RspDailyTask = 134,//领取每日任务奖励响应
        ReqRewardTask = 135,//领取活跃奖励任务请求
        RspRewardTask = 136,//领取活跃奖励任务响应
                            //天赋相关
        ReqTalentUp = 200, //天赋升级请求
        RspTalentUp = 201, //天赋升级响应
        ReqChangeTalent = 202, //天赋切换的请求
        RspChangeTalent = 203, //天赋切换的响应
        #region PVP战斗
        ReqEnterPVP = 300,
        RspEnterPVP = 301,
        ReqTransform = 302,//转发位置请求
        RspTransform = 303,//
        ReqState = 304,//玩家状态请求
        RspState = 305,
        ReqDamage = 306,//玩家伤害请求
        RspDamage = 307,
        ReqRevive = 308,
        RspRevive = 309,
        ReqExitPVP = 310,
        RspExitPVP = 311,
        ReqRecover = 312,
        RspRecover = 313,
        #endregion
    }
[ProtoContract]
    public enum Error
    {
        None = 0,
        RegisterError = 1001,//注册失败
        AcctExistError = 1002,//账号已存在
        LoginExistError = 1003,//不存在该账号
        LoginInvalidError = 1004,//账号或密码无效
        PerSonError = 1005,//该角色不存在
        TalentError = 1006,//天赋选择错误
        AcctUpdateError = 1007,//更新失败
        NotGoodError = 1008,//没有该物品
        NotAuraError = 1009,//星晶不足
        NotRuviaError = 1010,//云晶不足
        NotCrystalError = 1011,//彩晶不足
        TaskIDError = 1013,//任务id错误
        DamageError = 1014,//造成伤害异常
        NotFriendError = 1015,//好友不存在
        FriendMeError = 1016,//不能添加自己为好友
        FriendNameError = 1017,//该用户不存在
        FriendRequestExistError = 1021,//已经申请过该好友
        FriendExistError = 1022,//好友已存在
        FriendRequestError = 1023,//好友申请失败
        FriendAddConfirmError = 1024,//好友拒绝失败
        FriendDelError = 1025,//删除好友失败
        FriendGiftError = 1026,//赠送失败
        GoldNotEnoughError = 1027,//货币不足
        NameExistError = 1028,//名字已存在
        RewardTaskRefreshError = 1029, //本日任务已刷新
        RewardActiveNotTaskError = 1030, //活跃度不足领取错误
        RewardTaskError = 1031, //奖励已领取
        DailyTaskNotError = 1032, //没有该每日任务
        DailyTaskFinishError = 1033, //该每日任务未完成
        DailyTaskRewardError = 1034,  //该每日任务奖励领取失败
        RewardActiveError = 1035, //活跃度奖励领取失败
        MissRuviaTalentUp = 1050, //天赋升级失败 余额不足
        EnterPVPError = 1100, //进入PVP失败
        ExitPVPError = 1101, //退出PVP失败
        TransformError = 1102, //位置同步失败
        StateError = 1103, //玩家状态同步失败
    }


    #region 配置信息
[ProtoContract]
    public class IPCfg
    {

        [ProtoMember(1)]
        public const string srvIP = "10.201.17.142";


        [ProtoMember(2)]
        public const int srvPort = 17666;
    }
    #endregion
}
