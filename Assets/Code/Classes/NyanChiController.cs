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

    private void FixedUpdate ()
    {
        if (_NyanPrefab.IsObstacle () || _ChiPrefab.IsObstacle ())
            return;

        _WC.MoveWorld ();
        _NyanPrefab.ApplyMovement ();
        _ChiPrefab.ApplyMovement ();
    }
}
