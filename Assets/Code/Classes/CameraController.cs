using UnityEngine;
using System.Collections;

//TODO: Think of better name for scroll speed.
//TODO: Move coroutine out of update loop as is a waste.
//TODO: Consider making the entire scrolling a corouting and move it out of update.
public class CameraController : MonoBehaviour
{
    [Tooltip ("The speed of the camera's movement.")]
    [SerializeField] private float _ScrollSpeed = 5f;
    public int RoutinesRunning = 0;

    private bool _CanMove = false;
    private bool _IsRunning = false;
    private Transform _Transform = null;

    private void Awake()
    {
        _Transform = GetComponent<Transform> ();
        EventManager.OnObstacleHit += ObstacleHit;
    }

    private void Update()
    {
        // If an obstacle was hit and the camera can move, start scrolling.
        if (_CanMove)
        {
            _Transform.Translate (_Transform.right * _ScrollSpeed * Time.fixedDeltaTime);
            StopAllCoroutines ();
            _IsRunning = false; 
        }
        // If no obstacle is hit, then start moving the camera back to the centre of the screen.
        else if (!_CanMove && _Transform.position.x >= 0.05f && !_IsRunning)
            StartCoroutine (MoveTo (new Vector3 (0.0f, _Transform.position.y, _Transform.position.z)));
    }

    private IEnumerator MoveTo (Vector3 position)
    {
        _IsRunning = true;
        var startPos = _Transform.position;
        var elapsedTime = 0.0f;

        while (elapsedTime < 5f)
        {
            _Transform.position = Vector3.Lerp (startPos, position, elapsedTime / 5f);
            elapsedTime += Time.deltaTime;

            yield return new WaitForEndOfFrame ();
        }

        StopAllCoroutines ();
        _IsRunning = false;
    }

    private void ObstacleHit (bool hit)
    {
        if (hit)
            _CanMove = true;
        else
            _CanMove = false;
    }

    private void OnDestroy()
    {
        EventManager.OnObstacleHit -= ObstacleHit;
    }
}
