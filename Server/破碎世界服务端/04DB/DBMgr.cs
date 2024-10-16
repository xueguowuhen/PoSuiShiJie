/****************************************************
    文件：DBMgr
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-16 20:31:20
	功能：数据库处理
*****************************************************/
using MySql.Data.MySqlClient;
using System.Xml.Linq;
using CommonNet;
using System.Data;
using static CfgSvc;
using System.Diagnostics;
using System.Reflection.PortableExecutable;

internal class DBMgr
{
    private static DBMgr instance = null;
    public static DBMgr Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new DBMgr();
            }
            return instance;
        }
    }
    private MySqlConnection conn;
    private string connectionString = "datasource=121.40.86.210;port=3306;database=morangData;user=root;pwd=CRY3ajSbR4sXAR4B;";


    public void Init()
    {
        //conn = new MySqlConnection("datasource=121.40.86.210;port=3306;database=morangData;user=root;pwd=CRY3ajSbR4sXAR4B;");
        //conn.Open();
        GameCommon.Log("DBMgr Init Done.");
    }
    /// <summary>
    /// 判断是否存在该账号
    /// </summary>
    /// <param name="acct"></param>
    /// <param name="pass"></param>
    /// <returns></returns>
    public bool QueryPlayerData(string acct)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from account where acct =@acct", conn);
                cmd.Parameters.AddWithValue("acct", acct);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    return reader.Read(); // 返回是否存在该账号
                }
            }
            catch (Exception ex)
            {
                GameCommon.Log("Query PlayerData By Acct&Pass Error:" + ex, ComLogType.Error);
                return false;
            }
        }
    }
    /// <summary>
    /// 获取该账号数据
    /// </summary>
    /// <param name="acct"></param> 
    /// <param name="pass"></param>
    /// <returns></returns>
    public PlayerData? GetPlayerData(string acct, string pass)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                PlayerData? playerData = null;
                MySqlCommand cmd = new MySqlCommand("select * from account where acct =@acct", conn);
                cmd.Parameters.AddWithValue("acct", acct);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())//判断是否存在该账号
                    {
                        string _pass = reader.GetString("pass");
                        if (_pass.Equals(pass))//密码正确
                        {
                            bool name = reader.IsDBNull(reader.GetOrdinal("name"));
                            if (!name)
                            {
                                playerData = new PlayerData
                                {
                                    id = reader.GetInt32("id"),
                                    name = reader.GetString("name"),
                                    type = reader.GetInt32("type"),
                                    exp = reader.GetInt32("exp"),
                                    level = reader.GetInt32("level"),
                                    aura = reader.GetFloat("aura"),
                                    ruvia = reader.GetFloat("ruvia"),
                                    crystal = reader.GetFloat("crystal"),
                                    ad = reader.GetInt32("ad"),
                                    ap = reader.GetInt32("ap"),
                                    addef = reader.GetInt32("addef"),
                                    apdef = reader.GetInt32("apdef"),
                                    dodge = reader.GetInt32("dodge"),
                                    practice = reader.GetInt32("practice"),
                                    critical = reader.GetInt32("critical"),
                                    rewardTask = new RewardTask(),
                                    TalentID = new List<int>(),
                                    TalentsData = new List<Talent>(),
                                    Bag = new List<BagList>(),
                                    Taskid = reader.GetInt32("Taskid"),
                                    dailyTasks = new List<DailyTask>(),
                                    FriendList = new List<FriendItem>(),
                                    AddFriendList = new List<FriendItem>(),
                                };
                                string[] powers = reader.GetString("power").Split("|");
                                playerData.power = int.Parse(powers[0]);
                                playerData.powerMax = int.Parse(powers[1]);
                                string[] hps = reader.GetString("hp").Split("|");
                                playerData.Hp = int.Parse(hps[0]);
                                playerData.Hpmax = int.Parse(hps[1]);
                                string[] Manas = reader.GetString("Mana").Split("|");
                                playerData.Mana = int.Parse(Manas[0]);
                                playerData.ManaMax = int.Parse(Manas[1]);
                                // 解析 TalentID，过滤掉空字符串 
                                playerData.TalentID = reader.GetString("TalentID")
                                    .Split('|')
                                    .Where(t => !string.IsNullOrEmpty(t)) // 过滤掉空字符串 
                                    .Select(int.Parse)
                                    .ToList();
                                // 解析 TalentsData 
                                string[] talent = reader.GetString("TalentsData").Split('|').Where(t => !string.IsNullOrEmpty(t)).ToArray();
                                for (int i = 0; i < talent.Length; i++)
                                {
                                    string[] strs = talent[i].Split("#");
                                    int result;
                                    if (int.TryParse(strs[0], out result))
                                    {
                                        playerData.TalentsData.Add(new Talent
                                        {
                                            TalentID = int.Parse(strs[0]),
                                            Level = int.Parse(strs[1]),
                                        });
                                    }
                                }
                                //解析bag
                                string[] Bag = reader.GetString("Bag").Split("|").Where(t => !string.IsNullOrEmpty(t)).ToArray();
                                for (int i = 0; i < Bag.Length; i++)
                                {
                                    string[] strs = Bag[i].Split("#");
                                    int result;
                                    if (int.TryParse(strs[0], out result))
                                    {
                                        playerData.Bag.Add(new BagList
                                        {
                                            GoodsID = int.Parse(strs[0]),
                                            count = int.Parse(strs[1]),
                                        });
                                    }
                                }

                                string[] RewardTask = reader.GetString("RewardTask").Split("|");
                                string[] reward = RewardTask[0].Split("#");
                                playerData.rewardTask.TaskProgress = new List<int>();
                                for (int i = 0; i < reward.Length; i++)
                                {
                                    playerData.rewardTask.TaskProgress.Add(int.Parse(reward[i]));
                                }
                                playerData.rewardTask.LastTime = DateTime.Parse(RewardTask[1]);
                                //解析DailyTask
                                string[] DailyTask = reader.GetString("DailyTask").Split("|").Where(t => !string.IsNullOrEmpty(t)).ToArray();
                                for (int i = 0; i < DailyTask.Length; i++)
                                {
                                    string[] strs = DailyTask[i].Split("#");
                                    int result;
                                    if (int.TryParse(strs[0], out result))
                                    {
                                        playerData.dailyTasks.Add(new DailyTask
                                        {
                                            TaskID = int.Parse(strs[0]),//每日任务id
                                            TaskReward = int.Parse(strs[1]),//任务进度
                                            TaskFinish = bool.Parse(strs[2]),//任务完成状态
                                        });
                                    }
                                }
                                //好友id
                                string[] FriendId = reader.GetString("Friend").Split("|");
                                AddFriendsToPlayerData(playerData.FriendList, FriendId.ToList());
                                //好友申请列表
                                string[] AddFriend = reader.GetString("AddFriend").Split("|");
                                AddFriendsToPlayerData(playerData.AddFriendList, AddFriend.ToList());
                            }//账号存在内容
                            else //存在账号，但没有创建角色
                            {
                                playerData = new PlayerData
                                {
                                    id = reader.GetInt32("id"),
                                    TalentsData = new List<Talent>
                                    {

                                    }
                                };
                            }
                            return playerData;
                        }
                        else//密码错误
                        {
                            return null;
                        }
                    }
                    else//不存在该账号
                    {
                        return null;
                    }
                }

            }
            catch (Exception ex)
            {
                GameCommon.Log("RegistAcct PlayeData Error:" + ex, ComLogType.Error);
                return null;
            }
        }
    }
    /// <summary>
    /// 根据id获取好友数据
    /// </summary>
    /// <param name="FriendList"></param>
    /// <param name="accounts"></param>
    /// <returns></returns>
    public void AddFriendsToPlayerData(List<FriendItem> FriendList, List<string> accounts)
    {
        if (accounts == null || accounts.Count == 0)
        {
            return; // 如果没有好友账号，直接返回
        }

        using (var conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();

                // 创建查询字符串，使用IN来查询多个账号
                string query = "SELECT id,type, name, level,aura,ruvia,crystal FROM account WHERE id IN (" +
                               string.Join(", ", accounts.Select(a => $"'{a}'")) + ")";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read()) // 遍历查询结果
                    {
                        FriendList.Add(new FriendItem
                        {
                            id = reader.GetInt32("id"),
                            type = reader.GetString("type"),
                            name = reader.GetString("name"),
                            level = reader.GetInt32("level"),
                            aura = reader.GetInt32("aura"),
                            ruvia = reader.GetInt32("ruvia"),
                            crystal = reader.GetInt32("crystal"),
                        });
                    }
                }
            }
            catch (Exception ex)
            {
                GameCommon.Log("Error querying player data for friends: " + ex, ComLogType.Error);
            }
        }
    }
    /// <summary>
    /// 根据好友名称获取好友数据
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public FriendItem GetPlayerDataByFriendName(string name)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("select * from account where name =@name", conn);
                cmd.Parameters.AddWithValue("name", name);
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())//判断是否存在该账号
                    {
                        FriendItem friendItem = new FriendItem
                        {
                            id = reader.GetInt32("id"),
                            type = reader.GetString("type"),
                            name = reader.GetString("name"),
                            level = reader.GetInt32("level"),
                            aura = reader.GetInt32("aura"),
                            ruvia = reader.GetInt32("ruvia"),
                            crystal = reader.GetInt32("crystal"),
                            FriendList = ParseFriendList(reader.GetString("Friend")),
                            AddFriendList = ParseFriendList(reader.GetString("AddFriend")),
                        };
                        return friendItem;
                    }
                    else//不存在该账号
                    {
                        return null;
                    }
                }
            }
            catch (Exception ex)
            {
                GameCommon.Log("GetPlayerDataByFriendName Error:" + ex, ComLogType.Error);
                return null;
            }
        }
    }
    private List<int> ParseFriendList(string friendData)
    {
        if (string.IsNullOrEmpty(friendData))
        {
            return new List<int>(); // 返回空列表
        }
        return friendData.Split('|')
                         .Select(idString => int.TryParse(idString, out int id) ? id : 0)
                         .Where(id => id != 0) // 过滤掉无效值
                         .ToList();
    }

    /// <summary>
    /// 注册账号
    /// </summary>
    /// <returns></returns>
    public bool RegistAcct(string acct, string pass)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("insert into account set acct =@acct,pass=@pass", conn);
                cmd.Parameters.AddWithValue("acct", acct);
                cmd.Parameters.AddWithValue("pass", pass);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                GameCommon.Log("RegistAcct PlayeData Error:" + ex, ComLogType.Error);
                return false;
            }
            return true;
        }
    }

    /// <summary>
    /// 检查该名称是否已经被使用
    /// </summary>
    /// <param name="name">要检查的账户名称</param>
    /// <returns>如果名称已被使用则返回 true，否则返回 false</returns>
    public bool CheckName(string name)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand("SELECT COUNT(*) FROM account WHERE name = @name", conn);
                cmd.Parameters.AddWithValue("name", name);

                // 执行查询并获取结果
                int count = Convert.ToInt32(cmd.ExecuteScalar());
                return count > 0; // 如果计数大于 0，表示该名称已被使用
            }
            catch (Exception ex)
            {
                GameCommon.Log("CheckName Error: " + ex, ComLogType.Error);
                return true;
            }
        }
    }

    /// <summary>
    /// 更新好友数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="playerData"></param>
    /// <returns></returns>
    public bool UpdateFriend(FriendItem friendItem)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                "update account set Friend=@Friend,AddFriend=@AddFriend,aura=@aura,ruvia=@ruvia,crystal=@crystal where id=@id", conn);
                cmd.Parameters.AddWithValue("id", friendItem.id);
                cmd.Parameters.AddWithValue("aura", friendItem.aura);
                cmd.Parameters.AddWithValue("ruvia", friendItem.ruvia);
                cmd.Parameters.AddWithValue("crystal", friendItem.crystal);
                cmd.Parameters.AddWithValue("Friend", string.Join("|", friendItem.FriendList.Select(f => f)));
                cmd.Parameters.AddWithValue("AddFriend", string.Join("|", friendItem.AddFriendList.Select(f => f)));
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                GameCommon.Log("Update PlayerData Error:" + e, ComLogType.Error);
                return false;
            }
            return true;
        }
    }

    #region 天赋相关的数据库检索
    /// <summary>
    /// 检查并更新天赋数据
    /// </summary>
    /// <returns></returns>
    public bool CheckAndUpdateTalent(int id, int talentID, int talentLevel,TalentCfg talentCfg)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            try
            {
                string talentsdata = "";
                conn.Open();
                MySqlCommand cmd1 = new MySqlCommand("SELECT TalentsData FROM account WHERE id=@id", conn);
                cmd1.Parameters.AddWithValue("id", id);
                using (MySqlDataReader reader = cmd1.ExecuteReader())
                {
                    Console.WriteLine(reader.Read());
                    talentsdata = reader.GetString("TalentsData");
                }
                Talent[] talents = ParseTalentDataFromMysql(talentsdata);
                for (int i = 0; i < talents.Length; i++)
                {
                    if (talentID == talents[i].TalentID && talentLevel - talents[i].Level == 1 &&talentLevel<=talentCfg.MaxLevel)
                    {
                        talents[i].Level++;
                        string Talents = "";
                        foreach (Talent j in talents)
                        {
                            Talents += j.TalentID;
                            Talents += "#";
                            Talents += j.Level;
                            Talents += "|";
                        }
                        MySqlCommand cmd2 = new MySqlCommand("UPDATE account set TalentsData=@data WHERE id=@id", conn);
                        cmd2.Parameters.AddWithValue("data", Talents);
                        cmd2.Parameters.AddWithValue("id", id);
                        cmd2.ExecuteNonQuery();
                        return true;
                    }
                }
                return false;

            }
            catch (Exception e)
            {
                GameCommon.Log("CheckAndUpdateTalent:" + e, ComLogType.Error);
                return false;
            }
        }
    }




    /// <summary>
    /// 解析数据库天赋数据(工具方法)
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    public static Talent[] ParseTalentDataFromMysql(string data)
    {
        string[] talent = data.Split('|').Where(t => !string.IsNullOrEmpty(t)).ToArray();
        List<Talent> talents = new List<Talent>();
        for (int i = 0; i < talent.Length; i++)
        {
            string[] strs = talent[i].Split("#");
            if (int.TryParse(strs[0], out int result))
            {
                talents.Add(new Talent
                {
                    TalentID = int.Parse(strs[0]),
                    Level = int.Parse(strs[1]),
                });
            }
        }
        return talents.ToArray();
    } 
    #endregion

    /// <summary>
    /// 更新服务器用户数据
    /// </summary>
    /// <param name="id"></param>
    /// <param name="playerData"></param>
    /// <returns></returns>
    public bool UpdatePlayerData(PlayerData playerData)
    {
        using (var conn = new MySqlConnection(connectionString))
        {
            try
            {
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(
                "update account set name=@name,type=@type,exp=@exp,level=@level,power=@power,aura=@aura," +
                "ruvia=@ruvia,crystal=@crystal,hp=@hp,mana=@mana,ad=@ad,ap=@ap,addef=@addef,apdef=@apdef,dodge=@dodge,practice=@practice," +
                "critical=@critical,TalentID=@TalentID,TalentsData=@TalentsData,Bag=@Bag,RewardTask=@RewardTask,DailyTask=@DailyTask," +
                "Taskid=@Taskid,Friend=@Friend,AddFriend=@AddFriend where id=@id", conn);
                cmd.Parameters.AddWithValue("id", playerData.id);
                cmd.Parameters.AddWithValue("name", playerData.name);
                cmd.Parameters.AddWithValue("type", playerData.type);
                cmd.Parameters.AddWithValue("exp", playerData.exp);
                cmd.Parameters.AddWithValue("level", playerData.level);
                cmd.Parameters.AddWithValue("aura", playerData.aura);
                cmd.Parameters.AddWithValue("ruvia", playerData.ruvia);
                cmd.Parameters.AddWithValue("crystal", playerData.crystal);
                cmd.Parameters.AddWithValue("ad", playerData.ad);
                cmd.Parameters.AddWithValue("ap", playerData.ap);
                cmd.Parameters.AddWithValue("addef", playerData.addef);
                cmd.Parameters.AddWithValue("apdef", playerData.apdef);
                cmd.Parameters.AddWithValue("dodge", playerData.dodge);
                cmd.Parameters.AddWithValue("practice", playerData.practice);
                cmd.Parameters.AddWithValue("critical", playerData.critical);
                cmd.Parameters.AddWithValue("power", $"{playerData.power}|{playerData.powerMax}");
                cmd.Parameters.AddWithValue("hp", $"{playerData.Hp}|{playerData.Hpmax}");
                cmd.Parameters.AddWithValue("mana", $"{playerData.Mana}|{playerData.ManaMax}");
                cmd.Parameters.AddWithValue("TalentID", string.Join("|", playerData.TalentID));
                //拼接天赋数据
                string Talents = "";
                foreach (Talent i in playerData.TalentsData)
                {
                    Talents += i.TalentID;
                    Talents += "#";
                    Talents += i.Level;
                    Talents += "|";
                }
                cmd.Parameters.AddWithValue("TalentsData", Talents);
                //拼接背包数据
                string Bag = "";
                if (playerData.Bag != null)
                {
                    foreach (var kvp in playerData.Bag)
                    {
                        Bag += kvp.GoodsID;
                        Bag += "#";
                        Bag += kvp.count;
                        Bag += "|";
                    }
                }
                cmd.Parameters.AddWithValue("Bag", Bag);

                cmd.Parameters.AddWithValue("Taskid", playerData.Taskid);

                //拼接每日奖励数据
                string RewardTask = "";
                if (playerData.rewardTask != null)
                {

                    foreach (var kvp in playerData.rewardTask.TaskProgress)
                    {
                        RewardTask += kvp;
                        RewardTask += "#";
                    }
                    //去掉最后一个字符
                    if (RewardTask.Length > 0)
                    {
                        RewardTask = RewardTask.Substring(0, RewardTask.Length - 1);
                    }
                }
                cmd.Parameters.AddWithValue("RewardTask", $"{RewardTask}|{playerData.rewardTask.LastTime}");

                string DailyTask = "";
                if (playerData.dailyTasks != null)
                {

                    foreach (var kvp in playerData.dailyTasks)
                    {
                        DailyTask += kvp.TaskID;
                        DailyTask += "#";
                        DailyTask += kvp.TaskReward;
                        DailyTask += "#";
                        DailyTask += kvp.TaskFinish;
                        DailyTask += "|";
                    }
                }
                cmd.Parameters.AddWithValue("DailyTask", DailyTask);
                cmd.Parameters.AddWithValue("Friend", string.Join("|", playerData.FriendList.Select(f => f.id)));
                cmd.Parameters.AddWithValue("AddFriend", string.Join("|", playerData.AddFriendList.Select(f => f.id)));
                cmd.ExecuteNonQuery();

            }
            catch (Exception e)
            {
                GameCommon.Log("Update PlayerData Error:" + e, ComLogType.Error);
                return false;
            }
            return true;
        }
    }
}
