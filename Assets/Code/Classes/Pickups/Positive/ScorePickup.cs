using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Score")]
public class ScorePickup : Collectable
{
    protected override void Collected ()
    {
        base.Collected ();
        Destroy (this.gameObject);
    }
}
