/****************************************************
    文件：ExtractRootMotion
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-10-23 17:38:59
	功能：Nothing
*****************************************************/
using UnityEditor;
using UnityEngine;
public static class ExtractRootMotion
{

    /// <summary>
    /// 根据给定的时间获取动画中的根位置
    /// </summary>
    /// <param name="time">要获取的位置时间点（单位：秒）</param>
    /// <returns>根位置</returns>
    public static Vector3 GetRootPositionAtTime(AnimationClip animationClip, float time)
    {
        if (animationClip == null)
        {
            Debug.LogWarning("AnimationClip is not assigned.");
            return Vector3.zero; // 返回零向量表示未指定动画剪辑
        }

        // 获取动画曲线
        AnimationCurve curveX = AnimationUtility.GetEditorCurve(animationClip, EditorCurveBinding.FloatCurve("Root", typeof(Transform), "Position.X"));
        AnimationCurve curveY = AnimationUtility.GetEditorCurve(animationClip, EditorCurveBinding.FloatCurve("Root", typeof(Transform), "Position.Y"));
        AnimationCurve curveZ = AnimationUtility.GetEditorCurve(animationClip, EditorCurveBinding.FloatCurve("Root", typeof(Transform), "Position.Z"));


        // 确保时间在动画剪辑范围内
        time = Mathf.Clamp(time, 0, animationClip.length);

        // 计算当前位置
        float deltaX = curveX.Evaluate(time);
        float deltaY = curveY.Evaluate(time);
        float deltaZ = curveZ.Evaluate(time);

        return new Vector3(deltaX, deltaY, deltaZ);
    }

}