using UnityEngine;

[AddComponentMenu ("Extended/UI/Menu/Menu UI Controller")]
public class MenuUIController : MonoBehaviour
{
    [Tooltip ("The screen responsible for the main menu behaviour.")]
    public GameObject MenuScreen = null;
    [Tooltip ("The screen responsible for the level select behaviour.")]
    public GameObject LevelSelectScreen = null;
    [Tooltip ("The screen responsible for showing the scoreboard to the player.")]
    public GameObject ScoreScreen = null;
    [Tooltip ("The screen responsible for handling the game settings.")]
    public GameObject SettingsScreen = null;
    [Tooltip ("The screen responsible for displaying the credit information.")]
    public GameObject CreditsScreen = null;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        EventManager.OnMenuStateChanged += MenuStateChanged;
    }

    private void Start ()
    {
        EventManager.MenuStateChanged (MenuStates.Menu);
    }

    private void MenuStateChanged (MenuStates state)
    {
        switch (state)
        {
            case MenuStates.Menu:
                ChangeScreen (MenuScreen);
                break;
            case MenuStates.LevelSelect:
                ChangeScreen (LevelSelectScreen);
                break;
            case MenuStates.Scoreboard:
                ChangeScreen (ScoreScreen);
                break;
            case MenuStates.Settings:
                ChangeScreen (SettingsScreen);
                break;
            case MenuStates.Credits:
                ChangeScreen (CreditsScreen);
                break;
        }
    }

    private void ChangeScreen (GameObject screen)
    {
        MenuScreen.SetActive (false);
        LevelSelectScreen.SetActive (false);
        ScoreScreen.SetActive (false);
        SettingsScreen.SetActive (false);
        CreditsScreen.SetActive (false);

        screen.SetActive (true);
    }

    private void OnDestroy ()
    {
        EventManager.OnMenuStateChanged -= MenuStateChanged;
    }
}
