using UnityEngine;
using System.Collections;

public class CannonTower1 : Tower
{
    [SerializeField] private GameObject goCannon     = null;
    [SerializeField] private Transform  tfEffect     = null;
    [SerializeField] private Transform  tfEffect2    = null;
    [SerializeField] private ParticleSystem particle = null;
    [SerializeField] private Transform  tfCollider   = null;

    protected override void AttackStart()
    {
        base.AttackStart();
        goCannon.SetActive(true);

        StopCoroutine("CannonAnimation");
        StartCoroutine("CannonAnimation");

        if (tfEffect2 != null)
        {
            StopCoroutine("CannonAnimation2");
            StartCoroutine("CannonAnimation2");
        }
    }

    protected override void Attack()
    {
        base.Attack();
        tfCollider.transform.position = Target.transform.position;
    }

    protected override void AttackEnd()
    {
        base.AttackEnd();

        goCannon.SetActive(false);
        StopCoroutine("CannonAnimation");
        StopCoroutine("CannonAnimation2");
    }

    IEnumerator CannonAnimation()
    {
        while (true)
        {
            tfEffect.position = Vector3.MoveTowards(tfEffect.position, Target.transform.position, Time.deltaTime * 4);

            if(tfEffect.position == Target.transform.position)
            {
                if (particle != null)
                {
                    particle.transform.position = Target.transform.position;
                    StartCoroutine("Explosion");
                }
                yield return new WaitForSeconds(0.1f);
                tfEffect.localPosition = Vector3.zero;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator CannonAnimation2()
    {
        yield return new WaitForSeconds(0.2f);
        while (true)
        {
            tfEffect2.position = Vector3.MoveTowards(tfEffect2.position, Target.transform.position, Time.deltaTime * 4);

            if (tfEffect2.position == Target.transform.position)
            {
                yield return new WaitForSeconds(0.1f);
                tfEffect2.localPosition = Vector3.zero;
            }

            yield return new WaitForEndOfFrame();
        }
    }

    IEnumerator Explosion()
    {
        if (particle != null)
            particle.Play();

        while (particle.isPlaying)
            yield return null;
    }
}