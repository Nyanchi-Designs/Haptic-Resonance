using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[AddComponentMenu ("Extended/UI/Game/Level Complete Screen")]
public class LevelCompleteScreenController : MonoBehaviour
{
    [SerializeField] private Text _FinalScoreLabel = null;

    private void Awake ()
    {
        EventManager.OnScoreChanged += ScoreChanged;
    }

    private void ScoreChanged (int score, bool isCaller)
    {
        _FinalScoreLabel.text = "Final Score: " + score;
    }

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

    private void OnDestroy ()
    {
        EventManager.OnScoreChanged -= ScoreChanged;
    }
}
