public delegate void ChangePassiveAmount (int amount);
public delegate void ChangeScore (int score, bool isCaller);
public delegate void ChangeSpeed (float speed);
public delegate void HitObstacle (bool hit);

public static class EventManager
{
    public static event ChangePassiveAmount OnPassiveAmountChanged;
    public static event ChangeScore OnScoreChanged;
    public static event ChangeSpeed OnSpeedChanged;
    public static event HitObstacle OnObstacleHit;

    public static void PassiveAmountChanged (int amount)
    {
        OnPassiveAmountChanged (amount);
    }

    public static void ScoreChanged (int score, bool isCaller)
    {
        OnScoreChanged (score, isCaller);
    }

    public static void SpeedChanged (float speed)
    {
        OnSpeedChanged (speed);
    }

    public static void ObstacleHit (bool hit)
    {
        OnObstacleHit (hit);
    }
}
