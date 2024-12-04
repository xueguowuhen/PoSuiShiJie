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
    public class LuaViewBehaviourWrap
    {
        public static void __Register(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            System.Type type = typeof(LuaViewBehaviour);
            Utils.BeginObjectRegister(type, L, translator, 0, 1, 0, 0);

            Utils.RegisterFunc(L, Utils.METHOD_IDX, "YieldAndCallback", _m_YieldAndCallback);





            Utils.EndObjectRegister(type, L, translator, null, null,
                null, null, null);

            Utils.BeginClassRegister(type, L, __CreateInstance, 1, 0, 0);






            Utils.EndClassRegister(type, L, translator);
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int __CreateInstance(RealStatePtr L)
        {

            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
                if (LuaAPI.lua_gettop(L) == 1)
                {

                    LuaViewBehaviour gen_ret = new LuaViewBehaviour();
                    translator.Push(L, gen_ret);

                    return 1;
                }

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return LuaAPI.luaL_error(L, "invalid arguments to LuaViewBehaviour constructor!");

        }








        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_YieldAndCallback(RealStatePtr L)
        {
            try
            {

                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);


                LuaViewBehaviour gen_to_be_invoked = (LuaViewBehaviour)translator.FastGetCSObj(L, 1);



                {
                    object _to_yield = translator.GetObject(L, 2, typeof(object));
                    System.Action _callback = translator.GetDelegate<System.Action>(L, 3);

                    gen_to_be_invoked.YieldAndCallback(_to_yield, _callback);



                    return 0;
                }

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }

        }










    }
}
