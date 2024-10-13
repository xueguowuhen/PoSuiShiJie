/****************************************************
    文件：PathDefine
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-14 10:03:35
	功能：路径配置文件
*****************************************************/
using System;
using System.Collections.Generic;
using UnityEngine;
public class PathDefine
{
    #region Configs
    public const string personCfg = "ResCfg/person";
    public const string TalentCfg = "ResCfg/Talent";
    public const string RdnameCfg = "ResCfg/rdname";
    public const string ShopItemCfg = "ResCfg/ShopItem";
    public const string mapCfg = "ResCfg/map";
    public const string TaskCfg = "ResCfg/guide";
    public const string SkillCfg = "ResCfg/skill";
    public const string SkillMoveCfg = "ResCfg/skillmove";
    public const string SkillActionCfg = "ResCfg/skillaction";
    public const string SkillFxCfg = "ResCfg/skillfx";
    public const string EnemyCfg = "ResCfg/MonsterData";
    //public const string 
    #endregion
    #region ResPrefab
    public const string PC = "PC";
    public const string Android = "Android";
    public const string AssetBundles = "AssetBundles";
    public const string ResProns = "ResProns/";
    public const string AssetBundleManifest = "AssetBundleManifest";
    public const string Main = "Main";
    //public const string kaixiya

    public const string ResMainProns = "ResMainProns/";
    public const string ResIntroItem = "ResPerfer/IntroItem";
    public const string ResTalentItem = "ResPerfer/TalentItem";
    public const string ResTalentimg = "ResPerfer/TalenImg";
    public const string ResShopItem = "ResPerfer/Item/ShopItem";
    public const string ResTipInfoWnd = "ResPerfer/TipInfoWnd";
    public const string ResBuyTipWnd = "ResPerfer/BuyTipWnd";
    public const string ResBagItem = "ResPerfer/Item/BagItem";
    public const string ResUseTipWnd = "ResPerfer/UseTipWnd";
    public const string ResFriendsItem = "ResPerfer/Item/FriendsItem";
    public const string HPItemPrefab = "ResPerfer/ItemEntityHp";
    public const string ResHard = "ResUI/Hard/";
    public const string ResUI = "ResUI/";
    public const string Equip = "Equip/";
    public const string props = "props/";
    public const string icon = "icon/";
    public const string HpRoot = "HpRoot";
    #endregion
    #region GameObjectPath
    public const string LoginWndPath = "Canvas/LoginWnd";
    public const string RegisterWndPath = "Canvas/RegisterWnd";
    public const string loadingWndPath = "Canvas/LoadingWnd";
    public const string CharaterCamera = "CharaterCamera";
    #region LoginWnd
    public const string LoginBtn_close = "LoginCenten/btn_close1";
    public const string LoginBtn_Register = "LoginCenten/Register";
    public const string LoginBtn_Toggle = "LoginCenten/remenber/Toggle";
    public const string LoginBtn_Login = "LoginCenten/LoginBtn";
    public const string LoginUsername = "LoginCenten/Username/InputField";
    public const string LoginPassword = "LoginCenten/Password/InputField";
    public const string imgbg = "bg_big";
    #endregion
    #region LoadingWnd
    public const string Loading_txtTips = "BottomWnd/txtTips";
    public const string Loading_imageFG = "BottomWnd/loadingfg";
    public const string Loading_txtPrg = "BottomWnd/loadingfg/txtPrg";
    #endregion
    #region DownloadWnd
    public const string Downing_txtSpeed = "BottomWnd/txtSpeed";
    public const string Downing_txtTips = "BottomWnd/txtTips";
    public const string Downing_txtProgress = "BottomWnd/txtProgress";
    public const string Downing_txtPrg = "BottomWnd/loadingfg/txtPrg";
    public const string Downing_imageFG = "BottomWnd/loadingfg";
    public const string abCompareInfo = "abCompareInfo.txt";
    public const string abCompareInfo_TMP = "abCompareInfo_TMP.txt";
    public const string abCompareInfo_Cache = "abCompareInfo_Cache.txt";
    #endregion
    #region RegisterWnd
    public const string Register_close = "RegisterCenten/btn_close1";
    public const string Register_Register = "RegisterCenten/Register";
    public const string Register_Username = "RegisterCenten/Username/InputField";
    public const string Register_Password = "RegisterCenten/Password/InputField";
    public const string Register_RePassword = "RegisterCenten/RePassword/InputField";
    #endregion
    #region DynamicWnd
    public const string TextTips = "CenterPin/TxtTips";
    public const string hpItemRoot = "LeftButtomPin/hpItemRoot";
    #endregion
    #region ItemEntityHP
    public const string txtCritical = "txtCritical";
    public const string txtDodge = "txtDodge";
    public const string txtHp = "txtHp";
    #endregion
    #region ShopWnd
    public const string Buttons = "Buttons";
    public const string Slots = "Slots";
    #endregion
    #region BuyTipWnd
    public const string quantity = "quantity";
    public const string ItemImage = "ItemImage";
    public const string Decrease = "Decrease";
    public const string Increase = "Increase";
    public const string Buy = "Buy";
    public const string Close = "Close";
    #endregion
    #region CreateWnd
    public const string Bg = "Bg";
    public const string CreateWndbtn_close = "Bg/btn_close";
    public const string OneWnd = "OneWnd";
    public const string ManageBtn = "CreateLeft/ManageBtn";
    public const string VirtualBtn = "CreateLeft/VirtualBtn";
    public const string GodBtn = "CreateLeft/GodBtn";

    public const string BaseBtn = "CreateRight/BaseBtn";
    public const string AttributeBtn = "CreateRight/AttributeBtn";
    public const string NextBtn = "CreateRight/NextBtn";
    public const string Presons = "Presons";
    public const string BaseMain = "CreateRight/BaseMain";
    public const string NameText = "Name/NameText";
    public const string IntroText = "Intro/IntroText";
    public const string fightText = "fight/fightText";
    public const string TwoMain = "CreateRight/TwoMain";
    public const string IntroRank = "Intro/IntroRank";
    public const string IntroItem = "ResPerfer/IntroItem";

    public const string TwoWnd = "TwoWnd";
    public const string CreateBtn = "CreateBtn";
    public const string LastBtn = "LastBtn";
    public const string GameName = "CreateRight/GameName/InputField";
    public const string NameRandBtn = "CreateRight/NameRandBtn";
    public const string TalentList = "CreateLeft/TalentList";
    public const string TalentBtn = "CreateLeft/TalentBtn";
    public const string TalentItemimg = "TalenImg";
    public const string Talentxt = "Talentxt";
    public const string TalentItemText = "Text";
    #endregion
    #region UseTipWnd
    public const string UseButton = "All/UseButton";
    public const string UseText = "All/UseButton/UseText";
    public const string ItemText = "All";
    public const string CloseButton = "All/CloseButton";
    #endregion
    #region BagWnd
    public const string AllBtn = "Bag/background/BagTatleBtn/AllBtn";
    public const string EuqipBtn = "Bag/background/BagTatleBtn/EuqipBtn";
    public const string materialBtn = "Bag/background/BagTatleBtn/materialBtn";
    public const string consumeBtn = "Bag/background/BagTatleBtn/consumeBtn";
    public const string SearchBtn = "Bag/background/BagTatleBtn/SearchBtn";
    public const string CloseBtn = "Bag/background/BagTatleBtn/CloseBtn";
    public const string BagContent = "Bag/background/Slots/ScrollRect/ViewPort/BagContent";
    #endregion
    #region MainCityWnd
    public const string MapRoot = "MapRoot";
    public const string HpProImg = "LeftCenten/HpPro/HpProImg";
    public const string HpText = "LeftCenten/HpPro/HpText";
    public const string ManaProImg = "LeftCenten/ManaPro/ManaProImg";
    public const string ManaText = "LeftCenten/ManaPro/ManaText";
    public const string Head1 = "LeftCenten/Head1";
    public const string Headimg = "LeftCenten/Head1/Headimg";
    public const string Head2 = "LeftCenten/Head2";
    public const string Head2img = "LeftCenten/Head2/Head2img";
    public const string Head3 = "LeftCenten/Head3";
    public const string Head3img = "LeftCenten/Head3/Head3img";

    public const string TaskPro = "RightCenten/TaskPro";
    public const string Task = "RightCenten/TaskPro/TaskText";
    public const string SignBtn = "RightCenten/SignBtn";
    public const string AchivementBtn = "RightCenten/AchivementBtn";
    public const string ShopBtn = "RightCenten/ShopBtn";
    public const string menuAni = "RigfhtButtom/MenuRoot";
    public const string ArenaBtn = "RigfhtButtom/MenuRoot/ArenaBtn";
    public const string MenuBtn = "RigfhtButtom/MenuRoot/MenuBtn";
    public const string BagBtn = "RigfhtButtom/MenuRoot/BagBtn";
    public const string ChatBtn = "RigfhtButtom/MenuRoot/ChatBtn";
    public const string TaskBtn = "RigfhtButtom/MenuRoot/TaskBtn";
    public const string SkillBtn = "RigfhtButtom/MenuRoot/SkillBtn";
    public const string SettingsBtn = "RigfhtButtom/MenuRoot/SettingsBtn";
    public const string imgTouch = "LeftBottom/imgTouch";
    public const string imgDirBg = "LeftBottom/imgTouch/imgDirBg";
    public const string imgDirPoint = "LeftBottom/imgTouch/imgDirBg/imgDirPoint";
    #endregion
    #region BattleWnd
    public const string btnNormal = "RigfhtButtom/btnNormal";
    public const string btnSkill1 = "RigfhtButtom/btnSkill1";
    public const string SkillCD1 = "RigfhtButtom/btnSkill1/imgCD";
    public const string SkillTxt1 = "RigfhtButtom/btnSkill1/imgCD/txtCD";
    public const string btnSkill2 = "RigfhtButtom/btnSkill2";
    public const string SkillCD2 = "RigfhtButtom/btnSkill2/imgCD";
    public const string SkillTxt2 = "RigfhtButtom/btnSkill2/imgCD/txtCD";
    public const string btnSkill3 = "RigfhtButtom/btnSkill3";
    public const string SkillCD3 = "RigfhtButtom/btnSkill3/imgCD";
    public const string SkillTxt3 = "RigfhtButtom/btnSkill3/imgCD/txtCD";
    public const string BtnQuitBattle = "RigfhtButtom/btnQuitBattle";

    #endregion
    #region GuideWnd
    public const string TextTalk = "BottomPin/bgTalk/TextTalk";
    public const string TextName = "BottomPin/bgTalk/TextName";
    public const string BtnNext = "BottomPin/BtnNext";
    public const string imgIcon = "BottomPin/imgIcon";
    public const string PlayerName = "我";
    //public const string PlayerNpc
    #endregion
    #region PersonWnd
    public const string PersonWnd = "ResPerfer/PersonWnd";
    public const string BtnPerson = "PersonItem/PersonOne";
    public const string BtnClose = "CloseBtn";
    public const string ItemOne = "Centen/PersonBg/ItemOne";
    public const string ItemTwo = "Centen/PersonBg/ItemTwo";
    public const string Img = "Image";
    public const string Person = "Person";
    public const string PersonItem = "PersonItem";
    public const string Talent = "Talent";
    public const string TalentOne = "talentOne";
    public const string attribute = "attribute";
    public const string Hp = "Hp";
    public const string Experience = "Experience";
    public const string Slider = "Slider";
    public const string Attr = "Attr";
    public const string HpSlider = "HpSlider";
    public const string MagicSlider = "MagicSlider";

    #endregion
    #endregion
}