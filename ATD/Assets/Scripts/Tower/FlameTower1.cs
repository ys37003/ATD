using UnityEngine;

public class FlameTower1 : Tower
{
   [SerializeField] private GameObject goEffect = null;
   [SerializeField] private Transform  tfEffect = null;

    protected override void AttackStart()
    {
        base.AttackStart();
        goEffect.SetActive(true);
    }

    protected override void Attack()
    {
        base.Attack();

        FollowTarget();
    }

    protected override void AttackEnd()
    {
        base.AttackEnd();

        goEffect.SetActive(false);
    }

    private void FollowTarget()
    {
        tfEffect.LookAt(Target.transform);
    }
}