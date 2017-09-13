using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Stunted Jump")]
public class StuntedJumpPickup : TimedCollectable
{
    [Tooltip ("What is the player's jump height whilst stunted?")]
    [SerializeField] private float _StuntedJumpHeight = .25f;
    [Tooltip ("How many bonus points are awarded to the player's collected score whilst this effect is active?")]
    [SerializeField] private int _Bonus = 1;

    protected override void Collected ()
    {
        base.Collected ();
    }

    protected override void EndOfEffect ()
    {
    }
}
