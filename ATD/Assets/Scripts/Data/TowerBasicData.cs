public struct TowerBasicData
{
    public E_TowerType Type;
    public float Hp;
    public float Atk;
    public float Speed;
    public float Range;
    public float Area;
    public E_TileSize Size;

    public TowerBasicData(TowerBasicData data)
    {
        Type = data.Type;
        Hp = data.Hp;
        Atk = data.Atk;
        Speed = data.Speed;
        Range = data.Range;
        Area = data.Area;
        Size = data.Size;
    }

    public TowerBasicData(E_TowerType type, float hp, float atk, float speed, float range, float area, E_TileSize size)
    {
        Type = type;
        Hp = hp;
        Atk = atk;
        Speed = speed;
        Range = range;
        Area = area;
        Size = size;
    }
}