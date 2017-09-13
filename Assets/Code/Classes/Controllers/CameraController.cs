using UnityEngine;
using System.Collections;

//TODO: Think of better name for scroll speed.
[AddComponentMenu ("Extended/Controllers/Camera Controller")]
public class CameraController : MonoBehaviour
{
    [Tooltip ("The speed of the camera's movement.")]
    [SerializeField] private float _ScrollSpeed = 3f;

    private Transform _Transform = null;

    private void Awake()
    {
        _Transform = GetComponent<Transform> ();
        EventManager.OnObstacleHit += ObstacleHit;
    }

    private IEnumerator MoveTo (Vector3 position, float speed)
    {
        var startPos = _Transform.position;
        var elapsedTime = 0.0f;

        while (elapsedTime < speed)
        {
            _Transform.position = Vector3.Lerp (startPos, position, elapsedTime / speed);
            elapsedTime += Time.fixedDeltaTime;

            yield return new WaitForEndOfFrame ();
        }

        StopAllCoroutines ();
    }

    private void ObstacleHit (bool hit)
    {
        if (hit)
        {
            // Scroll to the right of the screen.
            StopAllCoroutines ();
            StartCoroutine (MoveTo (new Vector3 (15.0f, _Transform.position.y, _Transform.position.z), _ScrollSpeed));
        }
        else
            // Scroll back to the center area of the screen.
            StartCoroutine (MoveTo (new Vector3 (0.0f, _Transform.position.y, _Transform.position.z), 5f));
    }

    private void OnDestroy ()
    {
        EventManager.OnObstacleHit -= ObstacleHit;
    }
}
