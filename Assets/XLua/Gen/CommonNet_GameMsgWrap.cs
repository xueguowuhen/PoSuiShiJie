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
    public class CommonNetGameMsgWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(CommonNet.GameMsg);
			Utils.BeginObjectRegister(type, L, translator, 0, 0, 54, 54);
			
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqLogin", _g_get_reqLogin);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspLogin", _g_get_rspLogin);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqRegister", _g_get_reqRegister);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspRegister", _g_get_rspRegister);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqCreateGame", _g_get_reqCreateGame);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspCreateGame", _g_get_rspCreateGame);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqShop", _g_get_reqShop);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspShop", _g_get_rspShop);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqRaffleSingle", _g_get_reqRaffleSingle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspRaffleSingle", _g_get_rspRaffleSingle);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqRaffleTan", _g_get_reqRaffleTan);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspRaffleTan", _g_get_rspRaffleTan);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqBag", _g_get_reqBag);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspBag", _g_get_rspBag);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqTask", _g_get_reqTask);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspTask", _g_get_rspTask);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspCreatePlayer", _g_get_rspCreatePlayer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspDeletePlayer", _g_get_rspDeletePlayer);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqEnterPVP", _g_get_reqEnterPVP);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspEnterPVP", _g_get_rspEnterPVP);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqExitPVP", _g_get_reqExitPVP);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspExitPVP", _g_get_rspExitPVP);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqTransform", _g_get_reqTransform);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspTransform", _g_get_rspTransform);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqPlayerState", _g_get_reqPlayerState);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspPlayerState", _g_get_rspPlayerState);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqDamage", _g_get_reqDamage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspDamage", _g_get_rspDamage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqRevive", _g_get_reqRevive);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspRevive", _g_get_rspRevive);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqRecover", _g_get_reqRecover);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspRecover", _g_get_rspRecover);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqSearchFriend", _g_get_reqSearchFriend);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspSearchFriend", _g_get_rspSearchFriend);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqAddFriend", _g_get_reqAddFriend);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspAddFriend", _g_get_rspAddFriend);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqDelFriend", _g_get_reqDelFriend);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspDelFriend", _g_get_rspDelFriend);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqFriendGift", _g_get_reqFriendGift);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspFriendGift", _g_get_rspFriendGift);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqFriendAddConfirm", _g_get_reqFriendAddConfirm);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspFriendAddConfirm", _g_get_rspFriendAddConfirm);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqDailyTask", _g_get_reqDailyTask);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspDailyTask", _g_get_rspDailyTask);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqRewardTask", _g_get_reqRewardTask);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspRewardTask", _g_get_rspRewardTask);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "heartbeatMessage", _g_get_heartbeatMessage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqSystemTimeMessage", _g_get_reqSystemTimeMessage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspSystemTimeMessage", _g_get_rspSystemTimeMessage);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "systemSessionID", _g_get_systemSessionID);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqTalentUp", _g_get_reqTalentUp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspTalentUp", _g_get_rspTalentUp);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "reqChangeTalent", _g_get_reqChangeTalent);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "rspChangeTalent", _g_get_rspChangeTalent);
            
			Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqLogin", _s_set_reqLogin);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspLogin", _s_set_rspLogin);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqRegister", _s_set_reqRegister);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspRegister", _s_set_rspRegister);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqCreateGame", _s_set_reqCreateGame);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspCreateGame", _s_set_rspCreateGame);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqShop", _s_set_reqShop);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspShop", _s_set_rspShop);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqRaffleSingle", _s_set_reqRaffleSingle);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspRaffleSingle", _s_set_rspRaffleSingle);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqRaffleTan", _s_set_reqRaffleTan);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspRaffleTan", _s_set_rspRaffleTan);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqBag", _s_set_reqBag);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspBag", _s_set_rspBag);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqTask", _s_set_reqTask);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspTask", _s_set_rspTask);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspCreatePlayer", _s_set_rspCreatePlayer);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspDeletePlayer", _s_set_rspDeletePlayer);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqEnterPVP", _s_set_reqEnterPVP);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspEnterPVP", _s_set_rspEnterPVP);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqExitPVP", _s_set_reqExitPVP);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspExitPVP", _s_set_rspExitPVP);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqTransform", _s_set_reqTransform);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspTransform", _s_set_rspTransform);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqPlayerState", _s_set_reqPlayerState);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspPlayerState", _s_set_rspPlayerState);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqDamage", _s_set_reqDamage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspDamage", _s_set_rspDamage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqRevive", _s_set_reqRevive);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspRevive", _s_set_rspRevive);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqRecover", _s_set_reqRecover);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspRecover", _s_set_rspRecover);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqSearchFriend", _s_set_reqSearchFriend);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspSearchFriend", _s_set_rspSearchFriend);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqAddFriend", _s_set_reqAddFriend);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspAddFriend", _s_set_rspAddFriend);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqDelFriend", _s_set_reqDelFriend);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspDelFriend", _s_set_rspDelFriend);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqFriendGift", _s_set_reqFriendGift);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspFriendGift", _s_set_rspFriendGift);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqFriendAddConfirm", _s_set_reqFriendAddConfirm);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspFriendAddConfirm", _s_set_rspFriendAddConfirm);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqDailyTask", _s_set_reqDailyTask);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspDailyTask", _s_set_rspDailyTask);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqRewardTask", _s_set_reqRewardTask);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspRewardTask", _s_set_rspRewardTask);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "heartbeatMessage", _s_set_heartbeatMessage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqSystemTimeMessage", _s_set_reqSystemTimeMessage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspSystemTimeMessage", _s_set_rspSystemTimeMessage);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "systemSessionID", _s_set_systemSessionID);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqTalentUp", _s_set_reqTalentUp);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspTalentUp", _s_set_rspTalentUp);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "reqChangeTalent", _s_set_reqChangeTalent);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "rspChangeTalent", _s_set_rspChangeTalent);
            
			
			Utils.EndObjectRegister(type, L, translator, null, null,
			    null, null, null);

		    Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);
			
			
            
			
			
			
			Utils.EndClassRegister(type, L, translator);
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            
			try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
				if(LuaAPI.lua_gettop(L) == 1)
				{
					
					CommonNet.GameMsg gen_ret = new CommonNet.GameMsg();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to CommonNet.GameMsg constructor!");
            
        }
        
		
        
		
        
        
        
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqLogin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqLogin);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspLogin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspLogin);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqRegister(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqRegister);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspRegister(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspRegister);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqCreateGame(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqCreateGame);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspCreateGame(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspCreateGame);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqShop(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqShop);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspShop(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspShop);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqRaffleSingle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqRaffleSingle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspRaffleSingle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspRaffleSingle);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqRaffleTan(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqRaffleTan);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspRaffleTan(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspRaffleTan);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqBag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqBag);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspBag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspBag);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqTask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqTask);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspTask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspTask);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspCreatePlayer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspCreatePlayer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspDeletePlayer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspDeletePlayer);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqEnterPVP(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqEnterPVP);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspEnterPVP(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspEnterPVP);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqExitPVP(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqExitPVP);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspExitPVP(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspExitPVP);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqTransform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqTransform);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspTransform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspTransform);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqPlayerState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqPlayerState);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspPlayerState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspPlayerState);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqDamage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqDamage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspDamage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspDamage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqRevive(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqRevive);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspRevive(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspRevive);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqRecover(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqRecover);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspRecover(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspRecover);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqSearchFriend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqSearchFriend);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspSearchFriend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspSearchFriend);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqAddFriend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqAddFriend);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspAddFriend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspAddFriend);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqDelFriend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqDelFriend);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspDelFriend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspDelFriend);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqFriendGift(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqFriendGift);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspFriendGift(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspFriendGift);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqFriendAddConfirm(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqFriendAddConfirm);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspFriendAddConfirm(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspFriendAddConfirm);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqDailyTask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqDailyTask);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspDailyTask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspDailyTask);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqRewardTask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqRewardTask);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspRewardTask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspRewardTask);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_heartbeatMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.heartbeatMessage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqSystemTimeMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqSystemTimeMessage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspSystemTimeMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspSystemTimeMessage);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_systemSessionID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.systemSessionID);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqTalentUp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqTalentUp);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspTalentUp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspTalentUp);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_reqChangeTalent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.reqChangeTalent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_rspChangeTalent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.rspChangeTalent);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqLogin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqLogin = (CommonNet.ReqLogin)translator.GetObject(L, 2, typeof(CommonNet.ReqLogin));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspLogin(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspLogin = (CommonNet.RspLogin)translator.GetObject(L, 2, typeof(CommonNet.RspLogin));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqRegister(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqRegister = (CommonNet.ReqRegister)translator.GetObject(L, 2, typeof(CommonNet.ReqRegister));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspRegister(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspRegister = (CommonNet.RspRegister)translator.GetObject(L, 2, typeof(CommonNet.RspRegister));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqCreateGame(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqCreateGame = (CommonNet.ReqCreateGame)translator.GetObject(L, 2, typeof(CommonNet.ReqCreateGame));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspCreateGame(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspCreateGame = (CommonNet.RspCreateGame)translator.GetObject(L, 2, typeof(CommonNet.RspCreateGame));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqShop(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqShop = (CommonNet.ReqShop)translator.GetObject(L, 2, typeof(CommonNet.ReqShop));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspShop(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspShop = (CommonNet.RspShop)translator.GetObject(L, 2, typeof(CommonNet.RspShop));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqRaffleSingle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqRaffleSingle = (CommonNet.ReqRaffleSingle)translator.GetObject(L, 2, typeof(CommonNet.ReqRaffleSingle));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspRaffleSingle(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspRaffleSingle = (CommonNet.RspRaffleSingle)translator.GetObject(L, 2, typeof(CommonNet.RspRaffleSingle));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqRaffleTan(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqRaffleTan = (CommonNet.ReqRaffleTan)translator.GetObject(L, 2, typeof(CommonNet.ReqRaffleTan));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspRaffleTan(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspRaffleTan = (CommonNet.RspRaffleTan)translator.GetObject(L, 2, typeof(CommonNet.RspRaffleTan));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqBag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqBag = (CommonNet.ReqBag)translator.GetObject(L, 2, typeof(CommonNet.ReqBag));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspBag(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspBag = (CommonNet.RspBag)translator.GetObject(L, 2, typeof(CommonNet.RspBag));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqTask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqTask = (CommonNet.ReqTask)translator.GetObject(L, 2, typeof(CommonNet.ReqTask));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspTask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspTask = (CommonNet.RspTask)translator.GetObject(L, 2, typeof(CommonNet.RspTask));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspCreatePlayer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspCreatePlayer = (CommonNet.RspCreatePlayer)translator.GetObject(L, 2, typeof(CommonNet.RspCreatePlayer));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspDeletePlayer(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspDeletePlayer = (CommonNet.RspDeletePlayer)translator.GetObject(L, 2, typeof(CommonNet.RspDeletePlayer));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqEnterPVP(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqEnterPVP = (CommonNet.ReqEnterPVP)translator.GetObject(L, 2, typeof(CommonNet.ReqEnterPVP));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspEnterPVP(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspEnterPVP = (CommonNet.RspEnterPVP)translator.GetObject(L, 2, typeof(CommonNet.RspEnterPVP));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqExitPVP(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqExitPVP = (CommonNet.ReqExitPVP)translator.GetObject(L, 2, typeof(CommonNet.ReqExitPVP));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspExitPVP(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspExitPVP = (CommonNet.RspExitPVP)translator.GetObject(L, 2, typeof(CommonNet.RspExitPVP));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqTransform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqTransform = (CommonNet.ReqTransform)translator.GetObject(L, 2, typeof(CommonNet.ReqTransform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspTransform(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspTransform = (CommonNet.RspTransform)translator.GetObject(L, 2, typeof(CommonNet.RspTransform));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqPlayerState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqPlayerState = (CommonNet.ReqPlayerState)translator.GetObject(L, 2, typeof(CommonNet.ReqPlayerState));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspPlayerState(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspPlayerState = (CommonNet.RspPlayerState)translator.GetObject(L, 2, typeof(CommonNet.RspPlayerState));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqDamage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqDamage = (CommonNet.ReqDamage)translator.GetObject(L, 2, typeof(CommonNet.ReqDamage));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspDamage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspDamage = (CommonNet.RspDamage)translator.GetObject(L, 2, typeof(CommonNet.RspDamage));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqRevive(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqRevive = (CommonNet.ReqRevive)translator.GetObject(L, 2, typeof(CommonNet.ReqRevive));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspRevive(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspRevive = (CommonNet.RspRevive)translator.GetObject(L, 2, typeof(CommonNet.RspRevive));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqRecover(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqRecover = (CommonNet.ReqRecover)translator.GetObject(L, 2, typeof(CommonNet.ReqRecover));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspRecover(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspRecover = (CommonNet.RspRecover)translator.GetObject(L, 2, typeof(CommonNet.RspRecover));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqSearchFriend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqSearchFriend = (CommonNet.ReqSearchFriend)translator.GetObject(L, 2, typeof(CommonNet.ReqSearchFriend));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspSearchFriend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspSearchFriend = (CommonNet.RspSearchFriend)translator.GetObject(L, 2, typeof(CommonNet.RspSearchFriend));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqAddFriend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqAddFriend = (CommonNet.ReqAddFriend)translator.GetObject(L, 2, typeof(CommonNet.ReqAddFriend));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspAddFriend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspAddFriend = (CommonNet.RspAddFriend)translator.GetObject(L, 2, typeof(CommonNet.RspAddFriend));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqDelFriend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqDelFriend = (CommonNet.ReqDelFriend)translator.GetObject(L, 2, typeof(CommonNet.ReqDelFriend));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspDelFriend(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspDelFriend = (CommonNet.RspDelFriend)translator.GetObject(L, 2, typeof(CommonNet.RspDelFriend));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqFriendGift(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqFriendGift = (CommonNet.ReqFriendGift)translator.GetObject(L, 2, typeof(CommonNet.ReqFriendGift));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspFriendGift(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspFriendGift = (CommonNet.RspFriendGift)translator.GetObject(L, 2, typeof(CommonNet.RspFriendGift));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqFriendAddConfirm(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqFriendAddConfirm = (CommonNet.ReqFriendAddConfirm)translator.GetObject(L, 2, typeof(CommonNet.ReqFriendAddConfirm));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspFriendAddConfirm(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspFriendAddConfirm = (CommonNet.RspFriendAddConfirm)translator.GetObject(L, 2, typeof(CommonNet.RspFriendAddConfirm));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqDailyTask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqDailyTask = (CommonNet.ReqDailyTask)translator.GetObject(L, 2, typeof(CommonNet.ReqDailyTask));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspDailyTask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspDailyTask = (CommonNet.RspDailyTask)translator.GetObject(L, 2, typeof(CommonNet.RspDailyTask));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqRewardTask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqRewardTask = (CommonNet.ReqRewardTask)translator.GetObject(L, 2, typeof(CommonNet.ReqRewardTask));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspRewardTask(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspRewardTask = (CommonNet.RspRewardTask)translator.GetObject(L, 2, typeof(CommonNet.RspRewardTask));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_heartbeatMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.heartbeatMessage = (CommonNet.HeartbeatMessage)translator.GetObject(L, 2, typeof(CommonNet.HeartbeatMessage));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqSystemTimeMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqSystemTimeMessage = (CommonNet.ReqSystemTimeMessage)translator.GetObject(L, 2, typeof(CommonNet.ReqSystemTimeMessage));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspSystemTimeMessage(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspSystemTimeMessage = (CommonNet.RspSystemTimeMessage)translator.GetObject(L, 2, typeof(CommonNet.RspSystemTimeMessage));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_systemSessionID(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.systemSessionID = (CommonNet.SystemSessionID)translator.GetObject(L, 2, typeof(CommonNet.SystemSessionID));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqTalentUp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqTalentUp = (CommonNet.ReqTalentUp)translator.GetObject(L, 2, typeof(CommonNet.ReqTalentUp));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspTalentUp(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspTalentUp = (CommonNet.RspTalentUp)translator.GetObject(L, 2, typeof(CommonNet.RspTalentUp));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_reqChangeTalent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.reqChangeTalent = (CommonNet.ReqChangeTalent)translator.GetObject(L, 2, typeof(CommonNet.ReqChangeTalent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_rspChangeTalent(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                CommonNet.GameMsg gen_to_be_invoked = (CommonNet.GameMsg)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.rspChangeTalent = (CommonNet.RspChangeTalent)translator.GetObject(L, 2, typeof(CommonNet.RspChangeTalent));
            
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }
        
		
		
		
		
    }
}
