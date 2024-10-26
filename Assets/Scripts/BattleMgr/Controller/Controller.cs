/****************************************************
    文件：Controller
	作者：无痕
    邮箱: 1450411269@qq.com
    日期：2024-05-10 9:17:55
	功能：表现实体控制器基类
*****************************************************/
using System;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Controller : MonoBehaviour
{
    public Animator animator;
    public float targetVelocity;
    public int action;
    public bool isInput;
    protected bool isRos = false;//判断旋转
    public bool ss = true;
    public Vector3 moveDistance;
    public bool isSkillRos
    {
        get
        {
            return ss;
        }
        set
        {
            //GameCommon.Log("被修改了");
            ss = value;
        }
    }


   // public bool isMove = false;//判断移动
    protected bool skillMove = false;//判断技能移动
    public Vector3 eulerAngles;
    //protected float skillUpMoveSpeed;
    public bool isLocal = false;//判断是否本地
    public bool isShoolr = true;//判断是否平滑
    protected Vector2 dir = Vector2.zero;
    protected float skillMoveSpeed;
    protected Transform camTran;
    public Vector2 Dir
    {
        get
        {
            return dir;
        }
        set
        {
            //Debug.Log(value);
            if (value == Vector2.zero)
            {
                isRos = false;
            }
            else
            {
                isRos = true;
            }
            dir = value;
        }
    }
    public virtual void SetFx(string name, float destroy = 0)
    {

    }
    public virtual void CreateFx(string name, float destroy = 0)
    {

    }
    public virtual void SetTrans(double time, Vector3 Pos, Vector3 Rot)
    {
    }
    protected virtual void SetDir()
    {

    }

    public virtual void SetSkillMoveState(bool move, float skillSpeed = 0f)
    {
        skillMove = move;
        skillMoveSpeed = skillSpeed;
    }

    public virtual void SetRootMotion(bool isMotion)
    {
        animator.applyRootMotion = isMotion;
    }
    public virtual void SetMove()
    {

    }
    public virtual void SetVelocity(float Velocity)
    {
        targetVelocity = Velocity;
        animator.SetFloat("Velocity", Velocity);
        //daggerskill1fx.gameObject.SetActive(true);
    }
    public virtual void SetEvade(bool isEvade)
    {

    }
    public virtual void SetAction(int act)
    {
        action = act;
        animator.SetInteger("Action", act);
    }
    public virtual AniPlayerState GetAction()
    {
        action = animator.GetInteger("Action");
        return (AniPlayerState)action;
    }
    public virtual void SetHasInput(bool isInput)
    {
        this.isInput = isInput;
        animator.SetBool("HasInput", isInput);
    }
    public virtual bool GetHasInput()
    {
        return isInput;
    }
    public virtual AniPlayerState GetWalkOrRunState()
    {
        if (targetVelocity == Constants.VelocityWalk)
        {
            return AniPlayerState.WalkStop;
        }
        else if (targetVelocity == Constants.VelocityRun)
        {
            return AniPlayerState.RunStop;
        }
        return AniPlayerState.WalkStop;
    }
    public virtual void SetAniCrossFade(string name, float time)
    {
        animator.CrossFade(name, time);
    }
    public virtual void SetAniPlay(string name)
    {
        animator.Play(name);
    }
    public virtual void DestroySelf()
    {
        Destroy(transform.parent.gameObject);
    }
    public virtual void SetAtkRotationLocal(Vector2 atkDir)
    {
        //从当前角度到目标角度的方向
        float angle = Vector2.SignedAngle(atkDir, new Vector2(0, 1)) + camTran.eulerAngles.y;
        float roundedAngle = Mathf.Round(angle / 45) * 45;
        eulerAngles = new Vector3(0, roundedAngle, 0);
        isShoolr = false;
        transform.localRotation = Quaternion.Euler(eulerAngles);
        //   GameCommon.Log(eulerAngles.ToString());
    }
    public virtual void SetAtkRotationRetemo(Vector3 atkDir)
    {
        eulerAngles = atkDir;
        transform.localRotation = Quaternion.Euler(atkDir);
    }
    public void SetAniRootMove(bool IsShow = true)
    {
        animator.applyRootMotion = IsShow;
    }
    public virtual void Init()
    {
    }
    #region 战斗测试
    public virtual void SetLogic(AniState logic)
    {
    }
    #endregion
}
