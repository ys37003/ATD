public struct MonsterData
{
    public E_MonsterType Type;
    public float Hp;
    public float Atk;
    public float Speed;
    public float MoveSpeed;
    public float Area;
    public int RewardGold;

    public MonsterData(MonsterData data)
    {
        Type = data.Type;
        Hp = data.Hp;
        Atk = data.Atk;
        Speed = data.Speed;
        MoveSpeed = data.MoveSpeed;
        Area = data.Area;
        RewardGold = data.RewardGold;
    }

    public MonsterData(E_MonsterType type, float hp, float atk, float speed, float moveSpeed, float area, int rewardGold)
    {
        Type = type;
        Hp = hp;
        Atk = atk;
        Speed = speed;
        MoveSpeed = moveSpeed;
        Area = area;
        RewardGold = rewardGold;
    }
}