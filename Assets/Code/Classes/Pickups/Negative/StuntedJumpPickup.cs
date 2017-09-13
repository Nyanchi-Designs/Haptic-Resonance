using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Stunted Jump")]
public class StuntedJumpPickup : Collectable
{
    [SerializeField] private float _Duration = 1f;
    [SerializeField] private int _Bonus = 1;

    protected override void Collected ()
    {
    }
}
