using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Slowdown")]
public class SlowDownPickup : TimedCollectable
{
    [Tooltip ("What is the player's speed when slowed down?")]
    [SerializeField] private float _SlowedSpeed = 4f;
    [Tooltip ("How many penalty points are taken from the player's collected score whilst this effect is active?")]
    [SerializeField] private int _PassivePenalty = 2;

    protected override void Collected ()
    {
        base.Collected ();
        EventManager.SpeedChanged (_SlowedSpeed);
    }

    protected override void EndOfEffect ()
    {
        EventManager.SpeedChanged (5f);
        Destroy (this.gameObject);
    }
}
