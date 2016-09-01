using UnityEngine;
using System.Collections.Generic;

public class TowerManager : MonoBehaviour
{
    private static TowerManager instance;
    public  static TowerManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(TowerManager)) as TowerManager;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("TowerManager");
                instance = obj.AddComponent<TowerManager>() as TowerManager;
            }

            return instance;
        }
    }

    [SerializeField]
    private Tower MainTower = null;

    void Start()
    {
        Tile.Position pos = new Tile.Position(12, 18);
        TileManager.Instance.CanBuild(pos);
        BuildTower(pos, E_TowerType.MainTower);
    }

    public Tower GetMainTower()
    {
        return MainTower;
    }

    public void BuildTower(Tile.Position pos, E_TowerType type)
    {
        Tower tower = ObjectPoolManager.Instance.GetTower(type);

        if (tower.Type == E_TowerType.BasicTower)
            return;

        if (tower.Type == E_TowerType.MainTower)
            MainTower = tower;

        tower.transform.parent = transform;
        tower.transform.localPosition = TileManager.Instance.GetTIleRealPosision(pos);
        tower.SetData(TowerDataManager.Instance.GetTowerBasicData(type), pos);
        tower.OnDestroyTower = TileManager.Instance.OnDestroyTower;
        tower.OnDestroyTower += (p) => { ObjectPoolManager.Instance.SetTower(tower); };
        tower.SetActive(true);
    }
}