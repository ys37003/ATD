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
        towerBasicDataDic = new Dictionary<E_TowerType, TowerBasicData>();

        towerBasicDataDic.Add(E_TowerType.MainTower, new TowerBasicData(E_TowerType.MainTower, 1000, 10, 0.2f, 5, 5, E_TileSize.Tile9));

        towerBasicDataDic.Add(E_TowerType.CannonTower1, new TowerBasicData(E_TowerType.CannonTower1, 100, 40, 2, 9, 9, E_TileSize.Tile1));
        towerBasicDataDic.Add(E_TowerType.CannonTower2, new TowerBasicData(E_TowerType.CannonTower2, 200, 50, 2, 9, 9, E_TileSize.Tile1));
        towerBasicDataDic.Add(E_TowerType.CannonTower3, new TowerBasicData(E_TowerType.CannonTower3, 300, 40, 2, 9, 9, E_TileSize.Tile4));

        towerBasicDataDic.Add(E_TowerType.LaserTower1, new TowerBasicData(E_TowerType.LaserTower1, 100, 7, 0.2f, 6, 5, E_TileSize.Tile1));
        towerBasicDataDic.Add(E_TowerType.LaserTower2, new TowerBasicData(E_TowerType.LaserTower2, 100, 7, 0.2f, 7, 6, E_TileSize.Tile4));
        towerBasicDataDic.Add(E_TowerType.LaserTower3, new TowerBasicData(E_TowerType.LaserTower3, 100, 7, 0.2f, 9, 7, E_TileSize.Tile9));

        towerBasicDataDic.Add(E_TowerType.FlameTower1, new TowerBasicData(E_TowerType.FlameTower1, 200, 3, 0.2f, 5, 5, E_TileSize.Tile4));
        towerBasicDataDic.Add(E_TowerType.FlameTower2, new TowerBasicData(E_TowerType.FlameTower2, 300, 5, 0.2f, 7, 7, E_TileSize.Tile4));
        towerBasicDataDic.Add(E_TowerType.FlameTower3, new TowerBasicData(E_TowerType.FlameTower3, 400, 5, 0.2f, 5, 5, E_TileSize.Tile9));

        towerBasicDataDic.Add(E_TowerType.AssistTower1, new TowerBasicData(E_TowerType.AssistTower1, 150, 0, 0, 0, 0, E_TileSize.Tile4));
        towerBasicDataDic.Add(E_TowerType.AssistTower2, new TowerBasicData(E_TowerType.AssistTower2, 150, 0, 0, 0, 0, E_TileSize.Tile4));
        towerBasicDataDic.Add(E_TowerType.AssistTower3, new TowerBasicData(E_TowerType.AssistTower3, 150, 0, 0, 0, 0, E_TileSize.Tile4));

        towerBasicDataDic.Add(E_TowerType.DefenseTower1, new TowerBasicData(E_TowerType.DefenseTower1, 500, 0, 0, 0, 0, E_TileSize.Tile4));
        towerBasicDataDic.Add(E_TowerType.DefenseTower2, new TowerBasicData(E_TowerType.DefenseTower2, 700, 0, 0, 0, 0, E_TileSize.Tile4));
        towerBasicDataDic.Add(E_TowerType.DefenseTower3, new TowerBasicData(E_TowerType.DefenseTower3, 700, 9, 0, 0, 0, E_TileSize.Tile4));
    }

    public TowerBasicData GetTowerBasicData(E_TowerType type)
    {
        if (towerBasicDataDic.ContainsKey(type))
            return new TowerBasicData(towerBasicDataDic[type]);

        Debug.LogError("Find Not TowerBasicData : " + type.ToString());
        return new TowerBasicData(E_TowerType.BasicTower, 1, 0, 0, 0, 0, E_TileSize.Tile1);
    }
}