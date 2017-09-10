using UnityEngine;
using System.Collections;

public class WorldController : MonoBehaviour
{
    public bool CanMove = true;

    [Tooltip("The speed of the world's scrolling movement.")]
    [SerializeField] private float _ScrollSpeed = 0.0f;
    [Tooltip("The gameobject to control as the world.")]
    [SerializeField] private Transform _World = null;

    public void MoveWorld ()
    {
        if(_World != null && CanMove)
            _World.Translate (Vector3.left * _ScrollSpeed * Time.deltaTime);
    }
}
