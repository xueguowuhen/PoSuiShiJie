/****************************************************
    文件：CameraPlayerCtrl.cs
    作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/10/25 14:7:11
    功能：Nothing
*****************************************************/
using UnityEngine;

public class CameraPlayerCtrl : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform m_CameraUpAndDown;
    public Transform m_CameraZoomContainer;
    public Transform m_CameraContainer;
    private Transform LookAtTarget;
    private float offsety;
    [SerializeField, Header("相机左右移动速度")]
    private float rotationSpeed = 10.0f;
    [SerializeField, Header("相机上下移动速度")]
    private float upAndDownSpeed = 10.0f;
    [SerializeField, Header("相机偏移距离")]
    private float CamTargetOffset = 2.8f;
    [SerializeField, Header("相机碰撞平滑速度")]
    private float smoothingSpeed = 5.0f;
    [SerializeField, Header("相机碰撞偏移距离")]
    private float CamPosOffset = 0.2f;

    public void Init(MapCfg mapCfg, Transform lookAtTarget)
    {
        m_CameraZoomContainer.position = mapCfg.CameraZoomContainerPos;
        m_CameraZoomContainer.rotation = Quaternion.Euler(mapCfg.CameraZoomContainerRote);
        m_CameraUpAndDown.position = mapCfg.CameraUpAndDownPos;
        m_CameraUpAndDown.rotation = Quaternion.Euler(mapCfg.CameraUpAndDownRote);
        offsety = mapCfg.CameraFollowAndRotatePos.y;
        transform.position = mapCfg.CameraFollowAndRotatePos;
        transform.rotation = Quaternion.Euler(mapCfg.CameraFollowAndRotateRote);
        this.LookAtTarget = lookAtTarget;
    }
    private Vector3 targetPosition;
    public void Update()
    {
        if (LookAtTarget == null) return;

        Vector3 TargetLookAtPos = new Vector3(LookAtTarget.position.x, LookAtTarget.position.y + offsety, LookAtTarget.position.z);
        gameObject.transform.position = TargetLookAtPos;
        // 保持相机始终看向目标
        m_CameraZoomContainer.LookAt(TargetLookAtPos);
        // 定义射线的起点和终点
        Vector3 startPoint = TargetLookAtPos; // 射线的起点
        Vector3 endPoint = m_CameraContainer.position; // 射线的终点
                                                       // Debug.DrawLine(startPoint, endPoint, Color.red);
                                                       //定义一条射线
        RaycastHit hit;
        if (Physics.Linecast(startPoint, endPoint, out hit))
        {
            if (name != "MainCamera")
            {
                //如果射线碰撞的不是相机，那么就取得射线碰撞点到玩家的距离
                float currentDistance = Vector3.Distance(hit.point, LookAtTarget.position);
                //如果射线碰撞点小于玩家与相机本来的距离，就说明角色身后是有东西，为了避免穿墙，就把相机拉近
                if (currentDistance < CamTargetOffset)
                {
                    // 平滑过渡到目标位置
                    targetPosition = new Vector3(currentDistance - CamPosOffset, 0, 0);
                    //  Debug.Log("碰撞点距离：" + (currentDistance- CamPosOffset));
                    m_CameraZoomContainer.transform.localPosition = Vector3.Lerp(m_CameraZoomContainer.transform.localPosition, targetPosition, Time.deltaTime * smoothingSpeed);
                }
            }
        }
        else
        {
            // 如果没有碰撞，检查后方
            // 可以添加一个新的射线从角色向相反方向检测
            Vector3 backwardStartPoint = LookAtTarget.position + Vector3.up; // 角色位置
            Vector3 backwardEndPoint = LookAtTarget.position - m_CameraContainer.forward * CamTargetOffset; // 后方一定距离
            if (!Physics.Linecast(backwardStartPoint, backwardEndPoint))
            {
                // 如果后方没有物体，返回到初始位置
                Vector3 targetPosition = new Vector3(CamTargetOffset, 0, 0);
                m_CameraZoomContainer.transform.localPosition = Vector3.Lerp(m_CameraZoomContainer.transform.localPosition, targetPosition, Time.deltaTime * smoothingSpeed);
            }
            else
            {
                if (targetPosition == Vector3.zero) return;
                m_CameraZoomContainer.transform.localPosition = Vector3.Lerp(m_CameraZoomContainer.transform.localPosition, targetPosition, Time.deltaTime * smoothingSpeed);
                Debug.Log(targetPosition);
            }
        }

    }
    [SerializeField, Header("相机最低角度")]
    public float minVerticalAngle = -30f; // 最低角度
    [SerializeField, Header("相机最高角度")]
    public float maxVerticalAngle = 30f;  // 最高角度
    public void SetDir(Vector2 dir)
    {
        if (LookAtTarget == null) return;

        // 获取当前的z轴旋转
        float currentZRotation = m_CameraUpAndDown.transform.localEulerAngles.z;

        if (currentZRotation > 180)
        {
            currentZRotation -= 360; // 将值转换为 -180 到 180 的范围
        }

        if (dir == Vector2.left)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime * -1, 0);

        }
        else if (dir == Vector2.right)
        {
            transform.Rotate(0, rotationSpeed * Time.deltaTime * 1, 0);
        }
        else if (dir == Vector2.up)
        {
            currentZRotation -= upAndDownSpeed * Time.deltaTime; // 向上旋

            currentZRotation = Mathf.Clamp(currentZRotation, -30f, 70f);
            // 应用限制后的旋转值
            m_CameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0, currentZRotation);

        }
        else if (dir == Vector2.down)
        {
            currentZRotation += upAndDownSpeed * Time.deltaTime; // 向上旋
            m_CameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0, Mathf.Clamp(m_CameraUpAndDown.transform.localEulerAngles.z, -30f, 70f));
            // 对旋转值进行限制
            currentZRotation = Mathf.Clamp(currentZRotation, -30f, 70f);
            // 应用限制后的旋转值
            m_CameraUpAndDown.transform.localEulerAngles = new Vector3(0, 0, currentZRotation);

        }

    }


}
