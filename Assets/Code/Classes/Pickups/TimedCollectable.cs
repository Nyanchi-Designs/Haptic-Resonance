using UnityEngine;
using System.Collections;

public abstract class TimedCollectable : Collectable
{
    [Tooltip ("How long will this pickup's effect last on the player?")]
    [SerializeField] protected float _Duration = 1f;

    protected override void Collected ()
    {
        Invoke ("EndOfEffect", _Duration);
    }

    protected abstract void EndOfEffect ();

    protected void OnDestroy ()
    {
        CancelInvoke ("EndOfEffect");
    }
}
