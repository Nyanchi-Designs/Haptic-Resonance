using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu ("Extended/UI/Game/Game Screen")]
public class GameScreenController : MonoBehaviour
{
    [SerializeField] private Text _ScoreLabel = null;

    private void Awake ()
    {
        EventManager.OnScoreChanged += ScoreChanged;
    }

    private void ScoreChanged (int score, bool isCaller)
    {
        _ScoreLabel.text = "Score: " + score;
    }

    private void OnDestroy ()
    {
        EventManager.OnScoreChanged -= ScoreChanged;
    }
}
