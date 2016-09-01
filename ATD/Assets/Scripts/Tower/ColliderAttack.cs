using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum E_AttackType
{
    MainTain,
    OwnShot,
}

public class ColliderAttack : MonoBehaviour
{
    protected List<Monster> AttackedMonsterList = new List<Monster>();

    protected float Atk;
    protected float speed;
    protected float range;

    void OnEnable()
    {
        StopCoroutine("FSM");
        StartCoroutine("FSM");
    }

    public virtual void SetData(TowerBasicData data)
    {
        Atk = data.Atk;
        speed = data.Speed;
        range = data.Range;
    }

    public void RemoveTarget(Monster target)
    {
        AttackedMonsterList.Remove(target);
    }

    public Monster RemainTarget()
    {
        if(AttackedMonsterList.Count > 0)
        {
            return AttackedMonsterList[0];
        }

        return null;
    }

    IEnumerator FSM()
    {
        var attackDelay = new WaitForSeconds(speed);
        while (true)
        {
            Attack();
            yield return attackDelay;
        }
    }

   protected virtual void Attack()
    {
        if (AttackedMonsterList.Count == 0)
            return;

        System.Action tempAction = null;

        foreach (Monster mon in AttackedMonsterList)
        {
            Monster tempMon = mon;
            tempMon.Damaged(Damage());

            if (tempMon.CurrentState == E_MonsterState.Dead)
            {
                tempAction += () =>
                {
                   AttackedMonsterList.Remove(tempMon);
                };
            }
        }

        if (tempAction != null)
            tempAction();
    }

    protected virtual float Damage()
    {
        return Atk;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Monster monster = col.GetComponent<Monster>();

        if (monster != null)
        {
            AttackedMonsterList.Add(monster);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        Monster monster = col.GetComponent<Monster>();

        if (monster != null)
        {
            AttackedMonsterList.Remove(monster);
        }
    }
}