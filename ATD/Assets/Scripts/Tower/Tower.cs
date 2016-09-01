using UnityEngine;
using System.Collections;
using Spine.Unity;

public enum E_TowerType
{
    MainTower,

    BasicTower,

    CannonTower1,
    CannonTower2,
    CannonTower3,

    LaserTower1,
    LaserTower2,
    LaserTower3,

    FlameTower1,
    FlameTower2,
    FlameTower3,

    AssistTower1,
    AssistTower2,
    AssistTower3,

    DefenseTower1,
    DefenseTower2,
    DefenseTower3,
}

public enum E_TowerState
{
    Idle,
    Attack,
    Destroy,
}

public class Tower : MonoBehaviour
{
    public float Hp        { get; private set; }
    public float Atk       { get; private set; }
    public float Speed     { get; private set; }
    public float Range     { get; private set; }
    public float Area      { get; private set; }
    public E_TileSize Size { get; private set; }

    public E_TowerState     CurrentState    { get; private set; }
    public TowerBasicData   Data            { get; private set; }
    public Monster          Target          { get; private set; }

    public E_TowerType      Type;
    public Tile.Position    Pos         { get; private set; }
    public Transform        TfCenter    { get; private set; }

    public System.Action<Tile.Position> OnDestroyTower;

    [SerializeField] private SkeletonAnimation  skltAnimation   = null;
    [SerializeField] private SpriteRenderer     spRenderer      = null;
    [SerializeField] private TowerAttackArea    attackArea      = null;
    [SerializeField] private ColliderAttack     colliderAttack  = null;

    void Awake()
    {
             if (skltAnimation != null) TfCenter = skltAnimation.transform;
        else if (spRenderer    != null) TfCenter = spRenderer.transform;
    }

    void OnEnable()
    {
        Init();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetData(TowerBasicData data, Tile.Position pos)
    {
        Data = data;
        Hp = Data.Hp;
        Atk = Data.Atk;
        Speed = Data.Speed;
        Range = Data.Range;
        Area = Data.Area;
        Size = Data.Size;

        colliderAttack.SetData(Data);
        Pos = pos;

             if (skltAnimation != null) skltAnimation.GetComponent<Renderer>().sortingOrder = GetSortingtOrder(Pos);
        else if (spRenderer    != null) spRenderer.sortingOrder = GetSortingtOrder(Pos);
    }

    public void Damaged(float damage)
    {
        Hp -= damage;

        if (Hp <= 0)
            CurrentState = E_TowerState.Destroy;
    }

    public void UnBuildTower()
    {
        CurrentState = E_TowerState.Destroy;
    }

    private void Init()
    {
        Target = null;
        CurrentState = E_TowerState.Idle;
        attackArea.CircleCol.radius = Area;
        attackArea.SetActive(true);

        ChangeAni("normal", true);

        StopCoroutine("FSM");
        StartCoroutine("FSM");
    }

    private IEnumerator FSM()
    {
        while (true)
        {
            switch (CurrentState)
            {
                case E_TowerState.Idle:
                    {
                        Idle();
                    }
                    break;

                case E_TowerState.Attack:
                    {
                        Attack();

                        if (Target.CurrentState == E_MonsterState.Dead || Vector3.Distance(TfCenter.position, Target.transform.position) > Range)
                        {
                            colliderAttack.RemoveTarget(Target);
                            Target = colliderAttack.RemainTarget();
                            if (Target == null)
                            {
                                AttackEnd();
                            }
                        }
                    }
                    break;

                case E_TowerState.Destroy:
                    {
                        AttackEnd();
                        if (Type != E_TowerType.MainTower)
                            ChangeAni("hit", false);
                        yield return new WaitForSeconds(1);
                        DestoryTower();
                    }
                    break;
            }

            yield return null;
        }
    }
    
    protected virtual void Idle()
    {
        if (Target == null && attackArea.target != null)
        {
            AttackStart();
        }
    }

    protected virtual void AttackStart()
    {
        Target = attackArea.target;
        CurrentState = E_TowerState.Attack;
        ChangeAni("attack", true);
        attackArea.SetActive(false);
    }

    protected virtual void Attack()
    {
        if (CurrentState != E_TowerState.Attack)
            return;
    }

    protected virtual void AttackEnd()
    {
        CurrentState = E_TowerState.Idle;
        ChangeAni("normal", true);
        attackArea.SetActive(true);
    }

    public virtual void DestoryTower()
    {
        if (OnDestroyTower != null)
            OnDestroyTower(Pos);
    }

    protected void ChangeAni(string animationName, bool loop)
    {
        if (skltAnimation == null)
            return;

        skltAnimation.loop = loop;
        skltAnimation.AnimationName = animationName;
        skltAnimation.Initialize(true);
    }

    private int GetSortingtOrder(Tile.Position pos)
    {
        return pos.x + pos.y * 100;
    }

    private void OnMouseDown()
    {
        ShopManager.Instance.InitShop(Data.Type, this);
    }
}