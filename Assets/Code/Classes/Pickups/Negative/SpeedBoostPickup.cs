using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Speed Boost")]
public class SpeedBoostPickup : TimedCollectable
{
    [Tooltip ("What is the player's speed when boosted?")]
    [SerializeField] private float _BoostedSpeed = 1f;
    [Tooltip ("How many bonus points are awarded to the player's collected score whilst this effect is active?")]
    [SerializeField] private int _Bonus = 2;

    protected override void Collected ()
    {
        base.Collected ();
    }

    protected override void EndOfEffect ()
    {
    }
}
