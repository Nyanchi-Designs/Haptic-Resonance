using UnityEngine;

public class WorldController : MonoBehaviour
{
    [Tooltip ("The speed at which the world scrolls across the screen toward the player.")]
    [SerializeField] private float _ScrollSpeed = 5f;
    [Tooltip ("The GameObject containing the world hierarchy to move.")]
    [SerializeField] private Transform _World = null;

    private bool _CanMove = true;

    private void Awake()
    {
        EventManager.OnObstacleHit += ObstacleHit;
    }

    private void Update ()
    {
        // If there is no obstacle in the way then move the world.
        if (_CanMove)
            _World.Translate (Vector2.left * _ScrollSpeed * Time.deltaTime);
    }

    private void ObstacleHit (bool hit)
    {
        if (hit)
            _CanMove = false;
        else
            _CanMove = true;
    }

    private void OnDestroy()
    {
        EventManager.OnObstacleHit -= ObstacleHit;
    }
}
