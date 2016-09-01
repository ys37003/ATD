using UnityEngine;
using System.Collections.Generic;

public class MonsterDataManager : MonoBehaviour
{
    private static MonsterDataManager instance;
    public static MonsterDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(MonsterDataManager)) as MonsterDataManager;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("MonsterDataManager");
                instance = obj.AddComponent<MonsterDataManager>() as MonsterDataManager;
            }

            return instance;
        }
    }

    private Dictionary<E_MonsterType, MonsterData> monsterDic;

    void Awake()
    {
        monsterDic = new Dictionary<E_MonsterType, MonsterData>();

        monsterDic.Add(E_MonsterType.A, new MonsterData(E_MonsterType.A, 300, 5,  1,    1.5f, 2, 10));
        monsterDic.Add(E_MonsterType.B, new MonsterData(E_MonsterType.B, 500, 7,  0.5f, 1.2f, 2, 20));
        monsterDic.Add(E_MonsterType.C, new MonsterData(E_MonsterType.C, 700, 50, 3,    1.0f, 2, 30));
    }

    public MonsterData GetMonsterData(E_MonsterType type)
    {
        if (monsterDic.ContainsKey(type))
            return new MonsterData(monsterDic[type]);

        Debug.LogError("Find Not monsterDic : " + type.ToString());
        return new MonsterData(E_MonsterType.A, 1, 1, 1, 1, 1, 0);
    }
}