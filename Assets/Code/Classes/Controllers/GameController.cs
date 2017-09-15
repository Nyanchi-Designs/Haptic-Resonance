using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
    [Tooltip ("The players current score total.")]
    [ReadOnly] public int Score = 0;
    [Tooltip ("The current multiplier added to the total score increase.")]
    [ReadOnly] public int Multiplier = 1;
    [Tooltip ("How much is the player's score awarded passively?")]
    [SerializeField] private int _PassiveAmount = 3;
    [Tooltip ("How long is the wait between passive score awards.")]
    [SerializeField] private float _PassiveAwardTime = 1f;

    private void Awake ()
    {
        EventManager.OnScoreChanged += ScoreChanged;
    }

    private void Start ()
    {
        StartCoroutine (IncreaseScore ());
    }

    private IEnumerator IncreaseScore ()
    {
        yield return new WaitForSeconds (_PassiveAwardTime);
        EventManager.ScoreChanged (_PassiveAmount, false);

        StopAllCoroutines ();
        StartCoroutine (IncreaseScore ());
    }

    private void ScoreChanged (int score, bool isCaller)
    {
        if(!isCaller)
        {
            Score += score * Multiplier;
            EventManager.ScoreChanged (Score, true);
        }
    }

    private void OnDestroy ()
    {
        EventManager.OnScoreChanged -= ScoreChanged;
    }
}
