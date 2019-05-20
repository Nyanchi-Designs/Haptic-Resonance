using UnityEngine;
using System.Collections;

//TODO: Think of better name for scroll speed.
//TODO: Consider moving normal movement into Update again.
//TODO: Get scroll speed as a percentage of the current world speed.
[AddComponentMenu ("Extended/Controllers/Camera Controller")]
public class CameraController : MonoBehaviour
{
    private float _ScrollSpeed = 0f;
    private float _CenteringSpeed = 5f;
    private Transform _Transform = null;

    private void Awake()
    {
        _Transform = GetComponent<Transform> ();

        EventManager.OnSpeedChanged += SpeedChanged;
        EventManager.OnObstacleHit += ObstacleHit;
    }

    private void SpeedChanged (float speed)
    {
        _ScrollSpeed = speed - 2.5f;
        _CenteringSpeed = speed;
    }

    private void ObstacleHit (bool hit)
    {
        //if (hit)
        //{
        //    // Stop all previous scrolling.
        //    StopAllCoroutines ();
        //    // Scroll to the right of the screen.
        //    StartCoroutine (Move (new Vector3 (50.0f, _Transform.position.y, _Transform.position.z), _ScrollSpeed));
        //}
        //else
        //{
            StopAllCoroutines ();
            // Scroll back to the center area of the screen.
            StartCoroutine (MoveTo (new Vector3 (0.0f, _Transform.position.y, _Transform.position.z), _CenteringSpeed));
        //}
    }

    private IEnumerator Move (Vector3 position, float speed)
    {
        var startPos = _Transform.position;
        var elapsedTime = 0.0f;

        while (elapsedTime < 5f)
        {
            _Transform.Translate (Vector2.right * speed * Time.deltaTime);
            elapsedTime += Time.fixedDeltaTime;

            yield return new WaitForEndOfFrame ();
        }

        StopAllCoroutines ();
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

    private void OnDestroy ()
    {
        EventManager.OnSpeedChanged -= SpeedChanged;
        EventManager.OnObstacleHit -= ObstacleHit;
    }
}
