using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Slowdown")]
public class SlowDownPickup : Collectable
{
    [SerializeField] private float _SlowedSpeed = 1f;
    [SerializeField] private float _Duration = 1f;
    [SerializeField] private int _Penalty = 2;

    protected override void Collected ()
    {
    }
}
