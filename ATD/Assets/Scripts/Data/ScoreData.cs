public struct ScoreData
{
    public string Name;
    public int Score;

    public ScoreData(ScoreData data)
    {
        Name = data.Name;
        Score = data.Score;
    }

    public ScoreData(string name, int score)
    {
        Name = name;
        Score = score;
    }
}
