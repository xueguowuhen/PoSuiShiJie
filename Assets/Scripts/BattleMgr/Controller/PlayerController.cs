/****************************************************
    文件：PlayerController.cs
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024/4/25 23:35:2
	功能：玩家控制器
*****************************************************/

using CommonNet;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerController : Controller
{
    private Vector3 camOffset;
    public float currentVelocity;
    public int RemotePlayerId;
    private bool IsRun;
    public CharacterController characterController;
    public int syncRate = 45;
    private Vector3 PlayerCurrentRot;
    public float playerCurrentVelocity;
    public Action TransCB = null;
    public int playerCurrentAction;
    private float rotationSpeed = 4f;
    private Queue<DataTrans> PosQue = new Queue<DataTrans>();
    private Dictionary<string, GameObject> fxDic = new Dictionary<string, GameObject>();
    //private int delayfps = 0;
    private DataTrans dataTrans;
    /// <summary>
    /// 是否由寻路接管旋转
    /// </summary>
    private bool IsNavMeshRos = false;
    public override void Init()
    {
        camTran = Camera.main.transform;
        camOffset = transform.position - camTran.position;
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
        if (isLocal)
        {

            SetCam();
        }
    }
    private void FixedUpdate()
    {
        if (skillMove)
        {
            SetSkillMove();
        }
        if (isMove)
        {
            SetMove();
        }
        if (isLocal)
        {
            SendPlayerTransform();
        }
        if (!isLocal)//引入延迟补偿机制
        {
            SetDelaySation();
        }
        SetDir();
    }
    public void SendPlayerTransform()
    {
        if (PlayerCurrentRot != eulerAngles)
        {
            PlayerCurrentRot = eulerAngles;
            GameMsg msg = new GameMsg
            {
                cmd = (int)CMD.ReqTransform,
                reqTransform = new ReqTransform
                {
                    isShoolr = isShoolr,
                    time = TimerSvc.Instance.GetNwTime(),
                    Pos_X = transform.position.x,
                    Pos_Y = transform.position.y,
                    Pos_Z = transform.position.z,
                    Rot_X = eulerAngles.x,
                    Rot_Y = eulerAngles.y,
                    Rot_Z = eulerAngles.z,
                }
            };
            NetSvc.instance.SendMsg(msg);
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
            float roundedAngle = Mathf.Round(angle / 45) * 45;
            eulerAngles = new Vector3(0, roundedAngle, 0);
        }
        isShoolr = true;
        // 计算当前角度与目标角度之间的插值
        Quaternion targetRotation = Quaternion.Euler(eulerAngles);
        Quaternion currentRotation = transform.localRotation;
        Quaternion interpolatedRotation = Quaternion.Slerp(currentRotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);

        // 应用插值后的角度
        transform.localRotation = interpolatedRotation;
    }
    /// <summary>
    /// 
    /// </summary>
    public void SetDelaySation()
    {
        if (PosQue.Count <= 0) return;
        dataTrans = PosQue.Dequeue();//取出值
        //double delaytime = TimerSvc.Instance.GetNwTime() - dataTrans.time;//获取延迟毫秒数
        //delayfps = (int)(delaytime / 20);//计算延迟帧数
        eulerAngles = dataTrans.Rot;
        Vector3 targetPostion = dataTrans.pos;
        characterController.enabled = false;
        transform.position = targetPostion;
        characterController.enabled = true;
    }
    public override void SetTrans(double time, Vector3 Pos, Vector3 Rot)
    {
        DataTrans dataTrans = new DataTrans
        {
            time = time,
            pos = Pos,
            Rot = Rot
        };
        PosQue.Enqueue(dataTrans);
    }
    public override void SetMove()
    {
        if (IsRun)
        {
            moveDistance = Constants.PlayerRunSpeed;
            characterController.Move(transform.forward * Time.fixedDeltaTime * Constants.PlayerRunSpeed);
        }
        else
        {
            moveDistance = Constants.PlayerWalkSpeed;
            characterController.Move(transform.forward * Time.fixedDeltaTime * Constants.PlayerWalkSpeed);
        }
    }
    private void SetSkillMove()
    {
        moveDistance = skillMoveSpeed;
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
    public void SetCam()
    {
        if (camTran != null)
        {
            camTran.position = transform.position - camOffset;
        }

    }
    public void SetNavMeshRot(bool IsRos)
    {
        IsNavMeshRos = IsRos;
    }
    public override void SetVelocity(float Velocity, bool IsRun = false)
    {
        targetVelocity = Velocity;
        this.IsRun = IsRun;
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
            go = ResSvc.instance.ABLoadPrefab(names, name);
            go.transform.SetParent(transform, false);
            fxDic.Add(name, go);
            go.SetActive(true);
            TimerSvc.Instance.AddTimeTask((int tid) =>
            {
                go.SetActive(false);
            }, destroy);
        }
    }
}