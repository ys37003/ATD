using UnityEngine;
using System.Collections;
using Spine.Unity;

public enum E_MonsterType
{
    A,
    B,
    C
}

public enum E_MonsterState
{
    Move,
    Attack,
    Dead,
}

public class Monster : MonoBehaviour
{
    public float Hp     { get; private set; }
    public float Atk    { get; private set; }
    public float Speed  { get; private set; }
    public float MoveSpeed  { get; private set; }
    public float Area   { get; private set; }

    [SerializeField] private SkeletonAnimation skltAnimation = null;

    public E_MonsterState CurrentState { get; private set; }
    public MonsterData Data { get; private set; }
    public Tower Target { get; private set; }

    public E_MonsterType Type;

    void OnEnable()
    {
        Init();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetData(MonsterData data)
    {
        Data = data;
        Hp = Data.Hp;
        Atk = Data.Atk;
        Speed = Data.Speed;
        MoveSpeed = Data.MoveSpeed;
        Area = Data.Area;
    }

    void Init()
    {
        CurrentState = E_MonsterState.Move;
        Target = TowerManager.Instance.GetMainTower();

        ChangeAni("walk", true);

        StopCoroutine("FSM");
        StartCoroutine("FSM");

    }

    IEnumerator FSM()
    {
        var attackDelay = new WaitForSeconds(Speed);

        while (true)
        {
            if (Target == null || Target.CurrentState == E_TowerState.Destroy)
            {
                Target = TowerManager.Instance.GetMainTower();
                CurrentState = E_MonsterState.Move;
                ChangeAni("walk", true);
            }

            skltAnimation.skeleton.FlipX = transform.position.x - Target.TfCenter.position.x >= 0;

            switch (CurrentState)
            {
                case E_MonsterState.Move:
                    {
                        if (Vector3.Distance(transform.position, Target.TfCenter.position) < Area)
                        {
                            CurrentState = E_MonsterState.Attack;
                            ChangeAni("attack", true);
                            break;
                        }

                        transform.position = Vector3.MoveTowards(transform.position, Target.TfCenter.position, Time.deltaTime * MoveSpeed);
                    }
                    break;

                case E_MonsterState.Attack:
                    {
                        transform.Translate(Vector3.up * 0.00001f);
                        Target.Damaged(Atk);
                        yield return attackDelay;
                    }
                    break;

                case E_MonsterState.Dead:
                    {
                        ChangeAni("dead", false);
                        yield return new WaitForSpineAnimationComplete(skltAnimation.state.GetCurrent(0));
                        ObjectPoolManager.Instance.SetMonster(this);
                        ShopManager.Instance.OnUpdateGold(Data.RewardGold);
                    }
                    break;
            }
            yield return null;
        }
    }

    public void Damaged(float damage)
    {
        Hp -= damage;

        if (Hp <= 0)
            CurrentState = E_MonsterState.Dead;
    }

    private void ChangeAni(string animationName, bool loop)
    {
        skltAnimation.loop = loop;
        skltAnimation.AnimationName = animationName;
        skltAnimation.Initialize(true);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Tower tower = col.GetComponent<Tower>();
        if (tower != null)
        {
            Target = tower;
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Tower tower = col.GetComponent<Tower>();
        if (tower != null && Target == tower)
        {
            Target = null;
        }
    }
}