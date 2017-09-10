using UnityEngine;
using System.Collections;

//TODO: Clean up character code.
[RequireComponent (typeof (CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Tooltip ("The height of the player's jump. (Use negative values for underneath and postive for above.)")]
    [SerializeField] private float _JumpHeight = 0.5f;
    [Tooltip ("The gravity affecting the player's fall speed and force required to push off the ground. (Use postive values for underneath and negative for above.)")]
    [SerializeField] private float _Gravity = -1.0f;
    [Tooltip ("What objects does the player recognise as ground?")]
    [SerializeField] private LayerMask _GroundLayers;
    [Tooltip ("What objects does the player recognise as floors?")]
    [SerializeField] private LayerMask _ObstacleLayers;
    [Tooltip ("Is this character upside down?")]
    [SerializeField] private bool _Invert = false;

    private float _YVelocity = 0.0f;
    private Vector3 _MovVelocity = Vector3.zero;
    private Transform _Transform = null;
    private CharacterController _CC = null;

    private void Awake ()
    {
        AssignReferences ();
    }

    private void AssignReferences ()
    {
        _Transform = GetComponent<Transform> ();
        _CC = GetComponent<CharacterController> ();
    }

    private void Update ()
    {
        Move ();
    }

    public void ApplyMovement ()
    {
        ApplyGravity ();
    }

    private void ApplyGravity ()
    {
        if (!CanJump ())
            _YVelocity += _Gravity * Time.fixedDeltaTime;
        else if (CanJump ())
            _YVelocity = 0.0f;
    }

    public void Jump ()
    {
        if (CanJump ())
            _YVelocity = _JumpHeight;
    }

    private bool CanJump ()
    {
        if(_Invert)
            if (Physics.Linecast (_Transform.position, new Vector3 (_Transform.position.x, _Transform.position.y + 1.1f, _Transform.position.z), _GroundLayers))
                return true;

        if (Physics.Linecast (_Transform.position, new Vector3 (_Transform.position.x, _Transform.position.y - 1.1f, _Transform.position.z), _GroundLayers))
            return true;

        return false;
    }

    private void Move ()
    {
        _CC.Move (new Vector3 (_MovVelocity.x, _YVelocity, 0.0f));
    }

    public bool IsObstacle ()
    {
        // Head check.
        if (Physics.Linecast (_Transform.position, new Vector3 (_Transform.position.x + .55f, _Transform.position.y + 1f, _Transform.position.z), _ObstacleLayers))
            return true;

        // Mid check.
        if (Physics.Linecast (_Transform.position, new Vector3 (_Transform.position.x + .55f, _Transform.position.y - 1f, _Transform.position.z), _ObstacleLayers))
            return true;

        // Feet check.
        if ((Physics.Linecast (_Transform.position, new Vector3 (_Transform.position.x + .55f, _Transform.position.y, _Transform.position.z), _ObstacleLayers)))
            return true;

        return false;
    }
}
