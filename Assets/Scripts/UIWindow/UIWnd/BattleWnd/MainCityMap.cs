/****************************************************
    文件：MainCityMap.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/28 11:5:59
	功能：城镇NPC
*****************************************************/

using UnityEngine;

public class MainCityMap : MonoBehaviour
{
    public Transform[] NpcPosTrans;
    #region 敌人测试
    public Transform EnemyTest;
    public Transform GetEnemy()
    {
        EnemyTest.GetComponent<EnemyController>().Init(EnemyTest.gameObject); //在进入地图后 获取到敌人时直接经行初始化
        return EnemyTest;
    }
    #endregion
}