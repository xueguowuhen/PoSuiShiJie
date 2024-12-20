#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
using LuaAPI = XLua.LuaDLL.Lua;
using LuaCSFunction = XLua.LuaDLL.lua_CSFunction;
using RealStatePtr = System.IntPtr;
#endif



namespace XLua.CSObjectWrap
{
    using Utils = XLua.Utils;
    public class DGTweeningCoreABSSequentiableWrap
    {
        public static void __Register(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            System.Type type = typeof(DG.Tweening.Core.ABSSequentiable);
            Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);






            Utils.EndObjectRegister(type, L, translator, null, null,
                null, null, null);

            Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);






            Utils.EndClassRegister(type, L, translator);
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "DG.Tweening.Core.ABSSequentiable does not have a constructor!");
        }

















    }
}
