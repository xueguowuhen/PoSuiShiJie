/****************************************************
    文件：DailyTaskItem.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/13 18:51:24
    功能：Nothing
*****************************************************/
using CommonNet;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DailyTaskItem : WindowItem
{
    public Text Title;
    public Text mtask;
    public Text TaskInfoText;
    public Image TaskItemFg;
    public Button ToBtn;
    public Text TaskToText;
    public GameObject ToBtnObj;
    public GameObject Claimed;
    private bool IsDailyTask = false;
    private int IsDailyTaskID;
    // Start is called before the first frame update
    /// <summary>
    /// 设置UI
    /// </summary>
    /// <param name="taskDailyCfg"></param>
    /// <param name="TaskActive"></param>
    /// <param name="playerData"></param>
    public void SetUI(TaskDailyCfg taskDailyCfg,float TaskActive, PlayerData playerData)
    {
        Title.SetText(taskDailyCfg.mTitle);
        mtask.SetText(taskDailyCfg.mTask);
        TaskInfoText.SetText(taskDailyCfg.Active);
        IsDailyTaskID = taskDailyCfg.ID;
        //获取任务数据
        DailyTask dailyTask = playerData.dailyTasks.FirstOrDefault(t => t.TaskID == IsDailyTaskID);
        TaskItemFg.SetImageFillAmount(dailyTask.TaskReward / (float)taskDailyCfg.Count);
        bool isSend = dailyTask.TaskReward >= taskDailyCfg.Count;

        BtnState(true);
        if (dailyTask.TaskFinish)
        {
            TaskToText.SetText("已领取");
            BtnState(false);
        }
        else
        {
            if (isSend)
            {
                TaskToText.SetText("请领取");
                AddListener(ToBtn, ClickReceive);
            }
            else
            {
                TaskToText.SetText("前往");
                AddListener(ToBtn, ClickTo);
            }
        }
        if(TaskActive >= 100)
        {
            ToBtnObj.SetActive(false);
            Claimed.SetActive(false);
        }
    }
    private void BtnState(bool state)
    {
        ToBtnObj.SetActive(state);
        Claimed.SetActive(!state);
    }
    /// <summary>
    /// 点击前往按钮
    /// </summary>
    private void ClickTo()
    {

    }
    /// <summary>
    /// 点击领取按钮
    /// </summary>
    private void ClickReceive()
    {
        if (IsDailyTask)
        {
            GameRoot.AddTips("请稍等，操作频率过快。");
            return;
        }
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.ReqDailyTask,
            reqDailyTask = new ReqDailyTask()
            {
                TaskID = IsDailyTaskID
            }
        };
        NetSvc.instance.SendMsg(gameMsg);
        IsDailyTask = true;
        TimerSvc.Instance.AddTimeTask((tid) =>
        {
            IsDailyTask = false;
        }, 2f);
    }
}
