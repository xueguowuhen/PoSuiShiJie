/****************************************************
    文件：EnemyController.cs
	作者：#CreateAuthor#
    邮箱: 1785275942@qq.com
    日期：2024/6/18 9:50:5
	功能：敌人表现实体控制器
*****************************************************/

using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Controller
{
    public CharacterController characterController;
    private Dictionary<string, GameObject> fxDic = new Dictionary<string, GameObject>();
    public bool IsAiMove = false;
    private float TargetDis;

    //private float MoveTime = 1.3f; 
    //private float MoveTimeCount = 0;

    private Vector3 AiTargetDir;

    [SerializeField]
    //private EntityEnemy.Logic logic;
    private AniState aniState;
    public override void SetAction(int act)
    {
        base.SetAction(act);
        //没有动画
    }
    public override void SetVelocity(float Velocity)
    {
        animator.SetFloat("Blend", Velocity);
        //base.SetVelocity(Velocity, IsRun);

    }
    public void Init(GameObject Enemy)
    {
        characterController = Enemy.GetComponent<CharacterController>();
        //  moveDistance = 10;
    }
    public void Update()
    {
        //if (isMove)
        //{
        //    SetMove();
        //}
        if (IsAiMove)
        {
            characterController.Move(transform.forward * Time.deltaTime * 1.8f);
        }
        SetDir();
    }
    public override void SetMove()
    {
        if (IsAiMove)
        {
            return;
        }
        //Debug.Log("正常移动"); //受逻辑控制
        characterController.Move(transform.forward * Time.deltaTime * 1.8f);
    }
    public void AiSetMove(float dis, bool open = true)  //ai步行设置原理:过近会后撤 过远会 环绕靠近(未实现)
    {
        IsAiMove = open;
        TargetDis = dis;
    }
    //public override void SetTrans(double time, Vector3 Pos, Vector3 Rot)
    //{
    //    //transform.localEulerAngles = Rot;
    //}
    protected override void SetDir() //方向变动 驱动ismove 驱动setmove
    {
        if (isRos)
        {
            float angle = Vector2.SignedAngle(Dir, new Vector2(0, 1));
            eulerAngles = new Vector3(0, angle, 0);
            transform.localEulerAngles = eulerAngles;
            //Debug.Log("旋转方向");
        }
    }
    public override void SetLogic(AniState logic)
    {
        this.aniState = logic;
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
            //go = AssetLoaderSvc.instance.ABLoadPrefab(names, name);
            //go.transform.SetParent(transform, false);
            //fxDic.Add(name, go);
            //go.SetActive(true);
            //TimerSvc.Instance.AddTimeTask((int tid) =>
            //{
            //    go.SetActive(false);
            //}, destroy);
        }
    }
}