using UnityEngine;
using System.Collections.Generic;

public class TowerDataManager : MonoBehaviour
{
    private static TowerDataManager instance;
    public  static TowerDataManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(TowerDataManager)) as TowerDataManager;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("TowerDataManager");
                instance = obj.AddComponent<TowerDataManager>() as TowerDataManager;
            }

            return instance;
        }
    }

    private Dictionary<E_TowerType, TowerBasicData> towerBasicDataDic;

    void Awake()
    {
        towerBasicDataDic = NetworkManager.Instance.GetTowerDataDic();
    }

    public TowerBasicData GetTowerBasicData(E_TowerType type)
    {
        if (towerBasicDataDic.ContainsKey(type))
            return new TowerBasicData(towerBasicDataDic[type]);

        Debug.LogError("Find Not TowerBasicData : " + type.ToString());
        return new TowerBasicData(E_TowerType.BasicTower, 1, 0, 0, 0, 0, E_TileSize.Tile1);
    }
}