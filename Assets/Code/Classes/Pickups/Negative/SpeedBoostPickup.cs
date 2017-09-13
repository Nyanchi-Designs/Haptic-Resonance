using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Speed Boost")]
public class SpeedBoostPickup : Collectable
{
    [SerializeField] private float _BoostedSpeed = 1f;
    [SerializeField] private float _Duration = 1f;
    [SerializeField] private int _Bonus = 2;

    protected override void Collected ()
    {
    }
}
