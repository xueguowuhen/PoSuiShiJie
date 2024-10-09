/****************************************************
    文件：ComTools
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-04-24 9:05:18
	功能：工具类
*****************************************************/
using System;
using UnityEngine;
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
}
