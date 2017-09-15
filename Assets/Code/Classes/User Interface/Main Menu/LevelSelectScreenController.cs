using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu ("Extended/UI/Menu/Level Select Screen")]
public class LevelSelectScreenController : MonoBehaviour
{
    public void OpenLevel (int level)
    {
        SceneManager.LoadScene (level);
    }

    public void Menu ()
    {
        EventManager.MenuStateChanged (MenuStates.Menu);
    }
}
