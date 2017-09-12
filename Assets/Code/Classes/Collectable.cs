using UnityEngine;
using System.Collections;

public abstract class Collectable : MonoBehaviour
{
    [Tooltip ("How much is the player awarded upon collecting this.")]
    [SerializeField] protected int _Score = 0;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag ("Player"))
            Collected ();
    }

    protected abstract void Collected ();
}
