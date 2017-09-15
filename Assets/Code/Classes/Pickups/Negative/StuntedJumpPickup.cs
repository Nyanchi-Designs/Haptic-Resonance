using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Stunted Jump")]
public class StuntedJumpPickup : TimedCollectable
{
    [Tooltip ("How many bonus points are awarded to the player's collected score whilst this effect is active?")]
    [SerializeField] private int _PassiveBonus = 1;

    private PlayerController _Player = null;

    protected override void Collected ()
    {
        base.Collected ();

        _Player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerController> ();
        _Player.JumpHeight = _Player.JumpHeight / 1.25f;
        
        EventManager.PassiveAmountChanged (_PassiveBonus);
    }

    protected override void EndOfEffect ()
    {
        _Player.JumpHeight = _Player.JumpHeight * 1.25f;
        EventManager.PassiveAmountChanged (0);
        Destroy (this.gameObject);
    }
}
