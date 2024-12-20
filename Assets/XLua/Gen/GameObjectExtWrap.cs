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
    public class GameObjectExtWrap
    {
        public static void __Register(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            System.Type type = typeof(GameObjectExt);
            Utils.BeginObjectRegister(type, L, translator, 0, 0, 0, 0);






            Utils.EndObjectRegister(type, L, translator, null, null,
                null, null, null);

            Utils.BeginClassRegister(type, L, __CreateInstance, 2, 0, 0);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "GetOrAddLuaComponent", _m_GetOrAddLuaComponent_xlua_st_);






            Utils.EndClassRegister(type, L, translator);
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {
            return LuaAPI.luaL_error(L, "GameObjectExt does not have a constructor!");
        }








        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_GetOrAddLuaComponent_xlua_st_(RealStatePtr L)
        {
            try
            {

                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);




                {
                    UnityEngine.GameObject _str = (UnityEngine.GameObject)translator.GetObject(L, 1, typeof(UnityEngine.GameObject));

                    LuaViewBehaviour gen_ret = GameObjectExt.GetOrAddLuaComponent(_str);
                    translator.Push(L, gen_ret);



                    return 1;
                }

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }

        }










    }
}
