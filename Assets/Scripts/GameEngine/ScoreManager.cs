public class ScoreManager
{
    public int Score { get; private set; }

    public void IncreaseScore()
    {
        Score++;
    }

    public void DecreaseScore()
    {
        if (Score > 0)
        {
            Score--;
        }
    }

    public void ResetScore()
    {
        Score = 0;
    }
}