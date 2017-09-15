using UnityEngine;

[AddComponentMenu ("Extended/UI/Menu/Scoreboard Screen")]
public class ScoreboardScreenController : MonoBehaviour
{
    public void Menu ()
    {
        EventManager.MenuStateChanged (MenuStates.Menu);
    }
}
