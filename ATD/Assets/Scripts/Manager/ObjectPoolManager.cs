using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance;
    public  static ObjectPoolManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(ObjectPoolManager)) as ObjectPoolManager;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("ObjectPoolManager");
                instance = obj.AddComponent<ObjectPoolManager>() as ObjectPoolManager;
            }

            return instance;
        }
    }

    [SerializeField] Transform tfTower = null;
    [SerializeField] Transform tfMonster = null;

    private List<Tower> TowerList = new List<Tower>();
    private List<Monster> MonsterList = new List<Monster>();

    private Dictionary<E_TowerType, Tower> TowerDic = new Dictionary<E_TowerType, Tower>();
    private Dictionary<E_MonsterType, Monster> MonsterDic = new Dictionary<E_MonsterType, Monster>();

    void Awake()
    {
        Transform tfTowerCopy = transform.FindChild("CopiedTower");
        Transform tfMonsterCopy = transform.FindChild("CopiedMonster");

        for(int i = 0; i < tfTowerCopy.childCount; ++i)
        {
            Tower tower = tfTowerCopy.GetChild(i).GetComponent<Tower>();
            TowerDic.Add(tower.Type, tower);
        }

        for(int i = 0; i < tfMonsterCopy.childCount; ++i)
        {
            Monster monster = tfMonsterCopy.GetChild(i).GetComponent<Monster>();
            MonsterDic.Add(monster.Type, monster);
        }
    }

    public Tower GetTower(E_TowerType type)
    {
        Tower tower = TowerList.Find((temp) =>
        {
            return temp.Data.Type == type;
        });

        if (tower == null)
        {
            tower = Instantiate(TowerDic[type]);
        }

        TowerList.Remove(tower);

        return tower;
    }

    public Monster GetMonster(E_MonsterType type)
    {
        Monster monster = MonsterList.Find((temp) =>
        {
            return temp.Data.Type == type;
        });

        if (monster == null)
        {
            monster = Instantiate(MonsterDic[type]);
        }

        MonsterList.Remove(monster);

        return monster;
    }

    public void SetTower(Tower tower)
    {
        TowerList.Add(tower);
        tower.transform.parent = tfTower;
        tower.SetActive(false);
    }

    public void SetMonster(Monster monster)
    {
        MonsterList.Add(monster);
        monster.transform.parent = tfMonster;
        monster.SetActive(false);
    }
}