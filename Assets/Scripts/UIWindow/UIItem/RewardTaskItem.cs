/****************************************************
    文件：RewardTaskItem.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/13 18:51:14
    功能：Nothing
*****************************************************/
using CommonNet;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardTaskItem : WindowItem
{
    public Text RewardText;
    public Image RewardIcon;
    public Text rewardText;
    public Image rewardIcon2;
    public Text rewardText2;
    private Button rewardBtn;//领取奖励按钮
    private bool isTask = false;
    private int taskRewardID;
    /// <summary>
    /// 设置UI
    /// </summary>
    /// <param name="taskRewardCfg"></param>
    /// <param name="taskActive"></param>
    /// <param name="taskProgress"></param>
    public void SetUI(TaskRewardCfg taskRewardCfg, float taskActive, int taskProgress)
    {
        bool IsTask = taskProgress == 1 ? true : false;
        rewardBtn = GetComponent<Button>();
        taskRewardID = taskRewardCfg.ID;
        RewardText.SetText(taskRewardCfg.Value, true);//活跃度是满足
        bool isShowBtn = taskActive >= taskRewardCfg.Value;
        rewardBtn.interactable = isShowBtn;
        if (IsTask)
        {
            rewardBtn.interactable = false;
            RewardText.SetText("已领取", true);//活跃度是满足
        }
        if (taskRewardCfg != null && taskRewardCfg.rewardItems.Count > 1)
        {
            ItemCfg itemCfg = ResSvc.instance.GetShopItemCfg(taskRewardCfg.rewardItems[0].ItemID);
            ComTools.GetItemSprite(itemCfg.type, itemCfg.mImg, (Texture2D texture) =>
           {
               RewardIcon.overrideSprite = texture.CreateSprite();
           });
            rewardText.SetText(taskRewardCfg.rewardItems[0].Count.ToString(), true);
            ItemCfg item2Cfg = ResSvc.instance.GetShopItemCfg(taskRewardCfg.rewardItems[1].ItemID);
            ComTools.GetItemSprite(item2Cfg.type, item2Cfg.mImg, (Texture2D texture) =>
           {
               rewardIcon2.overrideSprite = texture.CreateSprite();
           });
            rewardText2.SetText(taskRewardCfg.rewardItems[1].Count.ToString(), true);
            AddListener(rewardBtn, OnBtnClick);
        }
    }
    private void OnBtnClick()
    {
        if (isTask)
        {
            GameRoot.AddTips("请稍等，操作频率过快。");
            return;
        }
        GameMsg gameMsg = new GameMsg()
        {
            cmd = (int)CMD.ReqRewardTask,
            reqRewardTask = new ReqRewardTask()
            {
                RewardTaskID = taskRewardID
            }
        };
        NetSvc.instance.SendMsg(gameMsg);
        isTask = true;
        TimerSvc.Instance.AddTimeTask((tid) =>
        {
            isTask = false;
        }, 2f);
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
