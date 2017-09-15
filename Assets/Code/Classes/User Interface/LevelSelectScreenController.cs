using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu ("Extended/UI/Level Select Screen")]
public class LevelSelectScreenController : MonoBehaviour
{
    public void OpenLevel (string level)
    {
        SceneManager.LoadScene (level);
    }

    public void Menu ()
    {
        EventManager.MenuStateChanged (MenuStates.Menu);
    }
}
