using UnityEngine;
using System.Collections;
using Assets.Code.Classes;

//TODO: Consider making each character have it's own component for controlling behaviour.
[AddComponentMenu ("Extended/Controllers/Player Controller")]
public class PlayerController : ExtendedMonoBehaviour
{
    [Tooltip ("How high is the character able to jump before falling again?")]
    [SerializeField] public float JumpHeight = 25f;
    [Tooltip ("How heavy is the pull affecting the characters fall? (The higher the value, the faster the fall and the more force required to push.)")]
    [SerializeField] private float _Gravity = -7.0f;
    [Tooltip ("The speed at which the characters move across the world.")]
    [SerializeField] private float _MovementSpeed = 20.0f;
    [Tooltip ("The speed at which the character will move to attempt to catch up.")]
    [SerializeField] private float _CatchupSpeed = 25.0f;
    [Tooltip ("What layers does the character recognise as jumpable ground?")]
    [SerializeField] private LayerMask _GroundLayers;
    [Tooltip ("What layers does the character recognise as stoppable obstacles?")]
    [SerializeField] private LayerMask _ObstacleLayers;
    [Tooltip ("The prefab of the Nyan character with it's necessary components.")]
    [SerializeField] private Rigidbody2D _Nyan = null;
    [Tooltip ("The prefab of the Chi character with it's necessary components.")]
    [SerializeField] private Rigidbody2D _Chi = null;

    [SerializeField] private Vector3 _StartPosition = Vector3.zero;

    protected override void Awake()
    {
        _Nyan = Instantiate (_Nyan.gameObject, new Vector3 (0.0f, 3f, 0.0f), Quaternion.identity).GetComponent<Rigidbody2D> ();
        _Chi = Instantiate (_Chi.gameObject, new Vector3 (0.0f, -3f, 0.0f), Quaternion.identity).GetComponent<Rigidbody2D> ();
        _Nyan.tag = "Player";
        _Chi.tag = "Player";

        _StartPosition = _Nyan.transform.position;

        EventManager.OnGameStateChanged += OnGameStateChanged;
    }

    protected override void OnDestroy ()
    {
        EventManager.OnGameStateChanged -= OnGameStateChanged;
    }

    private void Start ()
    {
        SetDefaults ();
    }

    private void SetDefaults ()
    {
        _Nyan.gravityScale = -_Gravity;
        _Nyan.isKinematic = false;
        _Nyan.constraints = RigidbodyConstraints2D.FreezeRotation;
        _Chi.gravityScale = _Gravity;
        _Chi.isKinematic = false;
        _Chi.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    protected override void Update ()
    {
        base.Update ();
        DebugLines ();
        GetInput ();
        Move ();

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

    private void Move ()
    {
        // Newer movment which allows them to move back to original starting position by using the camera position as an anchor. 
        //TODO: Expose the player offset from the camera position so that a designer can tweak the offset between them in unity.
        
        if (Approximately (_Nyan.position.x, Camera.main.transform.position.x - 5.0f, 0.075f) == true)
        {
            _Nyan.velocity = new Vector2 (_MovementSpeed * 10 * Time.fixedDeltaTime, _Nyan.velocity.y);
        }
        else if (_Nyan.position.x < Camera.main.transform.position.x - 5.0f && Approximately (_Nyan.position.x, Camera.main.transform.position.x - 5.0f, 0.075f) == false)
        {
            _Nyan.velocity = new Vector2 (_CatchupSpeed * 10 * Time.fixedDeltaTime, _Nyan.velocity.y);
        }

        if (Approximately (_Chi.position.x, Camera.main.transform.position.x - 5.0f, 0.075f) == true)
        {
            _Chi.velocity = new Vector2 (_MovementSpeed * 10 * Time.fixedDeltaTime, _Chi.velocity.y);
        }
        else if (_Chi.position.x < Camera.main.transform.position.x - 5.0f && Approximately (_Chi.position.x, Camera.main.transform.position.x - 5.0f, 0.075f) == false)
        {
            _Chi.velocity = new Vector2 (_CatchupSpeed * 10 * Time.fixedDeltaTime, _Chi.velocity.y);
        }

        // Original movement which moves their position further and further back over time.

        ////NYAN
        //if (Approximately (_Nyan.position.x, _Chi.position.x, 0.075f) == true || _Nyan.position.x > _Chi.position.x)
        //{
        //    print ("Nya CAught up");
        //    _Nyan.velocity = new Vector2 (_MovementSpeed * 10 * Time.fixedDeltaTime, _Nyan.velocity.y);
        //}
        
        //if (_Nyan.position.x < _Chi.position.x && Approximately (_Nyan.position.x, _Chi.position.x, 0.075f) == false)
        //{
        //    _Nyan.velocity = new Vector2 (_CatchupSpeed * 10 * Time.fixedDeltaTime, _Nyan.velocity.y);
        //}

        ////CHI
        //if (Approximately (_Chi.position.x, _Nyan.position.x, 0.075f) == true || _Nyan.position.x > _Chi.position.x)
        //{
        //    print ("Chi CAught up");
        //    _Chi.velocity = new Vector2 (_MovementSpeed * 10 * Time.fixedDeltaTime, _Chi.velocity.y);
        //}

        //if (_Chi.position.x < _Nyan.position.x && Approximately (_Nyan.position.x, _Chi.position.x, 0.075f) == false)
        //{
        //    _Chi.velocity = new Vector2 (_CatchupSpeed * 10 * Time.fixedDeltaTime, _Chi.velocity.y);
        //}
    }

    private bool Approximately (float a, float b, float tolerance)
    {
        return (Mathf.Abs (a - b) < tolerance);
    }

    private void Jump (Rigidbody2D character)
    {
        if (character == _Nyan && IsGrounded (_Nyan))
            _Nyan.AddForce (Vector2.up * JumpHeight, ForceMode2D.Impulse);

        if (character == _Chi && IsGrounded (_Chi))
            _Chi.AddForce (Vector2.up * -JumpHeight, ForceMode2D.Impulse);
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

    private void OnGameStateChanged (GameStates gameState)
    {
        switch (gameState)
        {
            case GameStates.GameLoop:
                this._ShouldUpdate = true;
                break;
            case GameStates.LevelFailed:
                this._ShouldUpdate = false;
                break;
            case GameStates.LevelComplete:
                this._ShouldUpdate = false;
                break;
            case GameStates.LevelSelect:
                this._ShouldUpdate = false;
                break;
            case GameStates.Paused:
                this._ShouldUpdate = false;
                break;
            default:
                break;
        }
    }
}
