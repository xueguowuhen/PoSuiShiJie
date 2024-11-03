/****************************************************
    文件：TimerSvc
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-07 8:35:33
	功能：计时系统
*****************************************************/
using Mysqlx.Crud;
using System.Diagnostics;

public class TimerSvc
{
    class TaskPack
    {
        public int tid;
        public Action<int> cb;
        public TaskPack(int tid, Action<int> cb)
        {
            this.tid = tid;
            this.cb = cb;
        }
    }
    private static TimerSvc instance = null;
    public static TimerSvc Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new TimerSvc();
            }
            return instance;
        }
    }
    private ShowTimer st = null;
    Queue<TaskPack> queue= new Queue<TaskPack>();
    private static readonly string tpQueLock = "tpQueLock";
    public void Init()
    {
        st = new ShowTimer(1000);
        queue.Clear();
        //设置日志输出
        st.SetLog((string info) =>
        {
            GameCommon.Log(info);
        });
        st.SetHandle((Action<int> cb, int tid) =>
        {
            if (cb != null)
            {
                lock (tpQueLock)
                {
                    queue.Enqueue(new TaskPack(tid, cb));
                }
            }
        });
        GameCommon.Log("TimerSvc Init Done");
    }
    public void ClearQueue()
    {
        lock (tpQueLock)
        {
            queue.Clear();
        }
    }
    public void Update()
    {
        while (queue.Count > 0)
        {
            TaskPack taskPack = null;
            lock (tpQueLock)
            {
                taskPack = queue.Dequeue();
            }
            if (taskPack != null)
            {
                taskPack.cb(taskPack.tid);
            }
        }
    }
    public int AddTimeTask(Action<int> callback, double delay, TimeUnit timeUnit = TimeUnit.Millisecond, int count = 1)
    {
        return st.AddTimeTask(callback, delay, timeUnit, count);
    }
    public void DeleteTimeTask(int tid)
    {
         st.DeleteTimeTask(tid);
    }
    public double GetUTCMilliseconds()
    {
       return st.GetUTCMilliseconds();
    }
    public long GetTimestamp()
    {
        return st.GetTimestamp();
    }
}
