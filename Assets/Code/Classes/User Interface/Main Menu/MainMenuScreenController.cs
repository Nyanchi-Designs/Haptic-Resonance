using UnityEngine;

[AddComponentMenu ("Extended/UI/Menu/Main Menu Screen")]
public class MainMenuScreenController : MonoBehaviour
{
    public void LevelSelect ()
    {
        EventManager.MenuStateChanged (MenuStates.LevelSelect);
    }

    public void Scoreboard ()
    {
        EventManager.MenuStateChanged (MenuStates.Scoreboard);
    }

    public void Settings ()
    {
        EventManager.MenuStateChanged (MenuStates.Settings);
    }

    public void Credits ()
    {
        EventManager.MenuStateChanged (MenuStates.Credits);
    }

    public void Exit ()
    {
        Application.Quit ();
    }
}
