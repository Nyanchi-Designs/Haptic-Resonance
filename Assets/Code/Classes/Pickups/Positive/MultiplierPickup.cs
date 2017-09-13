using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Multiplier")]
public class MultiplierPickup : Collectable
{
    [SerializeField] private int _Multiplier = 2;

    protected override void Collected ()
    {
    }
}
