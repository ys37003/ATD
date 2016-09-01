using UnityEngine;

public class LaserAttack1 : ColliderAttack
{
    public override void SetData(TowerBasicData data)
    {
        base.SetData(data);

        GetComponent<CircleCollider2D>().radius = 0.5f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        Monster monster = col.GetComponent<Monster>();

        if (monster != null && AttackedMonsterList.Count == 0)
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