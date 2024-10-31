using ComNet;
namespace CommonNet
{
    public class GameMsg : TraMsg
    {
        public ReqLogin? reqLogin;//请求登录
        public RspLogin? rspLogin;
        public ReqRegister? reqRegister;//请求注册
        public RspRegister? rspRegister;
        public ReqCreateGame? reqCreateGame; //玩家创建角色请求
        public RspCreateGame? rspCreateGame;
        public ReqShop? reqShop;//商店请求
        public RspShop? rspShop;
        public ReqBag? reqBag;//背包请求
        public RspBag? rspBag;

        public ReqTask? reqTask;//任务请求
        public RspTask? rspTask;

        public RspCreatePlayer? rspCreatePlayer;
        public RspDeletePlayer? rspDeletePlayer;
        public ReqEnterPVP? reqEnterPVP;
        public RspEnterPVP? rspEnterPVP;
        public ReqExitPVP? reqExitPVP;
        public RspExitPVP? rspExitPVP;
        public ReqTransform? reqTransform;
        public RspTransform? rspTransform;
        public ReqPlayerState? reqPlayerState;//玩家状态请求
        public RspPlayerState? rspPlayerState;
        public ReqDamage? reqDamage;
        public RspDamage? rspDamage;
        public ReqRevive? reqRevive;
        public RspRevive? rspRevive;
        public ReqRecover? reqRecover;
        public RspRecover? rspRecover;
        #region 好友请求与响应
        // 新增的好友请求与响应
        public ReqSearchFriend? reqSearchFriend; // 好友搜索请求
        public RspSearchFriend? rspSearchFriend; // 好友搜索响应
        public ReqAddFriend? reqAddFriend; // 好友申请请求
        public RspAddFriend? rspAddFriend; // 好友申请响应
        public ReqDelFriend? reqDelFriend; // 好友删除请求
        public RspDelFriend? rspDelFriend; // 好友删除响应
        public ReqFriendGift? reqFriendGift; // 好友赠送请求
        public RspFriendGift? rspFriendGift; // 好友赠送响应
        public ReqFriendAddConfirm? reqFriendAddConfirm; // 好友添加确认请求
        public RspFriendAddConfirm? rspFriendAddConfirm; // 好友添加确认响应
        #endregion
        #region 每日任务的请求与响应
        public ReqDailyTask? reqDailyTask; // 领取每日任务奖励请求
        public RspDailyTask? rspDailyTask; // 领取每日任务奖励响应
        public ReqRewardTask? reqRewardTask; // 领取活跃奖励任务请求
        public RspRewardTask? rspRewardTask; // 领取活跃奖励任务响应
        public HeartbeatMessage? heartbeatMessage;
        #endregion
        #region 天赋相关请求与响应
        public ReqTalentUp? reqTalentUp;
        public RspTalentUp? rspTalentUp;
        public ReqChangeTalent? reqChangeTalent;
        public RspChangeTalent? rspChangeTalent;
        #endregion
    }
    #region 登录注册相关
    public class ReqLogin
    {
        public string? acct;
        public string? pass;
    }
    public class RspLogin//玩家个人数据
    {
        public PlayerData? playerData;
        public List<PlayerData>? playerList;//其他玩家id数据
    }

    public class ReqRegister
    {
        public string? acct;
        public string? pass;
    }
    public class RspRegister
    {
        public bool isSucc;
    }
    public class ReqCreateGame
    {
        public int id;
        public string? name;

    }
    public class RspCreateGame
    {
        public PlayerData? playerData;
        public List<PlayerData>? playerDataList;
    }
    #endregion

    #region 城镇系统请求

    #region 商店相关请求与响应
    public class ReqShop
    {
        public BuyType buyType;
        public int id;
        public int count;
    }
    public class RspShop
    {
        public List<BagList> Bag;
        public float aura;
        public float ruvia;
        public float crystal;
        public List<DailyTask> dailyTasks;//每日任务数据
    }
    public class RspBag
    {
        public PlayerData playerData;
    }

    #endregion


    #region 好友请求与响应

    public class ReqSearchFriend//好友搜索
    {
        public string name;
    }
    public class RspSearchFriend
    {
        public FriendItem Friend;//好友数据
    }
    public class ReqAddFriend//好友申请
    {
        public int id;//对方id
        public string name;//对方名字
    }
    public class RspAddFriend
    {
        public float aura;//角色星晶
        public float ruvia;//角色云晶
        public float crystal; //角色彩晶
        public List<FriendItem> FriendList;//好友列表
        public List<FriendItem> AddFriendList;//好友申请列表
        public bool isSucc;//是否申请成功
    }
    public class ReqDelFriend//好友删除
    {
        public int id;//对方id
        public string name;//对方名字
    }
    public class RspDelFriend
    {
        public List<FriendItem> FriendList;//好友列表
    }
    public class ReqFriendGift//好友赠送
    {
        public int id;//好友id
        public string name;//好友名字
        public BuyType buyType;//赠送类型
        public int count;//数量
    }
    public class RspFriendGift
    {
        public List<BagList> Bag;//背包数据
        public float aura;//角色星晶
        public float ruvia;//角色云晶
        public float crystal; //角色彩晶
    }
    public class ReqFriendAddConfirm//好友添加确认
    {
        public int id;//对方id
        public string name;//对方名字
        public bool isAgree;//true同意，false拒绝
    }
    /// <summary>
    /// 好友添加确认响应
    /// </summary>
    public class RspFriendAddConfirm
    {
        public bool isAgree;//同意或失败
        public List<FriendItem> FriendList;//好友列表
        public List<FriendItem> AddFriendList;//好友申请列表
    }
    #endregion

    #region 每日任务相关请求与响应
    public class ReqTask
    {
        public int Taskid;
    }
    public class RspTask
    {
        public int Taskid;
        public float aura;
        public int lv;
        public int exp;
    }
    public class ReqDailyTask//根据ID领取奖励
    {
        public int TaskID;

    }
    public class RspDailyTask
    {
        public List<DailyTask> dailyTasks;//每日任务数据
    }
    public class ReqRewardTask//根据ID领取活跃奖励任务
    {
        public int RewardTaskID;
    }
    public class RspRewardTask
    {
        public RewardTask rewardTask;
        public List<BagList>? Bag;//背包数据
    }
    public class RewardTask
    {
        public List<int>? TaskProgress;//每日任务领取进度0为未领取，1为领取第一个奖励
        public DateTime LastTime;//上次更新时间
    }
    #endregion

    #region 天赋相关请求与响应

    /// <summary>
    /// 天赋升级请求
    /// </summary>
    public class ReqTalentUp
    {
        public int TalentId;
        public int NextLevel;
    }
    /// <summary>
    /// 天赋升级响应
    /// </summary>
    public class RspTalentUp
    {
        public bool IsUpSuccess;
        //public int ruvia; //升级后剩余的云晶 用于比对客户端数据
        public List<Talent>? talents; //最新的天赋数据
        public BattleData? battleData;
        public bool NeedUpdate; //需要更新战斗数据
    }
    /// <summary>
    /// 天赋切换请求
    /// </summary>
    public class ReqChangeTalent
    {
        public List<int>? CurrTalents;
        //public BattleData battleData;
    }

    /// <summary>
    /// 天赋切换响应
    /// </summary>
    public class RspChangeTalent
    {
        public bool IsChangeSuccess;
        public PlayerData? playerData;
    }
    #endregion
    #endregion

    #region PVP战斗请求

    public class ReqEnterPVP
    {
        public int id;
    }
    public class RspEnterPVP
    {
        public bool isSucc;
        public List<PlayerData> PlayerDataList;
    }
    public class ReqExitPVP
    {
        public int id;
    }
    public class RspExitPVP
    {
        public bool isSucc;
    }
    #endregion

    #region 心跳机制请求
    public class HeartbeatMessage
    {
        public string Heartbeat = "Heartbeat";
    }
    #endregion

    #region 战斗同步请求

    public class ReqTransform
    {
        public int playerID;
        public double time;
        public bool isShoolr;
        public float Pos_X;
        public float Pos_Y;
        public float Pos_Z;
        public float Rot_X;
        public float Rot_Y;
        public float Rot_Z;
    }
    public class RspTransform
    {
        public int playerID;
        public double time;
        public bool isShoolr;
        public float Pos_X;
        public float Pos_Y;
        public float Pos_Z;
        public float Rot_X;
        public float Rot_Y;
        public float Rot_Z;
    }

    public class ReqPlayerState
    {
        public int PlayerID;
        public int AniState;
        public int SkillID;
    }
    public class RspPlayerState
    {
        public int PlayerID;
        public int AniState;
        public int SkillID;
    }
    public class RspCreatePlayer
    {
        public PlayerData playerData;
    }
    public class RspDeletePlayer
    {
        public int PlayerID;

    }
    public class ReqDamage
    {
        public int id;
        public int damageState;
        public int Damage;
    }
    public class RspDamage
    {
        public int id;
        public int hp;
        public int damageState;
        public int Damage;
    }
    /// <summary>
    /// 玩家复活请求并返回主页
    /// </summary>
    public class ReqRecover
    {
        public int id;
        public bool isRevive;//True复活，False死亡返回主页
    }
    /// <summary>
    /// 玩家复活响应并返回主页
    /// </summary>
    public class RspRecover
    {
        public int id;
        public bool isRevive;
        public int Hp;//生命
        public int Hpmax;//生命上限
    }
    public class ReqRevive
    {
        public int id;
    }

    public class RspRevive
    {
        public int id;
        public int hp;
    }

    #endregion

    #region 自定义数据/枚举
    public class ReqBag//使用资源
    {
        public int id;
        public int count;
    }
    public class PlayerData
    {
        public int id;
        public string? name;//角色名称
        public int type;//角色类型
        public int exp;//角色经验
        public int level;//角色等级
        public int power;//角色体力
        public int powerMax;//体力上限
        public float aura;//角色星晶
        public float ruvia;//角色云晶
        public float crystal; //角色彩晶
        public int Hp;//生命
        public int Hpmax;//生命上限
        public int Mana;//法力
        public int ManaMax;//法力上限
        public int ad;//物攻
        public int ap;//魔攻
        public int addef;//物抗
        public int apdef;//魔抗
        public int dodge;//闪避概率
        public int critical;//暴击概率
        public float practice;//修炼速度
        public RewardTask? rewardTask;//每日任务进度
        public List<DailyTask>? dailyTasks;//每日任务数据
        public List<int>? TalentID;//当前选中的天赋ID
        public List<Talent>? TalentsData; //所有天赋数据
        public List<BagList>? Bag;//背包数据
        public List<FriendItem>? FriendList;//好友列表
        public List<FriendItem>? AddFriendList;//好友申请列表

        public int Taskid;
        public int SkillID;
        public int AniState;
        public float Pos_X;
        public float Pos_Y;
        public float Pos_Z;
        public float Rot_X;
        public float Rot_Y;
        public float Rot_Z;
    }
    public class BagList
    {
        public int GoodsID;
        public int count;
    }

    /// <summary>
    /// 战斗相关数据
    /// </summary>
    public class BattleData
    {
        public int Hp;//生命
        public int Hpmax;//生命上限
        public int Mana;//法力
        public int ManaMax;//法力上限
        public int ad;//物攻
        public int ap;//魔攻
        public int addef;//物抗
        public int apdef;//魔抗
        public int dodge;//闪避概率
        public int critical;//暴击概率
    }

    public class Talent
    {
        public int TalentID;
        public int Level;
    }
    public class FriendItem
    {
        public int id;
        public string? type;
        public string? name;
        public int level;
        public float aura;//角色星晶
        public float ruvia;//角色云晶
        public float crystal; //角色彩晶
        public List<int>? FriendList;//好友id列表
        public List<int>? AddFriendList;//好友id申请列表
    }

    public class DailyTask
    {
        public int TaskID;//每日任务id
        public int TaskReward;//任务进度
        public bool TaskFinish;//任务完成状态
    }

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
    public enum DamageState
    {
        None = 0,
        Critical = 1,//暴击
        Dodge = 2//闪避
    }
    public enum BagType
    {
        consume,//消耗品
        equip,//装备
        material,//材料
    }
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
    public enum CMD
    {
        None = 0,
        heartbeatMessage = 1,
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
    public class IPCfg
    {

        public const string srvIP = "10.201.26.183";

        public const int srvPort = 17666;
    }
    #endregion
}
