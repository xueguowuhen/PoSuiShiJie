#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using RealStatePtr = System.IntPtr;
#endif

using System;


namespace XLua
{
    public partial class DelegateBridge : DelegateBridgeBase
    {

        public void __Gen_Delegate_Imp0(CommonNet.GameMsg p0)
        {
#if THREAD_SAFE || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
            RealStatePtr L = luaEnv.rawL;
            int errFunc = LuaAPI.pcall_prepare(L, errorFuncRef, luaReference);
            ObjectTranslator translator = luaEnv.translator;
            translator.Push(L, p0);

            PCall(L, 1, 0, errFunc);



            LuaAPI.lua_settop(L, errFunc - 1);

#if THREAD_SAFE || HOTFIX_ENABLE
            }
#endif
        }

        public void __Gen_Delegate_Imp1(UnityEngine.GameObject p0)
        {
#if THREAD_SAFE || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
            RealStatePtr L = luaEnv.rawL;
            int errFunc = LuaAPI.pcall_prepare(L, errorFuncRef, luaReference);
            ObjectTranslator translator = luaEnv.translator;
            translator.Push(L, p0);

            PCall(L, 1, 0, errFunc);



            LuaAPI.lua_settop(L, errFunc - 1);

#if THREAD_SAFE || HOTFIX_ENABLE
            }
#endif
        }

        public void __Gen_Delegate_Imp2()
        {
#if THREAD_SAFE || HOTFIX_ENABLE
            lock (luaEnv.luaEnvLock)
            {
#endif
            RealStatePtr L = luaEnv.rawL;
            int errFunc = LuaAPI.pcall_prepare(L, errorFuncRef, luaReference);


            PCall(L, 0, 0, errFunc);



            LuaAPI.lua_settop(L, errFunc - 1);

#if THREAD_SAFE || HOTFIX_ENABLE
            }
#endif
        }


        static DelegateBridge()
        {
            Gen_Flag = true;
        }

        public override Delegate GetDelegateByType(Type type)
        {

            if (type == typeof(SocketDispatcher.OnActionHandler))
            {
                return new SocketDispatcher.OnActionHandler(__Gen_Delegate_Imp0);
            }

            if (type == typeof(LuaViewBehaviour.delLuaAwake))
            {
                return new LuaViewBehaviour.delLuaAwake(__Gen_Delegate_Imp1);
            }

            if (type == typeof(XLuaCustomExport.OnCreate))
            {
                return new XLuaCustomExport.OnCreate(__Gen_Delegate_Imp1);
            }

            if (type == typeof(LuaViewBehaviour.delLuaStart))
            {
                return new LuaViewBehaviour.delLuaStart(__Gen_Delegate_Imp2);
            }

            if (type == typeof(LuaViewBehaviour.delLuaUpdate))
            {
                return new LuaViewBehaviour.delLuaUpdate(__Gen_Delegate_Imp2);
            }

            if (type == typeof(LuaViewBehaviour.delLuaEnable))
            {
                return new LuaViewBehaviour.delLuaEnable(__Gen_Delegate_Imp2);
            }

            if (type == typeof(LuaViewBehaviour.delLuaDisable))
            {
                return new LuaViewBehaviour.delLuaDisable(__Gen_Delegate_Imp2);
            }

            if (type == typeof(LuaViewBehaviour.delLuaOnDestroy))
            {
                return new LuaViewBehaviour.delLuaOnDestroy(__Gen_Delegate_Imp2);
            }

            return null;
        }
    }

}