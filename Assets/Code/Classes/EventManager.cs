public delegate void ChangeMenuState (MenuStates state);
public delegate void ChangeGameState (GameStates state);
public delegate void ChangePassiveAmount (int amount);
public delegate void ChangeScore (int score, bool isCaller);
public delegate void ChangeSpeed (float speed);
public delegate void HitObstacle (bool hit);

public static class EventManager
{
    public static event ChangeMenuState OnMenuStateChanged;
    public static event ChangeGameState OnGameStateChanged;
    public static event ChangePassiveAmount OnPassiveAmountChanged;
    public static event ChangeScore OnScoreChanged;
    public static event ChangeSpeed OnSpeedChanged;
    public static event HitObstacle OnObstacleHit;

    public static void MenuStateChanged (MenuStates state)
    {
        if (OnMenuStateChanged != null)
            OnMenuStateChanged (state);
    }

    public static void GameStateChanged (GameStates state)
    {
        if (OnGameStateChanged != null)
            OnGameStateChanged (state);
    }

    public static void PassiveAmountChanged (int amount)
    {
        if (OnPassiveAmountChanged != null)
            OnPassiveAmountChanged (amount);
    }

    public static void ScoreChanged (int score, bool isCaller)
    {
        if (OnScoreChanged != null)
            OnScoreChanged (score, isCaller);
    }

    public static void SpeedChanged (float speed)
    {
        if (OnSpeedChanged != null)
            OnSpeedChanged (speed);
    }

    public static void ObstacleHit (bool hit)
    {
        if (OnObstacleHit != null)
            OnObstacleHit (hit);
    }
}
