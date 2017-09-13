public delegate void ChangeScore (int score, bool isCaller);
public delegate void HitObstacle (bool hit);

public static class EventManager
{
    public static event ChangeScore OnScoreChanged;
    public static event HitObstacle OnObstacleHit;

    public static void ScoreChanged (int score, bool isCaller)
    {
        OnScoreChanged (score, isCaller);
    }

    public static void ObstacleHit (bool hit)
    {
        OnObstacleHit (hit);
    }
}
