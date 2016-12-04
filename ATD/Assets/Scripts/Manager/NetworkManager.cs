using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using LitJson;

public class NetworkManager : MonoBehaviour
{
    private static NetworkManager instance;
    public  static NetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(NetworkManager)) as NetworkManager;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("NetworkManager");
                instance = obj.AddComponent<NetworkManager>() as NetworkManager;
            }

            return instance;
        }
    }

    private Dictionary<E_TowerType, List<TowerSimpleData>> towerCostDIc = new Dictionary<E_TowerType, List<TowerSimpleData>>();
    private Dictionary<E_MonsterType, MonsterData> monsterDic = new Dictionary<E_MonsterType, MonsterData>();
    private Dictionary<E_TowerType, TowerBasicData> towerBasicDataDic = new Dictionary<E_TowerType, TowerBasicData>();
    private List<ScoreData> scoreDataList = new List<ScoreData>();

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    public void Init()
    {
        StartCoroutine("GetShopData");
        StartCoroutine("GetMonsterData");
        StartCoroutine("GetTowerData");
        StartCoroutine("GetScoreData");
    }

    public Dictionary<E_TowerType, List<TowerSimpleData>> GetShopDataDic()
    {
        return new Dictionary<E_TowerType, List<TowerSimpleData>>(towerCostDIc);
    }

    public Dictionary<E_MonsterType, MonsterData> GetMonsterDataDic()
    {
        return new Dictionary<E_MonsterType, MonsterData>(monsterDic);
    }

    public Dictionary<E_TowerType, TowerBasicData> GetTowerDataDic()
    {
        return new Dictionary<E_TowerType, TowerBasicData>(towerBasicDataDic);
    }

    public List<ScoreData> GetScoreDataList()
    {
        return new List<ScoreData>(scoreDataList);
    }

    IEnumerator GetShopData()
    {
        WWW www = new WWW("localhost:8080/datas/shop");

        yield return www;

        JsonData data = JsonMapper.ToObject(www.text);

        towerCostDIc.Clear();
        towerCostDIc.Add(E_TowerType.BasicTower, new List<TowerSimpleData>());
        towerCostDIc.Add(E_TowerType.CannonTower1, new List<TowerSimpleData>());
        towerCostDIc.Add(E_TowerType.LaserTower1, new List<TowerSimpleData>());
        towerCostDIc.Add(E_TowerType.FlameTower1, new List<TowerSimpleData>());
        towerCostDIc.Add(E_TowerType.AssistTower1, new List<TowerSimpleData>());
        towerCostDIc.Add(E_TowerType.DefenseTower1, new List<TowerSimpleData>());
        towerCostDIc.Add(E_TowerType.DefenseTower2, new List<TowerSimpleData>());

        for (int i = 0; i < data.Count; ++i)
        {
            TowerSimpleData tsd = new TowerSimpleData
            (
                (E_TowerType)(int)data[i]["tower_type"],
                (E_TileSize)(int)data[i]["tile_size"],
                (int)data[i]["cost"]
            );

            switch (tsd.TowerType)
            {
                case E_TowerType.CannonTower1:
                case E_TowerType.LaserTower1:
                case E_TowerType.FlameTower1:
                //case E_TowerType.AssistTower1:
                case E_TowerType.DefenseTower1:
                    towerCostDIc[E_TowerType.BasicTower].Add(tsd);
                    break;
                case E_TowerType.CannonTower2:
                case E_TowerType.CannonTower3:
                    towerCostDIc[E_TowerType.CannonTower1].Add(tsd);
                    break;
                //case E_TowerType.LaserTower2:
                case E_TowerType.LaserTower3:
                    towerCostDIc[E_TowerType.LaserTower1].Add(tsd);
                    break;
                case E_TowerType.FlameTower2:
                case E_TowerType.FlameTower3:
                    towerCostDIc[E_TowerType.FlameTower1].Add(tsd);
                    break;
                case E_TowerType.AssistTower2:
                case E_TowerType.AssistTower3:
                    towerCostDIc[E_TowerType.AssistTower1].Add(tsd);
                    break;
                case E_TowerType.DefenseTower2:
                    towerCostDIc[E_TowerType.DefenseTower1].Add(tsd);
                    break;
                //case E_TowerType.DefenseTower3:
                //    towerCostDIc[E_TowerType.DefenseTower2].Add(tsd);
                //    break;
                default:
                    break;
            }
        }

        Debug.Log("load");
    }

    IEnumerator GetMonsterData()
    {
        WWW www = new WWW("localhost:8080/datas/monster");

        yield return www;

        JsonData data = JsonMapper.ToObject(www.text);

        monsterDic.Clear();
        for(int i = 0; i< data.Count; ++i)
        {
            MonsterData md = new MonsterData
            (
                (E_MonsterType)(int)data[i]["monster_type"],
                float.Parse(data[i]["hp"].ToString()),
                float.Parse(data[i]["atk"].ToString()),
                float.Parse(data[i]["atk_speed"].ToString()),
                float.Parse(data[i]["move_speed"].ToString()),
                float.Parse(data[i]["area"].ToString()),
                (int)data[i]["drop_gold"]
            );

            monsterDic.Add(md.Type, md);
        }

        Debug.Log("load");
    }

    IEnumerator GetTowerData()
    {
        WWW www = new WWW("localhost:8080/datas/tower");

        yield return www;

        JsonData data = JsonMapper.ToObject(www.text);

        towerBasicDataDic.Clear();
        for (int i = 0; i < data.Count; ++i)
        {
            TowerBasicData tbd = new TowerBasicData
            (
                (E_TowerType)(int)data[i]["tower_type"],
                float.Parse(data[i]["hp"].ToString()),
                float.Parse(data[i]["atk"].ToString()),
                float.Parse(data[i]["speed"].ToString()),
                float.Parse(data[i]["range"].ToString()),
                float.Parse(data[i]["area"].ToString()),
                (E_TileSize)(int)data[i]["tile_size"]
            );

            towerBasicDataDic.Add(tbd.Type, tbd);
        }

        Debug.Log("load");
    }

    IEnumerator GetScoreData()
    {
        WWW www = new WWW("localhost:8080/rank");

        yield return www;

        JsonData data = JsonMapper.ToObject(www.text);

        scoreDataList.Clear();
        for(int i = 0; i< data.Count; ++i)
        {
            ScoreData sd = new ScoreData
            (
                data[i]["name"].ToString(),
                (int)data[i]["score"]
            );

            scoreDataList.Add(sd);
        }
    }
}