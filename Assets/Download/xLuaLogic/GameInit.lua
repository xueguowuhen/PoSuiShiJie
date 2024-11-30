---
--- Generated by EmmyLua(https://github.com/EmmyLua)
--- Created by lzh.
--- DateTime: 2024-11-12 19:48
---
print("启动GameInit.lua")
-- 加载全局配置
--require可以引入外部的脚本或模块，并在当前脚本中使用这些模块的功能或变量。
require("Download/xLuaLogic/CtrMgr")
require("Download/xLuaLogic/Data/DBModelMgr")

GameInit = {}
local this = GameInit

function GameInit.InitView()
        require("Download/xLuaLogic/Modules/UIRoot/UIRootView");
        require("Download/xLuaLogic/Modules/Task/TaskView");
end

function GameInit.Init()
   this.InitView()
    CtrlMgr.Init()
    DBModelMgr.Init()
  -- GameInit.LoadView(CtrlName.UIRootCtrl)
end
function GameInit.LoadView(type)
    local ctrl=CtrlMgr.GetCtrl(type)
    if  ctrl ~=nil then
        ctrl.Awake()
    end
end