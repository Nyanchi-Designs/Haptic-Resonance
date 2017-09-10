using UnityEngine;
using System.Collections;

//TODO: Consider spawning in Nyan and chi.
public class NyanChiController : MonoBehaviour
{
    [SerializeField] private PlayerController _NyanPrefab = null;
    [SerializeField] private PlayerController _ChiPrefab = null;

    private WorldController _WC = null;

    private void Awake ()
    {
        _WC = GameObject.FindGameObjectWithTag ("GameController").GetComponent<WorldController> ();
    }

    private void Update ()
    {
        GetInput ();
    }

    private void FixedUpdate ()
    {
        _NyanPrefab.ApplyMovement ();
        _ChiPrefab.ApplyMovement ();

        if (_NyanPrefab.IsObstacle () || _ChiPrefab.IsObstacle ())
        {

        }
        else
            _WC.MoveWorld ();
    }
    
    private void GetInput ()
    {
        ButtonInput ();
        TouchInput ();
    }

    private void ButtonInput ()
    {
        if (Input.GetKeyUp (KeyCode.A) || Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetButtonDown ("Left"))
            _NyanPrefab.Jump ();

        if (Input.GetKeyUp (KeyCode.D) || Input.GetKeyUp (KeyCode.RightArrow) || Input.GetButtonDown ("Right"))
            _ChiPrefab.Jump ();
    }

    private void TouchInput ()
    {
        if(Input.touchCount > 0)
        {
            var touch = Input.GetTouch (0);

            if (touch.position.x < Screen.width / 2)
                _NyanPrefab.Jump ();
            else if (touch.position.x > Screen.width / 2)
                _ChiPrefab.Jump ();
        }
    }
}
