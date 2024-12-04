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
    public class GameRootWrap
    {
        public static void __Register(RealStatePtr L)
        {
            ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
            System.Type type = typeof(GameRoot);
            Utils.BeginObjectRegister(type, L, translator, 0, 6, 5, 5);

            Utils.RegisterFunc(L, Utils.METHOD_IDX, "ResSvcInit", _m_ResSvcInit);
            Utils.RegisterFunc(L, Utils.METHOD_IDX, "XLuaRootInit", _m_XLuaRootInit);
            Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetScreenSpaceCamera", _m_SetScreenSpaceCamera);
            Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetScreenSpaceOverlay", _m_SetScreenSpaceOverlay);
            Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPlayerData", _m_SetPlayerData);
            Utils.RegisterFunc(L, Utils.METHOD_IDX, "SetPlayerDataList", _m_SetPlayerDataList);


            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlayerData", _g_get_PlayerData);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "PlayerDataList", _g_get_PlayerDataList);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "loadingWnd", _g_get_loadingWnd);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "dynamicWnd", _g_get_dynamicWnd);
            Utils.RegisterFunc(L, Utils.GETTER_IDX, "uiCamera", _g_get_uiCamera);

            Utils.RegisterFunc(L, Utils.SETTER_IDX, "PlayerData", _s_set_PlayerData);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "PlayerDataList", _s_set_PlayerDataList);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "loadingWnd", _s_set_loadingWnd);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "dynamicWnd", _s_set_dynamicWnd);
            Utils.RegisterFunc(L, Utils.SETTER_IDX, "uiCamera", _s_set_uiCamera);


            Utils.EndObjectRegister(type, L, translator, null, null,
                null, null, null);

            Utils.BeginClassRegister(type, L, __CreateInstance, 3, 1, 1);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "AddTips", _m_AddTips_xlua_st_);
            Utils.RegisterFunc(L, Utils.CLS_IDX, "SetSocketFail", _m_SetSocketFail_xlua_st_);



            Utils.RegisterFunc(L, Utils.CLS_GETTER_IDX, "Instance", _g_get_Instance);

            Utils.RegisterFunc(L, Utils.CLS_SETTER_IDX, "Instance", _s_set_Instance);


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

                    GameRoot gen_ret = new GameRoot();
                    translator.Push(L, gen_ret);

                    return 1;
                }

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return LuaAPI.luaL_error(L, "invalid arguments to GameRoot constructor!");

        }








        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_ResSvcInit(RealStatePtr L)
        {
            try
            {

                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);


                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);



                {

                    gen_to_be_invoked.ResSvcInit();



                    return 0;
                }

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_XLuaRootInit(RealStatePtr L)
        {
            try
            {

                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);


                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);



                {

                    gen_to_be_invoked.XLuaRootInit();



                    return 0;
                }

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_AddTips_xlua_st_(RealStatePtr L)
        {
            try
            {




                {
                    string _tips = LuaAPI.lua_tostring(L, 1);

                    GameRoot.AddTips(_tips);



                    return 0;
                }

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetScreenSpaceCamera(RealStatePtr L)
        {
            try
            {

                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);


                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);



                {

                    gen_to_be_invoked.SetScreenSpaceCamera();



                    return 0;
                }

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetScreenSpaceOverlay(RealStatePtr L)
        {
            try
            {

                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);


                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);



                {

                    gen_to_be_invoked.SetScreenSpaceOverlay();



                    return 0;
                }

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetSocketFail_xlua_st_(RealStatePtr L)
        {
            try
            {




                {
                    bool _IsShow = LuaAPI.lua_toboolean(L, 1);

                    GameRoot.SetSocketFail(_IsShow);



                    return 0;
                }

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPlayerData(RealStatePtr L)
        {
            try
            {

                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);


                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);


                int gen_param_count = LuaAPI.lua_gettop(L);

                if (gen_param_count == 2 && translator.Assignable<CommonNet.PlayerData>(L, 2))
                {
                    CommonNet.PlayerData _playerData = (CommonNet.PlayerData)translator.GetObject(L, 2, typeof(CommonNet.PlayerData));

                    gen_to_be_invoked.SetPlayerData(_playerData);



                    return 0;
                }
                if (gen_param_count == 2 && translator.Assignable<CommonNet.BattleData>(L, 2))
                {
                    CommonNet.BattleData _battleData = (CommonNet.BattleData)translator.GetObject(L, 2, typeof(CommonNet.BattleData));

                    gen_to_be_invoked.SetPlayerData(_battleData);



                    return 0;
                }

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }

            return LuaAPI.luaL_error(L, "invalid arguments to GameRoot.SetPlayerData!");

        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _m_SetPlayerDataList(RealStatePtr L)
        {
            try
            {

                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);


                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);



                {
                    System.Collections.Generic.List<CommonNet.PlayerData> _playerDataList = (System.Collections.Generic.List<CommonNet.PlayerData>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<CommonNet.PlayerData>));

                    gen_to_be_invoked.SetPlayerDataList(_playerDataList);



                    return 0;
                }

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }

        }




        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlayerData(RealStatePtr L)
        {
            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);

                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.PlayerData);
            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_PlayerDataList(RealStatePtr L)
        {
            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);

                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.PlayerDataList);
            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_Instance(RealStatePtr L)
        {
            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
                translator.Push(L, GameRoot.Instance);
            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_loadingWnd(RealStatePtr L)
        {
            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);

                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.loadingWnd);
            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_dynamicWnd(RealStatePtr L)
        {
            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);

                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.dynamicWnd);
            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _g_get_uiCamera(RealStatePtr L)
        {
            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);

                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);
                translator.Push(L, gen_to_be_invoked.uiCamera);
            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 1;
        }



        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PlayerData(RealStatePtr L)
        {
            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);

                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.PlayerData = (CommonNet.PlayerData)translator.GetObject(L, 2, typeof(CommonNet.PlayerData));

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_PlayerDataList(RealStatePtr L)
        {
            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);

                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.PlayerDataList = (System.Collections.Generic.List<CommonNet.PlayerData>)translator.GetObject(L, 2, typeof(System.Collections.Generic.List<CommonNet.PlayerData>));

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_Instance(RealStatePtr L)
        {
            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);
                GameRoot.Instance = (GameRoot)translator.GetObject(L, 1, typeof(GameRoot));

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_loadingWnd(RealStatePtr L)
        {
            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);

                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.loadingWnd = (LoadingWnd)translator.GetObject(L, 2, typeof(LoadingWnd));

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_dynamicWnd(RealStatePtr L)
        {
            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);

                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.dynamicWnd = (DynamicWnd)translator.GetObject(L, 2, typeof(DynamicWnd));

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }

        [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
        static int _s_set_uiCamera(RealStatePtr L)
        {
            try
            {
                ObjectTranslator translator = ObjectTranslatorPool.Instance.Find(L);

                GameRoot gen_to_be_invoked = (GameRoot)translator.FastGetCSObj(L, 1);
                gen_to_be_invoked.uiCamera = (UnityEngine.Camera)translator.GetObject(L, 2, typeof(UnityEngine.Camera));

            }
            catch (System.Exception gen_e)
            {
                return LuaAPI.luaL_error(L, "c# exception:" + gen_e);
            }
            return 0;
        }





    }
}
