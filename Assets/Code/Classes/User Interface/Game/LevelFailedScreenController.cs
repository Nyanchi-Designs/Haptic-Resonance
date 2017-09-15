using UnityEngine;
using UnityEngine.SceneManagement;

[AddComponentMenu ("Extended/UI/Game/Level Failed Screen")]
public class LevelFailedScreenController : MonoBehaviour
{
    public void Retry ()
    {
        SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
    }

    public void LevelSelect ()
    {
        EventManager.GameStateChanged (GameStates.LevelSelect);
    }

    public void Menu ()
    {
        SceneManager.LoadScene ("Main Menu");
    }
}
