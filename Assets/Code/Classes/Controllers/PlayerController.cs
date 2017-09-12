using UnityEngine;
using System.Collections;

//TODO: Consider making each character have it's own component for controlling behaviour.
public class PlayerController : MonoBehaviour
{
    [Tooltip ("How high is the character able to jump before falling again?")]
    [SerializeField] private float _JumpHeight = 25f;
    [Tooltip ("How heavy is the pull affecting the characters fall? (The higher the value, the faster the fall and the more force required to push.)")]
    [SerializeField] private float _Gravity = -7.0f;
    [Tooltip ("What layers does the character recognise as jumpable ground?")]
    [SerializeField] private LayerMask _GroundLayers;
    [Tooltip ("What layers does the character recognise as stoppable obstacles?")]
    [SerializeField] private LayerMask _ObstacleLayers;
    [Tooltip ("The prefab of the Nyan character with it's necessary components.")]
    [SerializeField] private Rigidbody2D _Nyan = null;
    [Tooltip ("The prefab of the Chi character with it's necessary components.")]
    [SerializeField] private Rigidbody2D _Chi = null;

    private void Awake()
    {
        _Nyan = Instantiate (_Nyan.gameObject, new Vector3 (0.0f, 3f, 0.0f), Quaternion.identity).GetComponent<Rigidbody2D> ();
        _Chi = Instantiate (_Chi.gameObject, new Vector3 (0.0f, -3f, 0.0f), Quaternion.identity).GetComponent<Rigidbody2D> ();
        _Nyan.tag = "Player";
        _Chi.tag = "Player";
    }

    private void Start ()
    {
        SetDefaults ();
    }

    private void SetDefaults ()
    {
        _Nyan.gravityScale = -_Gravity;
        _Nyan.constraints = RigidbodyConstraints2D.FreezeRotation;
        _Chi.gravityScale = _Gravity;
        _Chi.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void Update ()
    {
        DebugLines ();
        GetInput ();

        // Keep characters at middle of world.
        _Nyan.position = new Vector2 (0.0f, _Nyan.position.y);
        _Chi.position = new Vector2 (0.0f, _Chi.position.y);

        if (HitObstacle (_Nyan) || HitObstacle (_Chi))
            EventManager.ObstacleHit (true);
        else
            EventManager.ObstacleHit (false);
    }

    private void DebugLines ()
    {
        // Grounded lines.
        Debug.DrawLine (_Nyan.transform.position, new Vector3 (_Nyan.transform.position.x, _Nyan.transform.position.y - 1.1f, 0.0f), Color.red);

        Debug.DrawLine (_Chi.transform.position, new Vector3 (_Chi.transform.position.x, _Chi.transform.position.y + 1.1f, 0.0f), Color.green);

        // Obstacle Check Lines.
        Debug.DrawLine (_Nyan.transform.position, new Vector3 (_Nyan.transform.position.x + 0.55f, _Nyan.transform.position.y + .5f, 0.0f), Color.red);
        Debug.DrawLine (_Nyan.transform.position, new Vector3 (_Nyan.transform.position.x + 0.55f, _Nyan.transform.position.y, 0.0f), Color.red);
        Debug.DrawLine (_Nyan.transform.position, new Vector3 (_Nyan.transform.position.x + 0.55f, _Nyan.transform.position.y - .5f, 0.0f), Color.red);

        Debug.DrawLine (_Chi.transform.position, new Vector3 (_Chi.transform.position.x + 0.55f, _Chi.transform.position.y + .5f, 0.0f), Color.green);
        Debug.DrawLine (_Chi.transform.position, new Vector3 (_Chi.transform.position.x + 0.55f, _Chi.transform.position.y, 0.0f), Color.green);
        Debug.DrawLine (_Chi.transform.position, new Vector3 (_Chi.transform.position.x + 0.55f, _Chi.transform.position.y - .5f, 0.0f), Color.green);
    }

    private void GetInput ()
    {
        ButtonInput ();
        TouchInput ();
    }

    private void ButtonInput ()
    {
        if (Input.GetKeyDown (KeyCode.A))
            Jump (_Nyan);
        if (Input.GetKeyDown (KeyCode.D))
            Jump (_Chi);
    }

    private void TouchInput ()
    {
        if(Input.touchCount == 1)
        {
            var firstTouch = Input.GetTouch (0);

            if(firstTouch.phase == TouchPhase.Began)
            {
                if (firstTouch.position.x < Screen.width / 2)
                Jump (_Nyan);

                if (firstTouch.position.x > Screen.width / 2)
                    Jump (_Chi);
            }
        }

        if(Input.touchCount > 1)
        {
            var firstTouch = Input.GetTouch (0);
            var secondTouch = Input.GetTouch (1);

            if(firstTouch.phase == TouchPhase.Began && secondTouch.phase == TouchPhase.Began)
            {
                if (firstTouch.position.x < Screen.width / 2)
                    Jump (_Nyan);

                if (firstTouch.position.x > Screen.width / 2)
                    Jump (_Chi);

                if (secondTouch.position.x < Screen.width / 2)
                    Jump (_Nyan);
                
                if (secondTouch.position.x > Screen.width / 2)
                    Jump (_Chi);
            }
        }
    }

    private void Jump (Rigidbody2D character)
    {
        if (character == _Nyan && IsGrounded (_Nyan))
            _Nyan.AddForce (Vector2.up * _JumpHeight, ForceMode2D.Impulse);

        if (character == _Chi && IsGrounded (_Chi))
            _Chi.AddForce (Vector2.up * -_JumpHeight, ForceMode2D.Impulse);
    }
    
    private bool IsGrounded(Rigidbody2D character)
    {
        if (character == _Nyan)
        {
            if (Physics2D.Linecast (_Nyan.transform.position, new Vector3 (_Nyan.transform.position.x, _Nyan.transform.position.y - 1.1f, 0.0f), _GroundLayers))
                return true;
        }

        if (character == _Chi)
        {
            if (Physics2D.Linecast (_Chi.transform.position, new Vector3 (_Chi.transform.position.x, _Chi.transform.position.y + 1.1f, 0.0f), _GroundLayers))
                return true;
        }

        return false;
    }

    private bool HitObstacle (Rigidbody2D character)
    {
        if (character == _Nyan)
        {
            if (Physics2D.Linecast (_Nyan.transform.position, new Vector3 (_Nyan.transform.position.x + 0.55f, _Nyan.transform.position.y + .5f, 0.0f), _ObstacleLayers))
                return true;
            if (Physics2D.Linecast (_Nyan.transform.position, new Vector3 (_Nyan.transform.position.x + 0.55f, _Nyan.transform.position.y, 0.0f), _ObstacleLayers))
                return true;
            if (Physics2D.Linecast (_Nyan.transform.position, new Vector3 (_Nyan.transform.position.x + 0.55f, _Nyan.transform.position.y - .5f, 0.0f), _ObstacleLayers))
                return true;
        }
        
        if (character == _Chi)
        {
            if (Physics2D.Linecast (_Chi.transform.position, new Vector3 (_Chi.transform.position.x + 0.55f, _Chi.transform.position.y + .5f, 0.0f), _ObstacleLayers))
                return true;
            if (Physics2D.Linecast (_Chi.transform.position, new Vector3 (_Chi.transform.position.x + 0.55f, _Chi.transform.position.y, 0.0f), _ObstacleLayers))
                return true;
            if (Physics2D.Linecast (_Chi.transform.position, new Vector3 (_Chi.transform.position.x + 0.55f, _Chi.transform.position.y - .5f, 0.0f), _ObstacleLayers))
                return true;
        }

        return false;
    }
}
