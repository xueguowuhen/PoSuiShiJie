/****************************************************
    文件：ComTools
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-24 9:05:18
	功能：工具类
*****************************************************/
using CommonNet;
using DG.Tweening;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

public class ComTools
{
    public static int RDInt(int min, int max, Random rd = null)
    {
        if (rd == null) rd = new Random();
        int val = rd.Next(min, max);
        return val;
    }
    public static void GetItemSprite(ItemType itemType, string pathName, Action<Texture2D> callback)
    {
        string path = PathDefine.ResUI;

        switch (itemType)
        {
            case ItemType.consume:
            case ItemType.material:
                path += PathDefine.props;
                break;
            case ItemType.equip:
                path += PathDefine.Equip;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null);
        }

        LoadSprite(path, pathName, callback);
    }

    public static void GetIconSprite(ItemType itemType, Action<Texture2D> callback)
    {
        string path = PathDefine.ResUI + PathDefine.icon;

        switch (itemType)
        {
            case ItemType.consume:
                LoadSprite(path, "aura", callback);
                break;
            case ItemType.equip:
                LoadSprite(path, "ruvia", callback);
                break;
            case ItemType.material:
                LoadSprite(path, "crystal", callback);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(itemType), itemType, null);
        }
    }
    public static void GetImg(string path, Action<Texture2D> callback)
    {

        LoadSprite(PathDefine.ResHard, path, callback);

    }
    public static void GetIconSprite(BuyType buyType, Action<Texture2D> callback)
    {
        string path = PathDefine.ResUI + PathDefine.icon;
        LoadSprite(path, buyType.ToString(), callback);
    }
    public static void LoadSprite(string path, string name, Action<Texture2D> callback)
    {
#if DEBUG_ASSETBUNDLE
        AssetLoaderSvc.instance.LoadOrDownload<Texture2D>(path, name, (Texture2D sprite) =>
       {
           callback(sprite);
       });
#elif UNITY_EDITOR
        string path1 = Path.Combine(PathDefine.Download, path , name+ PathDefine.Png);
        Debug.Log(path1);
        callback( UnityEditor.AssetDatabase.LoadAssetAtPath<Texture2D>(path1));
#endif

    }


    /// <summary>
    /// 根据等级和经验计算本级经验值
    /// </summary>
    /// <param name="level"></param>
    /// <param name="BaseExp"></param>
    /// <param name="ExpMul"></param>
    /// <returns></returns>
    public static float GetExperienceForLevel(int level, int BaseExp, float ExpMul)
    {
        float experience = BaseExp;

        // 计算每级的经验需求
        for (int i = 1; i < level; i++)
        {
            experience *= ExpMul; // 以增幅计算下一级经验
        }

        return experience; // 返回指定级别的经验需求
    }

    public CommonNet.BattleData GetBattleDataFromPlayerData(PlayerData playerData)
    {
        CommonNet.BattleData data = new CommonNet.BattleData
        {
            Hp = playerData.Hp,
            Hpmax = playerData.Hpmax,
            Mana = playerData.Mana,
            ManaMax = playerData.ManaMax,
            ad = playerData.ad,
            addef = playerData.addef,
            ap = playerData.ap,
            apdef = playerData.apdef,
            dodge = playerData.dodge,
            critical = playerData.critical,
        };
        return data;
    }
}
