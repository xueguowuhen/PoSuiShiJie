/****************************************************
    文件：LayoutAdjustment.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/7 21:3:16
    功能：画布大小调整
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LayoutAdjustment : MonoBehaviour
{
    private GridLayoutGroup gridLayoutGroup;
    private RectTransform rectTransform;

    // Start is called before the first frame update
    void Start()
    {
        gridLayoutGroup = GetComponent<GridLayoutGroup>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        AdjustLayout();
    }
    // 更新画布大小的方法
    public void AdjustLayout()
    {
        // 假设可以通过gridLayoutGroup获取子物体数量
        int itemCount = gridLayoutGroup.transform.childCount;

        float paddingTop = gridLayoutGroup.padding.top; // 上边距
        float paddingBottom = gridLayoutGroup.padding.bottom; // 下边距
                                                              // 获取上下边距
        float paddingLeft = gridLayoutGroup.padding.left; // 左边距
        float paddingRight = gridLayoutGroup.padding.right;
        float spacingY = gridLayoutGroup.spacing.y; // y轴间距
        // 获取 x 轴间距
        float spacingX = gridLayoutGroup.spacing.x;
        // 根据子物体数量计算行数
        int columns = gridLayoutGroup.constraintCount; // 列数
        if(itemCount < columns)
        {
            columns = itemCount;
        }
        int rows = Mathf.CeilToInt((float)itemCount / columns); // 计算行数

        // 物体的高度
        float itemSizeY = gridLayoutGroup.cellSize.y; // 物体的高度
        float itemSizeX = gridLayoutGroup.cellSize.x;
        // 计算总高度（考虑间距）
        float height = rows * itemSizeY + (rows - 1) * spacingY - paddingTop - paddingBottom; // 考虑间距
                                                                                              // 计算总宽度（考虑间距和边距）
        float width = columns * itemSizeX + (columns - 1) * spacingX - paddingLeft - paddingRight+ itemSizeX/2;
        // 设置 RectTransform 的高度
        rectTransform.sizeDelta = new Vector2(width, height);

    }
}
