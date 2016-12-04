using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MonsterRespawn : MonoBehaviour
{
    public List<MonsterRespawnData> RespawnDataList = new List<MonsterRespawnData>();

    public bool isEnd = false;

    void Start()
    {
        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        if (RespawnDataList.Count == 0)
            yield break;

        MonsterRespawnData data = RespawnDataList[0];
        int listCount = 0;
        int dataCount = 0;
        Monster monster = null;

        var startDelay = new WaitForSeconds(data.startDelay);
        var delay = new WaitForSeconds(data.Delay);

        while (true)
        {
            if(dataCount == 0)
            {
                yield return startDelay;
            }

            monster = ObjectPoolManager.Instance.GetMonster(data.Type);
            monster.transform.parent = transform;
            monster.transform.localPosition = Vector3.zero;
            monster.SetData(MonsterDataManager.Instance.GetMonsterData(data.Type));
            monster.SetActive(true);
            dataCount++;

            yield return delay;

            if (dataCount == data.Count)
            {
                dataCount = 0;
                listCount++;

                if (RespawnDataList.Count == listCount)
                {
                    isEnd = true;
                    break;
                }

                data = RespawnDataList[listCount];
            }
        }
    }
}