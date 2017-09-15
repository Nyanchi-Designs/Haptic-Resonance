using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Speed Boost")]
public class SpeedBoostPickup : TimedCollectable
{
    [Tooltip ("What is the player's speed when boosted?")]
    [SerializeField] private float _BoostedSpeed = 7f;
    [Tooltip ("How many bonus points are awarded to the player's collected score whilst this effect is active?")]
    [SerializeField] private int _PassiveBonus = 2;

    protected override void Collected ()
    {
        base.Collected ();
        EventManager.SpeedChanged (_BoostedSpeed);
    }

    protected override void EndOfEffect ()
    {
        EventManager.SpeedChanged (5f);
        Destroy (this.gameObject);
    }
}
