using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Multiplier")]
public class MultiplierPickup : TimedCollectable
{
    [Tooltip ("What score multiplier is awarded to the player's collected score?")]
    [SerializeField] private int _Multiplier = 2;

    protected override void Collected ()
    {
        base.Collected ();
    }

    protected override void EndOfEffect ()
    {
    }
}
