using UnityEngine;

[AddComponentMenu ("Extended/Collectables/Stunted Jump")]
public class StuntedJumpPickup : TimedCollectable
{
    [Tooltip ("How many bonus points are awarded to the player's collected score whilst this effect is active?")]
    [SerializeField] private int _PassiveBonus = 1;
    [SerializeField] private float _JumpHeight = 1.25f;

    private PlayerController _Player = null;

    protected override void Collected ()
    {
        base.Collected ();
        
        EventManager.PassiveAmountChanged (_PassiveBonus);
        EventManager.ChangeJumpHeight (false, _JumpHeight, _Other.gameObject);
    }

    protected override void EndOfEffect ()
    {
        EventManager.PassiveAmountChanged (0);
        EventManager.ChangeJumpHeight (true, 0.0f, _Other.gameObject);
        Destroy (this.gameObject);
    }
}
