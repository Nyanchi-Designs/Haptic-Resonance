using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Multiplier")]
public class MultiplierPickup : Collectable
{
    [Tooltip ("What score multiplier is awarded to the player's collected score?")]
    [SerializeField] private int _Multiplier = 2;
    [Tooltip ("How long will this pickup's effect last on the player?")]
    [SerializeField] private float _Duration = 1f;

    protected override void Collected ()
    {
    }
}
