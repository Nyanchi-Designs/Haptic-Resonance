using UnityEngine;

public delegate void ChangeMenuState (MenuStates state);
public delegate void ChangeGameState (GameStates state);
public delegate void ChangePassiveAmount (int amount);
public delegate void ChangeScore (int score, bool isCaller);
public delegate void ChangeSpeed (bool reset, float speed, GameObject character);
public delegate void ChangeJumpHeight (bool reset, float jumpHeight, GameObject character);
public delegate void Push (Vector2 direction, float pushAmount, GameObject character);
public delegate void InversePolarity ();
public delegate void HitObstacle (bool hit);

public static class EventManager
{
    public static event ChangeMenuState OnMenuStateChanged;
    public static event ChangeGameState OnGameStateChanged;
    public static event ChangePassiveAmount OnPassiveAmountChanged;
    public static event ChangeScore OnScoreChanged;
    public static event ChangeSpeed OnSpeedChanged;
    public static event ChangeJumpHeight OnJumpHeightChanged;
    public static event Push OnPushed;
    public static event InversePolarity OnPolarityInversed;
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

    public static void SpeedChanged (bool reset, float speed, GameObject character)
    {
        if (OnSpeedChanged != null)
            OnSpeedChanged (reset, speed, character);
    }

    public static void ChangeJumpHeight (bool reset, float jumpHeight, GameObject character)
    {
        if (OnJumpHeightChanged != null)
            OnJumpHeightChanged (reset, jumpHeight, character);
    }

    public static void Push (Vector2 direction, float pushAmount, GameObject character)
    {
        if (OnPushed != null)
            OnPushed (direction, pushAmount, character);
    }

    public static void InversePolarity ()
    {
        if (OnPolarityInversed != null)
            OnPolarityInversed ();
    }

    public static void ObstacleHit (bool hit)
    {
        if (OnObstacleHit != null)
            OnObstacleHit (hit);
    }
}
