using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Stunted Jump")]
public class StuntedJumpPickup : Collectable
{
    [Tooltip ("How long will this pickup's effect last on the player?")]
    [SerializeField] private float _Duration = 1f;
    [Tooltip ("How many bonus points are awarded to the player's collected score whilst this effect is active?")]
    [SerializeField] private int _Bonus = 1;

    protected override void Collected ()
    {
    }
}
