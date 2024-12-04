
/****************************************************
    文件：EnemyController.cs
	作者：Kong
    邮箱: 1785275942@qq.com
    日期：2024/6/18 9:50:5
	功能：辅助画圆表现
*****************************************************/
using System.Collections.Generic;
using UnityEngine;
public class DrawCircle : MonoBehaviour  //直接挂在物体本身 只显示在Scene画面中 Game窗口不显示
{
    public List<float> m_Circles = new List<float> { 1 };  //要画的圆的半径
    // 控制圆环显示与隐藏的开关
    public bool showCircle = false;
    // 圆环的分辨率（边的数量）
    private int resolution = 128;
    void OnDrawGizmos()
    {
        // 只有在开关打开时才绘制圆环
        if (showCircle)
        {
            // 设置圆环的颜色为蓝色
            Gizmos.color = Color.blue;
            // 获取物体的位置作为圆环的中心点
            Vector3 center = transform.position;
            // 计算每个顶点的角度增量
            float angleIncrement = 2 * Mathf.PI / resolution;
            // 绘制圆环
            for (int i = 0; i < resolution; i++)
            {
                float angle = i * angleIncrement;
                for (int j = 0; j < m_Circles.Count; j++)
                {
                    float x = center.x + m_Circles[j] * Mathf.Cos(angle);
                    float z = center.z + m_Circles[j] * Mathf.Sin(angle);
                    float nextAngle = (i + 1) * angleIncrement;
                    float nextX = center.x + m_Circles[j] * Mathf.Cos(nextAngle);
                    float nextZ = center.z + m_Circles[j] * Mathf.Sin(nextAngle);
                    Gizmos.DrawLine(new Vector3(x, center.y, z), new Vector3(nextX, center.y, nextZ));
                }
                #region 原代码 单圆形
                // 计算当前顶点在圆环上的角度
                //float angleIncrement = 2 * Mathf.PI / resolution;
                // 计算顶点在圆环上的位置
                //float x = center.x + radius * Mathf.Cos(angle);
                //float z = center.z + radius * Mathf.Sin(angle);
                // 绘制连接到下一个顶点的线条
                //float nextAngle = (i + 1) * angleIncrement;
                //float nextX = center.x + radius * Mathf.Cos(nextAngle);
                //float nextZ = center.z + radius * Mathf.Sin(nextAngle);
                //Gizmos.DrawLine(new Vector3(x, center.y, z), new Vector3(nextX, center.y, nextZ));

                #endregion
            }
        }
    }
    void Update()
    {
        //开关
        if (Input.GetKeyDown(KeyCode.C))
        {
            showCircle = !showCircle; // 切换圆环的显示状态
        }
    }
}
