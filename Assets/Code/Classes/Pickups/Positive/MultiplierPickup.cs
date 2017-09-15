using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Multiplier")]
public class MultiplierPickup : TimedCollectable
{
    [Tooltip ("What score multiplier is awarded to the player's collected score?")]
    [SerializeField] private int _Multiplier = 2;

    private GameController _GameController = null;

    protected override void Collected ()
    {
        base.Collected ();

        _GameController = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
        _GameController.Multiplier = _Multiplier;
    }

    protected override void EndOfEffect ()
    {
        Destroy (this.gameObject);
        _GameController.Multiplier = 1;
    }
}
