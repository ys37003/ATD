using UnityEngine;

public class MainTower : Tower
{
    [SerializeField] private GameObject goEffect = null;

    protected override void AttackStart()
    {
        base.AttackStart();
        goEffect.SetActive(true);
    }

    protected override void AttackEnd()
    {
        base.AttackEnd();
        goEffect.SetActive(false);
    }
}