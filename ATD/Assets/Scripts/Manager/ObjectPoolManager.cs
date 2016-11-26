using UnityEngine;
using System.Collections.Generic;

public class ObjectPoolManager : MonoBehaviour
{
    private static ObjectPoolManager instance;
    /// <summary>
    /// 싱글톤
    /// </summary>
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

    [SerializeField] Transform tfTower   = null;
    [SerializeField] Transform tfMonster = null;

    /// <summary>
    /// 타워 오브젝트풀
    /// </summary>
    private List<Tower> TowerList = new List<Tower>();

    /// <summary>
    /// 몬스터 오브젝트풀
    /// </summary>
    private List<Monster> MonsterList = new List<Monster>();

    private Dictionary<E_TowerType, Tower>     TowerDic   = new Dictionary<E_TowerType, Tower>();
    private Dictionary<E_MonsterType, Monster> MonsterDic = new Dictionary<E_MonsterType, Monster>();

    void Awake()
    {
        Transform tfTowerCopy = transform.FindChild("CopiedTower");
        Transform tfMonsterCopy = transform.FindChild("CopiedMonster");

        /*
            하이어라키에 미리 배치되어있던 타워와 몬스터를
            오브젝트풀이 비었을때 생성하기위해 사용 할 Dictionary에 저장
        */
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

    /// <summary>
    /// 오브젝트풀에서 새로운 타워를 얻는다.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public Tower GetTower(E_TowerType type)
    {
        // 오브젝트풀에 해당 타입의 타워가 있다면 tower에 반환
        Tower tower = TowerList.Find((temp) =>
        {
            return temp.Data.Type == type;
        });

        // 오브젝트풀에 해당 타입의 타워가 없다면 새로 생성
        if (tower == null)
        {
            tower = Instantiate(TowerDic[type]);
        }
        // 있다면 해당 타워를 사용하기 위해서 오브젝트풀에서 제거
        else
        {
            TowerList.Remove(tower);
        }

        return tower;
    }

    /// <summary>
    /// 위의 GetTower 함수와 같은 역할
    /// </summary>
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

    /// <summary>
    /// Tower의 사용이 종료되면
    /// 오브젝트풀에 다시 넣어주고 Active를 끈다.
    /// </summary>
    /// <param name="tower"></param>
    public void SetTower(Tower tower)
    {
        TowerList.Add(tower);
        tower.transform.parent = tfTower;
        tower.SetActive(false);
    }

    /// <summary>
    /// 위의 SetTower 함수와 같은 역할
    /// </summary>
    public void SetMonster(Monster monster)
    {
        MonsterList.Add(monster);
        monster.transform.parent = tfMonster;
        monster.SetActive(false);
    }
}