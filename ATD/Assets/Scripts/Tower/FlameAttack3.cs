using UnityEngine;
using System.Collections;

public class FlameAttack3 : ColliderAttack
{
    public override void SetData(TowerBasicData data)
    {
        base.SetData(data);

        GetComponent<CircleCollider2D>().radius = data.Area;
    }
}