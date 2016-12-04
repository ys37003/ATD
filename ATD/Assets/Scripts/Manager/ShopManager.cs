using UnityEngine;
using System.Collections.Generic;

public class ShopManager : MonoBehaviour
{
    private static ShopManager instance;
    public  static ShopManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType(typeof(ShopManager)) as ShopManager;
            }

            if (instance == null)
            {
                GameObject obj = new GameObject("TowerShopManager");
                instance = obj.AddComponent<ShopManager>() as ShopManager;
            }

            return instance;
        }
    }

    private Dictionary<E_TowerType, List<TowerSimpleData>> TowerCostDIc = null;

    [SerializeField] private List<TowerInfo> towerinfoList = null;
    [SerializeField] private UIGrid grid = null;
    [SerializeField] private UILabel LabelGold = null;
    [SerializeField] private UnBuildTower unBuildTower = null;
    [SerializeField] private BackTowerList backTowerList = null;

    public TowerSimpleData SelectedTSD { get; private set; }
    public int Gold { get; private set; }

    void Awake()
    {
        TowerCostDIc = NetworkManager.Instance.GetShopDataDic();

        foreach (TowerInfo info in towerinfoList)
        {
            info.OnClickBtn += CursorManager.Instance.OnClickTowerInfo;
            info.OnClickBtn += TileManager.Instance.OnClickTowerInfo;
            info.OnClickBtn += onClickTowerInfo;
        }

        unBuildTower.OnClickBtn += () => { InitShop(E_TowerType.BasicTower); };
        backTowerList.OnClickBtn += () => { InitShop(E_TowerType.BasicTower); };
    }

    void Start()
    {
        InitShop(E_TowerType.BasicTower);
        OnUpdateGold(2000);
    }

    public void InitShop(E_TowerType type, Tower tower = null)
    {
        unBuildTower.Target = tower;
        if (type == E_TowerType.MainTower)
        {
            type = E_TowerType.BasicTower;
        }

        unBuildTower.SetActive(type != E_TowerType.BasicTower);
        backTowerList.SetActive(type != E_TowerType.BasicTower);

        if (TowerCostDIc.ContainsKey(type))
        {
            List<TowerSimpleData> tempList = TowerCostDIc[type];

            for (int i = 0; i < towerinfoList.Count; ++i)
            {
                if (i < tempList.Count)
                {
                    towerinfoList[i].SetData(tempList[i], tower);
                    towerinfoList[i].SetActive(true);
                }
                else
                {
                    towerinfoList[i].SetActive(false);
                }
            }
        }
        else
        {
            foreach (TowerInfo info in towerinfoList)
            {
                info.SetActive(false);
            }
        }

        grid.Reposition();
    }

    public bool buyTower(TowerSimpleData data = null)
    {
        TowerSimpleData temp = data == null ? SelectedTSD : data;

        bool buy = Gold - temp.Cost >= 0;
        if(!buy)
        {
            Debug.Log("Gold is Lack");
        }

        return buy;
    }

    public void OnUpdateGold(int addGold)
    {
        Gold += addGold;
        LabelGold.text = string.Format("Gold : {0}", Gold);
    }

    private void onClickTowerInfo(TowerSimpleData data)
    {
        SelectedTSD = data;
    }
}