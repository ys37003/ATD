using UnityEngine;
using System.Collections;

public class LaserTower1 : Tower
{
    [SerializeField] private GameObject goEffect = null;
    [SerializeField] private Transform  tfEffect = null;
    [SerializeField] private Transform  tfLightning = null;
    [SerializeField] private SpriteRenderer srLightning = null;

    protected override void AttackStart()
    {
        base.AttackStart();
        goEffect.SetActive(true);

        StopCoroutine("LightningAnimation");
        StartCoroutine("LightningAnimation");
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
        StopCoroutine("LightningAnimation");
    }

    private void FollowTarget()
    {
        tfEffect.LookAt(Target.transform);
        if (Type == E_TowerType.LaserTower1)
            tfLightning.localScale = new Vector3(Vector3.Distance(tfLightning.position, Target.transform.position) * 0.33f, 1, 1);
    }

    IEnumerator LightningAnimation()
    {
        bool reverse = false;
        while(true)
        {
            if(reverse)
            {
                srLightning.color -= Color.black * Time.deltaTime * 7;

                if(srLightning.color.a <= 0)
                {
                    reverse = false;
                    srLightning.sprite = Resources.Load<Sprite>(string.Format("Effect/Laser/Laser1/Lightning1-{0}", Random.Range(1, 3)));
                }
            }
            else
            {
                srLightning.color += Color.black * Time.deltaTime * 7;

                if (srLightning.color.a >= 1)
                {
                    reverse = true;
                }
            }
            yield return null;
        }
    }
}