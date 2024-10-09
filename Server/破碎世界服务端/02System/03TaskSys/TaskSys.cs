/****************************************************
    文件：TaskSys
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-29 21:09:35
	功能：Nothing
*****************************************************/
using CommonNet;
using static CfgSvc;

public class TaskSys
{
    private static TaskSys instance = null;
    public static TaskSys Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TaskSys();
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
        GameCommon.Log("TaskSys Init Done");
    }
    public void ReqTask(MsgPack pack)
    {
        ReqTask reqTask = pack.gameMsg.reqTask;
        GameMsg msg = new GameMsg
        {
            cmd = (int)CMD.RspTask
        };
        PlayerData playerData = cacheSvc.GetPlayerDataBySession(pack.session);
        if(playerData.Taskid ==reqTask.Taskid)
        {
            TaskCfg taskCfg = cfgSvc.GetTaskCfgData(reqTask.Taskid);
            playerData.aura += taskCfg.aura;
            playerData.exp += taskCfg.exp;//后续需要做升级处理
            playerData.Taskid += 1;
            if (!cacheSvc.UpdatePlayerData(playerData))
            {
                msg.err = (int)Error.PerSonError;
            }
            else
            {
                msg.rspTask = new RspTask//更新数据
                {
                    aura = playerData.aura,
                    exp = playerData.exp,
                    lv = playerData.level,
                    Taskid = playerData.Taskid,
                };

            }
        }
        else
        {
            msg.err =(int)Error.TaskIDError;
        }
        pack.session.SendMsg(msg);
    }
}