public class TowerSimpleData
{
    public E_TowerType TowerType;
    public E_TileSize TowerSize;
    public int Cost;

    public TowerSimpleData(TowerSimpleData data)
    {
        TowerType = data.TowerType;
        TowerSize = data.TowerSize;
        Cost      = data.Cost;
    }

    public TowerSimpleData(E_TowerType type, E_TileSize size, int cost)
    {
        TowerType = type;
        TowerSize = size;
        Cost = cost;
    }
}