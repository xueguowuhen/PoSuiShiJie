/****************************************************
    文件：CameraPlayerCtrl.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/25 14:7:11
    功能：Nothing
*****************************************************/
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CameraPlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform m_CameraUpAndDown;
    public Transform m_CameraZoomContainer;
    public Transform m_CameraContainer;
    private Transform LookAtTarget;
    private Vector3 offset;
    public float rotationAgle = 30f;
    public float rotationSpeed = 10.0f;
    public void Init( MapCfg mapCfg,Transform lookAtTarget)
    {
        m_CameraZoomContainer.position= new Vector3(0, 0, mapCfg.mainCamPos.z);
        m_CameraUpAndDown.position = new Vector3(0, mapCfg.mainCamPos.y, 0);
        m_CameraContainer.rotation = Quaternion.Euler(new Vector3(mapCfg.mainCamRote.x, 0, 0));
       // transform.localEulerAngles = mapCfg.mainCamRote;
        this.LookAtTarget = lookAtTarget;
        offset = LookAtTarget.position - m_CameraZoomContainer.position;
    }
     
    public void Update()
    {
        if (LookAtTarget == null) return;

        // 计算新的摄像头位置
        //Vector3 targetPosition = LookAtTarget.position - offset; // 在目标位置上添加偏移量
        //m_CameraZoomContainer.position = targetPosition; // 更新摄像头位置
        gameObject.transform.position=LookAtTarget.transform.position;
        // 保持相机始终看向目标
         m_CameraZoomContainer.LookAt(LookAtTarget);

    }
    public float minVerticalAngle = -30f; // 最低角度
    public float maxVerticalAngle = 30f;  // 最高角度
    private float currentVerticalAngle = 0f;

    public void SetDir(Vector2 dir)
    {
        if (LookAtTarget == null) return;

        // 计算旋转的水平角度
        //transform.RotateAround(LookAtTarget.position, Vector3.up, dir.x * rotationSpeed * Time.deltaTime);

        //// 计算当前垂直角度并限制上下范围
        //float newVerticalAngle = currentVerticalAngle + (-dir.y * rotationSpeed * Time.deltaTime);
        //newVerticalAngle = Mathf.Clamp(newVerticalAngle, minVerticalAngle, maxVerticalAngle);

        //float angleChange = newVerticalAngle - currentVerticalAngle;
        //transform.RotateAround(LookAtTarget.position, transform.right, angleChange);

        //// 更新当前的垂直角度
        //currentVerticalAngle = newVerticalAngle;
        //offset = LookAtTarget.position - transform.position;
        //// 保持相机始终看向目标
        //transform.LookAt(LookAtTarget);
    }


}
