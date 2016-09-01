using UnityEngine;
using System.Collections;

public class TowerInfo : MonoBehaviour
{
    [SerializeField] private UIButton  btn = null;
    [SerializeField] private UISprite  sprite = null;
    [SerializeField] private UILabel   labelCost = null;

    private TowerSimpleData data;
    private Tower target;

    public System.Action<TowerSimpleData> OnClickBtn;

    void Awake()
    {
        EventDelegate.Add(btn.onClick, onClickBtn);
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetData(TowerSimpleData data, Tower target)
    {
        this.data = data;
        this.target = target;
        sprite.spriteName = data.TowerType.ToString();
        labelCost.text = data.Cost.ToString();
    }

    private void onClickBtn()
    {
        switch (data.TowerType)
        {
            case E_TowerType.CannonTower1:
            case E_TowerType.LaserTower1:
            case E_TowerType.FlameTower1:
            case E_TowerType.AssistTower1:
            case E_TowerType.DefenseTower1:
                break;
            default:
                {
                    if (target != null && ShopManager.Instance.buyTower(data) && TileManager.Instance.CanUpgrade(target.Size, data.TowerSize, target.Pos))
                        ChangeTower();

                    return;
                }
        }

        if (OnClickBtn != null)
            OnClickBtn(data);
    }

    private void ChangeTower()
    {
        Tile.Position pos = target.Pos;
        target.DestoryTower();
        TileManager.Instance.CanBuild(pos);
        ShopManager.Instance.OnUpdateGold(-data.Cost);
        TowerManager.Instance.BuildTower(pos, data.TowerType);
        ShopManager.Instance.InitShop(E_TowerType.BasicTower);
    }
}