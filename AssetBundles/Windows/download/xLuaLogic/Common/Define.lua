---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lzh.
--- DateTime: 2024-11-12 19:39
---
print("启动了Define.lua")

CtrlName={

    RaffleCtrl = "RaffleCtrl",--抽奖内容控制器
}
RaffleBuildMax=10--抽奖最大数量
--CS是一种命名空间，UnityEngine是Unity的命名空间，允许直接访问Unity的接口
www=CS.UnityEngine.WWW;--WWW类
GameObject=CS.UnityEngine.GameObject;
Object=CS.UnityEngine.Object ;
Color=CS.UnityEngine.Color;
Vector3=CS.UnityEngine.Vector3;
luaMgr = CS.LuaMgr.Instance;
LuaHelper = CS.LuaHelper.Instance;
CMD=CS.CommonNet.CMD;