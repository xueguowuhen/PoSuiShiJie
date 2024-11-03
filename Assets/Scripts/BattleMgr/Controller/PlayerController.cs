/****************************************************
    文件：PlayerController.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/25 23:35:2
	功能：玩家控制器
*****************************************************/

using CommonNet;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : Controller
{
    public float currentVelocity;
    /// <summary>
    /// 远程人物id
    /// </summary>
    public int RemotePlayerId;
    //private bool IsRun;
    public bool IsEvade;
    public CharacterController characterController;
    //public int syncRate = 45;
    private Vector3 PlayerCurrentRot;
    // public float playerCurrentVelocity;
    public Action TransCB = null;
    public int playerCurrentAction;
    private float rotationSpeed = 15f;
    private Queue<DataTrans> PosQue = new Queue<DataTrans>();
    private Dictionary<string, GameObject> fxDic = new Dictionary<string, GameObject>();
    //private int delayfps = 0;
    private DataTrans dataTrans;
    [SerializeField, Header("闪避移动倍数")]
    [Range(0f, 10f)]
    private float EvadeRange;
    /// <summary>
    /// 是否由寻路接管旋转
    /// </summary>
    private bool IsNavMeshRos = false;
    float smoothingFactor = 5f; // 插值因子，可以根据需求调整

    #region 测试数据
    private float elapsedTime;
    private float totalDistance;
    private Vector3 tartgetPos;

    #endregion

    public override void Init()
    {
        camTran = Camera.main.transform;
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }
    #region OnDrawGizmos 玩家攻击角度划线
    //void OnDrawGizmos()
    //{
    //    // 获取对象的正前方向
    //    Vector3 forward = transform.forward;

    //    // 计算左边30度方向的向量
    //    Quaternion leftRotation = Quaternion.Euler(0, -60, 0);
    //    Vector3 leftDirection = leftRotation * forward;

    //    // 计算右边30度方向的向量
    //    Quaternion rightRotation = Quaternion.Euler(0, 60, 0);
    //    Vector3 rightDirection = rightRotation * forward;

    //    // 起点为对象的位置
    //    Vector3 startPosition = transform.position;

    //    // 终点为起点加上方向向量乘以线的长度
    //    Vector3 leftEndPosition = startPosition + leftDirection * 6;
    //    Vector3 rightEndPosition = startPosition + rightDirection * 6;

    //    // 设置线条颜色
    //    Gizmos.color = Color.red;

    //    // 绘制左边30度的线
    //    Gizmos.DrawLine(startPosition, leftEndPosition);

    //    // 绘制右边30度的线
    //    Gizmos.DrawLine(startPosition, rightEndPosition);
    //}
    #endregion
    public void Update()
    {
        if (currentVelocity != targetVelocity)
        {
            UpdateMixVelocity();

        }
    }
    private void FixedUpdate()
    {
        if (skillMove)
        {
            SetSkillMove();
        }
        if (isLocal)
        {
            SendPlayerTransform();
        }
        if (!isLocal)//引入延迟补偿机制
        {
            SetDelaySation();
            RemoteUpdatePlayer();
        }
        SetDir();
    }
    private void OnAnimatorMove()
    {
        SetMove();
    }
    public void SendPlayerTransform()
    {
        if (PlayerCurrentRot != eulerAngles || currentVelocity != targetVelocity)
        {
            PlayerCurrentRot = eulerAngles;
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqTransform,
                reqTransform = new ReqTransform
                {
                    isShoolr = isShoolr,
                    time = TimerSvc.Instance.GetCurrServerTime(),
                    Pos_X = transform.position.x,
                    Pos_Y = transform.position.y,
                    Pos_Z = transform.position.z,
                    Rot_X = eulerAngles.x,
                    Rot_Y = eulerAngles.y,
                    Rot_Z = eulerAngles.z,
                    speed = targetVelocity
                }
            };
            NetSvc.instance.SendMsgAsync(msg);
        }

    }
    protected override void SetDir()
    {

        if (skillMove || !isSkillRos) { return; }
        if (IsNavMeshRos) { return; }
        if (isLocal && isRos)
        {
            //从当前角度到目标角度的方向
            float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1)) + camTran.eulerAngles.y;
            // 检查角度是否是0度，45度,90
            // 检查角度是否是45的倍数
            if (angle % 20 != 0)
            {
                // 找到最近的45的倍数
                angle = Mathf.Round(angle / 20) * 20;
            }
            eulerAngles = new Vector3(0, angle, 0);
        }
        isShoolr = true;
        Quaternion targetRotation = Quaternion.Euler(eulerAngles);
        Quaternion currentRotation = transform.localRotation;
        Quaternion interpolatedRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        transform.localRotation = interpolatedRotation;
        // Debug.Log("目标：" + targetRotation.eulerAngles);
    }
    /// <summary>
    /// 
    /// </summary>
    public void SetDelaySation()
    {
        if (PosQue.Count <= 0) return;

        dataTrans = PosQue.Dequeue(); // 取出值
        long delayTime = TimerSvc.Instance.GetCurrServerTime() - dataTrans.time; // 获取延迟毫秒数
        float delaySeconds = delayTime / 1000f; // 转换为秒

        targetVelocity = dataTrans.speed;
      //  Debug.Log("当前速度"+currentVelocity+"速度：" + targetVelocity);
        eulerAngles = dataTrans.Rot;
        float speed = targetVelocity == Constants.VelocityRun ? Constants.PlayerRunSpeed : Constants.PlayerWalkSpeed;
        // 预测位置
        Vector3 targetPosition = dataTrans.pos + (animator.deltaPosition * speed * delaySeconds);

        characterController.enabled = false;

        // 平滑插值
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothingFactor);
        characterController.enabled = true;
    }
    public void RemoteUpdatePlayer()
    {

    }
    public override void SetTrans(long time, Vector3 Pos, Vector3 Rot, float speed)
    {
        DataTrans dataTrans = new DataTrans
        {
            time = time,
            pos = Pos,
            Rot = Rot,
            speed = speed
        };
        PosQue.Enqueue(dataTrans);
    }
    public override void SetMove()
    {
        float speed = currentVelocity == Constants.VelocityRun ? Constants.PlayerRunSpeed : Constants.PlayerWalkSpeed;

        moveDistance = speed * animator.deltaPosition /** Time.deltaTime*/;

        // 角色移动
        if (animator.AnimationAtTag("Movement"))
        {
            characterController.Move(moveDistance);

        }
        else
        {
            animator.ApplyBuiltinRootMotion();
        }
        if (animator.AnimationAtTag("Evade") && !animator.IsInTransition(0))
        {
            moveDistance *= EvadeRange;
            characterController.Move(moveDistance);
        }
    }


    private void SetSkillMove()
    {
        //moveDistance = skillMoveSpeed;
        characterController.Move(transform.forward * Time.fixedDeltaTime * skillMoveSpeed);

    }
    public void UpdateMixVelocity()
    {
        //判断当前blender值小于平滑速度就直接设置
        if (Mathf.Abs(currentVelocity - targetVelocity) < Constants.AccelerSpeed * Time.deltaTime)
        {
            currentVelocity = targetVelocity;
        }
        else if (currentVelocity > targetVelocity)
        {
            currentVelocity -= Constants.AccelerSpeed * Time.deltaTime;
        }
        else
        {
            currentVelocity += Constants.AccelerSpeed * Time.deltaTime;
        }
        animator.SetFloat("Velocity", currentVelocity);
    }
    public void SetNavMeshRot(bool IsRos)
    {
        IsNavMeshRos = IsRos;
    }
    public override void SetVelocity(float Velocity)
    {
        targetVelocity = Velocity;
    }
    public override void SetEvade(bool IsEvade)
    {
        this.IsEvade = IsEvade;
    }
    public override void SetFx(string name, float destroy = 0)
    {
        GameObject go;
        if (fxDic.TryGetValue(name, out go))
        {
            go.SetActive(true);
            TimerSvc.Instance.AddTimeTask((int tid) =>
            {
                go.SetActive(false);
            }, destroy);
        }
        else
        {
            go = transform.Find(name).gameObject;
            fxDic.Add(name, go);
            go.SetActive(true);
            TimerSvc.Instance.AddTimeTask((int tid) =>
            {
                go.SetActive(false);
            }, destroy);
        }
    }
    public override void CreateFx(string name, float destroy = 0)
    {
        GameObject go;
        string names = "fx";//包名
        //if (fxDic.TryGetValue(name, out go))
        //{
        //    go.SetActive(true);
        //    TimerSvc.Instance.AddTimeTask((int tid) =>
        //    {
        //        go.SetActive(false);
        //    }, destroy);
        //}
        //else
        //{
        //    go = ResSvc.instance.ABLoadPrefab(names, name);
        //    go.transform.SetParent(transform, false);
        //    fxDic.Add(name, go);
        //    go.SetActive(true);
        //    TimerSvc.Instance.AddTimeTask((int tid) =>
        //    {
        //        go.SetActive(false);
        //    }, destroy);
        //}
    }
}