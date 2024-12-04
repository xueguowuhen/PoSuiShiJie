#if USE_UNI_LUA
using LuaAPI = UniLua.Lua;
using RealStatePtr = UniLua.ILuaState;
using LuaCSFunction = UniLua.CSharpFunctionDelegate;
#else
#endif

using System;
using System.Collections.Generic;
using System.Reflection;


namespace XLua.CSObjectWrap
{
    public class XLua_Gen_Initer_Register__
    {


        static void wrapInit0(LuaEnv luaenv, ObjectTranslator translator)
        {

            translator.DelayWrapLoader(typeof(RaffleItemCfg), RaffleItemCfgWrap.__Register);


            translator.DelayWrapLoader(typeof(GameObjectExt), GameObjectExtWrap.__Register);


            translator.DelayWrapLoader(typeof(PathDefine), PathDefineWrap.__Register);


            translator.DelayWrapLoader(typeof(GameRoot), GameRootWrap.__Register);


            translator.DelayWrapLoader(typeof(RaffleItem), RaffleItemWrap.__Register);


            translator.DelayWrapLoader(typeof(LuaViewBehaviour), LuaViewBehaviourWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.AutoPlay), DGTweeningAutoPlayWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.AxisConstraint), DGTweeningAxisConstraintWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.Ease), DGTweeningEaseWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.LogBehaviour), DGTweeningLogBehaviourWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.LoopType), DGTweeningLoopTypeWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.PathMode), DGTweeningPathModeWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.PathType), DGTweeningPathTypeWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.RotateMode), DGTweeningRotateModeWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.ScrambleMode), DGTweeningScrambleModeWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.TweenType), DGTweeningTweenTypeWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.UpdateType), DGTweeningUpdateTypeWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.DOTweenModuleUI), DGTweeningDOTweenModuleUIWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.DOTween), DGTweeningDOTweenWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.DOVirtual), DGTweeningDOVirtualWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.EaseFactory), DGTweeningEaseFactoryWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.Tweener), DGTweeningTweenerWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.Tween), DGTweeningTweenWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.Sequence), DGTweeningSequenceWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.TweenParams), DGTweeningTweenParamsWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.Core.ABSSequentiable), DGTweeningCoreABSSequentiableWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions>), DGTweeningCoreTweenerCore_3_UnityEngineVector3UnityEngineVector3DGTweeningPluginsOptionsVectorOptions_Wrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.TweenExtensions), DGTweeningTweenExtensionsWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.TweenSettingsExtensions), DGTweeningTweenSettingsExtensionsWrap.__Register);


            translator.DelayWrapLoader(typeof(DG.Tweening.ShortcutExtensions), DGTweeningShortcutExtensionsWrap.__Register);


            translator.DelayWrapLoader(typeof(CommonNet.GameMsg), CommonNetGameMsgWrap.__Register);


            translator.DelayWrapLoader(typeof(CommonNet.CMD), CommonNetCMDWrap.__Register);


            translator.DelayWrapLoader(typeof(CommonNet.Error), CommonNetErrorWrap.__Register);


            translator.DelayWrapLoader(typeof(LuaHelper), LuaHelperWrap.__Register);



        }

        static void Init(LuaEnv luaenv, ObjectTranslator translator)
        {

            wrapInit0(luaenv, translator);


        }

        static XLua_Gen_Initer_Register__()
        {
            XLua.LuaEnv.AddIniter(Init);
        }


    }

}
namespace XLua
{
    public partial class ObjectTranslator
    {
        static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ s_gen_reg_dumb_obj = new XLua.CSObjectWrap.XLua_Gen_Initer_Register__();
        static XLua.CSObjectWrap.XLua_Gen_Initer_Register__ gen_reg_dumb_obj { get { return s_gen_reg_dumb_obj; } }
    }

    internal partial class InternalGlobals
    {

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE0(UnityEngine.CanvasGroup target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE1(UnityEngine.UI.Graphic target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE2(UnityEngine.UI.Graphic target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE3(UnityEngine.UI.Image target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE4(UnityEngine.UI.Image target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE5(UnityEngine.UI.Image target, float endValue, float duration);

        delegate DG.Tweening.Sequence __GEN_DELEGATE6(UnityEngine.UI.Image target, UnityEngine.Gradient gradient, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE7(UnityEngine.UI.LayoutElement target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE8(UnityEngine.UI.LayoutElement target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE9(UnityEngine.UI.LayoutElement target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE10(UnityEngine.UI.Outline target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE11(UnityEngine.UI.Outline target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE12(UnityEngine.UI.Outline target, UnityEngine.Vector2 endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE13(UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE14(UnityEngine.RectTransform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE15(UnityEngine.RectTransform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE16(UnityEngine.RectTransform target, UnityEngine.Vector3 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE17(UnityEngine.RectTransform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE18(UnityEngine.RectTransform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE19(UnityEngine.RectTransform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE20(UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE21(UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE22(UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE23(UnityEngine.RectTransform target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE24(UnityEngine.RectTransform target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE25(UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE26(UnityEngine.RectTransform target, UnityEngine.Vector2 punch, float duration, int vibrato, float elasticity, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE27(UnityEngine.RectTransform target, float duration, float strength, int vibrato, float randomness, bool snapping, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE28(UnityEngine.RectTransform target, float duration, UnityEngine.Vector2 strength, int vibrato, float randomness, bool snapping, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Sequence __GEN_DELEGATE29(UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE30(UnityEngine.UI.ScrollRect target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE31(UnityEngine.UI.ScrollRect target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE32(UnityEngine.UI.ScrollRect target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE33(UnityEngine.UI.Slider target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE34(UnityEngine.UI.Text target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<int, int, DG.Tweening.Plugins.Options.NoOptions> __GEN_DELEGATE35(UnityEngine.UI.Text target, int fromValue, int endValue, float duration, bool addThousandsSeparator, System.Globalization.CultureInfo culture);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE36(UnityEngine.UI.Text target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<string, string, DG.Tweening.Plugins.Options.StringOptions> __GEN_DELEGATE37(UnityEngine.UI.Text target, string endValue, float duration, bool richTextEnabled, DG.Tweening.ScrambleMode scrambleMode, string scrambleChars);

        delegate DG.Tweening.Tweener __GEN_DELEGATE38(UnityEngine.UI.Graphic target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE39(UnityEngine.UI.Image target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE40(UnityEngine.UI.Text target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.CircleOptions> __GEN_DELEGATE41(UnityEngine.RectTransform target, UnityEngine.Vector2 center, float endValueDegrees, float duration, bool relativeCenter, bool snapping);

        delegate void __GEN_DELEGATE42(DG.Tweening.Tween t);

        delegate void __GEN_DELEGATE43(DG.Tweening.Tween t, bool withCallbacks);

        delegate void __GEN_DELEGATE44(DG.Tweening.Tween t);

        delegate void __GEN_DELEGATE45(DG.Tweening.Tween t);

        delegate void __GEN_DELEGATE46(DG.Tweening.Tween t, float to, bool andPlay);

        delegate void __GEN_DELEGATE47(DG.Tweening.Tween t, float to, bool andPlay);

        delegate void __GEN_DELEGATE48(DG.Tweening.Tween t, bool complete);

        delegate void __GEN_DELEGATE49(DG.Tweening.Tween t, float deltaTime, float unscaledDeltaTime);

        delegate DG.Tweening.Tween __GEN_DELEGATE50(DG.Tweening.Tween t);

        delegate DG.Tweening.Tween __GEN_DELEGATE51(DG.Tweening.Tween t);

        delegate void __GEN_DELEGATE52(DG.Tweening.Tween t);

        delegate void __GEN_DELEGATE53(DG.Tweening.Tween t);

        delegate void __GEN_DELEGATE54(DG.Tweening.Tween t, bool includeDelay, float changeDelayTo);

        delegate void __GEN_DELEGATE55(DG.Tweening.Tween t, bool includeDelay);

        delegate void __GEN_DELEGATE56(DG.Tweening.Tween t);

        delegate void __GEN_DELEGATE57(DG.Tweening.Tween t);

        delegate void __GEN_DELEGATE58(DG.Tweening.Tween t, int waypointIndex, bool andPlay);

        delegate UnityEngine.YieldInstruction __GEN_DELEGATE59(DG.Tweening.Tween t);

        delegate UnityEngine.YieldInstruction __GEN_DELEGATE60(DG.Tweening.Tween t);

        delegate UnityEngine.YieldInstruction __GEN_DELEGATE61(DG.Tweening.Tween t);

        delegate UnityEngine.YieldInstruction __GEN_DELEGATE62(DG.Tweening.Tween t, int elapsedLoops);

        delegate UnityEngine.YieldInstruction __GEN_DELEGATE63(DG.Tweening.Tween t, float position);

        delegate UnityEngine.Coroutine __GEN_DELEGATE64(DG.Tweening.Tween t);

        delegate int __GEN_DELEGATE65(DG.Tweening.Tween t);

        delegate float __GEN_DELEGATE66(DG.Tweening.Tween t);

        delegate float __GEN_DELEGATE67(DG.Tweening.Tween t);

        delegate float __GEN_DELEGATE68(DG.Tweening.Tween t, bool includeLoops);

        delegate float __GEN_DELEGATE69(DG.Tweening.Tween t, bool includeLoops);

        delegate float __GEN_DELEGATE70(DG.Tweening.Tween t, bool includeLoops);

        delegate float __GEN_DELEGATE71(DG.Tweening.Tween t);

        delegate bool __GEN_DELEGATE72(DG.Tweening.Tween t);

        delegate bool __GEN_DELEGATE73(DG.Tweening.Tween t);

        delegate bool __GEN_DELEGATE74(DG.Tweening.Tween t);

        delegate bool __GEN_DELEGATE75(DG.Tweening.Tween t);

        delegate bool __GEN_DELEGATE76(DG.Tweening.Tween t);

        delegate int __GEN_DELEGATE77(DG.Tweening.Tween t);

        delegate UnityEngine.Vector3 __GEN_DELEGATE78(DG.Tweening.Tween t, float pathPercentage);

        delegate UnityEngine.Vector3[] __GEN_DELEGATE79(DG.Tweening.Tween t, int subdivisionsXSegment);

        delegate float __GEN_DELEGATE80(DG.Tweening.Tween t);

        delegate DG.Tweening.Tween __GEN_DELEGATE81(DG.Tweening.Tween t);

        delegate DG.Tweening.Tween __GEN_DELEGATE82(DG.Tweening.Tween t, bool autoKillOnCompletion);

        delegate DG.Tweening.Tween __GEN_DELEGATE83(DG.Tweening.Tween t, object objectId);

        delegate DG.Tweening.Tween __GEN_DELEGATE84(DG.Tweening.Tween t, string stringId);

        delegate DG.Tweening.Tween __GEN_DELEGATE85(DG.Tweening.Tween t, int intId);

        delegate DG.Tweening.Tween __GEN_DELEGATE86(DG.Tweening.Tween t, UnityEngine.GameObject gameObject);

        delegate DG.Tweening.Tween __GEN_DELEGATE87(DG.Tweening.Tween t, UnityEngine.GameObject gameObject, DG.Tweening.LinkBehaviour behaviour);

        delegate DG.Tweening.Tween __GEN_DELEGATE88(DG.Tweening.Tween t, object target);

        delegate DG.Tweening.Tween __GEN_DELEGATE89(DG.Tweening.Tween t, int loops);

        delegate DG.Tweening.Tween __GEN_DELEGATE90(DG.Tweening.Tween t, int loops, DG.Tweening.LoopType loopType);

        delegate DG.Tweening.Tween __GEN_DELEGATE91(DG.Tweening.Tween t, DG.Tweening.Ease ease);

        delegate DG.Tweening.Tween __GEN_DELEGATE92(DG.Tweening.Tween t, DG.Tweening.Ease ease, float overshoot);

        delegate DG.Tweening.Tween __GEN_DELEGATE93(DG.Tweening.Tween t, DG.Tweening.Ease ease, float amplitude, float period);

        delegate DG.Tweening.Tween __GEN_DELEGATE94(DG.Tweening.Tween t, UnityEngine.AnimationCurve animCurve);

        delegate DG.Tweening.Tween __GEN_DELEGATE95(DG.Tweening.Tween t, DG.Tweening.EaseFunction customEase);

        delegate DG.Tweening.Tween __GEN_DELEGATE96(DG.Tweening.Tween t);

        delegate DG.Tweening.Tween __GEN_DELEGATE97(DG.Tweening.Tween t, bool recyclable);

        delegate DG.Tweening.Tween __GEN_DELEGATE98(DG.Tweening.Tween t, bool isIndependentUpdate);

        delegate DG.Tweening.Tween __GEN_DELEGATE99(DG.Tweening.Tween t, DG.Tweening.UpdateType updateType);

        delegate DG.Tweening.Tween __GEN_DELEGATE100(DG.Tweening.Tween t, DG.Tweening.UpdateType updateType, bool isIndependentUpdate);

        delegate DG.Tweening.Tween __GEN_DELEGATE101(DG.Tweening.Tween t);

        delegate DG.Tweening.Tween __GEN_DELEGATE102(DG.Tweening.Tween t, bool inverted);

        delegate DG.Tweening.Tween __GEN_DELEGATE103(DG.Tweening.Tween t, DG.Tweening.TweenCallback action);

        delegate DG.Tweening.Tween __GEN_DELEGATE104(DG.Tweening.Tween t, DG.Tweening.TweenCallback action);

        delegate DG.Tweening.Tween __GEN_DELEGATE105(DG.Tweening.Tween t, DG.Tweening.TweenCallback action);

        delegate DG.Tweening.Tween __GEN_DELEGATE106(DG.Tweening.Tween t, DG.Tweening.TweenCallback action);

        delegate DG.Tweening.Tween __GEN_DELEGATE107(DG.Tweening.Tween t, DG.Tweening.TweenCallback action);

        delegate DG.Tweening.Tween __GEN_DELEGATE108(DG.Tweening.Tween t, DG.Tweening.TweenCallback action);

        delegate DG.Tweening.Tween __GEN_DELEGATE109(DG.Tweening.Tween t, DG.Tweening.TweenCallback action);

        delegate DG.Tweening.Tween __GEN_DELEGATE110(DG.Tweening.Tween t, DG.Tweening.TweenCallback action);

        delegate DG.Tweening.Tween __GEN_DELEGATE111(DG.Tweening.Tween t, DG.Tweening.TweenCallback<int> action);

        delegate DG.Tweening.Tween __GEN_DELEGATE112(DG.Tweening.Tween t, DG.Tweening.Tween asTween);

        delegate DG.Tweening.Tween __GEN_DELEGATE113(DG.Tweening.Tween t, DG.Tweening.TweenParams tweenParams);

        delegate DG.Tweening.Sequence __GEN_DELEGATE114(DG.Tweening.Sequence s, DG.Tweening.Tween t);

        delegate DG.Tweening.Sequence __GEN_DELEGATE115(DG.Tweening.Sequence s, DG.Tweening.Tween t);

        delegate DG.Tweening.Sequence __GEN_DELEGATE116(DG.Tweening.Sequence s, DG.Tweening.Tween t);

        delegate DG.Tweening.Sequence __GEN_DELEGATE117(DG.Tweening.Sequence s, float atPosition, DG.Tweening.Tween t);

        delegate DG.Tweening.Sequence __GEN_DELEGATE118(DG.Tweening.Sequence s, float interval);

        delegate DG.Tweening.Sequence __GEN_DELEGATE119(DG.Tweening.Sequence s, float interval);

        delegate DG.Tweening.Sequence __GEN_DELEGATE120(DG.Tweening.Sequence s, DG.Tweening.TweenCallback callback);

        delegate DG.Tweening.Sequence __GEN_DELEGATE121(DG.Tweening.Sequence s, DG.Tweening.TweenCallback callback);

        delegate DG.Tweening.Sequence __GEN_DELEGATE122(DG.Tweening.Sequence s, float atPosition, DG.Tweening.TweenCallback callback);

        delegate DG.Tweening.Tweener __GEN_DELEGATE123(DG.Tweening.Tweener t);

        delegate DG.Tweening.Tweener __GEN_DELEGATE124(DG.Tweening.Tweener t, bool isRelative);

        delegate DG.Tweening.Tweener __GEN_DELEGATE125(DG.Tweening.Tweener t, bool setImmediately, bool isRelative);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE126(DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> t, float fromAlphaValue, bool setImmediately, bool isRelative);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE127(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> t, float fromValue, bool setImmediately, bool isRelative);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.CircleOptions> __GEN_DELEGATE128(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.CircleOptions> t, float fromValueDegrees, bool setImmediately, bool isRelative);

        delegate DG.Tweening.Tween __GEN_DELEGATE129(DG.Tweening.Tween t, float delay);

        delegate DG.Tweening.Tween __GEN_DELEGATE130(DG.Tweening.Tween t, float delay, bool asPrependedIntervalIfSequence);

        delegate DG.Tweening.Tween __GEN_DELEGATE131(DG.Tweening.Tween t);

        delegate DG.Tweening.Tween __GEN_DELEGATE132(DG.Tweening.Tween t, bool isRelative);

        delegate DG.Tweening.Tween __GEN_DELEGATE133(DG.Tweening.Tween t);

        delegate DG.Tweening.Tween __GEN_DELEGATE134(DG.Tweening.Tween t, bool isSpeedBased);

        delegate DG.Tweening.Tweener __GEN_DELEGATE135(DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> t, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE136(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> t, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE137(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> t, DG.Tweening.AxisConstraint axisConstraint, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE138(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> t, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE139(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> t, DG.Tweening.AxisConstraint axisConstraint, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE140(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions> t, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE141(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions> t, DG.Tweening.AxisConstraint axisConstraint, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE142(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> t, bool useShortest360Route);

        delegate DG.Tweening.Tweener __GEN_DELEGATE143(DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> t, bool alphaOnly);

        delegate DG.Tweening.Tweener __GEN_DELEGATE144(DG.Tweening.Core.TweenerCore<UnityEngine.Rect, UnityEngine.Rect, DG.Tweening.Plugins.Options.RectOptions> t, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE145(DG.Tweening.Core.TweenerCore<string, string, DG.Tweening.Plugins.Options.StringOptions> t, bool richTextEnabled, DG.Tweening.ScrambleMode scrambleMode, string scrambleChars);

        delegate DG.Tweening.Tweener __GEN_DELEGATE146(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE147(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, DG.Tweening.AxisConstraint axisConstraint, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE148(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.CircleOptions> t, float endValueDegrees, bool relativeCenter, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE149(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, DG.Tweening.AxisConstraint lockPosition, DG.Tweening.AxisConstraint lockRotation);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE150(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, bool closePath, DG.Tweening.AxisConstraint lockPosition, DG.Tweening.AxisConstraint lockRotation);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE151(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Vector3 lookAtPosition, System.Nullable<UnityEngine.Vector3> forwardDirection, System.Nullable<UnityEngine.Vector3> up);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE152(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Vector3 lookAtPosition, bool stableZRotation);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE153(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Transform lookAtTransform, System.Nullable<UnityEngine.Vector3> forwardDirection, System.Nullable<UnityEngine.Vector3> up);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE154(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Transform lookAtTransform, bool stableZRotation);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE155(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, float lookAhead, System.Nullable<UnityEngine.Vector3> forwardDirection, System.Nullable<UnityEngine.Vector3> up);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE156(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, float lookAhead, bool stableZRotation);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE157(UnityEngine.Camera target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE158(UnityEngine.Camera target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE159(UnityEngine.Camera target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE160(UnityEngine.Camera target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE161(UnityEngine.Camera target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE162(UnityEngine.Camera target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Rect, UnityEngine.Rect, DG.Tweening.Plugins.Options.RectOptions> __GEN_DELEGATE163(UnityEngine.Camera target, UnityEngine.Rect endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Rect, UnityEngine.Rect, DG.Tweening.Plugins.Options.RectOptions> __GEN_DELEGATE164(UnityEngine.Camera target, UnityEngine.Rect endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE165(UnityEngine.Camera target, float duration, float strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE166(UnityEngine.Camera target, float duration, UnityEngine.Vector3 strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE167(UnityEngine.Camera target, float duration, float strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE168(UnityEngine.Camera target, float duration, UnityEngine.Vector3 strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE169(UnityEngine.Light target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE170(UnityEngine.Light target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE171(UnityEngine.Light target, float endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE172(UnityEngine.LineRenderer target, DG.Tweening.Color2 startValue, DG.Tweening.Color2 endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE173(UnityEngine.Material target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE174(UnityEngine.Material target, UnityEngine.Color endValue, string property, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE175(UnityEngine.Material target, UnityEngine.Color endValue, int propertyID, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE176(UnityEngine.Material target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE177(UnityEngine.Material target, float endValue, string property, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE178(UnityEngine.Material target, float endValue, int propertyID, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE179(UnityEngine.Material target, float endValue, string property, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE180(UnityEngine.Material target, float endValue, int propertyID, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE181(UnityEngine.Material target, UnityEngine.Vector2 endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE182(UnityEngine.Material target, UnityEngine.Vector2 endValue, string property, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE183(UnityEngine.Material target, UnityEngine.Vector2 endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE184(UnityEngine.Material target, UnityEngine.Vector2 endValue, string property, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE185(UnityEngine.Material target, UnityEngine.Vector4 endValue, string property, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE186(UnityEngine.Material target, UnityEngine.Vector4 endValue, int propertyID, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE187(UnityEngine.TrailRenderer target, float toStartWidth, float toEndWidth, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE188(UnityEngine.TrailRenderer target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE189(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE190(UnityEngine.Transform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE191(UnityEngine.Transform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE192(UnityEngine.Transform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE193(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE194(UnityEngine.Transform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE195(UnityEngine.Transform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE196(UnityEngine.Transform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> __GEN_DELEGATE197(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration, DG.Tweening.RotateMode mode);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Quaternion, DG.Tweening.Plugins.Options.NoOptions> __GEN_DELEGATE198(UnityEngine.Transform target, UnityEngine.Quaternion endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> __GEN_DELEGATE199(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration, DG.Tweening.RotateMode mode);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Quaternion, DG.Tweening.Plugins.Options.NoOptions> __GEN_DELEGATE200(UnityEngine.Transform target, UnityEngine.Quaternion endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE201(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE202(UnityEngine.Transform target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE203(UnityEngine.Transform target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE204(UnityEngine.Transform target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE205(UnityEngine.Transform target, float endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE206(UnityEngine.Transform target, UnityEngine.Vector3 towards, float duration, DG.Tweening.AxisConstraint axisConstraint, System.Nullable<UnityEngine.Vector3> up);

        delegate DG.Tweening.Tweener __GEN_DELEGATE207(UnityEngine.Transform target, UnityEngine.Vector3 towards, float duration, DG.Tweening.AxisConstraint axisConstraint, System.Nullable<UnityEngine.Vector3> up);

        delegate DG.Tweening.Tweener __GEN_DELEGATE208(UnityEngine.Transform target, UnityEngine.Vector3 punch, float duration, int vibrato, float elasticity, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE209(UnityEngine.Transform target, UnityEngine.Vector3 punch, float duration, int vibrato, float elasticity);

        delegate DG.Tweening.Tweener __GEN_DELEGATE210(UnityEngine.Transform target, UnityEngine.Vector3 punch, float duration, int vibrato, float elasticity);

        delegate DG.Tweening.Tweener __GEN_DELEGATE211(UnityEngine.Transform target, float duration, float strength, int vibrato, float randomness, bool snapping, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE212(UnityEngine.Transform target, float duration, UnityEngine.Vector3 strength, int vibrato, float randomness, bool snapping, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE213(UnityEngine.Transform target, float duration, float strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE214(UnityEngine.Transform target, float duration, UnityEngine.Vector3 strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE215(UnityEngine.Transform target, float duration, float strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE216(UnityEngine.Transform target, float duration, UnityEngine.Vector3 strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Sequence __GEN_DELEGATE217(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping);

        delegate DG.Tweening.Sequence __GEN_DELEGATE218(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE219(UnityEngine.Transform target, UnityEngine.Vector3[] path, float duration, DG.Tweening.PathType pathType, DG.Tweening.PathMode pathMode, int resolution, System.Nullable<UnityEngine.Color> gizmoColor);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE220(UnityEngine.Transform target, UnityEngine.Vector3[] path, float duration, DG.Tweening.PathType pathType, DG.Tweening.PathMode pathMode, int resolution, System.Nullable<UnityEngine.Color> gizmoColor);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE221(UnityEngine.Transform target, DG.Tweening.Plugins.Core.PathCore.Path path, float duration, DG.Tweening.PathMode pathMode);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE222(UnityEngine.Transform target, DG.Tweening.Plugins.Core.PathCore.Path path, float duration, DG.Tweening.PathMode pathMode);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE223(DG.Tweening.Tween target, float endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE224(UnityEngine.Light target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE225(UnityEngine.Material target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE226(UnityEngine.Material target, UnityEngine.Color endValue, string property, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE227(UnityEngine.Material target, UnityEngine.Color endValue, int propertyID, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE228(UnityEngine.Transform target, UnityEngine.Vector3 byValue, float duration, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE229(UnityEngine.Transform target, UnityEngine.Vector3 byValue, float duration, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE230(UnityEngine.Transform target, UnityEngine.Vector3 byValue, float duration, DG.Tweening.RotateMode mode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE231(UnityEngine.Transform target, UnityEngine.Vector3 byValue, float duration, DG.Tweening.RotateMode mode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE232(UnityEngine.Transform target, UnityEngine.Vector3 punch, float duration, int vibrato, float elasticity);

        delegate DG.Tweening.Tweener __GEN_DELEGATE233(UnityEngine.Transform target, UnityEngine.Vector3 byValue, float duration);

        delegate int __GEN_DELEGATE234(UnityEngine.Component target, bool withCallbacks);

        delegate int __GEN_DELEGATE235(UnityEngine.Material target, bool withCallbacks);

        delegate int __GEN_DELEGATE236(UnityEngine.Component target, bool complete);

        delegate int __GEN_DELEGATE237(UnityEngine.Material target, bool complete);

        delegate int __GEN_DELEGATE238(UnityEngine.Component target);

        delegate int __GEN_DELEGATE239(UnityEngine.Material target);

        delegate int __GEN_DELEGATE240(UnityEngine.Component target, float to, bool andPlay);

        delegate int __GEN_DELEGATE241(UnityEngine.Material target, float to, bool andPlay);

        delegate int __GEN_DELEGATE242(UnityEngine.Component target);

        delegate int __GEN_DELEGATE243(UnityEngine.Material target);

        delegate int __GEN_DELEGATE244(UnityEngine.Component target);

        delegate int __GEN_DELEGATE245(UnityEngine.Material target);

        delegate int __GEN_DELEGATE246(UnityEngine.Component target);

        delegate int __GEN_DELEGATE247(UnityEngine.Material target);

        delegate int __GEN_DELEGATE248(UnityEngine.Component target);

        delegate int __GEN_DELEGATE249(UnityEngine.Material target);

        delegate int __GEN_DELEGATE250(UnityEngine.Component target, bool includeDelay);

        delegate int __GEN_DELEGATE251(UnityEngine.Material target, bool includeDelay);

        delegate int __GEN_DELEGATE252(UnityEngine.Component target, bool includeDelay);

        delegate int __GEN_DELEGATE253(UnityEngine.Material target, bool includeDelay);

        delegate int __GEN_DELEGATE254(UnityEngine.Component target);

        delegate int __GEN_DELEGATE255(UnityEngine.Material target);

        delegate int __GEN_DELEGATE256(UnityEngine.Component target);

        delegate int __GEN_DELEGATE257(UnityEngine.Material target);

        delegate void __GEN_DELEGATE258(UnityEngine.UI.Text txtObj, string text, bool isAnimation, DG.Tweening.ScrambleMode scrambleMode);

        delegate UnityEngine.Sprite __GEN_DELEGATE259(UnityEngine.Texture2D texture);

        delegate void __GEN_DELEGATE260(UnityEngine.UI.Text txtObj, int endValue, bool isAnimation, float duration);

        delegate void __GEN_DELEGATE261(UnityEngine.UI.Image txtObj, float endValue, bool isAnimation, float duration);

        delegate bool __GEN_DELEGATE262(UnityEngine.Animator animator, string tagName, int indexLayer);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE263(UnityEngine.CanvasGroup target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE264(UnityEngine.UI.Graphic target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE265(UnityEngine.UI.Graphic target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE266(UnityEngine.UI.Image target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE267(UnityEngine.UI.Image target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE268(UnityEngine.UI.Image target, float endValue, float duration);

        delegate DG.Tweening.Sequence __GEN_DELEGATE269(UnityEngine.UI.Image target, UnityEngine.Gradient gradient, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE270(UnityEngine.UI.LayoutElement target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE271(UnityEngine.UI.LayoutElement target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE272(UnityEngine.UI.LayoutElement target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE273(UnityEngine.UI.Outline target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE274(UnityEngine.UI.Outline target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE275(UnityEngine.UI.Outline target, UnityEngine.Vector2 endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE276(UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE277(UnityEngine.RectTransform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE278(UnityEngine.RectTransform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE279(UnityEngine.RectTransform target, UnityEngine.Vector3 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE280(UnityEngine.RectTransform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE281(UnityEngine.RectTransform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE282(UnityEngine.RectTransform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE283(UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE284(UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE285(UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE286(UnityEngine.RectTransform target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE287(UnityEngine.RectTransform target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE288(UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE289(UnityEngine.RectTransform target, UnityEngine.Vector2 punch, float duration, int vibrato, float elasticity, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE290(UnityEngine.RectTransform target, float duration, float strength, int vibrato, float randomness, bool snapping, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE291(UnityEngine.RectTransform target, float duration, UnityEngine.Vector2 strength, int vibrato, float randomness, bool snapping, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Sequence __GEN_DELEGATE292(UnityEngine.RectTransform target, UnityEngine.Vector2 endValue, float jumpPower, int numJumps, float duration, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE293(UnityEngine.UI.ScrollRect target, UnityEngine.Vector2 endValue, float duration, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE294(UnityEngine.UI.ScrollRect target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE295(UnityEngine.UI.ScrollRect target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE296(UnityEngine.UI.Slider target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE297(UnityEngine.UI.Text target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<int, int, DG.Tweening.Plugins.Options.NoOptions> __GEN_DELEGATE298(UnityEngine.UI.Text target, int fromValue, int endValue, float duration, bool addThousandsSeparator, System.Globalization.CultureInfo culture);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE299(UnityEngine.UI.Text target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<string, string, DG.Tweening.Plugins.Options.StringOptions> __GEN_DELEGATE300(UnityEngine.UI.Text target, string endValue, float duration, bool richTextEnabled, DG.Tweening.ScrambleMode scrambleMode, string scrambleChars);

        delegate DG.Tweening.Tweener __GEN_DELEGATE301(UnityEngine.UI.Graphic target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE302(UnityEngine.UI.Image target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE303(UnityEngine.UI.Text target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.CircleOptions> __GEN_DELEGATE304(UnityEngine.RectTransform target, UnityEngine.Vector2 center, float endValueDegrees, float duration, bool relativeCenter, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE305(DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> t, float fromAlphaValue, bool setImmediately, bool isRelative);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.CircleOptions> __GEN_DELEGATE306(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.CircleOptions> t, float fromValueDegrees, bool setImmediately, bool isRelative);

        delegate DG.Tweening.Tweener __GEN_DELEGATE307(DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> t, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE308(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> t, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE309(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> t, DG.Tweening.AxisConstraint axisConstraint, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE310(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions> t, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE311(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions> t, DG.Tweening.AxisConstraint axisConstraint, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE312(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> t, bool useShortest360Route);

        delegate DG.Tweening.Tweener __GEN_DELEGATE313(DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> t, bool alphaOnly);

        delegate DG.Tweening.Tweener __GEN_DELEGATE314(DG.Tweening.Core.TweenerCore<UnityEngine.Rect, UnityEngine.Rect, DG.Tweening.Plugins.Options.RectOptions> t, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE315(DG.Tweening.Core.TweenerCore<string, string, DG.Tweening.Plugins.Options.StringOptions> t, bool richTextEnabled, DG.Tweening.ScrambleMode scrambleMode, string scrambleChars);

        delegate DG.Tweening.Tweener __GEN_DELEGATE316(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE317(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions> t, DG.Tweening.AxisConstraint axisConstraint, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE318(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.CircleOptions> t, float endValueDegrees, bool relativeCenter, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE319(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, DG.Tweening.AxisConstraint lockPosition, DG.Tweening.AxisConstraint lockRotation);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE320(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, bool closePath, DG.Tweening.AxisConstraint lockPosition, DG.Tweening.AxisConstraint lockRotation);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE321(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Vector3 lookAtPosition, System.Nullable<UnityEngine.Vector3> forwardDirection, System.Nullable<UnityEngine.Vector3> up);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE322(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Vector3 lookAtPosition, bool stableZRotation);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE323(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Transform lookAtTransform, System.Nullable<UnityEngine.Vector3> forwardDirection, System.Nullable<UnityEngine.Vector3> up);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE324(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, UnityEngine.Transform lookAtTransform, bool stableZRotation);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE325(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, float lookAhead, System.Nullable<UnityEngine.Vector3> forwardDirection, System.Nullable<UnityEngine.Vector3> up);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE326(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> t, float lookAhead, bool stableZRotation);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE327(UnityEngine.Camera target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE328(UnityEngine.Camera target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE329(UnityEngine.Camera target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE330(UnityEngine.Camera target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE331(UnityEngine.Camera target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE332(UnityEngine.Camera target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Rect, UnityEngine.Rect, DG.Tweening.Plugins.Options.RectOptions> __GEN_DELEGATE333(UnityEngine.Camera target, UnityEngine.Rect endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Rect, UnityEngine.Rect, DG.Tweening.Plugins.Options.RectOptions> __GEN_DELEGATE334(UnityEngine.Camera target, UnityEngine.Rect endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE335(UnityEngine.Camera target, float duration, float strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE336(UnityEngine.Camera target, float duration, UnityEngine.Vector3 strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE337(UnityEngine.Camera target, float duration, float strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE338(UnityEngine.Camera target, float duration, UnityEngine.Vector3 strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE339(UnityEngine.Light target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE340(UnityEngine.Light target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE341(UnityEngine.Light target, float endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE342(UnityEngine.LineRenderer target, DG.Tweening.Color2 startValue, DG.Tweening.Color2 endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE343(UnityEngine.Material target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE344(UnityEngine.Material target, UnityEngine.Color endValue, string property, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE345(UnityEngine.Material target, UnityEngine.Color endValue, int propertyID, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE346(UnityEngine.Material target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE347(UnityEngine.Material target, float endValue, string property, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions> __GEN_DELEGATE348(UnityEngine.Material target, float endValue, int propertyID, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE349(UnityEngine.Material target, float endValue, string property, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE350(UnityEngine.Material target, float endValue, int propertyID, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE351(UnityEngine.Material target, UnityEngine.Vector2 endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE352(UnityEngine.Material target, UnityEngine.Vector2 endValue, string property, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE353(UnityEngine.Material target, UnityEngine.Vector2 endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE354(UnityEngine.Material target, UnityEngine.Vector2 endValue, string property, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE355(UnityEngine.Material target, UnityEngine.Vector4 endValue, string property, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE356(UnityEngine.Material target, UnityEngine.Vector4 endValue, int propertyID, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE357(UnityEngine.TrailRenderer target, float toStartWidth, float toEndWidth, float duration);

        delegate DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions> __GEN_DELEGATE358(UnityEngine.TrailRenderer target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE359(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE360(UnityEngine.Transform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE361(UnityEngine.Transform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE362(UnityEngine.Transform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE363(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE364(UnityEngine.Transform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE365(UnityEngine.Transform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE366(UnityEngine.Transform target, float endValue, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> __GEN_DELEGATE367(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration, DG.Tweening.RotateMode mode);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Quaternion, DG.Tweening.Plugins.Options.NoOptions> __GEN_DELEGATE368(UnityEngine.Transform target, UnityEngine.Quaternion endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Vector3, DG.Tweening.Plugins.Options.QuaternionOptions> __GEN_DELEGATE369(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration, DG.Tweening.RotateMode mode);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Quaternion, DG.Tweening.Plugins.Options.NoOptions> __GEN_DELEGATE370(UnityEngine.Transform target, UnityEngine.Quaternion endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE371(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE372(UnityEngine.Transform target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE373(UnityEngine.Transform target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE374(UnityEngine.Transform target, float endValue, float duration);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions> __GEN_DELEGATE375(UnityEngine.Transform target, float endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE376(UnityEngine.Transform target, UnityEngine.Vector3 towards, float duration, DG.Tweening.AxisConstraint axisConstraint, System.Nullable<UnityEngine.Vector3> up);

        delegate DG.Tweening.Tweener __GEN_DELEGATE377(UnityEngine.Transform target, UnityEngine.Vector3 towards, float duration, DG.Tweening.AxisConstraint axisConstraint, System.Nullable<UnityEngine.Vector3> up);

        delegate DG.Tweening.Tweener __GEN_DELEGATE378(UnityEngine.Transform target, UnityEngine.Vector3 punch, float duration, int vibrato, float elasticity, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE379(UnityEngine.Transform target, UnityEngine.Vector3 punch, float duration, int vibrato, float elasticity);

        delegate DG.Tweening.Tweener __GEN_DELEGATE380(UnityEngine.Transform target, UnityEngine.Vector3 punch, float duration, int vibrato, float elasticity);

        delegate DG.Tweening.Tweener __GEN_DELEGATE381(UnityEngine.Transform target, float duration, float strength, int vibrato, float randomness, bool snapping, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE382(UnityEngine.Transform target, float duration, UnityEngine.Vector3 strength, int vibrato, float randomness, bool snapping, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE383(UnityEngine.Transform target, float duration, float strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE384(UnityEngine.Transform target, float duration, UnityEngine.Vector3 strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE385(UnityEngine.Transform target, float duration, float strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE386(UnityEngine.Transform target, float duration, UnityEngine.Vector3 strength, int vibrato, float randomness, bool fadeOut, DG.Tweening.ShakeRandomnessMode randomnessMode);

        delegate DG.Tweening.Sequence __GEN_DELEGATE387(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping);

        delegate DG.Tweening.Sequence __GEN_DELEGATE388(UnityEngine.Transform target, UnityEngine.Vector3 endValue, float jumpPower, int numJumps, float duration, bool snapping);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE389(UnityEngine.Transform target, UnityEngine.Vector3[] path, float duration, DG.Tweening.PathType pathType, DG.Tweening.PathMode pathMode, int resolution, System.Nullable<UnityEngine.Color> gizmoColor);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE390(UnityEngine.Transform target, UnityEngine.Vector3[] path, float duration, DG.Tweening.PathType pathType, DG.Tweening.PathMode pathMode, int resolution, System.Nullable<UnityEngine.Color> gizmoColor);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE391(UnityEngine.Transform target, DG.Tweening.Plugins.Core.PathCore.Path path, float duration, DG.Tweening.PathMode pathMode);

        delegate DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions> __GEN_DELEGATE392(UnityEngine.Transform target, DG.Tweening.Plugins.Core.PathCore.Path path, float duration, DG.Tweening.PathMode pathMode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE393(UnityEngine.Light target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE394(UnityEngine.Material target, UnityEngine.Color endValue, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE395(UnityEngine.Material target, UnityEngine.Color endValue, string property, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE396(UnityEngine.Material target, UnityEngine.Color endValue, int propertyID, float duration);

        delegate DG.Tweening.Tweener __GEN_DELEGATE397(UnityEngine.Transform target, UnityEngine.Vector3 byValue, float duration, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE398(UnityEngine.Transform target, UnityEngine.Vector3 byValue, float duration, bool snapping);

        delegate DG.Tweening.Tweener __GEN_DELEGATE399(UnityEngine.Transform target, UnityEngine.Vector3 byValue, float duration, DG.Tweening.RotateMode mode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE400(UnityEngine.Transform target, UnityEngine.Vector3 byValue, float duration, DG.Tweening.RotateMode mode);

        delegate DG.Tweening.Tweener __GEN_DELEGATE401(UnityEngine.Transform target, UnityEngine.Vector3 punch, float duration, int vibrato, float elasticity);

        delegate DG.Tweening.Tweener __GEN_DELEGATE402(UnityEngine.Transform target, UnityEngine.Vector3 byValue, float duration);

        delegate int __GEN_DELEGATE403(UnityEngine.Component target, bool withCallbacks);

        delegate int __GEN_DELEGATE404(UnityEngine.Material target, bool withCallbacks);

        delegate int __GEN_DELEGATE405(UnityEngine.Component target, bool complete);

        delegate int __GEN_DELEGATE406(UnityEngine.Material target, bool complete);

        delegate int __GEN_DELEGATE407(UnityEngine.Component target);

        delegate int __GEN_DELEGATE408(UnityEngine.Material target);

        delegate int __GEN_DELEGATE409(UnityEngine.Component target, float to, bool andPlay);

        delegate int __GEN_DELEGATE410(UnityEngine.Material target, float to, bool andPlay);

        delegate int __GEN_DELEGATE411(UnityEngine.Component target);

        delegate int __GEN_DELEGATE412(UnityEngine.Material target);

        delegate int __GEN_DELEGATE413(UnityEngine.Component target);

        delegate int __GEN_DELEGATE414(UnityEngine.Material target);

        delegate int __GEN_DELEGATE415(UnityEngine.Component target);

        delegate int __GEN_DELEGATE416(UnityEngine.Material target);

        delegate int __GEN_DELEGATE417(UnityEngine.Component target);

        delegate int __GEN_DELEGATE418(UnityEngine.Material target);

        delegate int __GEN_DELEGATE419(UnityEngine.Component target, bool includeDelay);

        delegate int __GEN_DELEGATE420(UnityEngine.Material target, bool includeDelay);

        delegate int __GEN_DELEGATE421(UnityEngine.Component target, bool includeDelay);

        delegate int __GEN_DELEGATE422(UnityEngine.Material target, bool includeDelay);

        delegate int __GEN_DELEGATE423(UnityEngine.Component target);

        delegate int __GEN_DELEGATE424(UnityEngine.Material target);

        delegate int __GEN_DELEGATE425(UnityEngine.Component target);

        delegate int __GEN_DELEGATE426(UnityEngine.Material target);

        static InternalGlobals()
        {
            extensionMethodMap = new Dictionary<Type, IEnumerable<MethodInfo>>()
            {

                {typeof(UnityEngine.CanvasGroup), new List<MethodInfo>(){

                  new __GEN_DELEGATE0(DG.Tweening.DOTweenModuleUI.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE263(DG.Tweening.DOTweenModuleUI.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.UI.Graphic), new List<MethodInfo>(){

                  new __GEN_DELEGATE1(DG.Tweening.DOTweenModuleUI.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE2(DG.Tweening.DOTweenModuleUI.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE38(DG.Tweening.DOTweenModuleUI.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE264(DG.Tweening.DOTweenModuleUI.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE265(DG.Tweening.DOTweenModuleUI.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE301(DG.Tweening.DOTweenModuleUI.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.UI.Image), new List<MethodInfo>(){

                  new __GEN_DELEGATE3(DG.Tweening.DOTweenModuleUI.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE4(DG.Tweening.DOTweenModuleUI.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE5(DG.Tweening.DOTweenModuleUI.DOFillAmount)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE6(DG.Tweening.DOTweenModuleUI.DOGradientColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE39(DG.Tweening.DOTweenModuleUI.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE261(GameObjectExt.SetImageFillAmount)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE266(DG.Tweening.DOTweenModuleUI.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE267(DG.Tweening.DOTweenModuleUI.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE268(DG.Tweening.DOTweenModuleUI.DOFillAmount)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE269(DG.Tweening.DOTweenModuleUI.DOGradientColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE302(DG.Tweening.DOTweenModuleUI.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.UI.LayoutElement), new List<MethodInfo>(){

                  new __GEN_DELEGATE7(DG.Tweening.DOTweenModuleUI.DOFlexibleSize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE8(DG.Tweening.DOTweenModuleUI.DOMinSize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE9(DG.Tweening.DOTweenModuleUI.DOPreferredSize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE270(DG.Tweening.DOTweenModuleUI.DOFlexibleSize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE271(DG.Tweening.DOTweenModuleUI.DOMinSize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE272(DG.Tweening.DOTweenModuleUI.DOPreferredSize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.UI.Outline), new List<MethodInfo>(){

                  new __GEN_DELEGATE10(DG.Tweening.DOTweenModuleUI.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE11(DG.Tweening.DOTweenModuleUI.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE12(DG.Tweening.DOTweenModuleUI.DOScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE273(DG.Tweening.DOTweenModuleUI.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE274(DG.Tweening.DOTweenModuleUI.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE275(DG.Tweening.DOTweenModuleUI.DOScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.RectTransform), new List<MethodInfo>(){

                  new __GEN_DELEGATE13(DG.Tweening.DOTweenModuleUI.DOAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE14(DG.Tweening.DOTweenModuleUI.DOAnchorPosX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE15(DG.Tweening.DOTweenModuleUI.DOAnchorPosY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE16(DG.Tweening.DOTweenModuleUI.DOAnchorPos3D)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE17(DG.Tweening.DOTweenModuleUI.DOAnchorPos3DX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE18(DG.Tweening.DOTweenModuleUI.DOAnchorPos3DY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE19(DG.Tweening.DOTweenModuleUI.DOAnchorPos3DZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE20(DG.Tweening.DOTweenModuleUI.DOAnchorMax)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE21(DG.Tweening.DOTweenModuleUI.DOAnchorMin)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE22(DG.Tweening.DOTweenModuleUI.DOPivot)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE23(DG.Tweening.DOTweenModuleUI.DOPivotX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE24(DG.Tweening.DOTweenModuleUI.DOPivotY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE25(DG.Tweening.DOTweenModuleUI.DOSizeDelta)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE26(DG.Tweening.DOTweenModuleUI.DOPunchAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE27(DG.Tweening.DOTweenModuleUI.DOShakeAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE28(DG.Tweening.DOTweenModuleUI.DOShakeAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE29(DG.Tweening.DOTweenModuleUI.DOJumpAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE41(DG.Tweening.DOTweenModuleUI.DOShapeCircle)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE276(DG.Tweening.DOTweenModuleUI.DOAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE277(DG.Tweening.DOTweenModuleUI.DOAnchorPosX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE278(DG.Tweening.DOTweenModuleUI.DOAnchorPosY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE279(DG.Tweening.DOTweenModuleUI.DOAnchorPos3D)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE280(DG.Tweening.DOTweenModuleUI.DOAnchorPos3DX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE281(DG.Tweening.DOTweenModuleUI.DOAnchorPos3DY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE282(DG.Tweening.DOTweenModuleUI.DOAnchorPos3DZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE283(DG.Tweening.DOTweenModuleUI.DOAnchorMax)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE284(DG.Tweening.DOTweenModuleUI.DOAnchorMin)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE285(DG.Tweening.DOTweenModuleUI.DOPivot)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE286(DG.Tweening.DOTweenModuleUI.DOPivotX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE287(DG.Tweening.DOTweenModuleUI.DOPivotY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE288(DG.Tweening.DOTweenModuleUI.DOSizeDelta)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE289(DG.Tweening.DOTweenModuleUI.DOPunchAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE290(DG.Tweening.DOTweenModuleUI.DOShakeAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE291(DG.Tweening.DOTweenModuleUI.DOShakeAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE292(DG.Tweening.DOTweenModuleUI.DOJumpAnchorPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE304(DG.Tweening.DOTweenModuleUI.DOShapeCircle)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.UI.ScrollRect), new List<MethodInfo>(){

                  new __GEN_DELEGATE30(DG.Tweening.DOTweenModuleUI.DONormalizedPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE31(DG.Tweening.DOTweenModuleUI.DOHorizontalNormalizedPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE32(DG.Tweening.DOTweenModuleUI.DOVerticalNormalizedPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE293(DG.Tweening.DOTweenModuleUI.DONormalizedPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE294(DG.Tweening.DOTweenModuleUI.DOHorizontalNormalizedPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE295(DG.Tweening.DOTweenModuleUI.DOVerticalNormalizedPos)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.UI.Slider), new List<MethodInfo>(){

                  new __GEN_DELEGATE33(DG.Tweening.DOTweenModuleUI.DOValue)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE296(DG.Tweening.DOTweenModuleUI.DOValue)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.UI.Text), new List<MethodInfo>(){

                  new __GEN_DELEGATE34(DG.Tweening.DOTweenModuleUI.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE35(DG.Tweening.DOTweenModuleUI.DOCounter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE36(DG.Tweening.DOTweenModuleUI.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE37(DG.Tweening.DOTweenModuleUI.DOText)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE40(DG.Tweening.DOTweenModuleUI.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE258(GameObjectExt.SetText)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE260(GameObjectExt.SetText)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE297(DG.Tweening.DOTweenModuleUI.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE298(DG.Tweening.DOTweenModuleUI.DOCounter)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE299(DG.Tweening.DOTweenModuleUI.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE300(DG.Tweening.DOTweenModuleUI.DOText)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE303(DG.Tweening.DOTweenModuleUI.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Tween), new List<MethodInfo>(){

                  new __GEN_DELEGATE42(DG.Tweening.TweenExtensions.Complete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE43(DG.Tweening.TweenExtensions.Complete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE44(DG.Tweening.TweenExtensions.Flip)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE45(DG.Tweening.TweenExtensions.ForceInit)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE46(DG.Tweening.TweenExtensions.Goto)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE47(DG.Tweening.TweenExtensions.GotoWithCallbacks)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE48(DG.Tweening.TweenExtensions.Kill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE49(DG.Tweening.TweenExtensions.ManualUpdate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE50(DG.Tweening.TweenExtensions.Pause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE51(DG.Tweening.TweenExtensions.Play)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE52(DG.Tweening.TweenExtensions.PlayBackwards)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE53(DG.Tweening.TweenExtensions.PlayForward)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE54(DG.Tweening.TweenExtensions.Restart)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE55(DG.Tweening.TweenExtensions.Rewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE56(DG.Tweening.TweenExtensions.SmoothRewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE57(DG.Tweening.TweenExtensions.TogglePause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE58(DG.Tweening.TweenExtensions.GotoWaypoint)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE59(DG.Tweening.TweenExtensions.WaitForCompletion)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE60(DG.Tweening.TweenExtensions.WaitForRewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE61(DG.Tweening.TweenExtensions.WaitForKill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE62(DG.Tweening.TweenExtensions.WaitForElapsedLoops)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE63(DG.Tweening.TweenExtensions.WaitForPosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE64(DG.Tweening.TweenExtensions.WaitForStart)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE65(DG.Tweening.TweenExtensions.CompletedLoops)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE66(DG.Tweening.TweenExtensions.Delay)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE67(DG.Tweening.TweenExtensions.ElapsedDelay)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE68(DG.Tweening.TweenExtensions.Duration)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE69(DG.Tweening.TweenExtensions.Elapsed)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE70(DG.Tweening.TweenExtensions.ElapsedPercentage)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE71(DG.Tweening.TweenExtensions.ElapsedDirectionalPercentage)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE72(DG.Tweening.TweenExtensions.IsActive)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE73(DG.Tweening.TweenExtensions.IsBackwards)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE74(DG.Tweening.TweenExtensions.IsComplete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE75(DG.Tweening.TweenExtensions.IsInitialized)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE76(DG.Tweening.TweenExtensions.IsPlaying)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE77(DG.Tweening.TweenExtensions.Loops)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE78(DG.Tweening.TweenExtensions.PathGetPoint)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE79(DG.Tweening.TweenExtensions.PathGetDrawPoints)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE80(DG.Tweening.TweenExtensions.PathLength)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE81(DG.Tweening.TweenSettingsExtensions.SetAutoKill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE82(DG.Tweening.TweenSettingsExtensions.SetAutoKill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE83(DG.Tweening.TweenSettingsExtensions.SetId)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE84(DG.Tweening.TweenSettingsExtensions.SetId)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE85(DG.Tweening.TweenSettingsExtensions.SetId)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE86(DG.Tweening.TweenSettingsExtensions.SetLink)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE87(DG.Tweening.TweenSettingsExtensions.SetLink)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE88(DG.Tweening.TweenSettingsExtensions.SetTarget)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE89(DG.Tweening.TweenSettingsExtensions.SetLoops)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE90(DG.Tweening.TweenSettingsExtensions.SetLoops)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE91(DG.Tweening.TweenSettingsExtensions.SetEase)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE92(DG.Tweening.TweenSettingsExtensions.SetEase)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE93(DG.Tweening.TweenSettingsExtensions.SetEase)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE94(DG.Tweening.TweenSettingsExtensions.SetEase)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE95(DG.Tweening.TweenSettingsExtensions.SetEase)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE96(DG.Tweening.TweenSettingsExtensions.SetRecyclable)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE97(DG.Tweening.TweenSettingsExtensions.SetRecyclable)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE98(DG.Tweening.TweenSettingsExtensions.SetUpdate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE99(DG.Tweening.TweenSettingsExtensions.SetUpdate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE100(DG.Tweening.TweenSettingsExtensions.SetUpdate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE101(DG.Tweening.TweenSettingsExtensions.SetInverted)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE102(DG.Tweening.TweenSettingsExtensions.SetInverted)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE103(DG.Tweening.TweenSettingsExtensions.OnStart)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE104(DG.Tweening.TweenSettingsExtensions.OnPlay)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE105(DG.Tweening.TweenSettingsExtensions.OnPause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE106(DG.Tweening.TweenSettingsExtensions.OnRewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE107(DG.Tweening.TweenSettingsExtensions.OnUpdate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE108(DG.Tweening.TweenSettingsExtensions.OnStepComplete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE109(DG.Tweening.TweenSettingsExtensions.OnComplete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE110(DG.Tweening.TweenSettingsExtensions.OnKill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE111(DG.Tweening.TweenSettingsExtensions.OnWaypointChange)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE112(DG.Tweening.TweenSettingsExtensions.SetAs)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE113(DG.Tweening.TweenSettingsExtensions.SetAs)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE129(DG.Tweening.TweenSettingsExtensions.SetDelay)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE130(DG.Tweening.TweenSettingsExtensions.SetDelay)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE131(DG.Tweening.TweenSettingsExtensions.SetRelative)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE132(DG.Tweening.TweenSettingsExtensions.SetRelative)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE133(DG.Tweening.TweenSettingsExtensions.SetSpeedBased)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE134(DG.Tweening.TweenSettingsExtensions.SetSpeedBased)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE223(DG.Tweening.ShortcutExtensions.DOTimeScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Sequence), new List<MethodInfo>(){

                  new __GEN_DELEGATE114(DG.Tweening.TweenSettingsExtensions.Append)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE115(DG.Tweening.TweenSettingsExtensions.Prepend)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE116(DG.Tweening.TweenSettingsExtensions.Join)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE117(DG.Tweening.TweenSettingsExtensions.Insert)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE118(DG.Tweening.TweenSettingsExtensions.AppendInterval)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE119(DG.Tweening.TweenSettingsExtensions.PrependInterval)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE120(DG.Tweening.TweenSettingsExtensions.AppendCallback)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE121(DG.Tweening.TweenSettingsExtensions.PrependCallback)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE122(DG.Tweening.TweenSettingsExtensions.InsertCallback)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Tweener), new List<MethodInfo>(){

                  new __GEN_DELEGATE123(DG.Tweening.TweenSettingsExtensions.From)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE124(DG.Tweening.TweenSettingsExtensions.From)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE125(DG.Tweening.TweenSettingsExtensions.From)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Color, UnityEngine.Color, DG.Tweening.Plugins.Options.ColorOptions>), new List<MethodInfo>(){

                  new __GEN_DELEGATE126(DG.Tweening.TweenSettingsExtensions.From)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE143(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE305(DG.Tweening.TweenSettingsExtensions.From)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE313(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3, DG.Tweening.Plugins.Options.VectorOptions>), new List<MethodInfo>(){

                  new __GEN_DELEGATE127(DG.Tweening.TweenSettingsExtensions.From)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE138(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE139(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.CircleOptions>), new List<MethodInfo>(){

                  new __GEN_DELEGATE128(DG.Tweening.TweenSettingsExtensions.From)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE148(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE306(DG.Tweening.TweenSettingsExtensions.From)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE318(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Core.TweenerCore<float, float, DG.Tweening.Plugins.Options.FloatOptions>), new List<MethodInfo>(){

                  new __GEN_DELEGATE135(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE307(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector2, UnityEngine.Vector2, DG.Tweening.Plugins.Options.VectorOptions>), new List<MethodInfo>(){

                  new __GEN_DELEGATE136(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE137(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE308(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE309(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector4, UnityEngine.Vector4, DG.Tweening.Plugins.Options.VectorOptions>), new List<MethodInfo>(){

                  new __GEN_DELEGATE140(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE141(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE310(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE311(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Quaternion, UnityEngine.Vector3, DG.Tweening.Plugins.Options.QuaternionOptions>), new List<MethodInfo>(){

                  new __GEN_DELEGATE142(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE312(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Rect, UnityEngine.Rect, DG.Tweening.Plugins.Options.RectOptions>), new List<MethodInfo>(){

                  new __GEN_DELEGATE144(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE314(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Core.TweenerCore<string, string, DG.Tweening.Plugins.Options.StringOptions>), new List<MethodInfo>(){

                  new __GEN_DELEGATE145(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE315(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, UnityEngine.Vector3[], DG.Tweening.Plugins.Options.Vector3ArrayOptions>), new List<MethodInfo>(){

                  new __GEN_DELEGATE146(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE147(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE316(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE317(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(DG.Tweening.Core.TweenerCore<UnityEngine.Vector3, DG.Tweening.Plugins.Core.PathCore.Path, DG.Tweening.Plugins.Options.PathOptions>), new List<MethodInfo>(){

                  new __GEN_DELEGATE149(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE150(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE151(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE152(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE153(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE154(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE155(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE156(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE319(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE320(DG.Tweening.TweenSettingsExtensions.SetOptions)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE321(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE322(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE323(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE324(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE325(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE326(DG.Tweening.TweenSettingsExtensions.SetLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.Camera), new List<MethodInfo>(){

                  new __GEN_DELEGATE157(DG.Tweening.ShortcutExtensions.DOAspect)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE158(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE159(DG.Tweening.ShortcutExtensions.DOFarClipPlane)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE160(DG.Tweening.ShortcutExtensions.DOFieldOfView)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE161(DG.Tweening.ShortcutExtensions.DONearClipPlane)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE162(DG.Tweening.ShortcutExtensions.DOOrthoSize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE163(DG.Tweening.ShortcutExtensions.DOPixelRect)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE164(DG.Tweening.ShortcutExtensions.DORect)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE165(DG.Tweening.ShortcutExtensions.DOShakePosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE166(DG.Tweening.ShortcutExtensions.DOShakePosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE167(DG.Tweening.ShortcutExtensions.DOShakeRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE168(DG.Tweening.ShortcutExtensions.DOShakeRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE327(DG.Tweening.ShortcutExtensions.DOAspect)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE328(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE329(DG.Tweening.ShortcutExtensions.DOFarClipPlane)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE330(DG.Tweening.ShortcutExtensions.DOFieldOfView)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE331(DG.Tweening.ShortcutExtensions.DONearClipPlane)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE332(DG.Tweening.ShortcutExtensions.DOOrthoSize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE333(DG.Tweening.ShortcutExtensions.DOPixelRect)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE334(DG.Tweening.ShortcutExtensions.DORect)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE335(DG.Tweening.ShortcutExtensions.DOShakePosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE336(DG.Tweening.ShortcutExtensions.DOShakePosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE337(DG.Tweening.ShortcutExtensions.DOShakeRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE338(DG.Tweening.ShortcutExtensions.DOShakeRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.Light), new List<MethodInfo>(){

                  new __GEN_DELEGATE169(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE170(DG.Tweening.ShortcutExtensions.DOIntensity)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE171(DG.Tweening.ShortcutExtensions.DOShadowStrength)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE224(DG.Tweening.ShortcutExtensions.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE339(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE340(DG.Tweening.ShortcutExtensions.DOIntensity)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE341(DG.Tweening.ShortcutExtensions.DOShadowStrength)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE393(DG.Tweening.ShortcutExtensions.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.LineRenderer), new List<MethodInfo>(){

                  new __GEN_DELEGATE172(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE342(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.Material), new List<MethodInfo>(){

                  new __GEN_DELEGATE173(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE174(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE175(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE176(DG.Tweening.ShortcutExtensions.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE177(DG.Tweening.ShortcutExtensions.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE178(DG.Tweening.ShortcutExtensions.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE179(DG.Tweening.ShortcutExtensions.DOFloat)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE180(DG.Tweening.ShortcutExtensions.DOFloat)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE181(DG.Tweening.ShortcutExtensions.DOOffset)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE182(DG.Tweening.ShortcutExtensions.DOOffset)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE183(DG.Tweening.ShortcutExtensions.DOTiling)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE184(DG.Tweening.ShortcutExtensions.DOTiling)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE185(DG.Tweening.ShortcutExtensions.DOVector)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE186(DG.Tweening.ShortcutExtensions.DOVector)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE225(DG.Tweening.ShortcutExtensions.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE226(DG.Tweening.ShortcutExtensions.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE227(DG.Tweening.ShortcutExtensions.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE235(DG.Tweening.ShortcutExtensions.DOComplete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE237(DG.Tweening.ShortcutExtensions.DOKill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE239(DG.Tweening.ShortcutExtensions.DOFlip)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE241(DG.Tweening.ShortcutExtensions.DOGoto)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE243(DG.Tweening.ShortcutExtensions.DOPause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE245(DG.Tweening.ShortcutExtensions.DOPlay)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE247(DG.Tweening.ShortcutExtensions.DOPlayBackwards)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE249(DG.Tweening.ShortcutExtensions.DOPlayForward)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE251(DG.Tweening.ShortcutExtensions.DORestart)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE253(DG.Tweening.ShortcutExtensions.DORewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE255(DG.Tweening.ShortcutExtensions.DOSmoothRewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE257(DG.Tweening.ShortcutExtensions.DOTogglePause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE343(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE344(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE345(DG.Tweening.ShortcutExtensions.DOColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE346(DG.Tweening.ShortcutExtensions.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE347(DG.Tweening.ShortcutExtensions.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE348(DG.Tweening.ShortcutExtensions.DOFade)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE349(DG.Tweening.ShortcutExtensions.DOFloat)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE350(DG.Tweening.ShortcutExtensions.DOFloat)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE351(DG.Tweening.ShortcutExtensions.DOOffset)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE352(DG.Tweening.ShortcutExtensions.DOOffset)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE353(DG.Tweening.ShortcutExtensions.DOTiling)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE354(DG.Tweening.ShortcutExtensions.DOTiling)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE355(DG.Tweening.ShortcutExtensions.DOVector)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE356(DG.Tweening.ShortcutExtensions.DOVector)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE394(DG.Tweening.ShortcutExtensions.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE395(DG.Tweening.ShortcutExtensions.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE396(DG.Tweening.ShortcutExtensions.DOBlendableColor)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE404(DG.Tweening.ShortcutExtensions.DOComplete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE406(DG.Tweening.ShortcutExtensions.DOKill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE408(DG.Tweening.ShortcutExtensions.DOFlip)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE410(DG.Tweening.ShortcutExtensions.DOGoto)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE412(DG.Tweening.ShortcutExtensions.DOPause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE414(DG.Tweening.ShortcutExtensions.DOPlay)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE416(DG.Tweening.ShortcutExtensions.DOPlayBackwards)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE418(DG.Tweening.ShortcutExtensions.DOPlayForward)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE420(DG.Tweening.ShortcutExtensions.DORestart)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE422(DG.Tweening.ShortcutExtensions.DORewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE424(DG.Tweening.ShortcutExtensions.DOSmoothRewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE426(DG.Tweening.ShortcutExtensions.DOTogglePause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.TrailRenderer), new List<MethodInfo>(){

                  new __GEN_DELEGATE187(DG.Tweening.ShortcutExtensions.DOResize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE188(DG.Tweening.ShortcutExtensions.DOTime)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE357(DG.Tweening.ShortcutExtensions.DOResize)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE358(DG.Tweening.ShortcutExtensions.DOTime)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.Transform), new List<MethodInfo>(){

                  new __GEN_DELEGATE189(DG.Tweening.ShortcutExtensions.DOMove)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE190(DG.Tweening.ShortcutExtensions.DOMoveX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE191(DG.Tweening.ShortcutExtensions.DOMoveY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE192(DG.Tweening.ShortcutExtensions.DOMoveZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE193(DG.Tweening.ShortcutExtensions.DOLocalMove)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE194(DG.Tweening.ShortcutExtensions.DOLocalMoveX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE195(DG.Tweening.ShortcutExtensions.DOLocalMoveY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE196(DG.Tweening.ShortcutExtensions.DOLocalMoveZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE197(DG.Tweening.ShortcutExtensions.DORotate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE198(DG.Tweening.ShortcutExtensions.DORotateQuaternion)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE199(DG.Tweening.ShortcutExtensions.DOLocalRotate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE200(DG.Tweening.ShortcutExtensions.DOLocalRotateQuaternion)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE201(DG.Tweening.ShortcutExtensions.DOScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE202(DG.Tweening.ShortcutExtensions.DOScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE203(DG.Tweening.ShortcutExtensions.DOScaleX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE204(DG.Tweening.ShortcutExtensions.DOScaleY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE205(DG.Tweening.ShortcutExtensions.DOScaleZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE206(DG.Tweening.ShortcutExtensions.DOLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE207(DG.Tweening.ShortcutExtensions.DODynamicLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE208(DG.Tweening.ShortcutExtensions.DOPunchPosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE209(DG.Tweening.ShortcutExtensions.DOPunchScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE210(DG.Tweening.ShortcutExtensions.DOPunchRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE211(DG.Tweening.ShortcutExtensions.DOShakePosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE212(DG.Tweening.ShortcutExtensions.DOShakePosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE213(DG.Tweening.ShortcutExtensions.DOShakeRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE214(DG.Tweening.ShortcutExtensions.DOShakeRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE215(DG.Tweening.ShortcutExtensions.DOShakeScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE216(DG.Tweening.ShortcutExtensions.DOShakeScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE217(DG.Tweening.ShortcutExtensions.DOJump)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE218(DG.Tweening.ShortcutExtensions.DOLocalJump)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE219(DG.Tweening.ShortcutExtensions.DOPath)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE220(DG.Tweening.ShortcutExtensions.DOLocalPath)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE221(DG.Tweening.ShortcutExtensions.DOPath)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE222(DG.Tweening.ShortcutExtensions.DOLocalPath)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE228(DG.Tweening.ShortcutExtensions.DOBlendableMoveBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE229(DG.Tweening.ShortcutExtensions.DOBlendableLocalMoveBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE230(DG.Tweening.ShortcutExtensions.DOBlendableRotateBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE231(DG.Tweening.ShortcutExtensions.DOBlendableLocalRotateBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE232(DG.Tweening.ShortcutExtensions.DOBlendablePunchRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE233(DG.Tweening.ShortcutExtensions.DOBlendableScaleBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE359(DG.Tweening.ShortcutExtensions.DOMove)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE360(DG.Tweening.ShortcutExtensions.DOMoveX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE361(DG.Tweening.ShortcutExtensions.DOMoveY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE362(DG.Tweening.ShortcutExtensions.DOMoveZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE363(DG.Tweening.ShortcutExtensions.DOLocalMove)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE364(DG.Tweening.ShortcutExtensions.DOLocalMoveX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE365(DG.Tweening.ShortcutExtensions.DOLocalMoveY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE366(DG.Tweening.ShortcutExtensions.DOLocalMoveZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE367(DG.Tweening.ShortcutExtensions.DORotate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE368(DG.Tweening.ShortcutExtensions.DORotateQuaternion)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE369(DG.Tweening.ShortcutExtensions.DOLocalRotate)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE370(DG.Tweening.ShortcutExtensions.DOLocalRotateQuaternion)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE371(DG.Tweening.ShortcutExtensions.DOScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE372(DG.Tweening.ShortcutExtensions.DOScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE373(DG.Tweening.ShortcutExtensions.DOScaleX)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE374(DG.Tweening.ShortcutExtensions.DOScaleY)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE375(DG.Tweening.ShortcutExtensions.DOScaleZ)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE376(DG.Tweening.ShortcutExtensions.DOLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE377(DG.Tweening.ShortcutExtensions.DODynamicLookAt)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE378(DG.Tweening.ShortcutExtensions.DOPunchPosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE379(DG.Tweening.ShortcutExtensions.DOPunchScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE380(DG.Tweening.ShortcutExtensions.DOPunchRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE381(DG.Tweening.ShortcutExtensions.DOShakePosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE382(DG.Tweening.ShortcutExtensions.DOShakePosition)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE383(DG.Tweening.ShortcutExtensions.DOShakeRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE384(DG.Tweening.ShortcutExtensions.DOShakeRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE385(DG.Tweening.ShortcutExtensions.DOShakeScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE386(DG.Tweening.ShortcutExtensions.DOShakeScale)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE387(DG.Tweening.ShortcutExtensions.DOJump)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE388(DG.Tweening.ShortcutExtensions.DOLocalJump)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE389(DG.Tweening.ShortcutExtensions.DOPath)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE390(DG.Tweening.ShortcutExtensions.DOLocalPath)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE391(DG.Tweening.ShortcutExtensions.DOPath)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE392(DG.Tweening.ShortcutExtensions.DOLocalPath)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE397(DG.Tweening.ShortcutExtensions.DOBlendableMoveBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE398(DG.Tweening.ShortcutExtensions.DOBlendableLocalMoveBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE399(DG.Tweening.ShortcutExtensions.DOBlendableRotateBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE400(DG.Tweening.ShortcutExtensions.DOBlendableLocalRotateBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE401(DG.Tweening.ShortcutExtensions.DOBlendablePunchRotation)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE402(DG.Tweening.ShortcutExtensions.DOBlendableScaleBy)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.Component), new List<MethodInfo>(){

                  new __GEN_DELEGATE234(DG.Tweening.ShortcutExtensions.DOComplete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE236(DG.Tweening.ShortcutExtensions.DOKill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE238(DG.Tweening.ShortcutExtensions.DOFlip)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE240(DG.Tweening.ShortcutExtensions.DOGoto)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE242(DG.Tweening.ShortcutExtensions.DOPause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE244(DG.Tweening.ShortcutExtensions.DOPlay)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE246(DG.Tweening.ShortcutExtensions.DOPlayBackwards)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE248(DG.Tweening.ShortcutExtensions.DOPlayForward)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE250(DG.Tweening.ShortcutExtensions.DORestart)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE252(DG.Tweening.ShortcutExtensions.DORewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE254(DG.Tweening.ShortcutExtensions.DOSmoothRewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE256(DG.Tweening.ShortcutExtensions.DOTogglePause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE403(DG.Tweening.ShortcutExtensions.DOComplete)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE405(DG.Tweening.ShortcutExtensions.DOKill)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE407(DG.Tweening.ShortcutExtensions.DOFlip)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE409(DG.Tweening.ShortcutExtensions.DOGoto)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE411(DG.Tweening.ShortcutExtensions.DOPause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE413(DG.Tweening.ShortcutExtensions.DOPlay)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE415(DG.Tweening.ShortcutExtensions.DOPlayBackwards)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE417(DG.Tweening.ShortcutExtensions.DOPlayForward)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE419(DG.Tweening.ShortcutExtensions.DORestart)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE421(DG.Tweening.ShortcutExtensions.DORewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE423(DG.Tweening.ShortcutExtensions.DOSmoothRewind)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				  new __GEN_DELEGATE425(DG.Tweening.ShortcutExtensions.DOTogglePause)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.Texture2D), new List<MethodInfo>(){

                  new __GEN_DELEGATE259(GameObjectExt.CreateSprite)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

                {typeof(UnityEngine.Animator), new List<MethodInfo>(){

                  new __GEN_DELEGATE262(GameObjectExt.AnimationAtTag)
#if UNITY_WSA && !UNITY_EDITOR
                                      .GetMethodInfo(),
#else
                                      .Method,
#endif
				
				}},

            };

            genTryArrayGetPtr = StaticLuaCallbacks.__tryArrayGet;
            genTryArraySetPtr = StaticLuaCallbacks.__tryArraySet;
        }
    }
}
