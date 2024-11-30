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
    public class LuaHelperWrap 
    {
        public static void __Register(RealStatePtr L)
        {
			ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			System.Type type = typeof(LuaHelper);
			Utils.BeginObjectRegister(type, L, translator, 0, 2, 6, 0);
			
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "AddEventListener", _m_AddEventListener);
			Utils.RegisterFunc(L, Utils.METHOD_IDX, "RemoveEventListener", _m_RemoveEventListener);
			
			
			Utils.RegisterFunc(L, Utils.GETTER_IDX, "MainCitySys", _g_get_MainCitySys);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "AssetLoaderSvc", _g_get_AssetLoaderSvc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "ResSvc", _g_get_ResSvc);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "GameRoot", _g_get_GameRoot);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "GameObjectPoolManager", _g_get_GameObjectPoolManager);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "NetSvc", _g_get_NetSvc);
            
			
			
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
					
					LuaHelper gen_ret = new LuaHelper();
					translator.Push(L, gen_ret);
                    
					return 1;
				}
				
			}
			catch(System.Exception gen_e) {
				return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
			}
            return LuaAPI.luaL_error(L, "invalid arguments to LuaHelper constructor!");
            
        }
        
		
        
		
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddEventListener(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaHelper gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    CommonNet.CMD _protoCode;translator.Get(L, 2, out _protoCode);
                    SocketDispatcher.OnActionHandler _callback = translator.GetDelegate<SocketDispatcher.OnActionHandler>(L, 3);
                    
                    gen_to_be_invoked.AddEventListener( _protoCode, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_RemoveEventListener(RealStatePtr L)
        {
		    try {
            
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            
            
                LuaHelper gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
            
            
                
                {
                    CommonNet.CMD _protoCode;translator.Get(L, 2, out _protoCode);
                    SocketDispatcher.OnActionHandler _callback = translator.GetDelegate<SocketDispatcher.OnActionHandler>(L, 3);
                    
                    gen_to_be_invoked.RemoveEventListener( _protoCode, _callback );
                    
                    
                    
                    return 0;
                }
                
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            
        }
        
        
        
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_MainCitySys(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaHelper gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.MainCitySys);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_AssetLoaderSvc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaHelper gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.AssetLoaderSvc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_ResSvc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaHelper gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.ResSvc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_GameRoot(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaHelper gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.GameRoot);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_GameObjectPoolManager(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaHelper gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.GameObjectPoolManager);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_NetSvc(RealStatePtr L)
        {
		    try {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
			
                LuaHelper gen_to_be_invoked = (LuaHelper)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.NetSvc);
            } catch(System.Exception gen_e) {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }
        
        
        
		
		
		
		
    }
}
