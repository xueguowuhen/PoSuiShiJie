/****************************************************
    文件：DailyTaskSys
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-13 18:13:11
	功能：Nothing
*****************************************************/
using CommonNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CfgSvc;


public class DailyTaskSys
{
    private static DailyTaskSys instance = null;
    public static DailyTaskSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DailyTaskSys();
            }
            return instance;
        }
    }
    private CacheSvc cacheSvc;
    private CfgSvc cfgSvc;
    public void Init()
    {
        cacheSvc = CacheSvc.Instance;
        cfgSvc = CfgSvc.Instance;
        GameCommon.Log("DailyTaskSys Init Done");
    }
    /// <summary>
    /// 奖励领取
    /// </summary>
    /// <param name="pack"></param>
    public void ReqRewardTask(MsgPack pack)
    {
        ReqRewardTask reqRewardTask = pack.gameMsg.reqRewardTask;
        int taskId = reqRewardTask.RewardTaskID;
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.RspRewardTask
        };
        //本日任务已刷新
        if (UpdateRewardTask(playerData))
        {
            gameMsg.err = (int)Error.RewardTaskRefreshError;
            pack.session.SendMsg(gameMsg);
            return;
        }
        int TaskActive = GetTotalTaskActive(playerData);
        int Active = cfgSvc.GetTaskRewardActive(taskId);
        bool IsReward = TaskActive >= Active;
        if (IsReward)//判断活跃度是否满足领取条件
        {
            //活跃度满足领取条件，领取奖励
            int rewardIndex = cfgSvc.GetTaskRewardCfgIndex(taskId);
            bool IsFinish = playerData.rewardTask.TaskProgress[rewardIndex] == 1 ? true : false;
            if (IsFinish)
            {
                //奖励已领取
                gameMsg.err = (int)Error.RewardTaskError;
                pack.session.SendMsg(gameMsg);
                return;
            }
            //奖励未领取
            playerData.rewardTask.TaskProgress[rewardIndex] = 1;
            TaskRewardCfg taskRewardCfg = cfgSvc.GetTaskRewardCfgListData(taskId);
            //给予奖励
            foreach (var item in taskRewardCfg.rewardItems)
            {
                ShopSys.Instance.AddOrUpdateItemInBag(playerData, item.ItemID, item.Count);
            }
            if (!cacheSvc.UpdatePlayerData(playerData))
            {
                gameMsg.err = (int)Error.RewardActiveError;
                pack.session.SendMsg(gameMsg);
                return;
            }
            gameMsg.rspRewardTask = new RspRewardTask()
            {
                rewardTask = playerData.rewardTask,
                Bag = playerData.Bag,
            };
        }
        else //不满足则返回
        {
            gameMsg.err = (int)Error.RewardTaskError;
        }
        pack.session.SendMsg(gameMsg);
    }

    public bool UpdateRewardTask(PlayerData playerData)
    {
        //获取任务数据
        // 获取当前时间
        DateTime currentTime = DateTime.Now;
        if (playerData.rewardTask == null)
        {
            return false;
        }
        // 获取上次任务更新时间
        DateTime lastTime = playerData.rewardTask.LastTime;
        // 判断是否间隔了一天
        if ((currentTime.Date - lastTime.Date).TotalDays >= 1)
        {
            // 在这里处理任务更新，比如重置任务或者给与奖励
            // 重置任务
            InitPlayerDailyTask(playerData);
            cacheSvc.UpdatePlayerData(playerData);
            return true;
        }
        return false;
    }
    /// <summary>
    /// 获取任务活跃度
    /// </summary>
    /// <param name="taskId"></param>
    /// <returns></returns>
    private int GetTotalTaskActive(PlayerData playerData)
    {
        //获取所有完成的任务列表
        List<DailyTask> dailyTaskList = playerData.dailyTasks.Where(t => t.TaskFinish == true).ToList();
        int TaskActive = 0;
        //遍历任务列表，刷新任务进度条
        foreach (var task in dailyTaskList)
        {
            //根据ID获取活跃度
            TaskActive += cfgSvc.GettTaskDailyActive(task.TaskID);
        }
        return TaskActive;
    }
    /// <summary>
    /// 初始化玩家每日任务
    /// </summary>
    /// <param name="playerData"></param>
    private void InitPlayerDailyTask(PlayerData playerData)
    {
        playerData.rewardTask.LastTime = DateTime.Now;
        playerData.dailyTasks = cfgSvc.GetTaskDailyCfgData();
        playerData.rewardTask.TaskProgress = new List<int>(new int[cfgSvc.GetTaskRewardCount()]);//任务进度初始化
    }
    public void ReqDailyTask(MsgPack pack)
    {
        ReqDailyTask reqDailyTask = pack.gameMsg.reqDailyTask;

        int taskId = reqDailyTask.TaskID;
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.RspDailyTask
        };
        //本日任务已刷新
        if (UpdateRewardTask(playerData))
        {
            gameMsg.err = (int)Error.RewardTaskRefreshError;
            pack.session.SendMsg(gameMsg);
            return;
        }
        DailyTask dailyTask = playerData.dailyTasks.FirstOrDefault(t => t.TaskID == taskId);
        if (dailyTask != null)
        {
            if (dailyTask.TaskFinish)//任务已完成
            {
                gameMsg.err = (int)Error.DailyTaskNotError;
                pack.session.SendMsg(gameMsg);
                return;
            }
            //任务未完成
            int TaskReward = dailyTask.TaskReward;
            TaskDailyCfg taskDailyCfg = cfgSvc.GetTaskDailyCfgData(taskId);
            bool IsFinish = TaskReward >= taskDailyCfg.Count ? true : false;
            if (!IsFinish)//不允许领取奖励
            {
                gameMsg.err = (int)Error.DailyTaskFinishError;
                pack.session.SendMsg(gameMsg);
                return;

            }
            //奖励领取
            dailyTask.TaskFinish = true;
            if (!cacheSvc.UpdatePlayerData(playerData))
            {
                gameMsg.err = (int)Error.DailyTaskRewardError;
                pack.session.SendMsg(gameMsg);
                return;
            }
            gameMsg.rspDailyTask = new RspDailyTask()
            {
                dailyTasks = playerData.dailyTasks,
            };
        }
        else
        {
            gameMsg.err = (int)Error.DailyTaskNotError;
        }
        pack.session.SendMsg(gameMsg);
    }
    public ref int GetPlayerDailyTask(DailyTaskType dailyTaskType, PlayerData playerData)
    {
        switch (dailyTaskType)
        {
            case DailyTaskType.aura:
                return ref playerData.dailyTasks[0].TaskReward;
            case DailyTaskType.ruvia:
                return ref playerData.dailyTasks[1].TaskReward;
            case DailyTaskType.crystal:
                return ref playerData.dailyTasks[2].TaskReward;
            case DailyTaskType.material:
                return ref playerData.dailyTasks[3].TaskReward;
            case DailyTaskType.equip:
                return ref playerData.dailyTasks[4].TaskReward;
            case DailyTaskType.consume:
                return ref playerData.dailyTasks[5].TaskReward;
        }
        return ref playerData.dailyTasks[0].TaskReward;
    }
}

