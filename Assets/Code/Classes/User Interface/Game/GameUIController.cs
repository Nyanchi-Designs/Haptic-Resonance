using UnityEngine;

[AddComponentMenu ("Extended/UI/Game/Game UI Controller")]
public class GameUIController : MonoBehaviour
{
    [Tooltip ("The screen responsible for handling the game loop behaviour.")]
    public GameObject GameScreen = null;
    [Tooltip ("The screen responsible for displaying the level failed behaviour.")]
    public GameObject LevelFailedScreen = null;
    [Tooltip ("The screen responsible for displaying the level complete behaviour.")]
    public GameObject LevelCompleteScreen = null;
    [Tooltip ("The screen responsible for displaying the level select behaviour.")]
    public GameObject LevelSelectScreen = null;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        EventManager.OnGameStateChanged += GameStateChanged;
    }

    private void GameStateChanged (GameStates state)
    {
        switch (state)
        {
            case GameStates.GameLoop:
                ChangeScreen (GameScreen);
                break;
            case GameStates.LevelFailed:
                ChangeScreen (LevelFailedScreen);
                break;
            case GameStates.LevelComplete:
                ChangeScreen (LevelCompleteScreen);
                break;
            case GameStates.LevelSelect:
                ChangeScreen (LevelSelectScreen);
                break;
        }
    }

    private void ChangeScreen (GameObject screen)
    {
        GameScreen.SetActive (false);
        LevelFailedScreen.SetActive (false);
        LevelCompleteScreen.SetActive (false);
        LevelSelectScreen.SetActive (false);

        screen.SetActive (true);
    }

    private void OnDestroy ()
    {
        EventManager.OnGameStateChanged -= GameStateChanged;
    }
}
