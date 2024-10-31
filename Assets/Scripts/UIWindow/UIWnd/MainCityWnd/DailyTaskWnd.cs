/****************************************************
    文件：DailyTaskWnd.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/13 18:49:14
    功能：Nothing
*****************************************************/
using CommonNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DailyTaskWnd : WindowRoot
{
    public Image DailyTaskSliderFg;
    public Text DailyTaskSliderText;
    public GameObject RewardSlotsContent;
    public GameObject TaskSlotsContent;
    public Button CloseBtn;
    private GameObjectPool RewardPool;
    private GameObjectPool DailyPool;
    // Start is called before the first frame update
    private float TaskActive;

    protected override void InitWnd()
    {
        base.InitWnd();
        RefreshTask();
    }


    protected override void SetGameObject()
    {
        base.SetGameObject();
        InitRewardPool();
        InitDailyPool();
        AddListener(CloseBtn, ClickClose);
    }
    #region 初始化对象池
    /// <summary>
    /// 初始化人物池
    /// </summary>
    private void InitRewardPool()
    {
        GameObject gameObject = resSvc.LoadPrefab(PathDefine.ResRewardTaskItem, cache: true, instan: false);
        RewardPool = GameObjectPoolManager.Instance.CreatePrefabPool(gameObject);
        RewardPool.MaxCount = 15;//设置最大缓存数量
        RewardPool.cullMaxPerPass = 5;
        RewardPool.cullAbove = 15;
        RewardPool.cullDespawned = true;
        RewardPool.cullDelay = 2;
        RewardPool.Init();
    }
    /// <summary>
    /// 初始化人物池
    /// </summary>
    private void InitDailyPool()
    {
        GameObject gameObject = resSvc.LoadPrefab(PathDefine.ResDailyTaskItem, cache: true, instan: false);
        DailyPool = GameObjectPoolManager.Instance.CreatePrefabPool(gameObject);
        DailyPool.MaxCount = 15;//设置最大缓存数量
        DailyPool.cullMaxPerPass = 5;
        DailyPool.cullAbove = 15;
        DailyPool.cullDespawned = true;
        DailyPool.cullDelay = 2;
        DailyPool.Init();
    }
    #endregion

    #region 加载活跃界面
    private void LoadRewardTask()
    {
        ClearDailyTask(RewardSlotsContent, RewardPool);
        PlayerData playerData = GameRoot.Instance.PlayerData;
        //TODO:加载活跃界面
        int count = resSvc.GetTaskRewardCount();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = RewardPool.GetObject();
            obj.transform.SetParent(RewardSlotsContent.transform, false);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<RewardTaskItem>().SetUI(resSvc.GetTaskRewardCfgListData(i), TaskActive, playerData.rewardTask.TaskProgress[i]);
        }
    }
    #endregion
    #region 加载任务界面
    private void LoadDailyTask()
    {
        ClearDailyTask(TaskSlotsContent, DailyPool);
        PlayerData playerData = GameRoot.Instance.PlayerData;
        int count = resSvc.GetTaskDailyCount();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = DailyPool.GetObject();
            obj.transform.SetParent(TaskSlotsContent.transform, false);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<DailyTaskItem>().SetUI(resSvc.GetTaskDailyCfgListData(i), TaskActive, playerData);
        }
    }
    private void RefreshDailyTask()
    {
        PlayerData playerData = GameRoot.Instance.PlayerData;
        //获取所有完成的任务列表
        List<DailyTask> dailyTaskList = playerData.dailyTasks.Where(t => t.TaskFinish == true).ToList();
        TaskActive = 0;
        //遍历任务列表，刷新任务进度条
        foreach (var task in dailyTaskList)
        {
            //根据ID获取活跃度
            TaskActive += resSvc.GettTaskDailyActive(task.TaskID);
        }
        //刷新进度条
        DailyTaskSliderText.SetText(string.Format("{0}/{1}", TaskActive, 100));
        float value = (float)TaskActive / 100;
        DailyTaskSliderFg.fillAmount = value;
    }
    public void RefreshTask()
    {
        RefreshDailyTask();
        LoadRewardTask();
        LoadDailyTask();
    }
    #endregion
    #region 对象回收
    /// <summary>
    /// 清空面板
    /// </summary>
    private void ClearDailyTask(GameObject Content, GameObjectPool pool)
    {
        if (Content != null)  //清空当前的商店物品
        {
            for (int i = Content.transform.childCount - 1; i >= 0; i--)
            {
                pool.ReturnObject(Content.transform.GetChild(i).gameObject);
            }
        }
    }
    #endregion
    private void ClickClose()
    {
        SetWndState(false);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
