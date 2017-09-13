public delegate void HitObstacle (bool hit);

public static class EventManager
{
    public static event HitObstacle OnObstacleHit;

    public static void ObstacleHit (bool hit)
    {
        OnObstacleHit (hit);
    }
}
