using UnityEngine;

public class TowerAttackArea : MonoBehaviour
{
    public CircleCollider2D CircleCol;
    public Monster target { get; private set; }

    public void SetActive(bool active)
    {
        if(active)
        {
            target = null;
        }

        gameObject.SetActive(active);
    }

    void OnTriggerStay2D(Collider2D col)
    {
        // 타겟이 죽지않았으면 종료
        if (target != null && target.CurrentState != E_MonsterState.Dead)
            return;

        // 타겟이 죽었다면 타겟 초기화
        if (target != null && target.CurrentState == E_MonsterState.Dead)
            target = null;

        Monster monster = col.GetComponent<Monster>();

        if (monster == null)
            return;

        // 몬스터가 죽었다면 종료
        if (monster.CurrentState == E_MonsterState.Dead)
            return;

        // 몬스터가 죽지 않았다면 타겟으로 지정
        target = monster;
    }
}