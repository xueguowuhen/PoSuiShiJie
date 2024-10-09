/****************************************************
    文件：Constants
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-14 19:56:58
	功能：常量配置
*****************************************************/
public enum TxtColor
{
    Red,//红色
    Green,//绿色
    Blue,//蓝色
    Yellow,//黄色
    Grey,//灰色
    Purple,//紫色
    Pink,//粉色
    White,//白色
}
public enum WindowType
{
    None=0,     //不加载
    FromTop,    // 从上方加载
    FromBottom, // 从下方加载
    FromLeft,   // 从左侧加载
    FromRight,  // 从右侧加载
    FromCenter  // 从中间加载
}

public enum DamageType
{
    None,
    AD = 1,
    AP = 2,
}
/// <summary>
/// 实体状态枚举
/// </summary>
public enum EntityState
{
    None,
    Bati,
}
public enum EntityType
{
    /// <summary>
    /// 未设置
    /// </summary>
    None,
    /// <summary>
    /// 玩家
    /// </summary>
    Player,
    /// <summary>
    /// 怪物
    /// </summary>
    Monster,
}
public class Constants
{
    #region Color
    private const string ColorRed = "<color=#FF0000FF>";
    private const string ColorGreen = "<color=#00FF00FF>";
    private const string ColorBlue = "<color=#00B4FFFF>";
    private const string ColorYellow = "<color=#FFFF00FF>";
    private const string ColorGrey = "<color=#808080>";
    private const string ColorPurple = "<color=#800080>";
    private const string ColorPink = "<color=#Ffc0cb>";
    private const string ColorWhite = "<color=#FFFFFF>";
    private const string ColorEnd = "</color>";
    public static string Color(string str, TxtColor c)
    {
        string result = "";
        switch (c)
        {
            case TxtColor.Red:
                result = ColorRed + str + ColorEnd;
                break;
            case TxtColor.Green:
                result = ColorGreen + str + ColorEnd;
                break;
            case TxtColor.Blue:
                result = ColorBlue + str + ColorEnd;
                break;
            case TxtColor.Yellow:
                result = ColorYellow + str + ColorEnd;
                break;
            case TxtColor.Grey:
                result = ColorGrey + str + ColorEnd;
                break;
            case TxtColor.Purple:
                result = ColorPurple + str + ColorEnd;
                break;
            case TxtColor.Pink:
                result = ColorPink + str + ColorEnd;
                break;
            case TxtColor.White:
                result = ColorWhite + str + ColorEnd;
                break;
        }
        return result;
    }
    #endregion

    public const string SceneLogin = "GameLogin";

    public const float MainExitTime = 0.5f;//界面进入退出时间
    public const float MainEnterTime = 0.5f;

    public const int MaxSelect = 3;//最大天赋选择数
    public const int MaxTalen = 9;//最大天赋加载数
    public const float CreatefieldOfView = 1f;//主摄像头显示

    //标准宽高比
    public const int ScreenStandardWidth = 1920;
    public const int ScreenStandardHeight = 1080;

    //摇杆点标准距离
    public const int ScreenOPDis = 160;

    //混合参数
    public const int VelocityIdle = 0;
    public const int VelocityWalk =5 ;
    public const int VelocityRun=10;
    //角色移动速度
    public const int PlayerIdleSpeed = 0;
    public const int PlayerWalkSpeed = 4;
    public const int PlayerRunSpeed = 8;
    public const int MonsterMoveSpeed = 4;
    //运动平滑加速度
    public const int AccelerSpeed = 20;
    public const float AccelerHPSpeed = 0.3f;

    //对话摄像头偏移值

    public const int CameraMackNpc = 1 << 6;
    public const int CameraMackPlayer = 1 << 7;
    public const float GuideCamera_x = 0f;
    public const float GuideCamera_y = 3.1f;
    public const float GuideCamera_Z = 4.58f;
    public const int GuideCameraRos_x = 0;
    public const int GuideCameraRos_y = 180;
    public const int GuideCameraRos_z = 0;
    //层级配置
    public const int PlayerLayer = 7;
    public const int NPCLayer = 6;
    //战斗默认参数
    public const int ActionDefault = -1;
    public const int VelocityDefault = 0;
    public const int ActionHit = 101;
    public const int ActionDie = 100;
    //普通连招有效间隔
    public const int ComboSpace = 500;
    //死亡后间隔多久时间复活
    public const int Revive = 5;
    //残血保护设置
    public const int HpProtect = 50;
    //补偿最小距离
    public const float distance = 0.3f;
    #region charater
    public const int ManageID = 20001;
    public const int VirtualID = 20002;
    public const int GodID = 20003;

    #endregion
    #region MainCity
    public const int MainCityMapID =10000;//主城地图ID
    public const float CamerafieldOfView = 65f;//主摄像头显示
    #endregion
}
