using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Collider2D))]
public abstract class Collectable : MonoBehaviour
{
    [Tooltip ("How much is the player awarded upon collecting this.")]
    [SerializeField] protected int _Score = 0;

    protected Collider2D _Other = null;

    private void Awake ()
    {
        GetComponent<Collider2D> ().isTrigger = true;
    }

    private void OnTriggerEnter2D (Collider2D other)
    {
        if (other.CompareTag ("Player"))
        {
            _Other = other;
            Collected ();
        }
    }

    protected virtual void Collected ()
    {
        EventManager.ScoreChanged (_Score, false);
        this.gameObject.SetActive (false);
    }
}
