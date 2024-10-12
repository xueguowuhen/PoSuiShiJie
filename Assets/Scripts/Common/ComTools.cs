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
    public static Sprite GetItemSprite(ItemType itemType, string PathName)
    {
        switch (itemType)
        {
            case ItemType.consume:
                return Resources.Load<Sprite>(PathDefine.ResUI + PathDefine.props + PathName);
            case ItemType.equip:
                return Resources.Load<Sprite>(PathDefine.ResUI + PathDefine.Equip + PathName);
            case ItemType.material:
                return Resources.Load<Sprite>(PathDefine.ResUI + PathDefine.props + PathName);
        }
        return null;
    }
    public static Sprite GetIconSprite(ItemType itemType)
    {
        switch (itemType)
        {
            case ItemType.consume:
                return Resources.Load<Sprite>(PathDefine.ResUI + PathDefine.icon + "aura");
            case ItemType.equip:
                return Resources.Load<Sprite>(PathDefine.ResUI + PathDefine.icon + "ruvia");
            case ItemType.material:
                return Resources.Load<Sprite>(PathDefine.ResUI + PathDefine.icon + "crystal");
        }
        return null;
    }
    public static Sprite GetIconSprite(BuyType itemType)
    {
        switch (itemType)
        {
            case BuyType.aura:
                return Resources.Load<Sprite>(PathDefine.ResUI + PathDefine.icon + "aura");
            case BuyType.ruvia:
                return Resources.Load<Sprite>(PathDefine.ResUI + PathDefine.icon + "ruvia");
            case BuyType.crystal:
                return Resources.Load<Sprite>(PathDefine.ResUI + PathDefine.icon + "crystal");
        }
        return null;
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

}
