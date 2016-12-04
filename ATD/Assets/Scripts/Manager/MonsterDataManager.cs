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
        monsterDic = NetworkManager.Instance.GetMonsterDataDic();
    }

    public MonsterData GetMonsterData(E_MonsterType type)
    {
        if (monsterDic.ContainsKey(type))
            return new MonsterData(monsterDic[type]);

        Debug.LogError("Find Not monsterDic : " + type.ToString());
        return new MonsterData(E_MonsterType.A, 1, 1, 1, 1, 1, 0);
    }
}