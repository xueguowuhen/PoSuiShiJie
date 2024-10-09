/****************************************************
    文件：Timer
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-02 11:57:20
	功能：计时器
*****************************************************/
using System.Timers;
using Timer = System.Timers.Timer;

public class ShowTimer
{
    private Action<string>? taskLog;
    private Action<Action<int>, int> TaskHandle;
    private static readonly string locktid = "lock";
    private DateTime startDateTime = new DateTime(1970, 1, 1, 0, 0, 0);
    private double nowTime;
    private Timer srvTimer;
    private int tid;
    private int frameCounter;
    private static readonly string lockTime = "lockTime";
    private static readonly string lockFrame = "lockFrame";
    private List<int> tidLst = new List<int>();
    private List<int> recTidLst = new List<int>();

    private List<TimerTask> tasktimers = new List<TimerTask>();
    private List<TimerTask> tmptimers = new List<TimerTask>();
    private List<int> tmpDelTimes= new List<int>();

    private List<FrameTask> taskframes = new List<FrameTask>();
    private List<FrameTask> tmpframes = new List<FrameTask>();
    private List<int> tmpDelFrame=new List<int>();
    public ShowTimer(int interval = 0)//若间隔不为0，则创建一个定时间隔器
    {
        tidLst.Clear();
        recTidLst.Clear();
        tmptimers.Clear();
        tasktimers.Clear();
        taskframes.Clear();
        tmpframes.Clear();
        if (interval != 0)
        {
            srvTimer = new Timer(interval)
            {
                AutoReset = true
            };
            srvTimer.Elapsed += (object sender, ElapsedEventArgs args) =>
            {
                Update();
            };
            srvTimer.Start();
        }

    }
    public void Update()
    {
        CheckTimeTask();
        CheckFrameTask();
        DeleteTimeTask();
        DeleteFrameTask();
        if (recTidLst.Count > 0)
        {
            lock (locktid)
            {
                RecycleTid();
            }

        }
    }
    private void CheckTimeTask()
    {
        if (tmptimers.Count > 0)
        {
            lock (lockTime)
            {
                //加入缓存区中的定时任务,使用缓存添加可以避免主列表在遍历时被进行修改，修改会导致迭代器失效
                for (int tmpIndex = 0; tmpIndex < tmptimers.Count; tmpIndex++)
                {
                    tasktimers.Add(tmptimers[tmpIndex]);
                }
                tmptimers.Clear();
            }
        }
        nowTime = GetUTCMilliseconds();
        for (int index = 0; index < tasktimers.Count; index++)
        {
            TimerTask task = tasktimers[index];
            if (nowTime.CompareTo(task.destTime) < 0)//检测当前时间是否小于延迟时间
            {
                continue;
            }
            else
            {
                Action<int> ab = task.callback;
                try
                {
                    if (TaskHandle != null)
                    {
                        TaskHandle(ab, task.tid);
                    }
                    else
                    {
                        if (ab != null)
                        {
                            ab(task.tid);
                        }
                    }
                }
                catch (Exception e)
                {
                    LogInfo(e.ToString());
                }
                //移出已经完成的任务
                if (task.count == 1)
                {
                    tasktimers.RemoveAt(index);
                    index--;
                    recTidLst.Add(index);//然后移出对应的ID
                }
                else
                {
                    if (task.count != 0)
                    {
                        task.count -= 1;
                    }
                    task.destTime += task.delay;
                }
            }
        }
    }
    private void CheckFrameTask()
    {
        if (tmpframes.Count > 0)
        {
            lock (lockFrame)
            {
                //加入缓存区中的定时任务,使用缓存添加可以避免主列表在遍历时被进行修改，修改会导致迭代器失效
                for (int tmpIndex = 0; tmpIndex < tmpframes.Count; tmpIndex++)
                {
                    taskframes.Add(tmpframes[tmpIndex]);
                }
                tmpframes.Clear();
            }
        }
        frameCounter += 1;
        for (int index = 0; index < taskframes.Count; index++)
        {
            FrameTask task = taskframes[index];
            if (frameCounter < task.destFrame)//检测当前时间是否小于延迟时间
            {
                continue;
            }
            else
            {
                Action<int> ab = task.callback;
                try
                {
                    if (TaskHandle != null)
                    {
                        TaskHandle(ab, task.tid);
                    }
                    else
                    {
                        if (ab != null)
                        {
                            ab(task.tid);
                        }
                    }
                }
                catch (Exception e)
                {
                    LogInfo(e.ToString());
                }
                //移出已经完成的任务
                if (task.count == 1)
                {
                    taskframes.RemoveAt(index);
                    index--;
                    recTidLst.Add(index);//然后移出对应的ID
                }
                else
                {
                    if (task.count != 0)
                    {
                        task.count -= 1;
                    }
                    task.destFrame += task.delay;
                }
            }
        }
    }
    /// <summary>
    /// 删除定时任务
    /// </summary>
    private void DeleteTimeTask()
    {
        if(tmpDelTimes.Count > 0)
        {
            lock (lockTime)
            {
                for(int i = 0;i< tmpDelTimes.Count; i++)
                {
                    bool isDel = false;
                    int delTid = tmpDelTimes[i];
                    for(int j = 0;j< tasktimers.Count; j++)
                    {
                        TimerTask task = tasktimers[j];
                        if(task.tid == delTid)
                        {
                            tasktimers.RemoveAt(j);
                            recTidLst.Add(delTid);
                            LogInfo("del:" + Thread.CurrentThread.ManagedThreadId);
                            isDel = true;
                            break;
                        }
                    }
                    if(isDel)
                    {
                        continue;
                    }
                    for(int k = 0;k< tmptimers.Count; k++)
                    {
                        TimerTask task = tmptimers[k];
                        if (task.tid == delTid)
                        {
                            tmptimers.RemoveAt(k);
                            recTidLst.Add(delTid);
                            LogInfo("del:" + Thread.CurrentThread.ManagedThreadId);
                            isDel = true;
                            break;
                        }
                    }
                }
                tmpDelTimes.Clear();
            }
        }
    }
    /// <summary>
    /// 删除定帧任务
    /// </summary>
    private void DeleteFrameTask()
    {
        if (tmpDelFrame.Count > 0)
        {
            lock (lockTime)
            {
                for (int i = 0; i < tmpDelFrame.Count; i++)
                {
                    bool isDel = false;
                    int delTid = tmpDelFrame[i];
                    for (int j = 0; j < taskframes.Count; j++)
                    {
                        FrameTask task = taskframes[j];
                        if (task.tid == delTid)
                        {
                            taskframes.RemoveAt(j);
                            recTidLst.Add(delTid);
                            isDel = true;
                            break;
                        }
                    }
                    if (isDel)
                    {
                        continue;
                    }
                    for (int k = 0; k < tmpframes.Count; k++)
                    {
                        FrameTask task = tmpframes[k];
                        if (task.tid == delTid)
                        {
                            tmpframes.RemoveAt(k);
                            recTidLst.Add(delTid);
                            isDel = true;
                            break;
                        }
                    }
                }
                tmpDelFrame.Clear();
            }
        }
    }
    #region 定时任务
    /// <summary>
    /// 添加定时任务
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="delay"></param>
    /// <param name="timeUnit"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int AddTimeTask(Action<int> callback, double delay, TimeUnit timeUnit = TimeUnit.Millisecond, int count = 1)
    {
        if (timeUnit != TimeUnit.Millisecond)
        {
            switch (timeUnit)
            {
                case TimeUnit.Second:
                    delay = delay * 1000;
                    break;

                case TimeUnit.Minute:
                    delay = delay * 1000 * 60;
                    break;
                case TimeUnit.Hour:
                    delay = delay * 1000 * 60 * 60;
                    break;
                case TimeUnit.Day:
                    delay = delay * 1000 * 60 * 60 * 24;
                    break;
                default:
                    LogInfo("Add Task TimeUnit Type Error");
                    break;
            }
        }
        int tid = GetTid();
        LogInfo("Add Time Task");
        nowTime = GetUTCMilliseconds();//获取延迟后时间,realtimeSinceStartup是秒钟单位
        lock (lockTime)
        {
            tmptimers.Add(new TimerTask(tid, nowTime + delay, callback, delay, count));//添加进时间任务列表中
        }
        return tid;
    }
    /// <summary>
    /// 删除任务
    /// </summary>
    /// <param name="tid"></param>
    /// <returns></returns>
    public void DeleteTimeTask(int tid)
    {
        lock(lockTime)
        {
            tmpDelTimes.Add(tid);
        }
        //bool exitst = false;
        //for (int i = 0; i < tasktimers.Count; i++)//遍历任务列表
        //{
        //    TimerTask task = tasktimers[i];
        //    if (task.tid == tid)//如果查询到
        //    {
        //        tasktimers.RemoveAt(i);//则删除任务列表中的数据
        //        for (int j = 0; j < tidLst.Count; j++)//然后遍历任务ID列表进行清除
        //        {
        //            if (tidLst[j] == tid)
        //            {
        //                tidLst.RemoveAt(j);
        //                break;
        //            }
        //        }
        //        exitst = true;
        //        break;
        //    }
        //}
        //if (!exitst)
        //{
        //    for (int i = 0; i < tmptimers.Count; i++)//遍历缓存列表进行清除数据
        //    {
        //        TimerTask task = tmptimers[i];
        //        if (tmptimers[i].tid == tid)
        //        {
        //            tmptimers.RemoveAt(i);
        //            for (int j = 0; j < tidLst.Count; j++)//然后遍历任务ID列表进行清除
        //            {
        //                if (tidLst[j] == tid)
        //                {
        //                    tidLst.RemoveAt(j);
        //                    break;
        //                }
        //            }
        //            exitst = true;
        //            break;
        //        }
        //    }
        //}
       // return exitst;
    }
    /// <summary>
    /// 任务替换
    /// </summary>
    /// <param name="tid"></param>
    /// <param name="callback"></param>
    /// <param name="deley"></param>
    /// <param name="timeUnit"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool ReplaceTimeTask(int tid, Action<int> callback, float delay, TimeUnit timeUnit = TimeUnit.Millisecond, int count = 1)
    {
        if (timeUnit != TimeUnit.Millisecond)
        {
            switch (timeUnit)
            {
                case TimeUnit.Second:
                    delay = delay * 1000;
                    break;

                case TimeUnit.Minute:
                    delay = delay * 1000 * 60;
                    break;
                case TimeUnit.Hour:
                    delay = delay * 1000 * 60 * 60;
                    break;
                case TimeUnit.Day:
                    delay = delay * 1000 * 60 * 60 * 24;
                    break;
                default:
                    LogInfo("Add Task TimeUnit Type Error");
                    break;
            }
        }
        LogInfo("Add Time Task");
        nowTime = GetUTCMilliseconds();//获取延迟后时间,realtimeSinceStartup是秒钟单位
        TimerTask newTask = new TimerTask(tid, nowTime + delay, callback, delay, count);//用新的任务替换旧的任务
        bool isRep = false;
        for (int i = 0; i < tasktimers.Count; i++)//在任务列表中查找并替换
        {
            if (tasktimers[i].tid == tid)
            {
                tasktimers[i] = newTask;//进行对应的任务替换
                isRep = true;
                break;
            }
        }
        if (!isRep)
        {
            for (int i = 0; i < tmptimers.Count; i++)//在缓存列表中查找并替换
            {
                if (tmptimers[i].tid == tid)
                {
                    tmptimers[i] = newTask;
                    isRep = true;
                    break;
                }
            }
        }
        return isRep;
    }
    #endregion
    #region 定帧任务
    /// <summary>
    /// 添加定时任务
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="delay"></param>
    /// <param name="timeUnit"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public int AddFrameTask(Action<int> callback, int delay, int count = 1)
    {
        int tid = GetTid();
        LogInfo("Add Time Task");
        lock (lockFrame)
        {
            tmpframes.Add(new FrameTask(tid, frameCounter + delay, callback, delay, count));//添加进时间任务列表中
        }
        return tid;
    }
    /// <summary>
    /// 删除定帧任务
    /// </summary>
    /// <param name="tid"></param>
    /// <returns></returns>
    public void DeleteFrameTask(int tid)
    {
        lock (lockTime)
        {
            tmpDelTimes.Add(tid);
        }
        //bool exitst = false;
        //for (int i = 0; i < taskframes.Count; i++)//遍历任务列表
        //{
        //    FrameTask task = taskframes[i];
        //    if (task.tid == tid)//如果查询到
        //    {
        //        taskframes.RemoveAt(i);//则删除任务列表中的数据
        //        for (int j = 0; j < tidLst.Count; j++)//然后遍历任务ID列表进行清除
        //        {
        //            if (tidLst[j] == tid)
        //            {
        //                tidLst.RemoveAt(j);
        //                break;
        //            }
        //        }
        //        exitst = true;
        //        break;
        //    }
        //}
        //if (!exitst)
        //{
        //    for (int i = 0; i < tmpframes.Count; i++)//遍历缓存列表进行清除数据
        //    {
        //        FrameTask task = tmpframes[i];
        //        if (tmpframes[i].tid == tid)
        //        {
        //            tmpframes.RemoveAt(i);
        //            for (int j = 0; j < tidLst.Count; j++)//然后遍历任务ID列表进行清除
        //            {
        //                if (tidLst[j] == tid)
        //                {
        //                    tidLst.RemoveAt(j);
        //                    break;
        //                }
        //            }
        //            exitst = true;
        //            break;
        //        }
        //    }
        //}
        //return exitst;
    }
    /// <summary>
    /// 任务替换
    /// </summary>
    /// <param name="tid"></param>
    /// <param name="callback"></param>
    /// <param name="deley"></param>
    /// <param name="timeUnit"></param>
    /// <param name="count"></param>
    /// <returns></returns>
    public bool ReplaceFrameTask(int tid, Action<int> callback, int delay, int count = 1)
    {
        LogInfo("Add Time Task");
        FrameTask newTask = new FrameTask(tid, frameCounter + delay, callback, delay, count);//用新的任务替换旧的任务
        bool isRep = false;
        for (int i = 0; i < taskframes.Count; i++)//在任务列表中查找并替换
        {
            if (taskframes[i].tid == tid)
            {
                taskframes[i] = newTask;//进行对应的任务替换
                isRep = true;
                break;
            }
        }
        if (!isRep)
        {
            for (int i = 0; i < tmpframes.Count; i++)//在缓存列表中查找并替换
            {
                if (tmpframes[i].tid == tid)
                {
                    tmpframes[i] = newTask;
                    isRep = true;
                    break;
                }
            }
        }
        return isRep;
    }
    #endregion
    #region 工具类
    /// <summary>
    /// 获取人物的专属ID
    /// </summary>
    /// <returns></returns>
    private int GetTid()
    {
        lock (locktid)
        {
            tid += 1;
            //安全代码,防止达到int上限
            while (true)
            {
                if (tid == int.MaxValue)
                {
                    tid = 0;
                }
                bool used = false;//遍历判断本次ID是否与ID列表中的ID重复
                for (int i = 0; i < tidLst.Count; i++)
                {
                    if (tid == tidLst[i])
                    {
                        used = true;
                        break;
                    }
                }
                if (!used)//不重复则返回
                {
                    break;
                }
                else//重复+1
                {
                    tid += 1;
                }
                tidLst.Add(tid);
            }
        }
        return tid;
    }
    /// <summary>
    /// 清理任务ID列表，将已经结束完成的任务ID清理出去
    /// </summary>
    private void RecycleTid()
    {
        for (int i = 0; i < recTidLst.Count; i++)
        {
            int tid = recTidLst[i];//遍历获取任务ID
            for (int j = 0; j < tidLst.Count; j++)
            {
                if (tidLst[j] == tid)
                {
                    tidLst.RemoveAt(j);
                    break;
                }
            }
        }
        recTidLst.Clear();
    }
    /// <summary>
    /// 设置日志
    /// </summary>
    /// <param name="log"></param>
    public void SetLog(Action<string> log)
    {
        taskLog = log;
    }
    /// <summary>
    /// 重置情况数据
    /// </summary>
    public void Reset()
    {
        tid = 0;
        tidLst.Clear();
        recTidLst.Clear();
        tmptimers.Clear();
        tasktimers.Clear();
        taskframes.Clear();
        tmpframes.Clear();
        taskLog = null;
        srvTimer.Stop();
    }
    /// <summary>
    /// 日志调用
    /// </summary>
    /// <param name="msg"></param>
    private void LogInfo(string msg)
    {
        if (taskLog != null)
        {
            taskLog(msg);
        }
    }
    /// <summary>
    /// 获取世界时间的毫秒数
    /// </summary>
    /// <returns></returns>
    private double GetUTCMilliseconds()
    {//当前时间减去计算机元年
        TimeSpan ts = DateTime.UtcNow - startDateTime;
        return ts.TotalMilliseconds;
    }
    public void SetHandle(Action<Action<int>, int> handle)
    {
        TaskHandle = handle;
    }
    public int GetYear()
    {
        return GetLocalDateTime().Year;
    }
    public int GetMonth()
    {
        return GetLocalDateTime().Month;
    }
    public int GetDay()
    {
        return GetLocalDateTime().Day;
    }
    public int GetWeek()
    {
        return (int)GetLocalDateTime().DayOfWeek;
    }
    /// <summary>
    /// 获取当前时间的字符串
    /// </summary>
    /// <returns></returns>
    public string GetLocalTimeStr()
    {
        DateTime dt = GetLocalDateTime();
        string str = GetTimeStr(dt.Hour) + ":" + GetTimeStr(dt.Minute) + ":" + GetTimeStr(dt.Second);
        return str;
    }
    /// <summary>
    /// 获取时间字符串
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private string GetTimeStr(int time)
    {
        if (time < 10)
        {
            return "0" + time;
        }
        else
        {
            return time.ToString();
        }
    }
    public DateTime GetLocalDateTime()
    {
        DateTime dt = TimeZoneInfo.ConvertTime(startDateTime.AddMilliseconds(nowTime), TimeZoneInfo.FindSystemTimeZoneById("China Standard Time"));
        return dt;
    }
    /// <summary>
    /// 获取当前毫秒时间
    /// </summary>
    /// <returns></returns>
    public double GetMillisecondsTime()
    {
        return nowTime;
    }
    #endregion
}
/// <summary>
/// 时间定时
/// </summary>
public class TimerTask
{
    public int tid;
    public double destTime;//延迟时间
    public double delay;//本次任务延时时间
    public Action<int> callback;//回调函数
    public int count;//任务次数
    public TimerTask(int tid, double destTime, Action<int> callback, double delay, int count)
    {
        this.tid = tid;
        this.destTime = destTime;
        this.callback = callback;
        this.count = count;
        this.delay = delay;
    }
}
/// <summary>
/// 帧定时
/// </summary>
public class FrameTask
{
    public int tid;
    public int destFrame;//延迟时间
    public int delay;//本次任务延时时间
    public Action<int> callback;//回调函数
    public int count;//任务次数
    public FrameTask(int tid, int destFrame, Action<int> callback, int delay, int count)
    {
        this.tid = tid;
        this.destFrame = destFrame;
        this.callback = callback;
        this.count = count;
        this.delay = delay;
    }
}
public enum TimeUnit
{
    Millisecond,//毫秒
    Second,//秒
    Minute,//分钟
    Hour,//小时
    Day,//天
}