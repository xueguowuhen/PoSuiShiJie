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
    private Action onRewardBack;
    private Action onDailyBack;
    public GameObject RewardDownLoad;
    public string RewardDownLoadUrl;
    public Image RewardDownLoadImg;
    public GameObject TaskDownLoad;
    public string TaskDownLoadUrl;
    public Image TaskDownLoadImg;
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
        RewardDownLoadUrl = PathDefine.ResRewardTaskItem;
        loaderSvc.LoadPrefab(PathDefine.ResItem, RewardDownLoadUrl, (GameObject obj) =>
       {
           GameObject gameObject = obj;
           Instantiate(obj);
           RewardPool = GameObjectPoolManager.Instance.CreatePrefabPool(gameObject);
           RewardPool.MaxCount = 15;//设置最大缓存数量
           RewardPool.cullMaxPerPass = 5;
           RewardPool.cullAbove = 15;
           RewardPool.cullDespawned = true;
           RewardPool.cullDelay = 2;
           RewardDownLoadUrl = null;
           RewardPool.Init();
           if (onRewardBack!= null)
           {
               onRewardBack();
           }
       }, cache: true, instan: false);
    }
    /// <summary>
    /// 初始化人物池
    /// </summary>
    private void InitDailyPool()
    {
        TaskDownLoadUrl = PathDefine.ResDailyTaskItem;
        loaderSvc.LoadPrefab(PathDefine.ResItem, TaskDownLoadUrl, (GameObject obj) =>
       {
           GameObject objInst = obj;
           Instantiate(obj);
           DailyPool = GameObjectPoolManager.Instance.CreatePrefabPool(objInst);
           DailyPool.MaxCount = 15;//设置最大缓存数量
           DailyPool.cullMaxPerPass = 5;
           DailyPool.cullAbove = 15;
           DailyPool.cullDespawned = true;
           DailyPool.cullDelay = 2;
           TaskDownLoadUrl =null;
           DailyPool.Init();
           if (onDailyBack!= null)
           {
               onDailyBack();
           }
       }, cache: true, instan: false);
    }
    #endregion

    #region 加载活跃界面
    private void LoadRewardTask()
    {
        SetActive(RewardDownLoad, false);
        ClearDailyTask(RewardSlotsContent, RewardPool);
        PlayerData playerData = GameRoot.Instance.PlayerData;
        //TODO:加载活跃界面
        int count = resSvc.GetTaskRewardCount();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = RewardPool.GetObject();
            // 延迟一帧设置对象的 Transform
            obj.transform.SetParent(RewardSlotsContent.transform, false);
            //StartCoroutine(SetTransformNextFrame(obj));
            //obj.transform.localPosition = Vector3.zero;
            //obj.transform.localScale = Vector3.one;
            obj.GetComponent<RewardTaskItem>().SetUI(resSvc.GetTaskRewardCfgListData(i), TaskActive, playerData.rewardTask.TaskProgress[i]);
        }
    }
    #endregion
    IEnumerator SetTransformNextFrame(GameObject obj)
    {
        yield return null;  // 延迟一帧
        obj.transform.localPosition = Vector3.zero;
        obj.transform.localScale = Vector3.one;
    }
    #region 加载任务界面
    private void LoadDailyTask()
    {
        SetActive(TaskDownLoad, false);
        ClearDailyTask(TaskSlotsContent, DailyPool);
        PlayerData playerData = GameRoot.Instance.PlayerData;
        int count = resSvc.GetTaskDailyCount();
        for (int i = 0; i < count; i++)
        {
            GameObject obj = DailyPool.GetObject();
            obj.transform.SetParent(TaskSlotsContent.transform, false);
            //StartCoroutine(SetTransformNextFrame(obj));
            //obj.transform.localPosition = Vector3.zero;
            //obj.transform.localScale = Vector3.one;
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
        if (RewardPool == null)
        {

            SetActive(RewardDownLoad, true);
            onRewardBack = LoadRewardTask;
        }
        else
        {

            LoadRewardTask();
        }
        if (DailyPool == null)
        {
            SetActive(TaskDownLoad, true);
            onDailyBack = LoadDailyTask;
        }
        else
        {
            LoadDailyTask();
        }
    }
    #endregion
    #region 对象回收
    /// <summary>
    /// 清空面板
    /// </summary>
    private void ClearDailyTask(GameObject Content, GameObjectPool pool)
    {
        if (pool == null) return;
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
    protected override void ClearWnd()
    {
        base.ClearWnd();
        onRewardBack=null;
        onDailyBack=null;
    }
    // Update is called once per frame
    void Update()
    {
        if (RewardDownLoadUrl != null)
        {

            float progress = DowningSys.instance.GetDownUrlProgress(RewardDownLoadUrl);
            RewardDownLoadImg.fillAmount = progress;
        }
        if (TaskDownLoadUrl != null)
        {

            float progress = DowningSys.instance.GetDownUrlProgress(TaskDownLoadUrl);
            TaskDownLoadImg.fillAmount = progress;
        }
    }
}
