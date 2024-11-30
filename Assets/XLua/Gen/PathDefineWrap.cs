#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
#endif

using XLua;
using System.Collections.Generic;


namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class PathDefineWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(PathDefine);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);
			
			
			
			
			
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 130, 0, 0);
			
			
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ABDownload", PathDefine.ABDownload);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Download", PathDefine.Download);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "personCfg", PathDefine.personCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TalentCfg", PathDefine.TalentCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RdnameCfg", PathDefine.RdnameCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ShopItemCfg", PathDefine.ShopItemCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "mapCfg", PathDefine.mapCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TaskCfg", PathDefine.TaskCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TaskDailyCfg", PathDefine.TaskDailyCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TaskRewardCfg", PathDefine.TaskRewardCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RaffleItemCfg", PathDefine.RaffleItemCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SkillCfg", PathDefine.SkillCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SkillMoveCfg", PathDefine.SkillMoveCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SkillActionCfg", PathDefine.SkillActionCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SkillFxCfg", PathDefine.SkillFxCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "EnemyCfg", PathDefine.EnemyCfg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Xml", PathDefine.Xml);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AssetBundle", PathDefine.AssetBundle);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Windows", PathDefine.Windows);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Android", PathDefine.Android);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AssetBundles", PathDefine.AssetBundles);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResProns", PathDefine.ResProns);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AssetBundleManifest", PathDefine.AssetBundleManifest);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Main", PathDefine.Main);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResPerson", PathDefine.ResPerson);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResItem", PathDefine.ResItem);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResShopItem", PathDefine.ResShopItem);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RaffleItem", PathDefine.RaffleItem);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResBagItem", PathDefine.ResBagItem);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResFriendsItem", PathDefine.ResFriendsItem);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResAttributeText", PathDefine.ResAttributeText);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResRewardTaskItem", PathDefine.ResRewardTaskItem);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResDailyTaskItem", PathDefine.ResDailyTaskItem);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Prefab", PathDefine.Prefab);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "EntityHp", PathDefine.EntityHp);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResHard", PathDefine.ResHard);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ResUI", PathDefine.ResUI);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Equip", PathDefine.Equip);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "props", PathDefine.props);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "icon", PathDefine.icon);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "HpRoot", PathDefine.HpRoot);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "btnEvade", PathDefine.btnEvade);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Png", PathDefine.Png);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "RaffleCtrl", PathDefine.RaffleCtrl);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TextTips", PathDefine.TextTips);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "hpItemRoot", PathDefine.hpItemRoot);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "txtCritical", PathDefine.txtCritical);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "txtDodge", PathDefine.txtDodge);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "txtHp", PathDefine.txtHp);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "quantity", PathDefine.quantity);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ItemImage", PathDefine.ItemImage);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Decrease", PathDefine.Decrease);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Increase", PathDefine.Increase);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Buy", PathDefine.Buy);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Close", PathDefine.Close);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Bg", PathDefine.Bg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CreateWndbtn_close", PathDefine.CreateWndbtn_close);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "OneWnd", PathDefine.OneWnd);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ManageBtn", PathDefine.ManageBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "VirtualBtn", PathDefine.VirtualBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GodBtn", PathDefine.GodBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BaseBtn", PathDefine.BaseBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AttributeBtn", PathDefine.AttributeBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NextBtn", PathDefine.NextBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Presons", PathDefine.Presons);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BaseMain", PathDefine.BaseMain);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NameText", PathDefine.NameText);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "IntroText", PathDefine.IntroText);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "fightText", PathDefine.fightText);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TwoMain", PathDefine.TwoMain);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "IntroRank", PathDefine.IntroRank);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "IntroItem", PathDefine.IntroItem);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TwoWnd", PathDefine.TwoWnd);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CreateBtn", PathDefine.CreateBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "LastBtn", PathDefine.LastBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "GameName", PathDefine.GameName);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "NameRandBtn", PathDefine.NameRandBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TalentList", PathDefine.TalentList);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TalentBtn", PathDefine.TalentBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TalentItemimg", PathDefine.TalentItemimg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Talentxt", PathDefine.Talentxt);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TalentItemText", PathDefine.TalentItemText);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UseButton", PathDefine.UseButton);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "UseText", PathDefine.UseText);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ItemText", PathDefine.ItemText);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "CloseButton", PathDefine.CloseButton);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MapRoot", PathDefine.MapRoot);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "HpProImg", PathDefine.HpProImg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "HpText", PathDefine.HpText);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ManaProImg", PathDefine.ManaProImg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ManaText", PathDefine.ManaText);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Head1", PathDefine.Head1);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Headimg", PathDefine.Headimg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Head2", PathDefine.Head2);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Head2img", PathDefine.Head2img);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Head3", PathDefine.Head3);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Head3img", PathDefine.Head3img);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TaskPro", PathDefine.TaskPro);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "Task", PathDefine.Task);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SignBtn", PathDefine.SignBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "AchivementBtn", PathDefine.AchivementBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ShopBtn", PathDefine.ShopBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "menuAni", PathDefine.menuAni);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ArenaBtn", PathDefine.ArenaBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "MenuBtn", PathDefine.MenuBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BagBtn", PathDefine.BagBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "ChatBtn", PathDefine.ChatBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TaskBtn", PathDefine.TaskBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SkillBtn", PathDefine.SkillBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SettingsBtn", PathDefine.SettingsBtn);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "imgTouch", PathDefine.imgTouch);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "imgDirBg", PathDefine.imgDirBg);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "imgDirPoint", PathDefine.imgDirPoint);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "btnNormal", PathDefine.btnNormal);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "btnSkill1", PathDefine.btnSkill1);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SkillCD1", PathDefine.SkillCD1);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SkillTxt1", PathDefine.SkillTxt1);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "btnSkill2", PathDefine.btnSkill2);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SkillCD2", PathDefine.SkillCD2);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SkillTxt2", PathDefine.SkillTxt2);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "btnSkill3", PathDefine.btnSkill3);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SkillCD3", PathDefine.SkillCD3);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "SkillTxt3", PathDefine.SkillTxt3);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BtnQuitBattle", PathDefine.BtnQuitBattle);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TextTalk", PathDefine.TextTalk);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "TextName", PathDefine.TextName);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "BtnNext", PathDefine.BtnNext);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "imgIcon", PathDefine.imgIcon);
            Utils.RegisterObject(L, translator, Utils.CLS_IDX, "PlayerName", PathDefine.PlayerName);
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "PathDefine does not have a constructor!");
        }
        
		
        
		
        
        
        
        
        
        
        
        
        
		
		
		
		
    }
}
